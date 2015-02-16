namespace NHibernate.Migrations.Generation
{
    public interface IMigrationGenerator
    {
        string GenerateMigration(MigrationGenerationArguments args);
    }
}
