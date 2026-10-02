using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class WayDataConfigure : IMessage<WayDataConfigure>, IMessage, IEquatable<WayDataConfigure>, IDeepCloneable<WayDataConfigure>, IBufferMessage
{
	private static readonly MessageParser<WayDataConfigure> _parser = new MessageParser<WayDataConfigure>(() => new WayDataConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private uint id_;

	public const int WayTypeFieldNumber = 2;

	private WayType wayType_;

	public const int WayParamFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_wayParam_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> wayParam_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<WayDataConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WayReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public uint Id
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
	public WayType WayType
	{
		get
		{
			return wayType_;
		}
		private set
		{
			wayType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> WayParam => wayParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayDataConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayDataConfigure(WayDataConfigure other)
		: this()
	{
		id_ = other.id_;
		wayType_ = other.wayType_;
		wayParam_ = other.wayParam_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayDataConfigure Clone()
	{
		return new WayDataConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WayDataConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(WayDataConfigure other)
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
		if (WayType != other.WayType)
		{
			return false;
		}
		if (!wayParam_.Equals(other.wayParam_))
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
		if (WayType != WayType.None)
		{
			num ^= WayType.GetHashCode();
		}
		num ^= wayParam_.GetHashCode();
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
			output.WriteFixed32(Id);
		}
		if (WayType != WayType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)WayType);
		}
		wayParam_.WriteTo(ref output, _repeated_wayParam_codec);
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
		if (WayType != WayType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)WayType);
		}
		num += wayParam_.CalculateSize(_repeated_wayParam_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(WayDataConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.WayType != WayType.None)
			{
				WayType = other.WayType;
			}
			wayParam_.Add(other.wayParam_);
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
				Id = input.ReadFixed32();
				break;
			case 16u:
				WayType = (WayType)input.ReadEnum();
				break;
			case 26u:
			case 29u:
				wayParam_.AddEntriesFrom(ref input, _repeated_wayParam_codec);
				break;
			}
		}
	}
}
