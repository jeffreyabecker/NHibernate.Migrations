using NHibernate.Migrations.Fluent.Builders;
using NHibernate.SqlTypes;

namespace NHibernate.Migrations.Fluent
{
    public static class ColumnBuilderExtensions
    {

        public static FluentColumnModel Custom(this IColumnBuilder c, string sqlType, bool nullable = true,
                                               string sqlDefault = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }

        public static FluentColumnModel Custom(this IColumnBuilder c, SqlType sqlType, bool nullable = true,
                                       string sqlDefault = null)
        {
            return new FluentColumnModel
            {
                SqlTypeCode = sqlType,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }


        public static FluentColumnModel AnsiString(this IColumnBuilder c, int length, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.GetAnsiString(length),
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }

        public static FluentColumnModel Binary(this IColumnBuilder c, int length, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.GetBinary(length),
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }

        public static FluentColumnModel BinaryBlob(this IColumnBuilder c, int length, bool nullable = true, string sqlDefault = null,
                                                   string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.GetBinaryBlob(length),
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }

        public static FluentColumnModel String(this IColumnBuilder c, int length, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.GetString(length),
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel StringClob(this IColumnBuilder c, int length, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.GetStringClob(length),
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }

        public static FluentColumnModel Guid(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Guid,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Boolean(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Boolean,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Byte(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Byte,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Currency(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Currency,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Date(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Date,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel DateTime(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.DateTime,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel DateTime2(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.DateTime2,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel DateTimeOffSet(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.DateTimeOffSet,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Decimal(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Decimal,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Double(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Double,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Int16(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Int16,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Int32(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Int32,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Int64(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Int64,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel SByte(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.SByte,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Single(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Single,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel Time(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.Time,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel UInt16(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.UInt16,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel UInt32(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.UInt32,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        }
        public static FluentColumnModel UInt64(this IColumnBuilder c, bool nullable = true, string sqlDefault = null, string sqlType = null)
        {
            return new FluentColumnModel
            {
                SqlType = sqlType,
                SqlTypeCode = SqlTypeFactory.UInt64,
                Nullable = nullable,
                DefaultValue = sqlDefault
            };
        } 




    }
}