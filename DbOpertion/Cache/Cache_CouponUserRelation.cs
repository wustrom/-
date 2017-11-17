using Common;
using Common.Extend;
using Common.Helper;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Cache
{
    /// <summary>
    /// 优惠卷用户关系缓存
    /// </summary>
    public partial class Cache_CouponUserRelation : SingleTon<Cache_CouponUserRelation>
    {
        /// <summary>
        /// 筛选全部
        /// </summary>
        /// <returns></returns>
        public List<CouponUserRelation> SelectAll(int UserId, string SearchKey, string Key, DataTablesOrderDir desc)
        {
            bool asc;
            if (desc == DataTablesOrderDir.Asc)
            {
                asc = true;
            }
            else
            {
                asc = false;
            }
            return CouponUserRelationOper.Instance.SelectByUserId(UserId, SearchKey, Key, asc);
        }

        /// <summary>
        /// 分页筛选数据
        /// </summary>
        /// <returns></returns>
        public List<CouponUserRelation> SelectByPage(int UserId, string SearchKey, string Key, int start, int pageSize, DataTablesOrderDir desc)
        {
            bool asc;
            if (desc == DataTablesOrderDir.Asc)
            {
                asc = true;
            }
            else
            {
                asc = false;
            }
            return CouponUserRelationOper.Instance.SelectPageByUserId(UserId, SearchKey, Key, start, pageSize, asc);
        }

        /// <summary>
        /// 筛选后的数据条数
        /// </summary>
        /// <returns></returns>
        public int SelectSearchCount(int UserId, string SearchKey, string Key, DataTablesOrderDir desc)
        {
            bool asc;
            if (desc == DataTablesOrderDir.Asc)
            {
                asc = true;
            }
            else
            {
                asc = false;
            }
            return CouponUserRelationOper.Instance.SelectSearchCount(UserId, SearchKey, Key, asc);
        }

        /// <summary>
        /// 发放优惠券给用户
        /// </summary>
        /// <param name="CouponId">优惠券Id</param>
        /// <param name="CouponToUser">用户优惠券关系信息</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public string Insert_GrantCouponToUser(int CouponId, List<int> UserId)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var transaction = sqlHelper.GetTransaction(connection);
            CouponUserRelation CouponToUser = new Models.CouponUserRelation();
            try
            {
                var result = CouponOper.Instance.SelectByCouponId(CouponId, connection, transaction);
                var Flag = true;
                foreach (var item in UserId)
                {
                    CouponToUser.CouponId = CouponId;
                    CouponToUser.CouponName = result.CouponName;
                    CouponToUser.CouponDescribe = result.CouponDescribe;
                    CouponToUser.ReleaseDate = DateTime.Now;
                    int day = Convert.ToInt32(result.ExpirationDay);
                    CouponToUser.ExpirationDate = DateTime.Now.AddDays(day);
                    CouponToUser.UserId = item;
                    if (!CouponUserRelationOper.Instance.Insert(CouponToUser, connection, transaction))
                    {
                        Flag = false;
                        break;
                    }
                }
                if (Flag)
                {
                    transaction.Commit();
                    connection.Close();
                    return true.ToString().ToLower();
                }
                else
                {
                    transaction.Rollback();
                    connection.Close();
                    return false.ToString().ToLower();
                }
            }
            catch (Exception e)
            {

                transaction.Rollback();
                connection.Close();
                return e.Message;
            }
        }
        /// <summary>
        /// 根据Id多选删除用户优惠券关系表数据
        /// </summary>
        /// <param name="CouponUserRelationId">用户优惠券关系表Id</param>
        /// <returns></returns>
        public bool Delete_CouponUserRelationByIds(List<int> CouponUserRelationId)
        {
            var flag = CouponUserRelationOper.Instance.DeleteByIds(CouponUserRelationId);
            return flag;
            }
    }
}
