using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GachaCountRewardS2C : IMessage<GachaCountRewardS2C>, IMessage, IEquatable<GachaCountRewardS2C>, IDeepCloneable<GachaCountRewardS2C>, IBufferMessage
{
	private static readonly MessageParser<GachaCountRewardS2C> _parser = new MessageParser<GachaCountRewardS2C>(() => new GachaCountRewardS2C());

	private UnknownFieldSet _unknownFields;

	public const int RewardCountFieldNumber = 1;

	private int rewardCount_;

	public const int PoolIdFieldNumber = 2;

	private int poolId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaCountRewardS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[205];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public GachaCountRewardS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCountRewardS2C(GachaCountRewardS2C other)
		: this()
	{
		rewardCount_ = other.rewardCount_;
		poolId_ = other.poolId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCountRewardS2C Clone()
	{
		return new GachaCountRewardS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaCountRewardS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaCountRewardS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RewardCount != other.RewardCount)
		{
			return false;
		}
		if (PoolId != other.PoolId)
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
		if (RewardCount != 0)
		{
			num ^= RewardCount.GetHashCode();
		}
		if (PoolId != 0)
		{
			num ^= PoolId.GetHashCode();
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
		if (RewardCount != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(RewardCount);
		}
		if (PoolId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(PoolId);
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
		if (RewardCount != 0)
		{
			num += 5;
		}
		if (PoolId != 0)
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
	public void MergeFrom(GachaCountRewardS2C other)
	{
		if (other != null)
		{
			if (other.RewardCount != 0)
			{
				RewardCount = other.RewardCount;
			}
			if (other.PoolId != 0)
			{
				PoolId = other.PoolId;
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
				RewardCount = input.ReadSFixed32();
				break;
			case 21u:
				PoolId = input.ReadSFixed32();
				break;
			}
		}
	}
}
