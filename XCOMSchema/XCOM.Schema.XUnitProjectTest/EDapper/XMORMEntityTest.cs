using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Linq;
using XCOM.Schema.Standard.Utility;
using XCOM.Schema.XUnitProjectTest.Model;
using XCOM.Schema.XUnitProjectTest.Repository;
using XCOM.Schema.XUnitProjectTest.Utility;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.EDapper
{
    public class XMORMEntityTest
    {

        private readonly string filed = "SysNo AS SysNo,CompanyName AS CompanyName,CommonStatus AS CommonStatus,InUserSysNo AS InUserSysNo,IsDelete AS IsDelete,InDate AS InDate,Amount AS Amount,EditDate AS EditDate,Gid AS Gid_Id";

        #region 枚举测试

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMEnum_Equals(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CommonStatus = @CommonStatus ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CommonStatus.Equals(CommonStatusType.Delete));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected),SqlUtility.Processing(sql));


        }
        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMEnum_Equals_Int(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CommonStatus = @CommonStatus ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CommonStatus.Equals((int)CommonStatusType.Delete));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMEnum(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CommonStatus = @CommonStatus ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CommonStatus == CommonStatusType.Delete);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }
        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMEnum_not(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CommonStatus <> @CommonStatus ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CommonStatus != CommonStatusType.Delete);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }
        #endregion

        #region Bool测试
        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMBool(string dbKey)
        {
            var expected = $"select {filed} from express_test  where IsDelete = @IsDelete ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.IsDelete);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMBool_NOT(string dbKey)
        {
            var expected = $"select {filed} from express_test  where IsDelete = @IsDelete ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            !d.IsDelete);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMBool_Equal(string dbKey)
        {
            var expected = $"select {filed} from express_test  where IsDelete = @IsDelete ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.IsDelete == false);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        #endregion


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("MZXSystem")]
        public void ORMFiled_Length(string dbKey)
        {
            var expected = $"select {filed} from express_test  where len(CompanyName) =@CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Length == 4);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("MZXSystem")]
        public void ORMFiled_Length1(string dbKey)
        {
            var code = "1234";
            var expected = $"select {filed} from express_test  where len(CompanyName) =@CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Length == code.Length + 4);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("MZXSystem")]
        public void ORMFiled_Length2(string dbKey)
        {
            var code = "1234";
            var expected = $"select {filed} from express_test  where len(CompanyName) =@CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Length.Equals(code.Length + 4));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }

        #region 字符串测试


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName = @CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName == "123");
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_t(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName = CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            "123" == d.CompanyName);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName = @CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Equals("123"));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_1_t(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName = CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            "123".Equals(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_Param(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName = CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = "123";
            var query = rep.Query(dbKey).Where(d =>
            para.Equals(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_Param_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName = @CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = "123";
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Equals(para));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_Param_not(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName <> @CompanyName ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = "123";
            var query = rep.Query(dbKey).Where(d =>
            !d.CompanyName.Equals(para));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Equal_Param_not_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName <> CompanyName  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = "123";
            var query = rep.Query(dbKey).Where(d =>
            !para.Equals(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName like CONCAT('%',@CompanyName,'%') ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = "123";
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Contains(para));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName like CONCAT('%',CompanyName,'%') ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = "123";
            var query = rep.Query(dbKey).Where(d =>
            para.Contains(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Convert(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName like CONCAT('%',CompanyName,'%') ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var query = rep.Query(dbKey).Where(d =>
            para.ToString().Contains(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Convert_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName like CONCAT('%',@CompanyName,'%') ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Contains(para.ToString()));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Convert_add(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName like CONCAT('%',@CompanyName,'%') ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var para2 = "456";
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.Contains((para.ToString() + para2)));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Convert_add_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName like CONCAT('%',CompanyName,'%')  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var para2 = "456";
            var query = rep.Query(dbKey).Where(d =>
            (para.ToString() + para2).Contains(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Startlike(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName like CONCAT(CompanyName,'%')   ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var para2 = "456";
            var query = rep.Query(dbKey).Where(d =>
            (para.ToString() + para2).StartsWith(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Startlike_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName like CONCAT(@CompanyName,'%')   ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var para2 = "456";
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.StartsWith((para.ToString() + para2)));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Endlike(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @CompanyName like CONCAT('%',CompanyName)   ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var para2 = "456";
            var query = rep.Query(dbKey).Where(d =>
            (para.ToString() + para2).EndsWith(d.CompanyName));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_Endlike_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName like CONCAT('%',@CompanyName)   ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = 123;
            var para2 = "456";
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName.EndsWith((para.ToString() + para2)));
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_In(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName in @CompanyName  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = new List<string>
            {
                "123",
                "456",
                "789"
            };
            var query = rep.Query(dbKey).Where(d =>
            para.Contains(d.CompanyName)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_notIn(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName not in @CompanyName  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var para = new List<string>
            {
                "123",
                "456",
                "789"
            };
            var query = rep.Query(dbKey).Where(d =>
            !para.Contains(d.CompanyName)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }



        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_not(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName is null  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            string.IsNullOrEmpty(d.CompanyName)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_not_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName is not null  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            !string.IsNullOrEmpty(d.CompanyName)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_not_null(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName is null  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            string.IsNullOrWhiteSpace(d.CompanyName)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_not_null_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName is not null  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            !string.IsNullOrWhiteSpace(d.CompanyName)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_not_null_null(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName is not null  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.InUserSysNo == null && !string.IsNullOrWhiteSpace(d.CompanyName)
            ); 
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Param_not_null_null1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName is not null  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.InUserSysNo == null && !string.IsNullOrWhiteSpace(d.CompanyName) && !string.IsNullOrWhiteSpace(null)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Contains_Func(string dbKey)
        {
            var expected = $"select {filed} from express_test  where CompanyName =@CompanyName  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.CompanyName == ValueUtility.GetString()
            ); ;
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }
        #endregion

        #region Int测试

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled(string dbKey)
        {
            var expected = $"select {filed} from express_test  where SysNo = @SysNo ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.SysNo == 1);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where SysNo <> @SysNo ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.SysNo != 1);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_More(string dbKey)
        {
            var expected = $"select {filed} from express_test  where SysNo > @SysNo ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.SysNo > 1);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_MoreEqual(string dbKey)
        {
            var expected = $"select {filed} from express_test  where SysNo >= @SysNo ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.SysNo >= 1);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMFiled_Fun(string dbKey)
        {
            var expected = $"select {filed} from express_test  where SysNo >= @SysNo ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.SysNo >= ValueUtility.GetInt());
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));


        }



        #endregion

        #region DateTime 测试

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMDateTime(string dbKey)
        {
            var expected = $"select {filed} from express_test  where InDate >= @InDate ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.InDate >= ValueUtility.GetDateTime()
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMDateTime_eq(string dbKey)
        {
            var expected = $"select {filed} from express_test  where InDate = @InDate ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.InDate.Equals(ValueUtility.GetDateTime())
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMDateTime_eq_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @InDate =InDate  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            ValueUtility.GetDateTime().Equals(d.InDate)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMDateTime_all(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @InDate =InDate  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var dt = DateTime.Now.ToString();
            var query = rep.Query(dbKey).Where(d =>
            DateTime.Parse(dt).Equals(d.InDate)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        #endregion


        #region Guid 测试

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMGuid(string dbKey)
        {
            var expected = $"select {filed} from express_test  where Gid = @Gid ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.Gid_Id == ValueUtility.GetGuid()
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMGuid_eq(string dbKey)
        {
            var expected = $"select {filed} from express_test  where Gid = @Gid ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            d.Gid_Id.Equals(ValueUtility.GetGuid())
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMGuid_eq_1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @Gid =Gid  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(d =>
            ValueUtility.GetGuid().Equals(d.Gid_Id)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORMGuid_all(string dbKey)
        {
            var expected = $"select {filed} from express_test  where @Gid =Gid  ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var dt = DateTime.Now.ToString();
            var query = rep.Query(dbKey).Where(d =>
            ValueUtility.GetGuidConvert().Equals(d.Gid_Id)
            );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }

        #endregion


        #region 综合测试
        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_all(string dbKey)
        {
            var expected = $"select {filed} from express_test  where (( (CommonStatus=@CommonStatus) or (IsDelete=@IsDelete)) " +
                $"and ((CommonStatus=@CommonStatus0) and ((CompanyName is not null) or (CompanyName is null)))) " +
                $"and ( (CommonStatus<>@CommonStatus1) or ((IsDelete=@IsDelete0) and (Gid=@Gid)))" +
                $" ORDER BY SysNo ";
            var rep = new ExpressTestRepository();
            var dt = DateTime.Now.ToString();
            var query = rep.Query(dbKey).Where(d =>
            (d.CommonStatus.Equals((int)CommonStatusType.Valid) || d.IsDelete)
            && (d.CommonStatus.Equals(CommonStatusType.Valid) && (!string.IsNullOrWhiteSpace(d.CompanyName) || string.IsNullOrEmpty(d.CompanyName)))
            && (!d.CommonStatus.Equals(CommonStatusType.Valid) || (!d.IsDelete && d.Gid_Id == ValueUtility.GetGuidConvert()))
             );
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));

        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_all_1(string dbKey)
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                var rep = new ExpressTestRepository();
                var dt = DateTime.Now.ToString();
                var query = rep.Query(dbKey).Where(d =>
                !(d.CommonStatus.Equals((int)CommonStatusType.Valid) || d.IsDelete)
                 );
                var sql = query.DebugSql();
            });
            var msg = $"还有表达式类型【Not】未解析";
            Assert.Equal(msg, ex.Message);

        }




        #endregion

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_Other(string dbKey)
        {
            var rep = new ExpressTestRepository();
            var expression = XMLinq.True<ExpressTestEntity>();
            var query = rep.Query(dbKey).Where(expression);
            var sql = query.DebugSql();
        }



    }
}
