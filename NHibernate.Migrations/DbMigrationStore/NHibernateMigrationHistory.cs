using System;
using System.Configuration;

namespace NHibernate.Migrations.DbMigrationStore
{
    public class NHibernateMigrationHistory : IMigrationVersion
    {
        public NHibernateMigrationHistory(string version, Cfg.Configuration configuration, string context)
        {
            Context = context;
            Version = version;
            Configuration = configuration;
        }

        protected NHibernateMigrationHistory()
        {
            
        }

        public virtual string Context { get; protected set; }

        public virtual string Version { get; protected set; }
        public virtual Cfg.Configuration Configuration { get; protected set; }

        public override int GetHashCode()
        {
            return (Context + Version).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var that = obj as IMigrationVersion;
            return new MigrationVersionComparitor().Compare(this, that) == 0;
        }
    }
}
