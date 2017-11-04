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
    /// 电子储存卡缓存
    /// </summary>
    public partial class Cache_ElectronicCard : SingleTon<Cache_ElectronicCard>
    {
        /// <summary>
        /// 绑定卡片
        /// </summary>
        /// <param name="tokenString">用户Token</param>
        /// <param name="CardName">卡名</param>
        /// <param name="CardPassword">卡密</param>
        /// <returns></returns>
        public string Bind_Card(string tokenString, string CardName, string CardPassword)
        {
            Token token = new Token(tokenString);
            var resultCard = VaildCard(CardName, CardPassword);
            if (resultCard != null)
            {
                var Relation = ElectronicCardRelationOper.Instance.SelectByUserIdAndElectronicId(token.Payload.UserID, resultCard.ElectronicId);
                if (Relation != null)
                {
                    return "改用户已绑定此卡片";
                }
                else
                {
                    return ElectronicCardRelationOper.Instance.SetUserRelation(token.Payload.UserID, resultCard.ElectronicId).ToString().ToLower();
                }

            }
            return "并无对应卡片";
        }

        /// <summary>
        /// 验证卡名与卡片是否对应
        /// </summary>
        /// <param name="CardName">卡名</param>
        /// <param name="CardPassword">卡密</param>
        /// <returns></returns>
        public ElectronicCard VaildCard(string CardName, string CardPassword, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            return ElectronicCardOper.Instance.VaildCard(CardName, CardPassword, connection, transaction);
        }

    }
}
