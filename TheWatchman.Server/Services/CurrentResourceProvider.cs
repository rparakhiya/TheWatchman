using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TheWatchman.Server.Services
{
    public class CurrentResourceProvider : ICurrentResourceProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentResourceProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentResourceId()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
