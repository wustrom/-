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
    public partial class CommonHtmlOper : SingleTon<CommonHtmlOper>
    {
        /// <summary>
        /// 筛选全部数据
        /// </summary>
        ///  <param name="Key">主键</param>
        ///  <param name="desc">排序</param>
        /// <returns>对象列表</returns>
        public List<CommonHtml> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CommonHtml>();
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
        public CommonHtml SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CommonHtml>();
            query.Where(p => p.HtmlID == KeyId);
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
        public List<CommonHtml> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CommonHtml>();
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
            var query = new LambdaQuery<CommonHtml>();
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
            var delete = new LambdaDelete<CommonHtml>();
            delete.Where(p => p.HtmlID == KeyId);
            return delete.GetDeleteResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="commonhtml">模型</param>
        /// <returns>是否成功</returns>
        public bool Update(CommonHtml commonhtml, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<CommonHtml>();
            if (!commonhtml.HtmlID.IsNullOrEmpty())
            {
                update.Where(p => p.HtmlID == commonhtml.HtmlID);
            }
            if (!commonhtml.HtmlType.IsNullOrEmpty())
            {
                update.Set(p => p.HtmlType == commonhtml.HtmlType);
            }
            if (!commonhtml.HtmlContent.IsNullOrEmpty())
            {
                update.Set(p => p.HtmlContent == commonhtml.HtmlContent);
            }
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <param name="commonhtml">模型</param>
        /// <returns>是否成功</returns>
        public bool Insert(CommonHtml commonhtml, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var insert = new LambdaInsert<CommonHtml>();
            if (!commonhtml.HtmlType.IsNullOrEmpty())
            {
                insert.Insert(p => p.HtmlType == commonhtml.HtmlType);
            }
            if (!commonhtml.HtmlContent.IsNullOrEmpty())
            {
                insert.Insert(p => p.HtmlContent == commonhtml.HtmlContent);
            }
            return insert.GetInsertResult(connection, transaction);
        }

    }
}
