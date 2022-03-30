using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.DataAnnotations;

namespace XCOM.Schema.Standard.Extension
{
    public static class XMType
    {
        /// <summary>
        /// 获取实体匹配的特性字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetMappedProperties(this Type obj)
        {
            return obj.GetProperties().Where(f => !Attribute.IsDefined(f, typeof(XMNotMappedAttribute))).ToArray();
        }

        /// <summary>
        /// 获取表主键列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetKeyPropertity(this Type obj)
        {
            return obj.GetProperties().Where(a => a.GetCustomAttribute<XMKeyAttribute>() != null).ToArray();
        }

        /// <summary> 
        /// 获取字段备注名
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo property)
        {
            var columnAttribute = property.GetCustomAttribute<XMColumnAttribute>();
            return columnAttribute == null ? property.Name : columnAttribute.Name;
        }

        /// <summary>
        /// 获取表备注名
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static string GetTableName(this Type typeInfo)
        {
            var tableAttribute = typeInfo.GetCustomAttribute<XMTableAttribute>();
            return tableAttribute == null ? typeInfo.Name : tableAttribute.Name;
        }

        /// <summary>
        /// 判断属性是否自定义字段
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsDefinedAttribute(this PropertyInfo property)
        {
            return property.IsDefined(typeof(XMDatabaseGeneratedAttribute));
        }
        /// <summary>
        /// 获取属性真实类型
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static Type PropertyRealType(this PropertyInfo prop)
        {
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return prop.PropertyType.GetGenericArguments()[0];
            }
            return prop.PropertyType;
        }

        #region 属性设置 取值 

        /// <summary>
        /// 实体获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static object GetProperty<T>(this T source, string Name) where T : class, new()
        {
            PropertyInfo[] propertys = source.GetType().GetProperties();// 获得此模型的公共属性
            PropertyInfo pi = propertys.Where(ex => ex.Name == Name).FirstOrDefault();
            if (pi != null)
            {
                return pi.GetValue(source, null);
            }
            return null;
        }

        /// <summary>
        /// 实体设置赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="Name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetProperty<T>(this T source, string Name, object value) where T : class, new()
        {
            PropertyInfo[] propertys = source.GetType().GetProperties();// 获得此模型的公共属性
            PropertyInfo pi = propertys.Where(ex => ex.Name == Name).FirstOrDefault();
            if (pi != null)
            {
                pi.SetValue(source, value, null);
            }
            return true;
        }


        #endregion

    }
}
