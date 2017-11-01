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
    public partial class Cache_AUser : SingleTon<Cache_AUser>
    {

        /// <summary>
        /// 筛选全部后台用户信息
        /// </summary>
        public List<AUser> SelectAllAdminUser(string Key, DataTablesOrderDir? desc)
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
            return AUserOper.Instance.SelectAll(Key, order);
        }

        /// <summary>
        /// 筛选全部会员用户数目
        /// </summary>
        public int SelectAdminUserCount(string Key, DataTablesOrderDir? desc)
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
            return AUserOper.Instance.SelectCount(Key, order);
        }

        /// <summary>
        /// 后台用户登入信息
        /// </summary>
        /// <param name="UserName">用户名称</param>
        /// <param name="UserPassWord">用户密码</param>
        public AUser AdminUserLogin(string UserName, string UserPassWord)
        {
            string PassWordMd5 = MD5Helper.StrToMD5WithKey(UserPassWord);
            var list = AUserOper.Instance.LoginOn(UserName, PassWordMd5);
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        /// <summary>
        /// 根据后台用户id筛选用户信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public TUser AdminGetUserInfo(int UserId)
        {
            var User = TUserOper.Instance.SelectById(UserId);
            return User;
        }
       
    }
}
