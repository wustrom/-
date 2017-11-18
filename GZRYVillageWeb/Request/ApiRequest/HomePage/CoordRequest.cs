using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 地址请求
    /// </summary>
    public class CoordRequest
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double? Longitude { get; set; }
    }
}