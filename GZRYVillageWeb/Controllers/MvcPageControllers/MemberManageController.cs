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
using Common.Helper;
using Common.Enum_My;
using Common.Extend;
using System.Drawing;
using System.Drawing.Imaging;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{

    public class MemberManageController : Controller
    {
        // GET: MemberManage
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
        /// 上传卡片图案
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
                //给图片重新命名
                var fileName = RandHelper.Instance.Str(6) + DateTime.Now.Ticks + fileExten;
                try
                {
                    var image = Image.FromStream(upImg.InputStream);
                    ImageUploadHelper.Instance.YaSuo(image, filePhysicalPath + "/" + fileName, 30);
                    //upImg.SaveAs(filePhysicalPath + "/" + fileName);
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
        /// 批量导入卡
        /// </summary>
        /// <param name="upExcel"></param>
        /// <param name="MemberShipTypeId">类型Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase upExcel, int MemberShipTypeId)
        {
            var fileExten = Path.GetExtension(upExcel.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/MemberUploadExcel");
            string pic = "";
            if (Directory.Exists(filePhysicalPath) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(filePhysicalPath);
            }
            if (fileExten == ".xlsx" || fileExten == ".xls")
            {
                var DataTable = ExcelHelper.ExcelToDataTable(upExcel);
                var cardList = DataTable.ConvertToList<Card>();
                cardList = cardList.GroupBy(p => p.卡片名称).Select(p => p.First()).ToList();
                List<MemberShipCard> ListMemberCard = new List<MemberShipCard>();
                foreach (var item in cardList)
                {
                    MemberShipCard Card = new MemberShipCard();
                    Card.CardName = item.卡片名称;
                    string PassWord = item.卡片密码;
                    Card.CardPassword = MD5Helper.StrToMD5WithKey(PassWord);//给密码MD5加密
                    Card.MemberShipTypeId = MemberShipTypeId;
                    Card.CreateDate = DateTime.Now;
                    ListMemberCard.Add(Card);
                }
                var result = Cache_MemberShipCard.Instance.Insert_MemberCard2(ListMemberCard);
                if (result == "true")
                {
                    return Json(new
                    {
                        pic = pic,
                        error = "上传成功"
                    });
                }
                else if (result == "false")
                {
                    return Json(new
                    {
                        pic = pic,
                        error = "导入时出现问题，请重新导入"
                    });
                }
                else
                {
                    return Json(new
                    {
                        pic = pic,
                        error = result
                    });
                }

            }
            else
            {
                return Json(new
                {
                    pic = pic,
                    error = "上传Excel格式不正确"
                });
            }
        }


    }
}