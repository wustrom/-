using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.DataTable
{
    /// <summary>
    /// 使用cardId的DataTable
    /// </summary>
    public class CardDataTableRequest: DataTableRequest
    {
        /// <summary>
        /// 卡号ID
        /// </summary>
        public int MemberShipTypeId { get; set; }


        /// <summary>
        ///  搜索激活日期(开始时间)
        /// </summary>
        public string ReleaseDate1 { get; set; }

        /// <summary>
        ///  搜索激活日期(结束时间)
        /// </summary>
        public string ReleaseDate2 { get; set; }

        /// <summary>
        ///  搜索生成日期(开始时间)
        /// </summary>
        public string CreateDate1 { get; set; }

        /// <summary>
        ///  搜索生成日期(结束时间)
        /// </summary>
        public string CreateDate2 { get; set; }
    }
}