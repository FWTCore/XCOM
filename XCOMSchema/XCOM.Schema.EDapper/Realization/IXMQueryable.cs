using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Data.DataAccess;
using XCOM.Schema.EDapper.DataAccess;

namespace XCOM.Schema.EDapper.Realization
{
    public interface IXMQueryable<T> where T : class, new()
    {
        #region Where

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IXMQueryable<T> Where(Expression<Func<T, bool>> expression);

        #endregion

        #region Skip Take

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IXMQueryable<T> Skip(int count);

        /// <summary>
        /// 取前N条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        IXMQueryable<T> Take(int count);

        #endregion

        #region 排序

        /// <summary>
        /// 正序
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IXMQueryable<T> OrderBy<T1>(Expression<Func<T, T1>> expression);

        /// <summary>
        /// 倒序
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IXMQueryable<T> OrderByDescending<T1>(Expression<Func<T, T1>> expression);

        /// <summary>
        /// 倒序
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IXMQueryable<T> OrderByDescending(string name);

        /// <summary>
        /// 正序
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IXMQueryable<T> OrderBy(string name);



        #endregion

        #region Count

        /// <summary>
        /// 总数
        /// </summary>
        /// <returns></returns>
        int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        #endregion

        #region First FirstOrDefault

        /// <summary>
        /// 查找一条实体  
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        T Find(object KeyValue);
        Task<T> FindAsync(object KeyValue);

        /// <summary>
        /// 获取默认地一条
        /// </summary>
        /// <returns></returns>
        T FirstOrDefault();
        Task<T> FirstOrDefaultAsync();
        T FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        #endregion

        #region ToList ToTopList ToPageList

        /// <summary>
        /// 提交数据库,返回IList集合
        /// </summary>
        /// <returns></returns>
        List<T> ToList();

        /// <summary>
        /// 提交数据库,返回IList集合
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> ToListAsync();

        /// <summary>
        /// 分页 默认第一页 15条
        /// </summary>
        /// <returns></returns>
        List<T> ToTopList();
        Task<IEnumerable<T>> ToTopListAsync();


        /// <summary>
        /// 提交数据库,返回IList集合
        /// </summary>
        /// <returns></returns>
        PageVOBase<T> ToPageList(PageROBase request);

        #endregion

        string DebugSql();

        DynamicParameters DebugParam();

    }
}
