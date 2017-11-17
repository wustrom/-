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
                    var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
                    var connection = sqlHelper.GetConnection();
                    var Transaction = sqlHelper.GetTransaction(connection);
                    try
                    {
                        if (ElectronicCardRelationOper.Instance.SetUserRelation(token.Payload.UserID, resultCard.ElectronicId))
                        {
                            if (ElectronicCardOper.Instance.ActivateCard(CardName, connection, Transaction))
                            {
                                Transaction.Commit();
                                connection.Close();
                                return true.ToString().ToLower();
                            }
                        }
                        Transaction.Rollback();
                        connection.Close();
                        return false.ToString().ToLower();
                    }
                    catch (Exception e)
                    {
                        Transaction.Rollback();
                        connection.Close();
                        return e.Message;
                    }
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

        /// <summary>
        /// 获得电子储值卡列表
        /// </summary>
        /// <param name="UserToken">用户Token</param>
        /// <returns>item1:电子储存卡与用户ID关系列表,item2:电子储存卡列表,item3:电子储存卡类型列表</returns>
        public Tuple<List<ElectronicCardRelation>, List<ElectronicCard>, List<ElectronicType>> GetElectronicList(string UserToken)
        {
            Token token = new Token(UserToken);
            List<ElectronicCardRelation> ListCardRelation = ElectronicCardRelationOper.Instance.SelectByUserId(token.Payload.UserID);
            var ListCardId = ListCardRelation.Select(p => p.ElectronicCardId.Value).ToList();
            var ListElecCard = ElectronicCardOper.Instance.SelectUserCard(ListCardId);
            var ListElecType = ElectronicTypeOper.Instance.SelectAll(null);
            return new Tuple<List<ElectronicCardRelation>, List<ElectronicCard>, List<ElectronicType>>(item1: ListCardRelation, item2: ListElecCard, item3: ListElecType);
        }

        /// <summary>
        /// 解除绑定卡片
        /// </summary>
        /// <param name="tokenString">用户Token</param>
        /// <param name="CardName">卡名</param>
        /// <param name="CardPassword">卡密</param>
        /// <returns></returns>
        public string RelieveBindElecCard(string tokenString, int relationId)
        {
            Token token = new Token(tokenString);
            var Relation = ElectronicCardRelationOper.Instance.SelectById(relationId);
            if (Relation == null)
            {
                return "并未绑定卡片";
            }
            else
            {
                if (token.Payload.UserID == Relation.UserId)
                {
                    return ElectronicCardRelationOper.Instance.DeleteById(relationId).ToString().ToLower();
                }
                else
                {
                    return "并不是当前用户的绑定,不能解除此关系";
                }
            }
        }
    }
}
