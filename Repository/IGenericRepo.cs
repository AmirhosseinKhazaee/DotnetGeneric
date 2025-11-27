public interface IGenericRepo<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(Guid id);
}
