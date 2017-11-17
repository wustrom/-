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
    public partial class Cache_Message : SingleTon<Cache_Message>
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
                token.Payload.exp = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd HH:mm:ss.fff");
                token.Payload.UserID = user.UserId;
                token.Payload.UserName = user.UserName;
                var tokenString = token.GetToken();
                //设置30天的用户Token
                MemCacheHelper1.Instance.writer.Modify("UserToken_" + user.UserId, tokenString, 30 * 60 * 24 - 50);
                return tokenString;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 消息列表排除Html
        /// </summary>
        /// <returns></returns>
        public List<Message> GetMessageListWithOutHtml()
        {
            return MessageOper.Instance.SelectWithOutHtml();
        }

        /// <summary>
        /// 消息列表排除Html
        /// </summary>
        /// <returns></returns>
        public Message SelectById(int KeyId)
        {
            return MessageOper.Instance.SelectById(KeyId).FirstOrDefault();
        }
    }
}
