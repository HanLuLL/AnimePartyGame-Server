using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class UIWindowConfigure : IMessage<UIWindowConfigure>, IMessage, IEquatable<UIWindowConfigure>, IDeepCloneable<UIWindowConfigure>, IBufferMessage
{
	private static readonly MessageParser<UIWindowConfigure> _parser = new MessageParser<UIWindowConfigure>(() => new UIWindowConfigure());

	private UnknownFieldSet _unknownFields;

	public const int WindowTypeFieldNumber = 1;

	private UIWindowType windowType_;

	public const int PackageNameFieldNumber = 2;

	private string packageName_ = "";

	public const int LayerFieldNumber = 3;

	private int layer_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UIWindowConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UIReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIWindowType WindowType
	{
		get
		{
			return windowType_;
		}
		private set
		{
			windowType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PackageName
	{
		get
		{
			return packageName_;
		}
		private set
		{
			packageName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Layer
	{
		get
		{
			return layer_;
		}
		private set
		{
			layer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIWindowConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIWindowConfigure(UIWindowConfigure other)
		: this()
	{
		windowType_ = other.windowType_;
		packageName_ = other.packageName_;
		layer_ = other.layer_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIWindowConfigure Clone()
	{
		return new UIWindowConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UIWindowConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UIWindowConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (WindowType != other.WindowType)
		{
			return false;
		}
		if (PackageName != other.PackageName)
		{
			return false;
		}
		if (Layer != other.Layer)
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
		if (WindowType != UIWindowType.None)
		{
			num ^= WindowType.GetHashCode();
		}
		if (PackageName.Length != 0)
		{
			num ^= PackageName.GetHashCode();
		}
		if (Layer != 0)
		{
			num ^= Layer.GetHashCode();
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
		if (WindowType != UIWindowType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)WindowType);
		}
		if (PackageName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(PackageName);
		}
		if (Layer != 0)
		{
			output.WriteRawTag(24);
			output.WriteSInt32(Layer);
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
		if (WindowType != UIWindowType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)WindowType);
		}
		if (PackageName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PackageName);
		}
		if (Layer != 0)
		{
			num += 1 + CodedOutputStream.ComputeSInt32Size(Layer);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UIWindowConfigure other)
	{
		if (other != null)
		{
			if (other.WindowType != UIWindowType.None)
			{
				WindowType = other.WindowType;
			}
			if (other.PackageName.Length != 0)
			{
				PackageName = other.PackageName;
			}
			if (other.Layer != 0)
			{
				Layer = other.Layer;
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
				WindowType = (UIWindowType)input.ReadEnum();
				break;
			case 18u:
				PackageName = input.ReadString();
				break;
			case 24u:
				Layer = input.ReadSInt32();
				break;
			}
		}
	}
}
