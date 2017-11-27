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
        /// 常见问题
        /// </summary>
        /// <returns>是否成功</returns>
        public CommonHtml CommonProblem(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CommonHtml>();
            query.Where(p => p.HtmlType == "commonproblem");
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 更新常见问题
        /// </summary>
        /// <returns>是否成功</returns>
        public bool UpdateCommonProblem(string Content, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<CommonHtml>();
            update.Where(p => p.HtmlType == "commonproblem");
            update.Set(p => p.HtmlContent == Content);
            return update.GetUpdateResult(connection, transaction);
        }

        /// <summary>
        /// 使用条款
        /// </summary>
        /// <returns>是否成功</returns>
        public CommonHtml TermsOfUse(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<CommonHtml>();
            query.Where(p => p.HtmlType == "termsofuse");
            return query.GetQueryList(connection, transaction).FirstOrDefault();
        }

        /// <summary>
        /// 更新使用条款
        /// </summary>
        /// <returns>是否成功</returns>
        public bool UpdateTermsOfUse(string Content, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var update = new LambdaUpdate<CommonHtml>();
            update.Where(p => p.HtmlType == "termsofuse");
            update.Set(p => p.HtmlContent == Content);
            return update.GetUpdateResult(connection, transaction);
        }

    }
}
