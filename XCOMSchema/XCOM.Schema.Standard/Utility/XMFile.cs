using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMFile
    {
        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="Path"></param>
        public static void CreateFile(string filePath)
        {
            if (!IsExistFile(filePath))
                File.Create(filePath);
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath, string encoding = "utf-8")
        {
            if (!IsExistFile(filePath))
                return string.Empty;
            Encoding code = Encoding.GetEncoding(encoding);
            return File.ReadAllText(filePath, code);
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="contents"></param>
        /// <param name="append"></param>
        /// <param name="encoding"></param>
        public static void WriteFile(string filePath,
                                     string contents,
                                     bool append = false,
                                     string encoding = "utf-8")
        {
            Encoding code = Encoding.GetEncoding(encoding);
            if (append)
                File.AppendAllText(filePath, contents, code);
            else
                File.WriteAllText(filePath, contents, code);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="OrignFile">原始文件</param>
        /// <param name="NewFile">新文件路径</param>
        public static void CopyFile(string sourceFileName,
                                    string destFileName,
                                    bool overwrite = true)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原始路径</param>
        /// <param name="NewFile">新路径</param>
        public static void MoveFile(string OrignFile, string NewFile)
        {
            File.Move(OrignFile, NewFile);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        public static void DeleteFile(string Path)
        {
            File.Delete(Path);
        }

        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }
        /// <summary>
        /// 获取文件的名称（不包含扩展名）
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        /// <summary>
        /// 获取文件的名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }
        /// <summary>
        /// 获取文件目录
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string fileName)
        {
            return Path.GetDirectoryName(fileName);
        }


        /// <summary>
        /// 获取文件夹大小
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }

        /// <summary>
        /// 获取指定文件详细属性
        /// </summary>
        /// <param name="filePath">文件详细路径</param>
        /// <returns></returns>
        public static FileInfo GetFileAttibe(string filePath)
        {
            FileInfo objFI = new FileInfo(filePath);
            return objFI;
        }


        #region 文件流操作

        /// <summary>
        /// 读取文件为 byte[]
        /// </summary>
        /// <param name="path">文件路径:绝对路径</param>
        public static byte[] FileToBytes(string path)
        {
            if (!File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;
        }

        /// <summary>
        /// byte[] 保存为 文件
        /// </summary>
        /// <param name="bytes">二进制流</param>
        /// <param name="saveFile">文件路径:绝对路径</param>
        public static void BytesToFile(byte[] bytes, string saveFile)
        {
            if (File.Exists(saveFile))
            {
                File.Delete(saveFile);
            }
            var dir = Path.GetDirectoryName(saveFile);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            FileStream fs = new FileStream(saveFile, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes, 0, bytes.Length);
            bw.Close();
            fs.Close();
        }

        #endregion 文件流操作






    }
}
