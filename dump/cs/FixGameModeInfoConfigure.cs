using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class FixGameModeInfoConfigure : IMessage<FixGameModeInfoConfigure>, IMessage, IEquatable<FixGameModeInfoConfigure>, IDeepCloneable<FixGameModeInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixGameModeInfoConfigure> _parser = new MessageParser<FixGameModeInfoConfigure>(() => new FixGameModeInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapModeTypeFieldNumber = 1;

	private MapModeType mapModeType_;

	public const int BeginTimeFieldNumber = 2;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 3;

	private Timestamp endTime_;

	public const int MapIDFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_mapID_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> mapID_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixGameModeInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixGameModeReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapModeType MapModeType
	{
		get
		{
			return mapModeType_;
		}
		private set
		{
			mapModeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapID => mapID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixGameModeInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixGameModeInfoConfigure(FixGameModeInfoConfigure other)
		: this()
	{
		mapModeType_ = other.mapModeType_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		mapID_ = other.mapID_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixGameModeInfoConfigure Clone()
	{
		return new FixGameModeInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixGameModeInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixGameModeInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapModeType != other.MapModeType)
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (!mapID_.Equals(other.mapID_))
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
		if (MapModeType != MapModeType.None)
		{
			num ^= MapModeType.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		num ^= mapID_.GetHashCode();
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
		if (MapModeType != MapModeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)MapModeType);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(EndTime);
		}
		mapID_.WriteTo(ref output, _repeated_mapID_codec);
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
		if (MapModeType != MapModeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MapModeType);
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		num += mapID_.CalculateSize(_repeated_mapID_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixGameModeInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.MapModeType != MapModeType.None)
		{
			MapModeType = other.MapModeType;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		mapID_.Add(other.mapID_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				MapModeType = (MapModeType)input.ReadEnum();
				break;
			case 18u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 26u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 34u:
			case 37u:
				mapID_.AddEntriesFrom(ref input, _repeated_mapID_codec);
				break;
			}
		}
	}
}
