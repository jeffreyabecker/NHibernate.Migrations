using System;
using System.Collections.Generic;

namespace NHibernate.Migrations
{
    public class MigrationVersionComparitor : IComparer<IMigrationVersion>
    {
        
        public int Compare(IMigrationVersion x, IMigrationVersion y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            if (x.Context != y.Context)
            {
                throw new MigrationVersionException("Cant compare versions from different contexts {0} / {1}", x.Context ?? "(null)", y.Context ?? "(null)");
            }
            return StringComparer.Ordinal.Compare(x.Version, y.Version);
        }
    }
}