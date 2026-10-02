using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CardConfigure : IMessage<CardConfigure>, IMessage, IEquatable<CardConfigure>, IDeepCloneable<CardConfigure>, IBufferMessage
{
	private static readonly MessageParser<CardConfigure> _parser = new MessageParser<CardConfigure>(() => new CardConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<CardInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, CardInfoConfigure.Parser);

	private readonly RepeatedField<CardInfoConfigure> infos_ = new RepeatedField<CardInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, CardInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, CardInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CardInfoConfigure.Parser), 18u);

	private readonly MapField<int, CardInfoConfigure> infoDict_ = new MapField<int, CardInfoConfigure>();

	public const int EffectPoolsFieldNumber = 3;

	private static readonly FieldCodec<CardEffectPoolConfigure> _repeated_effectPools_codec = FieldCodec.ForMessage(26u, CardEffectPoolConfigure.Parser);

	private readonly RepeatedField<CardEffectPoolConfigure> effectPools_ = new RepeatedField<CardEffectPoolConfigure>();

	public const int EffectPoolDictFieldNumber = 4;

	private static readonly MapField<int, CardEffectPoolConfigure>.Codec _map_effectPoolDict_codec = new MapField<int, CardEffectPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CardEffectPoolConfigure.Parser), 34u);

	private readonly MapField<int, CardEffectPoolConfigure> effectPoolDict_ = new MapField<int, CardEffectPoolConfigure>();

	public const int BattlePoolsFieldNumber = 5;

	private static readonly FieldCodec<CardBattlePoolConfigure> _repeated_battlePools_codec = FieldCodec.ForMessage(42u, CardBattlePoolConfigure.Parser);

	private readonly RepeatedField<CardBattlePoolConfigure> battlePools_ = new RepeatedField<CardBattlePoolConfigure>();

	public const int BattlePoolDictFieldNumber = 6;

	private static readonly MapField<int, CardBattlePoolConfigure>.Codec _map_battlePoolDict_codec = new MapField<int, CardBattlePoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CardBattlePoolConfigure.Parser), 50u);

	private readonly MapField<int, CardBattlePoolConfigure> battlePoolDict_ = new MapField<int, CardBattlePoolConfigure>();

	public const int AltArtsFieldNumber = 7;

	private static readonly FieldCodec<CardAltArtConfigure> _repeated_altArts_codec = FieldCodec.ForMessage(58u, CardAltArtConfigure.Parser);

	private readonly RepeatedField<CardAltArtConfigure> altArts_ = new RepeatedField<CardAltArtConfigure>();

	public const int AltArtDictFieldNumber = 8;

	private static readonly MapField<int, CardAltArtConfigure>.Codec _map_altArtDict_codec = new MapField<int, CardAltArtConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CardAltArtConfigure.Parser), 66u);

	private readonly MapField<int, CardAltArtConfigure> altArtDict_ = new MapField<int, CardAltArtConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CardConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CardReflection.Descriptor.MessageTypes[7];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CardInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CardInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CardEffectPoolConfigure> EffectPools => effectPools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CardEffectPoolConfigure> EffectPoolDict => effectPoolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CardBattlePoolConfigure> BattlePools => battlePools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CardBattlePoolConfigure> BattlePoolDict => battlePoolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CardAltArtConfigure> AltArts => altArts_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CardAltArtConfigure> AltArtDict => altArtDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardConfigure(CardConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		effectPools_ = other.effectPools_.Clone();
		effectPoolDict_ = other.effectPoolDict_.Clone();
		battlePools_ = other.battlePools_.Clone();
		battlePoolDict_ = other.battlePoolDict_.Clone();
		altArts_ = other.altArts_.Clone();
		altArtDict_ = other.altArtDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardConfigure Clone()
	{
		return new CardConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CardConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CardConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!effectPools_.Equals(other.effectPools_))
		{
			return false;
		}
		if (!EffectPoolDict.Equals(other.EffectPoolDict))
		{
			return false;
		}
		if (!battlePools_.Equals(other.battlePools_))
		{
			return false;
		}
		if (!BattlePoolDict.Equals(other.BattlePoolDict))
		{
			return false;
		}
		if (!altArts_.Equals(other.altArts_))
		{
			return false;
		}
		if (!AltArtDict.Equals(other.AltArtDict))
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
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= effectPools_.GetHashCode();
		num ^= EffectPoolDict.GetHashCode();
		num ^= battlePools_.GetHashCode();
		num ^= BattlePoolDict.GetHashCode();
		num ^= altArts_.GetHashCode();
		num ^= AltArtDict.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		effectPools_.WriteTo(ref output, _repeated_effectPools_codec);
		effectPoolDict_.WriteTo(ref output, _map_effectPoolDict_codec);
		battlePools_.WriteTo(ref output, _repeated_battlePools_codec);
		battlePoolDict_.WriteTo(ref output, _map_battlePoolDict_codec);
		altArts_.WriteTo(ref output, _repeated_altArts_codec);
		altArtDict_.WriteTo(ref output, _map_altArtDict_codec);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += effectPools_.CalculateSize(_repeated_effectPools_codec);
		num += effectPoolDict_.CalculateSize(_map_effectPoolDict_codec);
		num += battlePools_.CalculateSize(_repeated_battlePools_codec);
		num += battlePoolDict_.CalculateSize(_map_battlePoolDict_codec);
		num += altArts_.CalculateSize(_repeated_altArts_codec);
		num += altArtDict_.CalculateSize(_map_altArtDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CardConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			effectPools_.Add(other.effectPools_);
			effectPoolDict_.MergeFrom(other.effectPoolDict_);
			battlePools_.Add(other.battlePools_);
			battlePoolDict_.MergeFrom(other.battlePoolDict_);
			altArts_.Add(other.altArts_);
			altArtDict_.MergeFrom(other.altArtDict_);
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
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				effectPools_.AddEntriesFrom(ref input, _repeated_effectPools_codec);
				break;
			case 34u:
				effectPoolDict_.AddEntriesFrom(ref input, _map_effectPoolDict_codec);
				break;
			case 42u:
				battlePools_.AddEntriesFrom(ref input, _repeated_battlePools_codec);
				break;
			case 50u:
				battlePoolDict_.AddEntriesFrom(ref input, _map_battlePoolDict_codec);
				break;
			case 58u:
				altArts_.AddEntriesFrom(ref input, _repeated_altArts_codec);
				break;
			case 66u:
				altArtDict_.AddEntriesFrom(ref input, _map_altArtDict_codec);
				break;
			}
		}
	}
}
