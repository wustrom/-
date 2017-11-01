using Common;
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
    public partial class Cache_CouponCardRelation : SingleTon<Cache_CouponCardRelation>
    {
        /// <summary>
        /// 根据卡片ID查找优惠券的详细信息
        /// </summary>
        /// <param name="MemberShipCardId"></param>
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <param name="desc"></param>
        /// <returns>三元列表 1:数据列表 2:数据总条数 3：筛选后数据条数</returns>
        public Tuple<List<Coupon>, int, int> SelectCouponList(int MemberShipTypeId, string SearchKey, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            var coupon_ListId = CouponCardRelationOper.Instance.SelectByMemberShipCardId(MemberShipTypeId).Select(p => p.CouponId.Value).ToList();//优惠券ID列表
            var Coupon_List = CouponOper.Instance.SelectByPageByMemberCard(coupon_ListId, SearchKey, Key, start, pageSize, asc);//找出对应的优惠券
            var All_Count = CouponOper.Instance.SelectMemberCardCountByID(coupon_ListId, null);
            var Count = CouponOper.Instance.SelectMemberCardCountByID(coupon_ListId, SearchKey);
            return new Tuple<List<Coupon>, int, int>(Coupon_List, All_Count, Count);

        }
        /// <summary>
        /// 查找剩余优惠券的详细信息
        /// </summary>
        /// <param name="MemberShipCardId"></param>
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Tuple<List<Coupon>, int, int> SelectAllCouponList(int MemberShipTypeId, string SearchKey, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            var coupon_ListId = CouponCardRelationOper.Instance.SelectByMemberShipCardId(MemberShipTypeId).Select(p => p.CouponId.Value).ToList();//优惠券ID列表
            var Coupon_List = CouponOper.Instance.SelectByPageByAllMemberCard(coupon_ListId, SearchKey, Key, start, pageSize, asc);
            var All_Count = CouponOper.Instance.SelectAllMemberCardCount(coupon_ListId, null);
            var Count = CouponOper.Instance.SelectAllMemberCardCount(coupon_ListId, SearchKey);
            return new Tuple<List<Coupon>, int, int>(Coupon_List, All_Count, Count);

        }
        /// <summary>
        /// 根据Id删除对应的优惠券
        /// </summary>
        /// <param name="MemberShipCardId">卡片ID</param>
        /// <param name="CouponId">优惠券ID</param>
        /// <returns></returns>
        public bool Delete_CouponById(int MemberShipCardId, int CouponId)
        {
            return CouponCardRelationOper.Instance.Delete_CuoponId(MemberShipCardId, CouponId);
        }
        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="MemberShipCardId"></param>
        /// <returns></returns>
        public bool Insert_CouponById(int MemberShipCardId, int CouponId)
        {
            var list = CouponCardRelationOper.Instance.Check_CouponbyID(MemberShipCardId, CouponId);
            if (list.Count > 0)
            {
                return false;
            }
            else
            {
                return CouponCardRelationOper.Instance.Insert_Coupon(MemberShipCardId, CouponId);
            }

        }
    }
}
