using HanoiEntity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HanoiRepository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        private static List<T> _repo = new List<T>();

        public void Add(T entity)
        {
            entity.Id = _repo.Count + 1;
            _repo.Add(entity);
        }

        public T Get(int id)
        {
            return _repo.Find(x => x.Id == id);
        }

        public void Delete(int id)
        {
            _repo.Remove(Get(id));
        }

        public int Count()
        {
            return _repo.Count + 1;
        }

        public IEnumerable<T> Get(Func<T, bool> filter)
        {
            return _repo.Where(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return _repo.ToList();
        }
    }
}