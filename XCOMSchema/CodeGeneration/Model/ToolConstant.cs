using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Model
{
    public static class ToolConstant
    {
        #region 路径

        /// <summary>
        /// 系统操作的根文件夹
        /// </summary>
        public static readonly string PatchBaseFolder = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly string PatchEntityFile = "Entity";

        public static readonly string PatchIServiceFile = "IService";
        public static readonly string PatchServiceFile = "Service";

        public static readonly string PatchRepositoryFile = "Repository";
        public static readonly string PatchIRepositoryFile = "IRepository";


        #endregion

    }
}
