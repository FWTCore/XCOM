using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.LTS;
using XCOM.Schema.Standard.DataAnnotations;
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

        [Theory(DisplayName = "QuerySql")]
        [InlineData("QuerySql")]
        public void tt(string sqlKey)
        {
            VisitCondition<ModelList>(e => new ModelList
            {
                NO = "1"
                ,
                Id = ValueUtility.GetInt()
                ,
                CategoryCode = ValueUtility.GetString()
                ,
                InDate = ValueUtility.GetDateTime()
                ,
                GId = ValueUtility.GetGuidConvert()
                ,
                CategoryName = null
            });

        }


        private void VisitCondition<T>(Expression<Func<T, T>> expression)
        {
            if (expression != null)
            {
                Expression exp = expression.Body;
                var obj = new XMLambda(new DBConnection());
                var resultSql = obj.VisitXMLambda(exp);
            }
        }

    }


    public class ModelList
    {
        [XMColumn("Specifications")]
        public string NO { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public DateTime? InDate { get; set; }
        public int Id { get; set; }
        public Guid GId { get; set; }
    }



}
