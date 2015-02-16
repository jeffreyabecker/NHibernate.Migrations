using System.Linq;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Migrations.DbMigrationStore;
using NHibernate.Util;

namespace NHibernate.Migrations
{
    public static class MigrationExtensions
    {
        public static void MigrateToLatestVersion(this ISessionFactory sessionFactory)
        {
            new Migrator(sessionFactory).MigrateToLatestVersion();
        }
        public static Configuration SetMigrationContext(this Cfg.Configuration configuration, string context)
        {
            configuration.SetProperty(MigrationSettings.ContextName, context);
            return configuration;
        }
        public static Configuration UseTableBasedMigrations(this Cfg.Configuration configuration, string context, string catalog = null, string schema = null, string table = null)
        {
            var mapper = new ModelMapper();
            mapper.AddMapping(new NHibernateMigrationHistoryMapping(catalog, schema, table));
            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            configuration.SetMigrationContext(context);
            configuration.SetMigrationVersionStoreFactory(typeof(NHibernateMigrationHistoryVersionStoreFactory));
            return configuration;
        }

        public static Configuration SetMigrationVersionStoreFactory(this Cfg.Configuration configuration, System.Type type)
        {
            configuration.SetProperty(MigrationSettings.VersionStoreFactory, type.AssemblyQualifiedName);
            return configuration;
        }

        public static Configuration AddRegisteredMigration<TMigration>(this Cfg.Configuration configuration)
            where TMigration : IMigration
        {
            AddRegisterdMigration(configuration, typeof(TMigration).AssemblyQualifiedName);
            return configuration;
        }

        private static Configuration AddRegisterdMigration(Configuration configuration, string assemblyQualifiedName)
        {
            var existingProp = PropertiesHelper.GetString(MigrationSettings.RegisteredMigrations,
                                                          configuration.Properties, "");
            existingProp += assemblyQualifiedName + ";";
            configuration.SetProperty(MigrationSettings.RegisteredMigrations, existingProp);
            return configuration;
        }

        public static Configuration RegisterAllMigrationsFrom(this Configuration configuration, Assembly assembly)
        {
            var migrationTypes = assembly.GetExportedTypes()
                    .Where(t => ImplementsIMigration(t) && IsInstantiable(t));

            foreach (var migrationType in migrationTypes)
            {
                AddRegisterdMigration(configuration, migrationType.AssemblyQualifiedName);
            }
            return configuration;
        }

        private static bool IsInstantiable(System.Type type)
        {
            return !type.IsAbstract && !type.IsInterface &&
                   type.GetConstructors().Any(c => c.GetParameters().Length == 0);
        }
        private static bool ImplementsIMigration(System.Type type)
        {
            return typeof (IMigration).IsAssignableFrom(type);
        }
    }
}
