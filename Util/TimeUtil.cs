using System;
using System.Diagnostics;

namespace 关机助手.Util
{
    class TimeUtil
    {
        static Stopwatch _stopWatch;
        /// <summary>
        /// 登录数据库前使用Tik()开启秒表
        /// </summary>
        public static void Tik()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _stopWatch = stopWatch;
        }

        /// <summary>
        /// 停止秒表，并将秒数写入数据库
        /// </summary>
        /// <returns>写入是否成功</returns>
        public static bool Tok()
        {
            _stopWatch.Stop();
            TimeSpan timeSpan = _stopWatch.Elapsed;
            return new SqlConnectionAgency().ExecuteUpdate(WriteDBLogSQL(timeSpan)) != 0;
        }

        /// <summary>
        /// 停止秒表，并将秒数写入数据库
        /// </summary>
        /// <param name="writeErrorLog">出错时是否输出Log文件</param>
        public static void Tok(bool writeErrorLog)
        {
            bool tokSuccess = Tok();
            if (!writeErrorLog)
                return;
            if (!tokSuccess)
            {
                LogUtil.Log("Failed to write down login time to db.");
            }
        }

        private static string WriteDBLogSQL(TimeSpan duration)
        {
            String sql = "";//"SET IDENTITY_INSERT[DBLog] ON\n";
            sql += "INSERT INTO[DBLog](Id, EventType, EventTime, EventDuration)\n";
            sql += "(select (case when max(ID) is NULL then 1 else max(ID)+1 end),'Log in.',getdate(),'" + duration + "'\n";
            sql += "from DBLog)";
            return sql;
        }
    }
}
