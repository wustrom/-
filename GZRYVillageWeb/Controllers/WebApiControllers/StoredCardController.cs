using Common.Enum_My;
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
using System.Web.Http;

namespace GZRYVillageWeb.Controllers.WebApiControllers
{
    /// <summary>
    /// 电子储值卡控制器
    /// </summary>
    public class StoredCardController : ApiController
    {
        /// <summary>
        /// 绑定电子储值卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson BindElecCard(BindElecCardRequest request)
        {
            var CardResult = Cache_ElectronicCard.Instance.Bind_Card(request.UserToken, request.CardName, request.PassWord);
            ResultJson result = new ResultJson();
            if (CardResult == "true")
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataChangeSuccessMessage.Enum_GetString();
            }
            else if (CardResult == "false")
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = CardResult;
            }
            return result;
        }


    }
}