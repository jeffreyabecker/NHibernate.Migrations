using System.Collections.Generic;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations
{
    public interface IMigration
    {
        IEnumerable<IDdlOperation> GetOperations();
        IMigrationVersion GetVersion();
    }
}