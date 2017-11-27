using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOpertion.Models;
using DbOpertion.Operation;
using Common;
using Common.Helper;
using System.Configuration;
using Common.Result;

namespace DbOpertion.Cache
{
    public partial class Cache_QRCode : SingleTon<Cache_QRCode>
    {

        /// <summary>
        /// 新建订单并返回订单ID
        /// </summary>
        public int? CreateOrder(PayRecord record)
        {
            if (PayRecordOper.Instance.Insert(record))
            {
                return PayRecordOper.Instance.SelectByUserIdAndTime(record.UserId, record.ShopTime).PayRecordId;
            }
            return null;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        public bool DeleteOrder(int KeyId)
        {
            return PayRecordOper.Instance.DeleteById(KeyId);
        }

        /// <summary>
        /// 从卡里扣钱
        /// </summary>
        /// <param name="Card"></param>
        /// <param name="PayRecordId"></param>
        /// <param name="CouponId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Tuple<bool, decimal, decimal, string> PayFromCard(string Card, int PayRecordId, int CouponId, int UserId)
        {
            int Id = 0;
            if (int.TryParse(Card, out Id))
            {
                var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
                var connection = sqlHelper.GetConnection();
                var Transaction = sqlHelper.GetTransaction(connection);
                try
                {
                    var ElecCard = ElectronicCardOper.Instance.SelectById(Id, connection, Transaction);
                    var pay = PayRecordOper.Instance.SelectById(PayRecordId, connection, Transaction);
                    if (ElecCard != null && pay != null)
                    {
                        if (pay.NeedPayMoney > 0 && ElecCard.CaerMoney > 0)
                        {
                            //这次能够支付完成
                            if (ElecCard.CaerMoney > pay.NeedPayMoney)
                            {
                                ConsumptionRecord con = new ConsumptionRecord();
                                con.ElectronicId = ElecCard.ElectronicId;
                                con.PayRecordId = pay.PayRecordId;
                                con.PayMoney = pay.NeedPayMoney;
                                var result3 = ConsumptionRecordOper.Instance.Insert(con);
                                ElecCard.CaerMoney = ElecCard.CaerMoney - pay.NeedPayMoney;
                                var result2 = ElectronicCardOper.Instance.Update(ElecCard, connection, Transaction);
                                pay.NeedPayMoney = 0;
                                var result1 = PayRecordOper.Instance.Update(pay, connection, Transaction);
                                pay = PayRecordOper.Instance.SelectById(PayRecordId, connection, Transaction);
                                var userinfo = TUserOper.Instance.SelectById(UserId, connection, Transaction);
                                var Level = MemberShipLevelOper.Instance.SelectAll(null, true, connection, Transaction);
                                if (userinfo != null && Level.Count != 0)
                                {
                                    #region 判断是否升级，若是升级则加优惠卷
                                    var CurrentLevel1 = Level.Where(p => p.LevelMin <= userinfo.LevelScore && userinfo.LevelScore < (p.LevelMin + p.LevelMax)).FirstOrDefault();
                                    userinfo.ConsumptionTime++;
                                    userinfo.LevelScore++;
                                    var CurrentLevel2 = Level.Where(p => p.LevelMin <= userinfo.LevelScore && userinfo.LevelScore < (p.LevelMin + p.LevelMax)).FirstOrDefault();
                                    if (CurrentLevel1.NextLevelId == null)
                                    {
                                        userinfo.DiamondsMoney = userinfo.DiamondsMoney + pay.ShopMoney;
                                    }
                                    //升级更新用户优惠券
                                    if (CurrentLevel1 != CurrentLevel2)
                                    {
                                        if (!Cache_CouponLevelRelation.Instance.UpdateUserCoupon(CurrentLevel2.MembershipLevelId, userinfo.UserId, connection, Transaction))
                                        {
                                            return new Tuple<bool, decimal, decimal, string>(item1: false, item2: 0, item3: 0, item4: null);
                                        }
                                    }
                                    #endregion
                                    var result5 = TUserOper.Instance.Update(userinfo, connection, Transaction);
                                    if (CouponId == 0)
                                    {
                                        if (result1 && result2 && result3 && result5)
                                        {
                                            Transaction.Commit();
                                            connection.Close();
                                            return new Tuple<bool, decimal, decimal, string>(item1: true, item2: ElecCard.CaerMoney.Value, item3: 0, item4: ElecCard.CardName);
                                        }
                                    }
                                    var coupon = CouponUserRelationOper.Instance.SelectById(CouponId, connection, Transaction);
                                    if (coupon != null)
                                    {
                                        if (coupon.Forever != true)
                                        {
                                            coupon.IsUsed = true;
                                            var result4 = CouponUserRelationOper.Instance.Update(coupon, connection, Transaction);
                                            if (result1 && result2 && result3 && result5 && result4)
                                            {
                                                Transaction.Commit();
                                                connection.Close();
                                                return new Tuple<bool, decimal, decimal, string>(item1: true, item2: ElecCard.CaerMoney.Value, item3: 0, item4: ElecCard.CardName);
                                            }
                                        }
                                    }
                                }
                            }
                            //这次不能够支付完成
                            else
                            {
                                ConsumptionRecord con = new ConsumptionRecord();
                                con.ElectronicId = ElecCard.ElectronicId;
                                con.PayRecordId = pay.PayRecordId;
                                con.PayMoney = ElecCard.CaerMoney;
                                var result3 = ConsumptionRecordOper.Instance.Insert(con);
                                pay.NeedPayMoney = pay.NeedPayMoney - ElecCard.CaerMoney;
                                var result1 = PayRecordOper.Instance.Update(pay, connection, Transaction);
                                ElecCard.CaerMoney = 0;
                                var result2 = ElectronicCardOper.Instance.Update(ElecCard, connection, Transaction);
                                if (result1 && result2 && result3)
                                {
                                    Transaction.Commit();
                                    connection.Close();
                                    return new Tuple<bool, decimal, decimal, string>(item1: true, item2: 0, item3: pay.NeedPayMoney.Value, item4: ElecCard.CardName);
                                }
                            }
                        }
                    }
                    Transaction.Rollback();
                    connection.Close();
                    return new Tuple<bool, decimal, decimal, string>(item1: false, item2: 0, item3: 0, item4: null);
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                    connection.Close();
                }
            }
            return new Tuple<bool, decimal, decimal, string>(item1: false, item2: 0, item3: 0, item4: null);
        }

        /// <summary>
        /// 现金支付
        /// </summary>
        /// <param name="PayRecordId"></param>
        /// <param name="CouponId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool PayFromCash(int PayRecordId, int CouponId, int UserId)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var Transaction = sqlHelper.GetTransaction(connection);
            try
            {
                var pay = PayRecordOper.Instance.SelectById(PayRecordId, connection, Transaction);
                if (pay != null && pay.NeedPayMoney > 0)
                {

                    ConsumptionRecord con = new ConsumptionRecord();
                    con.ElectronicId = null;
                    con.PayRecordId = pay.PayRecordId;
                    con.PayMoney = pay.NeedPayMoney;
                    var result3 = ConsumptionRecordOper.Instance.Insert(con);
                    pay.NeedPayMoney = 0;
                    var result1 = PayRecordOper.Instance.Update(pay, connection, Transaction);
                    pay = PayRecordOper.Instance.SelectById(PayRecordId, connection, Transaction);
                    var userinfo = TUserOper.Instance.SelectById(UserId, connection, Transaction);
                    var Level = MemberShipLevelOper.Instance.SelectAll(null, true, connection, Transaction);
                    if (userinfo != null && Level.Count != 0)
                    {
                        #region 判断是否升级，若是升级则加优惠卷
                        var CurrentLevel1 = Level.Where(p => p.LevelMin <= userinfo.LevelScore && userinfo.LevelScore < (p.LevelMin + p.LevelMax)).FirstOrDefault();
                        userinfo.ConsumptionTime++;
                        userinfo.LevelScore++;
                        var CurrentLevel2 = Level.Where(p => p.LevelMin <= userinfo.LevelScore && userinfo.LevelScore < (p.LevelMin + p.LevelMax)).FirstOrDefault();
                        if (CurrentLevel1.NextLevelId == null)
                        {
                            userinfo.DiamondsMoney = userinfo.DiamondsMoney + pay.ShopMoney;
                        }
                        //升级更新用户优惠券
                        if (CurrentLevel1 != CurrentLevel2)
                        {
                            if (!Cache_CouponLevelRelation.Instance.UpdateUserCoupon(CurrentLevel2.MembershipLevelId, userinfo.UserId, connection, Transaction))
                            {
                                return false;
                            }
                        }
                        #endregion
                        var result5 = TUserOper.Instance.Update(userinfo, connection, Transaction);
                        if (CouponId == 0)
                        {
                            if (result1 && result3 && result5)
                            {
                                Transaction.Commit();
                                connection.Close();
                                return true;
                            }
                        }
                        var coupon = CouponUserRelationOper.Instance.SelectById(CouponId, connection, Transaction);
                        if (coupon != null)
                        {
                            if (coupon.Forever != true)
                            {
                                coupon.IsUsed = true;
                                var result4 = CouponUserRelationOper.Instance.Update(coupon, connection, Transaction);
                                if (result1 && result3 && result5 && result4)
                                {
                                    Transaction.Commit();
                                    connection.Close();
                                    return true;
                                }
                            }
                        }
                    }

                }
                Transaction.Rollback();
                connection.Close();
                return false;
            }
            catch (Exception)
            {
                Transaction.Rollback();
                connection.Close();
                return false;
            }
        }
    }
}
