using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SQLCommands
{
    class Commands
    {

        public static void InsertProcedure(string sqlconnection)
        {
            using (SqlConnection conn = new SqlConnection(sqlconnection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand insertProcedure = new SqlCommand(@"CREATE PROCEDURE pr_Insert
                                                                        @money  money = 0,
                                                                        @comment nvarchar(500),
                                                                        @day Datetime2,
                                                                        @dateCreated Datetime2,
                                                                        @title nvarchar(500)
                                                                        AS
                                                                        BEGIN

	                                                                        DECLARE @id Uniqueidentifier = newid()
	                                                                        INSERT INTO Wallet
	                                                                        VALUES (@id,@money,@comment,@day,@dateCreated)

	                                                                        INSERT INTO Category
	                                                                        VALUES(@id,@title)

	                                                                        SET NOCOUNT ON;
                                                                        END
                                                                        GO
                                                                        ", conn))
                    {

                        insertProcedure.ExecuteNonQuery();
                        conn.Dispose();
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
}
