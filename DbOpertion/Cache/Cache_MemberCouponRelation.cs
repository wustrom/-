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
    public class Cache_MemberCouponRelation : SingleTon<Cache_MemberCouponRelation>
    {
        /// <summary>
        /// 根据会员等级Id显示对应的优惠内容
        /// </summary>
        /// <param name="MembershipLevelId">会员等级Id</param>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始条数</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<MemberLevelRelation>, int, int> SelectMemberCouponRelationList(int MembershipLevelId, string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
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
            var list = MemberLevelRelationOper.Instance.SelectMemberCouponRelationList(MembershipLevelId, SearchKey, Key, start, PageSize, asc);
            var All_Count = MemberLevelRelationOper.Instance.SelectMemCouponRelationListCount(MembershipLevelId, null);
            var Count = MemberLevelRelationOper.Instance.SelectMemCouponRelationListCount(MembershipLevelId, SearchKey);
            return new Tuple<List<MemberLevelRelation>, int, int>(list, All_Count, Count);
        }
        /// <summary>
        /// 根据后台的等级Id查找等级优惠内容
        /// </summary>
        /// <param name="MembershipLevelId">等级Id</param>
        /// <returns></returns>
        public MemberLevelRelation GetMemberCouponRelationInfo(int MembershipLevelId)
        {
            var list_MemberCouponRelation = MemberLevelRelationOper.Instance.SelectById(MembershipLevelId);
            MemberLevelRelation MemRelation;
            if (list_MemberCouponRelation != null && list_MemberCouponRelation.Count != 0)
            {
                MemRelation = list_MemberCouponRelation.FirstOrDefault();
            }
            else
            {
                MemRelation = null;
            }
            return MemRelation;
        }
        /// <summary>
        /// 更新等级优惠内容信息
        /// </summary>
        /// <param name="MemRelation">会员卡与其对应的优惠内容的关系表信息</param>
        /// <returns></returns>
        public bool update_MemberCouponRelationInfo(MemberLevelRelation MemRelation)
        {
            var update = MemberLevelRelationOper.Instance.UpdateCouponContains(MemRelation);
            return update;
        }
        /// <summary>
        /// 根据Id删除优惠信息
        /// </summary>
        /// <param name="CouponContainsId">优惠内容Id</param>
        /// <returns></returns>
        public bool delete_MemberCouponRelationById(int CouponContainsId)
        {
            return MemberLevelRelationOper.Instance.Delete_MemberCouponRelatioById(CouponContainsId);
        }
        /// <summary>
        /// 添加优惠信息
        /// </summary>
        /// <param name="MemRelation">会员卡与其对应的优惠内容的关系表信息</param>
        /// <returns></returns>
        public bool Insert_MemberCouponRelation(MemberLevelRelation MemRelation)
        {
            var list = MemberLevelRelationOper.Instance.Check_MemberCouponRelation(MemRelation.CouponContains);
            if (list.Count > 0)
            {
                return false;
            }
            else
            {
                return MemberLevelRelationOper.Instance.Insert(MemRelation);
            }

        }
    }
}
