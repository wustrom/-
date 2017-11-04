using Common;
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
    public partial class Cache_ElectronicType : SingleTon<Cache_ElectronicType>
    {
        /// <summary>
        /// 筛选全部电子储值卡信息
        /// </summary>
        public List<ElectronicType> SelectElectronicTypeCard(string searchKey, string Key, int PageNo, int PageSize, DataTablesOrderDir? desc)
        {
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
        /// 筛选全部会员类型卡数目
        /// </summary>
        public int SelectElectronicTypeCardCount(string SearchKey, string Key, DataTablesOrderDir? desc)
        {
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
        /// 添加电子储值卡
        /// </summary>
        /// <param name="EleType"></param>
        /// <returns></returns>
        public bool Insert_ElectronicType(ElectronicType EleType)
        {
            ResultJson jsonresult = new ResultJson();
            var list_Name = ElectronicTypeOper.Instance.SelectElectronicTypeByName(null);
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
    }
}
