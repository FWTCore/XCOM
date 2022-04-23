using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMConvert
    {
        /// <summary>
        /// 将图片保存为base64字符串
        /// </summary>
        /// <param name="imageFileName"></param>
        /// <returns></returns>
        public static string ImageConvertBase64String(string imageFileName)
        {
            using (Bitmap bmp = new Bitmap(imageFileName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    return Convert.ToBase64String(arr);
                }
            }

        }

        public static string ImageConvertBase64String(Bitmap bmp)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
        }

        /// <summary>
        /// 将base64字符串转换成图片
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void Base64StringConvertImage(string base64String, string filePath)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                throw new Exception("base64String参数不能为空");
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception("filePath参数不能为空");
            }
            base64String = base64String.Replace("data:image/jpg;base64,", "").Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                using (var img = Image.FromStream(memStream))
                {
                    img.Save(filePath, ImageFormat.Jpeg);
                }
            }
        }



    }
}
