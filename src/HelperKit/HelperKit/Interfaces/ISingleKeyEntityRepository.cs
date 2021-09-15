namespace HelperKit.Interfaces
{
    public interface ISingleKeyEntityRepository<T, in TKey> : IRepository<T> where T : class
    {
        T GetSingle(TKey entityKey);
    }
}