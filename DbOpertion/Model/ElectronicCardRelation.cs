using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class ElectronicCardRelation
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 ElectronicCardRelationId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ElectronicCardId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? CreatTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ElectronicCardRelationId";
        }

    }
}
