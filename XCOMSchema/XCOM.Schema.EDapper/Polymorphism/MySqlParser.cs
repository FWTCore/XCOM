using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Model;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.EDapper.Polymorphism
{
    internal class MySqlParser : IParser
    {
        public string MethodAnalysis(string methodName, bool isNegate = false)
        {
            var format = new StringBuilder();
            switch (methodName)//系统级
            {
                case "StartsWith":
                    if (isNegate)
                        format.Append(" {0} not like CONCAT({1},'%') ");
                    else
                        format.Append(" {0} like CONCAT({1},'%') ");
                    break;
                case "EndsWith":
                    if (isNegate)
                        format.Append(" {0} not like CONCAT('%',{1}) ");
                    else
                        format.Append(" {0} like CONCAT('%',{1}) ");
                    break;
                case "Contains":
                    if (isNegate)
                        format.Append(" {0} not like CONCAT('%',{1},'%') ");
                    else
                        format.Append(" {0} like CONCAT('%',{1},'%') ");
                    break;
                case "Equals":
                    if (isNegate)
                        format.Append(" {0}<>{1} ");
                    else
                        format.Append(" {0}={1} ");
                    break;
                case "XMIn":
                    if (isNegate)
                        format.Append(" {0} not in {1} ");
                    else
                        format.Append(" {0} in {1} ");
                    break;
                case "IsNullOrEmpty":
                case "IsNullOrWhiteSpace":
                    if (isNegate)
                        format.Append(" {0} is not null ");
                    else
                        format.Append(" {0} is null ");
                    break;
                default:
                    return null;
            }

            return format.ToString();
        }

        /// <summary>
        /// TODO TEST
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public string ConditionOperationAnalysis(ConditionOperation operation, out string paramFormat)
        {
            paramFormat = "{0}";
            return operation switch
            {
                ConditionOperation.Equal => "{0}={1}",
                ConditionOperation.NotEqual => "{0}<>{1}",
                ConditionOperation.LessThan => "{0}<{1}",
                ConditionOperation.LessThanEqual => "{0}<={1}",
                ConditionOperation.MoreThan => "{0}>{1}",
                ConditionOperation.MoreThanEqual => "{0}>={1}",
                ConditionOperation.Like => "{0} LIKE CONCAT('%',{1},'%')",
                ConditionOperation.LikeLeft => "{0} LIKE CONCAT({1},'%')",
                ConditionOperation.LikeRight => "{0} LIKE CONCAT('%',{1})",
                ConditionOperation.NotLike => "{0} NOT LIKE CONCAT('%',{1},'%')",
                ConditionOperation.NotLikeLeft => "{0} NOT LIKE CONCAT({1},'%')",
                ConditionOperation.NotLikeRight => "{0} NOT LIKE CONCAT('%',{1})",
                ConditionOperation.In => "{0} IN {1}",
                ConditionOperation.NotIn => "{0} NOT IN {1}",
                _ => throw new Exception("ConditionOperation 无效")
            };
        }
        /// <summary>
        /// 生成临时表SQL脚本
        /// </summary>
        /// <param name="temporaryTableName"></param>
        /// <param name="endScript"></param>
        /// <returns></returns>
        public string TemporaryTableAnalysis(string temporaryTableName, out string endScript)
        {
            StringBuilder resultData = new();
            endScript = $"DROP TEMPORARY TABLE IF EXISTS {temporaryTableName};";
            resultData.AppendLine(endScript)
                .AppendLine($"CREATE TEMPORARY TABLE {temporaryTableName}(")
                .AppendLine("temp_xmabc_id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,");
            return resultData.ToString();
        }
        public string ConvertFieldType(PropertyInfo prop, int length)
        {
            StringBuilder empty = new();
            if (prop.PropertyRealType().IsEnum)
            {
                empty.Append("int");
            }
            else
            {
                switch (prop.PropertyRealType().Name.ToLower())
                {
                    case "int32":
                    case "int16":
                    case "int64":
                        empty.Append("int");
                        break;
                    case "decimal":
                    case "single":
                    case "double":
                        empty.Append("decimal(20,6)");
                        break;
                    case "datetime":
                        empty.Append("timestamp(3)");
                        break;
                    case "boolean":
                        empty.Append("int");
                        break;
                    default:
                        empty.Append(((length <= 65535) ? ((length <= 1000) ? ("varchar(" + length + ")") : "text") : "longtext"));
                        break;
                }
            }
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                empty.Append(" null");
            }
            return empty.ToString();
        }
        public string DBSafeParameterData(string parameterValue)
        {
            return parameterValue.Replace("\\", "\\\\").Replace("'", "\\'").Replace(";", "\\;");
        }

        /// <summary>
        /// 得到分页sql
        /// </summary>
        /// <param name="sqlPart"></param>
        /// <param name="skipCount"></param>
        /// <param name="takeCount"></param>
        /// <param name="isSql2008"></param>
        /// <returns></returns>
        public string SqlPagination(SqlPartModel sqlPart, int skipCount, int takeCount, bool isSql2008 = true)
        {
            if (string.IsNullOrWhiteSpace(sqlPart.Sql) || string.IsNullOrWhiteSpace(sqlPart.SqlCount)
                || string.IsNullOrWhiteSpace(sqlPart.SqlSelectField) || string.IsNullOrWhiteSpace(sqlPart.SqlFromCondition)
                || string.IsNullOrWhiteSpace(sqlPart.SqlOrderBy))
            {
                throw new Exception("sqlPart存在为空的脚本");
            }
            var resultData = new StringBuilder();
            if (skipCount == 0)
            {
                resultData.Append($"{sqlPart.Sql} LIMIT {takeCount}");
            }
            else
            {
                resultData.Append($"{sqlPart.Sql} LIMIT {skipCount},{takeCount}");
            }
            return resultData.ToString();
        }

    }
}
