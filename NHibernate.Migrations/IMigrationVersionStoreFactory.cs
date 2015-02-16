namespace NHibernate.Migrations
{
    public interface IMigrationVersionStoreFactory
    {
        IMigrationVersionStore GetMigrationVersionStore(ISessionFactory sessionFactory, string context);
    }
}