using Common.Enum_My;
using Common.Filter;
using Common.Filter.WebApi;
using Common.Push.YouMenOpertion;
using Common.Result;
using DbOpertion.Cache;
using GZRYVillageWeb.Request.ApiRequest.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GZRYVillageWeb.Controllers.WebApiControllers
{
    /// <summary>
    /// 推送控制器
    /// </summary>
    public class PushController : ApiController
    {
        /// <summary>
        /// 推送Android用户
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel PushByAndriodUser()
        {
            ResultJsonModel result = new ResultJsonModel();
            var ret = YouMenOpertion.Instance.AndriodPushByAllUser("评论提醒", "您的评论有回复", "您的评论有回复咯。。。。。", "评论提醒-UID:123");
            if (ret.ret == "SUCCESS")
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = ret.data.error_code.ToString();
            }
            return result;
        }

        /// <summary>
        /// 推送IOS用户
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel PushByIOSUser()
        {
            ResultJsonModel result = new ResultJsonModel();
            var ret = YouMenOpertion.Instance.IOSPushByAllUser("您的评论有回复", "评论提醒-UID:123");
            if (ret.ret == "SUCCESS")
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = ret.data.error_code.ToString();
            }
            return result;
        }
    }
}