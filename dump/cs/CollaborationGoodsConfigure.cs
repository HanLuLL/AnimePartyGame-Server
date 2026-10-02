using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CollaborationGoodsConfigure : IMessage<CollaborationGoodsConfigure>, IMessage, IEquatable<CollaborationGoodsConfigure>, IDeepCloneable<CollaborationGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<CollaborationGoodsConfigure> _parser = new MessageParser<CollaborationGoodsConfigure>(() => new CollaborationGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int GoodsIdFieldNumber = 2;

	private int goodsId_;

	public const int HeroIDFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_heroID_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> heroID_ = new RepeatedField<int>();

	public const int HeroNameFieldNumber = 4;

	private int heroName_;

	public const int MutiHeroNameFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_mutiHeroName_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> mutiHeroName_ = new RepeatedField<int>();

	public const int PlayerPhotoIDFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_playerPhotoID_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> playerPhotoID_ = new RepeatedField<int>();

	public const int AccountBackgroundIDFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_accountBackgroundID_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> accountBackgroundID_ = new RepeatedField<int>();

	public const int PhotoAndBackgroundNameFieldNumber = 8;

	private int photoAndBackgroundName_;

	public const int OtherRewardsFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_otherRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> otherRewards_ = new MapField<int, int>();

	public const int EmojiIDFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_emojiID_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> emojiID_ = new RepeatedField<int>();

	public const int ExtraBackgroundRewardsFieldNumber = 11;

	private static readonly MapField<int, int>.Codec _map_extraBackgroundRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 90u);

	private readonly MapField<int, int> extraBackgroundRewards_ = new MapField<int, int>();

	public const int ExtraItemRewardsFieldNumber = 12;

	private static readonly MapField<int, int>.Codec _map_extraItemRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 98u);

	private readonly MapField<int, int> extraItemRewards_ = new MapField<int, int>();

	public const int RelatedGoodsIdFieldNumber = 13;

	private int relatedGoodsId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CollaborationGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CollaborationReflection.Descriptor.MessageTypes[1];

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
	public int GoodsId
	{
		get
		{
			return goodsId_;
		}
		private set
		{
			goodsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> HeroID => heroID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeroName
	{
		get
		{
			return heroName_;
		}
		private set
		{
			heroName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MutiHeroName => mutiHeroName_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PlayerPhotoID => playerPhotoID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> AccountBackgroundID => accountBackgroundID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PhotoAndBackgroundName
	{
		get
		{
			return photoAndBackgroundName_;
		}
		private set
		{
			photoAndBackgroundName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> OtherRewards => otherRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> EmojiID => emojiID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ExtraBackgroundRewards => extraBackgroundRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ExtraItemRewards => extraItemRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelatedGoodsId
	{
		get
		{
			return relatedGoodsId_;
		}
		private set
		{
			relatedGoodsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationGoodsConfigure(CollaborationGoodsConfigure other)
		: this()
	{
		index_ = other.index_;
		goodsId_ = other.goodsId_;
		heroID_ = other.heroID_.Clone();
		heroName_ = other.heroName_;
		mutiHeroName_ = other.mutiHeroName_.Clone();
		playerPhotoID_ = other.playerPhotoID_.Clone();
		accountBackgroundID_ = other.accountBackgroundID_.Clone();
		photoAndBackgroundName_ = other.photoAndBackgroundName_;
		otherRewards_ = other.otherRewards_.Clone();
		emojiID_ = other.emojiID_.Clone();
		extraBackgroundRewards_ = other.extraBackgroundRewards_.Clone();
		extraItemRewards_ = other.extraItemRewards_.Clone();
		relatedGoodsId_ = other.relatedGoodsId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationGoodsConfigure Clone()
	{
		return new CollaborationGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CollaborationGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CollaborationGoodsConfigure other)
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
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (!heroID_.Equals(other.heroID_))
		{
			return false;
		}
		if (HeroName != other.HeroName)
		{
			return false;
		}
		if (!mutiHeroName_.Equals(other.mutiHeroName_))
		{
			return false;
		}
		if (!playerPhotoID_.Equals(other.playerPhotoID_))
		{
			return false;
		}
		if (!accountBackgroundID_.Equals(other.accountBackgroundID_))
		{
			return false;
		}
		if (PhotoAndBackgroundName != other.PhotoAndBackgroundName)
		{
			return false;
		}
		if (!OtherRewards.Equals(other.OtherRewards))
		{
			return false;
		}
		if (!emojiID_.Equals(other.emojiID_))
		{
			return false;
		}
		if (!ExtraBackgroundRewards.Equals(other.ExtraBackgroundRewards))
		{
			return false;
		}
		if (!ExtraItemRewards.Equals(other.ExtraItemRewards))
		{
			return false;
		}
		if (RelatedGoodsId != other.RelatedGoodsId)
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
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		num ^= heroID_.GetHashCode();
		if (HeroName != 0)
		{
			num ^= HeroName.GetHashCode();
		}
		num ^= mutiHeroName_.GetHashCode();
		num ^= playerPhotoID_.GetHashCode();
		num ^= accountBackgroundID_.GetHashCode();
		if (PhotoAndBackgroundName != 0)
		{
			num ^= PhotoAndBackgroundName.GetHashCode();
		}
		num ^= OtherRewards.GetHashCode();
		num ^= emojiID_.GetHashCode();
		num ^= ExtraBackgroundRewards.GetHashCode();
		num ^= ExtraItemRewards.GetHashCode();
		if (RelatedGoodsId != 0)
		{
			num ^= RelatedGoodsId.GetHashCode();
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
		if (GoodsId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(GoodsId);
		}
		heroID_.WriteTo(ref output, _repeated_heroID_codec);
		if (HeroName != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(HeroName);
		}
		mutiHeroName_.WriteTo(ref output, _repeated_mutiHeroName_codec);
		playerPhotoID_.WriteTo(ref output, _repeated_playerPhotoID_codec);
		accountBackgroundID_.WriteTo(ref output, _repeated_accountBackgroundID_codec);
		if (PhotoAndBackgroundName != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(PhotoAndBackgroundName);
		}
		otherRewards_.WriteTo(ref output, _map_otherRewards_codec);
		emojiID_.WriteTo(ref output, _repeated_emojiID_codec);
		extraBackgroundRewards_.WriteTo(ref output, _map_extraBackgroundRewards_codec);
		extraItemRewards_.WriteTo(ref output, _map_extraItemRewards_codec);
		if (RelatedGoodsId != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(RelatedGoodsId);
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
		if (GoodsId != 0)
		{
			num += 5;
		}
		num += heroID_.CalculateSize(_repeated_heroID_codec);
		if (HeroName != 0)
		{
			num += 5;
		}
		num += mutiHeroName_.CalculateSize(_repeated_mutiHeroName_codec);
		num += playerPhotoID_.CalculateSize(_repeated_playerPhotoID_codec);
		num += accountBackgroundID_.CalculateSize(_repeated_accountBackgroundID_codec);
		if (PhotoAndBackgroundName != 0)
		{
			num += 5;
		}
		num += otherRewards_.CalculateSize(_map_otherRewards_codec);
		num += emojiID_.CalculateSize(_repeated_emojiID_codec);
		num += extraBackgroundRewards_.CalculateSize(_map_extraBackgroundRewards_codec);
		num += extraItemRewards_.CalculateSize(_map_extraItemRewards_codec);
		if (RelatedGoodsId != 0)
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
	public void MergeFrom(CollaborationGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			heroID_.Add(other.heroID_);
			if (other.HeroName != 0)
			{
				HeroName = other.HeroName;
			}
			mutiHeroName_.Add(other.mutiHeroName_);
			playerPhotoID_.Add(other.playerPhotoID_);
			accountBackgroundID_.Add(other.accountBackgroundID_);
			if (other.PhotoAndBackgroundName != 0)
			{
				PhotoAndBackgroundName = other.PhotoAndBackgroundName;
			}
			otherRewards_.MergeFrom(other.otherRewards_);
			emojiID_.Add(other.emojiID_);
			extraBackgroundRewards_.MergeFrom(other.extraBackgroundRewards_);
			extraItemRewards_.MergeFrom(other.extraItemRewards_);
			if (other.RelatedGoodsId != 0)
			{
				RelatedGoodsId = other.RelatedGoodsId;
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
			case 21u:
				GoodsId = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				heroID_.AddEntriesFrom(ref input, _repeated_heroID_codec);
				break;
			case 37u:
				HeroName = input.ReadSFixed32();
				break;
			case 42u:
			case 45u:
				mutiHeroName_.AddEntriesFrom(ref input, _repeated_mutiHeroName_codec);
				break;
			case 50u:
			case 53u:
				playerPhotoID_.AddEntriesFrom(ref input, _repeated_playerPhotoID_codec);
				break;
			case 58u:
			case 61u:
				accountBackgroundID_.AddEntriesFrom(ref input, _repeated_accountBackgroundID_codec);
				break;
			case 69u:
				PhotoAndBackgroundName = input.ReadSFixed32();
				break;
			case 74u:
				otherRewards_.AddEntriesFrom(ref input, _map_otherRewards_codec);
				break;
			case 82u:
			case 85u:
				emojiID_.AddEntriesFrom(ref input, _repeated_emojiID_codec);
				break;
			case 90u:
				extraBackgroundRewards_.AddEntriesFrom(ref input, _map_extraBackgroundRewards_codec);
				break;
			case 98u:
				extraItemRewards_.AddEntriesFrom(ref input, _map_extraItemRewards_codec);
				break;
			case 109u:
				RelatedGoodsId = input.ReadSFixed32();
				break;
			}
		}
	}
}
