using Common.Extend;
using Common.Helper;
using Common.Mvc.Filter;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.MvcRequest.ElectronicCard;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// 上传卡片图案
        /// </summary>
        /// <param name="upImg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase upImg)
        {

            var fileExten = Path.GetExtension(upImg.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/ElectronicCardType");
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
                    upImg.SaveAs(filePhysicalPath + "/" + fileName);
                    pic = "/upload/ElectronicCardType/" + fileName;
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
        /// <param name="ElectronicTypeId">储值卡类型Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase upExcel, int ElectronicTypeId)
        {
            var fileExten = Path.GetExtension(upExcel.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/ElectronicCardUploadExcel");
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
                List<ElectronicCard> ListElectronicCard = new List<ElectronicCard>();
                var TypeCardMoney = Cache_ElectronicType.Instance.GetCardMoneyById(ElectronicTypeId);
                foreach (var item in cardList)
                {
                    ElectronicCard Card = new ElectronicCard();
                    Card.CardName = item.卡片名称;
                    string PassWord = item.卡片密码;
                    Card.CardPassword = MD5Helper.StrToMD5WithKey(PassWord);//给密码MD5加密
                    Card.ElectronicTypeId = ElectronicTypeId;
                    Card.CaerMoney = TypeCardMoney.CardMoney;
                    ListElectronicCard.Add(Card);
                }
                var result = Cache_ElectronicCard.Instance.Insert_ElectronicCardByExcel(ListElectronicCard);
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