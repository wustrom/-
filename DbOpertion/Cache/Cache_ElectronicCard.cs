using Common;
using Common.Helper;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Cache
{
    public partial class Cache_ElectronicCard : SingleTon<Cache_ElectronicCard>
    {
        /// <summary>
        /// 根据电子卡类型Id查找对应的卡片信息
        /// </summary>
        /// <param name="ElectronicTypeId">电子卡类型Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始数据</param>
        /// <param name="pageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<ElectronicCard>, int, int> SelectElectronicCardList(int ElectronicTypeId, string SearchKey, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            var list = ElectronicCardOper.Instance.Get_ElectronicCardByTypeId(ElectronicTypeId, SearchKey, Key, start, pageSize, asc);
            var All_Count = ElectronicCardOper.Instance.SelectSearchCount(ElectronicTypeId, null);
            var Count = ElectronicCardOper.Instance.SelectSearchCount(ElectronicTypeId, SearchKey);
            return new Tuple<List<ElectronicCard>, int, int>(list, All_Count, Count);

        }
        /// <summary>
        /// 添加电子储值卡
        /// </summary>
        /// <param name="card">电子储值卡信息</param>
        /// <returns></returns>
        public bool Insert_ElectronicCard(ElectronicCard card)
        {
            var list_Name = ElectronicCardOper.Instance.SelectElectronicCardByName(card.CardName);
            if (list_Name.Count > 0)
            {
                return false;
            }
            else
            {
                return ElectronicCardOper.Instance.Insert(card);
            }
        }

        /// <summary>
        /// 根据卡类型Id和卡Id查找绑定人
        /// </summary>
        /// <param name="ElectronicTypeId">卡类型Id</param>
        /// <param name="ElectronicId">卡Id</param>
        /// <returns></returns>
        public List<ElectronicCardUserInfo> GetByUserNickName(int ElectronicTypeId, int ElectronicId)
        {
            var List_UserNickName = ElectronicCardOper.Instance.SelectByUserNickName(ElectronicTypeId, ElectronicId).ToList();
            ElectronicCardUserInfo card=new Models.ElectronicCardUserInfo ();
            List<ElectronicCardUserInfo> List_Card = new List<Models.ElectronicCardUserInfo>();
            if (List_UserNickName != null && List_UserNickName.Count != 0)
            {
                foreach (var item in List_UserNickName)
                {
                    card.UserNickName = card.UserNickName+item.UserNickName+",";
                    card.UserID = item.UserID;
                    card.ElectronicId = item.ElectronicId;
                    card.ElectronicTypeId = item.ElectronicTypeId;
                    List_Card.Add(card);
                }
            }
            else
            {
                List_Card = null;
            }
            return List_Card;
        }
        public bool DeleteElectronicCardById(int ElectronicId)
        {
            return ElectronicCardOper.Instance.DeleteElectronicCardById(ElectronicId);
           
        }
 
        /// <summary>
        /// 将Excel里的数据添加到数据库中
        /// </summary>
        /// <param name="Electronic_Card"></param>
        /// <returns></returns>
        public string Insert_ElectronicCardByExcel(List<ElectronicCard> Electronic_Card)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var transaction = sqlHelper.GetTransaction(connection);
            try
            {
                var All_ElectronicCard = ElectronicCardOper.Instance.SelectExcelByName(connection, transaction);
                var ResultCard = Electronic_Card.Where(p =>!All_ElectronicCard.Contains(p.CardName)).ToList();
                if (ResultCard.Count == 0)
                {
                    transaction.Rollback();
                    connection.Close();
                    return "导入卡名全部重复，请查看是否重复导入！";
                }
                var Flag = true;
                foreach (var item in ResultCard)
                {
                    if (!ElectronicCardOper.Instance.Insert(item, connection, transaction))
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
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="KeyIds">电子储值卡Id</param>
        /// <returns></returns>

        public bool Delete_ElectronicCardByIds(List<int> KeyIds)
        {
            var flag = ElectronicCardOper.Instance.DeleteByIds(KeyIds);
            return flag;
        }
    }
}
