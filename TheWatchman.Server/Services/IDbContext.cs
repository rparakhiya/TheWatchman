using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using TheWatchman.Server.Entities;

namespace TheWatchman.Server.Services
{
    public interface IDbContext
    {
        public ILiteCollection<T> GetCollection<T, TId>()
            where T: IPersistableEntity<TId>;

        public List<T> GetAll<T, TId>()
            where T : IPersistableEntity<TId>;

        public ILiteQueryable<T> Query<T, TId>()
            where T : IPersistableEntity<TId>;

        public T Get<T, TId>(TId id)
            where T : IPersistableEntity<TId>;

        public void Insert<T, TId>(T entity)
            where T : IPersistableEntity<TId>;

        public void Update<T, TId>(T entity)
            where T : IPersistableEntity<TId>;

        public T Delete<T, TId>(TId id)
            where T : IPersistableEntity<TId>;
    }
}
