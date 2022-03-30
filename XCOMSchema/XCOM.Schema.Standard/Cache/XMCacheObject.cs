using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Cache
{
    public class XMCacheObject
    {
        /// <summary>
        /// 字典
        /// </summary>
        private static readonly Dictionary<string, object> _dataDic = new();


        /// <summary>
        /// 定义一个静态变量来保存类的实例
        /// </summary>
        private static XMCacheObject _session;

        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object _locker = new();


        /// <summary>
        /// 单例
        /// </summary>
        /// <returns>返回类型为Session</returns>
        public static XMCacheObject Instance
        {
            get
            {
                if (_session == null)
                {
                    lock (_locker)
                    {
                        if (_session == null)// 如果类的实例不存在则创建，否则直接返回
                        {
                            _session = new XMCacheObject();
                        }
                    }
                }
                return _session;
            }
        }

        #region Remove 移除

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            _dataDic.Remove(name);
        }

        /// <summary>
        /// 删除全部成员
        /// </summary>
        public static void RemoveAll()
        {
            _dataDic.Clear();
        }
        #endregion

        #region 本类的索引器

        /// <summary>
        /// 本类的索引器
        /// </summary>
        /// <returns>返回Object成员</returns>
#pragma warning disable CA1822 // 将成员标记为 static
        public object this[string index]
#pragma warning restore CA1822 // 将成员标记为 static
        {
            get
            {
                if (_dataDic.ContainsKey(index))
                {
                    Object obj = (Object)_dataDic[index];
                    return obj;
                }
                return null;
            }
            set
            {
                if (_dataDic.ContainsKey(index))
                {
                    _dataDic.Remove(index);
                }
                _dataDic.Add(index, value);
            }
        }
        #endregion


    }
}

