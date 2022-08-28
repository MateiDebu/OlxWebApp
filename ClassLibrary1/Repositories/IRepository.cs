namespace Core.Repositories
{
    public interface IRepository<T>
    {
        T Get();
        IEnumerable<T> GetAll(int offset,int limit);
        IEnumerable<T> FindByName(string name);
        Task<T?> FindById(int id);
        Task<T> Add(T element);
        Task<T> Update(T element);
        Task Delete(int id);

    }
}
 