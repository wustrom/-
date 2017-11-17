using Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.LambdaOpertion;
using Common.Extend;
using DbOpertion.Models;
using System.Data.SqlClient;
using Common.Helper;
using System.Data;

namespace DbOpertion.Operation
{
    public partial class ElectronicCardOper : SingleTon<ElectronicCardOper>
    {
        public string linq = "linq";
        /// <summary>
        /// ��ʾ���Ӵ�ֵ���б�
        /// </summary>
        /// <param name="ElectronicTypeId">���Ӵ�ֵ������Id</param>
        /// <param name="SearchKey">�����ؼ���</param>
        /// <param name="Key">����</param>
        /// <param name="start">��ʼ����</param>
        /// <param name="PageSize">ҳ�泤��</param>
        /// <param name="desc">����</param>
        /// <returns></returns>
        public List<ElectronicCard> Get_ElectronicCardByTypeId(int ElectronicTypeId, string SearchKey, string Key, int start, int PageSize, bool desc = true)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.ElectronicTypeId == ElectronicTypeId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey) || p.ElectronicTypeId.Contains(SearchKey) || p.CaerMoney.Contains(SearchKey) || p.CardCreateDate.Contains(SearchKey) || p.CardExpirationDay.Contains(SearchKey));
            }
            if (Key != null)
            {
                query.OrderByKey(Key, desc);
            }
            return query.GetQueryPageList(start, PageSize);
        }

        /// <summary>
        ///  ��������
        /// </summary>
        /// <param name="ElectronicTypeId">���Ӵ�ֵ������Id</param>
        /// <param name="SearchKey">�����ؼ���</param>
        /// <returns></returns>
        public int SelectSearchCount(int ElectronicTypeId, string SearchKey)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.ElectronicTypeId == ElectronicTypeId);
            if (!SearchKey.IsNullOrEmpty())
            {
                query.Where(p => p.CardName.Contains(SearchKey) || p.ElectronicTypeId.Contains(SearchKey) || p.CaerMoney.Contains(SearchKey) || p.CardCreateDate.Contains(SearchKey) || p.CardExpirationDay.Contains(SearchKey));
            }

            return query.GetQueryCount();
        }
        /// <summary>
        /// ��ӵ��Ӵ�ֵ��ʱɸѡ�ظ��Ŀ���
        /// </summary>
        /// <param name="ElectronicTypeId">���Ӵ�ֵ������Id</param>
        /// <param name="CardName">����</param>
        /// <returns></returns>
        public List<ElectronicCard> SelectElectronicCardByName(string CardName)
        {
            var query = new LambdaQuery<ElectronicCard>();
            query.Where(p => p.CardName == CardName);
            return query.GetQueryList();
        }
        /// <summary>
        /// ���ݿ�������Id�Ϳ�Id��ѯ��Ӧ�İ���
        /// </summary>
        /// <param name="ElectronicTypeId">������Id</param>
        /// <param name="ElectronicId">��Id</param>
        /// <returns></returns>
        public List<ElectronicCardUserInfo> SelectByUserNickName(int ElectronicTypeId, int ElectronicId)
        {
            List<SqlParameter> parmList = new List<SqlParameter>();
            parmList.Add(new SqlParameter("@ElectronicTypeId", ElectronicTypeId));
            parmList.Add(new SqlParameter("@ElectronicId", ElectronicId));
            string sql = string.Format(@"select c.UserNickName,c.UserId,d.ElectronicId,d.ElectronicTypeId from TUser c left join (	select b.UserId,a.ElectronicTypeId,a.ElectronicId
                                         from ElectronicCard a left join ElectronicCardRelation b on a.ElectronicId = b.ElectronicCardId)d 
                                         on c.UserId=d.UserId where ElectronicTypeId =@ElectronicTypeId and ElectronicId=@ElectronicId");
            return SqlOpertion.Instance.GetQueryList<ElectronicCardUserInfo>(sql, parmList);

        }

        /// <summary>
        /// ��ѯ���Ӵ�ֵ���б�������Ϣ
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<string> SelectExcelByName(IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var query = new LambdaQuery<ElectronicCard>();
            return query.GetQueryList(connection, transaction).Select(p => p.CardName).ToList();
        }
        /// <summary>
        /// ����Idɾ�����Ӵ�ֵ��
        /// </summary>
        /// <param name="ElectronicId">���Ӵ�ֵ��Id</param>
        /// <returns></returns>
        public bool DeleteElectronicCardById(int ElectronicId)
        {
            var delete = new LambdaDelete<ElectronicCard>();
            delete.Where(p => p.ElectronicId == ElectronicId);
            return delete.GetDeleteResult();
        }

        /// <summary>
        /// ����Idɾ����������
        /// </summary>
        /// <param name="KeyId">��ѡ�����Id</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteByIds(List<int> KeyId, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var delete = new LambdaDelete<ElectronicCard>();
            delete.Where(p => p.ElectronicId.ContainsIn(KeyId));
            return delete.GetDeleteResult();
        }
    }
}
