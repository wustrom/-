using Common.Enum_My;
using Common.Extend;
using Common.Helper;
using Common.Result;
using DbOpertion.Cache;
using DbOpertion.Models;
using GZRYVillageWeb.Request.AjaxRequest;
using GZRYVillageWeb.Response.AjaxResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GZRYVillageWeb.Controllers.MvcAjaxControllers
{
    /// <summary>
    /// 二维码Ajax控制器
    /// </summary>
    public class QRCodeAjaxController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QRCodeStr">二维码字符串</param>
        /// <returns></returns>
        public JsonResult Index(string QRCodeStr)
        {
            try
            {
                var str = EncryptionHelper.Instance.DESDecrypt(QRCodeStr);
                if (!str.IsNullOrEmpty())
                {
                    var array = str.Split(';').Where(p => !p.IsNullOrEmpty()).ToArray();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (array.Count() != 1)
                    {
                        foreach (var item in array)
                        {
                            var arrayItem = item.Split(':');
                            if (arrayItem.Count() == 2)
                            {
                                dic.Add(arrayItem[0], arrayItem[1]);
                            }
                            else if (arrayItem.Count() == 4)
                            {
                                dic.Add(arrayItem[0], arrayItem[1] + ":" + arrayItem[2] + ":" + arrayItem[3]);
                            }
                        }
                        var Time = dic.Where(p => p.Key.ToUpper() == "TIME").FirstOrDefault().Value;
                        if (Time != null)
                        {
                            var date = DateTime.Parse(Time);
                            var span = DateTime.Now - date;
                            if (span.Milliseconds > 1000 * 60)
                            {
                                ResultJson result1 = new ResultJson();
                                result1.HttpCode = 300;
                                result1.Message = "二维码已过期，请刷新!";
                                return new JsonResult() { Data = result1 };
                            }
                        }
                        else
                        {
                            ResultJson result1 = new ResultJson();
                            result1.HttpCode = 300;
                            result1.Message = "二维码已过期，请刷新!";
                            return new JsonResult() { Data = result1 };
                        }
                        var userId = dic.Where(p => p.Key.ToUpper() == "USERID").FirstOrDefault().Value;
                        if (dic.Where(p => p.Key.ToUpper() == "USERID").FirstOrDefault().Value != null)
                        {
                            ResultJsonModel<TUserInfoResponse, List<CouponInfoResponse>> json = new ResultJsonModel<TUserInfoResponse, List<CouponInfoResponse>>();
                            int IntId = 0;
                            if (int.TryParse(userId, out IntId))
                            {

                                var userInfo = Cache_TUser.Instance.GetUserInfo(IntId);
                                var membershipLevelList = Cache_MemberShipLevel.Instance.SelectLevelList();
                                if (userInfo != null)
                                {
                                    TUserInfoResponse response = new TUserInfoResponse(userInfo, membershipLevelList);
                                    json.HttpCode = 200;
                                    json.Message = Enum_Message.SuccessMessage.Enum_GetString();
                                    json.Model1 = response;
                                    var CouponList = Cache_Coupon.Instance.SelectByUserId(IntId);
                                    List<CouponInfoResponse> list_Response = new List<CouponInfoResponse>();
                                    foreach (var item in CouponList)
                                    {
                                        CouponInfoResponse infoResponse = new CouponInfoResponse(item);
                                        list_Response.Add(infoResponse);
                                    }
                                    json.Model2 = list_Response;
                                    return new JsonResult() { Data = json };
                                }
                                else
                                {
                                    json.HttpCode = 300;
                                    json.Message = "该用户并不存在";
                                    return new JsonResult() { Data = json };
                                }
                            }
                        }
                        ResultJson result = new ResultJson();
                        result.HttpCode = 300;
                        result.Message = "该用户并不存在";
                        return new JsonResult() { Data = result };
                    }
                }

            }
            catch (Exception)
            {

            }
            ResultJson resultJson = new ResultJson();
            resultJson.HttpCode = 300;
            resultJson.Message = "二维码格式不正确";
            return new JsonResult() { Data = resultJson };

        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <returns></returns>
        public JsonResult CreatOrder(CreateOrderRequest request)
        {
            PayRecord record = new PayRecord();
            record.StoreID = 1;
            record.UserId = request.UserID;
            record.ShopMoney = request.ShopMoney;
            record.NeedPayMoney = request.ShopMoney;
            record.ShopItem = request.ShopItem;
            record.ShopTime = DateTime.Now;
            var id = Cache_QRCode.Instance.CreateOrder(record);
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            if (id != null)
            {
                result.HttpCode = 200;
                result.Message = "生成订单成功";
                result.Model1 = id.ToString();
            }
            else
            {
                result.HttpCode = 300;
                result.Message = "生成订单失败";
            }
            return new JsonResult() { Data = result };
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteOrder(int PayRecordId)
        {
            var resultBool = Cache_QRCode.Instance.DeleteOrder(PayRecordId);
            ResultJsonModel<string> result = new ResultJsonModel<string>();
            if (resultBool)
            {
                result.HttpCode = 200;
                result.Message = "删除订单成功";
            }
            else
            {
                result.HttpCode = 300;
                result.Message = "删除订单失败";
            }
            return new JsonResult() { Data = result };
        }

        /// <summary>
        /// 电子储存卡二维码
        /// </summary>
        /// <param name="CardQRCodeStr"></param>
        /// <param name="PayRecordId"></param>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        public JsonResult ElecCardCode(string CardQRCodeStr, int PayRecordId, int CouponId)
        {
            try
            {
                var str = EncryptionHelper.Instance.DESDecrypt(CardQRCodeStr);
                if (!str.IsNullOrEmpty())
                {
                    var array = str.Split(';').Where(p => !p.IsNullOrEmpty()).ToArray();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (array.Count() != 1)
                    {
                        foreach (var item in array)
                        {
                            var arrayItem = item.Split(':');
                            if (arrayItem.Count() == 2)
                            {
                                dic.Add(arrayItem[0], arrayItem[1]);
                            }
                            else if (arrayItem.Count() == 4)
                            {
                                dic.Add(arrayItem[0], arrayItem[1] + ":" + arrayItem[2] + ":" + arrayItem[3]);
                            }
                        }
                        var Time = dic.Where(p => p.Key.ToUpper() == "TIME").FirstOrDefault().Value;
                        if (Time != null)
                        {
                            var date = DateTime.Parse(Time);
                            var span = DateTime.Now - date;
                            if (span.Milliseconds > 1000 * 60)
                            {
                                ResultJson result1 = new ResultJson();
                                result1.HttpCode = 300;
                                result1.Message = "二维码已过期，请刷新!";
                                return new JsonResult() { Data = result1 };
                            }
                        }
                        else
                        {
                            ResultJson result1 = new ResultJson();
                            result1.HttpCode = 300;
                            result1.Message = "二维码已过期，请刷新!";
                            return new JsonResult() { Data = result1 };
                        }

                        var Card = dic.Where(p => p.Key.ToUpper() == "CARDID").FirstOrDefault().Value;
                        if (Card == null)
                        {
                            ResultJson result1 = new ResultJson();
                            result1.HttpCode = 300;
                            result1.Message = "二维码格式不正确!";
                            return new JsonResult() { Data = result1 };
                        }
                        var userId = dic.Where(p => p.Key.ToUpper() == "USERID").FirstOrDefault().Value;
                        if (userId != null)
                        {
                            var returnResult = Cache_QRCode.Instance.PayFromCard(Card, PayRecordId, CouponId, int.Parse(userId));
                            if (returnResult.Item1)
                            {
                                ResultJsonModel<decimal, decimal, string, string> result2 = new ResultJsonModel<decimal, decimal, string, string>();
                                result2.HttpCode = 200;
                                result2.Message = "消费成功";
                                result2.Model1 = returnResult.Item2;
                                result2.Model2 = returnResult.Item3;
                                result2.Model3 = returnResult.Item4;
                                result2.Model4 = "UserId:" + userId + ";Time:" + DateTime.Now.ToString() + ";";
                                result2.Model4 = EncryptionHelper.Instance.DESEncrypt(result2.Model4);
                                return new JsonResult() { Data = result2 };
                            }
                            else
                            {
                                ResultJson result2 = new ResultJson();
                                result2.HttpCode = 300;
                                result2.Message = "当前卡片没有金额";
                                return new JsonResult() { Data = result2 };
                            }
                        }
                        ResultJson result = new ResultJson();
                        result.HttpCode = 300;
                        result.Message = "该用户并不存在";
                        return new JsonResult() { Data = result };
                    }
                }

            }
            catch (Exception)
            {

            }
            ResultJson resultJson = new ResultJson();
            resultJson.HttpCode = 300;
            resultJson.Message = "二维码格式不正确";
            return new JsonResult() { Data = resultJson };
        }

        /// <summary>
        /// 电子储存卡二维码
        /// </summary>
        /// <param name="PayRecordId"></param>
        /// <param name="CouponId"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public JsonResult PayForCash(int PayRecordId, int CouponId, int UserID)
        {
            try
            {
                var returnResult = Cache_QRCode.Instance.PayFromCash(PayRecordId, CouponId, UserID);
                if (returnResult)
                {
                    ResultJsonModel<string> result2 = new ResultJsonModel<string>();
                    result2.HttpCode = 200;
                    result2.Message = "消费成功";
                    result2.Model1 = "UserId:" + UserID + ";Time:" + DateTime.Now.ToString() + ";";
                    result2.Model1 = EncryptionHelper.Instance.DESEncrypt(result2.Model1);
                    return new JsonResult() { Data = result2 };
                }
                else
                {
                    ResultJson result2 = new ResultJson();
                    result2.HttpCode = 300;
                    result2.Message = "现金支付失败";
                    return new JsonResult() { Data = result2 };
                }
            }
            catch (Exception)
            {

            }
            ResultJson resultJson = new ResultJson();
            resultJson.HttpCode = 300;
            resultJson.Message = "现金支付失败";
            return new JsonResult() { Data = resultJson };
        }
    }
}