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
    /// 首页控制器
    /// </summary>
    public class HomePageController : ApiController
    {
        /// <summary>
        /// 获得第一页
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<GetMyCardResponse, UserLevelResponse, List<GetElecCardListResponse>, List<GetMyCouponResponse>, string, GetMessageListResponse> GetFirstPage(UserTokenRequest request)
        {
            ResultJsonModel<GetMyCardResponse, UserLevelResponse, List<GetElecCardListResponse>, List<GetMyCouponResponse>, string, GetMessageListResponse> result = new ResultJsonModel<GetMyCardResponse, UserLevelResponse, List<GetElecCardListResponse>, List<GetMyCouponResponse>, string, GetMessageListResponse>();
            //用户等级
            MyClubController myclub = new MyClubController();
            var LevelInfo = myclub.GetCurrentLevelInfo(request);
            if (LevelInfo.HttpCode == 200)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model2 = LevelInfo.Model1;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }

            //获得活跃卡
            GetMyCardListRequest CardRequest = new GetMyCardListRequest { Active = true, UserToken = request.UserToken, Host = Url.Request.Headers.Host };
            var ActiveCard = myclub.GetMyCardList(CardRequest);
            if (ActiveCard.HttpCode == 200)
            {
                result.Model1 = ActiveCard.ListData.FirstOrDefault();
            }

            //储值卡列表
            StoredCardController storedCard = new StoredCardController();
            TokenAndHostRequest tokenHostRequest = new TokenAndHostRequest { Host = Url.Request.Headers.Host, UserToken = request.UserToken };
            var ElecCardList = storedCard.GetElecCardList(tokenHostRequest);
            if (ElecCardList.HttpCode == 200)
            {
                result.Model3 = ElecCardList.Model1;
            }

            //优惠券
            var coupon = myclub.GetMyCouponAll(request);
            if (coupon.HttpCode == 200)
            {
                result.Model4 = coupon.Model1;
            }

            //消息列表
            MessageController message = new MessageController();
            HostRequest hostRequest = new HostRequest { Host = Url.Request.Headers.Host };
            var MessageList = message.GetMessageList(hostRequest);
            if (MessageList.HttpCode == 200)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model6 = MessageList.Model1.FirstOrDefault();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }

            return result;
        }

        /// <summary>
        /// 获得第一页
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<string, GetMessageListResponse> GetFirstPageNoLogin()
        {
            ResultJsonModel<string, GetMessageListResponse> result = new ResultJsonModel<string, GetMessageListResponse>();
            //消息列表
            MessageController message = new MessageController();
            HostRequest hostRequest = new HostRequest { Host = Url.Request.Headers.Host };
            var MessageList = message.GetMessageList(hostRequest);
            if (MessageList.HttpCode == 200)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model2 = MessageList.Model1.FirstOrDefault();
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