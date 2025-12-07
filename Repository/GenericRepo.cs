using System.Reflection;
using XGeneric.Attributes;
using XGeneric.Extensions;
using XGeneric.Models;

namespace XGeneric.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseModel
    {
        private readonly Dictionary<object, T> _repository = new();
        private readonly object _locker = new(); // lock object for thread-safety

        // Get the property marked with [XKey]
        private PropertyInfo GetKeyProperty()
        {
            var result = typeof(T).GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(XKeyAttribute)));
            if (result == null)
                throw new Exception($"No property with [XKey] found on {typeof(T).Name}");
            return result;
        }

        private object GetKeyValue(T entity)
        {
            var prop = GetKeyProperty();
            return prop.GetValue(entity) ?? throw new Exception("Key value is null");
        }

        #region Async Methods

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            List<T> result;

            lock (_locker)
            {
                result = _repository.Values.ToList();
            }

            return await Task.FromResult(result);
        }


        public async Task<T?> GetByIdAsync(object id)
        {
            if (id == null) return null;

            T? result;

            lock (_locker)
            {
                _repository.TryGetValue(id , out result);
            }
                return await Task.FromResult(result);
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity == null) return false;

            entity.IsXBaseModel(); // init metadata

            var key = GetKeyValue(entity);

            // If key is GUID and empty, create GUID
            if (key is Guid g && g == Guid.Empty)
            {
                key = Guid.NewGuid();
                GetKeyProperty().SetValue(entity, key);
            }

            lock (_locker)
            {
                if (_repository.ContainsKey(key)) return false;

                _repository.Add(key, entity);
                return true;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null) return false;

            var key = GetKeyValue(entity);
            if (key == null) return false;

            lock (_locker)
            {
                if (!_repository.ContainsKey(key)) return false;

                entity.UpdatedAt = DateTime.UtcNow;
                _repository[key] = entity;
                return true;
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            if (id == null) return false;

            lock (_locker)
            {
                if (!_repository.ContainsKey(id)) return false;

                return _repository.Remove(id);
            }
        }

        #endregion
    }
}
