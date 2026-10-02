using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CharacterConfigure : IMessage<CharacterConfigure>, IMessage, IEquatable<CharacterConfigure>, IDeepCloneable<CharacterConfigure>, IBufferMessage
{
	private static readonly MessageParser<CharacterConfigure> _parser = new MessageParser<CharacterConfigure>(() => new CharacterConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<CharacterInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, CharacterInfoConfigure.Parser);

	private readonly RepeatedField<CharacterInfoConfigure> infos_ = new RepeatedField<CharacterInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, CharacterInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, CharacterInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CharacterInfoConfigure.Parser), 18u);

	private readonly MapField<int, CharacterInfoConfigure> infoDict_ = new MapField<int, CharacterInfoConfigure>();

	public const int ExpressionPacksFieldNumber = 3;

	private static readonly FieldCodec<CharacterExpressionPackConfigure> _repeated_expressionPacks_codec = FieldCodec.ForMessage(26u, CharacterExpressionPackConfigure.Parser);

	private readonly RepeatedField<CharacterExpressionPackConfigure> expressionPacks_ = new RepeatedField<CharacterExpressionPackConfigure>();

	public const int ExpressionPackDictFieldNumber = 4;

	private static readonly MapField<int, CharacterExpressionPackConfigure>.Codec _map_expressionPackDict_codec = new MapField<int, CharacterExpressionPackConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CharacterExpressionPackConfigure.Parser), 34u);

	private readonly MapField<int, CharacterExpressionPackConfigure> expressionPackDict_ = new MapField<int, CharacterExpressionPackConfigure>();

	public const int HeroFavorGiftsFieldNumber = 5;

	private static readonly FieldCodec<CharacterHeroFavorGiftConfigure> _repeated_heroFavorGifts_codec = FieldCodec.ForMessage(42u, CharacterHeroFavorGiftConfigure.Parser);

	private readonly RepeatedField<CharacterHeroFavorGiftConfigure> heroFavorGifts_ = new RepeatedField<CharacterHeroFavorGiftConfigure>();

	public const int HeroFavorGiftDictFieldNumber = 6;

	private static readonly MapField<int, CharacterHeroFavorGiftConfigure>.Codec _map_heroFavorGiftDict_codec = new MapField<int, CharacterHeroFavorGiftConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CharacterHeroFavorGiftConfigure.Parser), 50u);

	private readonly MapField<int, CharacterHeroFavorGiftConfigure> heroFavorGiftDict_ = new MapField<int, CharacterHeroFavorGiftConfigure>();

	public const int VoicesFieldNumber = 7;

	private static readonly FieldCodec<CharacterVoiceConfigure> _repeated_voices_codec = FieldCodec.ForMessage(58u, CharacterVoiceConfigure.Parser);

	private readonly RepeatedField<CharacterVoiceConfigure> voices_ = new RepeatedField<CharacterVoiceConfigure>();

	public const int VoiceDictFieldNumber = 8;

	private static readonly MapField<int, CharacterVoiceConfigure>.Codec _map_voiceDict_codec = new MapField<int, CharacterVoiceConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CharacterVoiceConfigure.Parser), 66u);

	private readonly MapField<int, CharacterVoiceConfigure> voiceDict_ = new MapField<int, CharacterVoiceConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CharacterConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CharacterReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CharacterInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CharacterInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CharacterExpressionPackConfigure> ExpressionPacks => expressionPacks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CharacterExpressionPackConfigure> ExpressionPackDict => expressionPackDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CharacterHeroFavorGiftConfigure> HeroFavorGifts => heroFavorGifts_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CharacterHeroFavorGiftConfigure> HeroFavorGiftDict => heroFavorGiftDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CharacterVoiceConfigure> Voices => voices_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CharacterVoiceConfigure> VoiceDict => voiceDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterConfigure(CharacterConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		expressionPacks_ = other.expressionPacks_.Clone();
		expressionPackDict_ = other.expressionPackDict_.Clone();
		heroFavorGifts_ = other.heroFavorGifts_.Clone();
		heroFavorGiftDict_ = other.heroFavorGiftDict_.Clone();
		voices_ = other.voices_.Clone();
		voiceDict_ = other.voiceDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterConfigure Clone()
	{
		return new CharacterConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CharacterConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CharacterConfigure other)
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
		if (!expressionPacks_.Equals(other.expressionPacks_))
		{
			return false;
		}
		if (!ExpressionPackDict.Equals(other.ExpressionPackDict))
		{
			return false;
		}
		if (!heroFavorGifts_.Equals(other.heroFavorGifts_))
		{
			return false;
		}
		if (!HeroFavorGiftDict.Equals(other.HeroFavorGiftDict))
		{
			return false;
		}
		if (!voices_.Equals(other.voices_))
		{
			return false;
		}
		if (!VoiceDict.Equals(other.VoiceDict))
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
		num ^= expressionPacks_.GetHashCode();
		num ^= ExpressionPackDict.GetHashCode();
		num ^= heroFavorGifts_.GetHashCode();
		num ^= HeroFavorGiftDict.GetHashCode();
		num ^= voices_.GetHashCode();
		num ^= VoiceDict.GetHashCode();
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
		expressionPacks_.WriteTo(ref output, _repeated_expressionPacks_codec);
		expressionPackDict_.WriteTo(ref output, _map_expressionPackDict_codec);
		heroFavorGifts_.WriteTo(ref output, _repeated_heroFavorGifts_codec);
		heroFavorGiftDict_.WriteTo(ref output, _map_heroFavorGiftDict_codec);
		voices_.WriteTo(ref output, _repeated_voices_codec);
		voiceDict_.WriteTo(ref output, _map_voiceDict_codec);
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
		num += expressionPacks_.CalculateSize(_repeated_expressionPacks_codec);
		num += expressionPackDict_.CalculateSize(_map_expressionPackDict_codec);
		num += heroFavorGifts_.CalculateSize(_repeated_heroFavorGifts_codec);
		num += heroFavorGiftDict_.CalculateSize(_map_heroFavorGiftDict_codec);
		num += voices_.CalculateSize(_repeated_voices_codec);
		num += voiceDict_.CalculateSize(_map_voiceDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CharacterConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			expressionPacks_.Add(other.expressionPacks_);
			expressionPackDict_.MergeFrom(other.expressionPackDict_);
			heroFavorGifts_.Add(other.heroFavorGifts_);
			heroFavorGiftDict_.MergeFrom(other.heroFavorGiftDict_);
			voices_.Add(other.voices_);
			voiceDict_.MergeFrom(other.voiceDict_);
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
				expressionPacks_.AddEntriesFrom(ref input, _repeated_expressionPacks_codec);
				break;
			case 34u:
				expressionPackDict_.AddEntriesFrom(ref input, _map_expressionPackDict_codec);
				break;
			case 42u:
				heroFavorGifts_.AddEntriesFrom(ref input, _repeated_heroFavorGifts_codec);
				break;
			case 50u:
				heroFavorGiftDict_.AddEntriesFrom(ref input, _map_heroFavorGiftDict_codec);
				break;
			case 58u:
				voices_.AddEntriesFrom(ref input, _repeated_voices_codec);
				break;
			case 66u:
				voiceDict_.AddEntriesFrom(ref input, _map_voiceDict_codec);
				break;
			}
		}
	}

	public void Fix(FixCharacterConfigure FixCharacter)
	{
		if (FixCharacter == null)
		{
			return;
		}
		MapField<int, FixCharacterInfoConfigure> infoDict = FixCharacter.InfoDict;
		if (infoDict != null && infoDict.Count > 0)
		{
			for (int num = infos_.Count - 1; num >= 0; num--)
			{
				if (infoDict.ContainsKey(infos_[num].Id))
				{
					infoDict_.Remove(infos_[num].Id);
					infos_.RemoveAt(num);
				}
			}
		}
		if (FixCharacter.ShieldSkillDict == null || FixCharacter.ShieldSkillDict.Count <= 0)
		{
			return;
		}
		foreach (var (num3, fixCharacterShieldSkillConfigure2) in FixCharacter.ShieldSkillDict)
		{
			if (!infoDict_.ContainsKey(num3))
			{
				continue;
			}
			infoDict_[num3].PvePassiveSkills.Clear();
			infoDict_[num3].PvePassiveSkills.AddRange(fixCharacterShieldSkillConfigure2.PvePassiveSkills);
			for (int i = 0; i < infos_.Count; i++)
			{
				if (infos_[i].Id == num3)
				{
					infos_[i].PvePassiveSkills.Clear();
					infos_[i].PvePassiveSkills.AddRange(fixCharacterShieldSkillConfigure2.PvePassiveSkills);
				}
			}
		}
	}
}
