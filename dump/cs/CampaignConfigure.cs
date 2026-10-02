using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CampaignConfigure : IMessage<CampaignConfigure>, IMessage, IEquatable<CampaignConfigure>, IDeepCloneable<CampaignConfigure>, IBufferMessage
{
	private static readonly MessageParser<CampaignConfigure> _parser = new MessageParser<CampaignConfigure>(() => new CampaignConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ChaptersFieldNumber = 1;

	private static readonly FieldCodec<CampaignChapterConfigure> _repeated_chapters_codec = FieldCodec.ForMessage(10u, CampaignChapterConfigure.Parser);

	private readonly RepeatedField<CampaignChapterConfigure> chapters_ = new RepeatedField<CampaignChapterConfigure>();

	public const int ChapterDictFieldNumber = 2;

	private static readonly MapField<int, CampaignChapterConfigure>.Codec _map_chapterDict_codec = new MapField<int, CampaignChapterConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CampaignChapterConfigure.Parser), 18u);

	private readonly MapField<int, CampaignChapterConfigure> chapterDict_ = new MapField<int, CampaignChapterConfigure>();

	public const int LevelsFieldNumber = 3;

	private static readonly FieldCodec<CampaignLevelConfigure> _repeated_levels_codec = FieldCodec.ForMessage(26u, CampaignLevelConfigure.Parser);

	private readonly RepeatedField<CampaignLevelConfigure> levels_ = new RepeatedField<CampaignLevelConfigure>();

	public const int LevelDictFieldNumber = 4;

	private static readonly MapField<int, CampaignLevelConfigure>.Codec _map_levelDict_codec = new MapField<int, CampaignLevelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CampaignLevelConfigure.Parser), 34u);

	private readonly MapField<int, CampaignLevelConfigure> levelDict_ = new MapField<int, CampaignLevelConfigure>();

	public const int TriggersFieldNumber = 5;

	private static readonly FieldCodec<CampaignTriggerConfigure> _repeated_triggers_codec = FieldCodec.ForMessage(42u, CampaignTriggerConfigure.Parser);

	private readonly RepeatedField<CampaignTriggerConfigure> triggers_ = new RepeatedField<CampaignTriggerConfigure>();

	public const int TriggerDictFieldNumber = 6;

	private static readonly MapField<int, CampaignTriggerConfigure>.Codec _map_triggerDict_codec = new MapField<int, CampaignTriggerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CampaignTriggerConfigure.Parser), 50u);

	private readonly MapField<int, CampaignTriggerConfigure> triggerDict_ = new MapField<int, CampaignTriggerConfigure>();

	public const int TryOutsFieldNumber = 7;

	private static readonly FieldCodec<CampaignTryOutConfigure> _repeated_tryOuts_codec = FieldCodec.ForMessage(58u, CampaignTryOutConfigure.Parser);

	private readonly RepeatedField<CampaignTryOutConfigure> tryOuts_ = new RepeatedField<CampaignTryOutConfigure>();

	public const int TryOutDictFieldNumber = 8;

	private static readonly MapField<int, CampaignTryOutConfigure>.Codec _map_tryOutDict_codec = new MapField<int, CampaignTryOutConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CampaignTryOutConfigure.Parser), 66u);

	private readonly MapField<int, CampaignTryOutConfigure> tryOutDict_ = new MapField<int, CampaignTryOutConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CampaignConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CampaignReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CampaignChapterConfigure> Chapters => chapters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CampaignChapterConfigure> ChapterDict => chapterDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CampaignLevelConfigure> Levels => levels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CampaignLevelConfigure> LevelDict => levelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CampaignTriggerConfigure> Triggers => triggers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CampaignTriggerConfigure> TriggerDict => triggerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CampaignTryOutConfigure> TryOuts => tryOuts_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CampaignTryOutConfigure> TryOutDict => tryOutDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignConfigure(CampaignConfigure other)
		: this()
	{
		chapters_ = other.chapters_.Clone();
		chapterDict_ = other.chapterDict_.Clone();
		levels_ = other.levels_.Clone();
		levelDict_ = other.levelDict_.Clone();
		triggers_ = other.triggers_.Clone();
		triggerDict_ = other.triggerDict_.Clone();
		tryOuts_ = other.tryOuts_.Clone();
		tryOutDict_ = other.tryOutDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignConfigure Clone()
	{
		return new CampaignConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CampaignConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CampaignConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!chapters_.Equals(other.chapters_))
		{
			return false;
		}
		if (!ChapterDict.Equals(other.ChapterDict))
		{
			return false;
		}
		if (!levels_.Equals(other.levels_))
		{
			return false;
		}
		if (!LevelDict.Equals(other.LevelDict))
		{
			return false;
		}
		if (!triggers_.Equals(other.triggers_))
		{
			return false;
		}
		if (!TriggerDict.Equals(other.TriggerDict))
		{
			return false;
		}
		if (!tryOuts_.Equals(other.tryOuts_))
		{
			return false;
		}
		if (!TryOutDict.Equals(other.TryOutDict))
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
		num ^= chapters_.GetHashCode();
		num ^= ChapterDict.GetHashCode();
		num ^= levels_.GetHashCode();
		num ^= LevelDict.GetHashCode();
		num ^= triggers_.GetHashCode();
		num ^= TriggerDict.GetHashCode();
		num ^= tryOuts_.GetHashCode();
		num ^= TryOutDict.GetHashCode();
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
		chapters_.WriteTo(ref output, _repeated_chapters_codec);
		chapterDict_.WriteTo(ref output, _map_chapterDict_codec);
		levels_.WriteTo(ref output, _repeated_levels_codec);
		levelDict_.WriteTo(ref output, _map_levelDict_codec);
		triggers_.WriteTo(ref output, _repeated_triggers_codec);
		triggerDict_.WriteTo(ref output, _map_triggerDict_codec);
		tryOuts_.WriteTo(ref output, _repeated_tryOuts_codec);
		tryOutDict_.WriteTo(ref output, _map_tryOutDict_codec);
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
		num += chapters_.CalculateSize(_repeated_chapters_codec);
		num += chapterDict_.CalculateSize(_map_chapterDict_codec);
		num += levels_.CalculateSize(_repeated_levels_codec);
		num += levelDict_.CalculateSize(_map_levelDict_codec);
		num += triggers_.CalculateSize(_repeated_triggers_codec);
		num += triggerDict_.CalculateSize(_map_triggerDict_codec);
		num += tryOuts_.CalculateSize(_repeated_tryOuts_codec);
		num += tryOutDict_.CalculateSize(_map_tryOutDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CampaignConfigure other)
	{
		if (other != null)
		{
			chapters_.Add(other.chapters_);
			chapterDict_.MergeFrom(other.chapterDict_);
			levels_.Add(other.levels_);
			levelDict_.MergeFrom(other.levelDict_);
			triggers_.Add(other.triggers_);
			triggerDict_.MergeFrom(other.triggerDict_);
			tryOuts_.Add(other.tryOuts_);
			tryOutDict_.MergeFrom(other.tryOutDict_);
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
				chapters_.AddEntriesFrom(ref input, _repeated_chapters_codec);
				break;
			case 18u:
				chapterDict_.AddEntriesFrom(ref input, _map_chapterDict_codec);
				break;
			case 26u:
				levels_.AddEntriesFrom(ref input, _repeated_levels_codec);
				break;
			case 34u:
				levelDict_.AddEntriesFrom(ref input, _map_levelDict_codec);
				break;
			case 42u:
				triggers_.AddEntriesFrom(ref input, _repeated_triggers_codec);
				break;
			case 50u:
				triggerDict_.AddEntriesFrom(ref input, _map_triggerDict_codec);
				break;
			case 58u:
				tryOuts_.AddEntriesFrom(ref input, _repeated_tryOuts_codec);
				break;
			case 66u:
				tryOutDict_.AddEntriesFrom(ref input, _map_tryOutDict_codec);
				break;
			}
		}
	}
}
