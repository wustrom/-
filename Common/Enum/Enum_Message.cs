using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum_My
{
    /// <summary>
    /// 返回信息
    /// </summary>
    public enum Enum_Message
    {
        /// <summary>
        /// 用户Token失效信息
        /// </summary>
        TokenInvalidMessage = 0,
        /// <summary>
        /// 登入成功信息
        /// </summary>
        LoginSuccessMessage = 1,
        /// <summary>
        /// 成功返回信息
        /// </summary>
        SuccessMessage = 2,
        /// <summary>
        /// 没有更多数据信息
        /// </summary>
        NoMoreDataMessage = 3,
        /// <summary>
        /// 上传图片失败信息
        /// </summary>
        UpLoadImageFailMessage = 4,
        /// <summary>
        /// 用户不存在信息
        /// </summary>
        UserNotExitMessage = 5,
        /// <summary>
        /// 数据修改不成功信息
        /// </summary>
        DataNotSuccessMessage = 6,
        /// <summary>
        /// 数据存在信息
        /// </summary>
        DataExitMessage = 7,
        /// <summary>
        /// 数据修改失败信息
        /// </summary>
        DataChangeFailMessage = 8,
        /// <summary>
        /// 密码验证失败
        /// </summary>
        PasswordInvalidMessage = 9,
        /// <summary>
        /// 用户名和密码对应失败
        /// </summary>
        UserNameOrPasswordNotRightMessage = 10,
        /// <summary>
        /// 数据删除成功
        /// </summary>
        DataDeleteSuccessMessage = 11,
        /// <summary>
        /// 数据插入成功
        /// </summary>
        DataInsertSuccessMessage = 12,
        /// <summary>
        /// 当前名称已存在
        /// </summary>
        DataExitNameMessage=13,
        /// <summary>
        /// 当前用户名或手机号,邮箱已存在
        /// </summary>
        DataExitPhoneOrNameMessage = 14,
        /// <summary>
        /// 该会员卡目前没有绑定人
        /// </summary>
        DataNotExitUserNickName=15,
        /// <summary>
        /// 上传图片格式不正确
        /// </summary>
        DataNotSuccessUploadImage = 16,
        /// <summary>
        /// 该会员卡目前绑定人：
        /// </summary>
        DataSuccessNowUserNickName=17,
        /// <summary>
        /// 数据修改成功
        /// </summary>
        DataChangeSuccessMessage = 20,
    }

    public static partial class GetString
    {
        /// <summary>
        /// 根据模型获取对应信息
        /// </summary>
        /// <param name="Enum_Model">当前枚举</param>
        /// <returns></returns>
        public static string Enum_GetString(this Enum_Message Enum_Model)
        {
            string result = string.Empty;
            switch (Enum_Model)
            {
                case Enum_Message.TokenInvalidMessage:
                    result = "对不起,当前用户Token失效";
                    break;
                case Enum_Message.LoginSuccessMessage:
                    result = "登录成功";
                    break;
                case Enum_Message.SuccessMessage:
                    result = "数据成功返回";
                    break;
                case Enum_Message.NoMoreDataMessage:
                    result = "没有更多数据";
                    break;
                case Enum_Message.UpLoadImageFailMessage:
                    result = "图片上传失败";
                    break;
                case Enum_Message.UserNotExitMessage:
                    result = "当前用户不存在";
                    break;
                case Enum_Message.DataNotSuccessMessage:
                    result = "数据修改不成功";
                    break;
                case Enum_Message.DataExitMessage:
                    result = "数据已经存在";
                    break;
                case Enum_Message.DataChangeFailMessage:
                    result = "数据修改失败";
                    break;
                case Enum_Message.PasswordInvalidMessage:
                    result = "密码少于6位或者出现特异字符必须数字和字母混合";
                    break;
                case Enum_Message.UserNameOrPasswordNotRightMessage:
                    result = "用户名或密码错误";
                    break;
                case Enum_Message.DataDeleteSuccessMessage:
                    result = "数据删除成功";
                    break;
                case Enum_Message.DataInsertSuccessMessage:
                    result = "数据插入成功";
                    break;
                case Enum_Message.DataExitNameMessage:
                    result = "当前名称已存在";
                    break;
                case Enum_Message.DataExitPhoneOrNameMessage:
                    result = "当前用户名,手机号或邮箱已存在";
                    break;
                case Enum_Message.DataNotExitUserNickName:
                    result = "该会员卡目前没有绑定人";
                    break;
                case Enum_Message.DataNotSuccessUploadImage:
                    result = "上传图片格式不正确";
                    break;
                case Enum_Message.DataSuccessNowUserNickName:
                    result = "该会员卡目前绑定人：";
                    break;
                case Enum_Message.DataChangeSuccessMessage:
                    result = "数据修改成功";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }
    }
}
