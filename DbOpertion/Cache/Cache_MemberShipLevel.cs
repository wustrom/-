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
    public partial class Cache_MemberShipLevel : SingleTon<Cache_MemberShipLevel>
    {
        /// <summary>
        /// 显示会员等级列表
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">排序主键</param>
        /// <param name="start">开始条数</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public Tuple<List<MemberShipLevel>, int, int> SelectMemberShipLevelList(string SearchKey, string Key, int start, int PageSize, DataTablesOrderDir desc)
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
            var list = MemberShipLevelOper.Instance.SelectMemberShipLevelList(SearchKey, Key, start, PageSize, asc);
            var All_Count = MemberShipLevelOper.Instance.SelectMemberShipLevelListCount(null);
            var Count = MemberShipLevelOper.Instance.SelectMemberShipLevelListCount(SearchKey);
            return new Tuple<List<MemberShipLevel>, int, int>(list, All_Count, Count);
        }
        /// <summary>
        /// 根据后台等级Id筛选等级信息
        /// </summary>
        /// <param name="MembershipLevelId"></param>
        /// <returns></returns>
        public MemberShipLevel GetMemberShipLevelInfo(int MembershipLevelId)
        {
            var List_MemberLevel = MemberShipLevelOper.Instance.SelectById(MembershipLevelId);
            MemberShipLevel level;
            if (List_MemberLevel != null && List_MemberLevel.Count != 0)
            {
                level = List_MemberLevel.FirstOrDefault();
            }
            else
            {
                level = null;
            }
            return level;
        }
        /// <summary>
        /// 更新会员等级信息
        /// </summary>
        /// <param name="levelInfo"></param>
        /// <returns></returns>
        public string UpdateMenberLevelInfo(MemberShipLevel levelInfo)
        {
            var list = MemberShipLevelOper.Instance.Check_MembershipLevelName(levelInfo.LevelName, levelInfo.MembershipLevelId);
            if (list.Count > 0)
            {
                return "";
            }
            else
            {
                var update = MemberShipLevelOper.Instance.Update(levelInfo);
                return update.ToString().ToLower();
            }

        }

    }
}