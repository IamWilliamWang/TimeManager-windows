using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 关机助手.Util
{
    class SqlUtil
    {
        static DatabaseAgency database = new DatabaseAgency();

        /// <summary>
        /// 简单查询功能。
        /// 例如arg0:[Table1]  arg1:Id  arg2:Id>10
        /// </summary>
        /// <param name="查询列名">要查询的列名，用逗号隔开</param>
        /// <param name="查询表名">要查询的表名</param>
        /// <param name="查询条件">满足的查询条件，可为空</param>
        public static DataTable Select(string 查询表名,string 查询列名,string 查询条件 = null)
        {
            return database.ExecuteQuery(Select_Sql(查询表名, 查询列名, 查询条件));
        }

        /// <summary>
        /// 返回查询语句
        /// 例如arg0:[Table1]  arg1:Id  arg2:Id>10
        /// </summary>
        /// <param name="查询列名">要查询的列名，用逗号隔开</param>
        /// <param name="查询表名">要查询的表名</param>
        /// <param name="查询条件">满足的查询条件，可为空</param>
        public static String Select_Sql(string 查询表名, string 查询列名, string 查询条件 = null)
        {
            if (查询列名.IndexOfAny(new char[] { '(', ')' }) != -1)
                查询列名 = 查询列名.Replace("(", "").Replace(")", "");
            if (查询条件 != null)
                return "select " + 查询列名 + " from " + 查询表名 + " where " + 查询条件;
            else
                return "select " + 查询列名 + " from " + 查询表名;
        }

        /// <summary>
        /// 插入一条数据功能
        /// 例如arg0:[Table]  arg1:1,'data'  arg2:Id,Content
        /// </summary>
        /// <param name="表名称">插入表的表名</param>
        /// <param name="各列的值">插入数据对应各列的值</param>
        /// <param name="列名称">插入数据对应列的值，可为空</param>
        /// <returns></returns>
        public static bool Insert(string 表名称,string 各列的值,string 列名称=null)
        {
            return database.ExecuteUpdate(Insert_Sql(表名称, 各列的值, 列名称))>0;
        }

        /// <summary>
        /// 返回插入语句
        /// 例如arg0:[Table]  arg1:1,'data'  arg2:Id,Content
        /// </summary>
        /// <param name="表名称">插入表的表名</param>
        /// <param name="各列的值">插入数据对应各列的值</param>
        /// <param name="列名称">插入数据对应列的值，可为空</param>
        /// <returns></returns>
        public static String Insert_Sql(string 表名称, string 各列的值, string 列名称 = null)
        {
            if (列名称 == null)
                return "insert into " + 表名称 + " values (" + 各列的值 + ")";
            else
            {
                if (列名称.IndexOfAny(new char[] { '(', ')' }) != -1)
                    列名称 = 列名称.Replace("(", "").Replace(")", "");
                return "insert into " + 表名称 + "(" + 列名称 + ") values (" + 各列的值 + ")";
            }
        }

        /// <summary>
        /// 更新一条数据
        /// 例如arg0:[Table]  arg1:Context  arg2:'New text.'  arg3:Id=2
        /// </summary>
        /// <param name="表名称">要更新的表名</param>
        /// <param name="列名称">要更新条的列名</param>
        /// <param name="新值">需要更新的新数据</param>
        /// <param name="更新条件">更新数据的条件</param>
        /// <returns></returns>
        public static bool Update(string 表名称,string 列名称,string 新值,string 更新条件=null)
        {
            return database.ExecuteUpdate(Update_Sql(表名称, 列名称, 新值, 更新条件)) == 1;
        }

        /// <summary>
        /// 返回更新一条数据语句
        /// 例如arg0:[Table]  arg1:Context  arg2:'New text.'  arg3:Id=2
        /// </summary>
        /// <param name="表名称">要更新的表名</param>
        /// <param name="列名称">要更新条的列名</param>
        /// <param name="新值">需要更新的新数据</param>
        /// <param name="更新条件">更新数据的条件</param>
        /// <returns></returns>
        public static String Update_Sql(string 表名称, string 列名称, string 新值, string 更新条件 = null)
        {
            if (更新条件 == null)
                return ("update " + 表名称 + " set " + 列名称 + "=" + 新值);
            else
                return ("update " + 表名称 + " set " + 列名称 + "=" + 新值 + " where " + 更新条件);
        }

        /// <summary>
        /// 更新多条数据
        /// 例如arg0:[Table]  arg1:Context,price  arg2:'Red',5.5  arg3:Id=2
        /// </summary>
        /// <param name="表名称">要更新的表名</param>
        /// <param name="列名称">要更新条的列名串组</param>
        /// <param name="新值">需要更新的新数据串组</param>
        /// <param name="更新条件">更新数据的条件</param>
        /// <returns></returns>
        public static bool Update(string 表名称, string[] 列名称, string[] 新值, string 更新条件 = null)
        {
            if (列名称.Length < 1)
                throw new ArgumentNullException("列名称不可为空");
            else if(列名称.Length==1)
                return Update(表名称, 列名称[0], 新值[0], 更新条件);

            if (更新条件 == null)
            {
                string sql = "update " + 表名称 + " set " + 列名称[0] + "=" + 新值[0];
                for (int index = 1; index < 列名称.Length; index++)
                    sql += "," + 列名称[index] + "=" + 新值[index];
                return database.ExecuteUpdate(sql) == 1;
            }
            else
            {
                string sql = "update " + 表名称 + " set " + 列名称[0] + "=" + 新值[0];
                for (int index = 1; index < 列名称.Length; index++)
                    sql += "," + 列名称[index] + "=" + 新值[index];
                sql += " where " + 更新条件;
                return database.ExecuteUpdate(sql) == 1;
            }
        }

        /// <summary>
        /// 返回更新多条数据语句
        /// 例如arg0:[Table]  arg1:Context,price  arg2:'Red',5.5  arg3:Id=2
        /// </summary>
        /// <param name="表名称">要更新的表名</param>
        /// <param name="列名称">要更新条的列名串组</param>
        /// <param name="新值">需要更新的新数据串组</param>
        /// <param name="更新条件">更新数据的条件</param>
        /// <returns></returns>
        public static String Update_Sql(string 表名称, string[] 列名称, string[] 新值, string 更新条件 = null)
        {
            if (列名称.Length < 1)
                throw new ArgumentNullException("列名称不可为空");
            else if (列名称.Length == 1)
                return Update_Sql(表名称, 列名称[0], 新值[0], 更新条件);

            if (更新条件 == null)
            {
                string sql = "update " + 表名称 + " set " + 列名称[0] + "=" + 新值[0];
                for (int index = 1; index < 列名称.Length; index++)
                    sql += "," + 列名称[index] + "=" + 新值[index];
                return sql;
            }
            else
            {
                string sql = "update " + 表名称 + " set " + 列名称[0] + "=" + 新值[0];
                for (int index = 1; index < 列名称.Length; index++)
                    sql += "," + 列名称[index] + "=" + 新值[index];
                sql += " where " + 更新条件;
                return sql;
            }
        }

        /// <summary>
        /// 简单删除功能
        /// 例如arg0:[Table] arg1:Id<50
        /// </summary>
        /// <param name="表名称">要删除内容的表名</param>
        /// <param name="更新条件">更新条件，可为空</param>
        /// <returns></returns>
        public static bool Delete(string 表名称,string 更新条件=null)
        {
            if (更新条件 != null)
                return database.ExecuteUpdate("delete from " + 表名称 + " where " + 更新条件) > 0;
            else
                return database.ExecuteUpdate("delete from " + 表名称) > 0;
        }
    }
}
