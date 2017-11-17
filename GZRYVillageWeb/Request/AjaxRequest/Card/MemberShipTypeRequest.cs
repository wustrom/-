using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    public class MemberShipTypeRequest
    {
        /// <summary>
        /// 卡片图案
        /// </summary>
        [Required(ErrorMessage = "请选择卡片图案", AllowEmptyStrings = false)]
        public string CardImage { get; set; }
        /// <summary>
        /// 卡片名称
        /// </summary>
        [Required(ErrorMessage = "请输入卡片名称", AllowEmptyStrings = false)]
        public string CardName { get; set; }
        /// <summary>
        /// 卡片类型Id
        /// </summary>
        public int MemberShipTypeId { get; set; }
    }
}