using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.EDapper.Model
{
    internal class BaseModel<T> where T : class, new()
    {

        /// <summary>
        /// 初始化默认参数
        /// </summary>
        /// <param name="context"></param>
        public BaseModel()
        {
            this.MappedProperties = GetModelType().GetMappedProperties().ToList();
            this.AllProperties = GetModelType().GetProperties().ToList();
            this.PrimaryKeys = GetModelType().GetKeyPropertity().ToList();
            this.Parameters = new DynamicParameters();
        }

        #region 字段

        /// <summary>
        /// 主键列表
        /// </summary>
        internal List<PropertyInfo> PrimaryKeys { get; set; }

        /// <summary>
        /// 返回当前 T 匹配的所有公共属性(对外隐藏)
        /// </summary>
        internal List<PropertyInfo> MappedProperties { get; set; }

        /// <summary>
        /// 返回当前 T 所有公共属性(对外隐藏)
        /// </summary>
        internal List<PropertyInfo> AllProperties { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public DynamicParameters Parameters { get; set; }

        #endregion

        /// <summary>
        /// 获取对象类型
        /// </summary>
        /// <returns></returns>
        internal static Type GetModelType()
        {
            return typeof(T);
        }
        /// <summary>
        /// 获取实体对应的表名
        /// </summary>
        /// <returns></returns>
        internal static string GetTableName()
        {
            return GetModelType().GetTableName();
        }

    }
}
