using Common.Enum_My;
using Common.Filter;
using Common.Filter.WebApi;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using GZRYVillageWeb.Request.ApiRequest;
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

        /// <summary>
        /// 获得电子储值卡列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetElecCardListResponse>> GetElecCardList(TokenAndHostRequest request)
        {
            var CardResult = Cache_ElectronicCard.Instance.GetElectronicList(request.UserToken);
            List<GetElecCardListResponse> List_Response = new List<GetElecCardListResponse>();
            ResultJsonModel<List<GetElecCardListResponse>> result = new ResultJsonModel<List<GetElecCardListResponse>>();
            if (Url != null)
            {
                request.Host = Url.Request.Headers.Host;
            }
            foreach (var item in CardResult.Item1)
            {

                GetElecCardListResponse response = new GetElecCardListResponse(item, CardResult.Item2, CardResult.Item3, request.Host);
                if (response.CardImage != null)
                {
                    List_Response.Add(response);
                }
            }
            if (List_Response.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                var ZeroListResponse = List_Response.Where(p => p.CardMoney == 0).OrderByDescending(p => p.ElectronicCardRelationId).ToList();
                var NotZeroListResponse = List_Response.Where(p => p.CardMoney != 0).OrderByDescending(p => p.ElectronicCardRelationId).ToList();
                foreach (var item in ZeroListResponse)
                {
                    NotZeroListResponse.Add(item);
                }
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = NotZeroListResponse;
            }
            return result;
        }

        /// <summary>
        /// 获得第一张电子储值卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<GetElecCardListResponse> GetFirstElecCard(UserTokenRequest request)
        {
            ResultJsonModel<GetElecCardListResponse> jsonResult = new ResultJsonModel<GetElecCardListResponse>();
            TokenAndHostRequest hostrequest = new TokenAndHostRequest { Host = Url.Request.Headers.Host, UserToken = request.UserToken };
            var result = GetElecCardList(hostrequest);
            jsonResult.HttpCode = result.HttpCode;
            jsonResult.Message = result.Message;
            if (result.HttpCode == 200)
            {
                jsonResult.Model1 = result.Model1.FirstOrDefault();
            }
            return jsonResult;
        }

        /// <summary>
        /// 解除电子储存卡绑定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson RelieveBindElecCard(ElecRelationRequest request)
        {
            var result = Cache_ElectronicCard.Instance.RelieveBindElecCard(request.UserToken, request.RelationId.Value);
            ResultJson resultjson = new ResultJson();
            if (result == "false")
            {
                resultjson.HttpCode = 300;
                resultjson.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else if (result == "true")
            {
                resultjson.HttpCode = 200;
                resultjson.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                resultjson.HttpCode = 300;
                resultjson.Message = result;
            }
            return resultjson;
        }

        /// <summary>
        /// 存储卡消费记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetConsumptionRecordsResponse>> ConsumptionRecords(ElecRequest request)
        {
            Token token = new Token(request.UserToken);
            var result = Cache_Consumption.Instance.SelcctConsumptionByUserId(request.CardId.Value);
            List<GetConsumptionRecordsResponse> ListResponse = new List<GetConsumptionRecordsResponse>();
            ResultJsonModel<List<GetConsumptionRecordsResponse>> resultjson = new ResultJsonModel<List<GetConsumptionRecordsResponse>>();
            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    GetConsumptionRecordsResponse response = new GetConsumptionRecordsResponse(item);
                    ListResponse.Add(response);
                }
                resultjson.HttpCode = 200;
                resultjson.Message = Enum_Message.SuccessMessage.Enum_GetString();
                resultjson.Model1 = ListResponse;
            }
            else
            {
                resultjson.HttpCode = 300;
                resultjson.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return resultjson;
        }

        /// <summary>
        /// 卡片二维码
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<string> QRCode(ElecCardIdRequest request)
        {
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            if (request != null)
            {
                Token token = new Token(request.UserToken);
                var str = "UserId:" + token.Payload.UserID + ";CardId:" + request.CardId + ";Time:" + DateTime.Now.ToString() + ";";
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = EncryptionHelper.Instance.DESEncrypt(str);
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