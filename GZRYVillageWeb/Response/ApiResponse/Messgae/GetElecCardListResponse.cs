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
    public class GetMessageListResponse
    {
        /// <summary>
        /// 获得我的信息列表应答
        /// </summary>
        public GetMessageListResponse(Message message, string Host)
        {
            this.MessageID = message.MessageID;

            this.MessageName = message.MessageName;

            this.MessageImage = "http://" + Host + message.MessageImage;

            this.MessageDescribe = message.MessageDescribe;
        }

        /// <summary>
        /// 消息Id
        /// </summary>
        public Int32 MessageID { get; set; }
        /// <summary>
        /// 消息名称
        /// </summary>
        public String MessageName { get; set; }
        /// <summary>
        /// 消息图片
        /// </summary>
        public String MessageImage { get; set; }
        /// <summary>
        /// 消息简介
        /// </summary>
        public String MessageDescribe { get; set; }
    }
}