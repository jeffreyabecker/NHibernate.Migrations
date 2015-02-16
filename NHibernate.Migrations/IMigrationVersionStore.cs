using System.Collections.Generic;

namespace NHibernate.Migrations
{
    public interface IMigrationVersionStore
    {
        IMigrationVersion CurrentVersion { get; set; }
    }
}