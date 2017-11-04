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
    public partial class Cache_MemberShipType : SingleTon<Cache_MemberShipType>
    {
        /// <summary>
        /// 筛选全部会员类型卡
        /// </summary>
        /// <returns></returns>
        public List<MemberShipType> SelectAll()
        {
            var List_MemberShipType = MemberShipTypeOper.Instance.SelectAll(null);
            if (List_MemberShipType == null)
            {
                List_MemberShipType = new List<MemberShipType>();
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
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="desc"></param>
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
    }
}
