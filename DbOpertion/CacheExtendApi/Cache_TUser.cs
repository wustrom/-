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
using System.Data;

namespace DbOpertion.Cache
{
    /// <summary>
    /// 会员用户缓存
    /// </summary>
    public partial class Cache_TUser : SingleTon<Cache_TUser>
    {
        /// <summary>
        /// 会员用户登入信息
        /// </summary>
        /// <param name="UserName">用户名称</param>
        /// <param name="UserPassWord">用户密码</param>
        public string MemberUserLogin(string UserName, string UserPassWord)
        {
            string PassWordMd5 = MD5Helper.StrToMD5WithKey(UserPassWord);
            var list = TUserOper.Instance.LoginOn(UserName, PassWordMd5);
            if (list.Count > 0)
            {
                var user = list.FirstOrDefault();
                Token token = new Token();
                token.Payload.exp = DateTime.Now.AddDays(30).ToLongDateString();
                token.Payload.UserID = user.UserId;
                token.Payload.UserName = user.UserName;
                var tokenString = token.GetToken();
                //设置30天的用户Token
                MemCacheHelper1.Instance.writer.Modify("UserToken_" + user.UserId, tokenString, 30 * 60 * 24);
                return tokenString;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据后台用户id筛选用户信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public TUser MemberGetUserInfo(int UserId)
        {
            return TUserOper.Instance.SelectById(UserId);
        }

        /// <summary>
        /// 根据用户手机,用户名称筛选用户
        /// </summary>
        /// <param name="UserPhone">用户手机号码</param>
        /// <param name="UserName">用户名称</param>
        /// <returns></returns>
        public TUser SelectRepeat(string UserPhone, string UserName, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            return TUserOper.Instance.SelectRepeat(UserPhone, UserName, connection, transaction);
        }

        /// <summary>
        /// 用户Token
        /// </summary>
        /// <param name="tokenString">token字符串</param>
        /// <returns></returns>
        public Token GetUserToken(string tokenString)
        {
            Token token = new Token(tokenString);
            string ExitTokenString = MemCacheHelper1.Instance.reader.Get<string>("UserToken_" + token.Payload.UserID);
            if (ExitTokenString == tokenString)
            {
                return token;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据Token获得用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public TUser GetUserInfo(Token token)
        {
            return TUserOper.Instance.SelectById(token.Payload.UserID);
        }

        /// <summary>
        /// 设置用户注册验证码
        /// </summary>
        /// <param name="phone">用户手机号码</param>
        /// <returns></returns>
        public string SetMemberCode(string phone)
        {
            if (MemCacheHelper1.Instance.reader.Get("MemberResetCode_Phone=" + phone) == null)
            {
                string VerificationCode = RandHelper.Instance.Number(6);
                MemCacheHelper1.Instance.writer.Modify("MemberResetCode_Phone=" + phone, VerificationCode, 1);
                MemCacheHelper1.Instance.writer.Modify("MemberCode_Phone=" + phone, VerificationCode, 10);
                return VerificationCode;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户注册验证码
        /// </summary>
        /// <param name="phone">用户手机号码</param>
        /// <returns></returns>
        public string GetMemberCode(string phone)
        {
            var code = MemCacheHelper1.Instance.reader.Get("MemberCode_Phone=" + phone);
            return code == null ? null : code.ToString();
        }

        /// <summary>
        /// 用户注册验证码是否能重置
        /// </summary>
        /// <param name="Phone">用户手机</param>
        /// <returns></returns>
        public bool GetUserResetCode(string Phone)
        {
            var phone = MemCacheHelper1.Instance.reader.Get("MemberResetCode_Phone=" + Phone);
            return phone == null;
        }

        /// <summary>
        /// 清除注册缓存
        /// </summary>
        /// <param name="phone">用户手机号码</param>
        /// <returns></returns>
        public void ClearMemberCode(string phone)
        {
            MemCacheHelper1.Instance.writer.Remove("MemberResetCode_Phone=" + phone);
            MemCacheHelper1.Instance.writer.Remove("MemberCode_Phone=" + phone);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool UpdateUserInfo(TUser UserInfo)
        {
            var flag = TUserOper.Instance.Update(UserInfo);
            return flag;
        }
        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="CardName">卡名</param>
        /// <returns></returns>
        public TUser InsertUserInfo(TUser UserInfo, string CardName, bool Active)
        {
            string PassWordMd5 = MD5Helper.StrToMD5WithKey(UserInfo.UserPassword);
            UserInfo.UserPassword = PassWordMd5;
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var Transaction = sqlHelper.GetTransaction(connection);
            try
            {
                if (TUserOper.Instance.Insert(UserInfo, connection, Transaction))
                {
                    var user = SelectRepeat(UserInfo.UserPhone, null, connection, Transaction);
                    if (Cache_MemberShipCard.Instance.SetMemberShipUser(user.UserId, CardName, Active, connection, Transaction))
                    {
                        ClearMemberCode(UserInfo.UserPhone);
                        Transaction.Commit();
                        connection.Close();
                        return user;
                    }
                }
                Transaction.Rollback();
                connection.Close();
                return null;
            }
            catch (Exception)
            {
                Transaction.Rollback();
                connection.Close();
                return null;
            }

        }
    }
}
