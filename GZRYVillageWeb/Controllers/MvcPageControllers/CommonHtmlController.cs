using Common.Enum_My;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{
    /// <summary>
    /// 公共网页控制器
    /// </summary>
    public class CommonHtmlController : Controller
    {


        /// <summary>
        /// 使用条款
        /// </summary>
        /// <returns></returns>
        public ActionResult TermsOfUse()
        {
            ViewBag.CommonProblem = Cache_Html.Instance.CommonProblem();
            ViewBag.TermsOfUse = Cache_Html.Instance.TermsOfUse();
            return View();
        }

        /// <summary>
        /// 公共网页
        /// </summary>
        /// <returns></returns>
        public JsonResult CommonProblem()
        {
            var CommonProblem = Cache_Html.Instance.CommonProblem();
            var TermsOfUse = Cache_Html.Instance.TermsOfUse();
            ResultJsonModel<CommonHtml, CommonHtml> result = new ResultJsonModel<CommonHtml, CommonHtml>();
            if (CommonProblem == null || TermsOfUse == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = CommonProblem;
                result.Model2 = TermsOfUse;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改常见问题
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateCommonProblem(string HtmlContent)
        {
            var content = HtmlCodeHelper.HtmlToString(HtmlContent);
            ResultJson result = new ResultJson();
            if (!Cache_Html.Instance.UpdateCommonProblem(content))
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改使用条款
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateTermsOfUse(string HtmlContent)
        {
            var content = HtmlCodeHelper.HtmlToString(HtmlContent);
            ResultJson result = new ResultJson();
            if (!Cache_Html.Instance.UpdateTermsOfUse(content))
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}