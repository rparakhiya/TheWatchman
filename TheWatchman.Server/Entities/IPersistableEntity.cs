using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWatchman.Server.Entities
{
    public interface IPersistableEntity<TId>
    {
        TId Id { get; set; }
    }
}
