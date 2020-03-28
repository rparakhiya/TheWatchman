using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheWatchman.Monitor.Services
{
    public interface IHeartbeatService
    {
        Task RegisterHeartbeatAsync();
    }
}
