using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FontInfoConfigure : IMessage<FontInfoConfigure>, IMessage, IEquatable<FontInfoConfigure>, IDeepCloneable<FontInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<FontInfoConfigure> _parser = new MessageParser<FontInfoConfigure>(() => new FontInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int LoadedKeyCNFieldNumber = 3;

	private string loadedKeyCN_ = "";

	public const int LoadedKeyENFieldNumber = 4;

	private string loadedKeyEN_ = "";

	public const int LoadedKeyJPFieldNumber = 5;

	private string loadedKeyJP_ = "";

	public const int LoadedKeyCHTFieldNumber = 6;

	private string loadedKeyCHT_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FontInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FontReflection.Descriptor.MessageTypes[0];

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
	public string Name
	{
		get
		{
			return name_;
		}
		private set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeyCN
	{
		get
		{
			return loadedKeyCN_;
		}
		private set
		{
			loadedKeyCN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeyEN
	{
		get
		{
			return loadedKeyEN_;
		}
		private set
		{
			loadedKeyEN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeyJP
	{
		get
		{
			return loadedKeyJP_;
		}
		private set
		{
			loadedKeyJP_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeyCHT
	{
		get
		{
			return loadedKeyCHT_;
		}
		private set
		{
			loadedKeyCHT_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FontInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FontInfoConfigure(FontInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		name_ = other.name_;
		loadedKeyCN_ = other.loadedKeyCN_;
		loadedKeyEN_ = other.loadedKeyEN_;
		loadedKeyJP_ = other.loadedKeyJP_;
		loadedKeyCHT_ = other.loadedKeyCHT_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FontInfoConfigure Clone()
	{
		return new FontInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FontInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FontInfoConfigure other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (LoadedKeyCN != other.LoadedKeyCN)
		{
			return false;
		}
		if (LoadedKeyEN != other.LoadedKeyEN)
		{
			return false;
		}
		if (LoadedKeyJP != other.LoadedKeyJP)
		{
			return false;
		}
		if (LoadedKeyCHT != other.LoadedKeyCHT)
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (LoadedKeyCN.Length != 0)
		{
			num ^= LoadedKeyCN.GetHashCode();
		}
		if (LoadedKeyEN.Length != 0)
		{
			num ^= LoadedKeyEN.GetHashCode();
		}
		if (LoadedKeyJP.Length != 0)
		{
			num ^= LoadedKeyJP.GetHashCode();
		}
		if (LoadedKeyCHT.Length != 0)
		{
			num ^= LoadedKeyCHT.GetHashCode();
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
		if (Name.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Name);
		}
		if (LoadedKeyCN.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(LoadedKeyCN);
		}
		if (LoadedKeyEN.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(LoadedKeyEN);
		}
		if (LoadedKeyJP.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(LoadedKeyJP);
		}
		if (LoadedKeyCHT.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(LoadedKeyCHT);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (LoadedKeyCN.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyCN);
		}
		if (LoadedKeyEN.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyEN);
		}
		if (LoadedKeyJP.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyJP);
		}
		if (LoadedKeyCHT.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyCHT);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FontInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Name.Length != 0)
			{
				Name = other.Name;
			}
			if (other.LoadedKeyCN.Length != 0)
			{
				LoadedKeyCN = other.LoadedKeyCN;
			}
			if (other.LoadedKeyEN.Length != 0)
			{
				LoadedKeyEN = other.LoadedKeyEN;
			}
			if (other.LoadedKeyJP.Length != 0)
			{
				LoadedKeyJP = other.LoadedKeyJP;
			}
			if (other.LoadedKeyCHT.Length != 0)
			{
				LoadedKeyCHT = other.LoadedKeyCHT;
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
				Name = input.ReadString();
				break;
			case 26u:
				LoadedKeyCN = input.ReadString();
				break;
			case 34u:
				LoadedKeyEN = input.ReadString();
				break;
			case 42u:
				LoadedKeyJP = input.ReadString();
				break;
			case 50u:
				LoadedKeyCHT = input.ReadString();
				break;
			}
		}
	}
}
