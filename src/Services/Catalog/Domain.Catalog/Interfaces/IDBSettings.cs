namespace Domain.Catalog.Interfaces
{
    public interface IDBSettings
    {
        string ConnectionString { get; set; }

        string DataBase { get; set; }
    }
}
