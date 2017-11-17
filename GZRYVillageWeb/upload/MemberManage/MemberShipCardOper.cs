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
    public partial class MemberShipCardOper : SingleTon<MemberShipCardOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<MemberShipCard> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipCard>();
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
        public MemberShipCard SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipCard>();
            query.Where(p => p.MemberShipCardId == KeyId);
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
        public List<MemberShipCard> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<MemberShipCard>();
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
            var query = new LambdaQuery<MemberShipCard>();
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
            var delete = new LambdaDelete<MemberShipCard>();
            delete.Where(p => p.MemberShipCardId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="membershipcard">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(MemberShipCard membershipcard, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<MemberShipCard>();
            if (!membershipcard.MemberShipCardId.IsNullOrEmpty())
            {
                update.Where(p => p.MemberShipCardId == membershipcard.MemberShipCardId);
            }
            if (!membershipcard.CardName.IsNullOrEmpty())
            {
                update.Set(p => p.CardName == membershipcard.CardName);
            }
            if (!membershipcard.CardPassword.IsNullOrEmpty())
            {
                update.Set(p => p.CardPassword == membershipcard.CardPassword);
            }
            if (!membershipcard.MemberShipTypeId.IsNullOrEmpty())
            {
                update.Set(p => p.MemberShipTypeId == membershipcard.MemberShipTypeId);
            }
            if (!membershipcard.UserId.IsNullOrEmpty())
            {
                update.Set(p => p.UserId == membershipcard.UserId);
            }
            if (!membershipcard.CreateDate.IsNullOrEmpty())
            {
                update.Set(p => p.CreateDate == membershipcard.CreateDate);
            }
            if (!membershipcard.ReleaseDate.IsNullOrEmpty())
            {
                update.Set(p => p.ReleaseDate == membershipcard.ReleaseDate);
            }
            if (!membershipcard.IsUser.IsNullOrEmpty())
            {
                update.Set(p => p.IsUser == membershipcard.IsUser);
            }
            if (!membershipcard.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == membershipcard.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="membershipcard">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(MemberShipCard membershipcard, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<MemberShipCard>();
            if (!membershipcard.CardName.IsNullOrEmpty())
            {
                insert.Insert(p => p.CardName == membershipcard.CardName);
            }
            if (!membershipcard.CardPassword.IsNullOrEmpty())
            {
                insert.Insert(p => p.CardPassword == membershipcard.CardPassword);
            }
            if (!membershipcard.MemberShipTypeId.IsNullOrEmpty())
            {
                insert.Insert(p => p.MemberShipTypeId == membershipcard.MemberShipTypeId);
            }
            if (!membershipcard.UserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserId == membershipcard.UserId);
            }
            if (!membershipcard.CreateDate.IsNullOrEmpty())
            {
                insert.Insert(p => p.CreateDate == membershipcard.CreateDate);
            }
            if (!membershipcard.ReleaseDate.IsNullOrEmpty())
            {
                insert.Insert(p => p.ReleaseDate == membershipcard.ReleaseDate);
            }
            if (!membershipcard.IsUser.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsUser == membershipcard.IsUser);
            }
            if (!membershipcard.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == membershipcard.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
