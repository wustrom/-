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
    public partial class ElectronicCardRelationOper : SingleTon<ElectronicCardRelationOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<ElectronicCardRelation> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCardRelation>();
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
        public ElectronicCardRelation SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCardRelation>();
            query.Where(p => p.ElectronicCardRelationId == KeyId);
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
        public List<ElectronicCardRelation> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCardRelation>();
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
            var query = new LambdaQuery<ElectronicCardRelation>();
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
            var delete = new LambdaDelete<ElectronicCardRelation>();
            delete.Where(p => p.ElectronicCardRelationId == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="electroniccardrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(ElectronicCardRelation electroniccardrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<ElectronicCardRelation>();
            if (!electroniccardrelation.ElectronicCardRelationId.IsNullOrEmpty())
            {
                update.Where(p => p.ElectronicCardRelationId == electroniccardrelation.ElectronicCardRelationId);
            }
            if (!electroniccardrelation.ElectronicCardId.IsNullOrEmpty())
            {
                update.Set(p => p.ElectronicCardId == electroniccardrelation.ElectronicCardId);
            }
            if (!electroniccardrelation.UserId.IsNullOrEmpty())
            {
                update.Set(p => p.UserId == electroniccardrelation.UserId);
            }
            if (!electroniccardrelation.IsDelete.IsNullOrEmpty())
            {
                update.Set(p => p.IsDelete == electroniccardrelation.IsDelete);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="electroniccardrelation">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(ElectronicCardRelation electroniccardrelation, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<ElectronicCardRelation>();
            if (!electroniccardrelation.ElectronicCardId.IsNullOrEmpty())
            {
                insert.Insert(p => p.ElectronicCardId == electroniccardrelation.ElectronicCardId);
            }
            if (!electroniccardrelation.UserId.IsNullOrEmpty())
            {
                insert.Insert(p => p.UserId == electroniccardrelation.UserId);
            }
            if (!electroniccardrelation.IsDelete.IsNullOrEmpty())
            {
                insert.Insert(p => p.IsDelete == electroniccardrelation.IsDelete);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
