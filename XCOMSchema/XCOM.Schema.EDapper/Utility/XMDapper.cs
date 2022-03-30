using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Utility
{
    internal static class XMDapper
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object GetDapperParameter(this object parameters)
        {
            if (parameters is DynamicParameters)
            {
                var parameterDatas = parameters as DynamicParameters;
                var resultData = new Dictionary<string, object>();
                foreach (var item in parameterDatas.ParameterNames)
                {
                    var value = parameterDatas.Get<object>(item);
                    resultData.Add(item, value);
                }
                return resultData;
            }
            else
            {
                return parameters;
            }

        }



    }
}
