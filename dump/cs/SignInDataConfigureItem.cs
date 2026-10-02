using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SignInDataConfigureItem : IMessage<SignInDataConfigureItem>, IMessage, IEquatable<SignInDataConfigureItem>, IDeepCloneable<SignInDataConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<SignInDataConfigureItem> _parser = new MessageParser<SignInDataConfigureItem>(() => new SignInDataConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int DayFieldNumber = 1;

	private int day_;

	public const int RewardFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int RechargeRewardFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_rechargeReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> rechargeReward_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SignInDataConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SignInReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Day
	{
		get
		{
			return day_;
		}
		private set
		{
			day_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RechargeReward => rechargeReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInDataConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInDataConfigureItem(SignInDataConfigureItem other)
		: this()
	{
		day_ = other.day_;
		reward_ = other.reward_.Clone();
		rechargeReward_ = other.rechargeReward_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInDataConfigureItem Clone()
	{
		return new SignInDataConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SignInDataConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SignInDataConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Day != other.Day)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
		{
			return false;
		}
		if (!RechargeReward.Equals(other.RechargeReward))
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
		if (Day != 0)
		{
			num ^= Day.GetHashCode();
		}
		num ^= Reward.GetHashCode();
		num ^= RechargeReward.GetHashCode();
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
		if (Day != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Day);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		rechargeReward_.WriteTo(ref output, _map_rechargeReward_codec);
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
		if (Day != 0)
		{
			num += 5;
		}
		num += reward_.CalculateSize(_map_reward_codec);
		num += rechargeReward_.CalculateSize(_map_rechargeReward_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SignInDataConfigureItem other)
	{
		if (other != null)
		{
			if (other.Day != 0)
			{
				Day = other.Day;
			}
			reward_.MergeFrom(other.reward_);
			rechargeReward_.MergeFrom(other.rechargeReward_);
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
				Day = input.ReadSFixed32();
				break;
			case 18u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 26u:
				rechargeReward_.AddEntriesFrom(ref input, _map_rechargeReward_codec);
				break;
			}
		}
	}
}
