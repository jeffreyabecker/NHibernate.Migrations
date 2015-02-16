namespace NHibernate.Migrations.DbMigrationStore
{
    public class NHibernateMigrationHistoryVersionStoreFactory : IMigrationVersionStoreFactory
    {
        public IMigrationVersionStore GetMigrationVersionStore(ISessionFactory sessionFactory, string context)
        {
            return new NHibernateMigrationHistoryVersionStore(sessionFactory, context);
        }
    }
}