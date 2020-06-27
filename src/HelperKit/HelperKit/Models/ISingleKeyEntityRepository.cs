namespace HelperKit.Models
{
    public interface ISingleKeyEntityRepository<T, K> : IRepository<T> where T : class
    {
        T GetSingle(K entityKey);
    }
}