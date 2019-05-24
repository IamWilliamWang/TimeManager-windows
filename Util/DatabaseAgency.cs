using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    /// <summary>
    /// 数据库的代理类，负责执行不同数据库的相同操作。
    /// </summary>
    class DatabaseAgency /*<DbType> where DbType : DatabaseType*/
    {
        public DatabaseType DbType { get; }

        /// <summary>
        /// 无参构造函数，默认SqlServer。
        /// 由于数据库类都使用单例进行保护，该类可以任意被创建
        /// </summary>
        public DatabaseAgency() : this(DatabaseType.MSSqlServer) { }

        public DatabaseAgency(DatabaseType databaseType)
        {
            DbType = databaseType;
        }

        public string DbFullName
        { get
            {
                if (DbType == DatabaseType.MSSqlServer)
                    return SqlServerConnection.DbFullName;
                else if (DbType == DatabaseType.SqLite)
                    return SqliteConnection.DbFullName;
                else
                    return "";
            }
        }

        private object Instance
        { get
            {
                if (DbType == DatabaseType.MSSqlServer)
                    return SqlServerConnection.Instance;
                else if (DbType == DatabaseType.SqLite)
                    return SqliteConnection.Instance;
                else
                    return null;
            }
        }

        public ConnectionState ConnectionState
        { get
            {
                if (DbType == DatabaseType.MSSqlServer)
                    return SqlServerConnection.ConnectionState;
                else if (DbType == DatabaseType.SqLite)
                    return SqliteConnection.Instance.ConnectionState;
                else
                    return 0;
            }
        }

        public void OpenConnection()
        {
            if (DbType == DatabaseType.MSSqlServer)
                SqlServerConnection.OpenConnection();
            else if (DbType == DatabaseType.SqLite) 
                SqliteConnection.Instance.OpenConnection();
        }

        public void OpenConnection(String dbFullFilename)
        {
            if (DbType == DatabaseType.MSSqlServer)
                SqlServerConnection.OpenConnection(dbFullFilename);
            else if (DbType == DatabaseType.SqLite)
                SqliteConnection.Instance.OpenConnection(dbFullFilename);
        }

        public void CloseConnection()
        {
            if (DbType == DatabaseType.MSSqlServer)
                SqlServerConnection.CloseConnection();
            else if (DbType == DatabaseType.SqLite)
                SqliteConnection.Instance.CloseConnection();
        }

        public void DisposeConnection()
        {
            if (DbType == DatabaseType.MSSqlServer)
                SqlServerConnection.DisposeConnection();
            else if (DbType == DatabaseType.SqLite)
                SqliteConnection.Instance.DisposeConnection();
        }

        public void ResetConnection()
        {
            if (DbType == DatabaseType.MSSqlServer)
                SqlServerConnection.ResetConnection();
            else if (DbType == DatabaseType.SqLite)
                SqliteConnection.Instance.ResetConnection();
        }

        public Boolean ConnectionOpenned()
        {
            if (DbType == DatabaseType.MSSqlServer)
                return SqlServerConnection.ConnectionOpenned();
            else if (DbType == DatabaseType.SqLite)
                return SqliteConnection.Instance.ConnectionOpenned();
            else
                throw new ArgumentException();
            
        }
        
        public DataTable ExecuteQuery(string selectCommandText)
        {
            if (DbType == DatabaseType.MSSqlServer)
            	return SqlServerConnection.ExecuteQuery(selectCommandText);
            else if (DbType == DatabaseType.SqLite)
                return SqliteConnection.Instance.ExecuteQuery(selectCommandText);
            else
                throw new ArgumentException();
        }

        public void ExecuteQueryUsingCache(string selectCommandText)
        {
            if (DbType == DatabaseType.MSSqlServer)
            	SqlServerConnection.ExecuteQuery_delay(selectCommandText);
            else if (DbType == DatabaseType.SqLite)
                throw new NotImplementedException();
            else
                throw new ArgumentException();
        }

        public int ExecuteUpdate(string commandText)
        {
            if (DbType == DatabaseType.MSSqlServer)
                return SqlServerConnection.ExecuteUpdate(commandText);
            else if (DbType == DatabaseType.SqLite)
                return SqliteConnection.Instance.ExecuteUpdate(commandText);
            else
                throw new ArgumentException();
        }

        public void ExecuteUpdateUsingCache(string commandText)
        {
            if (DbType == DatabaseType.MSSqlServer)
                SqlServerConnection.ExecuteUpdateUsingCache(commandText);
            else if (DbType == DatabaseType.SqLite)
                throw new NotImplementedException();
        }

        public Boolean UpdateDatabase(DataTable dataTable)
        {
            if (DbType == DatabaseType.MSSqlServer)
                return SqlServerConnection.UpdateDatabase(dataTable);
            else if (DbType == DatabaseType.SqLite)
                return SqliteConnection.Instance.UpdateDatabase(dataTable);
            else
                throw new ArgumentException();
            
        }

        public int ClearCache()
        {
            if (this.DbType == DatabaseType.MSSqlServer)
                return SqlServerConnection.ClearCache();
            else
                throw new NotImplementedException();
        }
    }


    #region 将枚举类封装功能
    class DatabaseType //封装枚举类型可以查看具体调用情况
    {
        private enum EDatabaseType { MSSqlServer, SqLite };
        private EDatabaseType type;
        public DatabaseType() : this(EDatabaseType.MSSqlServer) { }
        private DatabaseType(EDatabaseType dbType)
        {
            this.type = dbType;
        }
        public static DatabaseType MSSqlServer { get { return new DatabaseType(EDatabaseType.MSSqlServer); } }
        public static DatabaseType SqLite { get { return new DatabaseType(EDatabaseType.SqLite); } }
        public static bool operator ==(DatabaseType t1, DatabaseType t2)
        {
            return t1.type == t2.type;
        }
        public static bool operator !=(DatabaseType t1, DatabaseType t2)
        {
            return t1.type != t2.type;
        }

        public override bool Equals(object obj)
        {
            return this.type == (obj as DatabaseType).type;
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode();
        }
    }
    //public enum DatabaseType { MSSqlServer, SqLite };
    #endregion
}
