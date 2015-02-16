using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CSharp;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Generation
{
    public class Builder2<TProvider> where TProvider : CodeDomProvider, new()
    {
        public string GenerateMigration(MigrationGenerationArguments args)
        {
            var compileUnit = new CodeCompileUnit();
            var ns = new CodeNamespace(args.CodeNamespace);
            ns.Imports.Add(new CodeNamespaceImport("NHibernate.Cfg"));
            ns.Imports.Add(new CodeNamespaceImport("NHibernate.Migrations.Fluent"));
            ns.Imports.Add(new CodeNamespaceImport("NHibernate.Migrations.Fluent.Builders"));
            
            var mc = new CodeTypeDeclaration(args.Name) {IsClass = true, TypeAttributes = TypeAttributes.Public};
            mc.BaseTypes.Add(typeof(FluentMigration));
            mc.Members.Add(GetConstructor(args));
            mc.Members.Add(GetBuildOperations(args));
            mc.Members.Add(GetGetConfigurationMethod());
            var configurationDataField = GetConfigurationDataField(args);
            mc.Members.Add(configurationDataField);
            compileUnit.Namespaces.Add(ns);
            ns.Types.Add(mc);

            var cscp = new TProvider();
            var sb = new StringBuilder();
            var tw = new IndentedTextWriter(new StringWriter(sb), "    ");
            cscp.GenerateCodeFromCompileUnit(compileUnit, tw, new CodeGeneratorOptions());
            tw.Flush();
            return sb.ToString();
        }

        private CodeMemberField GetConfigurationDataField(MigrationGenerationArguments args)
        {
            var versionString = FluentMigration.SerializeConfiguration(args.Cfg);
            var configurationDataField = new CodeMemberField(typeof (string), "_configurationData")
            {
                Attributes = MemberAttributes.Const,
                InitExpression = new CodePrimitiveExpression(versionString)
            };
            return configurationDataField;
        }

        private CodeMemberMethod GetGetConfigurationMethod()
        {
            var getConfigureationMethod = new CodeMemberMethod();
            getConfigureationMethod.Name = "GetConfiguration";
            getConfigureationMethod.ReturnType = new CodeTypeReference(typeof (NHibernate.Cfg.Configuration));
            getConfigureationMethod.Attributes = MemberAttributes.Override | MemberAttributes.Family;
            var returnStatement = new CodeMethodReturnStatement();
            returnStatement.Expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(),
                "GetConfigurationFromCompressedBytes", new CodeFieldReferenceExpression(new CodeThisReferenceExpression(),
                    "_configurationData"));
            getConfigureationMethod.Statements.Add(returnStatement);
            return getConfigureationMethod;
        }

        private CodeMemberMethod GetBuildOperations(MigrationGenerationArguments args)
        {
            var buildOperationsMethod = new CodeMemberMethod
            {
                Name = "BuildOperations",
                ReturnType = new CodeTypeReference(typeof (void))
            };
            var builderParam = new CodeParameterDeclarationExpression(typeof (IDdlOperationBuilderSurface), "builder");
            buildOperationsMethod.Parameters.Add(
                builderParam);
            buildOperationsMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            var opGens = new Dictionary<System.Type, IDdlOperationStatementGenerator>();
            opGens.Add(typeof(AddTableCommentsDdlOperation), new AddTableCommentsStatementGenerator() );
            opGens.Add(typeof(EnableForeignKeyConstratintDdlOperation), new EnableForeignKeyConstraintStatementGenerator());
            opGens.Add(typeof(DisableForeignKeyConstraintDdlOperation), new DisableForeignKeyConstraintStatementGenerator());
            opGens.Add(typeof(DropTableDdlOperation), new DropTableDdlOperationStatementGenerator());
            foreach (var op in args.Operations)
            {
                var gen = opGens[op.GetType()];
                foreach (var s in gen.GetStatements(op))
                {
                    buildOperationsMethod.Statements.Add(s);
                }

            }
            return buildOperationsMethod;
        }

        private CodeConstructor GetConstructor(MigrationGenerationArguments args)
        {
            var cons = new CodeConstructor {Attributes = MemberAttributes.Public};
            cons.BaseConstructorArgs.Add(new CodePrimitiveExpression(String.Format("{0:s}_{1}", DateTime.Now, args.Name)));
            cons.BaseConstructorArgs.Add(new CodePrimitiveExpression(args.Context));
            return cons;
        }
    }
}