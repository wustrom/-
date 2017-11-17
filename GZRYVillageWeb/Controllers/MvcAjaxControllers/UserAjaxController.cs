using Common.Filter.MvcAjax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbOpertion.Cache;
using Common.Helper.JsonHelper;
using DbOpertion.Models;
using Common.Result;
using Common.Enum_My;
using System.Diagnostics;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Response.AjaxResponse;
using Common.Helper;
using Common.Extend;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    /// <summary>
    /// 用户异步控制器
    /// </summary>
    public class UserAjaxController : Controller
    {
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_User_List(DataTableRequest param)
        {
            var List_user = Cache_TUser.Instance.SelectMemberUser(param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            List<TUserResponse> List_Response = new List<TUserResponse>();
            foreach (var item in List_user)
            {
                TUserResponse response = new TUserResponse(item);
                List_Response.Add(response);
            }
            DataTableResponse<TUserResponse> Parameter_Tuser = new DataTableResponse<TUserResponse>();
            var All_User_Count = Cache_TUser.Instance.SelectMemberUserCount(null, param.OrderBy, param.OrderDir);
            var Search_User_Count = Cache_TUser.Instance.SelectMemberUserCount(param.SearchKey, param.OrderBy, param.OrderDir);
            Parameter_Tuser.draw = param.Draw;
            Parameter_Tuser.recordsTotal = All_User_Count;
            Parameter_Tuser.recordsFiltered = Search_User_Count;
            Parameter_Tuser.data = List_Response;
            return Json(Parameter_Tuser.GetObject(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获得用户信息根据ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_User_ById(UserIdRequest request)
        {
            var user = Cache_TUser.Instance.MemberGetUserInfo(request.UserId);
            ResultJsonModel<TUser> result = new ResultJsonModel<TUser>();
            if (user == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = user;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获得购物记录列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Consumption_List(UserDataTableRequest param)
        {
            var Consuption_List = Cache_PayRecord.Instance.Consuption_List(param.UserId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<PayRecord> Parameter_Consumption = new DataTableResponse<PayRecord>();
            var All_Consuption_Count = Cache_PayRecord.Instance.SelectConsuptionCount(param.UserId, null, param.OrderBy, param.OrderDir);
            var Search_Consuption_Count = Cache_PayRecord.Instance.SelectConsuptionCount(param.UserId, param.SearchKey, param.OrderBy, param.OrderDir);
            Parameter_Consumption.draw = param.Draw;
            Parameter_Consumption.recordsTotal = All_Consuption_Count;
            Parameter_Consumption.recordsFiltered = Search_Consuption_Count;
            Parameter_Consumption.data = Consuption_List;
            return Json(Parameter_Consumption.GetObject(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获得优惠卷列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Coupon_List(UserDataTableRequest param)
        {
            var CouponUserRelation_List = Cache_CouponUserRelation.Instance.SelectByPage(param.UserId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<CouponUserRelation> Parameter_CouponUserRelation = new DataTableResponse<CouponUserRelation>();
            var All_CouponUserRelation_Count = Cache_CouponUserRelation.Instance.SelectSearchCount(param.UserId, null, param.OrderBy, param.OrderDir);
            var Search_CouponUserRelation_Count = Cache_CouponUserRelation.Instance.SelectSearchCount(param.UserId, param.SearchKey, param.OrderBy, param.OrderDir);
            Parameter_CouponUserRelation.draw = param.Draw;
            Parameter_CouponUserRelation.recordsTotal = All_CouponUserRelation_Count;
            Parameter_CouponUserRelation.recordsFiltered = Search_CouponUserRelation_Count;
            Parameter_CouponUserRelation.data = CouponUserRelation_List;
            return Json(Parameter_CouponUserRelation.GetObject(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Modify_User(UserReqest request)
        {
            TUser UserInfo = new TUser();
            UserInfo.UserId = request.UserId;
            UserInfo.UserName = request.UserName;
            UserInfo.UserNickName = request.UserNickName;
            UserInfo.UserPhone = request.UserPhone;
            UserInfo.UserEmail = request.UserEmail;
            UserInfo.Sex = request.Sex;
            UserInfo.ConsumptionTime = request.ConsumptionTime;
            var UpdateFlag = Cache_TUser.Instance.UpdateUserInfo(UserInfo);
            ResultJsonModel<TUser> result = new ResultJsonModel<TUser>();
            if (!UpdateFlag)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataInsertSuccessMessage.Enum_GetString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_UserInfo(UserReqest request)
        {
            TUser user = new TUser();
            user.UserName = request.UserName;
            user.UserNickName = request.UserNickName;
            user.UserPhone = request.UserPhone;
            user.UserEmail = request.UserEmail;
            string PassWord = "123456";
            string PassWordMd5 = MD5Helper.StrToMD5WithKey(PassWord);
            user.UserPassword = PassWordMd5;
            user.ConsumptionTime = request.ConsumptionTime;
            user.Sex = request.Sex;
            user.CreateTime = DateTime.Now;
            var InsertFlag = Cache_TUser.Instance.InsertUserInfo(user);
            ResultJsonModel<TUser> result = new ResultJsonModel<TUser>();
            if (InsertFlag == "false")
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
            }
            else if (InsertFlag == "true")
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataInsertSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitPhoneOrNameMessage.Enum_GetString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="request">用户信息表</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_UserByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var List_UserId = request.KeyIds.ConvertToList();
            var result = Cache_TUser.Instance.DeleteUserByIds(List_UserId);
            if (result)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "所选数据已成功删除";
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "由于一些未知原因，删除失败";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);

        }
    }
}