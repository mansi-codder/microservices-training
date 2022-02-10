using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Config
{
    public class MongoDBConfig
    {
        /// <summary>
        /// Section name in the appsettings file.
        /// </summary>
        public const string SectionName = "MongoDB";
        public string DatabaseName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string CreateConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}:{Port}";

                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }

}
