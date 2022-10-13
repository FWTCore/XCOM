using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Linq;
using XCOM.Schema.XUnitProjectTest.Model;
using XCOM.Schema.XUnitProjectTest.Repository;
using XCOM.Schema.XUnitProjectTest.Utility;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.EDapper
{
    public class XMORMExtensionTest
    {
        private readonly string filed = "SysNo AS SysNo,CompanyName AS CompanyName,CommonStatus AS CommonStatus,InUserSysNo AS InUserSysNo,IsDelete AS IsDelete,InDate AS InDate,Amount AS Amount,EditDate AS EditDate,gid AS Gid";


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_True(string dbKey)
        {
            var expected = $"select {filed} from express_test  where 1=1 ORDER BY SysNo ";
            var exception = XMLinq.True<ExpressTestEntity>();
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(exception);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_True_Other(string dbKey)
        {
            var expected = $"select {filed} from express_test  where (1=1) and (CommonStatus=@CommonStatus) ORDER BY SysNo ";
            var exception = XMLinq.True<ExpressTestEntity>();
            exception = exception.And(e => e.CommonStatus == CommonStatusType.Valid);
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(exception);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }
        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_True_Other1(string dbKey)
        {
            var expected = $"select {filed} from express_test  where ((1=1) and (CommonStatus=@CommonStatus)) and (1=1) ORDER BY SysNo ";
            var exception = XMLinq.True<ExpressTestEntity>();
            exception = exception.And(e => e.CommonStatus == CommonStatusType.Valid);
            exception = exception.And(e => true);
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(exception);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }

        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_True_Other2(string dbKey)
        {
            var expected = $"select {filed} from express_test  where (((1=1) and (CommonStatus=@CommonStatus)) and (1=1)) and (IsDelete=@IsDelete) ORDER BY SysNo ";
            var exception = XMLinq.True<ExpressTestEntity>();
            exception = exception.And(e => e.CommonStatus == CommonStatusType.Valid);
            exception = exception.And(e => true);
            exception = exception.And(e => e.IsDelete);
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(exception);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }


        [Theory(DisplayName = "XMORMEntityTest")]
        [InlineData("localhost")]
        public void ORM_True_Other3(string dbKey)
        {
            var expected = $"select {filed} from express_test  where (((1=1) and (CommonStatus=@CommonStatus)) and (1=0)) and (IsDelete=@IsDelete) ORDER BY SysNo ";
            var exception = XMLinq.True<ExpressTestEntity>();
            exception = exception.And(e => e.CommonStatus == CommonStatusType.Valid);
            exception = exception.And(e => false);
            exception = exception.And(e => e.IsDelete);
            var rep = new ExpressTestRepository();
            var query = rep.Query(dbKey).Where(exception);
            var sql = query.DebugSql();
            Assert.Equal(SqlUtility.Processing(expected), SqlUtility.Processing(sql));
        }
    }
}
