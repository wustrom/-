using Common.Push.YouMenBase;
using Common.Push.YouMenResult;
using Common.Push.YouMenResult.JsonAndroid;
using Common.Push.YouMenResult.JsonIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Push.YouMenOpertion
{
    public class YouMenOpertion : SingleTon<YouMenOpertion>
    {
        UMengMessagePush umPush = new UMengMessagePush("58105bf1aed17925a900395f", "v2ylbfwcsd1dqnkasionxf1uhicnxjey");

        /// <summary>
        /// 推送给所有安卓用户
        /// </summary>
        public ReturnJsonClass AndriodPushByAllUser()
        {
            PostUMengJsonAndroid postJson = new PostUMengJsonAndroid();
            postJson.type = "broadcast";
            postJson.payload = new YouMenResult.JsonAndroid.Payload();
            postJson.payload.display_type = "notification";
            postJson.payload.body = new ContentBody();
            postJson.payload.body.ticker = "评论提醒";
            postJson.payload.body.title = "您的评论有回复";
            postJson.payload.body.text = "您的评论有回复咯。。。。。";
            postJson.payload.body.after_open = "go_custom";
            postJson.payload.body.custom = "comment-notify";
            postJson.description = "评论提醒-UID:" + 123;
            postJson.thirdparty_id = "COMMENT";
            ReturnJsonClass resu = umPush.SendMessage(postJson);
            return resu;
        }

        /// <summary>
        /// 推送给所有IOS用户
        /// </summary>
        public ReturnJsonClass IOSPushByAllUser()
        {
            PostUMengJsonIOS postJson = new PostUMengJsonIOS();
            postJson.type = "broadcast";
            postJson.payload = new YouMenResult.JsonIOS.Payload();
            postJson.payload.aps = new Aps();
            postJson.payload.aps.alert = "123";
            postJson.payload.aps.badge = "123";
            postJson.payload.aps.category = "123";
            postJson.payload.aps.content_available = "123";
            postJson.payload.aps.sound = "123";
            postJson.description = "评论提醒-UID:" + 123;
            postJson.thirdparty_id = "COMMENT";
            ReturnJsonClass resu = umPush.SendMessage(postJson);
            return resu;
        }

        /// <summary>
        /// 根据自定义用户ID推送
        /// </summary>
        public void TestPushByAlias()
        {
            PostUMengJsonAndroid postJson = new PostUMengJsonAndroid();
            postJson.type = "customizedcast";
            postJson.alias_type = "USER_ID";
            postJson.alias = "5583";
            postJson.payload = new YouMenResult.JsonAndroid.Payload();
            postJson.payload.display_type = "notification";
            postJson.payload.body = new ContentBody();
            postJson.payload.body.ticker = "评论提醒Alias";
            postJson.payload.body.title = "您的评论有回复";
            postJson.payload.body.text = "Alias您的评论有回复咯。。。。。";
            postJson.payload.body.after_open = "go_custom";
            postJson.payload.body.custom = "comment-notify";
            postJson.thirdparty_id = "COMMENT";
            postJson.description = "评论提醒-UID:" + 5583;
            //ReturnJsonClass resu = umPush.SendMessage(postJson);
            umPush.AsynSendMessage(postJson, callBack);
        }

        private void callBack(ReturnJsonClass result)
        {
            ReturnJsonClass a1 = result;
        }
    }
}
