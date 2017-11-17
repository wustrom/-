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
        /// 筛选用户等级
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <param name="Key"></param>
        /// <param name="start"></param>
        /// <param name="PageSize"></param>
        /// <param name="desc"></param>
        /// <returns>item1:当前等级 item2:下个等级 item3:用户等级分数 item4:用户</returns>
        public Tuple<MemberShipLevel, MemberShipLevel, int, decimal> SelectUserLevel(int UserId)
        {
            var UserInfo = TUserOper.Instance.SelectById(UserId);
            if (UserInfo != null)
            {
                var MemberShipLevel_List = MemberShipLevelOper.Instance.SelectAll(null);
                if (MemberShipLevel_List == null || MemberShipLevel_List.Count == 0)
                {
                    return null;
                }
                else
                {
                    if (UserInfo.LevelScore == null)
                    {
                        UserInfo.LevelScore = 0;
                    }
                    var CurrentLevel = MemberShipLevel_List.Where(p => p.LevelMin <= UserInfo.LevelScore && (p.LevelMax + p.LevelMin) > UserInfo.LevelScore).ToList().FirstOrDefault();
                    var NextLevel = MemberShipLevel_List.Where(p => p.MembershipLevelId == CurrentLevel.NextLevelId).ToList().FirstOrDefault();
                    if (CurrentLevel == null || NextLevel == null)
                    {
                        return null;
                    }
                    else
                    {
                        return new Tuple<MemberShipLevel, MemberShipLevel, int, decimal>(item1: CurrentLevel, item2: NextLevel, item3: UserInfo.LevelScore == null ? 0 : UserInfo.LevelScore.Value, item4: UserInfo.DiamondsMoney == null ? 0 : UserInfo.DiamondsMoney.Value);
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 筛选等级信息
        /// </summary>
        /// <returns></returns>
        public Tuple<List<MemberShipLevel>, List<MemberLevelRelation>> SelectLevelListInfo()
        {
            var LevelInfo_List = MemberShipLevelOper.Instance.SelectAll(null, false);
            var MemberRelation_List = MemberLevelRelationOper.Instance.SelectAll(null, false);
            return new Tuple<List<MemberShipLevel>, List<MemberLevelRelation>>(item1: LevelInfo_List, item2: MemberRelation_List);
        }
    }
}