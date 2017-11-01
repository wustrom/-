using Common.Mvc.Filter;
using GZRYVillageWeb.Request.MvcRequest.ElectronicCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{
    public class ElectronicCardController : Controller
    {

        /// <summary>
        /// 电子储值卡页面
        /// </summary>
        /// <returns></returns>
        // GET: ElectronicCard
        [UserLogin]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根据电子储值卡类型显示对应的卡片
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult ElectronicCardView(ElectronicCardType request)
        {
            ViewBag.ElectronicTypeId = request.ElectronicTypeId;
            return View();
        }
        /// <summary>
        /// 根据用户Id显示对应的消费记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [UserLogin]
        public ActionResult Consumption(ElectronicCardType request)
        {
            ViewBag.ElectronicId = request.ElectronicId;
            return View();
        }
    }
}