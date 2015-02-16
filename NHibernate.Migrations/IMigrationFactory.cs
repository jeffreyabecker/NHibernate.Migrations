namespace NHibernate.Migrations
{
  public interface IMigrationFactory
  {
    IMigration CreateMigration(System.Type type);
  }
}