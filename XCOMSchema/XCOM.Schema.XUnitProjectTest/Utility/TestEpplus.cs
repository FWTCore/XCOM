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
        public void QuerySql(string sqlKey)
        {

            XMEPPlus.LoadExcel<>

        }
    }


    public class ModelList
    {
        public string NO { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }



}
