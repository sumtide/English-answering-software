using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace test
{
    /// <summary>
    /// MySql数据库链接脚本
    /// </summary>
    class Mysqlcon
    {
        #region 字段
        /// <summary>
        /// 链接字符串
        /// </summary>
        public static string connectionString = "server=localhost;port=3306;user=root;password=dongzhihui66; database=myapp;";
        /// <summary>
        /// 数据库链接定义
        /// </summary>
        private MySqlConnection dbConnection;
        /// <summary>
        /// SQL命令定义
        /// </summary>
        private MySqlCommand dbCommand;
        /// <summary>
        /// 数据读取定义
        /// </summary>
        private MySqlDataReader dataReader;

        #endregion

        #region 属性
        public Mysqlcon()
        {
            //构造数据库链接
            dbConnection = new MySqlConnection(connectionString);
            //根据状态打开数据库
            switch (dbConnection.State)
            {
                case ConnectionState.Broken:
                    dbConnection.Close();
                    dbConnection.Open();
                    break;
                case ConnectionState.Closed:
                    dbConnection.Open();
                    break;
                case ConnectionState.Connecting:
                    break;
                case ConnectionState.Executing:
                    break;
                case ConnectionState.Fetching:
                    break;
                case ConnectionState.Open:
                    break;
                default:
                    break;
            }
            //构造数据库链接
            dbConnection = new MySqlConnection(connectionString);
            //根据状态打开数据库
            switch (dbConnection.State)
            {
                case ConnectionState.Broken:
                    dbConnection.Close();
                    dbConnection.Open();
                    break;
                case ConnectionState.Closed:
                    dbConnection.Open();
                    break;
                case ConnectionState.Connecting:
                    break;
                case ConnectionState.Executing:
                    break;
                case ConnectionState.Fetching:
                    break;
                case ConnectionState.Open:
                    break;
                default:
                    break;

            }

        }
        #endregion

        #region  方法
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public MySqlDataReader ExecuteQuery(string queryString)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = queryString;
            dataReader = dbCommand.ExecuteReader();
            return dataReader;
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnect()
        {
            if (dbCommand != null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;
            if (dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;
        }
        #endregion

    }


}
