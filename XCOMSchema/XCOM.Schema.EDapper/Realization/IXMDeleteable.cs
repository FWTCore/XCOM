using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Realization
{
    /// <summary>
    /// 支持key和lambda表达式
    /// </summary>
    internal interface IXMDeleteable<T> where T : class, new()
    {
        /// <summary>
        /// 删除指定主键的实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        int Delete(object keyValue);
        /// <summary>
        /// 异步删除指定主键的实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Task<int> DeleteAsync(object keyValue);
        /// <summary>
        /// 删除lambda匹配的实体
        /// </summary>
        /// <param name="expression">lambda</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 异步删除lambda匹配的实体
        /// </summary>
        /// <param name="expression">lambda</param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<T, bool>> expression);



        string DebugSql();
    }
}
