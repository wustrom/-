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
        public ResultJson<UserLevelResponse> GetMyLevelInfo(UserTokenRequest request)
        {
            Token token = new Token(request.UserToken);
            ResultJson<UserLevelResponse> result = new ResultJson<UserLevelResponse>();
            var Level_Result = Cache_MemberShipLevel.Instance.SelectUserLevel(token.Payload.UserID);
            if (Level_Result != null)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.ListData.Add(new UserLevelResponse(Level_Result.Item1, Level_Result.Item2, Level_Result.Item3, Level_Result.Item4));
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
        public ResultJson<GetLevelListResponse> GetLevelListInfo()
        {
            ResultJson<GetLevelListResponse> result = new ResultJson<GetLevelListResponse>();
            var Level_Result = Cache_MemberShipLevel.Instance.SelectLevelListInfo();
            if (Level_Result != null)
            {
                foreach (var item in Level_Result.Item1)
                {
                    result.ListData.Add(new GetLevelListResponse(item, Level_Result.Item2));
                }
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
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
                    GetMyCardResponse response = new GetMyCardResponse(item, List_Type, Url.Request.Headers.Host);
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

        ///// <summary>
        ///// 获得用户二维码
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public ResultJson GetUserQRCode(UserTokenRequest request)
        //{
        //    Token token = new Token(request.UserToken);
        //    ResultJson result = new ResultJson();

        //    return null;
        //}
    }
}