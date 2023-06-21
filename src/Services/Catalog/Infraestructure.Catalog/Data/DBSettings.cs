using Domain.Catalog.Interfaces;

namespace Infraestructure.Catalog.Data
{
    public class DBSettings : IDBSettings
    {
        public string ConnectionString { get; set; }

        public string DataBase { get; set; }
    }
}
