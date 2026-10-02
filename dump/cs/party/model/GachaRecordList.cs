using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GachaRecordList : IMessage<GachaRecordList>, IMessage, IEquatable<GachaRecordList>, IDeepCloneable<GachaRecordList>, IBufferMessage
{
	private static readonly MessageParser<GachaRecordList> _parser = new MessageParser<GachaRecordList>(() => new GachaRecordList());

	private UnknownFieldSet _unknownFields;

	public const int GachaIdFieldNumber = 1;

	private int gachaId_;

	public const int PoolIdFieldNumber = 3;

	private int poolId_;

	public const int RecordsFieldNumber = 2;

	private static readonly FieldCodec<GachaRecord> _repeated_records_codec = FieldCodec.ForMessage(18u, GachaRecord.Parser);

	private readonly RepeatedField<GachaRecord> records_ = new RepeatedField<GachaRecord>();

	public const int CountFieldNumber = 4;

	private int count_;

	public const int RewardCountFieldNumber = 5;

	private int rewardCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaRecordList> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[24];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GachaId
	{
		get
		{
			return gachaId_;
		}
		set
		{
			gachaId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PoolId
	{
		get
		{
			return poolId_;
		}
		set
		{
			poolId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaRecord> Records => records_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Count
	{
		get
		{
			return count_;
		}
		set
		{
			count_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardCount
	{
		get
		{
			return rewardCount_;
		}
		set
		{
			rewardCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecordList()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecordList(GachaRecordList other)
		: this()
	{
		gachaId_ = other.gachaId_;
		poolId_ = other.poolId_;
		records_ = other.records_.Clone();
		count_ = other.count_;
		rewardCount_ = other.rewardCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecordList Clone()
	{
		return new GachaRecordList(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaRecordList);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaRecordList other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GachaId != other.GachaId)
		{
			return false;
		}
		if (PoolId != other.PoolId)
		{
			return false;
		}
		if (!records_.Equals(other.records_))
		{
			return false;
		}
		if (Count != other.Count)
		{
			return false;
		}
		if (RewardCount != other.RewardCount)
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
		if (GachaId != 0)
		{
			num ^= GachaId.GetHashCode();
		}
		if (PoolId != 0)
		{
			num ^= PoolId.GetHashCode();
		}
		num ^= records_.GetHashCode();
		if (Count != 0)
		{
			num ^= Count.GetHashCode();
		}
		if (RewardCount != 0)
		{
			num ^= RewardCount.GetHashCode();
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
		if (GachaId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GachaId);
		}
		records_.WriteTo(ref output, _repeated_records_codec);
		if (PoolId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PoolId);
		}
		if (Count != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Count);
		}
		if (RewardCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(RewardCount);
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
		if (GachaId != 0)
		{
			num += 5;
		}
		if (PoolId != 0)
		{
			num += 5;
		}
		num += records_.CalculateSize(_repeated_records_codec);
		if (Count != 0)
		{
			num += 5;
		}
		if (RewardCount != 0)
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
	public void MergeFrom(GachaRecordList other)
	{
		if (other != null)
		{
			if (other.GachaId != 0)
			{
				GachaId = other.GachaId;
			}
			if (other.PoolId != 0)
			{
				PoolId = other.PoolId;
			}
			records_.Add(other.records_);
			if (other.Count != 0)
			{
				Count = other.Count;
			}
			if (other.RewardCount != 0)
			{
				RewardCount = other.RewardCount;
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
				GachaId = input.ReadSFixed32();
				break;
			case 18u:
				records_.AddEntriesFrom(ref input, _repeated_records_codec);
				break;
			case 29u:
				PoolId = input.ReadSFixed32();
				break;
			case 37u:
				Count = input.ReadSFixed32();
				break;
			case 45u:
				RewardCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
