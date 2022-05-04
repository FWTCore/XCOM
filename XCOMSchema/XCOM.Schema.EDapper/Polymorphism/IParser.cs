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
    internal interface IParser
    {
        /// <summary>
        /// 函数方法翻译成sql
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="isNegate"></param>
        /// <returns></returns>
        string MethodAnalysis(string methodName, bool isNegate = false);
        /// <summary>
        /// 操作符翻译成sql模板
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="paramFormat"></param>
        /// <returns></returns>
        string ConditionOperationAnalysis(ConditionOperation operation, out string paramFormat);
        /// <summary>
        /// 生成临时表SQL脚本
        /// </summary>
        /// <param name="temporaryTableName"></param>
        /// <param name="endScript"></param>
        /// <returns></returns>
        string TemporaryTableAnalysis(string temporaryTableName, out string endScript);
        /// <summary>
        /// 解析字段类型
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        string ConvertFieldType(PropertyInfo prop, int length);
        /// <summary>
        /// 安全化处理参数
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        string DBSafeParameterData(string parameterValue);

        /// <summary>
        /// 得到分页的sql
        /// </summary>
        /// <param name="sqlPart"></param>
        /// <param name="skipCount"></param>
        /// <param name="takeCount"></param>
        /// <param name="isSql2008"></param>
        /// <returns></returns>
        string SqlPagination(SqlPartModel sqlPart, int skipCount, int takeCount, bool isSql2008 = true);

        /// <summary>
        /// 数据库函数解析
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        string FunctionAnalysis(string methodName, string parameterName);

    }
}
