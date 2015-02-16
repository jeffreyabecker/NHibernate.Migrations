namespace NHibernate.Migrations
{
    public interface IMigrationConfigurationFactory
    {
        Cfg.Configuration GetConfiguration();
    }
}