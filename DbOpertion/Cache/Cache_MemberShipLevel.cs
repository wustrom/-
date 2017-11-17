using Common;
using Common.Helper;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <param name="MembershipLevelId">会员等级Id</param>
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
        /// <param name="levelInfo">会员等级信息</param>
        /// <returns></returns>
        public string UpdateMenberLevelInfo(MemberShipLevel levelInfo, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var list = MemberShipLevelOper.Instance.Check_MembershipLevelName(levelInfo.LevelName, levelInfo.MembershipLevelId);
            if (list.Count > 0)
            {
                return "";
            }
            else
            {
                var update = MemberShipLevelOper.Instance.Update(levelInfo, connection, transaction);
                return update.ToString().ToLower();
            }
        }
        /// <summary>
        /// 修改下一等级的信息
        /// </summary>
        /// <param name="MembershipLevelId">会员等级Id</param>
        /// <param name="LevelMin">该等级所需条件</param>
        /// <returns></returns>
        public bool UpdateMemLevelMin(int MembershipLevelId, int LevelMin)
        {
            return MemberShipLevelOper.Instance.Update_MemberLevelMin(MembershipLevelId, LevelMin);
        }
        /// <summary>
        /// 用事物来修改会员等级信息
        /// </summary>
        /// <param name="level">会员等级信息</param>
        /// <returns></returns>
        public string UpdateMemLevelScore(MemberShipLevel level)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var transaction = sqlHelper.GetTransaction(connection);
            var LevelMax = level.LevelMax;
            var result = "";
            try
            {
                result = UpdateMenberLevelInfo(level, connection, transaction);
                if (result != "true")
                {
                    transaction.Rollback();
                    connection.Close();
                    return result;
                }
                var LevelBefor = MemberShipLevelOper.Instance.SelectById(level.MembershipLevelId, connection, transaction);
                if (LevelBefor.LevelMax != LevelMax)
                {
                    LevelBefor.LevelMax = LevelMax;
                    var Count = MemberShipLevelOper.Instance.SelectCount(null, true, connection, transaction);
                    level = LevelBefor;
                    for (int i = 0; i < Count - 1; i++)
                    {
                        if (level.NextLevelId == null)
                        {
                            break;
                        }
                        result = MemberShipLevelOper.Instance.Update_MemberLevelMin(level.NextLevelId, level.LevelMax + level.LevelMin, connection, transaction).ToString().ToLower();
                        if (result != "true")
                        {
                            break;
                        }
                        level = MemberShipLevelOper.Instance.SelectById(level.NextLevelId.Value, connection, transaction);
                    }
                }
                if (result != "true")
                {
                    transaction.Rollback();
                    connection.Close();
                    return result;
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }

            return true.ToString().ToLower();
        }
        
    }
}