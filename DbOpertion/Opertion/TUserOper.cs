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
    public partial class TUserOper : SingleTon<TUserOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<TUser> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<TUser>();
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
        public TUser SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<TUser>();
            query.Where(p => p.UserId == KeyId);
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
        public List<TUser> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<TUser>();
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
            var query = new LambdaQuery<TUser>();
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
            var delete = new LambdaDelete<TUser>();
            delete.Where(p => p.UserId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="tuser">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(TUser tuser, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<TUser>();
            if (!tuser.UserId.IsNullOrEmpty())
            {
                update.Where(p => p.UserId == tuser.UserId);
            }
            if (!tuser.UserName.IsNullOrEmpty())
            {
                update.Set(p => p.UserName == tuser.UserName);
            }
            if (!tuser.UserNickName.IsNullOrEmpty())
            {
                update.Set(p => p.UserNickName == tuser.UserNickName);
            }
            if (!tuser.UserPassword.IsNullOrEmpty())
            {
                update.Set(p => p.UserPassword == tuser.UserPassword);
            }
            if (!tuser.UserPhone.IsNullOrEmpty())
            {
                update.Set(p => p.UserPhone == tuser.UserPhone);
            }
            if (!tuser.UserEmail.IsNullOrEmpty())
            {
                update.Set(p => p.UserEmail == tuser.UserEmail);
            }
            if (!tuser.UserImage.IsNullOrEmpty())
            {
                update.Set(p => p.UserImage == tuser.UserImage);
            }
            if (!tuser.ConsumptionTime.IsNullOrEmpty())
            {
                update.Set(p => p.ConsumptionTime == tuser.ConsumptionTime);
            }
            if (!tuser.LevelScore.IsNullOrEmpty())
            {
                update.Set(p => p.LevelScore == tuser.LevelScore);
            }
            if (!tuser.CreateTime.IsNullOrEmpty())
            {
                update.Set(p => p.CreateTime == tuser.CreateTime);
            }
            if (!tuser.DiamondsMoney.IsNullOrEmpty())
            {
                update.Set(p => p.DiamondsMoney == tuser.DiamondsMoney);
            }
            if (!tuser.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == tuser.IsDelete);
            }
            if (!tuser.Sex.IsNullOrEmpty())
            {
                update.Set(p => p.Sex == tuser.Sex);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="tuser">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(TUser tuser, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<TUser>();
            if (!tuser.UserName.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserName == tuser.UserName);
            }
            if (!tuser.UserNickName.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserNickName == tuser.UserNickName);
            }
            if (!tuser.UserPassword.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserPassword == tuser.UserPassword);
            }
            if (!tuser.UserPhone.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserPhone == tuser.UserPhone);
            }
            if (!tuser.UserEmail.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserEmail == tuser.UserEmail);
            }
            if (!tuser.UserImage.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserImage == tuser.UserImage);
            }
            if (!tuser.ConsumptionTime.IsNullOrEmpty())
            {
                insert.Insert(p => p.ConsumptionTime == tuser.ConsumptionTime);
            }
            if (!tuser.LevelScore.IsNullOrEmpty())
            {
                insert.Insert(p => p.LevelScore == tuser.LevelScore);
            }
            if (!tuser.CreateTime.IsNullOrEmpty())
            {
                insert.Insert(p => p.CreateTime == tuser.CreateTime);
            }
            if (!tuser.DiamondsMoney.IsNullOrEmpty())
            {
                insert.Insert(p => p.DiamondsMoney == tuser.DiamondsMoney);
            }
            if (!tuser.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == tuser.IsDelete);
            }
            if (!tuser.Sex.IsNullOrEmpty())
            {
                insert.Insert(p => p.Sex == tuser.Sex);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
