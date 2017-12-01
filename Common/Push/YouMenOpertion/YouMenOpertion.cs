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
        UMengMessagePush umPushAndroid = new UMengMessagePush("5a1d04eaf43e4877a2000015", "b5onae9ms4nkfzueqibldr9u3t4uu5sb");
        UMengMessagePush umPushIos = new UMengMessagePush("5a1d06b88f4a9d1e8a000049", "xuw842xqepmikzmaq515yjrcbg27jel3");

        /// <summary>
        /// 推送给所有安卓用户
        /// </summary>
        public ReturnJsonClass AndriodPushByAllUser(string ticker,string Title, string text, string Description)
        {
            PostUMengJsonAndroid postJson = new PostUMengJsonAndroid();
            postJson.type = "broadcast";
            postJson.payload = new YouMenResult.JsonAndroid.Payload();
            postJson.payload.display_type = "notification";
            postJson.payload.body = new ContentBody();
            postJson.payload.body.ticker = ticker;
            postJson.payload.body.title = Title;
            postJson.payload.body.text = text;
            postJson.payload.body.after_open = "go_custom";
            postJson.payload.body.custom = "comment-notify";
            postJson.description = Description;
            postJson.production_mode = true;
            postJson.thirdparty_id = "COMMENT";
            ReturnJsonClass resu = umPushAndroid.SendMessage(postJson);
            return resu;
        }

        /// <summary>
        /// 推送给所有IOS用户
        /// </summary>
        public ReturnJsonClass IOSPushByAllUser(string Alert, string Description)
        {
            PostUMengJsonIOS postJson = new PostUMengJsonIOS();
            postJson.type = "broadcast";
            postJson.payload = new YouMenResult.JsonIOS.Payload();
            postJson.payload.aps = new Aps();
            postJson.payload.aps.alert = Alert;
            postJson.description = Description;
            postJson.thirdparty_id = "COMMENT";
            postJson.production_mode = false;
            ReturnJsonClass resu = umPushIos.SendMessage(postJson);
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
            umPushAndroid.AsynSendMessage(postJson, callBack);
        }

        private void callBack(ReturnJsonClass result)
        {
            ReturnJsonClass a1 = result;
        }
    }
}
