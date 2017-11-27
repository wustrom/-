using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    /// <summary>
    /// 建立订单请求
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [IntValid(ErrorMessage = "用户Id不能为空")]
        public int UserID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [IntValid(ErrorMessage = "金额不能为空")]
        public decimal ShopMoney { get; set; }
        /// <summary>
        /// 消费项目
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "消费项目不能为空")]
        public string ShopItem { get; set; }
    }
}