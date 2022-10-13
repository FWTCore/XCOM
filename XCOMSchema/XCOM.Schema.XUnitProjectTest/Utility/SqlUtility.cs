using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.XUnitProjectTest.Utility
{
    public class SqlUtility
    {

        public static string Processing(string sqlScript)
        {
            var result = sqlScript.RegexReplace(@"\r|\n|\\s|\r| ", "");
            return result.ToLower().Trim();
        }


        public static object GetDapperParameter(DynamicParameters parameters)
        {
            var resultData = new Dictionary<string, object>();
            foreach (var item in parameters.ParameterNames)
            {
                var value = parameters.Get<object>(item);
                resultData.Add(item, value);
            }
            return resultData;
        }



    }
}
