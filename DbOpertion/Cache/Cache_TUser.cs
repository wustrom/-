using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOpertion.Models;
using DbOpertion.Operation;
using Common;
using Common.Helper;
using System.Configuration;
using Common.Result;

namespace DbOpertion.Cache
{
    /// <summary>
    /// 会员用户缓存
    /// </summary>
    public partial class Cache_TUser : SingleTon<Cache_TUser>
    {
        /// <summary>
        /// 筛选全部会员用户信息
        /// </summary>
        public List<TUser> SelectMemberUser(string searchKey, string Key, int PageNo, int PageSize, DataTablesOrderDir? desc)
        {
            bool order = false;
            if (desc == DataTablesOrderDir.Asc)
            {
                order = false;
            }
            else if (desc == DataTablesOrderDir.Desc)
            {
                order = true;
            }
            return TUserOper.Instance.SelectSearchByPage(searchKey, Key, PageNo, PageSize, order);
        }

        /// <summary>
        /// 筛选全部会员用户数目
        /// </summary>
        public int SelectMemberUserCount(string SearchKey, string Key, DataTablesOrderDir? desc)
        {
            bool order = false;
            if (desc == DataTablesOrderDir.Asc)
            {
                order = false;
            }
            else if (desc == DataTablesOrderDir.Desc)
            {
                order = true;
            }
            return TUserOper.Instance.SelectSearchCount(SearchKey, Key, order);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool InsertUserInfo(TUser user)
        {
            return TUserOper.Instance.Insert(user);
        }
    }
}
