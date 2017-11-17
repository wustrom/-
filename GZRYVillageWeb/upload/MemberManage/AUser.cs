using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class AUser
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserNickName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserPassword { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserPhone { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserEmail { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserImage { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ConsumptionTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "UserId";
        }

    }
}
