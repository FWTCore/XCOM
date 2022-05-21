using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Excel;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.Utility
{
    public class TestEpplus
    {

        [Theory(DisplayName = "QuerySql")]
        [InlineData("QuerySql")]
        public void Epplues(string sqlKey)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("资产编号", "NO");
            dict.Add("资产分类.代码", "CategoryCode");
            dict.Add("资产分类", "CategoryName");
            dict.Add("配置标准分类名称", "Name");
            var patch = "F:\\九龙公安局.xlsx";
            var data = XMEPPlus.LoadExcel<List<ModelList>>(patch, hasHeader: true,headerMapp: dict) ;

        }
    }


    public class ModelList
    {
        public string NO { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
    }



}
