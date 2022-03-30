using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public static class XMEmit
    {
        private static string GenerateStringID()
        {
            long num = 1L;
            byte[] array = Guid.NewGuid().ToByteArray();
            byte[] array2 = array;
            foreach (byte b in array2)
            {
                num *= b + 1;
            }
            return $"{num - DateTime.Now.Ticks:x}";
        }
        //public static Type CreateType(Type interfaceType, IMethodEmit methodEmitter)
        //{
        //    return CreateType(interfaceType, methodEmitter, isPersist: false);
        //}

    }
}
