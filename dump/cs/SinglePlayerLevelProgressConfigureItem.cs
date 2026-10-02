using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerLevelProgressConfigureItem : IMessage<SinglePlayerLevelProgressConfigureItem>, IMessage, IEquatable<SinglePlayerLevelProgressConfigureItem>, IDeepCloneable<SinglePlayerLevelProgressConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerLevelProgressConfigureItem> _parser = new MessageParser<SinglePlayerLevelProgressConfigureItem>(() => new SinglePlayerLevelProgressConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int MissionIndexFieldNumber = 1;

	private int missionIndex_;

	public const int ProgressValueFieldNumber = 2;

	private int progressValue_;

	public const int NeedMoneyFieldNumber = 3;

	private int needMoney_;

	public const int CardWeightsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_cardWeights_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> cardWeights_ = new RepeatedField<int>();

	public const int CardRewardFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_cardReward_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> cardReward_ = new RepeatedField<int>();

	public const int RelicRewardFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_relicReward_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> relicReward_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerLevelProgressConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[10];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MissionIndex
	{
		get
		{
			return missionIndex_;
		}
		private set
		{
			missionIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProgressValue
	{
		get
		{
			return progressValue_;
		}
		private set
		{
			progressValue_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NeedMoney
	{
		get
		{
			return needMoney_;
		}
		private set
		{
			needMoney_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardWeights => cardWeights_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardReward => cardReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RelicReward => relicReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelProgressConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelProgressConfigureItem(SinglePlayerLevelProgressConfigureItem other)
		: this()
	{
		missionIndex_ = other.missionIndex_;
		progressValue_ = other.progressValue_;
		needMoney_ = other.needMoney_;
		cardWeights_ = other.cardWeights_.Clone();
		cardReward_ = other.cardReward_.Clone();
		relicReward_ = other.relicReward_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelProgressConfigureItem Clone()
	{
		return new SinglePlayerLevelProgressConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerLevelProgressConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerLevelProgressConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MissionIndex != other.MissionIndex)
		{
			return false;
		}
		if (ProgressValue != other.ProgressValue)
		{
			return false;
		}
		if (NeedMoney != other.NeedMoney)
		{
			return false;
		}
		if (!cardWeights_.Equals(other.cardWeights_))
		{
			return false;
		}
		if (!cardReward_.Equals(other.cardReward_))
		{
			return false;
		}
		if (!relicReward_.Equals(other.relicReward_))
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
		if (MissionIndex != 0)
		{
			num ^= MissionIndex.GetHashCode();
		}
		if (ProgressValue != 0)
		{
			num ^= ProgressValue.GetHashCode();
		}
		if (NeedMoney != 0)
		{
			num ^= NeedMoney.GetHashCode();
		}
		num ^= cardWeights_.GetHashCode();
		num ^= cardReward_.GetHashCode();
		num ^= relicReward_.GetHashCode();
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
		if (MissionIndex != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MissionIndex);
		}
		if (ProgressValue != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ProgressValue);
		}
		if (NeedMoney != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NeedMoney);
		}
		cardWeights_.WriteTo(ref output, _repeated_cardWeights_codec);
		cardReward_.WriteTo(ref output, _repeated_cardReward_codec);
		relicReward_.WriteTo(ref output, _repeated_relicReward_codec);
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
		if (MissionIndex != 0)
		{
			num += 5;
		}
		if (ProgressValue != 0)
		{
			num += 5;
		}
		if (NeedMoney != 0)
		{
			num += 5;
		}
		num += cardWeights_.CalculateSize(_repeated_cardWeights_codec);
		num += cardReward_.CalculateSize(_repeated_cardReward_codec);
		num += relicReward_.CalculateSize(_repeated_relicReward_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerLevelProgressConfigureItem other)
	{
		if (other != null)
		{
			if (other.MissionIndex != 0)
			{
				MissionIndex = other.MissionIndex;
			}
			if (other.ProgressValue != 0)
			{
				ProgressValue = other.ProgressValue;
			}
			if (other.NeedMoney != 0)
			{
				NeedMoney = other.NeedMoney;
			}
			cardWeights_.Add(other.cardWeights_);
			cardReward_.Add(other.cardReward_);
			relicReward_.Add(other.relicReward_);
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
				MissionIndex = input.ReadSFixed32();
				break;
			case 21u:
				ProgressValue = input.ReadSFixed32();
				break;
			case 29u:
				NeedMoney = input.ReadSFixed32();
				break;
			case 34u:
			case 37u:
				cardWeights_.AddEntriesFrom(ref input, _repeated_cardWeights_codec);
				break;
			case 42u:
			case 45u:
				cardReward_.AddEntriesFrom(ref input, _repeated_cardReward_codec);
				break;
			case 50u:
			case 53u:
				relicReward_.AddEntriesFrom(ref input, _repeated_relicReward_codec);
				break;
			}
		}
	}
}
