using GZRYVillageWeb.Request.MvcRequest.MemberShipLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{
    /// <summary>
    /// 会员等级控制器
    /// </summary>
    public class MemberShipLevelController : Controller
    {
        // GET: MemberShipLevel
        /// <summary>
        /// 会员等级页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LevelIndex()
        {
            return View();
        }
        /// <summary>
        ///等级优惠内容页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LevelCouponPage(MemberShipLevelRequest request)
        {
            ViewBag.MembershipLevelId = request.MembershipLevelId;
            return View();
        }
    }
}