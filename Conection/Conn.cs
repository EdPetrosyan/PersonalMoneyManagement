﻿using System;
using System.Data;
using System.Data.SqlClient;


namespace Conection
{
    public class Conn
    {
        public static void CreateDatabase(SqlConnectionStringBuilder sqlConnection)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnection.ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand create = new SqlCommand(@"IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'PersonalMoneyManagment' ) CREATE DATABASE[PersonalMoneyManagment]", conn))
                    {
                        create.ExecuteNonQuery();
                    }
                    AddTables(conn);
                }
                catch (Exception e)
                {
                    throw;
                }
                finally { conn.Dispose(); }
            }
        }

        public static void AddTables(SqlConnection conn)
        {
            try
            {
                conn.Open();
                using (SqlCommand addCategoryTable = new SqlCommand(@"IF NOT EXIST (SELECT name FROM sys.tables WHERE name = N'Category') 
                                                CREATE TABLE Category 
                                                (
                                                    UNIQUEIDENTIFIER NOT NULL,
                                                    [Title] NVARCHAR (MAX)   NOT NULL,
                                                    CONSTRAINT [PK_Category] PRIMARY KEY 
                                                )"))
                {
                    addCategoryTable.ExecuteNonQuery();
                }
                using (SqlCommand addWalletTable = new SqlCommand(@"IF NOT EXIST (SELECT name FROM sys.tables WHERE name = N'Wallet') 
                                                CREATE TABLE [Wallet] (
                                                    [Id]          UNIQUEIDENTIFIER NOT NULL,
                                                    [CategoryId]  UNIQUEIDENTIFIER NOT NULL,
                                                    [Amount]      MONEY            NOT NULL,
                                                    [Comment]     NVARCHAR (MAX)   NULL,
                                                    [Day]         DATETIME2 (7)    NOT NULL,
                                                    [DateCreated] DATETIME2 (7)    CONSTRAINT [DF_Wallet_DateCreated] DEFAULT (getutcdate()) NOT NULL,
                                                    CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED ([Id] ASC),
                                                    CONSTRAINT [FK_Wallet_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
                                                );"))
                {
                    addWalletTable.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
            }

            
        }
    }
}
