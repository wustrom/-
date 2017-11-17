using Common;
using Common.Helper;
using Common.Result;
using DbOpertion.Models;
using DbOpertion.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Cache
{
   public partial class Cache_ElectronicType:SingleTon<Cache_ElectronicType>
    {
        /// <summary>
        /// 筛选全部电子储值卡信息
        /// </summary>
        /// <param name="searchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="PageNo">开始数据</param>
        /// <param name="PageSize">页面长度</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public List<ElectronicType> SelectElectronicTypeCard(string searchKey, string Key, int PageNo, int PageSize, DataTablesOrderDir? desc)
        {
                if (Key == "CardImage")
                {
                    Key = null;
                }
                bool order = false;
            
                if (desc == DataTablesOrderDir.Asc)
                {
                    order = false;
                }
                else if (desc == DataTablesOrderDir.Desc)
                {
                    order = true;
                }

            
            return ElectronicTypeOper.Instance.SelectByPage(searchKey, Key, PageNo, PageSize, order);
        }
        /// <summary>
        ///  筛选全部会员类型卡数目
        /// </summary>
        /// <param name="SearchKey">搜索关键字</param>
        /// <param name="Key">主键</param>
        /// <param name="desc">排序</param>
        /// <returns></returns>
        public int SelectElectronicTypeCardCount(string SearchKey, string Key, DataTablesOrderDir? desc)
        {
            if (Key == "CardImage")
            {
                Key = null;
            }
            bool order = false;
            if (desc == DataTablesOrderDir.Asc)
            {
                order = false;
            }
            else if (desc == DataTablesOrderDir.Desc)
            {
                order = true;
            }
            return ElectronicTypeOper.Instance.SelectCount(SearchKey, Key, order);
        }
        /// <summary>
        /// 添加电子储值类型卡
        /// </summary>
        /// <param name="EleType">电子储值类型卡信息</param>
        /// <returns></returns>
        public bool Insert_ElectronicType(ElectronicType EleType)
        {
            var list_Name =ElectronicTypeOper.Instance.SelectElectronicTypeByName(EleType.CardTypeName);
            if (list_Name.Count > 0)
            {
              
                return false;
            }
            else
            {
                var flag = ElectronicTypeOper.Instance.Insert(EleType);
                return flag;
            }
           
        }
        /// <summary>
        /// 多选删除数据
        /// </summary>
        /// <param name="KeyIds">储值卡类型Id</param>
        /// <returns></returns>
        public bool DeleteElectronicTypeCardByIds(List<int> KeyIds)
        {
            var flag = ElectronicTypeOper.Instance.DeleteByIds(KeyIds);
            return flag;
        }
        /// <summary>
        /// 获得储值金额根据储值卡类型Id
        /// </summary>
        /// <param name="ElectronicTypeId"></param>
        /// <returns></returns>
        public ElectronicType GetCardMoneyById(int ElectronicTypeId)
        {
            var sqlHelper = SqlHelper.GetSqlServerHelper("transaction");
            var connection = sqlHelper.GetConnection();
            var transaction = sqlHelper.GetTransaction(connection);
            return ElectronicTypeOper.Instance.GetCardMoneyById(ElectronicTypeId);
        }
    }
}
