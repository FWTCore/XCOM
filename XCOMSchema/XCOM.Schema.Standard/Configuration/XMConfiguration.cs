using Microsoft.Extensions.Configuration;
using System;

namespace XCOM.Schema.Standard.Configuration
{
    public static class XMConfiguration
    {
        /// <summary>
        /// 配置对象
        /// </summary>
        private static IConfiguration _configuration { get; set; }
        static XMConfiguration()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", true, true).Build();
        }

        public static void Instance(string environmentName)
        {
            var fileName = "appsettings.json";
            if (!string.IsNullOrWhiteSpace(environmentName) && environmentName != "Production")
                fileName = $"appsettings.{environmentName}.json";
            _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile(fileName, true, true).Build();

        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetConfigOrDefault(string key, string defaultValue = "")
        {
            string config = _configuration[key];
            return string.IsNullOrWhiteSpace(config) ? defaultValue : config;
        }
        //public static T GetAppConfig<T>(string key)
        //{
        //    T data = _configuration.GetSection(key).GetSection();
        //    return data;
        //    return string.IsNullOrWhiteSpace(config) ? defaultValue : config;

        //}

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <returns></returns>
        public static IConfiguration AppConfig()
        {
            return _configuration;
        }
        /// <summary>
        /// 初始化配置对象
        /// </summary>
        /// <param name="config"></param>
        public static void InstanseConfiguration(IConfiguration config)
        {
            _configuration = config;
        }


        /// <summary>
        /// 解析文件为配置对象
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public static IConfiguration SetJsonFile(string jsonFilePath)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(jsonFilePath, true, true).Build();
            return configuration;
        }

    }
}
