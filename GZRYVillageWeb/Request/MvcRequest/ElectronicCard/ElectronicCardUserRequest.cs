using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.MvcRequest.ElectronicCard
{
    public class ElectronicCardUserRequest
    {
        /// <summary>
        /// 储值卡类型Id
        /// </summary>
        public int ElectronicTypeId { get; set; }
        /// <summary>
        /// 储值卡Id
        /// </summary>
        public int ElectronicId { get; set; }
    }
}