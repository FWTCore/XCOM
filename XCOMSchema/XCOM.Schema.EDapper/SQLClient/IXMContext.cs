using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.Realization;

namespace XCOM.Schema.EDapper.SQLClient
{
    public interface IXMContext<T> where T : class, new()
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IXMQueryable<T> Query(string connectionKey);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        string Insert(string connectionKey, T t);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<string> InsertAsync(string connectionKey, T t);
        /// <summary>
        /// 插入列表数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(string connectionKey, List<T> t);
        /// <summary>
        /// 插入列表数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> InsertAsync(string connectionKey, List<T> t);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int Delete(string connectionKey, object keyValue);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string connectionKey, object keyValue);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Delete(string connectionKey, Expression<Func<T, bool>> expression);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string connectionKey, Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IXMUpdateable<T> Update(string connectionKey);

    }
}
