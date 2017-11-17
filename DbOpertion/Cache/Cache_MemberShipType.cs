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
   public partial class Cache_MemberShipType: SingleTon<Cache_MemberShipType>
    {
        /// <summary>
        /// 筛选全部会员类型卡
        /// </summary>
        /// <returns></returns>
        public List<MemberShipType> SelectAll()
        {
            var List_MemberShipType = MemCacheHelper1.Instance.reader.Get<List<MemberShipType>>("List_MemberShipType");
            if (List_MemberShipType == null || List_MemberShipType.Count == 0)
            {
                List_MemberShipType = MemberShipTypeOper.Instance.SelectAll(null);
                if (List_MemberShipType == null)
                {
                    List_MemberShipType = new List<MemberShipType>();
                }
            }
            else
            {
                MemCacheHelper1.Instance.writer.Add("List_MemberShipType", List_MemberShipType);
            }
            return List_MemberShipType;
        }
        /// <summary>
        /// 筛选全部会员类型卡信息
        /// </summary>
        public List<MemberShipType> SelectMemberTypeCard(string searchKey, string Key, int PageNo, int PageSize, DataTablesOrderDir? desc)
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
            return MemberShipTypeOper.Instance.SelectByPage(searchKey, Key, PageNo, PageSize, order);
        }
     
        /// <summary>
        /// 筛选会员卡类型数目
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public int SelectMemberCardTypeCount(string SearchKey, string Key, DataTablesOrderDir? desc)
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
            return MemberShipTypeOper.Instance.SelectCount(SearchKey, Key, order);
        }
        /// <summary>
        /// 添加新类型卡
        /// </summary>
        /// <param name="Card">会员类型卡信息</param>
        /// <returns></returns>
        public bool Insert_MemCardType(MemberShipType Card)
        {
            var List_CheckName = MemberShipTypeOper.Instance.Check_MemCardName(Card.CardName);
            if (List_CheckName.Count > 0)
            {
                return false;
            }
            else
            {
                return MemberShipTypeOper.Instance.Insert(Card);
            }
           
        }
        /// <summary>
        /// 多选删除会员类型卡
        /// </summary>
        /// <param name="MemberShipCardId">会员卡Id</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete_MemberTypeCard(List<int> MemberShipTypeId)
        {

            var flag = MemberShipTypeOper.Instance.DeleteByIds(MemberShipTypeId);
            return flag;
        }
    }
}
