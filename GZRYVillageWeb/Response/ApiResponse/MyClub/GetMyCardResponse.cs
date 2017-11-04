using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 获得我的卡片的应答
    /// </summary>
    public class GetMyCardResponse
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="List_Type"></param>
        public GetMyCardResponse(MemberShipCard card, List<MemberShipType> List_Type, string Url)
        {
            //卡片ID
            this.MemberShipCardId = card.MemberShipCardId;
            //图片
            var type = List_Type.Where(p => p.MemberShipTypeId == card.MemberShipTypeId).FirstOrDefault();
            var ImageString = type == null ? null : type.CardImage;
            this.ImgUrl = "http://" + Url + ImageString;
        }
        /// <summary>
        /// 卡ID
        /// </summary>
        public Int32 MemberShipCardId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl { get; set; }
    }
}