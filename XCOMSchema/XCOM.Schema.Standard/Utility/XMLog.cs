using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Configuration/log4net.config", ConfigFileExtension = "config", Watch = true)]
namespace XCOM.Schema.Standard.Utility
{
    public class XMLog
    {
        private static readonly ILog log = LogManager.GetLogger(Assembly.GetCallingAssembly(), "XMLog");

        //声明一个通知的委托
        public delegate void NoticeEventHander(string message);
        //在委托的机制下我们建立以个通知事件
        public static event NoticeEventHander OnNotice;

        public static void Error(string message, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Error(message);
        }
        public static void Error(string message, Exception exception, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Error(message, exception);
        }
        public static void Error(Exception exception, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Error("系统Error信息", exception);
        }

        public static void Debug(string message, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Debug(message);
            //执行通知
            OnNotice?.Invoke($"系统异常，请及时处理，异常信息：{message}");
        }
        public static void Debug(string message, Exception exception, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Debug(message, exception);
        }
        public static void Debug(Exception exception, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Debug("系统Debug信息", exception);
        }

        public static void Info(string message, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Info(message);
        }
        public static void Info(string message, Exception exception, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Debug(message, exception);
        }
        public static void Info(Exception exception, Action registedProperties = null)
        {
            registedProperties?.Invoke();
            log.Info("系统Info信息", exception);
        }

    }
}
