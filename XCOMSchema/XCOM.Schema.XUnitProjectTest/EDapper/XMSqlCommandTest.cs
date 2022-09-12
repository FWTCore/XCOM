using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.SQLClient;
using XCOM.Schema.EDapper.Utility;
using XCOM.Schema.Standard.Utility;
using XCOM.Schema.XUnitProjectTest.Model;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.EDapper
{

    public class XMSqlCommandTest
    {
        public static IEnumerable<object[]> InputData_SqlKey(string val)
        {
            var driverData = new List<object[]>
            {
                new object [] { "CommonService_CreateSystemEventLog" },
                new object [] { "GetSystemCategoryChildren" },
                new object [] { "AuthCenter.Insert" },
                new object [] { "CommonService_QuerySytemEventLogList_NoneSql" },
                new object [] { "CommonService_DbConfig_NoneDB" },
                new object [] { "AuthCenterWrite_" },
                new object [] { val},
            };
            return driverData;
        }


        /// <summary>
        /// 检测脚本获取，数据库配置获取
        /// </summary>
        /// <param name="sqlKey"></param>
        [Theory(DisplayName = "XMSqlCommandTest")]
        [MemberData("InputData_SqlKey", "")]
        public void CommandTest(string sqlKey)
        {
            if (string.IsNullOrWhiteSpace(sqlKey))
            {
                var ex = Assert.Throws<Exception>(() => new XMSqlCommand(sqlKey));
                var msg = $"sqlKey 不能为空!";
                Assert.Equal(msg, ex.Message);
            }
            else if (sqlKey.EndsWith("_NoneSql"))
            {
                var ex = Assert.Throws<Exception>(() => new XMSqlCommand(sqlKey));
                var msg = $"SQLKey:{sqlKey} 无效!";
                Assert.Equal(msg, ex.Message);
            }
            else if (sqlKey.EndsWith("_NoneDB"))
            {
                var ex = Assert.Throws<Exception>(() => new XMSqlCommand(sqlKey));
                var msg = $"SQLKey:{sqlKey} 数据库配置无效!";
                Assert.Equal(msg, ex.Message);
            }
            else if (sqlKey.EndsWith('_'))
            {
                var ex = Assert.Throws<Exception>(() => new XMSqlCommand(sqlKey));
            }
            else
            {
                var cmd = new XMSqlCommand(sqlKey);
                Assert.IsType<XMSqlCommand>(cmd);
            }
        }


        [Theory(DisplayName = "CheckSql_Where")]
        //[InlineData("CheckSql_Where_SqlServer")]
        [InlineData("CheckSql_Where_Mysql")]
        public void CheckSql_Condition(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);

            DynamicParameters Parameters = new();

            #region 条件生成测试
            if (sqlKey.EndsWith("_SqlServer"))
            {

            }
            if (sqlKey.EndsWith("_Mysql"))
            {
                #region ConditionOperation
                // Equal
                string? valCondition = "AND CommonStatus=@CommonStatus";
                string? sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.Equal, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // NotEqual
                valCondition = "AND CommonStatus<>@CommonStatus1";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotEqual, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // LessThan
                valCondition = "AND CommonStatus<@CommonStatus2";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.LessThan, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // LessThanEqual
                valCondition = "AND CommonStatus<=@CommonStatus3";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.LessThanEqual, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // MoreThan
                valCondition = "AND CommonStatus>@CommonStatus4";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.MoreThan, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // MoreThanEqual
                valCondition = "AND CommonStatus>=@CommonStatus5";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.MoreThanEqual, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // Like
                valCondition = "AND CommonStatus LIKE CONCAT('%',@CommonStatus6,'%'))";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.Like, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // LikeLeft
                valCondition = "AND CommonStatus LIKE CONCAT(@CommonStatus7,'%'))";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.LikeLeft, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // LikeRight
                valCondition = "AND CommonStatus LIKE CONCAT('%',@CommonStatus8))";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.LikeRight, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // NotLike
                valCondition = "AND CommonStatus NOT LIKE CONCAT('%',@CommonStatus9,'%'))";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotLike, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // NotLikeLeft
                valCondition = "AND CommonStatus NOT LIKE CONCAT(@CommonStatus10,'%'))";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotLikeLeft, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // NotLikeRight
                valCondition = "AND CommonStatus NOT LIKE CONCAT('%',@CommonStatus11))";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotLikeRight, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // In
                valCondition = "AND CommonStatus IN @CommonStatus12";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.In, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // NotIn
                valCondition = "AND CommonStatus Not In @CommonStatus13";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotIn, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                #endregion

                // 测试条件
                valCondition = "CommonStatus Not In @CommonStatus14";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotIn, 1, false);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                // 测试多参数
                valCondition = "AND CommonStatus Not In @CommonStatus15";
                sqlCondition = cmd.SetQueryCondition("CommonStatus", ConditionOperation.NotIn, 1);
                Assert.Equal(valCondition.Trim().ToUpper(), sqlCondition.Trim().ToUpper());
                for (var i = 0; i <= 15; i++)
                {
                    if (i == 0)
                    {
                        Parameters.Add("@CommonStatus", 1);
                    }
                    else
                    {
                        Parameters.Add($"@CommonStatus{i}", 1);
                    }
                }
                Assert.True(XMJson.Serailze(Parameters).Equals(XMJson.Serailze(cmd.Parameters)));
            }


            #endregion

        }

        [Theory(DisplayName = "CheckSql_Sql_WhereReplace")]
        //[InlineData("CheckSql_Where_SqlServer")]
        [InlineData("CheckSql_Where_Mysql")]
        public void CheckSql_Sql_WhereReplace(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var valContent = @"SELECT  SysNo
FROM    YZ_Operation.dbo.SystemCategory WITH ( NOLOCK )
WHERE   ParentSysNo = @SysNo
AND CommonStatus=@CommonStatus
;";
            cmd.SetQueryCondition("CommonStatus", ConditionOperation.Equal, 1, false);
            Assert.Equal(valContent.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
                cmd.GetExecuteScript().Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

        }

        /// <summary>
        /// 检测tag
        /// </summary>
        /// <param name="sqlKey"></param>
        [Theory(DisplayName = "CheckSql_Tag")]
        [InlineData("CheckSql_tag")]
        public void CheckSql_Tag(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var valContent = @"SELECT  SysNo
FROM    YZ_Operation.dbo.SystemCategory WITH ( NOLOCK )
WHERE   ParentSysNo = @SysNo
 AND (sr.RoleCode IN(1) OR sur.RoleType=1)
;
";
            var sqlTag = @"AND (sr.RoleCode IN({0}) OR sur.RoleType={1})";

            Assert.Equal(sqlTag.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
                cmd.GetSQLTag("TagWhere").Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

            cmd.ReplaceAndSetSQLTag("TagWhere", 1, 1);
            Assert.Equal(valContent.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
                cmd.GetExecuteScript().Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

            cmd.SetQueryCondition(sqlTag);
            Assert.Equal(valContent.Replace("#STRWHERE#", sqlTag).Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
              cmd.GetExecuteScript().Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

        }


        /// <summary>
        /// 检测临时表tag
        /// </summary>
        /// <param name="sqlKey"></param>
        [Theory(DisplayName = "CheckSql_TemporyTag")]
        [InlineData("CheckSql_TemporyTag")]
        public void CheckSql_TemporyTag(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var valContent = @"
DROP TEMPORARY TABLE IF EXISTS #TagTempory;
CREATE TEMPORARY TABLE #TagTempory(
temp_xmabc_id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
Id int,Name varchar(10),Description varchar(10),Amount decimal(20,6),IsExist int,Type int);
INSERT INTO #TagTempory (Id,Name,Description,Amount,IsExist,Type)
VALUES
(1,'a1','1231231',2.78,1,1),
(2,'a2','1231231',2.78,1,1),
(3,'a3','1231231',2.78,1,1),
(4,'a4','444',42.78,0,1)
;
";
            var testDataList = new List<XUnitModelVO>
            {
                new XUnitModelVO
                {
                    Id = 1,
                    Name = "a1",
                    Amount = 2.78M,
                    IsExist = true,
                    Description = "1231231",
                    Type = XUnitType.Now
                },
                new XUnitModelVO
                {
                    Id = 2,
                    Name = "a2",
                    Amount = 2.78M,
                    IsExist = true,
                    Description = "1231231",
                    Type = XUnitType.Now
                },
                new XUnitModelVO
                {
                    Id = 3,
                    Name = "a3",
                    Amount = 2.78M,
                    IsExist = true,
                    Description = "1231231",
                    Type = XUnitType.Now
                },
                new XUnitModelVO
                {
                    Id = 4,
                    Name = "a4",
                    Amount = 42.78M,
                    IsExist = false,
                    Description = "444",
                    Type = XUnitType.Now
                }
            };
            var sql = cmd.GetTemporaryTableScript("#TagTempory", testDataList);
            Assert.Equal(valContent.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
                sql.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

            cmd.ReplaceSQLTag("TagTempory", sql);


            valContent = @"
DROP TEMPORARY TABLE IF EXISTS #TagTempory;
CREATE TEMPORARY TABLE #TagTempory(
temp_xmabc_id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
Id int,Name varchar(10),Description varchar(10),Amount decimal(20,6),IsExist int,Type int);
INSERT INTO #TagTempory (Id,Name,Description,Amount,IsExist,Type)
VALUES
(1,'a1','1231231',2.78,1,1),
(2,'a2','1231231',2.78,1,1),
(3,'a3','1231231',2.78,1,1),
(4,'a4','444',42.78,0,1)
;
SELECT  SysNo
FROM    YZ_Operation.dbo.SystemCategory WITH ( NOLOCK )
WHERE   ParentSysNo = @SysNo
;
DROP TEMPORARY TABLE IF EXISTS #TagTempory;
";
            Assert.Equal(valContent.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
           cmd.GetExecuteScript().Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

        }

        /// <summary>
        /// 检测临时表tag
        /// </summary>
        /// <param name="sqlKey"></param>
        [Theory(DisplayName = "CheckSql_TemporyTag")]
        [InlineData("CheckSql_TemporyTag")]
        public void CheckSql_TemporyTag2(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var valContent = @"
DROP TEMPORARY TABLE IF EXISTS #TagTempory;
CREATE TEMPORARY TABLE #TagTempory(
temp_xmabc_id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
Id int,Name varchar(10),Description varchar(10),Amount decimal(20,6),IsExist int,Type int);
INSERT INTO #TagTempory (Id,Name,Description,Amount,IsExist,Type)
VALUES
(1,'a1','1231231',2.78,1,1),
(2,'a2','1231231',2.78,1,1),
(3,'a3','1231231',2.78,1,1),
(4,'a4','444',42.78,0,1)
;
";
            var testDataList = new List<XUnitModelVO>
            {
                new XUnitModelVO
                {
                    Id = 1,
                    Name = "a1",
                    Amount = 2.78M,
                    IsExist = true,
                    Description = "1231231",
                    Type = XUnitType.Now
                },
                new XUnitModelVO
                {
                    Id = 2,
                    Name = "a2",
                    Amount = 2.78M,
                    IsExist = true,
                    Description = "1231231",
                    Type = XUnitType.Now
                },
                new XUnitModelVO
                {
                    Id = 3,
                    Name = "a3",
                    Amount = 2.78M,
                    IsExist = true,
                    Description = "1231231",
                    Type = XUnitType.Now
                },
                new XUnitModelVO
                {
                    Id = 4,
                    Name = "a4",
                    Amount = 42.78M,
                    IsExist = false,
                    Description = "444",
                    Type = XUnitType.Now
                }
            };
            var sql = cmd.GetTemporaryTableScript("#TagTempory", testDataList);
            Assert.Equal(valContent.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
                sql.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

            cmd.ReplaceSQLTag("TagTempory", sql);


            valContent = @"
DROP TEMPORARY TABLE IF EXISTS #TagTempory;
CREATE TEMPORARY TABLE #TagTempory(
temp_xmabc_id int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
Id int,Name varchar(10),Description varchar(10),Amount decimal(20,6),IsExist int,Type int);
INSERT INTO #TagTempory (Id,Name,Description,Amount,IsExist,Type)
VALUES
(1,'a1','1231231',2.78,1,1),
(2,'a2','1231231',2.78,1,1),
(3,'a3','1231231',2.78,1,1),
(4,'a4','444',42.78,0,1)
;
SELECT  SysNo
FROM    YZ_Operation.dbo.SystemCategory WITH ( NOLOCK )
WHERE   ParentSysNo = @SysNo
AND CommonStatus=@CommonStatus
;
DROP TEMPORARY TABLE IF EXISTS #TagTempory;
";
            cmd.SetQueryCondition("CommonStatus", ConditionOperation.Equal, 1);
            Assert.Equal(valContent.Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim(),
                cmd.GetExecuteScript().Replace(" ", "").Replace("\r", "").Replace("\n", "").ToUpper().Trim());

        }


        [Theory(DisplayName = "CheckSql_TemporyTag12")]
        [InlineData("CheckSql_TemporyTag")]
        public void CheckEntity(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var entity = new BaseClassC()
            {
                a = "a",
                b = "b",
                c = "c"
            };
            cmd.SetParameter(entity);


            var result = 11;

        }
    }

    public class BaseClassA
    {
        public string a { get; set; }
    }
    public class BaseClassB : BaseClassA
    {
        public string b { get; set; }
    }
    public class BaseClassC : BaseClassB
    {
        public string c { get; set; }
    }
}
