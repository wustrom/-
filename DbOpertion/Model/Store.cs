using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Store
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 StoreId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String Coord { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String Adress { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String Phone { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? Sales { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String StoreName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String UserPassword { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "StoreId";
        }

    }
}
