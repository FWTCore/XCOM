using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.SQLClient;
using XCOM.Schema.Standard.Utility;
using XCOM.Schema.XUnitProjectTest.Model;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.EDapper
{
    public class XMSqlCommandExecuteTest
    {


        [Theory(DisplayName = "QuerySql")]
        [InlineData("QuerySql")]
        public void QuerySql(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetQueryCondition("commonstatus", ConditionOperation.Equal, CommonStatusType.Valid);
            cmd.SetQueryCondition("sysno", ConditionOperation.Equal, 1);
            var actualDataList = cmd.Query<ExpressEntity>();
            var expectedDataList = new List<ExpressEntity>
            {
                new ExpressEntity
                {
                    SysNo = 1,
                    CompanyName = "阿斯达",
                    CommonStatus = CommonStatusType.Valid,
                    InUserSysNo = 123,
                    InUserName = "aaa1",
                    InDate = DateTime.Parse("2017-09-11 18:05:57.677")
                }
            };
            Assert.True(XMJson.Serailze(expectedDataList).Equals(XMJson.Serailze(actualDataList)));

        }


        [Theory(DisplayName = "QuerySql_Param")]
        [InlineData("QuerySql_Param")]
        public void QuerySql_Param(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetParameter("@sysno", 1);
            var actualData = cmd.QueryFirst<ExpressEntity>();
            var expectedData = new ExpressEntity
            {
                SysNo = 1,
                CompanyName = "阿斯达",
                CommonStatus = CommonStatusType.Valid,
                InUserSysNo = 123,
                InUserName = "aaa1",
                InDate = DateTime.Parse("2017-09-11 18:05:57.677")
            };

            Assert.True(XMJson.Serailze(expectedData).Equals(XMJson.Serailze(actualData)));

        }


        [Theory(DisplayName = "ExecuteSql_Param")]
        [InlineData("ExecuteSql_Param")]
        public void ExecuteSql_Param(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetParameter("@sysno", 1);
            var actualData = cmd.ExecuteScalar<int>();
            Assert.Equal(1, actualData);

        }


        [Theory(DisplayName = "ExecuteSql_Param2")]
        [InlineData("ExecuteSql_Param2")]
        public void ExecuteSql_Param2(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetParameter("@sysno", 1);
            var actualData = cmd.Execute();
            Assert.Equal(1, actualData);

        }

        [Theory(DisplayName = "ExecuteSql_Multi")]
        [InlineData("ExecuteSql_Multi")]
        public void ExecuteSql_Multi(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetParameter("@sysno", 1);
            cmd.SetParameter("@sysno1", 1);
            var invoice = new ExpressEntity();
            var invoiceItems = new List<ExpressEntity>();
            cmd.QueryMultiple((reader) =>
            {
                invoice = reader.ReadFirstOrDefault<ExpressEntity>();
                invoiceItems = reader.Read<ExpressEntity>().ToList();
            });

            var expectedData = new ExpressEntity
            {
                SysNo = 1,
                CompanyName = "阿斯达",
                CommonStatus = CommonStatusType.Valid,
                InUserSysNo = 123,
                InUserName = "aaa1",
                InDate = DateTime.Parse("2017-09-11 18:05:57.677")
            };
            var expectedDataList = new List<ExpressEntity>
            {
                expectedData
            };
            Assert.True(XMJson.Serailze(invoice).Equals(XMJson.Serailze(expectedData)));
            Assert.True(XMJson.Serailze(invoiceItems).Equals(XMJson.Serailze(expectedDataList)));

        }

        [Theory(DisplayName = "ExecuteSql_Transaction_ex")]
        [InlineData("ExecuteSql_Multi")]
        public void ExecuteSql_Transaction_ex(string sqlKey)
        {
            var msg = "手动异常阻止";
            var ex = Assert.Throws<Exception>(() =>
              {
                  var cmd = new XMSqlCommand(sqlKey);
                  cmd.SetParameter("@sysno", 10001);
                  cmd.SetParameter("@sysno1", 100000001);
                  cmd.ExecuteTransaction((con) =>
                  {
                      var invoice = new ExpressEntity();
                      var invoiceItems = new List<ExpressEntity>();
                      con.QueryMultiple((reader) =>
                      {
                          invoice = reader.ReadFirstOrDefault<ExpressEntity>();
                          invoiceItems = reader.Read<ExpressEntity>().ToList();
                      });
                      con.SetSqlKey("ExecuteSql_Param2");
                      con.SetParameter("@sysno", 10001);
                      con.Execute();
                      throw new Exception(msg);

                  });
              });

            Assert.Equal(msg, ex.Message);
        }

        [Theory(DisplayName = "ExecuteSql_Transaction")]
        [InlineData("ExecuteSql_Multi")]
        public void ExecuteSql_Transaction(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetParameter("@sysno", 10001);
            cmd.SetParameter("@sysno1", 100000001);
            cmd.ExecuteTransaction((con) =>
            {
                var invoice = new ExpressEntity();
                var invoiceItems = new List<ExpressEntity>();
                con.QueryMultiple((reader) =>
                {
                    invoice = reader.ReadFirstOrDefault<ExpressEntity>();
                    invoiceItems = reader.Read<ExpressEntity>().ToList();
                });
                con.SetSqlKey("ExecuteSql_Param2");
                con.SetParameter("@sysno", 10001);
                con.Execute();
                con.SetSqlKey("ExecuteSql_Param3");
                con.SetParameter("@sysno", 100000001);
                con.Execute();
            });

        }


        [Theory(DisplayName = "ExecuteSql_ParamEntity")]
        [InlineData("ExecuteSql_Param4")]
        public void ExecuteSql_ParamEntity(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var expectedData = new ExpressEntity
            {
                SysNo = 10001,
                CompanyName = "阿斯达",
                CommonStatus = CommonStatusType.Valid,
                InUserSysNo = 123,
                InUserName = "aaa1",
                InDate = DateTime.Parse("2017-09-11 18:05:57.677"),
                EditUserName = "bbb2",
            };
            cmd.SetParameter(expectedData);
            var result = cmd.Execute();
            Assert.Equal(1, result);
        }


        [Theory(DisplayName = "ExecuteSql_Tag")]
        [InlineData("ExecuteSql_Param_tag")]
        public void ExecuteSql_Tag(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            cmd.SetParameter("@sysno", 1);
            cmd.ReplaceAndSetSQLTag("TagWhere", "");
            cmd.SetParameter("@commonstatus", CommonStatusType.Valid);
            var result = cmd.QueryFirst<ExpressEntity>();

            var expectedData = new ExpressEntity
            {
                SysNo = 1,
                CompanyName = "阿斯达",
                CommonStatus = CommonStatusType.Valid,
                InUserSysNo = 123,
                InUserName = "aaa1",
                InDate = DateTime.Parse("2017-09-11 18:05:57.677")
            };
            Assert.True(XMJson.Serailze(expectedData).Equals(XMJson.Serailze(result)));

        }


        [Theory(DisplayName = "ExecuteSql_Tag")]
        [InlineData("ExecuteSql_Param_tag_rep")]
        public void ExecuteSql_Param_tag_rep(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var expectedData = new ExpressEntity
            {
                SysNo = 1,
                CompanyName = "阿斯达",
                CommonStatus = CommonStatusType.Valid,
                InUserSysNo = 123,
                InUserName = "aaa1",
                InDate = DateTime.Parse("2017-09-11 18:05:57.677")
            };
            var dataList = new List<ExpressEntity>()
            {
               expectedData
            };
            var temptable = cmd.GetTemporaryTableScript("TagTable", dataList);
            cmd.ReplaceSQLTag("TagTable", temptable);
            var result = cmd.QueryFirst<ExpressEntity>();

            Assert.True(XMJson.Serailze(expectedData).Equals(XMJson.Serailze(result)));

        }

        [Theory(DisplayName = "ExecuteSql_Tag")]
        [InlineData("ExecuteSql_Param_tag_rep_1")]
        public void ExecuteSql_Param_tag_rep_1(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var expectedData = new MysqlCharacterModel
            {
                Variable_name = "character_set_client",
                Value = "utf8mb4"
            };
            var dataList = new List<MysqlCharacterModel>()
            {
               expectedData
            };
            var temptable = cmd.GetTemporaryTableScript("TagTable", dataList);
            cmd.ReplaceSQLTag("TagTable", temptable);
            var result = cmd.QueryFirst<MysqlCharacterModel>();

            Assert.True(XMJson.Serailze(expectedData).Equals(XMJson.Serailze(result)));

        }


    }
}
