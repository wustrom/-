using Common.Push.YouMenBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Push.YouMenResult.JsonAndroid
{
    /// <summary>
    /// 发送到友盟的json实体类
    /// </summary>
    public class PostUMengJsonAndroid : PostUMengJsonBase
    {
        /// <summary>
        /// 必填 消息内容(Android最大为1840B),
        /// </summary>
        public Payload payload { get; set; }
    }


    public class Payload
    {
        /// <summary>
        /// 必填 消息类型，值可以为:notification-通知，message-消息
        /// </summary>
        public string display_type { get; set; }
        /// <summary>
        /// 必填 消息体
        /// </summary>
        public ContentBody body { get; set; }

        /// <summary>
        /// 可选 用户自定义key-value。只对"通知"(display_type=notification)生效。
        /// 可以配合通知到达后,打开App,打开URL,打开Activity使用。
        /// </summary>
        public SerializableDictionary<string, string> extra { get; set; }
    }

    public class ContentBody
    {
        /// <summary>
        /// 必填 通知栏提示文字
        /// </summary>
        public string ticker { get; set; }
        /// <summary>
        /// 必填 通知标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 必填 通知文字描述
        /// </summary>
        public string text { get; set; }
        public string icon { get; set; }
        public string largeIcon { get; set; }
        public string img { get; set; }
        public string sound { get; set; }
        public int builder_id { get; set; }
        public string play_vibrate { get; set; }
        public string play_lights { get; set; }
        public string play_sound { get; set; }
        /// <summary>
        /// 必填 点击"通知"的后续行为，默认为打开app。
        /// 取值：
        /// "go_app": 打开应用
        /// "go_url": 跳转到URL
        /// "go_activity": 打开特定的activity
        /// "go_custom": 用户自定义内容。
        /// </summary>
        public string after_open { get; set; }
        public string url { get; set; }
        public string activity { get; set; }
        public string custom { get; set; }
    }

    public class Policy
    {
        public string start_time { get; set; }
        public string expire_time { get; set; }
        public int max_send_num { get; set; }
        public string out_biz_no { get; set; }
    }
}
