using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Message
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 MessageID { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String MessageName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String MessageContains { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String MessageImage { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String MessageDescribe { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "MessageID";
        }

    }
}
