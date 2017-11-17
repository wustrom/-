using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Models
{
   public class MemberCardByTypeInfo
    {
        ///<summary>
        ///会员卡Id
        /// </summary>
        public Int32 MemberShipCardId { get; set; }
        /// <summary>
        ///会员卡名称
        /// </summary>
        public String CardName { get; set; }
        /// <summary>
        ///会员卡类型Id
        /// </summary>
        public Int32? MemberShipTypeId { get; set; }
        /// <summary>
        ///用户Id
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        /// 用户名/绑定人
        /// </summary>
        public string UserNickName { get; set; }
        /// <summary>
        ///激活日期
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        ///是否使用
        /// </summary>
        public Boolean? IsUser { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "MemberShipCardId";
        }
    }
}
