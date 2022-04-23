using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Data.DataAccess;

namespace XCOM.Schema.Standard.Utility
{
    public static class XMEnum
    {
        #region 枚举静态方法
        /// <summary>
        /// 字符串和枚举类型的转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T EnumFieldNameParse<T>(string name) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), name);
            }
            catch (Exception ex)
            {
                throw new Exception("传入的值与枚举值不匹配", ex);
            }
        }

        /// <summary>
        /// 对(位域)枚举值进行或运算
        /// [System.Flags]标记1<<1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emobjects"></param>
        /// <returns></returns>
        public static T MergerEnum<T>(params int[] emobjects) where T : Enum
        {
            try
            {
                var flags = typeof(T).GetCustomAttribute<FlagsAttribute>();
                if (flags == null)
                    throw new Exception($"枚举{typeof(T).Name}不行进行位域运算");
                var ret = 0;
                foreach (var item in emobjects)
                {
                    ret |= item;
                }
                return (T)Enum.ToObject(typeof(T), ret);
            }
            catch (Exception ex)
            {
                throw new Exception("合并(位域)枚举值异常", ex);
            }
        }

        /// <summary>
        /// 对(位域)枚举值进行拆分
        /// [System.Flags]标记1<<1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="emobject"></param>
        /// <returns></returns>
        public static List<T> SeparateEnum<T>(int emobject) where T : Enum, new()
        {
            try
            {
                var flags = typeof(T).GetCustomAttribute<FlagsAttribute>();
                if (flags == null)
                    throw new Exception($"枚举{typeof(T).Name}不行进行位域运算");
                var ret = new List<T>();
                foreach (int code in Enum.GetValues(typeof(T)))
                {
                    if ((code & emobject) == code)
                    {
                        ret.Add((T)Enum.ToObject(typeof(T), code));
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("拆分(位域)枚举值异常", ex);
            }

        }

        /// <summary>
        /// 返回枚举对应属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumTypeDTO> EnumToList<T>()
        {
            List<EnumTypeDTO> list = new List<EnumTypeDTO>();

            var items = Enum.GetValues(typeof(T));
            foreach (var item in items)
            {
                var entity = new EnumTypeDTO();
                object[] obj = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (obj != null && obj.Length > 0)
                {
                    DescriptionAttribute da = obj[0] as DescriptionAttribute;
                    entity.Desction = da.Description;
                }

                entity.Value = Convert.ToInt32(item);
                entity.Name = item.ToString();
                list.Add(entity);
            }
            return list;
        }

        #endregion


        #region 枚举扩展方法

        /// <summary>
        /// 获取枚举字段描述
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetEnumFieldDescription(this Enum em)
        {
            try
            {
                Type type = em.GetType();
                FieldInfo fd = type.GetField(em.ToString());
                if (fd == null)
                    return string.Empty;
                object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string fieldDescription = string.Empty;
                foreach (DescriptionAttribute attr in attrs)
                {
                    fieldDescription = attr.Description;
                }
                return fieldDescription;
            }
            catch (Exception ex)
            {
                throw new Exception("获取枚举字段描述异常", ex);
            }
        }

        #endregion


    }
}
