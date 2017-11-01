using Common;
using Common.Helper;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
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
        /// <param name="MemberShipTypeId"></param>
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Tuple<List<MemberShipCard>, int, int> SelectMemberCardList(int MemberShipTypeId, string SearchKey, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            var list = MemberShipCardOper.Instance.SelectMemCardByTypeId(MemberShipTypeId, SearchKey, Key, start, pageSize, asc);
            var MemberCard_List = MemberShipCardOper.Instance.SelectByPageByMemberCard(MemberShipTypeId, SearchKey, Key, start, pageSize);
            var All_Count = MemberShipCardOper.Instance.SelectMemberCardCountByTypeID(MemberShipTypeId, null);
            var Count = MemberShipCardOper.Instance.SelectMemberCardCountByTypeID(MemberShipTypeId, SearchKey);
            return new Tuple<List<MemberShipCard>, int, int>(MemberCard_List, All_Count, Count);

        }
    }
}
