public interface IGenericRepo<T> 
{
    IEnumerable<T> GetAll();
    T? GetById(object id);    // Use object, because key type is unknown
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(object id);   // object works for any key type
}
