using Common.Extend;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.LambdaOpertion
{
    public class SqlOpertion : SingleTon<SqlOpertion>
    {
        public string Linq = "linq";
        /// <summary>
        /// 排序字段
        /// </summary>
        private List<string> OrderByFields = new List<string>();
        /// <summary>
        /// Sql语句分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Sql"></param>
        /// <param name="List_Para"></param>
        /// <param name="start"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<T> GetQueryPage<T>(string Sql, List<SqlParameter> List_Para, string OrderBy, bool desc, int start, int PageSize) where T : class, new()
        {

            if (!OrderBy.IsNullOrEmpty())
            {
                if (desc)
                {
                    OrderBy = OrderBy + " desc ";
                }
                else
                {
                    OrderBy = OrderBy + " Asc ";
                }
                OrderBy = " Order By " + OrderBy;
            }
            string sql = string.Format(@"select * from( select row_number()over(order by tempcolumn)temprownumber, * from(
                                         select top {0} tempcolumn=0,* from (" + Sql + " )t " + OrderBy + ")tt)ttt where temprownumber>{1}", start + PageSize, start);
            var sqlHelper = SqlHelper.GetSqlServerHelper(Linq);
            return sqlHelper.ExecuteReader(sql, List_Para.ToArray()).ConvertToList<T>();
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Sql"></param>
        /// <param name="List_Para"></param>
        /// <returns></returns>
        public List<T> GetQueryList<T>(string Sql, List<SqlParameter> List_Para) where T : class, new()
        {
            string sql = string.Format(@"select * from (" + Sql + ")t");
            var sqlHelper = SqlHelper.GetSqlServerHelper(Linq);
            return sqlHelper.ExecuteReader(sql, List_Para.ToArray()).ConvertToList<T>();
        }


        /// <summary>
        /// Sql语句分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Sql"></param>
        /// <param name="List_Para"></param>
        /// <param name="start"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public int GetQueryCount(string Sql, List<SqlParameter> List_Para)
        {
            string sql = string.Format(@"select Count(*) from(" + Sql + ")t");
            var sqlHelper = SqlHelper.GetSqlServerHelper(Linq);
            return (int)sqlHelper.ExecuteReader(sql, List_Para.ToArray()).Rows[0][0];
        }


    }
}
