using Common.Enum_My;
using Common.Extend;
using Common.Filter.MvcAjax;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using DbOpertion.Operation;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Request.AjaxRequest.Card;
using GZRYVillageWeb.Request.AjaxRequest.DataTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    /// <summary>
    /// 会员卡管理Ajax控制器
    /// </summary>
    public class MemberCardAjaxController : Controller
    {
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
            var All_Card_Count = Cache_MemberShipType.Instance.SelectMemberCardTypeCount(null, param.OrderBy, param.OrderDir);
            var Search_Card_Count = Cache_MemberShipType.Instance.SelectMemberCardTypeCount(param.SearchKey, param.OrderBy, param.OrderDir);
            Parameter_Card.draw = param.Draw;
            Parameter_Card.data = List_card;
            Parameter_Card.recordsTotal = All_Card_Count;
            Parameter_Card.recordsFiltered = Search_Card_Count;
            return Json(Parameter_Card.GetObject(), JsonRequestBehavior.AllowGet);
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
            var List_card = Cache_MemberShipCard.Instance.SelectMemberCardList(param.MemberShipTypeId, param.SearchKey, param.ReleaseDate1, param.ReleaseDate2, param.CreateDate1, param.CreateDate2, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<MemberCardByTypeInfo> Parameter_Card = new DataTableResponse<MemberCardByTypeInfo>();
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
            var result = Cache_CouponCardRelation.Instance.Delete_CouponById(MemberShipTypeId, CouponId);
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

        /// <summary>
        /// 添加新类型卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_MemberTypeCard(MemberShipTypeRequest request)
        {
            MemberShipType CardType = new MemberShipType();
            CardType.CardName = request.CardName;
            CardType.CardImage = request.CardImage;
            var Insert_TypeCard = Cache_MemberShipType.Instance.Insert_MemCardType(CardType);
            ResultJsonModel<MemberShipType> result = new ResultJsonModel<MemberShipType>();
            if (Insert_TypeCard == false)
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
        /// 添加会员卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_MemberCard(InsertMemberCard request)
        {
            MemberShipCard card = new MemberShipCard();
            card.CardName = request.CardName;
            card.CreateDate = DateTime.Now;
            string PassWord = MD5Helper.StrToMD5WithKey(request.CardPassword);
            card.CardPassword = PassWord;
            card.MemberShipTypeId = request.MemberShipTypeId;
            var Insert_Card = Cache_MemberShipCard.Instance.Insert_MemberCard(card);
            ResultJsonModel<MemberShipCard> result = new ResultJsonModel<MemberShipCard>();
            if (Insert_Card == false)
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
        /// 根据Id多选删除会员类型卡
        /// </summary>
        /// <param name="request">会员类型卡Id</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_MemberTypeCardByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var ListMembershipTypeId = request.KeyIds.ConvertToList();
            var result = Cache_MemberShipType.Instance.Delete_MemberTypeCard(ListMembershipTypeId);
            if (result)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "会员类型卡成功删除";
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "由于一些未知原因，删除失败";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据Id多选删除会员卡
        /// </summary>
        /// <param name="request">Id信息</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_MemberCardByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var ListMemberCardId = request.KeyIds.ConvertToList();
            var result = Cache_MemberShipCard.Instance.Delete_MemberCard(ListMemberCardId);
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
