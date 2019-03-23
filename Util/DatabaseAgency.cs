using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    public enum DatabaseType { MSSqlServer, SqLite };
    /// <summary>
    /// 数据库的代理类，负责执行不同数据库的相同操作。
    /// </summary>
    class DatabaseAgency /*<DbType> where DbType : DatabaseType*/
    {
        private DatabaseType dbType;

        /// <summary>
        /// 无参构造函数，默认SqlServer。
        /// 由于数据库类都使用单例进行保护，该类可以任意被创建
        /// </summary>
        public DatabaseAgency() : this(DatabaseType.MSSqlServer) { }

        public DatabaseAgency(DatabaseType databaseType)
        {
            dbType = databaseType;
        }

        public string DbFullName
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

        private object Instance
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

        public DatabaseType DBType { get { return dbType; } }

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


    #region 将枚举类封装功能
    //internal class DatabaseType //
    //{
    //    private enum EDatabaseType { MSSqlServer, SqLite };
    //    private EDatabaseType type;
    //    private DatabaseType(EDatabaseType dbType)
    //    {
    //        this.type = dbType;
    //    }
    //    public static DatabaseType MSSqlServer { get { return new DatabaseType(EDatabaseType.MSSqlServer); } }
    //    public static DatabaseType SqLite { get { return new DatabaseType(EDatabaseType.SqLite); } }
    //    public static bool operator == (DatabaseType t1,DatabaseType t2)
    //    {
    //        return t1.type == t2.type;
    //    }
    //    public static bool operator != (DatabaseType t1, DatabaseType t2)
    //    {
    //        return !(t1 == t2);
    //    }
    //}
    #endregion
}
