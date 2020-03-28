namespace TheWatchman.Server.Services
{
    public interface IAppSettingsPersister
    {
        void UpdateKey<T>(string key, T value, string path = null);
    }
}