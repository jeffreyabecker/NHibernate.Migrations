using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public abstract class DdlGeneratorBase<TOperation> : IDdlOperationGenerator where TOperation : IDdlOperation
    {
        public void AppendOperation(StringBuilder sb, int index, IDdlOperation operation)
        {
            AppendOperation(sb, index, (TOperation) operation);
        }

        protected abstract void AppendOperation(StringBuilder sb, int index, TOperation op);
    }
}