using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ServerErrorConfigure : IMessage<ServerErrorConfigure>, IMessage, IEquatable<ServerErrorConfigure>, IDeepCloneable<ServerErrorConfigure>, IBufferMessage
{
	private static readonly MessageParser<ServerErrorConfigure> _parser = new MessageParser<ServerErrorConfigure>(() => new ServerErrorConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ServerErrorDealTypeFieldNumber = 2;

	private ServerErrorDealType serverErrorDealType_;

	public const int ServerErrorShowTypeFieldNumber = 3;

	private ServerErrorShowType serverErrorShowType_;

	public const int DescIdFieldNumber = 4;

	private int descId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ServerErrorConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ServerReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerErrorDealType ServerErrorDealType
	{
		get
		{
			return serverErrorDealType_;
		}
		private set
		{
			serverErrorDealType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerErrorShowType ServerErrorShowType
	{
		get
		{
			return serverErrorShowType_;
		}
		private set
		{
			serverErrorShowType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescId
	{
		get
		{
			return descId_;
		}
		private set
		{
			descId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerErrorConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerErrorConfigure(ServerErrorConfigure other)
		: this()
	{
		id_ = other.id_;
		serverErrorDealType_ = other.serverErrorDealType_;
		serverErrorShowType_ = other.serverErrorShowType_;
		descId_ = other.descId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerErrorConfigure Clone()
	{
		return new ServerErrorConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ServerErrorConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ServerErrorConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (ServerErrorDealType != other.ServerErrorDealType)
		{
			return false;
		}
		if (ServerErrorShowType != other.ServerErrorShowType)
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (ServerErrorDealType != ServerErrorDealType.None)
		{
			num ^= ServerErrorDealType.GetHashCode();
		}
		if (ServerErrorShowType != ServerErrorShowType.None)
		{
			num ^= ServerErrorShowType.GetHashCode();
		}
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext output)
	{
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (ServerErrorDealType != ServerErrorDealType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)ServerErrorDealType);
		}
		if (ServerErrorShowType != ServerErrorShowType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)ServerErrorShowType);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DescId);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref output);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (Id != 0)
		{
			num += 5;
		}
		if (ServerErrorDealType != ServerErrorDealType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ServerErrorDealType);
		}
		if (ServerErrorShowType != ServerErrorShowType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ServerErrorShowType);
		}
		if (DescId != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ServerErrorConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ServerErrorDealType != ServerErrorDealType.None)
			{
				ServerErrorDealType = other.ServerErrorDealType;
			}
			if (other.ServerErrorShowType != ServerErrorShowType.None)
			{
				ServerErrorShowType = other.ServerErrorShowType;
			}
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext input)
	{
		uint num;
		while ((num = input.ReadTag()) != 0)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				break;
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 16u:
				ServerErrorDealType = (ServerErrorDealType)input.ReadEnum();
				break;
			case 24u:
				ServerErrorShowType = (ServerErrorShowType)input.ReadEnum();
				break;
			case 37u:
				DescId = input.ReadSFixed32();
				break;
			}
		}
	}
}
