using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOpertion.Models;
using DbOpertion.Operation;

namespace DbOpertion.Cache
{
    public class Cache_Html : SingleTon<Cache_Html>
    {
        /// <summary>
        /// 常见问题
        /// </summary>
        /// <returns></returns>
        public CommonHtml CommonProblem()
        {
            return CommonHtmlOper.Instance.CommonProblem();
        }

        /// <summary>
        /// 使用条款
        /// </summary>
        /// <returns></returns>
        public CommonHtml TermsOfUse()
        {
            return CommonHtmlOper.Instance.TermsOfUse();
        }

        /// <summary>
        /// 更新常见问题
        /// </summary>
        /// <returns></returns>
        public bool UpdateCommonProblem(string Content)
        {
            return CommonHtmlOper.Instance.UpdateCommonProblem(Content);
        }

        /// <summary>
        /// 更新常见问题
        /// </summary>
        /// <returns></returns>
        public bool UpdateTermsOfUse(string Content)
        {
            return CommonHtmlOper.Instance.UpdateTermsOfUse(Content);
        }
    }
}
