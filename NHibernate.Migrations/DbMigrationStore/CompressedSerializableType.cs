using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.Type;
using SerializationException = NHibernate.Type.SerializationException;

namespace NHibernate.Migrations.DbMigrationStore
{
    [Serializable]
    public class CompressedSerializableType: MutableType
	{
		private readonly System.Type serializableClass;
		private readonly BinaryType binaryType;

		internal CompressedSerializableType() : this(typeof(Object))
		{
		}

		internal CompressedSerializableType(System.Type serializableClass) : base(new BinarySqlType())
		{
			this.serializableClass = serializableClass;
			binaryType = (BinaryType) NHibernateUtil.Binary;
		}

        internal CompressedSerializableType(System.Type serializableClass, BinarySqlType sqlType)
            : base(sqlType)
		{
			this.serializableClass = serializableClass;
			binaryType = (BinaryType) TypeFactory.GetBinaryType(sqlType.Length);
		}

		public override void Set(IDbCommand st, object value, int index)
		{
			binaryType.Set(st, ToBytes(value), index);
		}

		public override object Get(IDataReader rs, string name)
		{
			return Get(rs, rs.GetOrdinal(name));
		}

		public override object Get(IDataReader rs, int index)
		{
			byte[] bytes = (byte[]) binaryType.Get(rs, index);
			if (bytes == null)
			{
				return null;
			}
			else
			{
				return FromBytes(bytes);
			}
		}

		public override System.Type ReturnedClass
		{
			get { return serializableClass; }
		}

		public override bool IsEqual(object x, object y)
		{
			if (x == y)
				return true;

			if (x == null || y == null)
				return false;

			return x.Equals(y) || binaryType.IsEqual(ToBytes(x), ToBytes(y));
		}

		public override int GetHashCode(Object x, EntityMode entityMode)
		{
			return binaryType.GetHashCode(ToBytes(x), entityMode);
		}

		public override string ToString(object value)
		{
			return binaryType.ToString(ToBytes(value));
		}

		public override object FromStringValue(string xml)
		{
			return FromBytes((byte[])binaryType.FromStringValue(xml));
		}

		/// <summary></summary>
		public override string Name
		{
			get
			{
				return serializableClass == typeof(ISerializable) ? "serializable" : serializableClass.FullName;
			} 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object DeepCopyNotNull(object value)
		{
			return FromBytes(ToBytes(value));
		}

		private byte[] ToBytes(object obj)
		{
			try
			{
				BinaryFormatter bf = new BinaryFormatter();
				MemoryStream stream = new MemoryStream();
			    var comp = new System.IO.Compression.GZipStream(stream, CompressionMode.Compress);
				bf.Serialize(comp, obj);
                comp.Flush();
				return stream.ToArray();
			}
			catch (Exception e)
			{
				throw new SerializationException("Could not serialize a serializable property: ", e);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public object FromBytes(byte[] bytes)
		{
			try
			{
				var bf = new BinaryFormatter();
				return bf.Deserialize(new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress));
			}
			catch (Exception e)
			{
				throw new SerializationException("Could not deserialize a serializable property: ", e);
			}
		}

		public override object Assemble(object cached, ISessionImplementor session, object owner)
		{
			return (cached == null) ? null : FromBytes((byte[]) cached);
		}

		public override object Disassemble(object value, ISessionImplementor session, object owner)
		{
			return (value == null) ? null : ToBytes(value);
		}
	}
}
