using Common.Enum_My;
using Common.Extend;
using Common.Filter.MvcAjax;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.AjaxRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    /// <summary>
    /// 优惠卷Ajax控制器
    /// </summary>
    public class CouponAjaxController : Controller
    {
        /// <summary>
        /// 获得优惠卷列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Coupon_List(DataTableRequest param)
        {
            var List_Coupon = Cache_Coupon.Instance.SelectByPage(param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            var list_unknow = List_Coupon.Select(p => new { p.CouponName, p.CouponId }).ToList();
            DataTableResponse<Coupon> Parameter_Tuser = new DataTableResponse<Coupon>();
            var All_Coupon_Count = Cache_Coupon.Instance.SelectAllCount();
            var Search_Coupon_Count = Cache_Coupon.Instance.SelectSearchCount(param.SearchKey);
            Parameter_Tuser.draw = param.Draw;
            Parameter_Tuser.recordsTotal = All_Coupon_Count;
            Parameter_Tuser.recordsFiltered = Search_Coupon_Count;
            Parameter_Tuser.data = List_Coupon;
            return Json(Parameter_Tuser.GetObject(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult Insert_Coupon(CouponRequest request)
        {
            Coupon coupon = new Coupon();
            coupon.CouponName = request.CouponName;
            coupon.CouponDescribe = request.CouponDescribe;
            coupon.ExpirationDay = request.ExpirationDay;
            var Insert_Coupon = Cache_Coupon.Instance.InsertCoupon(coupon);
            ResultJsonModel<Coupon> result = new ResultJsonModel<Coupon>();
            if (Insert_Coupon == false)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataInsertSuccessMessage.Enum_GetString();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示已发放优惠券列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult Get_CouponUserRelationInfoList(CouponDataTableRequest param)
        {
            var List_Coupon = Cache_Coupon.Instance.SelectCouponUserRelationInfoList(param.CouponId,param.CreateDate1,param.CreateDate2,param.ReleaseDate1,param.ReleaseDate2, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<CouponUserRelationInfo> Parameter_Card = new DataTableResponse<CouponUserRelationInfo>();
            Parameter_Card.draw = param.Draw;
            Parameter_Card.data = List_Coupon.Item1;
            Parameter_Card.recordsTotal = List_Coupon.Item2;
            Parameter_Card.recordsFiltered = List_Coupon.Item3;
            return Json(Parameter_Card.GetObject(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="request">Id</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_CouponByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var List_CouponIds = request.KeyIds.ConvertToList();
            var result = Cache_Coupon.Instance.DeleteCouponByIds(List_CouponIds);
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
        /// <summary>
        /// 根据优惠券Id获得优惠券信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_CouponInfo_ById(CouponIdRequest request)
        {
            var coupon = Cache_Coupon.Instance.SelectCouponInfoById(request.CouponId);
            ResultJsonModel<Coupon> result = new ResultJsonModel<Coupon>();
            if (coupon == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = coupon;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据Id修改优惠券信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Update_CouponById(CouponRequest request)
        {
            Coupon Update_Coupon = new Coupon();
            Update_Coupon.CouponId = request.CouponId;
            Update_Coupon.CouponName = request.CouponName;
            Update_Coupon.CouponDescribe = request.CouponDescribe;
            Update_Coupon.ExpirationDay = request.ExpirationDay;
            var Update_flag = Cache_Coupon.Instance.Update_CouponById(Update_Coupon);
            ResultJsonModel<Coupon> result = new ResultJsonModel<Coupon>();
            if (!Update_flag)
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
        /// 发放优惠券给用户
        /// </summary>
        /// <returns></returns>
        public JsonResult GrantCouponToUser(int CouponId,DeleteByIdsRequest request)
        {
            var List_UserId = request.KeyIds.ConvertToList();
            var insert_CouponToUser = Cache_CouponUserRelation.Instance.Insert_GrantCouponToUser(CouponId, List_UserId);
            ResultJsonModel<CouponUserRelation> result = new ResultJsonModel<CouponUserRelation>();
            if (insert_CouponToUser == "false")
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
            }
            else if(insert_CouponToUser == "true")
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataInsertSuccessMessage.Enum_GetString();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据Id多选删除用户优惠券关系表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult Delete_CouponUserRelationByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var List_CouponUserRelationId = request.KeyIds.ConvertToList();
            var result = Cache_CouponUserRelation.Instance.Delete_CouponUserRelationByIds(List_CouponUserRelationId);
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