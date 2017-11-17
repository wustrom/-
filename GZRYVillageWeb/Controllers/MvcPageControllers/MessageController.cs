using Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcPageControllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
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
            string filePhysicalPath = Server.MapPath("~/upload/MessageImage");
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
                    pic = "/upload/MessageImage/" + fileName;
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