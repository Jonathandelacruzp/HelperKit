namespace HelperKit.Repositories
{
    public interface ISingleKeyEntityRepository<T, in K> : IRepository<T> where T : class
    {
        T GetSingle(K entityKey);
    }
}