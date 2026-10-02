using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using GameLogic;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Tools;

public sealed class FashionConfigure : IMessage<FashionConfigure>, IMessage, IEquatable<FashionConfigure>, IDeepCloneable<FashionConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionConfigure> _parser = new MessageParser<FashionConfigure>(() => new FashionConfigure());

	private UnknownFieldSet _unknownFields;

	public const int AccountHeadShotsFieldNumber = 1;

	private static readonly FieldCodec<FashionAccountHeadShotConfigure> _repeated_accountHeadShots_codec = FieldCodec.ForMessage(10u, FashionAccountHeadShotConfigure.Parser);

	private readonly RepeatedField<FashionAccountHeadShotConfigure> accountHeadShots_ = new RepeatedField<FashionAccountHeadShotConfigure>();

	public const int AccountHeadShotDictFieldNumber = 2;

	private static readonly MapField<int, FashionAccountHeadShotConfigure>.Codec _map_accountHeadShotDict_codec = new MapField<int, FashionAccountHeadShotConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FashionAccountHeadShotConfigure.Parser), 18u);

	private readonly MapField<int, FashionAccountHeadShotConfigure> accountHeadShotDict_ = new MapField<int, FashionAccountHeadShotConfigure>();

	public const int AccountBackgroundsFieldNumber = 3;

	private static readonly FieldCodec<FashionAccountBackgroundConfigure> _repeated_accountBackgrounds_codec = FieldCodec.ForMessage(26u, FashionAccountBackgroundConfigure.Parser);

	private readonly RepeatedField<FashionAccountBackgroundConfigure> accountBackgrounds_ = new RepeatedField<FashionAccountBackgroundConfigure>();

	public const int AccountBackgroundDictFieldNumber = 4;

	private static readonly MapField<int, FashionAccountBackgroundConfigure>.Codec _map_accountBackgroundDict_codec = new MapField<int, FashionAccountBackgroundConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FashionAccountBackgroundConfigure.Parser), 34u);

	private readonly MapField<int, FashionAccountBackgroundConfigure> accountBackgroundDict_ = new MapField<int, FashionAccountBackgroundConfigure>();

	public const int EffectsFieldNumber = 5;

	private static readonly FieldCodec<FashionEffectConfigure> _repeated_effects_codec = FieldCodec.ForMessage(42u, FashionEffectConfigure.Parser);

	private readonly RepeatedField<FashionEffectConfigure> effects_ = new RepeatedField<FashionEffectConfigure>();

	public const int EffectDictFieldNumber = 6;

	private static readonly MapField<int, FashionEffectConfigure>.Codec _map_effectDict_codec = new MapField<int, FashionEffectConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FashionEffectConfigure.Parser), 50u);

	private readonly MapField<int, FashionEffectConfigure> effectDict_ = new MapField<int, FashionEffectConfigure>();

	public const int CardBacksFieldNumber = 7;

	private static readonly FieldCodec<FashionCardBackConfigure> _repeated_cardBacks_codec = FieldCodec.ForMessage(58u, FashionCardBackConfigure.Parser);

	private readonly RepeatedField<FashionCardBackConfigure> cardBacks_ = new RepeatedField<FashionCardBackConfigure>();

	public const int CardBackDictFieldNumber = 8;

	private static readonly MapField<int, FashionCardBackConfigure>.Codec _map_cardBackDict_codec = new MapField<int, FashionCardBackConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FashionCardBackConfigure.Parser), 66u);

	private readonly MapField<int, FashionCardBackConfigure> cardBackDict_ = new MapField<int, FashionCardBackConfigure>();

	public const int DicesFieldNumber = 9;

	private static readonly FieldCodec<FashionDiceConfigure> _repeated_dices_codec = FieldCodec.ForMessage(74u, FashionDiceConfigure.Parser);

	private readonly RepeatedField<FashionDiceConfigure> dices_ = new RepeatedField<FashionDiceConfigure>();

	public const int DiceDictFieldNumber = 10;

	private static readonly MapField<int, FashionDiceConfigure>.Codec _map_diceDict_codec = new MapField<int, FashionDiceConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FashionDiceConfigure.Parser), 82u);

	private readonly MapField<int, FashionDiceConfigure> diceDict_ = new MapField<int, FashionDiceConfigure>();

	public const int KVsFieldNumber = 11;

	private static readonly FieldCodec<FashionKVConfigure> _repeated_kVs_codec = FieldCodec.ForMessage(90u, FashionKVConfigure.Parser);

	private readonly RepeatedField<FashionKVConfigure> kVs_ = new RepeatedField<FashionKVConfigure>();

	public const int KVDictFieldNumber = 12;

	private static readonly MapField<int, FashionKVConfigure>.Codec _map_kVDict_codec = new MapField<int, FashionKVConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FashionKVConfigure.Parser), 98u);

	private readonly MapField<int, FashionKVConfigure> kVDict_ = new MapField<int, FashionKVConfigure>();

	private readonly List<int> RareTreasureFashionIds = new List<int> { 71024, 71994, 70898 };

	private readonly List<ItemInfoConfigure> FashionData = new List<ItemInfoConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionAccountHeadShotConfigure> AccountHeadShots => accountHeadShots_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FashionAccountHeadShotConfigure> AccountHeadShotDict => accountHeadShotDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionAccountBackgroundConfigure> AccountBackgrounds => accountBackgrounds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FashionAccountBackgroundConfigure> AccountBackgroundDict => accountBackgroundDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionEffectConfigure> Effects => effects_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FashionEffectConfigure> EffectDict => effectDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionCardBackConfigure> CardBacks => cardBacks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FashionCardBackConfigure> CardBackDict => cardBackDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionDiceConfigure> Dices => dices_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FashionDiceConfigure> DiceDict => diceDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionKVConfigure> KVs => kVs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FashionKVConfigure> KVDict => kVDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionConfigure(FashionConfigure other)
		: this()
	{
		accountHeadShots_ = other.accountHeadShots_.Clone();
		accountHeadShotDict_ = other.accountHeadShotDict_.Clone();
		accountBackgrounds_ = other.accountBackgrounds_.Clone();
		accountBackgroundDict_ = other.accountBackgroundDict_.Clone();
		effects_ = other.effects_.Clone();
		effectDict_ = other.effectDict_.Clone();
		cardBacks_ = other.cardBacks_.Clone();
		cardBackDict_ = other.cardBackDict_.Clone();
		dices_ = other.dices_.Clone();
		diceDict_ = other.diceDict_.Clone();
		kVs_ = other.kVs_.Clone();
		kVDict_ = other.kVDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionConfigure Clone()
	{
		return new FashionConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!accountHeadShots_.Equals(other.accountHeadShots_))
		{
			return false;
		}
		if (!AccountHeadShotDict.Equals(other.AccountHeadShotDict))
		{
			return false;
		}
		if (!accountBackgrounds_.Equals(other.accountBackgrounds_))
		{
			return false;
		}
		if (!AccountBackgroundDict.Equals(other.AccountBackgroundDict))
		{
			return false;
		}
		if (!effects_.Equals(other.effects_))
		{
			return false;
		}
		if (!EffectDict.Equals(other.EffectDict))
		{
			return false;
		}
		if (!cardBacks_.Equals(other.cardBacks_))
		{
			return false;
		}
		if (!CardBackDict.Equals(other.CardBackDict))
		{
			return false;
		}
		if (!dices_.Equals(other.dices_))
		{
			return false;
		}
		if (!DiceDict.Equals(other.DiceDict))
		{
			return false;
		}
		if (!kVs_.Equals(other.kVs_))
		{
			return false;
		}
		if (!KVDict.Equals(other.KVDict))
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
		num ^= accountHeadShots_.GetHashCode();
		num ^= AccountHeadShotDict.GetHashCode();
		num ^= accountBackgrounds_.GetHashCode();
		num ^= AccountBackgroundDict.GetHashCode();
		num ^= effects_.GetHashCode();
		num ^= EffectDict.GetHashCode();
		num ^= cardBacks_.GetHashCode();
		num ^= CardBackDict.GetHashCode();
		num ^= dices_.GetHashCode();
		num ^= DiceDict.GetHashCode();
		num ^= kVs_.GetHashCode();
		num ^= KVDict.GetHashCode();
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
		accountHeadShots_.WriteTo(ref output, _repeated_accountHeadShots_codec);
		accountHeadShotDict_.WriteTo(ref output, _map_accountHeadShotDict_codec);
		accountBackgrounds_.WriteTo(ref output, _repeated_accountBackgrounds_codec);
		accountBackgroundDict_.WriteTo(ref output, _map_accountBackgroundDict_codec);
		effects_.WriteTo(ref output, _repeated_effects_codec);
		effectDict_.WriteTo(ref output, _map_effectDict_codec);
		cardBacks_.WriteTo(ref output, _repeated_cardBacks_codec);
		cardBackDict_.WriteTo(ref output, _map_cardBackDict_codec);
		dices_.WriteTo(ref output, _repeated_dices_codec);
		diceDict_.WriteTo(ref output, _map_diceDict_codec);
		kVs_.WriteTo(ref output, _repeated_kVs_codec);
		kVDict_.WriteTo(ref output, _map_kVDict_codec);
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
		num += accountHeadShots_.CalculateSize(_repeated_accountHeadShots_codec);
		num += accountHeadShotDict_.CalculateSize(_map_accountHeadShotDict_codec);
		num += accountBackgrounds_.CalculateSize(_repeated_accountBackgrounds_codec);
		num += accountBackgroundDict_.CalculateSize(_map_accountBackgroundDict_codec);
		num += effects_.CalculateSize(_repeated_effects_codec);
		num += effectDict_.CalculateSize(_map_effectDict_codec);
		num += cardBacks_.CalculateSize(_repeated_cardBacks_codec);
		num += cardBackDict_.CalculateSize(_map_cardBackDict_codec);
		num += dices_.CalculateSize(_repeated_dices_codec);
		num += diceDict_.CalculateSize(_map_diceDict_codec);
		num += kVs_.CalculateSize(_repeated_kVs_codec);
		num += kVDict_.CalculateSize(_map_kVDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionConfigure other)
	{
		if (other != null)
		{
			accountHeadShots_.Add(other.accountHeadShots_);
			accountHeadShotDict_.MergeFrom(other.accountHeadShotDict_);
			accountBackgrounds_.Add(other.accountBackgrounds_);
			accountBackgroundDict_.MergeFrom(other.accountBackgroundDict_);
			effects_.Add(other.effects_);
			effectDict_.MergeFrom(other.effectDict_);
			cardBacks_.Add(other.cardBacks_);
			cardBackDict_.MergeFrom(other.cardBackDict_);
			dices_.Add(other.dices_);
			diceDict_.MergeFrom(other.diceDict_);
			kVs_.Add(other.kVs_);
			kVDict_.MergeFrom(other.kVDict_);
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
				accountHeadShots_.AddEntriesFrom(ref input, _repeated_accountHeadShots_codec);
				break;
			case 18u:
				accountHeadShotDict_.AddEntriesFrom(ref input, _map_accountHeadShotDict_codec);
				break;
			case 26u:
				accountBackgrounds_.AddEntriesFrom(ref input, _repeated_accountBackgrounds_codec);
				break;
			case 34u:
				accountBackgroundDict_.AddEntriesFrom(ref input, _map_accountBackgroundDict_codec);
				break;
			case 42u:
				effects_.AddEntriesFrom(ref input, _repeated_effects_codec);
				break;
			case 50u:
				effectDict_.AddEntriesFrom(ref input, _map_effectDict_codec);
				break;
			case 58u:
				cardBacks_.AddEntriesFrom(ref input, _repeated_cardBacks_codec);
				break;
			case 66u:
				cardBackDict_.AddEntriesFrom(ref input, _map_cardBackDict_codec);
				break;
			case 74u:
				dices_.AddEntriesFrom(ref input, _repeated_dices_codec);
				break;
			case 82u:
				diceDict_.AddEntriesFrom(ref input, _map_diceDict_codec);
				break;
			case 90u:
				kVs_.AddEntriesFrom(ref input, _repeated_kVs_codec);
				break;
			case 98u:
				kVDict_.AddEntriesFrom(ref input, _map_kVDict_codec);
				break;
			}
		}
	}

	public void Fix()
	{
	}

	private bool RareTreasureFashion(int FashionId)
	{
		if (!RareTreasureFashionIds.Contains(FashionId))
		{
			return true;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(FashionId);
	}

	private bool IsVailFashion(int FashionId)
	{
		if (StaticConfigure.Item.InfoDict.TryGetValue(FashionId, out var value) && value.IsClientShow && value.IsVailItem())
		{
			return true;
		}
		return false;
	}

	public List<ItemInfoConfigure> GetHeadShotConfigs()
	{
		FashionData.Clear();
		MapField<int, ItemInfoConfigure> infoDict = StaticConfigure.Item.InfoDict;
		foreach (FashionAccountHeadShotConfigure item in accountHeadShots_)
		{
			if (infoDict.TryGetValue(item.Id, out var value) && IsVailFashion(value.Id) && RareTreasureFashion(value.Id))
			{
				FashionData.Add(value);
			}
		}
		FashionData.Sort(OnItemSorted);
		return FashionData;
	}

	public List<ItemInfoConfigure> GetBackgroundConfigs()
	{
		FashionData.Clear();
		MapField<int, ItemInfoConfigure> infoDict = StaticConfigure.Item.InfoDict;
		foreach (FashionAccountBackgroundConfigure item in accountBackgrounds_)
		{
			if (infoDict.TryGetValue(item.Id, out var value) && IsVailFashion(value.Id) && RareTreasureFashion(value.Id))
			{
				FashionData.Add(value);
			}
		}
		FashionData.Sort(OnItemSorted);
		return FashionData;
	}

	public List<ItemInfoConfigure> GetEffectConfigs()
	{
		FashionData.Clear();
		MapField<int, ItemInfoConfigure> infoDict = StaticConfigure.Item.InfoDict;
		foreach (FashionEffectConfigure item in effects_)
		{
			if (infoDict.TryGetValue(item.Id, out var value) && IsVailFashion(value.Id) && RareTreasureFashion(value.Id))
			{
				FashionData.Add(value);
			}
		}
		FashionData.Sort(OnItemSorted);
		return FashionData;
	}

	public List<ItemInfoConfigure> GetCardBackConfigs()
	{
		FashionData.Clear();
		MapField<int, ItemInfoConfigure> infoDict = StaticConfigure.Item.InfoDict;
		foreach (FashionCardBackConfigure item in cardBacks_)
		{
			if (infoDict.TryGetValue(item.Id, out var value) && IsVailFashion(value.Id) && RareTreasureFashion(value.Id))
			{
				FashionData.Add(value);
			}
		}
		FashionData.Sort(OnItemSorted);
		return FashionData;
	}

	public List<ItemInfoConfigure> GetDiceConfigs()
	{
		FashionData.Clear();
		MapField<int, ItemInfoConfigure> infoDict = StaticConfigure.Item.InfoDict;
		foreach (FashionDiceConfigure item in dices_)
		{
			if (infoDict.TryGetValue(item.Id, out var value) && IsVailFashion(value.Id) && RareTreasureFashion(value.Id))
			{
				FashionData.Add(value);
			}
		}
		FashionData.Sort(OnItemSorted);
		return FashionData;
	}

	public List<ItemInfoConfigure> GetMainBGConfigs()
	{
		FashionData.Clear();
		MapField<int, ItemInfoConfigure> infoDict = StaticConfigure.Item.InfoDict;
		foreach (FashionKVConfigure item in kVs_)
		{
			if (infoDict.TryGetValue(item.Id, out var value) && IsVailFashion(value.Id) && RareTreasureFashion(value.Id))
			{
				FashionData.Add(value);
			}
		}
		FashionData.Sort(OnItemSorted);
		return FashionData;
	}

	private int OnItemSorted(ItemInfoConfigure x, ItemInfoConfigure y)
	{
		BagItem item = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(x.Id);
		BagItem item2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItem(y.Id);
		if (item == null)
		{
			if (item2 == null)
			{
				return GetSortById(x, y);
			}
			return 1;
		}
		if (item2 == null)
		{
			return -1;
		}
		return GetSortById(x, y);
	}

	private int GetSortById(ItemInfoConfigure item1, ItemInfoConfigure item2)
	{
		if (item1.QualityType == item2.QualityType)
		{
			return item1.Id - item2.Id;
		}
		return item2.QualityType - item1.QualityType;
	}
}
