using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ActivityConfigure : IMessage<ActivityConfigure>, IMessage, IEquatable<ActivityConfigure>, IDeepCloneable<ActivityConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityConfigure> _parser = new MessageParser<ActivityConfigure>(() => new ActivityConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ActivityEntrance2SFieldNumber = 1;

	private static readonly FieldCodec<ActivityActivityEntrance2Configure> _repeated_activityEntrance2S_codec = FieldCodec.ForMessage(10u, ActivityActivityEntrance2Configure.Parser);

	private readonly RepeatedField<ActivityActivityEntrance2Configure> activityEntrance2S_ = new RepeatedField<ActivityActivityEntrance2Configure>();

	public const int ActivityEntrance2DictFieldNumber = 2;

	private static readonly MapField<int, ActivityActivityEntrance2Configure>.Codec _map_activityEntrance2Dict_codec = new MapField<int, ActivityActivityEntrance2Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityActivityEntrance2Configure.Parser), 18u);

	private readonly MapField<int, ActivityActivityEntrance2Configure> activityEntrance2Dict_ = new MapField<int, ActivityActivityEntrance2Configure>();

	public const int InfosFieldNumber = 3;

	private static readonly FieldCodec<ActivityInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(26u, ActivityInfoConfigure.Parser);

	private readonly RepeatedField<ActivityInfoConfigure> infos_ = new RepeatedField<ActivityInfoConfigure>();

	public const int InfoDictFieldNumber = 4;

	private static readonly MapField<int, ActivityInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, ActivityInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityInfoConfigure.Parser), 34u);

	private readonly MapField<int, ActivityInfoConfigure> infoDict_ = new MapField<int, ActivityInfoConfigure>();

	public const int TasksFieldNumber = 5;

	private static readonly FieldCodec<ActivityTaskConfigure> _repeated_tasks_codec = FieldCodec.ForMessage(42u, ActivityTaskConfigure.Parser);

	private readonly RepeatedField<ActivityTaskConfigure> tasks_ = new RepeatedField<ActivityTaskConfigure>();

	public const int TaskDictFieldNumber = 6;

	private static readonly MapField<int, ActivityTaskConfigure>.Codec _map_taskDict_codec = new MapField<int, ActivityTaskConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityTaskConfigure.Parser), 50u);

	private readonly MapField<int, ActivityTaskConfigure> taskDict_ = new MapField<int, ActivityTaskConfigure>();

	public const int ScratchoffsFieldNumber = 7;

	private static readonly FieldCodec<ActivityScratchoffConfigure> _repeated_scratchoffs_codec = FieldCodec.ForMessage(58u, ActivityScratchoffConfigure.Parser);

	private readonly RepeatedField<ActivityScratchoffConfigure> scratchoffs_ = new RepeatedField<ActivityScratchoffConfigure>();

	public const int ScratchoffDictFieldNumber = 8;

	private static readonly MapField<int, ActivityScratchoffConfigure>.Codec _map_scratchoffDict_codec = new MapField<int, ActivityScratchoffConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityScratchoffConfigure.Parser), 66u);

	private readonly MapField<int, ActivityScratchoffConfigure> scratchoffDict_ = new MapField<int, ActivityScratchoffConfigure>();

	public const int LightingsFieldNumber = 9;

	private static readonly FieldCodec<ActivityLightingConfigure> _repeated_lightings_codec = FieldCodec.ForMessage(74u, ActivityLightingConfigure.Parser);

	private readonly RepeatedField<ActivityLightingConfigure> lightings_ = new RepeatedField<ActivityLightingConfigure>();

	public const int LightingDictFieldNumber = 10;

	private static readonly MapField<int, ActivityLightingConfigure>.Codec _map_lightingDict_codec = new MapField<int, ActivityLightingConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityLightingConfigure.Parser), 82u);

	private readonly MapField<int, ActivityLightingConfigure> lightingDict_ = new MapField<int, ActivityLightingConfigure>();

	public const int ScratchoffPoolsFieldNumber = 11;

	private static readonly FieldCodec<ActivityScratchoffPoolConfigure> _repeated_scratchoffPools_codec = FieldCodec.ForMessage(90u, ActivityScratchoffPoolConfigure.Parser);

	private readonly RepeatedField<ActivityScratchoffPoolConfigure> scratchoffPools_ = new RepeatedField<ActivityScratchoffPoolConfigure>();

	public const int ScratchoffPoolDictFieldNumber = 12;

	private static readonly MapField<int, ActivityScratchoffPoolConfigure>.Codec _map_scratchoffPoolDict_codec = new MapField<int, ActivityScratchoffPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityScratchoffPoolConfigure.Parser), 98u);

	private readonly MapField<int, ActivityScratchoffPoolConfigure> scratchoffPoolDict_ = new MapField<int, ActivityScratchoffPoolConfigure>();

	public const int GachasFieldNumber = 13;

	private static readonly FieldCodec<ActivityGachaConfigure> _repeated_gachas_codec = FieldCodec.ForMessage(106u, ActivityGachaConfigure.Parser);

	private readonly RepeatedField<ActivityGachaConfigure> gachas_ = new RepeatedField<ActivityGachaConfigure>();

	public const int GachaDictFieldNumber = 14;

	private static readonly MapField<int, ActivityGachaConfigure>.Codec _map_gachaDict_codec = new MapField<int, ActivityGachaConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityGachaConfigure.Parser), 114u);

	private readonly MapField<int, ActivityGachaConfigure> gachaDict_ = new MapField<int, ActivityGachaConfigure>();

	public const int BingoFlipsFieldNumber = 15;

	private static readonly FieldCodec<ActivityBingoFlipConfigure> _repeated_bingoFlips_codec = FieldCodec.ForMessage(122u, ActivityBingoFlipConfigure.Parser);

	private readonly RepeatedField<ActivityBingoFlipConfigure> bingoFlips_ = new RepeatedField<ActivityBingoFlipConfigure>();

	public const int BingoFlipDictFieldNumber = 16;

	private static readonly MapField<int, ActivityBingoFlipConfigure>.Codec _map_bingoFlipDict_codec = new MapField<int, ActivityBingoFlipConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityBingoFlipConfigure.Parser), 130u);

	private readonly MapField<int, ActivityBingoFlipConfigure> bingoFlipDict_ = new MapField<int, ActivityBingoFlipConfigure>();

	public const int BingoFlipPoolsFieldNumber = 17;

	private static readonly FieldCodec<ActivityBingoFlipPoolConfigure> _repeated_bingoFlipPools_codec = FieldCodec.ForMessage(138u, ActivityBingoFlipPoolConfigure.Parser);

	private readonly RepeatedField<ActivityBingoFlipPoolConfigure> bingoFlipPools_ = new RepeatedField<ActivityBingoFlipPoolConfigure>();

	public const int BingoFlipPoolDictFieldNumber = 18;

	private static readonly MapField<int, ActivityBingoFlipPoolConfigure>.Codec _map_bingoFlipPoolDict_codec = new MapField<int, ActivityBingoFlipPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ActivityBingoFlipPoolConfigure.Parser), 146u);

	private readonly MapField<int, ActivityBingoFlipPoolConfigure> bingoFlipPoolDict_ = new MapField<int, ActivityBingoFlipPoolConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[11];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityActivityEntrance2Configure> ActivityEntrance2S => activityEntrance2S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityActivityEntrance2Configure> ActivityEntrance2Dict => activityEntrance2Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityTaskConfigure> Tasks => tasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityTaskConfigure> TaskDict => taskDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityScratchoffConfigure> Scratchoffs => scratchoffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityScratchoffConfigure> ScratchoffDict => scratchoffDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityLightingConfigure> Lightings => lightings_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityLightingConfigure> LightingDict => lightingDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityScratchoffPoolConfigure> ScratchoffPools => scratchoffPools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityScratchoffPoolConfigure> ScratchoffPoolDict => scratchoffPoolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityGachaConfigure> Gachas => gachas_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityGachaConfigure> GachaDict => gachaDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityBingoFlipConfigure> BingoFlips => bingoFlips_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityBingoFlipConfigure> BingoFlipDict => bingoFlipDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityBingoFlipPoolConfigure> BingoFlipPools => bingoFlipPools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityBingoFlipPoolConfigure> BingoFlipPoolDict => bingoFlipPoolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityConfigure(ActivityConfigure other)
		: this()
	{
		activityEntrance2S_ = other.activityEntrance2S_.Clone();
		activityEntrance2Dict_ = other.activityEntrance2Dict_.Clone();
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		tasks_ = other.tasks_.Clone();
		taskDict_ = other.taskDict_.Clone();
		scratchoffs_ = other.scratchoffs_.Clone();
		scratchoffDict_ = other.scratchoffDict_.Clone();
		lightings_ = other.lightings_.Clone();
		lightingDict_ = other.lightingDict_.Clone();
		scratchoffPools_ = other.scratchoffPools_.Clone();
		scratchoffPoolDict_ = other.scratchoffPoolDict_.Clone();
		gachas_ = other.gachas_.Clone();
		gachaDict_ = other.gachaDict_.Clone();
		bingoFlips_ = other.bingoFlips_.Clone();
		bingoFlipDict_ = other.bingoFlipDict_.Clone();
		bingoFlipPools_ = other.bingoFlipPools_.Clone();
		bingoFlipPoolDict_ = other.bingoFlipPoolDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityConfigure Clone()
	{
		return new ActivityConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!activityEntrance2S_.Equals(other.activityEntrance2S_))
		{
			return false;
		}
		if (!ActivityEntrance2Dict.Equals(other.ActivityEntrance2Dict))
		{
			return false;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!tasks_.Equals(other.tasks_))
		{
			return false;
		}
		if (!TaskDict.Equals(other.TaskDict))
		{
			return false;
		}
		if (!scratchoffs_.Equals(other.scratchoffs_))
		{
			return false;
		}
		if (!ScratchoffDict.Equals(other.ScratchoffDict))
		{
			return false;
		}
		if (!lightings_.Equals(other.lightings_))
		{
			return false;
		}
		if (!LightingDict.Equals(other.LightingDict))
		{
			return false;
		}
		if (!scratchoffPools_.Equals(other.scratchoffPools_))
		{
			return false;
		}
		if (!ScratchoffPoolDict.Equals(other.ScratchoffPoolDict))
		{
			return false;
		}
		if (!gachas_.Equals(other.gachas_))
		{
			return false;
		}
		if (!GachaDict.Equals(other.GachaDict))
		{
			return false;
		}
		if (!bingoFlips_.Equals(other.bingoFlips_))
		{
			return false;
		}
		if (!BingoFlipDict.Equals(other.BingoFlipDict))
		{
			return false;
		}
		if (!bingoFlipPools_.Equals(other.bingoFlipPools_))
		{
			return false;
		}
		if (!BingoFlipPoolDict.Equals(other.BingoFlipPoolDict))
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
		num ^= activityEntrance2S_.GetHashCode();
		num ^= ActivityEntrance2Dict.GetHashCode();
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= tasks_.GetHashCode();
		num ^= TaskDict.GetHashCode();
		num ^= scratchoffs_.GetHashCode();
		num ^= ScratchoffDict.GetHashCode();
		num ^= lightings_.GetHashCode();
		num ^= LightingDict.GetHashCode();
		num ^= scratchoffPools_.GetHashCode();
		num ^= ScratchoffPoolDict.GetHashCode();
		num ^= gachas_.GetHashCode();
		num ^= GachaDict.GetHashCode();
		num ^= bingoFlips_.GetHashCode();
		num ^= BingoFlipDict.GetHashCode();
		num ^= bingoFlipPools_.GetHashCode();
		num ^= BingoFlipPoolDict.GetHashCode();
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
		activityEntrance2S_.WriteTo(ref output, _repeated_activityEntrance2S_codec);
		activityEntrance2Dict_.WriteTo(ref output, _map_activityEntrance2Dict_codec);
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		tasks_.WriteTo(ref output, _repeated_tasks_codec);
		taskDict_.WriteTo(ref output, _map_taskDict_codec);
		scratchoffs_.WriteTo(ref output, _repeated_scratchoffs_codec);
		scratchoffDict_.WriteTo(ref output, _map_scratchoffDict_codec);
		lightings_.WriteTo(ref output, _repeated_lightings_codec);
		lightingDict_.WriteTo(ref output, _map_lightingDict_codec);
		scratchoffPools_.WriteTo(ref output, _repeated_scratchoffPools_codec);
		scratchoffPoolDict_.WriteTo(ref output, _map_scratchoffPoolDict_codec);
		gachas_.WriteTo(ref output, _repeated_gachas_codec);
		gachaDict_.WriteTo(ref output, _map_gachaDict_codec);
		bingoFlips_.WriteTo(ref output, _repeated_bingoFlips_codec);
		bingoFlipDict_.WriteTo(ref output, _map_bingoFlipDict_codec);
		bingoFlipPools_.WriteTo(ref output, _repeated_bingoFlipPools_codec);
		bingoFlipPoolDict_.WriteTo(ref output, _map_bingoFlipPoolDict_codec);
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
		num += activityEntrance2S_.CalculateSize(_repeated_activityEntrance2S_codec);
		num += activityEntrance2Dict_.CalculateSize(_map_activityEntrance2Dict_codec);
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += tasks_.CalculateSize(_repeated_tasks_codec);
		num += taskDict_.CalculateSize(_map_taskDict_codec);
		num += scratchoffs_.CalculateSize(_repeated_scratchoffs_codec);
		num += scratchoffDict_.CalculateSize(_map_scratchoffDict_codec);
		num += lightings_.CalculateSize(_repeated_lightings_codec);
		num += lightingDict_.CalculateSize(_map_lightingDict_codec);
		num += scratchoffPools_.CalculateSize(_repeated_scratchoffPools_codec);
		num += scratchoffPoolDict_.CalculateSize(_map_scratchoffPoolDict_codec);
		num += gachas_.CalculateSize(_repeated_gachas_codec);
		num += gachaDict_.CalculateSize(_map_gachaDict_codec);
		num += bingoFlips_.CalculateSize(_repeated_bingoFlips_codec);
		num += bingoFlipDict_.CalculateSize(_map_bingoFlipDict_codec);
		num += bingoFlipPools_.CalculateSize(_repeated_bingoFlipPools_codec);
		num += bingoFlipPoolDict_.CalculateSize(_map_bingoFlipPoolDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityConfigure other)
	{
		if (other != null)
		{
			activityEntrance2S_.Add(other.activityEntrance2S_);
			activityEntrance2Dict_.MergeFrom(other.activityEntrance2Dict_);
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			tasks_.Add(other.tasks_);
			taskDict_.MergeFrom(other.taskDict_);
			scratchoffs_.Add(other.scratchoffs_);
			scratchoffDict_.MergeFrom(other.scratchoffDict_);
			lightings_.Add(other.lightings_);
			lightingDict_.MergeFrom(other.lightingDict_);
			scratchoffPools_.Add(other.scratchoffPools_);
			scratchoffPoolDict_.MergeFrom(other.scratchoffPoolDict_);
			gachas_.Add(other.gachas_);
			gachaDict_.MergeFrom(other.gachaDict_);
			bingoFlips_.Add(other.bingoFlips_);
			bingoFlipDict_.MergeFrom(other.bingoFlipDict_);
			bingoFlipPools_.Add(other.bingoFlipPools_);
			bingoFlipPoolDict_.MergeFrom(other.bingoFlipPoolDict_);
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
				activityEntrance2S_.AddEntriesFrom(ref input, _repeated_activityEntrance2S_codec);
				break;
			case 18u:
				activityEntrance2Dict_.AddEntriesFrom(ref input, _map_activityEntrance2Dict_codec);
				break;
			case 26u:
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 34u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 42u:
				tasks_.AddEntriesFrom(ref input, _repeated_tasks_codec);
				break;
			case 50u:
				taskDict_.AddEntriesFrom(ref input, _map_taskDict_codec);
				break;
			case 58u:
				scratchoffs_.AddEntriesFrom(ref input, _repeated_scratchoffs_codec);
				break;
			case 66u:
				scratchoffDict_.AddEntriesFrom(ref input, _map_scratchoffDict_codec);
				break;
			case 74u:
				lightings_.AddEntriesFrom(ref input, _repeated_lightings_codec);
				break;
			case 82u:
				lightingDict_.AddEntriesFrom(ref input, _map_lightingDict_codec);
				break;
			case 90u:
				scratchoffPools_.AddEntriesFrom(ref input, _repeated_scratchoffPools_codec);
				break;
			case 98u:
				scratchoffPoolDict_.AddEntriesFrom(ref input, _map_scratchoffPoolDict_codec);
				break;
			case 106u:
				gachas_.AddEntriesFrom(ref input, _repeated_gachas_codec);
				break;
			case 114u:
				gachaDict_.AddEntriesFrom(ref input, _map_gachaDict_codec);
				break;
			case 122u:
				bingoFlips_.AddEntriesFrom(ref input, _repeated_bingoFlips_codec);
				break;
			case 130u:
				bingoFlipDict_.AddEntriesFrom(ref input, _map_bingoFlipDict_codec);
				break;
			case 138u:
				bingoFlipPools_.AddEntriesFrom(ref input, _repeated_bingoFlipPools_codec);
				break;
			case 146u:
				bingoFlipPoolDict_.AddEntriesFrom(ref input, _map_bingoFlipPoolDict_codec);
				break;
			}
		}
	}
}
