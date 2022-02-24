using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace test
{
    public class UserData
    {
        #region 字段
        public int times { get; set; }

        public int correcttimes { get; set; }

        public int errortimes { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 异步获取回答正确次数
        /// </summary>
        /// <returns></returns>
        public Task<int[]> GetCorrect()
        {
            return Task.Run(() => 
            { 
              return  GetCorrecttimes();
            });
        }
        private int[] GetCorrecttimes()
        {
            List<int> temp = new List<int>();
            //DbAccess dbAccess = new DbAccess();
            //SqlDataReader dataReader = dbAccess.ExecuteQuery("select correcttimes from UserData");
            //while (dataReader.Read())
            //{
            //    temp.Add(dataReader.GetInt32(dataReader.GetOrdinal("correcttimes")));
            //}
            //dbAccess.CloseConnect();
            Mysqlcon mysqlcon = new Mysqlcon();
            MySqlDataReader dataReader = mysqlcon.ExecuteQuery("select correcttimes from UserData");
            while (dataReader.Read())
            {
                temp.Add(dataReader.GetInt32(dataReader.GetOrdinal("correcttimes")));
            }
            mysqlcon.CloseConnect();
            return temp.ToArray();
        }
        /// <summary>
        /// 异步获取回答错误次数
        /// </summary>
        /// <returns></returns>
        public Task<int[]> GetError()
        {
            return Task.Run(() =>
            {
                return GetErrortimes();
            });
        }
        private int[] GetErrortimes()
        {
            List<int> temp = new List<int>();
            //DbAccess dbAccess = new DbAccess();
            //SqlDataReader dataReader = dbAccess.ExecuteQuery("select errortimes from UserData");
            //while (dataReader.Read())
            //{
            //    temp.Add(dataReader.GetInt32(dataReader.GetOrdinal("errortimes")));
            //}
            //dbAccess.CloseConnect();
            Mysqlcon mysqlcon = new Mysqlcon();
            MySqlDataReader dataReader = mysqlcon.ExecuteQuery("select errortimes from UserData");
            while (dataReader.Read())
            {
                temp.Add(dataReader.GetInt32(dataReader.GetOrdinal("errortimes")));
            }
            mysqlcon.CloseConnect();
            return temp.ToArray();

        }

        public void SaveData(int correct, int error)
        {
            //DbAccess dbAccess = new DbAccess();
            //dbAccess.ExecuteQuery("insert into UserData(correcttimes,errortimes) values(" + correct.ToString()+","+error.ToString()+")");
            //dbAccess.CloseConnect();   
            Mysqlcon mysqlcon = new Mysqlcon();
            mysqlcon.ExecuteQuery("insert into UserData(correcttimes,errortimes) values(" + correct.ToString() + "," + error.ToString() + ")");
            mysqlcon.CloseConnect();
        }
        #endregion
    }


}
