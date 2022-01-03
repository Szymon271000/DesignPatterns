using System;
using System.Data;
using System.Data.Common;

namespace AbstractFactory
{
    class Program
    {
        interface DatabaseAbstractFactory
        {
            DbConnection CreateConnection(string connString);
            DbCommand CreateCommand(IDbConnection connection, string sql);
        }

        interface DBConnection
        {
            string ConnectionString { get; set; }
            void Open();
            void Close();
        }

        interface DbCommand
        {
            object Execute();
        }

        public class SQLServerFactory : DatabaseAbstractFactory
        {
            public DbCommand CreateCommand(IDbConnection connection, string sql)
            {
                return new SqlServerCommand(connection, sql);
            }

            public DbConnection CreateConnection(string connString)
            {
                return new SqlServerConnection(connString);
            }
        }

        class SqlServerConnection : DBConnection
        {
            private string ConnectionString { get; set; }

            public SqlServerConnection(string str) {
                ConnectionString = str;
            }

            public void Close()
            {
                Console.WriteLine("Close connection");
            }

            public void Open()
            {
                Console.WriteLine($"Open connection: {ConnectionString}");
            }

            class SqlServerCommand : DbCommand
            {
                private DbConnection connection { get; set; }
                private string sql { get; set; }

                public SqlServerCommand(DbConnection conn, string sql)
                {
                    this.connection = conn;
                    this.sql = sql;
                }
                public object Execute()
                {
                    connection.Open();
                    Console.WriteLine("Esecuzione: " + sql);
                    connection.Close();
                    return null;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
