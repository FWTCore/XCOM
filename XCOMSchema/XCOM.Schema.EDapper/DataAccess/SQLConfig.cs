using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCOM.Schema.EDapper.DataAccess
{
    [Serializable]
    [XmlRoot]
    public class SQLConfig
    {
        [XmlArrayItem("SQL")]
        public List<SQLScript> SQLList { get; set; }
    }
}
