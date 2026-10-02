using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class WelfareInfoConfigure : IMessage<WelfareInfoConfigure>, IMessage, IEquatable<WelfareInfoConfigure>, IDeepCloneable<WelfareInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<WelfareInfoConfigure> _parser = new MessageParser<WelfareInfoConfigure>(() => new WelfareInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int WelfareTypeFieldNumber = 1;

	private WelfareType welfareType_;

	public const int IsShowFieldNumber = 2;

	private bool isShow_;

	public const int NameFieldNumber = 3;

	private int name_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<WelfareInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WelfareReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WelfareType WelfareType
	{
		get
		{
			return welfareType_;
		}
		private set
		{
			welfareType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShow
	{
		get
		{
			return isShow_;
		}
		private set
		{
			isShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Name
	{
		get
		{
			return name_;
		}
		private set
		{
			name_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WelfareInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WelfareInfoConfigure(WelfareInfoConfigure other)
		: this()
	{
		welfareType_ = other.welfareType_;
		isShow_ = other.isShow_;
		name_ = other.name_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WelfareInfoConfigure Clone()
	{
		return new WelfareInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WelfareInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(WelfareInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (WelfareType != other.WelfareType)
		{
			return false;
		}
		if (IsShow != other.IsShow)
		{
			return false;
		}
		if (Name != other.Name)
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
		if (WelfareType != WelfareType.None)
		{
			num ^= WelfareType.GetHashCode();
		}
		if (IsShow)
		{
			num ^= IsShow.GetHashCode();
		}
		if (Name != 0)
		{
			num ^= Name.GetHashCode();
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
		if (WelfareType != WelfareType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)WelfareType);
		}
		if (IsShow)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsShow);
		}
		if (Name != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Name);
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
		if (WelfareType != WelfareType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)WelfareType);
		}
		if (IsShow)
		{
			num += 2;
		}
		if (Name != 0)
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
	public void MergeFrom(WelfareInfoConfigure other)
	{
		if (other != null)
		{
			if (other.WelfareType != WelfareType.None)
			{
				WelfareType = other.WelfareType;
			}
			if (other.IsShow)
			{
				IsShow = other.IsShow;
			}
			if (other.Name != 0)
			{
				Name = other.Name;
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
				WelfareType = (WelfareType)input.ReadEnum();
				break;
			case 16u:
				IsShow = input.ReadBool();
				break;
			case 29u:
				Name = input.ReadSFixed32();
				break;
			}
		}
	}
}
