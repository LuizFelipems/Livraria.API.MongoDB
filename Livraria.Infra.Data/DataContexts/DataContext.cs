using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Livraria.Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public IMongoDatabase MongoDBConexao { get; set; }

        public DataContext(IOptions<SettingsInfra> options)
        {
            try
            {
                IMongoClient client = new MongoClient(options.Value.ConnectionString);
                MongoDBConexao = client.GetDatabase(options.Value.NomeBaseDados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                if (MongoDBConexao != null)
                    MongoDBConexao = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}