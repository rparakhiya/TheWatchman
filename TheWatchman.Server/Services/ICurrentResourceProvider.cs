using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWatchman.Server.Services
{
    public interface ICurrentResourceProvider
    {
        string GetCurrentResourceId();
    }
}
