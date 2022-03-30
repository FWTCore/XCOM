using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Utility;
using XCOM.Schema.XUnitProjectTest.Model;
using XCOM.Schema.XUnitProjectTest.Repository;
using Xunit;

namespace XCOM.Schema.XUnitProjectTest.EDapper
{
    public class XMSqlEntityTest
    {



        [Theory(DisplayName = "Insert")]
        [InlineData("localhost")]
        public void Insert(string dbKey)
        {
            var dataList = new List<ExpressEntity>{
                 new ExpressEntity()
                 {
                SysNo = 1,
                CommonStatus = CommonStatusType.Valid,
                CompanyName = "测试qwe1111",
                EditDate = DateTime.Now,
                EditUserName = "admin",
                EditUserSysNo = 1,
                InDate = DateTime.Now,
                InUserName = "admin的",
                InUserSysNo = 1,
                 },new ExpressEntity()
                 {
                SysNo = 1,
                CommonStatus = CommonStatusType.Valid,
                CompanyName = "测试qwe22222",
                EditDate = DateTime.Now,
                EditUserName = "admin",
                EditUserSysNo = 1,
                InDate = DateTime.Now,
                InUserName = "admin的",
                InUserSysNo = 1,
                 },new ExpressEntity()
                 {
                SysNo = 1,
                CommonStatus = CommonStatusType.Valid,
                CompanyName = "测试qwe121221",
                EditDate = DateTime.Now,
                EditUserName = "admin",
                EditUserSysNo = 1,
                InDate = DateTime.Now,
                InUserName = "admin的",
                InUserSysNo = 1,
                 }
            };

            var rep = new ExpressRepository();
            //var dd = rep.Insert(dbKey, dataList);
        }


        [Theory(DisplayName = "delete")]
        [InlineData("localhost")]
        public void Delete(string dbKey)
        {
            var rep = new ExpressRepository();
            rep.Delete(dbKey, 100000002);
            rep.Delete(dbKey, d => d.SysNo == 100000002);

        }

        [Theory(DisplayName = "Query")]
        [InlineData("localhost")]
        public void Query(string dbKey)
        {
            var rep = new ExpressRepository();
            var data = rep.Query(dbKey).Where(d => d.CommonStatus == CommonStatusType.Valid).Count();
            Assert.Equal(6, data);
        }






    }
}
