using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class CommonHtml
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 HtmlID { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String HtmlType { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String HtmlContent { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "HtmlID";
        }

    }
}
