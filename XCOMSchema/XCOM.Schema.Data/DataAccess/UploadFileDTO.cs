using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Data.DataAccess
{
    public class UploadFileDTO
    {
        public UploadFileDTO()
        {
            ContentType = "application/octet-stream";
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// 文件内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 数据流
        /// </summary>
        public Stream Stream { get; set; }

    }
}
