using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FavorWayConfigure : IMessage<FavorWayConfigure>, IMessage, IEquatable<FavorWayConfigure>, IDeepCloneable<FavorWayConfigure>, IBufferMessage
{
	private static readonly MessageParser<FavorWayConfigure> _parser = new MessageParser<FavorWayConfigure>(() => new FavorWayConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private uint id_;

	public const int WayListFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_wayList_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> wayList_ = new RepeatedField<int>();

	public const int WayTypeFieldNumber = 3;

	private WayType wayType_;

	public const int WayParamFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_wayParam_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> wayParam_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FavorWayConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FavorReflection.Descriptor.MessageTypes[5];

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
	public RepeatedField<int> WayList => wayList_;

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
	public FavorWayConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FavorWayConfigure(FavorWayConfigure other)
		: this()
	{
		id_ = other.id_;
		wayList_ = other.wayList_.Clone();
		wayType_ = other.wayType_;
		wayParam_ = other.wayParam_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FavorWayConfigure Clone()
	{
		return new FavorWayConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FavorWayConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FavorWayConfigure other)
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
		if (!wayList_.Equals(other.wayList_))
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
		num ^= wayList_.GetHashCode();
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
		wayList_.WriteTo(ref output, _repeated_wayList_codec);
		if (WayType != WayType.None)
		{
			output.WriteRawTag(24);
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
		num += wayList_.CalculateSize(_repeated_wayList_codec);
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
	public void MergeFrom(FavorWayConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			wayList_.Add(other.wayList_);
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
			case 18u:
			case 21u:
				wayList_.AddEntriesFrom(ref input, _repeated_wayList_codec);
				break;
			case 24u:
				WayType = (WayType)input.ReadEnum();
				break;
			case 34u:
			case 37u:
				wayParam_.AddEntriesFrom(ref input, _repeated_wayParam_codec);
				break;
			}
		}
	}
}
