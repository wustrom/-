using Common.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    public class InsertMemberCard
    {
        /// <summary>
        /// 生成日期
        /// </summary>
        [Required(ErrorMessage = "请选择日期", AllowEmptyStrings = false)]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 卡片名称
        /// </summary>
        [Required(ErrorMessage = "请输入卡片名称", AllowEmptyStrings = false)]
        public string CardName { get; set; }
        /// <summary>
        /// 卡片类型Id
        /// </summary>
        public int MemberShipTypeId { get; set; }
        /// <summary>
        /// 会员卡Id
        /// </summary>
        public int MemberShipCardId { get; set; }
        /// <summary>
        /// 卡片密码
        /// </summary>
        [Required(ErrorMessage = "请输入卡片密码", AllowEmptyStrings = false)]
        public string CardPassword { get; set; }
    }
}