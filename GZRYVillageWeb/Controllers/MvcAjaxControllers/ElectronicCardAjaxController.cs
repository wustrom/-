using Common.Enum_My;
using Common.Extend;
using Common.Filter.MvcAjax;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Request.AjaxRequest.DataTable;
using GZRYVillageWeb.Request.AjaxRequest.ElectronicType;
using GZRYVillageWeb.Response.AjaxResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    /// <summary>
    /// 电子储值卡Ajax控制器
    /// </summary>
    public class ElectronicCardAjaxController : Controller
    {
        /// <summary>
        /// 显示电子储值卡类型列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_ElectronicType_List(DataTableRequest param)
        {
            var List_ElectronicType = Cache_ElectronicType.Instance.SelectElectronicTypeCard(param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<ElectronicType> Parameter_Card = new DataTableResponse<ElectronicType>();
            var All_Card_Count = Cache_ElectronicType.Instance.SelectElectronicTypeCardCount(null, param.OrderBy, param.OrderDir);
            var Search_Card_Count = Cache_ElectronicType.Instance.SelectElectronicTypeCardCount(param.SearchKey, param.OrderBy, param.OrderDir);
            Parameter_Card.draw = param.Draw;
            Parameter_Card.data = List_ElectronicType;
            Parameter_Card.recordsTotal = All_Card_Count;
            Parameter_Card.recordsFiltered = Search_Card_Count;
            return Json(Parameter_Card.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据电子储值卡类型ID显示对应的卡片信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Card_List(ElectronicCardDataTableRequest param)
        {
            var List_card = Cache_ElectronicCard.Instance.SelectElectronicCardList(param.ElectronicTypeId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<ElectronicCard> Parameter_Card = new DataTableResponse<ElectronicCard>();
            Parameter_Card.draw = param.Draw;
            Parameter_Card.data = List_card.Item1;
            Parameter_Card.recordsTotal = List_card.Item2;
            Parameter_Card.recordsFiltered = List_card.Item3;
            return Json(Parameter_Card.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据电子储值卡Id查找对应用户的消费记录列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Consumption_List(ConsumptionDataTableRequest param)
        {
            var List_Consumption = Cache_PayRecord.Instance.SelectConsumptionList(param.ElectronicId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            DataTableResponse<PayRecordInfo> Parameter_Consumption = new DataTableResponse<PayRecordInfo>();
            Parameter_Consumption.draw = param.Draw;
            Parameter_Consumption.data = List_Consumption.Item1;
            Parameter_Consumption.recordsTotal = List_Consumption.Item2;
            Parameter_Consumption.recordsFiltered = List_Consumption.Item3;
            return Json(Parameter_Consumption.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增电子储值类型卡
        /// </summary>
        /// <param name="request">电子储值类型卡信息</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_ElectronicType(ElectronicTypeRequest request)
        {
            ElectronicType ElcCardtype = new ElectronicType();
            ElcCardtype.CardTypeName = request.CardTypeName;
            ElcCardtype.CardImage = request.CardImage;
            ElcCardtype.CardMoney = request.CardMoney;
            ElcCardtype.CardExpirationMonth = request.CardExpirationMonth;
            var InsertFlag = Cache_ElectronicType.Instance.Insert_ElectronicType(ElcCardtype);
            ResultJsonModel<ElectronicType> result = new ResultJsonModel<ElectronicType>();
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
        /// <summary>
        /// 添加电子储值卡
        /// </summary>
        /// <param name="request">电子储值卡信息</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_ElectronicCard(ElectronicCardRequest request)
        {
            ElectronicCard card = new ElectronicCard();
            var TypeCardMoney = Cache_ElectronicType.Instance.GetCardMoneyById(request.ElectronicTypeId);
            card.CaerMoney = TypeCardMoney.CardMoney;
            card.CardName = request.CardName;
            string PassWord = MD5Helper.StrToMD5WithKey(request.CardPassWord);
            card.CardPassword = PassWord;
            card.ElectronicTypeId = request.ElectronicTypeId;
            var Insert_Card = Cache_ElectronicCard.Instance.Insert_ElectronicCard(card);
            ResultJsonModel<ElectronicCard> result = new ResultJsonModel<ElectronicCard>();
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
        /// 根据卡片类型Id和卡片Id查询对应的绑定人
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_UserNickName(ElectronicCardUserRequest request)
        {

            var card = Cache_ElectronicCard.Instance.GetByUserNickName(request.ElectronicTypeId,request.ElectronicId);
            ResultJson jsonresult = new ResultJson();
            if (card != null)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "该会员卡目前绑定人："+card.FirstOrDefault().UserNickName;
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "该会员卡目前没有绑定人";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据Id删除电子储值卡
        /// </summary>
        /// <param name="request">电子储值卡信息</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_ElectronicCardById(ElectronicCardUserRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var result = Cache_ElectronicCard.Instance.DeleteElectronicCardById(request.ElectronicId);
            if (result)
            {
                jsonresult.HttpCode = 200;
                jsonresult.Message = "电子储值卡成功删除";
            }
            else
            {
                jsonresult.HttpCode = 300;
                jsonresult.Message = "由于一些未知原因，删除失败";
            }
            return Json(jsonresult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="request">id信息</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_ElectronicTypeCardByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var List_ElectronicTypeIds = request.KeyIds.ConvertToList();
            var result = Cache_ElectronicType.Instance.DeleteElectronicTypeCardByIds(List_ElectronicTypeIds);
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
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="request">id信息</param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Delete_ElectronicCardByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var List_ElectronicCardIds = request.KeyIds.ConvertToList();
            var result = Cache_ElectronicCard.Instance.Delete_ElectronicCardByIds(List_ElectronicCardIds);
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