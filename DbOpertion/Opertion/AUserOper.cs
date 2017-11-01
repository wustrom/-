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
    public partial class AUserOper : SingleTon<AUserOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<AUser> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<AUser>();
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
        public AUser SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<AUser>();
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
        public List<AUser> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<AUser>();
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
            var query = new LambdaQuery<AUser>();
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
            var delete = new LambdaDelete<AUser>();
            delete.Where(p => p.UserId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="auser">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(AUser auser, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<AUser>();
            if (!auser.UserId.IsNullOrEmpty())
            {
                update.Where(p => p.UserId == auser.UserId);
            }
            if (!auser.UserName.IsNullOrEmpty())
            {
                update.Set(p => p.UserName == auser.UserName);
            }
            if (!auser.UserNickName.IsNullOrEmpty())
            {
                update.Set(p => p.UserNickName == auser.UserNickName);
            }
            if (!auser.UserPassword.IsNullOrEmpty())
            {
                update.Set(p => p.UserPassword == auser.UserPassword);
            }
            if (!auser.UserPhone.IsNullOrEmpty())
            {
                update.Set(p => p.UserPhone == auser.UserPhone);
            }
            if (!auser.UserEmail.IsNullOrEmpty())
            {
                update.Set(p => p.UserEmail == auser.UserEmail);
            }
            if (!auser.UserImage.IsNullOrEmpty())
            {
                update.Set(p => p.UserImage == auser.UserImage);
            }
            if (!auser.ConsumptionTime.IsNullOrEmpty())
            {
                update.Set(p => p.ConsumptionTime == auser.ConsumptionTime);
            }
            if (!auser.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == auser.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="auser">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(AUser auser, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<AUser>();
            if (!auser.UserName.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserName == auser.UserName);
            }
            if (!auser.UserNickName.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserNickName == auser.UserNickName);
            }
            if (!auser.UserPassword.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserPassword == auser.UserPassword);
            }
            if (!auser.UserPhone.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserPhone == auser.UserPhone);
            }
            if (!auser.UserEmail.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserEmail == auser.UserEmail);
            }
            if (!auser.UserImage.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserImage == auser.UserImage);
            }
            if (!auser.ConsumptionTime.IsNullOrEmpty())
            {
                insert.Insert(p => p.ConsumptionTime == auser.ConsumptionTime);
            }
            if (!auser.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == auser.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
