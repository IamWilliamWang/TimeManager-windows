using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class SqlConnectionAgency
    {
        public enum DatabaseType { MSSqlServer, SqLite};
        private static DatabaseType dbType;
        public static string DbFullName
        { get
            {
                if (dbType == DatabaseType.MSSqlServer)
                    return SqlServerConnection.DbFullName;
                else if (dbType == DatabaseType.SqLite)
                    return SqliteConnection.DbFullName;
                else
                    return "";
            }
        }
        public static object Instance
        { get
            {
                if (dbType == DatabaseType.MSSqlServer)
                    return SqlServerConnection.Instance;
                else if (dbType == DatabaseType.SqLite)
                    return SqliteConnection.Instance;
                else
                    return null;
            }
        }
        public static DatabaseType DBType { get { return dbType; } }

        public ConnectionState ConnectionState
        { get
            {
                if (dbType == DatabaseType.MSSqlServer)
                    return SqlServerConnection.ConnectionState;
                else if (dbType == DatabaseType.SqLite)
                    return SqliteConnection.Instance.ConnectionState;
                else
                    return 0;
            }
        }

        public SqlConnectionAgency() : this(DatabaseType.MSSqlServer) { }

        public SqlConnectionAgency(DatabaseType databaseType)
        {
            dbType = databaseType;
        }

        public void OpenConnection()
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.OpenConnection();
                    break;
                case DatabaseType.SqLite:
                    SqliteConnection.Instance.OpenConnection();
                    break;
                default:
                    break;
            }
        }

        public void OpenConnection(String dbFullFilename)
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.OpenConnection(dbFullFilename);
                    break;
                case DatabaseType.SqLite:
                    SqliteConnection.Instance.OpenConnection(dbFullFilename);
                    break;
                default:
                    break;
            }
        }

        public void CloseConnection()
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.CloseConnection();
                    break;
                case DatabaseType.SqLite:
                    SqliteConnection.Instance.CloseConnection();
                    break;
                default:
                    break;
            }
        }

        public void DisposeConnection()
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.DisposeConnection();
                    break;
                case DatabaseType.SqLite:
                    SqliteConnection.Instance.DisposeConnection();
                    break;
                default:
                    break;
            }
        }

        public void ResetConnection()
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.ResetConnection();
                    break;
                case DatabaseType.SqLite:
                    SqliteConnection.Instance.ResetConnection();
                    break;
                default:
                    break;
            }
        }

        public Boolean ConnectionOpenned()
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    return SqlServerConnection.ConnectionOpenned();
                case DatabaseType.SqLite:
                    return SqliteConnection.Instance.ConnectionOpenned();
                default:
                    throw new ArgumentException();
            }
        }
        
        public DataTable ExecuteQuery(string selectCommandText)
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    return SqlServerConnection.ExecuteQuery(selectCommandText);
                case DatabaseType.SqLite:
                    return SqliteConnection.Instance.ExecuteQuery(selectCommandText);
                default:
                    throw new ArgumentException();
            }
        }

        public void ExecuteQueryUsingCache(string selectCommandText)
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.ExecuteQuery_delay(selectCommandText);
                    break;
                case DatabaseType.SqLite:
                    throw new NotImplementedException();
            }
        }

        public int ExecuteUpdate(string commandText)
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    return SqlServerConnection.ExecuteUpdate(commandText);
                case DatabaseType.SqLite:
                    return SqliteConnection.Instance.ExecuteUpdate(commandText);
                default:
                    throw new ArgumentException();
            }
        }

        public void ExecuteUpdateUsingCache(string commandText)
        {
            switch(dbType)
            {
                case DatabaseType.MSSqlServer:
                    SqlServerConnection.ExecuteUpdateUsingCache(commandText);
                    break;
                case DatabaseType.SqLite:
                    throw new NotImplementedException();
            }
        }

        public Boolean UpdateDatabase(DataTable dataTable)
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    return SqlServerConnection.UpdateDatabase(dataTable);
                case DatabaseType.SqLite:
                    return SqliteConnection.Instance.UpdateDatabase(dataTable);
                default:
                    throw new ArgumentException();
            }
        }

    }
}
