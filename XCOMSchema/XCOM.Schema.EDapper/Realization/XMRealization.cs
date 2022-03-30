using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Model;
using XCOM.Schema.EDapper.Polymorphism;
using XCOM.Schema.Standard.Cache;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.EDapper.Realization
{
    internal static class XMRealization
    {
        private static readonly string assemblyName = "XCOM.Schema.EDapper";

        public static XMQueryModel<T> GetQueryEntity<T>(XMProviderType dbType) where T : class, new()
        {
            string fullTypeName = $"{assemblyName}.Model.{dbType}.XMQueryModel`1";
            Type objType = Assembly.Load(assemblyName).GetType(fullTypeName);
            Type type = objType.MakeGenericType(typeof(T));
            return (XMQueryModel<T>)Activator.CreateInstance(type, null);
        }

        public static XMInsertModel<T> GetInsertEntity<T>(XMProviderType dbType) where T : class, new()
        {
            string fullTypeName = $"{assemblyName}.Model.{dbType}.XMInsertModel`1";
            Type objType = Assembly.Load(assemblyName).GetType(fullTypeName);
            Type type = objType.MakeGenericType(typeof(T));
            return (XMInsertModel<T>)Activator.CreateInstance(type, null);
        }

        public static XMDeleteModel<T> GetDeleteEntity<T>(XMProviderType dbType) where T : class, new()
        {
            string fullTypeName = $"{assemblyName}.Model.{dbType}.XMDeleteModel`1";
            Type objType = Assembly.Load(assemblyName).GetType(fullTypeName);
            Type type = objType.MakeGenericType(typeof(T));
            return (XMDeleteModel<T>)Activator.CreateInstance(type, null);
        }

        /// <summary>
        /// 获取sql语句多态对象
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static IParser GetPolymorphism(XMProviderType dbType)
        {
            var fullClassName = $"{assemblyName}.Polymorphism.{dbType}Parser";
            if (XMCacheObject.Instance[fullClassName] == null)
            {
                XMCacheObject.Instance[fullClassName] = XMReflection.GetReflectionObj(assemblyName, fullClassName);
            }
            return (IParser)XMCacheObject.Instance[fullClassName];
        }


    }
}
