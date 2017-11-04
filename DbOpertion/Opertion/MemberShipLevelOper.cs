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

namespace DbOpertion.Operation
{
    public partial class MemberShipLevelOper : SingleTon<MemberShipLevelOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberShipLevel> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryList(connection, transaction);
        }


        /// <summary>
        /// 根据主键筛选数据
        /// </summary>
        /// <param name="KeyId">主键Id</param>
        /// <returns>是否成功</returns>
        public MemberShipLevel SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            query.Where(p => p.MembershipLevelId == KeyId);
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 根据分页筛选数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberShipLevel> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize, connection, transaction);
        }

        /// <summary>
        /// 数据条数
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="start">开始数据</param>
        ///  <param name="PageSize">页面长度</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public int SelectCount(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipLevel>();
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryCount(connection, transaction);
        }


        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="KeyId">主键Id</param>
        /// <returns>是否成功</returns>
        public bool DeleteById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<MemberShipLevel>();
            delete.Where(p => p.MembershipLevelId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="membershiplevel">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(MemberShipLevel membershiplevel, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<MemberShipLevel>();
            if (!membershiplevel.MembershipLevelId.IsNullOrEmpty())
            {
                update.Where(p => p.MembershipLevelId == membershiplevel.MembershipLevelId);
            }
            if (!membershiplevel.LevelName.IsNullOrEmpty())
            {
                update.Set(p => p.LevelName == membershiplevel.LevelName);
            }
            if (!membershiplevel.LevelMin.IsNullOrEmpty())
            {
                update.Set(p => p.LevelMin == membershiplevel.LevelMin);
            }
            if (!membershiplevel.LevelMax.IsNullOrEmpty())
            {
                update.Set(p => p.LevelMax == membershiplevel.LevelMax);
            }
            if (!membershiplevel.NeedThing.IsNullOrEmpty())
            {
                update.Set(p => p.NeedThing == membershiplevel.NeedThing);
            }
            if (!membershiplevel.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == membershiplevel.IsDelete);
            }
            if (!membershiplevel.NextLevelId.IsNullOrEmpty())
            {
                update.Set(p => p.NextLevelId == membershiplevel.NextLevelId);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="membershiplevel">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(MemberShipLevel membershiplevel, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<MemberShipLevel>();
            if (!membershiplevel.LevelName.IsNullOrEmpty())
            {
                insert.Insert(p => p.LevelName == membershiplevel.LevelName);
            }
            if (!membershiplevel.LevelMin.IsNullOrEmpty())
            {
                insert.Insert(p => p.LevelMin == membershiplevel.LevelMin);
            }
            if (!membershiplevel.LevelMax.IsNullOrEmpty())
            {
                insert.Insert(p => p.LevelMax == membershiplevel.LevelMax);
            }
            if (!membershiplevel.NeedThing.IsNullOrEmpty())
            {
                insert.Insert(p => p.NeedThing == membershiplevel.NeedThing);
            }
            if (!membershiplevel.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == membershiplevel.IsDelete);
            }
            if (!membershiplevel.NextLevelId.IsNullOrEmpty())
            {
                insert.Insert(p => p.NextLevelId == membershiplevel.NextLevelId);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
