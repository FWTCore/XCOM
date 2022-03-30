using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.XUnitProjectTest.Utility
{
    internal class ValueUtility
    {

        public static int GetInt()
        {
            return -888;
        }

        public static string GetString()
        {
            return GetInt().ToString();
        }

        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static Guid GetGuid()
        {
            return Guid.NewGuid();
        }
        public static Guid GetGuidConvert()
        {
            var guid = Guid.NewGuid().ToString();
            return Guid.Parse(guid);
        }

    }
}
