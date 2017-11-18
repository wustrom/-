using Common.Helper;
using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 获得我的信息列表应答
    /// </summary>
    public class GetStoreListResponse
    {
        /// <summary>
        /// 获得我的信息列表应答
        /// </summary>
        public GetStoreListResponse(Store store, double? CoordX, double? CoordY)
        {
            //定位
            this.Coord = store.Coord;
            //地址
            this.Adress = store.Adress;
            //店名
            this.StoreName = store.StoreName;

            if (store.Coord != null && CoordX != null && CoordX != null)
            {
                string[] coordinate = store.Coord.Split(',');
                double coordX = double.Parse(coordinate[0]);
                double coordY = double.Parse(coordinate[1]);
                var Distance = BaiduMapHelper.Instance.getDistance(CoordX.Value, CoordY.Value, coordX, coordY);
                if (Distance < 2000)
                {
                    this.Distance = Distance.ToString();
                }
                else
                {
                    this.Distance = "2500";
                }
            }
            else
            {
                this.Distance = "2500";
            }

        }

        /// <summary>
        /// 定位
        /// </summary>
        public String Coord { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public String Adress { get; set; }

        /// <summary>
        /// 店名
        /// </summary>
        public String StoreName { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public string Distance { get; set; }
    }
}