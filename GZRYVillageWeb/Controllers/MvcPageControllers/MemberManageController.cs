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
using Common.Extend;
using Common.Filter.WebApi;
using Common.Helper;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{

    public class MemberManageController : Controller
    {
        /// <summary>
        /// 会员卡管理主页面
        /// </summary>
        /// <returns></returns>
        [UserLogin]
        public ActionResult Index()
        {
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

        /// <summary>
        /// 类型对应的会员卡页面
        /// </summary>
        /// <param name="upImg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {
            var fileExten = Path.GetExtension(upImg.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/MemberManage");
            string pic = "", error = "";
            if (Directory.Exists(filePhysicalPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(filePhysicalPath);
            }
            if (fileExten == ".jpg" || fileExten == ".jpeg" || fileExten == ".png")
            {
                var fileName = RandHelper.Instance.Str(6) + DateTime.Now.Ticks + fileExten;
                try
                {
                    upImg.SaveAs(filePhysicalPath + "/" + fileName);
                    pic = "/upload/MemberManage/" + fileName;
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
            else
            {
                return Json(new
                {
                    pic = pic,
                    error = "上传图片格式不正确"
                });
            }
        }

        public class Card
        {
            /// <summary>
            /// 卡片名称
            /// </summary>
            public string 卡片名称 { get; set; }

            /// <summary>
            /// 卡片密码
            /// </summary>
            public string 卡片密码 { get; set; }
        }

        /// <summary>
        /// 类型对应的会员卡页面
        /// </summary>
        /// <param name="upImg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase upImg)
        {
            var fileExten = Path.GetExtension(upImg.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/MemberManage");
            string pic = "", error = "";
            if (Directory.Exists(filePhysicalPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(filePhysicalPath);
            }
            if (fileExten == ".xlsx" || fileExten == ".xls")
            {
                var DataTable = ExcelHelper.ExcelToDataTable(upImg);
                var cardList = DataTable.ConvertToList<Card>();
                var fileName = RandHelper.Instance.Str(6) + DateTime.Now.Ticks + fileExten;
                try
                {
                    upImg.SaveAs(filePhysicalPath + "/" + fileName);
                    pic = "/upload/MemberManage/" + fileName;
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
            else
            {
                return Json(new
                {
                    pic = pic,
                    error = "上传图片格式不正确"
                });
            }
        }

    }
}