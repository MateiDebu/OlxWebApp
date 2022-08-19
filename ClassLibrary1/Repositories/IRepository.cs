namespace Core.Repositories
{
    public interface IRepository<T>
    {
        T Get();
        IEnumerable<T> GetAll();
        IEnumerable<T> FindByName(string name);
        IEnumerable<T> FindById(int id);
        T Add(T element);
        T Update(T element);
        bool Delete(int id);

    }
}
