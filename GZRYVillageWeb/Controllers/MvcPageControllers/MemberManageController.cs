using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.LambdaOpertion;
using Common;
using System.IO;
using System.Data;
using DbOpertion.Cache;
using Common.Result;
using GZRYVillageWeb.Request.MvcRequest.MemberCard;
using Common.Mvc.Filter;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{
    
    public class MemberManageController : Controller
    {
        // DbOpertion.Linq.TestDBLinqDataContext db = new DbOpertion.Linq.TestDBLinqDataContext();
        // GET: MemberManage
        /// <summary>
        /// 会员卡管理主页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult Index()
        {
              //调用对应的方法绑定下拉列表框
             //ViewBag:后台把数据传输到前台进行绑定
           // ViewBag.memberType = new SelectList(s.DDLSelectCoupon(), "CouponId", "CouponName");
            return View();
        }

        /// <summary>
        /// 卡片所对应的优惠券页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult Coupon(MemberShipCardIdRequest request)
        {
            ViewBag.MemberShipTypeId = request.MemberShipTypeId;
            return View();
        }
        ///// <summary>
        ///// 上传图片
        ///// </summary>
        ///// <param name="card"></param>
        ///// <returns></returns>
        //public ActionResult UploadImage(DbOpertion.Linq.MemberShipCard card)
        //{
        //    DAL.MemberCardDAL mem = new MemberCardDAL();

        //    string fileName = "";
        //    if (Request.Files["files"] != null)//判断是否能接收到上传的图片
        //    {
        //        var imgurl = Request.Files["files"];//得到上传图片
        //        string fileEx = Path.GetExtension(imgurl.FileName);
        //        if (fileEx == ".jpg" || fileEx == ".jpeg")
        //        { 
        //            //这储存这个类型 
        //            string imagePath = "/Upload/Images/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
        //            if (!Directory.Exists(imagePath))
        //            {    
        //                //判断是否有这个文件夹没有创建一个
        //                Directory.CreateDirectory(Server.MapPath(imagePath));
        //            }
        //              //写好保存路径存放
        //             fileName = imagePath + Guid.NewGuid().ToString() + imgurl.FileName;
        //             imgurl.SaveAs(Server.MapPath(fileName));
        //             return Content(fileName);
        //        }
        //        //return Content("只支持保存.jpg或者.jpeg格式的图片");
        //        card.CardName = Request.QueryString["CardName"];//卡片名称
        //        card.TypeImage = fileName;//卡片图案
        //        if (fileName != null && fileName!= "")
        //        {
        //            string CouponName = Request.QueryString["CouponName"];
        //            DataTable dt = mem.GetUserIDandIsDelete(CouponName);
        //            string IsUser = dt.Rows[0]["IsUsed"].ToString();//是否对应的有用户
        //            if (IsUser != null && IsUser != "")
        //            {
        //                card.IsUser = true;
        //            }
        //            else
        //            {
        //                card.IsUser = false;
        //            }
        //            card.IsDelete = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);//是否已删除
        //            db.MemberShipCard.InsertOnSubmit(card);
        //            db.SubmitChanges();
        //        }
        //        return Content("OK");



        //    }
        //    else
        //    {
        //        return Content("保存失败");
        //    }
        //}

        /// <summary>
        /// 类型对应的会员卡页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult MemberView(MemberShipCardIdRequest request)
        {
            ViewBag.MemberShipTypeId = request.MemberShipTypeId;
            return View();
        }


        /// <summary>
        /// 类型对应的会员卡页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult MemberView123(MemberShipCardIdRequest request)
        {
            ViewBag.MemberShipTypeId = request.MemberShipTypeId;
            return View();
        }


        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {
            string fileName = System.IO.Path.GetFileName(upImg.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/" + fileName);
            string pic = "", error = "";
            try
            {
                upImg.SaveAs(filePhysicalPath);
                pic = "/upload/" + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new
            {
                pic = pic,
                error = error
            });
        }
    }
}