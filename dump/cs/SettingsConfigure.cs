using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SettingsConfigure : IMessage<SettingsConfigure>, IMessage, IEquatable<SettingsConfigure>, IDeepCloneable<SettingsConfigure>, IBufferMessage
{
	private static readonly MessageParser<SettingsConfigure> _parser = new MessageParser<SettingsConfigure>(() => new SettingsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ResolutionsFieldNumber = 1;

	private static readonly FieldCodec<SettingsResolutionConfigure> _repeated_resolutions_codec = FieldCodec.ForMessage(10u, SettingsResolutionConfigure.Parser);

	private readonly RepeatedField<SettingsResolutionConfigure> resolutions_ = new RepeatedField<SettingsResolutionConfigure>();

	public const int ResolutionDictFieldNumber = 2;

	private static readonly MapField<int, SettingsResolutionConfigure>.Codec _map_resolutionDict_codec = new MapField<int, SettingsResolutionConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsResolutionConfigure.Parser), 18u);

	private readonly MapField<int, SettingsResolutionConfigure> resolutionDict_ = new MapField<int, SettingsResolutionConfigure>();

	public const int RefreshRatesFieldNumber = 3;

	private static readonly FieldCodec<SettingsRefreshRateConfigure> _repeated_refreshRates_codec = FieldCodec.ForMessage(26u, SettingsRefreshRateConfigure.Parser);

	private readonly RepeatedField<SettingsRefreshRateConfigure> refreshRates_ = new RepeatedField<SettingsRefreshRateConfigure>();

	public const int RefreshRateDictFieldNumber = 4;

	private static readonly MapField<int, SettingsRefreshRateConfigure>.Codec _map_refreshRateDict_codec = new MapField<int, SettingsRefreshRateConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsRefreshRateConfigure.Parser), 34u);

	private readonly MapField<int, SettingsRefreshRateConfigure> refreshRateDict_ = new MapField<int, SettingsRefreshRateConfigure>();

	public const int DisplayModesFieldNumber = 5;

	private static readonly FieldCodec<SettingsDisplayModeConfigure> _repeated_displayModes_codec = FieldCodec.ForMessage(42u, SettingsDisplayModeConfigure.Parser);

	private readonly RepeatedField<SettingsDisplayModeConfigure> displayModes_ = new RepeatedField<SettingsDisplayModeConfigure>();

	public const int DisplayModeDictFieldNumber = 6;

	private static readonly MapField<int, SettingsDisplayModeConfigure>.Codec _map_displayModeDict_codec = new MapField<int, SettingsDisplayModeConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsDisplayModeConfigure.Parser), 50u);

	private readonly MapField<int, SettingsDisplayModeConfigure> displayModeDict_ = new MapField<int, SettingsDisplayModeConfigure>();

	public const int LanguagesFieldNumber = 7;

	private static readonly FieldCodec<SettingsLanguageConfigure> _repeated_languages_codec = FieldCodec.ForMessage(58u, SettingsLanguageConfigure.Parser);

	private readonly RepeatedField<SettingsLanguageConfigure> languages_ = new RepeatedField<SettingsLanguageConfigure>();

	public const int LanguageDictFieldNumber = 8;

	private static readonly MapField<int, SettingsLanguageConfigure>.Codec _map_languageDict_codec = new MapField<int, SettingsLanguageConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsLanguageConfigure.Parser), 66u);

	private readonly MapField<int, SettingsLanguageConfigure> languageDict_ = new MapField<int, SettingsLanguageConfigure>();

	public const int AnchorsFieldNumber = 9;

	private static readonly FieldCodec<SettingsAnchorConfigure> _repeated_anchors_codec = FieldCodec.ForMessage(74u, SettingsAnchorConfigure.Parser);

	private readonly RepeatedField<SettingsAnchorConfigure> anchors_ = new RepeatedField<SettingsAnchorConfigure>();

	public const int AnchorDictFieldNumber = 10;

	private static readonly MapField<int, SettingsAnchorConfigure>.Codec _map_anchorDict_codec = new MapField<int, SettingsAnchorConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsAnchorConfigure.Parser), 82u);

	private readonly MapField<int, SettingsAnchorConfigure> anchorDict_ = new MapField<int, SettingsAnchorConfigure>();

	public const int QualitysFieldNumber = 11;

	private static readonly FieldCodec<SettingsQualityConfigure> _repeated_qualitys_codec = FieldCodec.ForMessage(90u, SettingsQualityConfigure.Parser);

	private readonly RepeatedField<SettingsQualityConfigure> qualitys_ = new RepeatedField<SettingsQualityConfigure>();

	public const int QualityDictFieldNumber = 12;

	private static readonly MapField<int, SettingsQualityConfigure>.Codec _map_qualityDict_codec = new MapField<int, SettingsQualityConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsQualityConfigure.Parser), 98u);

	private readonly MapField<int, SettingsQualityConfigure> qualityDict_ = new MapField<int, SettingsQualityConfigure>();

	public const int VoiceLanguagesFieldNumber = 13;

	private static readonly FieldCodec<SettingsVoiceLanguageConfigure> _repeated_voiceLanguages_codec = FieldCodec.ForMessage(106u, SettingsVoiceLanguageConfigure.Parser);

	private readonly RepeatedField<SettingsVoiceLanguageConfigure> voiceLanguages_ = new RepeatedField<SettingsVoiceLanguageConfigure>();

	public const int VoiceLanguageDictFieldNumber = 14;

	private static readonly MapField<int, SettingsVoiceLanguageConfigure>.Codec _map_voiceLanguageDict_codec = new MapField<int, SettingsVoiceLanguageConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SettingsVoiceLanguageConfigure.Parser), 114u);

	private readonly MapField<int, SettingsVoiceLanguageConfigure> voiceLanguageDict_ = new MapField<int, SettingsVoiceLanguageConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SettingsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SettingsReflection.Descriptor.MessageTypes[7];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsResolutionConfigure> Resolutions => resolutions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsResolutionConfigure> ResolutionDict => resolutionDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsRefreshRateConfigure> RefreshRates => refreshRates_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsRefreshRateConfigure> RefreshRateDict => refreshRateDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsDisplayModeConfigure> DisplayModes => displayModes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsDisplayModeConfigure> DisplayModeDict => displayModeDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsLanguageConfigure> Languages => languages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsLanguageConfigure> LanguageDict => languageDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsAnchorConfigure> Anchors => anchors_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsAnchorConfigure> AnchorDict => anchorDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsQualityConfigure> Qualitys => qualitys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsQualityConfigure> QualityDict => qualityDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SettingsVoiceLanguageConfigure> VoiceLanguages => voiceLanguages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SettingsVoiceLanguageConfigure> VoiceLanguageDict => voiceLanguageDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsConfigure(SettingsConfigure other)
		: this()
	{
		resolutions_ = other.resolutions_.Clone();
		resolutionDict_ = other.resolutionDict_.Clone();
		refreshRates_ = other.refreshRates_.Clone();
		refreshRateDict_ = other.refreshRateDict_.Clone();
		displayModes_ = other.displayModes_.Clone();
		displayModeDict_ = other.displayModeDict_.Clone();
		languages_ = other.languages_.Clone();
		languageDict_ = other.languageDict_.Clone();
		anchors_ = other.anchors_.Clone();
		anchorDict_ = other.anchorDict_.Clone();
		qualitys_ = other.qualitys_.Clone();
		qualityDict_ = other.qualityDict_.Clone();
		voiceLanguages_ = other.voiceLanguages_.Clone();
		voiceLanguageDict_ = other.voiceLanguageDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsConfigure Clone()
	{
		return new SettingsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SettingsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SettingsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!resolutions_.Equals(other.resolutions_))
		{
			return false;
		}
		if (!ResolutionDict.Equals(other.ResolutionDict))
		{
			return false;
		}
		if (!refreshRates_.Equals(other.refreshRates_))
		{
			return false;
		}
		if (!RefreshRateDict.Equals(other.RefreshRateDict))
		{
			return false;
		}
		if (!displayModes_.Equals(other.displayModes_))
		{
			return false;
		}
		if (!DisplayModeDict.Equals(other.DisplayModeDict))
		{
			return false;
		}
		if (!languages_.Equals(other.languages_))
		{
			return false;
		}
		if (!LanguageDict.Equals(other.LanguageDict))
		{
			return false;
		}
		if (!anchors_.Equals(other.anchors_))
		{
			return false;
		}
		if (!AnchorDict.Equals(other.AnchorDict))
		{
			return false;
		}
		if (!qualitys_.Equals(other.qualitys_))
		{
			return false;
		}
		if (!QualityDict.Equals(other.QualityDict))
		{
			return false;
		}
		if (!voiceLanguages_.Equals(other.voiceLanguages_))
		{
			return false;
		}
		if (!VoiceLanguageDict.Equals(other.VoiceLanguageDict))
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
		num ^= resolutions_.GetHashCode();
		num ^= ResolutionDict.GetHashCode();
		num ^= refreshRates_.GetHashCode();
		num ^= RefreshRateDict.GetHashCode();
		num ^= displayModes_.GetHashCode();
		num ^= DisplayModeDict.GetHashCode();
		num ^= languages_.GetHashCode();
		num ^= LanguageDict.GetHashCode();
		num ^= anchors_.GetHashCode();
		num ^= AnchorDict.GetHashCode();
		num ^= qualitys_.GetHashCode();
		num ^= QualityDict.GetHashCode();
		num ^= voiceLanguages_.GetHashCode();
		num ^= VoiceLanguageDict.GetHashCode();
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
		resolutions_.WriteTo(ref output, _repeated_resolutions_codec);
		resolutionDict_.WriteTo(ref output, _map_resolutionDict_codec);
		refreshRates_.WriteTo(ref output, _repeated_refreshRates_codec);
		refreshRateDict_.WriteTo(ref output, _map_refreshRateDict_codec);
		displayModes_.WriteTo(ref output, _repeated_displayModes_codec);
		displayModeDict_.WriteTo(ref output, _map_displayModeDict_codec);
		languages_.WriteTo(ref output, _repeated_languages_codec);
		languageDict_.WriteTo(ref output, _map_languageDict_codec);
		anchors_.WriteTo(ref output, _repeated_anchors_codec);
		anchorDict_.WriteTo(ref output, _map_anchorDict_codec);
		qualitys_.WriteTo(ref output, _repeated_qualitys_codec);
		qualityDict_.WriteTo(ref output, _map_qualityDict_codec);
		voiceLanguages_.WriteTo(ref output, _repeated_voiceLanguages_codec);
		voiceLanguageDict_.WriteTo(ref output, _map_voiceLanguageDict_codec);
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
		num += resolutions_.CalculateSize(_repeated_resolutions_codec);
		num += resolutionDict_.CalculateSize(_map_resolutionDict_codec);
		num += refreshRates_.CalculateSize(_repeated_refreshRates_codec);
		num += refreshRateDict_.CalculateSize(_map_refreshRateDict_codec);
		num += displayModes_.CalculateSize(_repeated_displayModes_codec);
		num += displayModeDict_.CalculateSize(_map_displayModeDict_codec);
		num += languages_.CalculateSize(_repeated_languages_codec);
		num += languageDict_.CalculateSize(_map_languageDict_codec);
		num += anchors_.CalculateSize(_repeated_anchors_codec);
		num += anchorDict_.CalculateSize(_map_anchorDict_codec);
		num += qualitys_.CalculateSize(_repeated_qualitys_codec);
		num += qualityDict_.CalculateSize(_map_qualityDict_codec);
		num += voiceLanguages_.CalculateSize(_repeated_voiceLanguages_codec);
		num += voiceLanguageDict_.CalculateSize(_map_voiceLanguageDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SettingsConfigure other)
	{
		if (other != null)
		{
			resolutions_.Add(other.resolutions_);
			resolutionDict_.MergeFrom(other.resolutionDict_);
			refreshRates_.Add(other.refreshRates_);
			refreshRateDict_.MergeFrom(other.refreshRateDict_);
			displayModes_.Add(other.displayModes_);
			displayModeDict_.MergeFrom(other.displayModeDict_);
			languages_.Add(other.languages_);
			languageDict_.MergeFrom(other.languageDict_);
			anchors_.Add(other.anchors_);
			anchorDict_.MergeFrom(other.anchorDict_);
			qualitys_.Add(other.qualitys_);
			qualityDict_.MergeFrom(other.qualityDict_);
			voiceLanguages_.Add(other.voiceLanguages_);
			voiceLanguageDict_.MergeFrom(other.voiceLanguageDict_);
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
				resolutions_.AddEntriesFrom(ref input, _repeated_resolutions_codec);
				break;
			case 18u:
				resolutionDict_.AddEntriesFrom(ref input, _map_resolutionDict_codec);
				break;
			case 26u:
				refreshRates_.AddEntriesFrom(ref input, _repeated_refreshRates_codec);
				break;
			case 34u:
				refreshRateDict_.AddEntriesFrom(ref input, _map_refreshRateDict_codec);
				break;
			case 42u:
				displayModes_.AddEntriesFrom(ref input, _repeated_displayModes_codec);
				break;
			case 50u:
				displayModeDict_.AddEntriesFrom(ref input, _map_displayModeDict_codec);
				break;
			case 58u:
				languages_.AddEntriesFrom(ref input, _repeated_languages_codec);
				break;
			case 66u:
				languageDict_.AddEntriesFrom(ref input, _map_languageDict_codec);
				break;
			case 74u:
				anchors_.AddEntriesFrom(ref input, _repeated_anchors_codec);
				break;
			case 82u:
				anchorDict_.AddEntriesFrom(ref input, _map_anchorDict_codec);
				break;
			case 90u:
				qualitys_.AddEntriesFrom(ref input, _repeated_qualitys_codec);
				break;
			case 98u:
				qualityDict_.AddEntriesFrom(ref input, _map_qualityDict_codec);
				break;
			case 106u:
				voiceLanguages_.AddEntriesFrom(ref input, _repeated_voiceLanguages_codec);
				break;
			case 114u:
				voiceLanguageDict_.AddEntriesFrom(ref input, _map_voiceLanguageDict_codec);
				break;
			}
		}
	}
}
