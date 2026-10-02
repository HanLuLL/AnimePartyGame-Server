using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattlePassRewardConfigureItem : IMessage<BattlePassRewardConfigureItem>, IMessage, IEquatable<BattlePassRewardConfigureItem>, IDeepCloneable<BattlePassRewardConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<BattlePassRewardConfigureItem> _parser = new MessageParser<BattlePassRewardConfigureItem>(() => new BattlePassRewardConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int LevelFieldNumber = 1;

	private int level_;

	public const int FreeRewardsFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_freeRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> freeRewards_ = new MapField<int, int>();

	public const int NormalRewardsFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_normalRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> normalRewards_ = new MapField<int, int>();

	public const int NormalIconFieldNumber = 4;

	private string normalIcon_ = "";

	public const int NormalInfoTypeFieldNumber = 5;

	private int normalInfoType_;

	public const int PremiumRewardsFieldNumber = 6;

	private static readonly MapField<int, int>.Codec _map_premiumRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<int, int> premiumRewards_ = new MapField<int, int>();

	public const int PremiumIconFieldNumber = 7;

	private string premiumIcon_ = "";

	public const int PremiumInfoTypeFieldNumber = 8;

	private int premiumInfoType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassRewardConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Level
	{
		get
		{
			return level_;
		}
		private set
		{
			level_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> FreeRewards => freeRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> NormalRewards => normalRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string NormalIcon
	{
		get
		{
			return normalIcon_;
		}
		private set
		{
			normalIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NormalInfoType
	{
		get
		{
			return normalInfoType_;
		}
		private set
		{
			normalInfoType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> PremiumRewards => premiumRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PremiumIcon
	{
		get
		{
			return premiumIcon_;
		}
		private set
		{
			premiumIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumInfoType
	{
		get
		{
			return premiumInfoType_;
		}
		private set
		{
			premiumInfoType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardConfigureItem(BattlePassRewardConfigureItem other)
		: this()
	{
		level_ = other.level_;
		freeRewards_ = other.freeRewards_.Clone();
		normalRewards_ = other.normalRewards_.Clone();
		normalIcon_ = other.normalIcon_;
		normalInfoType_ = other.normalInfoType_;
		premiumRewards_ = other.premiumRewards_.Clone();
		premiumIcon_ = other.premiumIcon_;
		premiumInfoType_ = other.premiumInfoType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardConfigureItem Clone()
	{
		return new BattlePassRewardConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassRewardConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassRewardConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Level != other.Level)
		{
			return false;
		}
		if (!FreeRewards.Equals(other.FreeRewards))
		{
			return false;
		}
		if (!NormalRewards.Equals(other.NormalRewards))
		{
			return false;
		}
		if (NormalIcon != other.NormalIcon)
		{
			return false;
		}
		if (NormalInfoType != other.NormalInfoType)
		{
			return false;
		}
		if (!PremiumRewards.Equals(other.PremiumRewards))
		{
			return false;
		}
		if (PremiumIcon != other.PremiumIcon)
		{
			return false;
		}
		if (PremiumInfoType != other.PremiumInfoType)
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
		if (Level != 0)
		{
			num ^= Level.GetHashCode();
		}
		num ^= FreeRewards.GetHashCode();
		num ^= NormalRewards.GetHashCode();
		if (NormalIcon.Length != 0)
		{
			num ^= NormalIcon.GetHashCode();
		}
		if (NormalInfoType != 0)
		{
			num ^= NormalInfoType.GetHashCode();
		}
		num ^= PremiumRewards.GetHashCode();
		if (PremiumIcon.Length != 0)
		{
			num ^= PremiumIcon.GetHashCode();
		}
		if (PremiumInfoType != 0)
		{
			num ^= PremiumInfoType.GetHashCode();
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
		if (Level != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Level);
		}
		freeRewards_.WriteTo(ref output, _map_freeRewards_codec);
		normalRewards_.WriteTo(ref output, _map_normalRewards_codec);
		if (NormalIcon.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(NormalIcon);
		}
		if (NormalInfoType != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NormalInfoType);
		}
		premiumRewards_.WriteTo(ref output, _map_premiumRewards_codec);
		if (PremiumIcon.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(PremiumIcon);
		}
		if (PremiumInfoType != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(PremiumInfoType);
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
		if (Level != 0)
		{
			num += 5;
		}
		num += freeRewards_.CalculateSize(_map_freeRewards_codec);
		num += normalRewards_.CalculateSize(_map_normalRewards_codec);
		if (NormalIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(NormalIcon);
		}
		if (NormalInfoType != 0)
		{
			num += 5;
		}
		num += premiumRewards_.CalculateSize(_map_premiumRewards_codec);
		if (PremiumIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PremiumIcon);
		}
		if (PremiumInfoType != 0)
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
	public void MergeFrom(BattlePassRewardConfigureItem other)
	{
		if (other != null)
		{
			if (other.Level != 0)
			{
				Level = other.Level;
			}
			freeRewards_.MergeFrom(other.freeRewards_);
			normalRewards_.MergeFrom(other.normalRewards_);
			if (other.NormalIcon.Length != 0)
			{
				NormalIcon = other.NormalIcon;
			}
			if (other.NormalInfoType != 0)
			{
				NormalInfoType = other.NormalInfoType;
			}
			premiumRewards_.MergeFrom(other.premiumRewards_);
			if (other.PremiumIcon.Length != 0)
			{
				PremiumIcon = other.PremiumIcon;
			}
			if (other.PremiumInfoType != 0)
			{
				PremiumInfoType = other.PremiumInfoType;
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
				Level = input.ReadSFixed32();
				break;
			case 18u:
				freeRewards_.AddEntriesFrom(ref input, _map_freeRewards_codec);
				break;
			case 26u:
				normalRewards_.AddEntriesFrom(ref input, _map_normalRewards_codec);
				break;
			case 34u:
				NormalIcon = input.ReadString();
				break;
			case 45u:
				NormalInfoType = input.ReadSFixed32();
				break;
			case 50u:
				premiumRewards_.AddEntriesFrom(ref input, _map_premiumRewards_codec);
				break;
			case 58u:
				PremiumIcon = input.ReadString();
				break;
			case 69u:
				PremiumInfoType = input.ReadSFixed32();
				break;
			}
		}
	}
}
