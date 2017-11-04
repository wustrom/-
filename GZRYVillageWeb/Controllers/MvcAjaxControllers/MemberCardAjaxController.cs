using Common.Enum_My;
using Common.Filter.MvcAjax;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using DbOpertion.Operation;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Request.AjaxRequest.Card;
using GZRYVillageWeb.Request.AjaxRequest.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    public class MemberCardAjaxController : Controller
    {
        /// <summary>
        /// 添加新类型卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Add_MemberCard(AddCardRequest request)
        {
            MemberShipCard card = new MemberShipCard();
            card.CardName = request.MemberShipCardName;
            card.CardPassword = request.CardPassword;
            card.MemberShipTypeId = request.MemberShipTypeId;
            card.ReleaseDate = request.ReleaseDate;
            card.UserId = request.UserId; ;
            card.IsDelete = request.IsDelete;
            card.IsUser = request.IsUser;
            var insertCard = MemberShipCardOper.Instance.Insert(card);
            ResultJsonModel<MemberShipCard> result = new ResultJsonModel<MemberShipCard>();
            if (!insertCard)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataExitMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }

            return Json(result,JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// 根据所输入的卡片ID查找所对应的优惠券信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Coupon_List(CardDataTableRequest param)
        {
            var CouponCardRelation_List = Cache_CouponCardRelation.Instance.SelectCouponList(param.MemberShipTypeId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<Coupon> Parameter_CouponCardRelation = new DataTableResponse<Coupon>();
            Parameter_CouponCardRelation.draw = param.Draw;
            Parameter_CouponCardRelation.data = CouponCardRelation_List.Item1;
            Parameter_CouponCardRelation.recordsTotal = CouponCardRelation_List.Item2;
            Parameter_CouponCardRelation.recordsFiltered = CouponCardRelation_List.Item3;
            return Json(Parameter_CouponCardRelation.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 显示剩余的全部优惠券
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Coupon_AllList(CardDataTableRequest param)
        {
            var CouponCardRelation_List = Cache_CouponCardRelation.Instance.SelectAllCouponList(param.MemberShipTypeId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<Coupon> Parameter_CouponCardRelation = new DataTableResponse<Coupon>();
            Parameter_CouponCardRelation.draw = param.Draw;
            Parameter_CouponCardRelation.data = CouponCardRelation_List.Item1;
            Parameter_CouponCardRelation.recordsTotal = CouponCardRelation_List.Item2;
            Parameter_CouponCardRelation.recordsFiltered = CouponCardRelation_List.Item3;
            return Json(Parameter_CouponCardRelation.GetObject(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示卡片类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_CardType_List(DataTableRequest param)
        {
            var List_card = Cache_MemberShipType.Instance.SelectMemberTypeCard(param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<MemberShipType> Parameter_Card = new DataTableResponse<MemberShipType>();
            var All_Card_Count = Cache_MemberShipType.Instance.SelectMemberCardTypeCount(null,param.OrderBy,param.OrderDir);
            var Search_Card_Count = Cache_MemberShipType.Instance.SelectMemberCardTypeCount(param.SearchKey,param.OrderBy,param.OrderDir);
            Parameter_Card.draw = param.Draw;
            Parameter_Card.data = List_card;
            Parameter_Card.recordsTotal = All_Card_Count;
            Parameter_Card.recordsFiltered = Search_Card_Count;
            return Json(Parameter_Card.GetObject(),JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据类型ID显示对应的会员卡
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Card_List(CardDataTableRequest param)
        {
           var List_card = Cache_MemberShipCard.Instance.SelectMemberCardList(param.MemberShipTypeId,param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<MemberShipCard> Parameter_Card = new DataTableResponse<MemberShipCard>();
            Parameter_Card.draw = param.Draw;
            Parameter_Card.data = List_card.Item1;
            Parameter_Card.recordsTotal = List_card.Item2;
            Parameter_Card.recordsFiltered = List_card.Item3;
            return Json(Parameter_Card.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据优惠券ID删除对应的优惠券
        /// </summary>
        /// <param name="MemberShipTypeId">类型ID</param>
        /// <param name="CouponId">优惠券ID</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_CouponById(int MemberShipTypeId, int CouponId)
        {
            ResultJson jsonresult = new ResultJson();
            var result= Cache_CouponCardRelation.Instance.Delete_CouponById(MemberShipTypeId, CouponId);
            if (result)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "优惠券成功删除";
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "由于一些未知原因，删除失败";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="MemberShipTypeId">类型ID</param>
        /// <param name="CouponId">优惠券ID</param>
        /// <returns></returns>
        
        public JsonResult Insert_CouponById(int MemberShipTypeId, int CouponId)
        {
            ResultJson jsonresult = new ResultJson();
            var result = Cache_CouponCardRelation.Instance.Insert_CouponById(MemberShipTypeId, CouponId);
            if (result)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "优惠券添加成功";
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "由于一些未知原因，添加失败";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
            
        }

        

    }
}
