using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Realization
{
    public interface IXMUpdateable<T> where T : class, new()
    {
        #region Where

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IXMUpdate<T> Where(Expression<Func<T, bool>> expression);

        #endregion

        #region Set

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IXMUpdateable<T> Set(Expression<Func<T, T>> expression);

        #endregion
    }

    public interface IXMUpdate<T> where T : class, new()
    {
        /// <summary>
        /// 更新方法
        /// </summary>
        /// <returns></returns>
        int Update();

        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<int> UpdateAsync();


        string DebugSql();

        DynamicParameters DebugParam();
    }
}
