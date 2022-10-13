using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.DataAnnotations;

namespace XCOM.Schema.XUnitProjectTest.Model
{
    [XMTable("express_test")]
    public class ExpressTestEntity
    {
        [XMKey]
        [XMDatabaseGenerated]
        [XMColumn("SysNo")]
        public int SysNo { get; set; }
        [XMColumn("CompanyName")]
        public string? CompanyName { get; set; }
        [XMColumn("CommonStatus")]
        public CommonStatusType? CommonStatus { get; set; }
        [XMColumn("InUserSysNo")]
        public int? InUserSysNo { get; set; }
        [XMColumn("IsDelete")]
        public bool IsDelete { get; set; }
        [XMColumn("InDate")]
        public DateTime? InDate { get; set; }
        [XMColumn("Amount")]
        public decimal Amount { get; set; }
        [XMColumn("EditDate")]
        public long? EditDate { get; set; }
        [XMColumn("Gid")]
        public Guid? Gid_Id { get; set; }
    }
}
