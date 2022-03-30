using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.DBClient;

namespace XCOM.Schema.EDapper.SQLClient
{
    public interface IXMSqlCommand : IXMSqlBase
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        string SetParameter(string paramName, object value);
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void SetParameter<T>(T entity) where T : class;
        /// <summary>
        /// 参数安全处理
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        string SetSafeParameter(string parameterValue);
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="condOperation"></param>
        /// <param name="objValue"></param>
        /// <param name="isOperator"></param>
        /// <returns></returns>
        string SetQueryCondition(string fieldName, ConditionOperation condOperation, object objValue, bool isOperator = true);

        /// <summary>
        /// 获取tag标签脚本
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        string GetSQLTag(string tagName);
        /// <summary>
        /// 替换tag标签脚本
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="replacement"></param>
        void ReplaceSQLTag(string tagName, string replacement);
        /// <summary>
        /// 替换tag标记脚本
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="args"></param>
        void ReplaceAndSetSQLTag(string tagName, params object[] args);
        /// <summary>
        /// 获取OR脚本模板
        /// </summary>
        /// <returns></returns>
        string GetOrTag();
        /// <summary>
        /// 获取AND脚本模板
        /// </summary>
        /// <returns></returns>
        string GetAndTag();
        /// <summary>
        /// 根据对象列表创建临时表脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempTableName"></param>
        /// <param name="dataList"></param>
        /// <param name="batchRowCount"></param>
        /// <returns></returns>
        string GetTemporaryTableScript<T>(string tempTableName, List<T> dataList, int batchRowCount = 1000);
        /// <summary>
        /// 获取执行脚本
        /// </summary>
        /// <returns></returns>
        string GetExecuteScript();

    }
}
