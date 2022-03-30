using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMReflection
    {
        #region 动态加载程序集

        /// <summary>
        /// 动态加载程序集,参数类型是泛型 [T]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="fullClassName">全名称 例如: PFT.Common.MsSqlServer.Queryable`1</param>
        /// <param name="par">参数</param>
        /// <returns></returns>
        public static object GetReflectionObj<T>(string assemblyName, string fullClassName, object par = null) where T : class, new()
        {
            if (par != null)
            {
                Type objType = Assembly.Load(assemblyName).GetType(fullClassName);
                Type type = objType.MakeGenericType(typeof(T));
                var result = Activator.CreateInstance(type, new object[] { par });
                return result;
            }
            else
            {
                var result = Assembly.Load(assemblyName).CreateInstance(fullClassName);
                return result;
            }
        }

        /// <summary>
        /// 动态加载程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="fullClassName">全名称 例如: PFT.Common.MsSqlServer.Queryable</param>
        /// <param name="par">参数</param>
        /// <returns></returns>
        public static object GetReflectionObj(string assemblyName, string fullClassName, object par = null)
        {
            if (par != null)
            {
                Type objType = Assembly.Load(assemblyName).GetType(fullClassName);
                object result = Activator.CreateInstance(objType, new object[] { par });//创建实例
                return result;
            }
            else
            {
                var result = Assembly.Load(assemblyName).CreateInstance(fullClassName);
                return result;
            }
        }

        /// <summary>
        /// 动态加载程序集类型(静态)
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static Type GetStaticReflectionObj(string assemblyName, string fullClassName)
        {
            return Assembly.Load(assemblyName).GetType(fullClassName);
        }

        /// <summary>
        /// 动态加载方法元数据(静态)
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="fullFunctionName"></param>
        /// <returns></returns>
        public static MethodInfo GetStaticMeberInfo(string assemblyName, string fullFunctionName)
        {
            var fullNames = fullFunctionName.Split('.').ToList();
            var functionName = fullNames.Last();
            fullNames.Remove(functionName);
            return GetStaticReflectionObj(assemblyName, string.Join(".", fullNames)).GetMethod(functionName);
        }
        /// <summary>
        /// 动态加载方法元数据(静态)
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="fullFunctionName"></param>
        /// <returns></returns>
        public static MethodInfo GetStaticMeberInfo(string assemblyName, string fullClassName, string functionName)
        {
            return GetStaticReflectionObj(assemblyName, fullClassName).GetMethod(functionName);
        }

        #endregion

    }
}

