using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.EDapper.DataAccess;
using XCOM.Schema.EDapper.SQLClient;
using XCOM.Schema.XUnitProjectTest.Model;
using XCOM.Schema.XUnitProjectTest.Utility;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.EDapper
{
    public class SqlCommandGenerate
    {

        #region 检测XMSqlCommand 对象生成

        #region 检测Sqlkey生成对象

        public static IEnumerable<object[]> InputData_SqlKey_Error(string val, string msg)
        {
            var driverData = new List<object[]>
            {
                new object [] {"express.notexist_dbkey","SQLKey:express.notexist_dbkey 数据库配置无效!"},
                new object [] {"express.notexist_sqlkey","SQLKey:express.notexist_sqlkey 无效!" },
                new object [] {val,msg},
            };
            return driverData;
        }
        /// <summary>
        /// 检测错误情况
        /// </summary>
        /// <param name="sqlKey"></param>
        /// <param name="msg"></param>
        [Theory]
        [MemberData("InputData_SqlKey_Error", "", "sqlKey 不能为空!")]
        public void CheckCommandobject_SqlKey_Error(string sqlKey, string msg)
        {
            var ex = Assert.Throws<Exception>(() => new XMSqlCommand(sqlKey));
            Assert.Equal(msg, ex.Message);
        }

        #endregion

        #region 检测Sqlkey+dbKey 生成对象

        public static IEnumerable<object[]> InputData_SqlKey_DBKey_Error()
        {
            var driverData = new List<object[]>
            {
                new object [] { "", "", "sqlKey 不能为空!" },
                new object [] { "express.notexist_sqlkey", "", "dbKey 不能为空!" },
                new object [] { "express.notexist_sqlkey", "express.notexist_dbkey", "SQLKey:express.notexist_sqlkey 无效!" },
                new object [] { "express.Insert", "express.notexist_dbkey", "SQLKey:express.notexist_dbkey 数据库配置无效!" },
            };
            return driverData;
        }
        /// <summary>
        /// 检测错误情况
        /// </summary>
        /// <param name="sqlKey"></param>
        /// <param name="msg"></param>
        [Theory]
        [MemberData("InputData_SqlKey_DBKey_Error")]
        public void CheckCommandobject_Error(string sqlKey, string dbKey, string msg)
        {
            var ex = Assert.Throws<Exception>(() => new XMSqlCommand(sqlKey, dbKey));
            Assert.Equal(msg, ex.Message);
        }

        #endregion

        #endregion

        #region 测试脚本配置获取
        public static IEnumerable<object[]> InputData_SqlKey()
        {
            var driverData = new List<object[]>
            {
                new object [] {
                    "express.Insert"
                    ,@"insert into express(companyname, commonstatus, inusersysno, inusername, indate, editusersysno, editusername, editdate)value (@CompanyName, @CommonStatus, @InUserSysNo, @InUserName, @InDate, @EditUserSysNo, @EditUserName, @EditDate)"
                },
                new object [] {
                    "express.update"
                    ,@"update express set CompanyName='测试' WHERE SysNo = 1"
                },
                new object [] {
                    "express.query"
                    ,@"select sysno,companyname,commonstatus,inusersysno,inusername,indate,editusersysno, editusername,editdate from express"
                },
                new object [] {
                    "express.delete"
                    ,@"delete from express where SysNo = 12"
                },
            };
            return driverData;
        }

        [Theory]
        [MemberData("InputData_SqlKey")]
        public void CheckGetSqlScript(string sqlKey, string expected)
        {
            var cmd = new XMSqlCommand(sqlKey);
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(cmd.CommandText));
        }

        #endregion

        #region 测试条件生成
        public static IEnumerable<object[]> InputData_Condition()
        {
            var driverData = new List<object[]>
            {
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.Equal,Value=1}
                    ,@"AND CommonStatus=@CommonStatus"
                    ,@"AND CommonStatus=@CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotEqual,Value=1}
                    ,@"AND CommonStatus<>@CommonStatus"
                    ,@"AND CommonStatus<>@CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LessThan,Value=1}
                    ,@"AND CommonStatus<@CommonStatus"
                    ,@"AND CommonStatus<@CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LessThanEqual,Value=1}
                    ,@"AND CommonStatus<=@CommonStatus"
                    ,@"AND CommonStatus<=@CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.MoreThan,Value=1}
                    ,@"AND CommonStatus>@CommonStatus"
                    ,@"AND CommonStatus>@CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.MoreThanEqual,Value=1}
                    ,@"AND CommonStatus>=@CommonStatus"
                    ,@"AND CommonStatus>=@CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.Like,Value=1}
                    ,@"AND CommonStatus LIKE '%'+@CommonStatus+'%'"
                    ,@"AND CommonStatus LIKE CONCAT('%',@CommonStatus,'%')"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LikeLeft,Value=1}
                    ,@"AND CommonStatus LIKE @CommonStatus+'%'"
                    ,@"AND CommonStatus LIKE CONCAT(@CommonStatus,'%')"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LikeRight,Value=1}
                    ,@"AND CommonStatus LIKE '%'+@CommonStatus"
                    ,@"AND CommonStatus LIKE CONCAT('%',@CommonStatus)"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotLike,Value=1}
                    ,@"AND CommonStatus NOT LIKE '%'+@CommonStatus+'%'"
                    ,@"AND CommonStatus NOT LIKE CONCAT('%',@CommonStatus,'%')"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotLikeLeft,Value=1}
                    ,@"AND CommonStatus NOT LIKE @CommonStatus+'%'"
                    ,@"AND CommonStatus NOT LIKE CONCAT(@CommonStatus,'%')"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotLikeRight,Value=1}
                    ,@"AND CommonStatus NOT LIKE '%'+@CommonStatus"
                    ,@"AND CommonStatus NOT LIKE CONCAT('%',@CommonStatus)"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.In,Value=1}
                    ,@"AND CommonStatus IN @CommonStatus"
                    ,@"AND CommonStatus IN @CommonStatus"
                },
                new object [] {
                    new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotIn,Value=1}
                    ,@"AND CommonStatus Not In @CommonStatus"
                    ,@"AND CommonStatus Not In @CommonStatus"
                },
            };
            return driverData;
        }

        [Theory]
        [MemberData("InputData_Condition")]
        public void CheckCondition(ConditionObject conditionObject, string expected_mssql, string expected_mysql)
        {
            var cmd = new XMSqlCommand("express.template_mysql");
            var sqlCondition = cmd.SetQueryCondition(conditionObject.FieldName, conditionObject.ConditionOperation, conditionObject.Value);
            Assert.Equal(SqlUtility.Processing(expected_mysql), SqlUtility.Processing(sqlCondition));
            cmd = new XMSqlCommand("express.template_mssql");
            sqlCondition = cmd.SetQueryCondition(conditionObject.FieldName, conditionObject.ConditionOperation, conditionObject.Value);
            Assert.Equal(SqlUtility.Processing(expected_mssql), SqlUtility.Processing(sqlCondition));
        }

        #endregion

        #region 测试参数
        public static IEnumerable<object[]> InputData_Param()
        {
            var driverData = new List<object[]>
            {
                new object [] {
                    new List<ConditionObject>{
                        new ConditionObject{ FieldName ="CommonStatus",ConditionOperation=ConditionOperation.Equal,Value=1}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotEqual,Value=2}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LessThan,Value=3}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LikeLeft,Value=4}
                    }
                    ,@"AND CommonStatus=@CommonStatus AND CommonStatus<>@CommonStatus0 AND CommonStatus<@CommonStatus1 AND CommonStatus LIKE @CommonStatus2+'%'"
                    ,@"AND CommonStatus=@CommonStatus AND CommonStatus<>@CommonStatus0 AND CommonStatus<@CommonStatus1 AND CommonStatus LIKE CONCAT(@CommonStatus2,'%')"
                    , new Dictionary<string, object>()
                    {
                        { "CommonStatus", 1}
                        ,{ "CommonStatus0", 2}
                        ,{ "CommonStatus1", 3}
                        ,{ "CommonStatus2", 4}
                    }
                },
            };
            return driverData;
        }
        [Theory]
        [MemberData("InputData_Param")]
        public void CheckParameter(List<ConditionObject> conditionObject, string expected_mssql, string expected_mysql, Dictionary<string, object> expected_param)
        {
            var cmd = new XMSqlCommand("express.template_mysql");
            conditionObject.ForEach(data =>
            {
                cmd.SetQueryCondition(data.FieldName, data.ConditionOperation, data.Value);
            });
            Assert.Equal(SqlUtility.Processing(expected_mysql), SqlUtility.Processing(cmd.QueryConditionString));
            Assert.Equal(expected_param, SqlUtility.GetDapperParameter(cmd.Parameters));

            cmd = new XMSqlCommand("express.template_mssql");
            conditionObject.ForEach(data =>
            {
                cmd.SetQueryCondition(data.FieldName, data.ConditionOperation, data.Value);
            });
            Assert.Equal(SqlUtility.Processing(expected_mssql), SqlUtility.Processing(cmd.QueryConditionString));
            Assert.Equal(expected_param, SqlUtility.GetDapperParameter(cmd.Parameters));
        }


        #endregion

        #region 测试设置参数
        public static IEnumerable<object[]> InputData_SetParam()
        {
            var driverData = new List<object[]>
            {
                new object [] {
                    new List<int>{
                        1
                        ,2
                        ,3
                    }
                    , new Dictionary<string, object>()
                    {
                        { "SysNo", 1}
                        ,{ "SysNo0", 2}
                        ,{ "SysNo1", 3}
                    }
                },
            };
            return driverData;
        }
        [Theory]
        [MemberData("InputData_SetParam")]
        public void CheckSetParameter(List<int> pa, Dictionary<string, object> expected_param)
        {
            var cmd = new XMSqlCommand("express.template_SetParam");
            pa.ForEach(p =>
            {
                cmd.SetParameter("SysNo", p);
            });

            Assert.Equal(expected_param, SqlUtility.GetDapperParameter(cmd.Parameters));

        }


        #endregion


        #region 检测对象参数

        [Theory]
        [InlineData("express.template_model_Param")]
        public void CheckEntityParameter(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var entity = new ModelParam()
            {
                SysNo = 1,
                CompanyName = "CompanyName",
                CommonStatus = CommonStatusType.Valid,
                InUserSysNo = 100,
                EditDate = DateTime.Now
            };
            cmd.SetParameter(entity);

            var expected_param = new Dictionary<string, object>()
            {
                {"sysno",1 }
                ,{"companyname","CompanyName" }
                ,{"commonstatus",1 }
            };
            Assert.Equal(expected_param, SqlUtility.GetDapperParameter(cmd.Parameters));
        }
        #endregion


        #region 检测WHERE替换
        public static IEnumerable<object[]> InputData_Replace_Condition()
        {
            var driverData = new List<object[]>
            {
                new object [] {
                    "express.template_condition"
                    ,"#STRCONDITION#"
                    ,new List<ConditionObject>{
                        new ConditionObject{ FieldName ="CommonStatus",ConditionOperation=ConditionOperation.Equal,Value=1}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotEqual,Value=2}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LessThan,Value=3}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LikeLeft,Value=4}
                    }
                    ,@"SELECT SysNo FROM express WHERE SysNo = @SysNo AND CommonStatus=@CommonStatus AND CommonStatus<>@CommonStatus0 AND CommonStatus<@CommonStatus1 AND CommonStatus LIKE @CommonStatus2+'%'"
                    ,@"SELECT SysNo FROM express WHERE SysNo = @SysNo AND CommonStatus=@CommonStatus AND CommonStatus<>@CommonStatus0 AND CommonStatus<@CommonStatus1 AND CommonStatus LIKE CONCAT(@CommonStatus2,'%')"
                    , new Dictionary<string, object>()
                    {
                        { "CommonStatus", 1}
                        ,{ "CommonStatus0", 2}
                        ,{ "CommonStatus1", 3}
                        ,{ "CommonStatus2", 4}
                    }
                },
                new object [] {
                    "express.template_where"
                    ,""
                    ,new List<ConditionObject>{
                        new ConditionObject{ FieldName ="CommonStatus",ConditionOperation=ConditionOperation.Equal,Value=1}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.NotEqual,Value=2}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LessThan,Value=3}
                        ,new ConditionObject{ FieldName="CommonStatus",ConditionOperation=ConditionOperation.LikeLeft,Value=4}
                    }
                    ,@"SELECT SysNo FROM express WHERE SysNo = @SysNo AND CommonStatus=@CommonStatus AND CommonStatus<>@CommonStatus0 AND CommonStatus<@CommonStatus1 AND CommonStatus LIKE @CommonStatus2+'%'"
                    ,@"SELECT SysNo FROM express WHERE SysNo = @SysNo AND CommonStatus=@CommonStatus AND CommonStatus<>@CommonStatus0 AND CommonStatus<@CommonStatus1 AND CommonStatus LIKE CONCAT(@CommonStatus2,'%')"
                    , new Dictionary<string, object>()
                    {
                        { "CommonStatus", 1}
                        ,{ "CommonStatus0", 2}
                        ,{ "CommonStatus1", 3}
                        ,{ "CommonStatus2", 4}
                    }
                },
            };
            return driverData;
        }
        [Theory]
        [MemberData("InputData_Replace_Condition")]
        public void CheckSql_ConditionReplace(string sqlKey, string placeholders, List<ConditionObject> conditionObject, string expected_mssql, string expected_mysql, Dictionary<string, object> expected_param)
        {
            var cmd = new XMSqlCommand(sqlKey, "localhost");
            conditionObject.ForEach(data =>
            {
                cmd.SetQueryCondition(data.FieldName, data.ConditionOperation, data.Value);
            });
            if (!string.IsNullOrWhiteSpace(placeholders))
                cmd.ReplacePlaceholders(placeholders, cmd.QueryConditionString);
            Assert.Equal(SqlUtility.Processing(expected_mysql), SqlUtility.Processing(cmd.GetExecuteScript()));
            Assert.Equal(expected_param, SqlUtility.GetDapperParameter(cmd.Parameters));

            cmd = new XMSqlCommand(sqlKey, "MZXSystem");
            conditionObject.ForEach(data =>
            {
                cmd.SetQueryCondition(data.FieldName, data.ConditionOperation, data.Value);
            });
            if (!string.IsNullOrWhiteSpace(placeholders))
                cmd.ReplacePlaceholders(placeholders, cmd.QueryConditionString);
            Assert.Equal(SqlUtility.Processing(expected_mssql), SqlUtility.Processing(cmd.GetExecuteScript()));
            Assert.Equal(expected_param, SqlUtility.GetDapperParameter(cmd.Parameters));
        }

        #endregion

        #region 检测tag标签
        public static IEnumerable<object[]> InputData_Tag()
        {
            var driverData = new List<object[]>
            {
                new object [] {
                    "express.template_tag"
                    ,"TagWhere"
                    ,new List<string>(){ "1","2", "3" }
                    ,1
                    ,@"AND InUserSysNo is not null"
                    ,@"AND (CompanyName IN({0}) OR InUserSysNo={1})"
                    ,@"SELECT  SysNo FROM    express WHERE   ParentSysNo = @SysNo AND InUserSysNo is not null"
                    ,@"SELECT  SysNo FROM    express WHERE   ParentSysNo = @SysNo AND (CompanyName IN('1','2','3') OR InUserSysNo=1)"
                },
            };
            return driverData;
        }
        [Theory]
        [MemberData("InputData_Tag")]
        public void CheckSql_tag(string sqlKey, string tagName, List<string> pa1, int pa2, string replace, string expected_tag, string expected_tag_replace, string expected)
        {
            var cmd = new XMSqlCommand(sqlKey);

            Assert.Equal(SqlUtility.Processing(expected_tag), SqlUtility.Processing(cmd.GetSQLTag(tagName)));
            cmd.ReplaceSQLTag(tagName, replace);
            Assert.Equal(SqlUtility.Processing(expected_tag_replace), SqlUtility.Processing(cmd.GetExecuteScript()));

            cmd = new XMSqlCommand(sqlKey);
            cmd.ReplaceAndSetSQLTag(tagName, $"'{string.Join("','", pa1.Select(e => cmd.SetSafeParameter(e)))}'", pa2);
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(cmd.GetExecuteScript()));

        }


        #endregion


        #region 检测临时表


        /// <summary>
        /// 检测临时表tag
        /// </summary>
        /// <param name="sqlKey"></param>
        [Theory]
        [InlineData("express.template_temptable")]
        public void CheckSql_TemporyTag(string sqlKey)
        {
            var cmd = new XMSqlCommand(sqlKey);
            var expected_Sql = @"
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
            var tempTableSql = cmd.GetTemporaryTableScript("#TagTempory", testDataList);

            Assert.Equal(SqlUtility.Processing(expected_Sql), SqlUtility.Processing(tempTableSql));

            cmd.ReplaceSQLTag("TagTempory", tempTableSql);


            expected_Sql = @"
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
            Assert.Equal(SqlUtility.Processing(expected_Sql), SqlUtility.Processing(cmd.GetExecuteScript()));

        }


        #endregion




        #region 条件对象定义

        /// <summary>
        /// 参数对象
        /// </summary>
        public class ConditionObject
        {
            /// <summary>
            /// 字段名称
            /// </summary>
            public string FieldName { get; set; }
            /// <summary>
            /// 条件操作符
            /// </summary>
            public ConditionOperation ConditionOperation { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public object Value { get; set; }
        }



        #endregion

        #region 对象参数

        public class BaseModel
        {
            public int SysNo { get; set; }
            public int? InUserSysNo { get; set; }
            public int? EditUserSysNo { get; set; }
        }

        public class ModelParam : BaseModel
        {
            public string CompanyName { get; set; }
            public CommonStatusType? CommonStatus { get; set; }
            public DateTime? EditDate { get; set; }
            public DateTime? InDate { get; set; }

        }

        #endregion


    }
}
