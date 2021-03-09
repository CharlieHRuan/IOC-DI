using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Framework
{
    public class ConfigurationManager
    {
        private static IConfigurationRoot _iConfigurationRoot;

        static ConfigurationManager()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _iConfigurationRoot = builder.Build();

        }

        public static string GetNode(string nodeName)
        {
            return _iConfigurationRoot[nodeName];
        }
    }
}
