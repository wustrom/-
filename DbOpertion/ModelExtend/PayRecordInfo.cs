using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Models
{
    public class PayRecordInfo
    {
    
            /// <summary>
            /// 消费记录表Id
            /// </summary>
            public Int32 PayRecordId { get; set; }
            /// <summary>
            ///电子储值卡Id
            /// </summary>
            public Int32? ElectronicId { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            public String UserNickName { get; set; }
            /// <summary>
            ///商店名称
            /// </summary>
            public String ShopItem { get; set; }
            /// <summary>
            ///消费金额
            /// </summary>
            public Decimal? PayMoney { get; set; }
            /// <summary>
            ///消费时间
            /// </summary>
            public DateTime? ShopTime { get; set; }
            /// <summary>
            /// 获取对应主键
            /// </summary>
            public string GetBuilderPrimaryKey()
            {
                return "PayRecordId";
            }
        }
    }

