using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Engine;
using NHibernate.Migrations.DbMigrationStore;
using NHibernate.Util;
using Environment = NHibernate.Cfg.Environment;

namespace NHibernate.Migrations
{
    public class Migrator
    {
        private readonly IMigrationVersionStore _migrationVersionStore;
        private readonly ISessionFactoryImplementor _sessionFactory;
        private readonly List<IMigration> _registeredMigrations;

        public Migrator(ISessionFactory sessionFactory)
          : this(sessionFactory, new ActivatorMigrationFactory())
        {}

        public Migrator(ISessionFactory sessionFactory, IMigrationFactory migrationFactory)
        {
            _sessionFactory = (ISessionFactoryImplementor)sessionFactory;

            IMigrationVersionStoreFactory migrationVersionStoreFactory = new NHibernateMigrationHistoryVersionStoreFactory();

            if (migrationVersionStoreFactory == null)
            {
                throw new HibernateConfigException(
                    "Tried to initialize a migrator without setting migrations.versionstorefactory");
            }

            if (migrationFactory is ActivatorMigrationFactory)
                ((ActivatorMigrationFactory)migrationFactory).AttachMigrationVersionStoreFactory(migrationVersionStoreFactory);

            _migrationVersionStore = migrationVersionStoreFactory.GetMigrationVersionStore(_sessionFactory, PropertiesHelper.GetString(MigrationSettings.ContextName, _sessionFactory.Settings.Properties, null));

            _registeredMigrations = GetRegisterdMigrations(_sessionFactory.Settings.Properties)
                               .Select(migrationFactory.CreateMigration)
                               .OrderBy(x => x.GetVersion(), new MigrationVersionComparitor())
                               .ToList();
        }

        private ICollection<System.Type> GetRegisterdMigrations(IDictionary<string, string> properties)
        {
            var items = PropertiesHelper.GetString(MigrationSettings.RegisteredMigrations, properties, String.Empty);
            var typeNames = items.Split(';').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)).ToList();
            var result = new List<System.Type>();
            foreach (var tn in typeNames)
            {
                var registeredType = System.Type.GetType(tn);
                if (typeof(IMigration).IsAssignableFrom(registeredType))
                {
                    result.Add(registeredType);
                }
                else
                {
                    throw new HibernateConfigException(String.Format("{0} is registered as a migration but does not implement IMigration", tn));
                }
            }
            return result;
        }

        private static IMigrationVersionStoreFactory GetMigrationVersionStoreFactory(IDictionary<string, string> properties)
        {
            IMigrationVersionStoreFactory migrationVersionStoreFactory = null;
            string migrationVersionStoreFactoryName = PropertiesHelper.GetString(MigrationSettings.VersionStoreFactory,
                                                                                 properties, String.Empty);
            if (!String.IsNullOrEmpty(migrationVersionStoreFactoryName))
            {
                migrationVersionStoreFactory =
                    (IMigrationVersionStoreFactory)
                        Environment.BytecodeProvider.ObjectsFactory.CreateInstance(
                            ReflectHelper.ClassForName(migrationVersionStoreFactoryName));
            }
            return migrationVersionStoreFactory;
        }

        
        public void MigrateToLatestVersion(Action<string> scripter = null, bool executeMigrations = true)
        {
            var latest = _registeredMigrations.Last();
            var resultVersion = Migrate(_migrationVersionStore.CurrentVersion, latest.GetVersion(), scripter, executeMigrations);
            _migrationVersionStore.CurrentVersion = resultVersion;
        }

        public IMigrationVersion Migrate(IMigrationVersion currentVersion, IMigrationVersion targetVersion, Action<string> scripter = null,  bool executeMigrations = true)
        {
            var scriptAction = scripter ?? ((s) => { });

            var migrationsToExecute = GetMigrationsToExecute(currentVersion, targetVersion).ToList();

            if (migrationsToExecute.Count == 0) return currentVersion;

            var operations = migrationsToExecute.SelectMany(m => m.GetOperations());

            var statements = operations.SelectMany(o => o.GetStatements(_sessionFactory.Dialect)).ToList();
            foreach (var s in statements)
            {
                scriptAction(s);
            }
            if (executeMigrations)
            {
                ExecuteMigrationStatements(statements);
            }
            return migrationsToExecute.Last().GetVersion();
        }

        private void ExecuteMigrationStatements(IEnumerable<string> statements)
        {
            using (var conn = _sessionFactory.ConnectionProvider.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    foreach (var s in statements)
                    {
                        cmd.CommandText = s;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public IEnumerable<IMigration> GetMigrationsToExecute(IMigrationVersion currentVersion, IMigrationVersion targetVersion)
        {
            var comp = new MigrationVersionComparitor();

            if (currentVersion == null)
            {
                return _registeredMigrations
                    .Where(m => comp.Compare(targetVersion, m.GetVersion()) >= 0)
                    .ToList();
            }

            var toExecute = _registeredMigrations
                .Where( m => comp.Compare(m.GetVersion(), currentVersion) > 0 && comp.Compare(targetVersion, m.GetVersion()) >= 0)
                .ToList();
            return toExecute;
            
        }
    }
}