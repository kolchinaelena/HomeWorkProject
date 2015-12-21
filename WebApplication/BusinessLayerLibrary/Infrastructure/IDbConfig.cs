using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayerLibrary.Infrastructure
{
    public interface IDbConfig
    {
        string SqlConnectionString { get; }
    }
}
