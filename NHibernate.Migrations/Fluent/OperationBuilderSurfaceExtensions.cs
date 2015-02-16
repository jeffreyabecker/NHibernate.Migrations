using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public static class OperationBuilderSurfaceExtensions
    {
        public static void Sql(this IDdlOperationBuilderSurface surface, string s)
        {
            surface.Run(new SqlDdlOperation(s));
        }

        public static void DisableForeignKeyConstraints(this IDdlOperationBuilderSurface surface)
        {
            surface.Run(new DisableForeignKeyConstraintDdlOperation());
        }

        public static void EnableForeignKeyConstraints(this IDdlOperationBuilderSurface surface)
        {
            surface.Run(new EnableForeignKeyConstratintDdlOperation());
        }

    }
}