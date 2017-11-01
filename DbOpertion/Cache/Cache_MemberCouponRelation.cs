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
        public Tuple<List<MemberCouponRelation>, int, int> SelectMemberCouponRelationList(int MembershipLevelId, string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
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
            var list = MemberCouponRelationOper.Instance.SelectMemberCouponRelationList(MembershipLevelId, SearchKey, Key, start, PageSize, asc);
            var All_Count = MemberCouponRelationOper.Instance.SelectMemCouponRelationListCount(MembershipLevelId, null);
            var Count = MemberCouponRelationOper.Instance.SelectMemCouponRelationListCount(MembershipLevelId, SearchKey);
            return new Tuple<List<MemberCouponRelation>, int, int>(list, All_Count, Count);
        }
        /// <summary>
        /// 根据后台的等级Id查找等级优惠内容
        /// </summary>
        /// <param name="MembershipLevelId">等级Id</param>
        /// <returns></returns>
        public MemberCouponRelation GetMemberCouponRelationInfo(int CouponContainsId)
        {
            var list_MemberCouponRelation = MemberCouponRelationOper.Instance.SelectById(CouponContainsId);
            MemberCouponRelation MemRelation;
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
        /// <param name="MemRelation"></param>
        /// <returns></returns>
        public bool update_MemberCouponRelationInfo(MemberCouponRelation MemRelation)
        {
            var update = MemberCouponRelationOper.Instance.Update(MemRelation);
            return update;
        }
        /// <summary>
        /// 根据Id删除优惠信息
        /// </summary>
        /// <param name="CouponContainsId"></param>
        /// <returns></returns>
        public bool delete_MemberCouponRelationById(int CouponContainsId)
        {
            return MemberCouponRelationOper.Instance.Delete_MemberCouponRelatioById(CouponContainsId);
        }
        /// <summary>
        /// 添加优惠信息
        /// </summary>
        /// <param name="MemRelation"></param>
        /// <returns></returns>
        public bool Insert_MemberCouponRelation(MemberCouponRelation MemRelation)
        {
            var list = MemberCouponRelationOper.Instance.Chenk_MemberCouponRelation(MemRelation.CouponContains);
            if (list.Count > 0)
            {
                return false;
            }
            else
            {
                return MemberCouponRelationOper.Instance.Insert(MemRelation);
            }

        }
    }
}
