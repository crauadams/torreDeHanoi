using HanoiEntity;
using System;
using System.Collections.Generic;

namespace HanoiRepository
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        T Get(int id);
        void Delete(int id);
        int Count();
        IEnumerable<T> Get(Func<T, bool> filter);
        IEnumerable<T> GetAll();
    }
}
