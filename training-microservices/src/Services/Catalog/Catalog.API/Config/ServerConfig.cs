
namespace Catalog.API.Config
{
    public class ServerConfig
    {
        public const string Name = "MongoDB";
        public MongoDBConfig MongoDB { get; set; } = new MongoDBConfig();
    }
}
