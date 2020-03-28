using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheWatchman.Server.Services
{
    public class AppSettingsPersister : IAppSettingsPersister
    {
        private readonly IEnvironmentProvider _environmentProvider;

        public AppSettingsPersister(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }
        
        public void UpdateKey<T>(string key, T value, string path = null)
        {
            if (path == null)
            {
                path = Path.Combine(Environment.CurrentDirectory, $"appsettings.{_environmentProvider.GetEnvironment() ?? "Development"}.json");
            }

            if (!File.Exists(path))
            {
                File.WriteAllText(path, $"{{\"{key}\": null}}");
            }
            
            var json =   File.ReadAllText(path);
            var jsonObj = JsonConvert.DeserializeObject<JObject>(json);

            if (!jsonObj.ContainsKey(key))
            {
                jsonObj.Add(key, JToken.FromObject(value));
            }
            else
            {
                jsonObj[key] = JToken.FromObject(value);
            }

            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

            File.WriteAllText(path, output);
        }
    }
}