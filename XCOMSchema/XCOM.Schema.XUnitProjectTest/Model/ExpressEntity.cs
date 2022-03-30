using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.DataAnnotations;

namespace XCOM.Schema.XUnitProjectTest.Model
{
    [XMTable("express")]
    public class ExpressEntity
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
        [XMColumn("InUserName")]
        public string? InUserName { get; set; }
        [XMColumn("InDate")]
        public DateTime? InDate { get; set; }
        [XMColumn("EditUserSysNo")]
        public int? EditUserSysNo { get; set; }
        [XMColumn("EditUserName")]
        public string? EditUserName { get; set; }
        [XMColumn("EditDate")]
        public DateTime? EditDate { get; set; }
    }

    public enum CommonStatusType
    {
        Delete = -999,
        Invalid = 0,
        Valid = 1,

    }

}
