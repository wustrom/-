using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using System.Data;
using DbOpertion.Models;
using Common.Result;

namespace DbOpertion.Operation
{
    public partial class MemberShipLevelOper : SingleTon<MemberShipLevelOper>
    {
        /// <summary>
        /// 查询会员等级列表
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始条数</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<MemberShipLevel> SelectMemberShipLevelList(string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.LevelName.Contains(SearchKey) || p.LevelMin.Contains(SearchKey) || p.LevelMin.Contains(SearchKey));
            }
            return query.GetQueryPageList(start, PageSize);
        }
        /// <summary>
        /// 根据分页筛选会员等级数据
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public int SelectMemberShipLevelListCount(string SearchKey)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.LevelName.Contains(SearchKey) || p.LevelMin.Contains(SearchKey) || p.LevelMin.Contains(SearchKey));
            }
            return query.GetQueryCount();
        }
        /// <summary>
        /// 根据等级Id查询对应的等级详细信息
        /// </summary>
        /// <param name="MembershipLevelId">会员等级Id</param>
        /// <returns></returns>
        public List<MemberShipLevel> SelectById(int MembershipLevelId)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            query.Where(p => p.MembershipLevelId == MembershipLevelId);
            return query.GetQueryList();
        }
        /// <summary>
        /// 判断等级名称是否相同
        /// </summary>
        /// <param name="LevelName">等级名称</param>
        /// <param name="MembershipLevelId">会员等级Id</param>
        /// <returns></returns>
        public List<MemberShipLevel> Check_MembershipLevelName(string LevelName, int MembershipLevelId)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            query.Where(p => p.LevelName == LevelName && p.MembershipLevelId != MembershipLevelId);
            return query.GetQueryList();
        }
        /// <summary>
        /// 修改当前等级时同时修改下一等级所需的条件
        /// </summary>
        /// <param name="MembershipLevelId">会员卡等级Id</param>
        /// <param name="LevelMin">当前等级所需条件</param>
        /// <param name="connection">表示到数据源的已打开连接</param>
        /// <param name="transaction">表示要在数据源上执行的事物</param>
        /// <returns></returns>
        public bool Update_MemberLevelMin(int? MembershipLevelId,int? LevelMin,IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<MemberShipLevel>();
            if (!MembershipLevelId.IsNullOrEmpty())
            {
                update.Where(p => p.MembershipLevelId == MembershipLevelId);
            }
            if (!LevelMin.IsNullOrEmpty())
            {
                update.Set(p => p.LevelMin == LevelMin);
            }
            return update.GetUpdateResult(connection, transaction);
        }

    }
}
