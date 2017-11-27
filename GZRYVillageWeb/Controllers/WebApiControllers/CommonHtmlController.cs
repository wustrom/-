using Common.Enum_My;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GZRYVillageWeb.Controllers.WebApiControllers
{
    /// <summary>
    /// 公用网页控制器
    /// </summary>
    public class CommonHtmlController : ApiController
    {
        /// <summary>
        /// 查看常见问题
        /// </summary>
        /// <returns></returns>
        public ResultJsonModel<string> CommonProblem()
        {
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            var html = Cache_Html.Instance.CommonProblem();
            if (html == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = html.HtmlContent;
            }
            return result;
        }

        /// <summary>
        /// 查看使用条款
        /// </summary>
        /// <returns></returns>
        public ResultJsonModel<string> TermsOfUse()
        {
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            var html = Cache_Html.Instance.TermsOfUse();
            if (html == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = html.HtmlContent;
            }
            return result;
        }
    }
}