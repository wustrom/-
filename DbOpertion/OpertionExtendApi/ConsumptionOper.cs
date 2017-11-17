using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using System.Data;
using DbOpertion.Models;
using System.Data.SqlClient;

namespace DbOpertion.Operation
{
    public partial class ConsumptionOper : SingleTon<ConsumptionOper>
    {
        /// <summary>
        /// ɸѡ���һ�����Ѽ�¼���ݸ����û�Id
        /// </summary>
        /// <param name="UserId">�û�Id</param>
        /// <returns>�����б�</returns>
        public Tuple<PayRecord, Store> SelectByUserIdLast(int UserId)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId);
            query.OrderBy(p => p.PayRecordId);
            var Record = query.GetQueryPageList(0, 1).FirstOrDefault();
            if (Record != null)
            {
                var queryStore = new LambdaQuery<Store>();
                queryStore.Where(p => p.StoreId == Record.StoreID);
                var store = queryStore.GetQueryPageList(0, 1).FirstOrDefault();
                return new Tuple<PayRecord, Store>(item1: Record, item2: store);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ҳɸѡ�û����ݸ���Id
        /// </summary>
        /// <param name="UserId">�û�Id</param>
        /// <returns>�����б�</returns>
        public Tuple<List<PayRecord>, List<Store>> SelectPageByUserId(int UserId, int start, int PageSize)
        {
            var query = new LambdaQuery<PayRecord>();
            query.Where(p => p.UserId == UserId);
            query.OrderBy(p => p.PayRecordId, true);
            var payRecordList = query.GetQueryPageList(start, PageSize);
            if (payRecordList != null)
            {
                var Ids = payRecordList.Where(p => p.StoreID != null).Select(p => p.StoreID.Value).Distinct().ToList();
                var queryStore = new LambdaQuery<Store>();
                queryStore.Where(p => p.StoreId.ContainsIn(Ids));
                var StoreList = queryStore.GetQueryList();
                return new Tuple<List<PayRecord>, List<Store>>(item1: payRecordList, item2: StoreList);
            }
            return null;
        }


        /// <summary>
        /// ɸѡ�û�Id�뿨ƬId
        /// </summary>
        ///  <param name="UserId">�û�Id</param>
        ///  <param name="CardId">��ƬId</param>
        /// <returns>�����б�</returns>
        public List<PayRecordInformation> SelectByUserAndCardId(int CardId)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            parmList.Add(new SqlParameter("@ElectronicTypeId", CardId));
            string sql = string.Format(@"SELECT a.ConsumptionRecordId,a.PayMoney,b.ShopTime,c.UserNickName,
                                                c.UserPhone,d.StoreName FROM ConsumptionRecord a
                                         LEFT JOIN PayRecord b on a.PayRecordId = b.PayRecordId
                                         LEFT JOIN TUser c on b.UserId = c.UserId
                                         LEFT JOIN Store d on d.StoreId= b.StoreID
                                         WHERE a.ElectronicId = @ElectronicId;");
            return SqlOpertion.Instance.GetQueryList<PayRecordInformation>(sql, parmList);
        }
    }
}
