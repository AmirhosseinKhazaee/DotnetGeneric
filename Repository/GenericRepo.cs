using System.Reflection;
using XGeneric.Attributes;
using XGeneric.Extensions;
using XGeneric.Models;

namespace XGeneric.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseModel
    {
        private readonly Dictionary<object, T> _repository = new();

        private PropertyInfo GetKeyProperty()
        {
            var result = typeof(T).GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(XKeyAttribute)));
            return result;
        }

        private object GetKeyValue(T entity)
        {
            var prop = GetKeyProperty();
            var result = prop.GetValue(entity);
            return result;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.Values;
        }

        public T GetById(object id)
        {
            if (id == null)
                return null;

            if (!_repository.ContainsKey(id))
                return null;

            return _repository[id];
        }

        public bool Add(T entity)
        {
            if (entity == null)
                return false;

            // init model metadata (CreatedAt, UpdatedAt, etc.)
            entity.IsXBaseModel();

            var key = GetKeyValue(entity);

            if (key == null)
                return false;

            // If key is GUID and empty, create GUID
            if (key is Guid g && g == Guid.Empty)
            {
                key = Guid.NewGuid();
                GetKeyProperty().SetValue(entity, key);
            }

            if (_repository.ContainsKey(key))
                return false;

            _repository.Add(key, entity);
            return true;
        }

        public bool Update(T entity)
        {
            if (entity == null)
                return false;

            var key = GetKeyValue(entity);
            if (key == null)
                return false;

            if (!_repository.ContainsKey(key))
                return false;

            entity.UpdatedAt = DateTime.UtcNow;

            _repository[key] = entity;
            return true;
        }

        public bool Delete(object id)
        {
            if (!_repository.ContainsKey(id))
                return false;

            return _repository.Remove(id);
        }
    }
}
