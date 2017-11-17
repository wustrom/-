using Common.Enum_My;
using Common.Extend;
using Common.Filter.MvcAjax;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Request.AjaxRequest.DataTable;
using GZRYVillageWeb.Request.MvcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    public class MessageAjaxController : Controller
    {
        /// <summary>
        /// 显示消息会员列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Get_Message_List(MessageDataTableRequest param)
        {
            var List_Message = Cache_Message.Instance.SelectMessageList(param.SearchKey,param.OrderBy,param.Start,param.Length,param.OrderDir);
            DataTableResponse<Message> Parameter_Message = new DataTableResponse<Message>();
            Parameter_Message.draw = param.Draw;
            Parameter_Message.data = List_Message.Item1;
            Parameter_Message.recordsTotal = List_Message.Item2;
            Parameter_Message.recordsFiltered = List_Message.Item3;
            return Json(Parameter_Message.GetObject(),JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Insert_Message(MessageRequest request)
        {
            Message message = new Message();
            message.MessageName = request.MessageName;
            //message.MessageContains =request.MessageContains;
            message.MessageDescribe =request.MessageDescribe;
            message.MessageImage = request.MessageImage;   
            var InsertFlag = Cache_Message.Instance.InsertMessage(message);
            ResultJsonModel<Message> result = new ResultJsonModel<Message>();
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
        /// 根据Id获得消息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult GetMessageInfoById(MessageIdRequest request)
        {
            var message = Cache_Message.Instance.GetMessageInfoById(request.MessageID);
            ResultJsonModel<Message> result = new ResultJsonModel<Message>();
            if (message == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.NoMoreDataMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 更新消息内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAjaxModelValidate]
        [MvcAjaxException]
        public JsonResult Update_MessageContains(UpdateMessageContainsRequest request)
        {
            request.MessageContains = HtmlCodeHelper.HtmlToString(request.MessageContains);
            Message message = new Message();
            message.MessageContains = request.MessageContains;
            message.MessageID = request.MessageID;
            var update = Cache_Message.Instance.Update_Message(message);
            ResultJsonModel<Message> result = new ResultJsonModel<Message>();
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
        /// 根据Id多选删除数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult Delete_MessageByIds(DeleteByIdsRequest request)
        {
            ResultJson jsonresult = new ResultJson();
            var List_MessageId = request.KeyIds.ConvertToList();
            var result = Cache_Message.Instance.DeleteMessageByIds(List_MessageId);
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