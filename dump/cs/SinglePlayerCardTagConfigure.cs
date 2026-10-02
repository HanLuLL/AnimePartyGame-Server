using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerCardTagConfigure : IMessage<SinglePlayerCardTagConfigure>, IMessage, IEquatable<SinglePlayerCardTagConfigure>, IDeepCloneable<SinglePlayerCardTagConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerCardTagConfigure> _parser = new MessageParser<SinglePlayerCardTagConfigure>(() => new SinglePlayerCardTagConfigure());

	private UnknownFieldSet _unknownFields;

	public const int SinglePlayerTagTypeFieldNumber = 1;

	private SinglePlayerTagType singlePlayerTagType_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerCardTagConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[17];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerTagType SinglePlayerTagType
	{
		get
		{
			return singlePlayerTagType_;
		}
		private set
		{
			singlePlayerTagType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardTagConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardTagConfigure(SinglePlayerCardTagConfigure other)
		: this()
	{
		singlePlayerTagType_ = other.singlePlayerTagType_;
		nameID_ = other.nameID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardTagConfigure Clone()
	{
		return new SinglePlayerCardTagConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerCardTagConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerCardTagConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SinglePlayerTagType != other.SinglePlayerTagType)
		{
			return false;
		}
		if (NameID != other.NameID)
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
		if (SinglePlayerTagType != SinglePlayerTagType.None)
		{
			num ^= SinglePlayerTagType.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
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
		if (SinglePlayerTagType != SinglePlayerTagType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)SinglePlayerTagType);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
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
		if (SinglePlayerTagType != SinglePlayerTagType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SinglePlayerTagType);
		}
		if (NameID != 0)
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
	public void MergeFrom(SinglePlayerCardTagConfigure other)
	{
		if (other != null)
		{
			if (other.SinglePlayerTagType != SinglePlayerTagType.None)
			{
				SinglePlayerTagType = other.SinglePlayerTagType;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
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
			case 8u:
				SinglePlayerTagType = (SinglePlayerTagType)input.ReadEnum();
				break;
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			}
		}
	}
}
