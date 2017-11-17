using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    /// <summary>
    /// 编码Helper
    /// </summary>
    public class HtmlCodeHelper
    {
        /// <summary>    
        /// Html里面的引号转换    
        /// </summary>    
        /// <param name="srcText">Html代码</param>    
        /// <returns></returns>    
        public static string HtmlToString(string srcText)
        {
            srcText = srcText.Replace("Single_quotation&marks", "'");
            srcText = srcText.Replace("Double_quotation&marks", "\"");
            srcText = srcText.Replace("BackSlash&marks", "\\");
            return srcText;
        }
    }
}
