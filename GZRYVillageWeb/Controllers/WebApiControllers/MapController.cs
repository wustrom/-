using Common.Enum_My;
using Common.Result;
using DbOpertion.CacheExtendApi;
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
    /// 地图控制器
    /// </summary>
    public class MapController : ApiController
    {
        /// <summary>
        /// 获得商铺列表
        /// </summary>
        public ResultJsonModel<List<GetStoreListResponse>> GetStoreList()
        {
            ResultJsonModel<List<GetStoreListResponse>> result = new ResultJsonModel<List<GetStoreListResponse>>();
            var storeList = Cache_Store.Instance.GetStoreList();
            List<GetStoreListResponse> ListResponse = new List<GetStoreListResponse>();
            foreach (var item in storeList)
            {
                ListResponse.Add(new GetStoreListResponse(item, null, null));
            }
            if (ListResponse.Count != 0)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = ListResponse;
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 获得商铺列表
        /// </summary>
        public ResultJsonModel<GetStoreListResponse> GetFirstStore(CoordRequest request)
        {
            ResultJsonModel<GetStoreListResponse> result = new ResultJsonModel<GetStoreListResponse>();
            var storeList = Cache_Store.Instance.GetStoreList();
            List<GetStoreListResponse> ListResponse = new List<GetStoreListResponse>();
            foreach (var item in storeList)
            {
                ListResponse.Add(new GetStoreListResponse(item, request.Latitude, request.Longitude));
            }
            if (ListResponse.Count != 0)
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = ListResponse.OrderBy(p => int.Parse(p.Distance)).FirstOrDefault();
                if (int.Parse(result.Model1.Distance) > 2000)
                {
                    result.Model1.Distance = "大于2公里";
                }
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