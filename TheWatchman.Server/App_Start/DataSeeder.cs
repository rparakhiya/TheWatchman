using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheWatchman.Server.Entities;
using TheWatchman.Server.Services;

namespace TheWatchman.Server
{
    public static class DataSeeder
    {
        public static void EnsureSeeded(this IDbContext dbContext)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Seed");

            SeedData<MonitoredResource, string>(dbContext, Path.Combine(path, "monitored-resources.json"));
        }

        private static void SeedData<T, TId>(IDbContext context, string filePath)
            where T : IPersistableEntity<TId>
        {
            if (!context.GetCollection<T, TId>().Query().ToList().Any())
            {
                var data = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
                context.GetCollection<T, TId>()
                    .InsertBulk(data);
            }

            context.GetCollection<T, TId>().EnsureIndex(x => x.Id);
        }
    }
}
