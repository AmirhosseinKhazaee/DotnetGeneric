using System;
using System.Collections.Generic;

namespace XGeneric.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseModel
    {
        private readonly Dictionary<Guid, T> _repository = new();

        public IEnumerable<T> GetAll()
        {
            return _repository.Values;
        }

        public T GetById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            if (!_repository.ContainsKey(id))
                return null;

            return _repository[id];
        }

        public bool Add(T entity)
        {
            if (entity == null)
                return false;

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity.createdAt = DateTime.UtcNow;

            if (_repository.ContainsKey(entity.Id))
                return false;

            _repository.Add(entity.Id, entity);
            return true;
        }

        public bool Update(T entity)
        {
            if (entity == null)
                return false;

            if (!_repository.ContainsKey(entity.Id))
                return false;

            _repository[entity.Id] = entity;
            return true;
        }

        public bool Delete(Guid id)
        {
            if (!_repository.ContainsKey(id))
                return false;

            return _repository.Remove(id);
        }
    }
}
