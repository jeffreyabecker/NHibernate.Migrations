using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NHibernate.Cfg;

namespace NHibernate.Migrations
{
    public interface IMigrationVersion 
    {
        string Context { get;  }
        string Version { get;  }
        Configuration Configuration { get; }
    }
}
