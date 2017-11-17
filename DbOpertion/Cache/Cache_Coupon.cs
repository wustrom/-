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
    /// <summary>
    /// 优惠卷缓存
    /// </summary>
    public partial class Cache_Coupon : SingleTon<Cache_Coupon>
    {
        /// <summary>
        /// 筛选全部
        /// </summary>
        /// <param name="Key">排序Key</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<Coupon> SelectAll(string Key, DataTablesOrderDir desc)
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
            return CouponOper.Instance.SelectAll(Key, asc);
        }

        /// <summary>
        /// 筛选全部条数
        /// </summary>
        /// <returns></returns>
        public int SelectAllCount()
        {
            return CouponOper.Instance.SelectCount(null);
        }

        /// <summary>
        /// 分页筛选
        /// </summary>
        /// <param name="SearchKey">搜索主键</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始序号</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序方式</param>
        /// <returns></returns>
        public List<Coupon> SelectByPage(string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
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
            return CouponOper.Instance.SelectByPage(SearchKey, Key, start, PageSize, asc);
        }

        /// <summary>
        /// 筛选搜索后的条数
        /// </summary>
        /// <returns></returns>
        public int SelectSearchCount(string SearchKey)
        {
            return CouponOper.Instance.SelectSearchCount(SearchKey);
        }

        /// <summary>
        /// 根据优惠券ID查找是否有对应的详细信息
        /// </summary>
        /// <param name="CouponID">优惠券Id</param>
        /// <returns></returns>
        public Coupon MemberGetCouponInfo(int CouponID)
        {
            var Coupon_List = CouponOper.Instance.SelectById(CouponID);
            Coupon card;
            if (Coupon_List != null && Coupon_List.Count != 0)
            {
                card = Coupon_List.FirstOrDefault();
            }
            else
            {
                card = null;
            }
            return card;
        }
        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public bool InsertCoupon(Coupon coupon)
        {
            var Check_Name = CouponOper.Instance.SelectCouponByName(coupon.CouponName);
            if (Check_Name.Count > 0)
            {
                return false;
            }
            else
            {
                return CouponOper.Instance.Insert(coupon);
            }

        }
        /// <summary>
        /// 显示已发放优惠券列表
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="start">开始数据</param>
        /// <param name="pageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<CouponUserRelationInfo>, int, int> SelectCouponUserRelationInfoList(int CouponId, string SearchKey, string ExpirationDate1, string ExpirationDate2, string ReleaseDate1, string ReleaseDate2, string Key, int start, int pageSize, DataTablesOrderDir desc)
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
            var list = CouponOper.Instance.Get_CouponUserRelationInfo(CouponId, ExpirationDate1,ExpirationDate2,ReleaseDate1,ReleaseDate2, SearchKey, Key, start, pageSize, asc);
            var All_Count = CouponOper.Instance.Select_CouponUserRelationInfoCount(CouponId, null, ExpirationDate1, ExpirationDate2, ReleaseDate1, ReleaseDate2);
            var Count = CouponOper.Instance.Select_CouponUserRelationInfoCount(CouponId, SearchKey, ExpirationDate1, ExpirationDate2, ReleaseDate1, ReleaseDate2);
            return new Tuple<List<CouponUserRelationInfo>, int, int>(list, All_Count, Count);

        }
        /// <summary>
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="CouponIds">优惠券Id</param>
        /// <returns></returns>
        public bool DeleteCouponByIds(List<int> CouponIds)
        {
            var flag = CouponOper.Instance.DeleteByIds(CouponIds);
            return flag;
        }
        /// <summary>
        /// 根据优惠券Id获得优惠券信息
        /// </summary>
        /// <param name="CouponId">优惠券Id</param>
        /// <returns></returns>
        public Coupon SelectCouponInfoById(int CouponId)
        {
            return CouponOper.Instance.SelectByCouponId(CouponId);

        }
        /// <summary>
        /// 根据Id修改优惠券信息
        /// </summary>
        /// <param name="coupon">优惠券信息</param>
        /// <returns></returns>
        public bool Update_CouponById(Coupon coupon)
        {
            var flag = CouponOper.Instance.Update(coupon);
            return flag;
        }
    }
}
