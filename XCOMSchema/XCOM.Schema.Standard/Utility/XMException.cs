using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    [Serializable]
    public class XMException : Exception
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        public XMException()
        {
        }

        public XMException(string message)
            : base(message)
        {
            XMLog.Error(message, () => { });
        }
        public XMException(string code, string message)
         : base(message)
        {
            Code = code;
            XMLog.Error(message, () => { });
        }

        public XMException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
            XMLog.Error(string.Format(messageFormat, args), () => { });
        }
        public XMException(string code, string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
            Code = code;
            XMLog.Error(string.Format(messageFormat, args), () => { });
        }

        public XMException(string message, Exception innerException)
            : base(message, innerException)
        {
            //只记录最原始的Exception信息
            if (!(innerException is XMException))
            {
                XMLog.Error(message, innerException, () => { });
            }
        }

        public XMException(string code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            //只记录最原始的Exception信息
            if (!(innerException is XMException))
            {
                XMLog.Error(message, innerException, () => { });
            }
        }

        /// <summary>
        /// 实现ISerialization接口所需要的反序列化构造函数。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        private XMException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // stringInfo = info.GetString("StringInfo");
        }

        /// <summary>
        ///  重写GetObjectData方法。如果添加了自定义字段，一定要重写基类GetObjectData方法的实现
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // 序列化自定义数据成员
            //info.AddValue("StringInfo", stringInfo);

            // 调用基类方法，序列化它的成员
            base.GetObjectData(info, context);
        }

    }
}
