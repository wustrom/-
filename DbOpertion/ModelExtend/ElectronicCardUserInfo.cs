using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Models
{
    //[Serializable]
  public  class ElectronicCardUserInfo
    {
        /// <summary>
        /// 电子储值卡类型Id
        /// </summary>
        public int ElectronicTypeId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserNickName { get; set; }
        /// <summary>
        /// 电子储值卡Id
        /// </summary>
        public int ElectronicId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID { get; set; }
       
    }
}
