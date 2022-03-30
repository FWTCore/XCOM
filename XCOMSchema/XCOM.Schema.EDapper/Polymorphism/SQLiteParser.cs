using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.Model;

namespace XCOM.Schema.EDapper.Polymorphism
{
    internal class SQLiteParser : IParser
    {

        /// <summary>
        /// 默认是MsSqlServer
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public string MethodAnalysis(string methodName, bool isNegate = false)
        {
            var format = new StringBuilder();
            switch (methodName)//系统级
            {
                case "StartsWith":
                    {
                        format.Append("(Like({1} || '%', {0})");
                        break;
                    }
                case "EndsWith":
                    {
                        format.Append("(Like('%' || {1}, {0})");
                        break;
                    }
                case "Contains":
                    {
                        format.Append("(Like('%' || {1} || '%', {0})");
                        break;
                    }
                case "Equals":
                    {
                        format.Append("({0} = {1} ");
                        break;
                    }
                case "XMIn":
                    {
                        format.Append("({0} in {1})");
                        break;
                    }
                case "XMNotIn":
                    {
                        format.Append("({0} not in {1})");
                        break;
                    }
                case "XMNotLike":
                    {
                        format.Append("({0} not like '%'+{1}+'%')");
                        break;
                    }
                case "XMNotStartsLike":
                    {
                        format.Append("({0} not like {1}+'%')");
                        break;
                    }
                case "XMNotEndsLike":
                    {
                        format.Append("({0} not like '%'+{1})");
                        break;
                    }
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
            paramFormat = "@{0}";
            return operation switch
            {
                ConditionOperation.Equal => "{0}={1}",
                ConditionOperation.NotEqual => "{0}<>{1}",
                ConditionOperation.LessThan => "{0}<{1}",
                ConditionOperation.LessThanEqual => "{0}<={1}",
                ConditionOperation.MoreThan => "{0}>{1}",
                ConditionOperation.MoreThanEqual => "{0}>={1}",
                ConditionOperation.Like => "{0} LIKE '%{1}%'",
                ConditionOperation.LikeLeft => "{0} LIKE '{1}%'",
                ConditionOperation.LikeRight => "{0} LIKE '%{1}'",
                ConditionOperation.NotLike => "{0} NOT LIKE '%{1}%'",
                ConditionOperation.NotLikeLeft => "{0} NOT LIKE '{1}%'",
                ConditionOperation.NotLikeRight => "{0} NOT LIKE '%{1}'",
                ConditionOperation.In => "{0} IN {1}",
                ConditionOperation.NotIn => "{0} NOT IN {1}",
                _ => throw new Exception("ConditionOperation 无效")
            };
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string TemporaryTableAnalysis(string temporaryTableName, out string endScript)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ConvertFieldType(PropertyInfo prop, int length)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string DBSafeParameterData(string parameterValue)
        {
            throw new NotImplementedException();
        }
        public string SqlPagination(SqlPartModel sqlPart, int skipCount, int takeCount, bool isSql2008 = true)
        {
            throw new NotImplementedException();
        }

    }
}
