using Common.Enum_My;
using Common.Filter.MvcAjax;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
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
    /// 电子储值卡控制器
    /// </summary>
    public class ElectronicCardAjaxController : Controller
    {
        // GET: ElectronicCardAjax
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 显示电子储值卡列表
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
            DataTableResponse<ElectronicCardInfo> Parameter_Card = new DataTableResponse<ElectronicCardInfo>();
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
            var List_Consumption = Cache_Consumption.Instance.SelectConsumptionList(param.ElectronicId, param.SearchKey, param.OrderBy, param.Start, param.Length, param.OrderDir);
            //List<ConsumptionResponse> List_Response = new List<ConsumptionResponse>();
            //foreach (var item in List_Consumption.Item1)
            //{
            //    ConsumptionResponse response = new ConsumptionResponse(item);
            //    List_Response.Add(response);
            //}
            //DataTableResponse<ConsumptionResponse> Parameter_Consumption = new DataTableResponse<ConsumptionResponse>();
           DataTableResponse<ConsumptionInfo> Parameter_Consumption = new DataTableResponse<ConsumptionInfo>();
            Parameter_Consumption.draw = param.Draw;
            //Parameter_Consumption.data = List_Response;
            Parameter_Consumption.data = List_Consumption.Item1;
            Parameter_Consumption.recordsTotal = List_Consumption.Item2;
            Parameter_Consumption.recordsFiltered = List_Consumption.Item3;
            return Json(Parameter_Consumption.GetObject(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增电子储值卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_ElectronicType(ElectronicTypeRequest request)
        {
            ElectronicType ElcCardtype = new ElectronicType();
            ElcCardtype.CardImage = request.CardImage;
            ElcCardtype.CardMoney = request.CardMoney;
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
    }
}