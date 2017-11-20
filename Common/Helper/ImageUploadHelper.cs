using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Helper
{
    public class ImageUploadHelper : SingleTon<ImageUploadHelper>
    {

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public Dictionary<string, string> ReadAsMultipart(HttpContent Content)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string root = HttpContext.Current.Server.MapPath("~/Content/UserImage");//指定要将文件存入的服务器物理位置
            dic.Add("path", root);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);//创建该文件
            }
            return dic;
        }

        //解析base64编码获取图片
        public Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }

        /// <summary>
        /// 无损压缩图片    
        /// <param name="sFile">原图片</param>    
        /// <param name="dFile">压缩后保存位置</param>    
        /// <param name="dHeight">高度</param>    
        /// <param name="dWidth"></param>    
        /// <param name="flag">压缩质量(数字越小压缩率越高) 1-100</param>    
        /// <returns></returns>    

        public bool YaSuo(Image iSource, string outPath, int flag)
        {
            ImageFormat tFormat = iSource.RawFormat;
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageDecoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                    iSource.Save(outPath, jpegICIinfo, ep);
                else
                    iSource.Save(outPath, tFormat);
                return true;
            }
            catch
            {
                return false;
            }
            iSource.Dispose();
        }
    }
}
