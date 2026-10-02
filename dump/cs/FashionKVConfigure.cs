using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FashionKVConfigure : IMessage<FashionKVConfigure>, IMessage, IEquatable<FashionKVConfigure>, IDeepCloneable<FashionKVConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionKVConfigure> _parser = new MessageParser<FashionKVConfigure>(() => new FashionKVConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int LoadedKeyFieldNumber = 2;

	private string loadedKey_ = "";

	public const int LoadedKeySFWFieldNumber = 3;

	private string loadedKeySFW_ = "";

	public const int CurrentFieldNumber = 4;

	private bool current_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionKVConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[5];

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
	public string LoadedKey
	{
		get
		{
			return loadedKey_;
		}
		private set
		{
			loadedKey_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeySFW
	{
		get
		{
			return loadedKeySFW_;
		}
		private set
		{
			loadedKeySFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Current
	{
		get
		{
			return current_;
		}
		private set
		{
			current_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionKVConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionKVConfigure(FashionKVConfigure other)
		: this()
	{
		id_ = other.id_;
		loadedKey_ = other.loadedKey_;
		loadedKeySFW_ = other.loadedKeySFW_;
		current_ = other.current_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionKVConfigure Clone()
	{
		return new FashionKVConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionKVConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionKVConfigure other)
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
		if (LoadedKey != other.LoadedKey)
		{
			return false;
		}
		if (LoadedKeySFW != other.LoadedKeySFW)
		{
			return false;
		}
		if (Current != other.Current)
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
		if (LoadedKey.Length != 0)
		{
			num ^= LoadedKey.GetHashCode();
		}
		if (LoadedKeySFW.Length != 0)
		{
			num ^= LoadedKeySFW.GetHashCode();
		}
		if (Current)
		{
			num ^= Current.GetHashCode();
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
		if (LoadedKey.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(LoadedKey);
		}
		if (LoadedKeySFW.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(LoadedKeySFW);
		}
		if (Current)
		{
			output.WriteRawTag(32);
			output.WriteBool(Current);
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
		if (LoadedKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKey);
		}
		if (LoadedKeySFW.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeySFW);
		}
		if (Current)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionKVConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.LoadedKey.Length != 0)
			{
				LoadedKey = other.LoadedKey;
			}
			if (other.LoadedKeySFW.Length != 0)
			{
				LoadedKeySFW = other.LoadedKeySFW;
			}
			if (other.Current)
			{
				Current = other.Current;
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
			case 18u:
				LoadedKey = input.ReadString();
				break;
			case 26u:
				LoadedKeySFW = input.ReadString();
				break;
			case 32u:
				Current = input.ReadBool();
				break;
			}
		}
	}
}
