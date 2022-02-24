using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace test
{
    public class Word
    {
        #region 字段
        public int id { get; set; }
        public string chinese { get; set; }
        public string english { get; set; }
        public int wrong { get; set; }

        public int right { get; set; }
        #endregion

        #region 属性
        public Word(int id)
        {
            this.id = id;
            //使用本地数据库连接
            //DbAccess dbAccess = new DbAccess();
            //SqlDataReader dataReader = dbAccess.ExecuteQuery("select * from Word where id=" + id.ToString());
            //while (dataReader.Read())
            //{
            //    chinese = dataReader.GetString(dataReader.GetOrdinal("chinese"));
            //    english = dataReader.GetString(dataReader.GetOrdinal("english"));
            //    wrong = int.Parse(dataReader.GetString(dataReader.GetOrdinal("wrongs")));
            //    right = int.Parse(dataReader.GetString(dataReader.GetOrdinal("rights")));
            //}
            //dbAccess.CloseConnect();

            //使用远程数据库连接
            Mysqlcon mysqlcon = new Mysqlcon();
            MySqlDataReader dataReader = mysqlcon.ExecuteQuery("select * from wordlist where id=" + id.ToString());
            while (dataReader.Read())
            {
                chinese = dataReader.GetString(dataReader.GetOrdinal("chinese"));
                english = dataReader.GetString(dataReader.GetOrdinal("english"));
                wrong = int.Parse(dataReader.GetString(dataReader.GetOrdinal("wrongs")));
                right = int.Parse(dataReader.GetString(dataReader.GetOrdinal("rights")));
            }
            mysqlcon.CloseConnect();
        }
        #endregion

        #region 方法
        public void AddMark()
        {
            wrong++;
            //DbAccess dbAccess = new DbAccess();
            //dbAccess.ExecuteQuery("update Word set wrongs=" + wrong.ToString() + " where id=" + id.ToString());
            //dbAccess.CloseConnect();
            Mysqlcon mysqlcon = new Mysqlcon();
            mysqlcon.ExecuteQuery("update Word set wrongs=" + wrong.ToString() + " where id=" + id.ToString());
            mysqlcon.CloseConnect();

        }
        public void DecreaseMark()
        {
             right++;
            //DbAccess dbAccess = new DbAccess();
            //dbAccess.ExecuteQuery("UPDATE Word SET [rights]=" + right.ToString() + " where id= " + id.ToString());
            //dbAccess.CloseConnect();
            Mysqlcon mysqlcon = new Mysqlcon();
            mysqlcon.ExecuteQuery("UPDATE Word SET rights=" + right.ToString() + " where id= " + id.ToString());
            mysqlcon.CloseConnect();
        }
        #endregion
    }
}
