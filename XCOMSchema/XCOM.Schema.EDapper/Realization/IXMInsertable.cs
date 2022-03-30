using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Realization
{
    /// <summary>
    /// 直接插入对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IXMInsertable<T> where T : class, new()
    {
        /// <summary>
        /// 插入单个实体，返回主键
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Insert(T obj);
        /// <summary>
        /// 异步插入单个实体，返回主键
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<string> InsertAsync(T obj);
        /// <summary>
        /// 批量插入实体列表
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        int Insert(List<T> objs);
        /// <summary>
        /// 异步批量插入实体列表
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        Task<int> InsertAsync(List<T> objs);


        string DebugSql();

    }
}
