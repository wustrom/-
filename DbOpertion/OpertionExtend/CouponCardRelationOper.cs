using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;

namespace DbOpertion.Operation
{
    public partial class CouponCardRelationOper : SingleTon<CouponCardRelationOper>
    {
        /// <summary>
        /// 根据卡片ID查找优惠券ID
        /// </summary>
        /// <param name="MemberShipCardId"></param>
        /// <returns></returns>
        public List<CouponCardRelation> SelectByMemberShipCardId(int MemberShipTypeId)
        {
            var query = new LambdaQuery<CouponCardRelation>();
            query.Where(p => p.MemberShipTypeId == MemberShipTypeId);//所有的优惠券ID的列表
            return query.GetQueryList();
        }

        /// <summary>
        /// 根据优惠券ID查找对应的优惠券信息
        /// </summary>
        /// <param name="MemberShipCardId"></param>
        /// <returns></returns>
        public List<CouponCardRelation> SelectByMemberShip(List<int> MemberShipCard)
        {
            var query = new LambdaQuery<CouponCardRelation>();
            query.Where(p => p.CouponId.ContainsIn(MemberShipCard));
            return query.GetQueryList();
        }
        /// <summary>
        /// 删除对应的优惠券
        /// </summary>
        /// <param name="MemberShipTypeId">卡片ID</param>
        /// <param name="CouponId">优惠券ID</param>
        /// <returns></returns>
        public bool Delete_CuoponId(int MemberShipTypeId, int CouponId)
        {
            var delete = new LambdaDelete<CouponCardRelation>();
            delete.Where(p => p.CouponId == CouponId && p.MemberShipTypeId == MemberShipTypeId); 
            return delete.GetDeleteResult();
        }
        /// <summary>
        /// 添加对应的优惠券
        /// </summary>
        /// <param name="MemberShipTypeId"></param>
        /// <returns></returns>
        public bool Insert_Coupon(int MemberShipTypeId, int CouponId)
        {
            var insert = new LambdaInsert<CouponCardRelation>();
            if (!MemberShipTypeId.IsNullOrEmpty())
            {
                insert.Insert(p => p.MemberShipTypeId == MemberShipTypeId);
            }
            if (!MemberShipTypeId.IsNullOrEmpty())
            {
                insert.Insert(p => p.CouponId == CouponId);
            }
            return insert.GetInsertResult();
        }
        /// <summary>
        /// 判断是否已存在对应的优惠券与卡片的关系
        /// </summary>
        /// <param name="MemberShipCardId">卡片ID</param>
        /// <param name="CouponId">优惠券ID</param>
        /// <returns></returns>
        public List<CouponCardRelation> Check_CouponbyID(int MemberShipTypeId, int CouponId)
        {
            var query = new LambdaQuery<CouponCardRelation>();
            query.Where(p => p.MemberShipTypeId == MemberShipTypeId && p.CouponId == CouponId);
            return query.GetQueryList();
        }

    }
}
