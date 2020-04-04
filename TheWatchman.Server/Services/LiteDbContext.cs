using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using TheWatchman.Server.Entities;

namespace TheWatchman.Server.Services
{
    public class LiteDbContext : IDbContext
    {
        private readonly LiteDatabase db;

        public LiteDbContext(LiteDatabase db)
        {
            this.db = db;
        }

        public ILiteCollection<T> GetCollection<T, TId>()
            where T: IPersistableEntity<TId>
        {
            return db.GetCollection<T>();
        }

        public ILiteQueryable<T> Query<T, TId>()
            where T: IPersistableEntity<TId>
        {
            return this.GetCollection<T, TId>().Query();
        }

        public List<T> GetAll<T, TId>()
            where T: IPersistableEntity<TId>
        {
            return this.Query<T, TId>().ToList();
        }

        public T Get<T, TId>(TId id)
            where T: IPersistableEntity<TId>
        {
            // TODO: Need to optimize
            return this.GetCollection<T, TId>()
                .Query().ToList()
                .First(x => x.Id.Equals(id));
        }

        public void Insert<T, TId>(T entity)
            where T: IPersistableEntity<TId>
        {
            this.GetCollection<T, TId>()
                .Insert(entity);
        }

        public void Update<T, TId>(T entity)
            where T: IPersistableEntity<TId>
        {
            this.GetCollection<T, TId>()
                .Update(entity);
        }

        public T Delete<T, TId>(TId id)
            where T: IPersistableEntity<TId>
        {
            var resource = Get<T, TId>(id);

            if (!this.GetCollection<T, TId>()
                .Delete(new BsonValue(id.ToString())))
            {
                throw new Exception($"Couldn't delete the document {id}. Returned false.");
            }

            return resource;
        }
    }
}
