using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace TheWatchman.Server.Options
{
    public class TOtpAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Token lifetime in seconds
        /// </summary>
        public int TokenLifetime { get; set; }
    }
}
