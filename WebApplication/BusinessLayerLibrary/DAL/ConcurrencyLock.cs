using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerLibrary.DAL
{
    public enum ConcurrencyLock
    {
        Optimistic,
        Pessimistic
    }
}
