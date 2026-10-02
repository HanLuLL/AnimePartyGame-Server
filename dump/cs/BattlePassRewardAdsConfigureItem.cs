using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class BattlePassRewardAdsConfigureItem : IMessage<BattlePassRewardAdsConfigureItem>, IMessage, IEquatable<BattlePassRewardAdsConfigureItem>, IDeepCloneable<BattlePassRewardAdsConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<BattlePassRewardAdsConfigureItem> _parser = new MessageParser<BattlePassRewardAdsConfigureItem>(() => new BattlePassRewardAdsConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int NormalUITypeFieldNumber = 2;

	private BattlePassUIType normalUIType_;

	public const int NormalItemIDFieldNumber = 3;

	private int normalItemID_;

	public const int NormalResourceFieldNumber = 4;

	private string normalResource_ = "";

	public const int NormalDescriptionFieldNumber = 5;

	private int normalDescription_;

	public const int PremiumUITypeFieldNumber = 6;

	private BattlePassUIType premiumUIType_;

	public const int PremiumItemIDFieldNumber = 7;

	private int premiumItemID_;

	public const int PremiumResourceFieldNumber = 8;

	private string premiumResource_ = "";

	public const int PremiumDescriptionFieldNumber = 9;

	private int premiumDescription_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassRewardAdsConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[7];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassUIType NormalUIType
	{
		get
		{
			return normalUIType_;
		}
		private set
		{
			normalUIType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NormalItemID
	{
		get
		{
			return normalItemID_;
		}
		private set
		{
			normalItemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string NormalResource
	{
		get
		{
			return normalResource_;
		}
		private set
		{
			normalResource_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NormalDescription
	{
		get
		{
			return normalDescription_;
		}
		private set
		{
			normalDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassUIType PremiumUIType
	{
		get
		{
			return premiumUIType_;
		}
		private set
		{
			premiumUIType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumItemID
	{
		get
		{
			return premiumItemID_;
		}
		private set
		{
			premiumItemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PremiumResource
	{
		get
		{
			return premiumResource_;
		}
		private set
		{
			premiumResource_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumDescription
	{
		get
		{
			return premiumDescription_;
		}
		private set
		{
			premiumDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardAdsConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardAdsConfigureItem(BattlePassRewardAdsConfigureItem other)
		: this()
	{
		index_ = other.index_;
		normalUIType_ = other.normalUIType_;
		normalItemID_ = other.normalItemID_;
		normalResource_ = other.normalResource_;
		normalDescription_ = other.normalDescription_;
		premiumUIType_ = other.premiumUIType_;
		premiumItemID_ = other.premiumItemID_;
		premiumResource_ = other.premiumResource_;
		premiumDescription_ = other.premiumDescription_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardAdsConfigureItem Clone()
	{
		return new BattlePassRewardAdsConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassRewardAdsConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassRewardAdsConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (NormalUIType != other.NormalUIType)
		{
			return false;
		}
		if (NormalItemID != other.NormalItemID)
		{
			return false;
		}
		if (NormalResource != other.NormalResource)
		{
			return false;
		}
		if (NormalDescription != other.NormalDescription)
		{
			return false;
		}
		if (PremiumUIType != other.PremiumUIType)
		{
			return false;
		}
		if (PremiumItemID != other.PremiumItemID)
		{
			return false;
		}
		if (PremiumResource != other.PremiumResource)
		{
			return false;
		}
		if (PremiumDescription != other.PremiumDescription)
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (NormalUIType != BattlePassUIType.None)
		{
			num ^= NormalUIType.GetHashCode();
		}
		if (NormalItemID != 0)
		{
			num ^= NormalItemID.GetHashCode();
		}
		if (NormalResource.Length != 0)
		{
			num ^= NormalResource.GetHashCode();
		}
		if (NormalDescription != 0)
		{
			num ^= NormalDescription.GetHashCode();
		}
		if (PremiumUIType != BattlePassUIType.None)
		{
			num ^= PremiumUIType.GetHashCode();
		}
		if (PremiumItemID != 0)
		{
			num ^= PremiumItemID.GetHashCode();
		}
		if (PremiumResource.Length != 0)
		{
			num ^= PremiumResource.GetHashCode();
		}
		if (PremiumDescription != 0)
		{
			num ^= PremiumDescription.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (NormalUIType != BattlePassUIType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)NormalUIType);
		}
		if (NormalItemID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NormalItemID);
		}
		if (NormalResource.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(NormalResource);
		}
		if (NormalDescription != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(NormalDescription);
		}
		if (PremiumUIType != BattlePassUIType.None)
		{
			output.WriteRawTag(48);
			output.WriteEnum((int)PremiumUIType);
		}
		if (PremiumItemID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(PremiumItemID);
		}
		if (PremiumResource.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(PremiumResource);
		}
		if (PremiumDescription != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(PremiumDescription);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (NormalUIType != BattlePassUIType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)NormalUIType);
		}
		if (NormalItemID != 0)
		{
			num += 5;
		}
		if (NormalResource.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(NormalResource);
		}
		if (NormalDescription != 0)
		{
			num += 5;
		}
		if (PremiumUIType != BattlePassUIType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PremiumUIType);
		}
		if (PremiumItemID != 0)
		{
			num += 5;
		}
		if (PremiumResource.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PremiumResource);
		}
		if (PremiumDescription != 0)
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
	public void MergeFrom(BattlePassRewardAdsConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.NormalUIType != BattlePassUIType.None)
			{
				NormalUIType = other.NormalUIType;
			}
			if (other.NormalItemID != 0)
			{
				NormalItemID = other.NormalItemID;
			}
			if (other.NormalResource.Length != 0)
			{
				NormalResource = other.NormalResource;
			}
			if (other.NormalDescription != 0)
			{
				NormalDescription = other.NormalDescription;
			}
			if (other.PremiumUIType != BattlePassUIType.None)
			{
				PremiumUIType = other.PremiumUIType;
			}
			if (other.PremiumItemID != 0)
			{
				PremiumItemID = other.PremiumItemID;
			}
			if (other.PremiumResource.Length != 0)
			{
				PremiumResource = other.PremiumResource;
			}
			if (other.PremiumDescription != 0)
			{
				PremiumDescription = other.PremiumDescription;
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
				Index = input.ReadSFixed32();
				break;
			case 16u:
				NormalUIType = (BattlePassUIType)input.ReadEnum();
				break;
			case 29u:
				NormalItemID = input.ReadSFixed32();
				break;
			case 34u:
				NormalResource = input.ReadString();
				break;
			case 45u:
				NormalDescription = input.ReadSFixed32();
				break;
			case 48u:
				PremiumUIType = (BattlePassUIType)input.ReadEnum();
				break;
			case 61u:
				PremiumItemID = input.ReadSFixed32();
				break;
			case 66u:
				PremiumResource = input.ReadString();
				break;
			case 77u:
				PremiumDescription = input.ReadSFixed32();
				break;
			}
		}
	}
}
