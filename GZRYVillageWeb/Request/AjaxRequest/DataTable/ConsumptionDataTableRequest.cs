using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.DataTable
{
    /// <summary>
    /// 使用ElectronicId的DataTable
    /// </summary>
    public class ConsumptionDataTableRequest:DataTableRequest
    {
        /// <summary>
        /// 电子储值卡Id
        /// </summary>
        public int ElectronicId { get; set; }
       
    }
}