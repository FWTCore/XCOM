using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCOM.Schema.EDapper.DataAccess
{
    [Serializable]
    public class SQLScript
    {
        [XmlAttribute]
        public string SQLKey { get; set; }

        [XmlAttribute]
        public string ConnectionKey { get; set; }

        [XmlElement]
        public string Text { get; set; }

        [XmlIgnore]
        public List<string> ParameterNameList { get; set; }

        [XmlAttribute]
        public string NoLimit5000 { get; set; }
        [XmlAttribute]
        public int TimeOut { get; set; }

    }
}
