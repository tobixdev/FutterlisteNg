namespace FutterlisteNg.Domain
{
    public interface IDataConfiguration
    {
        string ConnectionString { get; }
        string CollectionName { get; }
    }
}