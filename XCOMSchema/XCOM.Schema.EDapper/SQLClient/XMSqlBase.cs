using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;
using XCOM.Schema.EDapper.Realization;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.System;

namespace XCOM.Schema.EDapper.SQLClient
{
    public class XMSqlBase : XMDisposableResource
    {

        /// <summary>
        /// 数据库连接配置对象
        /// </summary>
        internal DBConnection _dbConfig;

        /// <summary>
        /// 默认Tag
        /// </summary>
        protected readonly string _defaultConditionTag = "#STRWHERE#";

        /// <summary>
        /// 参数
        /// </summary>
        public DynamicParameters Parameters { get; set; } = new DynamicParameters();

        /// <summary>
        /// 执行脚本配置对象
        /// </summary>
        public SQLScript sqlNode;

        /// <summary>
        /// 执行脚本
        /// </summary>
        protected readonly StringBuilder _commandText = new();
        public string CommandText
        {
            get
            {
                return _commandText.ToString();
            }
        }

        /// <summary>
        /// 条件
        /// </summary>
        protected readonly StringBuilder _queryConditionString = new();
        public string QueryConditionString
        {
            get
            {
                return _queryConditionString.ToString();
            }
        }

        /// <summary>
        /// 收尾脚本
        /// </summary>
        protected readonly StringBuilder _endScriptString = new();

        public string EndScriptString
        {
            get
            {
                return _endScriptString.ToString();
            }

        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 释放资源
            }
        }
        #region 设置方法

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        public string SetParameter(string paramName, object value)
        {
            if (paramName.StartsWith("@"))
            {
                paramName = paramName.Remove(0, 1);
            }
            if (this.Parameters.ParameterNames.Any(e => e.Equals(paramName)))
            {
                paramName = $"{paramName}{this.Parameters.ParameterNames.Count()}";
            }
            this.Parameters.Add(paramName, ConvertDbValue(value));
            return $"@{paramName}";
        }
        public void SetParameter<T>(T entity) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (string propName in sqlNode.ParameterNameList)
            {
                if (Parameters.ParameterNames.Any(f => f.ToLower().Trim() == $"@{propName.ToLower().Trim()}"))
                {
                    continue;
                }
                foreach (PropertyInfo propertyInfo in properties)
                {
                    string paramName = $"@{propertyInfo.Name.ToLower()}";
                    if (paramName == propName.ToLower())
                    {
                        this.Parameters.Add(paramName, ConvertDbValue(propertyInfo.GetValue(entity, null)));
                    }
                }
            }
        }
        /// <summary>
        /// 设置安全参数
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        public string SetSafeParameter(string parameterValue)
        {
            var parser = XMRealization.GetPolymorphism(this._dbConfig.DBProviderType);
            return parser.DBSafeParameterData(parameterValue);
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="condOperation"></param>
        /// <param name="objValue"></param>
        /// <param name="isOperator">是否返回逻辑运算符，默认true</param>
        /// <returns></returns>
        public string SetQueryCondition(string fieldName, ConditionOperation condOperation, object objValue, bool isOperator = true)
        {
            if (objValue == null || string.IsNullOrWhiteSpace(objValue.ToString()))
            {
                return null;
            }
            var paramName = this.SetParameter($"@{fieldName}", objValue);
            var parser = XMRealization.GetPolymorphism(this._dbConfig.DBProviderType);
            var format = parser.ConditionOperationAnalysis(condOperation, out string paramFormat);
            _ = _queryConditionString.AppendLine($" AND {string.Format(format, fieldName, string.Format(paramFormat, paramName))}");

            if (isOperator)
            {
                return $" AND {string.Format(format, fieldName, string.Format(paramFormat, paramName))}";
            }
            else
            {
                return string.Format(format, fieldName, string.Format(paramFormat, paramName));
            }
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="customerQueryCondition"></param>
        public void SetQueryCondition(string customerQueryCondition)
        {
            if (!string.IsNullOrWhiteSpace(customerQueryCondition))
            {
                _queryConditionString.AppendLine(customerQueryCondition);
            }
        }
        /// <summary>
        /// 获取tag 标记sql
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public string GetSQLTag(string tagName)
        {
            string pattern = "\\/\\*" + tagName + "\\{\\[([\\s\\S]*?)\\]\\}\\*\\/";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(_commandText.ToString());
            string result = string.Empty;
            if (match.Length > 0)
            {
                result = match.Groups[1].Value.Trim();
            }
            return result;
        }
        /// <summary>
        /// 替换tag标记脚本
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="replacement"></param>
        public void ReplaceSQLTag(string tagName, string replacement)
        {
            string pattern = "\\/\\*" + tagName + "\\{\\[([\\s\\S]*?)\\]\\}\\*\\/";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);
            var tempSql = regex.Replace(_commandText.ToString(), replacement);
            _commandText.Clear().Append(tempSql);
        }
        /// <summary>
        /// 替换tag标记脚本
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="args"></param>
        public void ReplaceAndSetSQLTag(string tagName, params object[] args)
        {
            string pattern = "\\/\\*" + tagName + "\\{\\[([\\s\\S]*?)\\]\\}\\*\\/";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(_commandText.ToString());
            string format = string.Empty;
            if (match.Length > 0)
            {
                format = match.Groups[1].Value.Trim();
            }
            if (args != null || args.Count() > 0)
            {
                format = string.Format(format, args);
            }
            var tempSql = regex.Replace(_commandText.ToString(), format);
            _commandText.Clear().Append(tempSql);
        }
#pragma warning disable CA1822 // 将成员标记为 static
        public string GetOrTag()
#pragma warning restore CA1822 // 将成员标记为 static
        {
            return "OR ({0})";
        }
#pragma warning disable CA1822 // 将成员标记为 static
        public string GetAndTag()
#pragma warning restore CA1822 // 将成员标记为 static
        {
            return "AND ({0})";
        }

        public string GetTemporaryTableScript<T>(string tempTableName, List<T> dataList, int batchRowCount = 1000)
        {
            if (_dbConfig.DBProviderType != XMProviderType.MySql && _dbConfig.DBProviderType != XMProviderType.MsSqlServer)
            {
                throw new Exception("目前GetTemporaryTableScript方法只支持SQL Server和MySQL两种数据库创建临时表！");
            }
            if (dataList == null)
            {
                throw new Exception("dataList不能为Null!");
            }
            Type typeFromHandle = typeof(T);
            List<PropertyInfo> list = typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

            List<FieldDimension> fieldDimensions = new();
            StringBuilder insertContent = new();
            StringBuilder filedContent = new();
            StringBuilder valueContent = new();

            var fieldDataList = new List<string>();
            if (dataList.Count > 0)
            {
                fieldDataList.Clear();
                foreach (PropertyInfo item in list)
                {
                    fieldDataList.Add(item.Name);
                }
                filedContent.Append(string.Join(",", fieldDataList));
                for (int i = 0; i < dataList.Count; i++)
                {
                    T val = dataList[i];
                    valueContent.Append('(');
                    fieldDataList.Clear();
                    foreach (PropertyInfo prop in list)
                    {
                        FieldDimension fieldLength = fieldDimensions.Find(f => f.Name == prop.Name);
                        if (fieldLength == null)
                        {
                            fieldLength = new FieldDimension(prop.Name, 10);
                            fieldDimensions.Add(fieldLength);
                        }
                        object valueobj = prop.GetValue(val, null);
                        if (valueobj == null)
                        {
                            fieldDataList.Add("null");
                            continue;
                        }
                        string value = ConvertDbValueStr(valueobj);
                        if (fieldLength.Length < value.Length)
                        {
                            fieldLength.Length = value.Length;
                        }
                        fieldDataList.Add(value);
                    }
                    valueContent.Append(string.Join($",", fieldDataList))
                        .Append(')');
                    if ((i + 1) % batchRowCount == 0 || i + 1 == dataList.Count)
                    {
                        insertContent.AppendLine($"INSERT INTO {tempTableName} ({filedContent})")
                            .AppendLine("VALUES")
                            .AppendLine(valueContent.ToString())
                            .AppendLine(";");
                        valueContent.Clear();
                    }
                    else
                    {
                        valueContent.Append($",{Environment.NewLine}");
                    }
                }
            }

            var parser = XMRealization.GetPolymorphism(this._dbConfig.DBProviderType);
            StringBuilder resultData = new(parser.TemporaryTableAnalysis(tempTableName, out var endScript));
            if (!string.IsNullOrWhiteSpace(endScript))
            {
                this._endScriptString.AppendLine(endScript);
            }
            fieldDataList = new List<string>();
            foreach (PropertyInfo prop2 in list)
            {
                FieldDimension fieldLength2 = fieldDimensions.Find((f) => f.Name == prop2.Name);
                if (fieldLength2 == null)
                {
                    fieldLength2 = new FieldDimension(prop2.Name, 100);
                    fieldDimensions.Add(fieldLength2);
                }
                fieldDataList.Add($"{prop2.Name} {parser.ConvertFieldType(prop2, fieldLength2.Length)}");
            }
            resultData.Append(string.Join(',', fieldDataList));
            resultData.AppendLine(");");
            return $"{resultData}{insertContent}";
        }

        /// <summary>
        /// 构造脚本
        /// </summary>
        /// <returns></returns>
        public string GetExecuteScript()
        {
            var resultSql = this._commandText.ToString();
            this._commandText.Clear();
            if (this._queryConditionString.Length > 0)
            {
                this._commandText.Append(resultSql.Replace(this._defaultConditionTag, this._queryConditionString.ToString()));
            }
            else
            {
                this._commandText.Append(resultSql.Replace(this._defaultConditionTag, ""));
            }
            if (this.EndScriptString.Length > 0)
            {
                return $"{this._commandText} {this.EndScriptString}";
            }
            else
            {
                return this._commandText.ToString();
            }
        }
        #endregion


        #region 私有方法

        /// <summary>
        /// 解析值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object ConvertDbValue(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return DBNull.Value;
            }
            Type type = value.GetType();
            if (type.IsEnum || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments() != null && type.GetGenericArguments().Length == 1 && type.GetGenericArguments()[0].IsEnum))
            {
                int num = (int)value;
                return num;
            }
            if (type == typeof(bool))
            {
                if (value.ToString().ToUpper() == "TRUE")
                {
                    return 1;
                }
                return 0;
            }
            return value;
        }
        /// <summary>
        /// 转换数据值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertDbValueStr(object value)
        {
            object obj = ConvertDbValue(value);
            if (obj is DBNull)
            {
                return "NULL";
            }
            if (obj.GetType().IsPrimitive || obj is decimal)
            {
                return obj.ToString();
            }
            return $"'{this.SetSafeParameter(obj.ToString())}'";
        }
        /// <summary>
        /// 获取脚本执行超时时间
        /// </summary>
        /// <returns></returns>
        protected int GetTimeout()
        {
            return this.sqlNode.TimeOut <= 0 ? this._dbConfig.TimeOut : this.sqlNode.TimeOut;
        }
        #endregion

    }
}
