using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    /// <summary>
    /// 电子储值卡
    /// </summary>
    public class ElectronicTypeRequest
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
        public string CardTypeName { get; set; }
        /// <summary>
        /// 储值金额
        /// </summary>
       [Required(ErrorMessage = "请输入储值金额", AllowEmptyStrings = false)]
       [DecimalValid(ErrorMessage = "储值金额请输入数字", AllowZero = true)]
        public decimal CardMoney { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
       [Required(ErrorMessage = "请输入过期时间",AllowEmptyStrings = false)]
       [IntValid(ErrorMessage ="过期时间请输入数字", AllowZero = true)]
        public int CardExpirationMonth { get; set; }
    }
}