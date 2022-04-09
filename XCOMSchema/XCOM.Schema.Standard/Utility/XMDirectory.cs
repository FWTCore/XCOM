using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMDirectory
    {
        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir">要创建的目录路径包括目录名</param>
        public static void CreateDirectory(string targetDir)
        {
            DirectoryInfo dir = new DirectoryInfo(targetDir);
            if (!dir.Exists)
                dir.Create();
        }

    }
}
