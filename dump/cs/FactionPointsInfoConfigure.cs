using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FactionPointsInfoConfigure : IMessage<FactionPointsInfoConfigure>, IMessage, IEquatable<FactionPointsInfoConfigure>, IDeepCloneable<FactionPointsInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<FactionPointsInfoConfigure> _parser = new MessageParser<FactionPointsInfoConfigure>(() => new FactionPointsInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapIDFieldNumber = 1;

	private int mapID_;

	public const int ActivityIDFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_activityID_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> activityID_ = new RepeatedField<int>();

	public const int VoteListFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_voteList_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> voteList_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FactionPointsInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FactionPointsReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapID
	{
		get
		{
			return mapID_;
		}
		private set
		{
			mapID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ActivityID => activityID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> VoteList => voteList_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FactionPointsInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FactionPointsInfoConfigure(FactionPointsInfoConfigure other)
		: this()
	{
		mapID_ = other.mapID_;
		activityID_ = other.activityID_.Clone();
		voteList_ = other.voteList_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FactionPointsInfoConfigure Clone()
	{
		return new FactionPointsInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FactionPointsInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FactionPointsInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapID != other.MapID)
		{
			return false;
		}
		if (!activityID_.Equals(other.activityID_))
		{
			return false;
		}
		if (!voteList_.Equals(other.voteList_))
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
		if (MapID != 0)
		{
			num ^= MapID.GetHashCode();
		}
		num ^= activityID_.GetHashCode();
		num ^= voteList_.GetHashCode();
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
		if (MapID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MapID);
		}
		activityID_.WriteTo(ref output, _repeated_activityID_codec);
		voteList_.WriteTo(ref output, _repeated_voteList_codec);
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
		if (MapID != 0)
		{
			num += 5;
		}
		num += activityID_.CalculateSize(_repeated_activityID_codec);
		num += voteList_.CalculateSize(_repeated_voteList_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FactionPointsInfoConfigure other)
	{
		if (other != null)
		{
			if (other.MapID != 0)
			{
				MapID = other.MapID;
			}
			activityID_.Add(other.activityID_);
			voteList_.Add(other.voteList_);
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
				MapID = input.ReadSFixed32();
				break;
			case 18u:
			case 21u:
				activityID_.AddEntriesFrom(ref input, _repeated_activityID_codec);
				break;
			case 26u:
			case 29u:
				voteList_.AddEntriesFrom(ref input, _repeated_voteList_codec);
				break;
			}
		}
	}
}
