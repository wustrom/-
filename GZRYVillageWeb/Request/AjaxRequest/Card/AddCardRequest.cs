﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.Card
{
    public class AddCardRequest
    {
        /// <summary>
        /// 卡片名称
        /// </summary>
        [Required(ErrorMessage = "请填写卡片名称", AllowEmptyStrings = false)]
        public string MemberShipCardName { get; set; }
        /// <summary>
        /// 卡片图案
        /// </summary>
        [Required(ErrorMessage = "请选择卡片图案", AllowEmptyStrings = false)]
        public int MemberShipTypeId { get; set; }
        /// <summary>
        /// 所包含的优惠券
        /// </summary>
       // [Required(ErrorMessage = "请选择优惠券", AllowEmptyStrings = false)]
        public string CouponName { get; set; }
        /// <summary>
        /// 卡密
        /// </summary>
        public string CardPassword { get; set; }
        /// <summary>
        /// 是否被使用
        /// </summary>
        public Boolean IsUser { get; set; }
        /// <summary>
        /// 是否失效
        /// </summary>
        public Boolean IsDelete { get; set; }

    }
}