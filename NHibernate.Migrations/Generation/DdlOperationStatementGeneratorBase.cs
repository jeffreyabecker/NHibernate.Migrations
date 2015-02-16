using System.CodeDom;
using System.Collections.Generic;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public abstract class DdlOperationStatementGeneratorBase<TOperation> : IDdlOperationStatementGenerator where TOperation : IDdlOperation
    {

        protected virtual string GetRandomVariableName()
        {
            return "zz" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName());
        }
        protected static CodeObjectCreateExpression GetDbNameExpression(DbName dbName)
        {
            return new CodeObjectCreateExpression(typeof(DbName), new CodePrimitiveExpression(dbName.Catalog),
                new CodePrimitiveExpression(dbName.Schema), new CodePrimitiveExpression(dbName.Name));
        }
        public IEnumerable<CodeStatement> GetStatements(IDdlOperation operation)
        {
            return GetStatements((TOperation)operation);
        }

        protected abstract IEnumerable<CodeStatement> GetStatements(TOperation operation);
    }
}