using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Data.DataAccess
{
    /// <summary>
    /// 通用分页返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponseBase<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 返回
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public long PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPages { get; set; }
    }
}
