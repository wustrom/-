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
    public partial class Cache_MemberShipCard : SingleTon<Cache_MemberShipCard>
    {
        /// <summary>
        /// 绑定卡片
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="CardName">卡名</param>
        /// <returns></returns>
        public bool Bind_Card(string tokenString, string CardName, string CardPassword,bool Active)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var Transaction = sqlHelper.GetTransaction(connection);
            try
            {
                Token token = new Token(tokenString);
                if (MemberShip(CardName, CardPassword, connection, Transaction))
                {
                    if (SetMemberShipUser(token.Payload.UserID, CardName, Active, connection, Transaction))
                    {
                        Transaction.Commit();
                        connection.Close();
                        return true;
                    }
                }
                Transaction.Rollback();
                connection.Close();
                return false;
            }
            catch (Exception)
            {
                Transaction.Rollback();
                connection.Close();
                return false;
            }

        }

        /// <summary>
        /// 验证密码成功
        /// </summary>
        /// <param name="CardName">卡名</param>
        /// <param name="CardPassword">卡密</param>
        /// <returns></returns>
        public bool MemberShip(string CardName, string CardPassword, IDbConnection connection, IDbTransaction transaction)
        {
            string CardPasswordMd5 = MD5Helper.StrToMD5WithKey(CardPassword);
            return MemberShipCardOper.Instance.MemberShip(CardName, CardPasswordMd5, connection, transaction);
        }

        /// <summary>
        /// 设置卡片的用户ID
        /// </summary>
        /// <param name="UserID">用户Id</param>
        /// <param name="CardName">卡名</param>
        /// <param name="firstFlag">是否第一次</param>
        /// <param name="connection">连接</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public bool SetMemberShipUser(int UserID, string CardName, bool Active, IDbConnection connection, IDbTransaction transaction)
        {
            var card = MemberShipCardOper.Instance.SelectMemberShipByCardName(CardName, connection, transaction);
            if (card != null)
            {
                if (Active)
                {
                    if (!ActiveOper.Instance.UpdateCardByUserId(UserID, card.MemberShipCardId, connection, transaction))
                    {
                        return false;
                    }
                }
                if (MemberShipCardOper.Instance.SetMemberShipUser(UserID, CardName, connection, transaction))
                {
                    var relation_list = CouponCardRelationOper.Instance.SelectByCardTypeId(card.MemberShipTypeId.Value, connection, transaction);
                    foreach (var item in relation_list)
                    {
                        var coupon = CouponOper.Instance.SelectById(item.CouponId.Value, connection, transaction);
                        CouponUserRelation userRelation = new CouponUserRelation();
                        userRelation.CouponDescribe = coupon.CouponDescribe;
                        userRelation.CouponName = coupon.CouponName;
                        userRelation.ExpirationDate = DateTime.Now.AddDays(coupon.ExpirationDay.Value);
                        userRelation.ReleaseDate = card.ReleaseDate;
                        userRelation.UserId = UserID;
                        if (!CouponUserRelationOper.Instance.Insert(userRelation, connection, transaction))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
