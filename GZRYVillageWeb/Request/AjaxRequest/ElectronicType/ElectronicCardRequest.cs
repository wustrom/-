using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.ElectronicType
{
    public class ElectronicCardRequest
    {
        /// <summary>
        /// 卡片名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡片名称不能为空")]
        public string CardName { get; set; }

        /// <summary>
        /// 卡片密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "卡片密码不能为空")]
        public string CardPassWord { get; set; }
        /// <summary>
        /// 电子储值卡类型Id
        /// </summary>
      public int ElectronicTypeId { get; set; }
        /// <summary>
        /// 卡片Id
        /// </summary>
        public int ElectronicId { get; set; }
    }
}