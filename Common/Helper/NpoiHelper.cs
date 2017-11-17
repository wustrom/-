/******************************************************************* 
 * 版权所有：  
 * 类 名 称：ExcelHelper 
 * 作    者：zk 
 * 电子邮箱：  
 * 创建日期：2012/2/25 10:17:21  
 * 修改描述：从excel导入datatable时，可以导入日期类型。 
 *           但对excel中的日期类型有一定要求，要求至少是yyyy/mm/dd类型日期； *            
 * 修改描述：将datatable导入excel中，对类型为字符串的数字进行处理， 
 *           导出数字为double类型； 
 *  
 *  
 * *******************************************************************/
using System;
using System.Data;
using System.IO;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Common.Helper
{
    public static class ExcelHelper
    {
        public static string Exprot(string filePath)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(filePath);
            try
            {
                //HttpPostedFileBase FileBase = null;
                //var stream = FileBase.InputStream;
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {

                    if (fileExt == ".xls")
                    {
                        workbook = new HSSFWorkbook(file);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(file);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            finally
            {

            }
            return null;
        }

        public static string ExprotStream(HttpPostedFileBase stream)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(stream.FileName);
            try
            {
                if (fileExt == ".xls")
                {
                    workbook = new HSSFWorkbook(stream.InputStream);
                }
                else if (fileExt == ".xlsx")
                {
                    workbook = new XSSFWorkbook(stream.InputStream);
                }
                else
                {
                    return null;
                }
            }
            finally
            {

            }
            return null;
        }

        public static DataTable ExcelToDataTable(HttpPostedFileBase stream)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            string fileExt = Path.GetExtension(stream.FileName);
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                if (fileExt == ".xlsx") // 2007版本
                    workbook = new XSSFWorkbook(stream.InputStream);
                else if (fileExt == ".xls") // 2003版本
                    workbook = new HSSFWorkbook(stream.InputStream);
                sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}