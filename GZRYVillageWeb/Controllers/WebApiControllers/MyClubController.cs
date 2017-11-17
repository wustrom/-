using Common.Enum_My;
using Common.Filter;
using Common.Filter.WebApi;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.ApiRequest;
using GZRYVillageWeb.Response.ApiResponse;
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
            if (Cache_MemberShipCard.Instance.Bind_Card(request.UserToken, request.CardName, request.CardPassword, request.Active.Value))
            {
                jsonResult.HttpCode = 200;
                jsonResult.Message = "卡片绑定成功";
            }
            else
            {
                jsonResult.HttpCode = 300;
                jsonResult.Message = "卡片绑定失败";
                var a = "\\";
            }
            return jsonResult;
        }

        /// <summary>
        /// 获得用户等级信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<UserLevelResponse, List<GetConsumptionRecordsResponse>, List<GetMyCouponResponse>> GetMyLevelInfo(UserTokenRequest request)
        {
            Token token = new Token(request.UserToken);
            ResultJsonModel<UserLevelResponse, List<GetConsumptionRecordsResponse>, List<GetMyCouponResponse>> result = new ResultJsonModel<UserLevelResponse, List<GetConsumptionRecordsResponse>, List<GetMyCouponResponse>>();
            var LevelInfo = GetCurrentLevelInfo(request);
            result.HttpCode = LevelInfo.HttpCode;
            result.Message = LevelInfo.Message;
            result.Model1 = LevelInfo.Model1;
            if (LevelInfo.HttpCode == 200)
            {
                TokenAndPageRequest pageRequest = new TokenAndPageRequest();
                pageRequest.PageNo = 1;
                pageRequest.UserToken = request.UserToken;
                var consump = LastConsumptionRecord(request);
                if (consump.HttpCode == 200)
                {
                    result.Model2 = consump.Model1;
                }
                var coupon = GetMyCoupon(pageRequest);
                if (coupon.HttpCode == 200)
                {
                    result.Model3 = coupon.Model1;
                }
            }
            return result;
        }

        /// <summary>
        /// 获得当前用户等级信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<UserLevelResponse> GetCurrentLevelInfo(UserTokenRequest request)
        {
            Token token = new Token(request.UserToken);
            ResultJsonModel<UserLevelResponse> result = new ResultJsonModel<UserLevelResponse>();
            var Level_Result = Cache_MemberShipLevel.Instance.SelectUserLevel(token.Payload.UserID);
            if (Level_Result != null)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = new UserLevelResponse(Level_Result.Item1, Level_Result.Item2, Level_Result.Item3, Level_Result.Item4);
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获得全部等级信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetLevelListResponse>, UserLevelResponse> GetLevelListInfo(UserTokenRequest request)
        {
            ResultJsonModel<List<GetLevelListResponse>, UserLevelResponse> result = new ResultJsonModel<List<GetLevelListResponse>, UserLevelResponse>();
            var Level_Result = Cache_MemberShipLevel.Instance.SelectLevelListInfo();
            if (Level_Result != null)
            {
                result.Model1 = new List<GetLevelListResponse>();
                foreach (var item in Level_Result.Item1)
                {
                    result.Model1.Add(new GetLevelListResponse(item, Level_Result.Item2));
                }
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            var currentLevel = GetCurrentLevelInfo(request);
            if (currentLevel.HttpCode == 200)
            {
                result.Model2 = currentLevel.Model1;
            }
            return result;
        }

        /// <summary>
        /// 获得用户卡列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson<GetMyCardResponse> GetMyCardList(GetMyCardListRequest request)
        {
            Token token = new Token(request.UserToken);
            List<MemberShipCard> Card_List = Cache_MemberShipCard.Instance.SelectCardList(token.Payload.UserID, request.Active);
            List<MemberShipType> List_Type = Cache_MemberShipType.Instance.SelectAll();
            ResultJson<GetMyCardResponse> result = new ResultJson<GetMyCardResponse>();
            if (Url != null)
            {
                request.Host = Url.Request.Headers.Host;
            }
            if (Card_List == null || Card_List.Count == 0)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                List<GetMyCardResponse> List_Response = new List<GetMyCardResponse>();
                foreach (var item in Card_List)
                {
                    GetMyCardResponse response = new GetMyCardResponse(item, List_Type, request.Host);
                    List_Response.Add(response);
                }
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.ListData = List_Response;
            }
            return result;
        }

        /// <summary>
        /// 设置活跃卡片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson SetActiveCard(SetActiveCardRequest request)
        {
            Token token = new Token(request.UserToken);
            ResultJson result = new ResultJson();
            if (Cache_MemberShipCard.Instance.UpdateCard(token.Payload.UserID, request.CardID))
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获得我的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetMyCouponResponse>> GetMyCoupon(TokenAndPageRequest request)
        {
            int PageSize = 5;
            Token token = new Token(request.UserToken);
            ResultJsonModel<List<GetMyCouponResponse>> result = new ResultJsonModel<List<GetMyCouponResponse>>();
            var Coupon_List = Cache_Coupon.Instance.SelectPageByUserId(token.Payload.UserID, (request.PageNo - 1) * PageSize, PageSize);
            List<GetMyCouponResponse> ListResponse = new List<GetMyCouponResponse>();
            if (Coupon_List.Count != 0)
            {
                foreach (var item in Coupon_List)
                {
                    var Response = new GetMyCouponResponse(item);
                    ListResponse.Add(Response);
                }
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = ListResponse;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获得我的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetMyCouponResponse>> GetMyCouponAll(UserTokenRequest request)
        {
            Token token = new Token(request.UserToken);
            ResultJsonModel<List<GetMyCouponResponse>> result = new ResultJsonModel<List<GetMyCouponResponse>>();
            var Coupon_List = Cache_Coupon.Instance.SelectByUserId(token.Payload.UserID);
            List<GetMyCouponResponse> ListResponse = new List<GetMyCouponResponse>();
            if (Coupon_List.Count != 0)
            {
                foreach (var item in Coupon_List)
                {
                    var Response = new GetMyCouponResponse(item);
                    ListResponse.Add(Response);
                }
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = ListResponse;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 用户最后一条消费记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetConsumptionRecordsResponse>> LastConsumptionRecord(UserTokenRequest request)
        {
            Token token = new Token(request.UserToken);
            var result = Cache_Consumption.Instance.SelcctLastConsumptionByUserId(token.Payload.UserID);
            List<GetConsumptionRecordsResponse> ListResponse = new List<GetConsumptionRecordsResponse>();
            ResultJsonModel<List<GetConsumptionRecordsResponse>> resultjson = new ResultJsonModel<List<GetConsumptionRecordsResponse>>();
            if (result != null)
            {
                GetConsumptionRecordsResponse response = new GetConsumptionRecordsResponse(result.Item1, result.Item2);
                ListResponse.Add(response);
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
        /// 用户消费记录分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<List<GetConsumptionRecordsResponse>> ConsumptionRecordPage(TokenAndPageRequest request)
        {
            int PageSize = 10;
            Token token = new Token(request.UserToken);
            var result = Cache_Consumption.Instance.SelectConsumptionPageByUserId(token.Payload.UserID, (request.PageNo - 1) * PageSize, PageSize);
            List<GetConsumptionRecordsResponse> ListResponse = new List<GetConsumptionRecordsResponse>();
            ResultJsonModel<List<GetConsumptionRecordsResponse>> resultjson = new ResultJsonModel<List<GetConsumptionRecordsResponse>>();
            if (result != null)
            {
                foreach (var item in result.Item1)
                {
                    var store = result.Item2.Where(p => p.StoreId == item.StoreID).FirstOrDefault();
                    if (store != null)
                    {
                        GetConsumptionRecordsResponse response = new GetConsumptionRecordsResponse(item, store);
                        ListResponse.Add(response);
                    }
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
    }
}