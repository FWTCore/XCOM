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
    internal class MsSqlServerParser : IParser
    {

        public string MethodAnalysis(string methodName, bool isNegate = false)
        {
            var format = new StringBuilder();
            switch (methodName)//系统级
            {
                case "StartsWith":
                    if (isNegate)
                        format.Append(" {0} not like {1}+'%' ");
                    else
                        format.Append(" {0} like {1}+'%' ");
                    break;
                case "EndsWith":
                    if (isNegate)
                        format.Append(" {0} not like '%'+{1} ");
                    else
                        format.Append(" {0} like '%'+{1} ");
                    break;
                case "Contains":
                    if (isNegate)
                        format.Append(" {0} not like '%'+{1}+'%' ");
                    else
                        format.Append(" {0} like '%'+{1}+'%' ");
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
        /// TODO
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
                ConditionOperation.Like => "{0} LIKE '%'+{1}+'%'",
                ConditionOperation.LikeLeft => "{0} LIKE {1}+'%'",
                ConditionOperation.LikeRight => "{0} LIKE '%'+{1}",
                ConditionOperation.NotLike => "{0} NOT LIKE '%'+{1}+'%'",
                ConditionOperation.NotLikeLeft => "{0} NOT LIKE {1}+'%'",
                ConditionOperation.NotLikeRight => "{0} NOT LIKE '%'+{1}",
                ConditionOperation.In => "{0} IN {1}",
                ConditionOperation.NotIn => "{0} NOT IN {1}",
                _ => throw new Exception("ConditionOperation 无效")
            };
        }

        public string TemporaryTableAnalysis(string temporaryTableName, out string endScript)
        {
            if (!temporaryTableName.Trim().StartsWith("#"))
            {
                throw new Exception("SQL Server的临时表名必须以#开头!");
            }

            StringBuilder script = new();
            script.AppendLine($"IF OBJECT_ID(N'Tempdb..{temporaryTableName}') IS NOT NULL")
                .AppendLine("BEGIN")
                .AppendLine($"\t Drop Table Tempdb..{temporaryTableName}")
                .AppendLine($"END");
            endScript = script.ToString();
            StringBuilder resultData = new(endScript);
            resultData.AppendFormat("CREATE TABLE {0}(temp_xmabc_id int IDENTITY(1,1) PRIMARY KEY", temporaryTableName);
            return resultData.ToString();
        }
        public string ConvertFieldType(PropertyInfo prop, int length)
        {
            if (prop.PropertyRealType().IsEnum)
            {
                return "int";
            }
            switch (prop.PropertyRealType().Name.ToLower())
            {
                case "int32":
                case "int16":
                case "int64":
                    return "int";
                case "decimal":
                case "single":
                case "double":
                    return "decimal(20,6)";
                case "datetime":
                    return "datetime";
                case "boolean":
                    return "bit";
                default:
                    if (length > 4000)
                    {
                        return "nvarchar(max)";
                    }
                    return "nvarchar(" + length + ")";
            }
        }
        public string DBSafeParameterData(string parameterValue)
        {
            return parameterValue.Replace("'", "''");
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
            if (isSql2008)
            {
                resultData.Append($"{sqlPart.Sql} offset {skipCount} row fetch next {takeCount} rows only");
            }
            else
            {
                resultData.Append($@"
                    SELECT {sqlPart.SqlSelectField} 
                    FROM ( 
                        SELECT {sqlPart.SqlSelectField},
                            row_number() over({sqlPart.SqlOrderBy}) AS 'RowNumber'
                        {sqlPart.SqlFromCondition} 
                    ) AS temp_table
                    WHERE RowNumber > {skipCount} and RowNumber <= {skipCount + takeCount}"
                    );

            }
            return resultData.ToString();
        }

    }
}
