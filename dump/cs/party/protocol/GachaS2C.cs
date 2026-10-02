using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GachaS2C : IMessage<GachaS2C>, IMessage, IEquatable<GachaS2C>, IDeepCloneable<GachaS2C>, IBufferMessage
{
	private static readonly MessageParser<GachaS2C> _parser = new MessageParser<GachaS2C>(() => new GachaS2C());

	private UnknownFieldSet _unknownFields;

	public const int RewardFieldNumber = 1;

	private static readonly FieldCodec<GachaReward> _repeated_reward_codec = FieldCodec.ForMessage(10u, GachaReward.Parser);

	private readonly RepeatedField<GachaReward> reward_ = new RepeatedField<GachaReward>();

	public const int CountFieldNumber = 2;

	private int count_;

	public const int PoolIdFieldNumber = 3;

	private int poolId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[203];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaReward> Reward => reward_;

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
	public GachaS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaS2C(GachaS2C other)
		: this()
	{
		reward_ = other.reward_.Clone();
		count_ = other.count_;
		poolId_ = other.poolId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaS2C Clone()
	{
		return new GachaS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!reward_.Equals(other.reward_))
		{
			return false;
		}
		if (Count != other.Count)
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
		num ^= reward_.GetHashCode();
		if (Count != 0)
		{
			num ^= Count.GetHashCode();
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
		reward_.WriteTo(ref output, _repeated_reward_codec);
		if (Count != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Count);
		}
		if (PoolId != 0)
		{
			output.WriteRawTag(29);
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
		num += reward_.CalculateSize(_repeated_reward_codec);
		if (Count != 0)
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
	public void MergeFrom(GachaS2C other)
	{
		if (other != null)
		{
			reward_.Add(other.reward_);
			if (other.Count != 0)
			{
				Count = other.Count;
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
			case 10u:
				reward_.AddEntriesFrom(ref input, _repeated_reward_codec);
				break;
			case 21u:
				Count = input.ReadSFixed32();
				break;
			case 29u:
				PoolId = input.ReadSFixed32();
				break;
			}
		}
	}
}
