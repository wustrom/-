using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.AjaxResponse
{
    public class TUserResponse
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tuser"></param>
        public TUserResponse(TUser tuser)
        {
            this.UserId = tuser.UserId;
            this.UserName = tuser.UserName;
            this.UserNickName = tuser.UserNickName;
            this.UserPassword = tuser.UserPassword;
            this.UserPhone = tuser.UserPhone;
            this.UserImage = tuser.UserImage;
            this.UserEmail = tuser.UserEmail;
            this.ConsumptionTime = tuser.ConsumptionTime;
            this.CreateTime = tuser.CreateTime;
            this.IsDelete = tuser.IsDelete;
            if (tuser.Sex == true)
            {
                this.Sex = "男";
            }
            else if (tuser.Sex == false)
            {
                this.Sex = "女";
            }
            else
            {
                this.Sex = "未知";
            }
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        ///用户名
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        ///用户昵称
        /// </summary>
        public String UserNickName { get; set; }
        /// <summary>
        ///用户密码
        /// </summary>
        public String UserPassword { get; set; }
        /// <summary>
        ///手机号码
        /// </summary>
        public String UserPhone { get; set; }
        /// <summary>
        ///电子邮件
        /// </summary>
        public String UserEmail { get; set; }
        /// <summary>
        ///用户图片
        /// </summary>
        public String UserImage { get; set; }
        /// <summary>
        ///购买次数
        /// </summary>
        public Int32? ConsumptionTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///是否失效
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        ///用户性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "UserId";
        }
    }
}