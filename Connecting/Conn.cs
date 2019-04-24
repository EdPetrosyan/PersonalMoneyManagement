using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecting
{
    public class Conn
    {
        public static void CreateDatabase(string sqlConnection)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand create = new SqlCommand(@"IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'PersonalMoneyManagment' ) CREATE DATABASE[PersonalMoneyManagment]", conn))
                    {
                        create.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally { conn.Dispose(); }
            }
            using (SqlConnection conn = new SqlConnection(sqlConnection))
            {

                AddTables(conn);
            }
        }

        private static void AddTables(SqlConnection conn)
        {
            try
            {
                conn.Open();
                using (SqlCommand addCategoryTable = new SqlCommand(@"IF NOT EXISTS (select * from PersonalMoneyManagment.sys.tables WHERE name = N'Category') 
                                                CREATE TABLE [PersonalMoneyManagment].[dbo].[Category] 
                                                (
                                                    [id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                                                    [Title] NVARCHAR (MAX)   NOT NULL,
                                                )", conn))
                {
                    addCategoryTable.ExecuteNonQuery();
                }
                using (SqlCommand addWalletTable = new SqlCommand(@"IF NOT EXISTS (select * from PersonalMoneyManagment.sys.tables WHERE name = N'Wallet') 
                                                CREATE TABLE [PersonalMoneyManagment].[dbo].[Wallet] (
                                                    [Id]          UNIQUEIDENTIFIER NOT NULL,
                                                    [CategoryId]  UNIQUEIDENTIFIER NOT NULL,
                                                    [Amount]      MONEY            NOT NULL,
                                                    [Comment]     NVARCHAR (MAX)   NULL,
                                                    [Day]         DATETIME2 (7)    NOT NULL,
                                                    [DateCreated] DATETIME2 (7)    CONSTRAINT [DF_Wallet_DateCreated] DEFAULT (getutcdate()) NOT NULL,
                                                    CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED ([Id] ASC),
                                                    CONSTRAINT [FK_Wallet_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
                                                );", conn))
                {
                    addWalletTable.ExecuteNonQuery();
                }
                conn.Dispose();
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
