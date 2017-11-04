using Aliyun.Acs.Dysmsapi.Model.V20170525;
using AliyunHelper.SendMail;
using Common.Enum_My;
using Common.Extend;
using Common.Filter;
using Common.Filter.WebApi;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using DbOpertion.Operation;
using GZRYVillageWeb.Common.Api.Enum;
using GZRYVillageWeb.Request.ApiRequest;
using GZRYVillageWeb.Response.ApiResponse;
using System;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace GZRYVillageWeb.Controllers
{
    /// <summary>
    /// 用户Api控制器
    /// </summary>
    public class UserController : ApiController
    {
        HttpSessionState session = HttpContext.Current.Session;

        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<UserTokenResponse> Login(UserLoginRequest request)
        {
            ResultJsonModel<UserTokenResponse> result = new ResultJsonModel<UserTokenResponse>();
            #region 用户密码查询
            var userToken = Cache_TUser.Instance.MemberUserLogin(request.UserPhone, request.UserPassword);
            if (userToken == null)
            {
                result.HttpCode = 301;
                result.Message = Enum_Message.UserNameOrPasswordNotRightMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.LoginSuccessMessage.Enum_GetString();
                result.Model1 = new UserTokenResponse { UserToken = userToken };
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson SendEmail(SendEmailRequest request)
        {
            ResultJson result = new ResultJson();
            if (Cache_TUser.Instance.GetUserResetCode(request.Phone))
            {
                string verification = Cache_TUser.Instance.SetMemberCode(request.Phone);
                Enum_SendEmailCode SendEmail;
                if (request.SendEmailType.IsNullOrEmpty() || request.SendEmailType.ToLower() == Enum_SearchType.Registration.Enum_GetString())
                {
                    SendEmail = Enum_SendEmailCode.UserRegistrationVerificationCode;
                }
                else if (request.SendEmailType.ToLower() == Enum_SearchType.ResetPassword.Enum_GetString())
                {
                    SendEmail = Enum_SendEmailCode.MessageChangeVerificationCode;
                }
                else if (request.SendEmailType.ToLower() == Enum_SearchType.ChangePhone.Enum_GetString())
                {
                    SendEmail = Enum_SendEmailCode.ModifyPasswordAuthenticationCode;
                }
                else
                {
                    SendEmail = Enum_SendEmailCode.AuthenticationCode;
                }
                SendSmsResponse Email = SendMail.Instance.SendEmail(request.Phone, verification, SendEmail);
                if (Email.Code.ToUpper() == "OK")
                {
                    result.HttpCode = 200;
                    result.Message = "发送信息成功";
                }
                else
                {
                    result.HttpCode = 500;
                    result.Message = Email.Message;
                }
            }
            else
            {
                result.HttpCode = 500;
                result.Message = "请过段时间重新发送";
            }
            return result;
        }

        /// <summary>
        /// 手机注册
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel MailRegister(MailRegisterRequest request)
        {
            ResultJsonModel result = new ResultJsonModel();
            string CardName;
            if (session["MemberShipCardName"] != null)
            {
                CardName = session["MemberShipCardName"].ToString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = "卡片验证超过时间限制，请重新验证";
                return result;
            }
            var users = Cache_TUser.Instance.SelectRepeat(request.Phone, request.UserName);
            if (users != null)
            {
                result.HttpCode = 300;
                result.Message = "用户已注册";
            }
            else
            {
                var verification = Cache_TUser.Instance.GetMemberCode(request.Phone);
                if (verification == null)
                {
                    result.HttpCode = 500;
                    result.Message = "请重新发送验证码";
                }
                else
                {
                    if (request.Code == verification)
                    {
                        TUser t_user = new TUser();
                        t_user.UserName = request.UserName;
                        t_user.UserNickName = request.UserNickName;
                        t_user.UserPhone = request.Phone;
                        t_user.UserPassword = request.PassWord;
                        t_user.Sex = request.UserSex;
                        t_user.CreateTime = DateTime.Now;
                        var user = Cache_TUser.Instance.InsertUserInfo(t_user, CardName, true);
                        if (user != null)
                        {
                            result.HttpCode = 200;
                            result.Message = "注册成功";
                        }
                        else
                        {
                            result.HttpCode = 300;
                            result.Message = "注册失败";
                        }
                    }
                    else
                    {
                        result.HttpCode = 400;
                        result.Message = "验证码错误";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel ForgotPassword(ForgotPasswordRequest request)
        {
            ResultJsonModel result = new ResultJsonModel();
            TUser MemUser = new TUser();
            MemUser.UserPhone = request.Phone;
            var users = Cache_TUser.Instance.SelectRepeat(request.Phone, null);
            VerificationCodeRequest CodeRequest = new VerificationCodeRequest { Code = request.Code, Phone = request.Phone };
            var CodeResonse = VerificationCode(CodeRequest);
            if (CodeResonse.HttpCode != 200)
            {
                result.HttpCode = 300;
                result.Message = CodeResonse.Message;
                return result;
            }
            if (users == null)
            {
                result.HttpCode = 300;
                result.Message = "用户尚未注册";
            }
            else
            {
                users.UserPassword = MD5Helper.StrToMD5WithKey(request.PassWord);
                users.UserPhone = request.Phone;
                if (TUserOper.Instance.Update(users))
                {
                    Cache_TUser.Instance.ClearMemberCode(request.Phone);
                    result.HttpCode = 200;
                    result.Message = "密码重置成功";
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = "密码重置失败";
                }
            }
            return result;
        }

        /// <summary>
        /// 重置手机号码
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel ResetPhone(ResetPhoneRequest request)
        {
            ResultJsonModel result = new ResultJsonModel();
            VerificationCodeRequest CodeRequest = new VerificationCodeRequest { Code = request.Code, Phone = request.Phone };
            var CodeResponse = VerificationCode(CodeRequest);
            if (CodeResponse.HttpCode == 200)
            {
                TUser MemUser = new TUser();
                MemUser.UserPhone = request.Phone;
                var users = Cache_TUser.Instance.SelectRepeat(request.Phone, null);
                if (users == null)
                {
                    result.HttpCode = 300;
                    result.Message = "用户尚未注册";
                }
                else
                {
                    if (TUserOper.Instance.Update(users))
                    {
                        Cache_TUser.Instance.ClearMemberCode(request.Phone);
                        result.HttpCode = 200;
                        result.Message = "密码重置成功";
                    }
                    else
                    {
                        result.HttpCode = 300;
                        result.Message = "密码重置失败";
                    }
                }
            }
            else
            {
                result.HttpCode = CodeResponse.HttpCode;
                result.Message = CodeResponse.Message;
            }
            return result;
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel VerificationCode(VerificationCodeRequest request)
        {
            ResultJsonModel result = new ResultJsonModel();
            var verification = Cache_TUser.Instance.GetMemberCode(request.Phone);
            if (verification == null)
            {
                result.HttpCode = 500;
                result.Message = "请重新发送验证码";
            }
            else
            {
                if (request.Code == verification)
                {
                    result.HttpCode = 200;
                    result.Message = "验证码正确";
                }
                else
                {
                    result.HttpCode = 400;
                    result.Message = "验证码错误";
                }
            }
            return result;
        }

        /// <summary>
        /// 验证卡片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJson VerificCard(MemberCardRequest request)
        {
            ResultJson jsonResult = new ResultJson();
            if (Cache_MemberShipCard.Instance.MemberShip(request.CardName, request.CardPassword, null, null))
            {
                session.Add("MemberShipCardName", request.CardName);
                jsonResult.HttpCode = 200;
                jsonResult.Message = "验证卡片成功";
            }
            else
            {
                jsonResult.HttpCode = 300;
                jsonResult.Message = "验证卡片失败";
            }
            return jsonResult;
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel<UserInfoResponse> GetUserInfo(UserTokenRequest request)
        {
            ResultJsonModel<UserInfoResponse> result = new ResultJsonModel<UserInfoResponse>();
            Token token = new Token(request.UserToken);
            var Tuser = Cache_TUser.Instance.GetUserInfo(token);
            if (Tuser == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
            }
            else
            {
                UserInfoResponse response = new UserInfoResponse(Tuser);
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                result.Model1 = response;
            }
            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel ModifyUserInfo(ModifyUserInfoRequest request)
        {
            ResultJsonModel result = new ResultJsonModel();
            Token token = new Token(request.UserToken);
            TUser Tuser = new TUser();
            Tuser.UserId = token.Payload.UserID;
            Tuser.Sex = request.UserSex;
            Tuser.UserNickName = request.UserNickName;
            if (!Cache_TUser.Instance.UpdateUserInfo(Tuser))
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
            }
            else
            {
                result.HttpCode = 200;
                result.Message = Enum_Message.SuccessMessage.Enum_GetString();
            }
            return result;
        }

        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [WebApiException]
        public ResultJsonModel ModifyPassword(ModifyPasswordRequest request)
        {
            ResultJsonModel result = new ResultJsonModel();
            Token token = new Token(request.UserToken);
            var Tuser = Cache_TUser.Instance.GetUserInfo(token);
            if (Tuser == null)
            {
                result.HttpCode = 300;
                result.Message = Enum_Message.UserNotExitMessage.Enum_GetString();
            }
            else
            {
                var Update_Result = Cache_TUser.Instance.UpdatePassword(Tuser, request.OldPassword, request.NewPassword);
                if (Update_Result == "true")
                {
                    result.HttpCode = 200;
                    result.Message = Enum_Message.SuccessMessage.Enum_GetString();
                }
                else if (Update_Result == "false")
                {
                    result.HttpCode = 300;
                    result.Message = Enum_Message.DataNotSuccessMessage.Enum_GetString();
                }
                else
                {
                    result.HttpCode = 300;
                    result.Message = Update_Result;
                }

            }
            return result;
        }
    }
}