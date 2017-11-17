using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;
using System.Data;

namespace DbOpertion.Operation
{
    public partial class TUserOper : SingleTon<TUserOper>
    {

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<TUser> SelectSearchByPage(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<TUser>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.UserName.Contains(SearchKey) || p.UserNickName.Contains(SearchKey) || p.UserPhone.Contains(SearchKey) || p.UserEmail.Contains(SearchKey) || p.ConsumptionTime.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectSearchCount(string SearchKey, string Key, bool desc = true)
        {
            var query = new LambdaQuery<TUser>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.UserName.Contains(SearchKey) || p.UserNickName.Contains(SearchKey) || p.UserPhone.Contains(SearchKey) || p.UserEmail.Contains(SearchKey) || p.ConsumptionTime.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount();
        }

        /// <summary>
        /// 根据用户手机筛选用户
        /// </summary>
        /// <param name="UserPhone">用户手机</param>
        /// <returns>对象</returns>
        public TUser SelectByPhone(string UserPhone)
        {
            var query = new LambdaQuery<TUser>();
            query.Where(p => p.UserPhone == UserPhone);
            var List_User = query.GetQueryList();
            return List_User == null ? null : List_User.FirstOrDefault();
        }
        /// <summary>
        /// 根据用户名，手机号码筛选用户
        /// </summary>
        /// <param name="UserPhone"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<TUser> SelectUserInfoByNameOrPhone(string UserName, string UserPhone, string UserEmail)
        {
            var query = new LambdaQuery<TUser>();
            query.Where(p => p.UserName == UserName || p.UserPhone == UserPhone || p.UserEmail == UserEmail);
            return query.GetQueryList();
        }


        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<TUser> SelectByUserNickName(string SearchKey)
        {
            var query = new LambdaQuery<TUser>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.UserNickName.Contains(SearchKey));
            }
            return query.GetQueryList();
        }
        /// <summary>
        /// 根据Id删除多条数据
        /// </summary>
        /// <param name="KeyId">所选对象的Id</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteByIds(List<int> KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<TUser>();
            delete.Where(p => p.UserId.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }
    }
}
