using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class RelicKeywordsConfigure : IMessage<RelicKeywordsConfigure>, IMessage, IEquatable<RelicKeywordsConfigure>, IDeepCloneable<RelicKeywordsConfigure>, IBufferMessage
{
	private static readonly MessageParser<RelicKeywordsConfigure> _parser = new MessageParser<RelicKeywordsConfigure>(() => new RelicKeywordsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int KeyWordTypeFieldNumber = 1;

	private RelicKeyWordType keyWordType_;

	public const int IconFieldNumber = 2;

	private string icon_ = "";

	public const int KeywordNameIDFieldNumber = 3;

	private int keywordNameID_;

	public const int KeywordDesIDFieldNumber = 4;

	private int keywordDesID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RelicKeywordsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RelicReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicKeyWordType KeyWordType
	{
		get
		{
			return keyWordType_;
		}
		private set
		{
			keyWordType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KeywordNameID
	{
		get
		{
			return keywordNameID_;
		}
		private set
		{
			keywordNameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KeywordDesID
	{
		get
		{
			return keywordDesID_;
		}
		private set
		{
			keywordDesID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicKeywordsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicKeywordsConfigure(RelicKeywordsConfigure other)
		: this()
	{
		keyWordType_ = other.keyWordType_;
		icon_ = other.icon_;
		keywordNameID_ = other.keywordNameID_;
		keywordDesID_ = other.keywordDesID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicKeywordsConfigure Clone()
	{
		return new RelicKeywordsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RelicKeywordsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RelicKeywordsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (KeyWordType != other.KeyWordType)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (KeywordNameID != other.KeywordNameID)
		{
			return false;
		}
		if (KeywordDesID != other.KeywordDesID)
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
		if (KeyWordType != RelicKeyWordType.None)
		{
			num ^= KeyWordType.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (KeywordNameID != 0)
		{
			num ^= KeywordNameID.GetHashCode();
		}
		if (KeywordDesID != 0)
		{
			num ^= KeywordDesID.GetHashCode();
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
		if (KeyWordType != RelicKeyWordType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)KeyWordType);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Icon);
		}
		if (KeywordNameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(KeywordNameID);
		}
		if (KeywordDesID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(KeywordDesID);
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
		if (KeyWordType != RelicKeyWordType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)KeyWordType);
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (KeywordNameID != 0)
		{
			num += 5;
		}
		if (KeywordDesID != 0)
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
	public void MergeFrom(RelicKeywordsConfigure other)
	{
		if (other != null)
		{
			if (other.KeyWordType != RelicKeyWordType.None)
			{
				KeyWordType = other.KeyWordType;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			if (other.KeywordNameID != 0)
			{
				KeywordNameID = other.KeywordNameID;
			}
			if (other.KeywordDesID != 0)
			{
				KeywordDesID = other.KeywordDesID;
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
				KeyWordType = (RelicKeyWordType)input.ReadEnum();
				break;
			case 18u:
				Icon = input.ReadString();
				break;
			case 29u:
				KeywordNameID = input.ReadSFixed32();
				break;
			case 37u:
				KeywordDesID = input.ReadSFixed32();
				break;
			}
		}
	}
}
