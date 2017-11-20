using Common.Extend;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    public class QRCodeAjaxController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QRCodeStr">二维码字符串</param>
        /// <returns></returns>
        public JsonResult Index(string QRCodeStr)
        {
            var str = EncryptionHelper.Instance.DESDecrypt(QRCodeStr);
            if (!str.IsNullOrEmpty())
            {
                return new JsonResult() { Data = new { aaa = 123, bbb = 123 } };
            }
            else
            {
                return new JsonResult() { Data = new { aaa = 123, bbb = 123 } };
            }
        }
    }
}