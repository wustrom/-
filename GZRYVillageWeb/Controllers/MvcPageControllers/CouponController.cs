using Common.Mvc.Filter;
using GZRYVillageWeb.Request.MvcRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{
    /// <summary>
    /// 优惠卷控制器
    /// </summary>
    public class CouponController : Controller
    {
        /// <summary>
        /// 优惠券页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 已发放优惠券管理页面
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [UserLogin]
        public ActionResult Coupon(CouponCardRequest request)
        {
            ViewBag.CouponId = request.CouponId;
            return View();
        }
        /// <summary>
        /// 发放优惠券页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult GrantCoupon(CouponCardRequest request)
        {
            ViewBag.CouponId = request.CouponId;
            return View();
        }
    }
}