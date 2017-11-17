using Common.Enum_My;
using Common.Filter.MvcAjax;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Request.AjaxRequest.DataTable;
using GZRYVillageWeb.Request.AjaxRequest.MembershipLevel;
using GZRYVillageWeb.Request.MvcRequest.MemberShipLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    /// <summary>
    /// 会员等级管理Ajax控制器
    /// </summary>
    public class MemberShipLevelAjaxController : Controller
    {
        /// <summary>
        /// 显示会员等级列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        // GET: MemberShipLevelAjax
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_MemberShipLevelList(MemberShipLevelDataTableRequest param)
        {
            var List_MemberShipLevel = Cache_MemberShipLevel.Instance.SelectMemberShipLevelList(param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<MemberShipLevel> Parameter_MemberShipLevel = new DataTableResponse<MemberShipLevel>();
            Parameter_MemberShipLevel.draw = param.Draw;
            Parameter_MemberShipLevel.data = List_MemberShipLevel.Item1;
            Parameter_MemberShipLevel.recordsTotal = List_MemberShipLevel.Item2;
            Parameter_MemberShipLevel.recordsFiltered = List_MemberShipLevel.Item3;
            return Json(Parameter_MemberShipLevel.GetObject(), JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 根据会员等级Id显示对应的优惠内容
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_MemberCouponRelationById(MemberCouponRelationDataTableRequest param)
        {
            var List_MemberCouponRelationById = Cache_MemberCouponRelation.Instance.SelectMemberCouponRelationList(param.MembershipLevelId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<MemberLevelRelation> Parameter_MemberCouponRelation = new DataTableResponse<MemberLevelRelation>();
            Parameter_MemberCouponRelation.draw = param.Draw;
            Parameter_MemberCouponRelation.data = List_MemberCouponRelationById.Item1;
            Parameter_MemberCouponRelation.recordsTotal = List_MemberCouponRelationById.Item2;
            Parameter_MemberCouponRelation.recordsFiltered = List_MemberCouponRelationById.Item3;
            return Json(Parameter_MemberCouponRelation.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获得等级信息根据Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_MembershipLevel_ById(MemberShipLevelRequest request)
        {
            var level = Cache_MemberShipLevel.Instance.GetMemberShipLevelInfo(request.MembershipLevelId);
            ResultJsonModel<MemberShipLevel> result = new ResultJsonModel<MemberShipLevel>();
            if (level == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = level;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新会员等级信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Update_MembershipLevel(MemberLevelRequest request)
        {
            MemberShipLevel level = new MemberShipLevel();
            level.MembershipLevelId = request.MembershipLevelId;
            level.LevelName = request.LevelName;
            level.LevelMax = request.LevelMax;
            level.IsDelete = request.IsDelete;
            var update = Cache_MemberShipLevel.Instance.UpdateMemLevelScore(level);
            ResultJsonModel<MemberShipLevel> result = new ResultJsonModel<MemberShipLevel>();
            if (update == "false")
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else if (update == "true")
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.DataInsertSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitNameMessage.Enum_GetString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获得等级优惠内容根据Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_MemberCouponRelationInfo(MemberCouponRelationRequest request)
        {
            var MemRelation = Cache_MemberCouponRelation.Instance.GetMemberCouponRelationInfo(request.MembershipLevelId);
            ResultJsonModel<MemberLevelRelation> result = new ResultJsonModel<MemberLevelRelation>();
            if (MemRelation == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = MemRelation;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新等级优惠内容信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Update_MemberCouponRelationInfo(LevelUpdateContainsRequest request)
        {
            request.CouponContains = HtmlCodeHelper.HtmlToString(request.CouponContains);
            MemberLevelRelation MemRelation = new MemberLevelRelation();
            MemRelation.CouponContains = request.CouponContains;
            MemRelation.MembershipLevelId = request.MembershipLevelId;
            var update = Cache_MemberCouponRelation.Instance.update_MemberCouponRelationInfo(MemRelation);
            ResultJsonModel<MemberLevelRelation> result = new ResultJsonModel<MemberLevelRelation>();
            if (!update)
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
        /// 根据Id删除对应的优惠信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_MemberCouponRelationById(MemberCouponRelationRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var result = Cache_MemberCouponRelation.Instance.delete_MemberCouponRelationById(request.CouponContainsId);
            if (result)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "对应的优惠信息成功删除";
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "由于未知原因，优惠信息删除失败";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加优惠信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_MemberCouponRelation(LevelAddContainsRequest request)
        {
            MemberLevelRelation MemRelation = new MemberLevelRelation();
            MemRelation.CouponContains = request.CouponContains;
            MemRelation.MembershipLevelId = request.MembershipLevelId;
            var InsertFlag = Cache_MemberCouponRelation.Instance.Insert_MemberCouponRelation(MemRelation);
            ResultJsonModel<MemberLevelRelation> result = new ResultJsonModel<MemberLevelRelation>();
            if (!InsertFlag)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}