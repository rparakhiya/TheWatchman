using System;

namespace TheWatchman.Server.Services
{
    public interface IRequestAuthenticator
    {
        bool Authenticate(string id, string auth);
    }
}