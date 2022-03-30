using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Realization;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.EDapper.SQLClient
{
    public class XMContext<T> : IXMContext<T> where T : class, new()
    {

        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        protected DBConnection _dbConfig;

        public int Delete(string connectionKey, object keyValue)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMDeleteable<T>)GetReflectionObj("XMDeleteable");
            return result.Delete(keyValue);
        }

        public int Delete(string connectionKey, Expression<Func<T, bool>> expression)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMDeleteable<T>)GetReflectionObj("XMDeleteable");
            return result.Delete(expression);
        }

        public async Task<int> DeleteAsync(string connectionKey, object keyValue)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMDeleteable<T>)GetReflectionObj("XMDeleteable");
            return await result.DeleteAsync(keyValue);
        }

        public async Task<int> DeleteAsync(string connectionKey, Expression<Func<T, bool>> expression)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMDeleteable<T>)GetReflectionObj("XMDeleteable");
            return await result.DeleteAsync(expression);
        }

        public string Insert(string connectionKey, T t)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMInsertable<T>)GetReflectionObj("XMInsertable");
            return result.Insert(t);
        }

        public int Insert(string connectionKey, List<T> t)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMInsertable<T>)GetReflectionObj("XMInsertable");
            return result.Insert(t);
        }

        public async Task<string> InsertAsync(string connectionKey, T t)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMInsertable<T>)GetReflectionObj("XMInsertable");
            return await result.InsertAsync(t);
        }

        public async Task<int> InsertAsync(string connectionKey, List<T> t)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMInsertable<T>)GetReflectionObj("XMInsertable");
            return await result.InsertAsync(t);
        }

        public IXMQueryable<T> Query(string connectionKey)
        {
            StructureDBConnection(connectionKey);
            var result = (IXMQueryable<T>)GetReflectionObj("XMQueryable");
            return result;
        }

        #region 获取反射实例化对象
        /// <summary>
        /// 构造数据库连接
        /// </summary>
        /// <param name="connectionKey"></param>
        private void StructureDBConnection(string connectionKey)
        {
            if (string.IsNullOrWhiteSpace(connectionKey))
            {
                throw new Exception("connectionKey 不能为空");
            }
            _dbConfig = XMDBConfig.ConfigSetting.DBConnectionList.FirstOrDefault(f => f.Key.Trim().ToUpper() == connectionKey.Trim().ToUpper());
            if (_dbConfig == null)
            {
                throw new Exception($"connectionKey:{connectionKey} 数据库配置无效!");
            }
        }


        /// <summary>
        /// 根据类名称 获取反射实例化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private object GetReflectionObj(string name)
        {
            string assemblyName = "XCOM.Schema.EDapper";
            string fullClassName = $"{assemblyName}.Realization.{this._dbConfig.DBProviderType}.{name}`1";
            var result = XMReflection.GetReflectionObj<T>(assemblyName, fullClassName, this._dbConfig);
            return result;
        }

        #endregion
    }
}
