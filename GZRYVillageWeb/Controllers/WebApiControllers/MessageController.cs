using Common.Enum_My;
using Common.Filter;
using Common.Filter.WebApi;
using Common.Result;
using DbOpertion.Cache;
using GZRYVillageWeb.Request.ApiRequest;
using GZRYVillageWeb.Request.ApiRequest.Message;
using GZRYVillageWeb.Response.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace GZRYVillageWeb.Controllers.WebApiControllers
{
    /// <summary>
    /// 消息中心控制器
    /// </summary>
    public class MessageController : ApiController
    {
        /// <summary>
        /// 获得消息列表
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetMessageListResponse>> GetMessageList(HostRequest request)
        {
            ResultJsonModel<List<GetMessageListResponse>> result = new ResultJsonModel<List<GetMessageListResponse>>();
            //消息列表
            var MessageList = Cache_Message.Instance.GetMessageListWithOutHtml();
            if (Url != null)
            {
                if (request == null)
                {
                    request = new HostRequest();
                }
                request.Host = Url.Request.Headers.Host;
            }
            if (MessageList.Count > 0)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = new List<GetMessageListResponse>();
                foreach (var item in MessageList)
                {
                    result.Model1.Add(new GetMessageListResponse(item, request.Host));
                }
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获得消息Html
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<string> GetMessageHtml(GetMessageHtmlByIdRequest request)
        {
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            //消息列表
            var MessageList = Cache_Message.Instance.SelectById(request.MessageId);
            if (MessageList != null)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = MessageList.MessageContains;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }
    }
}