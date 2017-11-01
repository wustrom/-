using Common.Filter;
using Common.Filter.WebApi;
using Common.Result;
using DbOpertion.Cache;
using GZRYVillageWeb.Request.ApiRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace GZRYVillageWeb.Controllers.WebApiControllers
{
    /// <summary>
    /// 我的俱乐部控制器
    /// </summary>
    public class MyClubController : ApiController
    {
        /// <summary>
        /// Session
        /// </summary>
        HttpSessionState session = HttpContext.Current.Session;

        /// <summary>
        /// 绑定卡片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson BindCard(MemberCardVaildRequest request)
        {
            ResultJson jsonResult = new ResultJson();
            if (Cache_MemberShipCard.Instance.Bind_Card(request.Token, request.CardName, request.CardPassword, request.Active.Value))
            {
                jsonResult.HttpCode = 200;
                jsonResult.Message = "卡片绑定成功";
            }
            else
            {
                jsonResult.HttpCode = 300;
                jsonResult.Message = "卡片绑定失败";
            }
            return jsonResult;
        }
    }
}