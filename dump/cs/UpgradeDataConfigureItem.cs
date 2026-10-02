using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class UpgradeDataConfigureItem : IMessage<UpgradeDataConfigureItem>, IMessage, IEquatable<UpgradeDataConfigureItem>, IDeepCloneable<UpgradeDataConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<UpgradeDataConfigureItem> _parser = new MessageParser<UpgradeDataConfigureItem>(() => new UpgradeDataConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int StarFieldNumber = 1;

	private int star_;

	public const int GoldFieldNumber = 2;

	private int gold_;

	public const int GoldcostFieldNumber = 3;

	private int goldcost_;

	public const int CostEnhanceFieldNumber = 4;

	private int costEnhance_;

	public const int RelicWeightsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_relicWeights_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> relicWeights_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UpgradeDataConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UpgradeReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Star
	{
		get
		{
			return star_;
		}
		private set
		{
			star_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Gold
	{
		get
		{
			return gold_;
		}
		private set
		{
			gold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Goldcost
	{
		get
		{
			return goldcost_;
		}
		private set
		{
			goldcost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CostEnhance
	{
		get
		{
			return costEnhance_;
		}
		private set
		{
			costEnhance_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RelicWeights => relicWeights_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpgradeDataConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpgradeDataConfigureItem(UpgradeDataConfigureItem other)
		: this()
	{
		star_ = other.star_;
		gold_ = other.gold_;
		goldcost_ = other.goldcost_;
		costEnhance_ = other.costEnhance_;
		relicWeights_ = other.relicWeights_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpgradeDataConfigureItem Clone()
	{
		return new UpgradeDataConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UpgradeDataConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UpgradeDataConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Star != other.Star)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (Goldcost != other.Goldcost)
		{
			return false;
		}
		if (CostEnhance != other.CostEnhance)
		{
			return false;
		}
		if (!relicWeights_.Equals(other.relicWeights_))
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
		if (Star != 0)
		{
			num ^= Star.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (Goldcost != 0)
		{
			num ^= Goldcost.GetHashCode();
		}
		if (CostEnhance != 0)
		{
			num ^= CostEnhance.GetHashCode();
		}
		num ^= relicWeights_.GetHashCode();
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
		if (Star != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Star);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Gold);
		}
		if (Goldcost != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Goldcost);
		}
		if (CostEnhance != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CostEnhance);
		}
		relicWeights_.WriteTo(ref output, _repeated_relicWeights_codec);
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
		if (Star != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (Goldcost != 0)
		{
			num += 5;
		}
		if (CostEnhance != 0)
		{
			num += 5;
		}
		num += relicWeights_.CalculateSize(_repeated_relicWeights_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UpgradeDataConfigureItem other)
	{
		if (other != null)
		{
			if (other.Star != 0)
			{
				Star = other.Star;
			}
			if (other.Gold != 0)
			{
				Gold = other.Gold;
			}
			if (other.Goldcost != 0)
			{
				Goldcost = other.Goldcost;
			}
			if (other.CostEnhance != 0)
			{
				CostEnhance = other.CostEnhance;
			}
			relicWeights_.Add(other.relicWeights_);
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
				Star = input.ReadSFixed32();
				break;
			case 21u:
				Gold = input.ReadSFixed32();
				break;
			case 29u:
				Goldcost = input.ReadSFixed32();
				break;
			case 37u:
				CostEnhance = input.ReadSFixed32();
				break;
			case 42u:
			case 45u:
				relicWeights_.AddEntriesFrom(ref input, _repeated_relicWeights_codec);
				break;
			}
		}
	}
}
