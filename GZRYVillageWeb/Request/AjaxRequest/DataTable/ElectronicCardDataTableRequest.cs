using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.DataTable
{
    /// <summary>
    /// 使用电子储值卡的DataTable
    /// </summary>
    public class ElectronicCardDataTableRequest:DataTableRequest
    {
        /// <summary>
        /// 储值卡类型Id
        /// </summary>
        public int ElectronicTypeId { get; set; }
        public string UserNickName { get; set; }
    }
}