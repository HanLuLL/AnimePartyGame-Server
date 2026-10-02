using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class BattleResourcePostProcessConfigure : IMessage<BattleResourcePostProcessConfigure>, IMessage, IEquatable<BattleResourcePostProcessConfigure>, IDeepCloneable<BattleResourcePostProcessConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattleResourcePostProcessConfigure> _parser = new MessageParser<BattleResourcePostProcessConfigure>(() => new BattleResourcePostProcessConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ThresholdFieldNumber = 2;

	private int threshold_;

	public const int IntensityFieldNumber = 3;

	private int intensity_;

	public const int ScatterFieldNumber = 4;

	private int scatter_;

	public const int TintFieldNumber = 5;

	private string tint_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattleResourcePostProcessConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattleResourceReflection.Descriptor.MessageTypes[1];

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
	public int Threshold
	{
		get
		{
			return threshold_;
		}
		private set
		{
			threshold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Intensity
	{
		get
		{
			return intensity_;
		}
		private set
		{
			intensity_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Scatter
	{
		get
		{
			return scatter_;
		}
		private set
		{
			scatter_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Tint
	{
		get
		{
			return tint_;
		}
		private set
		{
			tint_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourcePostProcessConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourcePostProcessConfigure(BattleResourcePostProcessConfigure other)
		: this()
	{
		id_ = other.id_;
		threshold_ = other.threshold_;
		intensity_ = other.intensity_;
		scatter_ = other.scatter_;
		tint_ = other.tint_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourcePostProcessConfigure Clone()
	{
		return new BattleResourcePostProcessConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattleResourcePostProcessConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattleResourcePostProcessConfigure other)
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
		if (Threshold != other.Threshold)
		{
			return false;
		}
		if (Intensity != other.Intensity)
		{
			return false;
		}
		if (Scatter != other.Scatter)
		{
			return false;
		}
		if (Tint != other.Tint)
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
		if (Threshold != 0)
		{
			num ^= Threshold.GetHashCode();
		}
		if (Intensity != 0)
		{
			num ^= Intensity.GetHashCode();
		}
		if (Scatter != 0)
		{
			num ^= Scatter.GetHashCode();
		}
		if (Tint.Length != 0)
		{
			num ^= Tint.GetHashCode();
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
		if (Threshold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Threshold);
		}
		if (Intensity != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Intensity);
		}
		if (Scatter != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Scatter);
		}
		if (Tint.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Tint);
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
		if (Threshold != 0)
		{
			num += 5;
		}
		if (Intensity != 0)
		{
			num += 5;
		}
		if (Scatter != 0)
		{
			num += 5;
		}
		if (Tint.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Tint);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattleResourcePostProcessConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Threshold != 0)
			{
				Threshold = other.Threshold;
			}
			if (other.Intensity != 0)
			{
				Intensity = other.Intensity;
			}
			if (other.Scatter != 0)
			{
				Scatter = other.Scatter;
			}
			if (other.Tint.Length != 0)
			{
				Tint = other.Tint;
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
			case 21u:
				Threshold = input.ReadSFixed32();
				break;
			case 29u:
				Intensity = input.ReadSFixed32();
				break;
			case 37u:
				Scatter = input.ReadSFixed32();
				break;
			case 42u:
				Tint = input.ReadString();
				break;
			}
		}
	}
}
