using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Push.YouMenResult.JsonIOS
{
    /// <summary>
    /// 发送到友盟的json实体类
    /// </summary>
    public class PostUMengJsonIOS : PostUMengJsonBase
    {
        /// <summary>
        /// 必填 消息内容(IOS最大为2012B),
        /// </summary>
        public Payload payload { get; set; }
    }


    public class Payload
    {
        /// <summary>
        /// 必填 消息类型，值可以为:notification-通知，message-消息
        /// </summary>
        public Aps aps { get; set; }
        ///下面的键值对好蛋疼
    }

    public class Aps
    {
        /// <summary>
        /// （提示内容）必填
        /// </summary>
        public string alert { get; set; }

        /// <summary>
        /// 角标(提示数据条数)选填
        /// </summary>
        public string badge { get; set; }

        /// <summary>
        /// 声音可选
        /// </summary>
        public string sound { get; set; }

        /// <summary>
        /// 可选
        /// </summary>
        public string content_available { get; set; }

        /// <summary>
        /// 可选, 注意: ios8才支持该字段。
        /// </summary>
        public string category { get; set; }

    }


    public class Policy
    {
        public string start_time { get; set; }
        public string expire_time { get; set; }
        public int max_send_num { get; set; }
        public string out_biz_no { get; set; }
    }
}
