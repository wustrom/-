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
    /// <summary>
    /// 会员卡缓存
    /// </summary>
    public partial class Cache_MemberShipCard : SingleTon<Cache_MemberShipCard>
    {
        /// <summary>
        /// 筛选全部会员卡
        /// </summary>
        /// <returns></returns>
        public List<MemberShipCard> SelectAll()
        {
            var List_MemberShipCard = MemCacheHelper1.Instance.reader.Get<List<MemberShipCard>>("List_MemberShipCard");
            if (List_MemberShipCard == null || List_MemberShipCard.Count == 0)
            {
                List_MemberShipCard = MemberShipCardOper.Instance.SelectAll(null);
                if (List_MemberShipCard == null)
                {
                    List_MemberShipCard = new List<MemberShipCard>();
                }
            }
            else
            {
                MemCacheHelper1.Instance.writer.Add("List_MemberShipCard", List_MemberShipCard);
            }
            return List_MemberShipCard;
        }
        /// <summary>
        /// 筛选全部会员卡信息
        /// </summary>
        public List<MemberShipCard> SelectMemberCard(string searchKey, string Key, int PageNo, int PageSize, DataTablesOrderDir? desc)
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
            return MemberShipCardOper.Instance.SelectByPage(searchKey, Key, PageNo, PageSize, order);
        }

        /// <summary>
        /// 根据类型ID查找对应的会员卡信息
        /// </summary>
        /// <param name="MemberShipTypeId">会员卡类型Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key"></param>
        /// <param name="start">开始数据</param>
        /// <param name="pageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<MemberCardByTypeInfo>, int, int> SelectMemberCardList(int MemberShipTypeId, string SearchKey, string ReleaseDate1, string ReleaseDate2, string CreateDate1, string CreateDate2, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            var list = MemberShipCardOper.Instance.SelectMemCardByTypeId(MemberShipTypeId, SearchKey, ReleaseDate1, ReleaseDate2, CreateDate1, CreateDate2, Key, start, pageSize, asc);
            var All_Count = MemberShipCardOper.Instance.SelectMemberCardCountByTypeID(MemberShipTypeId, null);
            var Count = MemberShipCardOper.Instance.SelectMemberCardCountByTypeID(MemberShipTypeId, SearchKey);
            return new Tuple<List<MemberCardByTypeInfo>, int, int>(list, All_Count, Count);

        }
        /// <summary>
        /// 添加会员卡
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool Insert_MemberCard(MemberShipCard card, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var CheckMemberName = MemberShipCardOper.Instance.SelectMemCardByName(card.CardName, connection, transaction);
            if (CheckMemberName.Count > 0)
            {
                return false;
            }
            else
            {
                return MemberShipCardOper.Instance.Insert(card, connection, transaction);
            }
        }

        /// <summary>
        /// 将Excel里的数据添加到数据库中
        /// </summary>
        /// <param name="List_Card">卡片信息</param>
        /// <returns></returns>
        public string Insert_MemberCard2(List<MemberShipCard> List_Card)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var transaction = sqlHelper.GetTransaction(connection);
            try
            {
                var AllListCard = MemberShipCardOper.Instance.SelectByName(connection, transaction);
                var ResultCard = List_Card.Where(p => !AllListCard.Contains(p.CardName)).ToList();
                if (ResultCard.Count == 0)
                {
                    transaction.Rollback();
                    connection.Close();
                    return "导入卡名全部重复，请查看是否重复导入！";
                }
                var Flag = true;
                foreach (var item in ResultCard)
                {
                    if (!MemberShipCardOper.Instance.Insert(item, connection, transaction))
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
        /// 多选删除会员类型卡
        /// </summary>
        /// <param name="MemberShipCardId">会员卡Id</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete_MemberCard(List<int> KeyIds)
        {
           
            var flag= MemberShipCardOper.Instance.DeleteByMemberCardIds(KeyIds);
            return flag;
        }
    }
}
