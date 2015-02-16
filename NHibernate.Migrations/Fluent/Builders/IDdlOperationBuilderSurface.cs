using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Fluent.Builders
{
    public interface IDdlOperationBuilderSurface
    {
        ICreateDdlOperationBuilder Create { get; }
        IAlterDdlOperationBuilder Alter { get; }
        IDropDdlOperationBuilder Drop { get; }
        void Run(IDdlOperation operation);
    }
}
