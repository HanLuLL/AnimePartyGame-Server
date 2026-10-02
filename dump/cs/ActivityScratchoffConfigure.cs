using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class ActivityScratchoffConfigure : IMessage<ActivityScratchoffConfigure>, IMessage, IEquatable<ActivityScratchoffConfigure>, IDeepCloneable<ActivityScratchoffConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityScratchoffConfigure> _parser = new MessageParser<ActivityScratchoffConfigure>(() => new ActivityScratchoffConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BgFieldNumber = 2;

	private string bg_ = "";

	public const int TitleImagesFieldNumber = 3;

	private static readonly FieldCodec<string> _repeated_titleImages_codec = FieldCodec.ForString(26u);

	private readonly RepeatedField<string> titleImages_ = new RepeatedField<string>();

	public const int MainHerosFieldNumber = 4;

	private static readonly FieldCodec<string> _repeated_mainHeros_codec = FieldCodec.ForString(34u);

	private readonly RepeatedField<string> mainHeros_ = new RepeatedField<string>();

	public const int MainHerosSFWFieldNumber = 5;

	private static readonly FieldCodec<string> _repeated_mainHerosSFW_codec = FieldCodec.ForString(42u);

	private readonly RepeatedField<string> mainHerosSFW_ = new RepeatedField<string>();

	public const int TaskNPCFieldNumber = 6;

	private string taskNPC_ = "";

	public const int TaskBeginTimeFieldNumber = 7;

	private Timestamp taskBeginTime_;

	public const int TaskEndTimeFieldNumber = 8;

	private Timestamp taskEndTime_;

	public const int EndHeroFieldNumber = 9;

	private string endHero_ = "";

	public const int EndHeroSFWFieldNumber = 10;

	private string endHeroSFW_ = "";

	public const int ScratchoffHerosFieldNumber = 11;

	private static readonly FieldCodec<string> _repeated_scratchoffHeros_codec = FieldCodec.ForString(90u);

	private readonly RepeatedField<string> scratchoffHeros_ = new RepeatedField<string>();

	public const int ScratchoffHerosSFWFieldNumber = 12;

	private static readonly FieldCodec<string> _repeated_scratchoffHerosSFW_codec = FieldCodec.ForString(98u);

	private readonly RepeatedField<string> scratchoffHerosSFW_ = new RepeatedField<string>();

	public const int ScratchoffNPCFieldNumber = 13;

	private string scratchoffNPC_ = "";

	public const int BeginTimeFieldNumber = 14;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 15;

	private Timestamp endTime_;

	public const int ScratchoffPoolIdsFieldNumber = 16;

	private static readonly FieldCodec<int> _repeated_scratchoffPoolIds_codec = FieldCodec.ForSFixed32(130u);

	private readonly RepeatedField<int> scratchoffPoolIds_ = new RepeatedField<int>();

	public const int RewardsFieldNumber = 17;

	private static readonly MapField<int, int>.Codec _map_rewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 138u);

	private readonly MapField<int, int> rewards_ = new MapField<int, int>();

	public const int ScratchVoiceFieldNumber = 18;

	private int scratchVoice_;

	public const int SpendsFieldNumber = 19;

	private static readonly MapField<int, int>.Codec _map_spends_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 154u);

	private readonly MapField<int, int> spends_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityScratchoffConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Bg
	{
		get
		{
			return bg_;
		}
		private set
		{
			bg_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> TitleImages => titleImages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> MainHeros => mainHeros_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> MainHerosSFW => mainHerosSFW_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string TaskNPC
	{
		get
		{
			return taskNPC_;
		}
		private set
		{
			taskNPC_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp TaskBeginTime
	{
		get
		{
			return taskBeginTime_;
		}
		private set
		{
			taskBeginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp TaskEndTime
	{
		get
		{
			return taskEndTime_;
		}
		private set
		{
			taskEndTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string EndHero
	{
		get
		{
			return endHero_;
		}
		private set
		{
			endHero_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string EndHeroSFW
	{
		get
		{
			return endHeroSFW_;
		}
		private set
		{
			endHeroSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> ScratchoffHeros => scratchoffHeros_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> ScratchoffHerosSFW => scratchoffHerosSFW_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ScratchoffNPC
	{
		get
		{
			return scratchoffNPC_;
		}
		private set
		{
			scratchoffNPC_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ScratchoffPoolIds => scratchoffPoolIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Rewards => rewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ScratchVoice
	{
		get
		{
			return scratchVoice_;
		}
		private set
		{
			scratchVoice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Spends => spends_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffConfigure(ActivityScratchoffConfigure other)
		: this()
	{
		id_ = other.id_;
		bg_ = other.bg_;
		titleImages_ = other.titleImages_.Clone();
		mainHeros_ = other.mainHeros_.Clone();
		mainHerosSFW_ = other.mainHerosSFW_.Clone();
		taskNPC_ = other.taskNPC_;
		taskBeginTime_ = ((other.taskBeginTime_ != null) ? other.taskBeginTime_.Clone() : null);
		taskEndTime_ = ((other.taskEndTime_ != null) ? other.taskEndTime_.Clone() : null);
		endHero_ = other.endHero_;
		endHeroSFW_ = other.endHeroSFW_;
		scratchoffHeros_ = other.scratchoffHeros_.Clone();
		scratchoffHerosSFW_ = other.scratchoffHerosSFW_.Clone();
		scratchoffNPC_ = other.scratchoffNPC_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		scratchoffPoolIds_ = other.scratchoffPoolIds_.Clone();
		rewards_ = other.rewards_.Clone();
		scratchVoice_ = other.scratchVoice_;
		spends_ = other.spends_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffConfigure Clone()
	{
		return new ActivityScratchoffConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityScratchoffConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityScratchoffConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (Bg != other.Bg)
		{
			return false;
		}
		if (!titleImages_.Equals(other.titleImages_))
		{
			return false;
		}
		if (!mainHeros_.Equals(other.mainHeros_))
		{
			return false;
		}
		if (!mainHerosSFW_.Equals(other.mainHerosSFW_))
		{
			return false;
		}
		if (TaskNPC != other.TaskNPC)
		{
			return false;
		}
		if (!object.Equals(TaskBeginTime, other.TaskBeginTime))
		{
			return false;
		}
		if (!object.Equals(TaskEndTime, other.TaskEndTime))
		{
			return false;
		}
		if (EndHero != other.EndHero)
		{
			return false;
		}
		if (EndHeroSFW != other.EndHeroSFW)
		{
			return false;
		}
		if (!scratchoffHeros_.Equals(other.scratchoffHeros_))
		{
			return false;
		}
		if (!scratchoffHerosSFW_.Equals(other.scratchoffHerosSFW_))
		{
			return false;
		}
		if (ScratchoffNPC != other.ScratchoffNPC)
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (!scratchoffPoolIds_.Equals(other.scratchoffPoolIds_))
		{
			return false;
		}
		if (!Rewards.Equals(other.Rewards))
		{
			return false;
		}
		if (ScratchVoice != other.ScratchVoice)
		{
			return false;
		}
		if (!Spends.Equals(other.Spends))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (Bg.Length != 0)
		{
			num ^= Bg.GetHashCode();
		}
		num ^= titleImages_.GetHashCode();
		num ^= mainHeros_.GetHashCode();
		num ^= mainHerosSFW_.GetHashCode();
		if (TaskNPC.Length != 0)
		{
			num ^= TaskNPC.GetHashCode();
		}
		if (taskBeginTime_ != null)
		{
			num ^= TaskBeginTime.GetHashCode();
		}
		if (taskEndTime_ != null)
		{
			num ^= TaskEndTime.GetHashCode();
		}
		if (EndHero.Length != 0)
		{
			num ^= EndHero.GetHashCode();
		}
		if (EndHeroSFW.Length != 0)
		{
			num ^= EndHeroSFW.GetHashCode();
		}
		num ^= scratchoffHeros_.GetHashCode();
		num ^= scratchoffHerosSFW_.GetHashCode();
		if (ScratchoffNPC.Length != 0)
		{
			num ^= ScratchoffNPC.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		num ^= scratchoffPoolIds_.GetHashCode();
		num ^= Rewards.GetHashCode();
		if (ScratchVoice != 0)
		{
			num ^= ScratchVoice.GetHashCode();
		}
		num ^= Spends.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (Bg.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Bg);
		}
		titleImages_.WriteTo(ref output, _repeated_titleImages_codec);
		mainHeros_.WriteTo(ref output, _repeated_mainHeros_codec);
		mainHerosSFW_.WriteTo(ref output, _repeated_mainHerosSFW_codec);
		if (TaskNPC.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(TaskNPC);
		}
		if (taskBeginTime_ != null)
		{
			output.WriteRawTag(58);
			output.WriteMessage(TaskBeginTime);
		}
		if (taskEndTime_ != null)
		{
			output.WriteRawTag(66);
			output.WriteMessage(TaskEndTime);
		}
		if (EndHero.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(EndHero);
		}
		if (EndHeroSFW.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(EndHeroSFW);
		}
		scratchoffHeros_.WriteTo(ref output, _repeated_scratchoffHeros_codec);
		scratchoffHerosSFW_.WriteTo(ref output, _repeated_scratchoffHerosSFW_codec);
		if (ScratchoffNPC.Length != 0)
		{
			output.WriteRawTag(106);
			output.WriteString(ScratchoffNPC);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(114);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(122);
			output.WriteMessage(EndTime);
		}
		scratchoffPoolIds_.WriteTo(ref output, _repeated_scratchoffPoolIds_codec);
		rewards_.WriteTo(ref output, _map_rewards_codec);
		if (ScratchVoice != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(ScratchVoice);
		}
		spends_.WriteTo(ref output, _map_spends_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (Bg.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Bg);
		}
		num += titleImages_.CalculateSize(_repeated_titleImages_codec);
		num += mainHeros_.CalculateSize(_repeated_mainHeros_codec);
		num += mainHerosSFW_.CalculateSize(_repeated_mainHerosSFW_codec);
		if (TaskNPC.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TaskNPC);
		}
		if (taskBeginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(TaskBeginTime);
		}
		if (taskEndTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(TaskEndTime);
		}
		if (EndHero.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(EndHero);
		}
		if (EndHeroSFW.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(EndHeroSFW);
		}
		num += scratchoffHeros_.CalculateSize(_repeated_scratchoffHeros_codec);
		num += scratchoffHerosSFW_.CalculateSize(_repeated_scratchoffHerosSFW_codec);
		if (ScratchoffNPC.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ScratchoffNPC);
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		num += scratchoffPoolIds_.CalculateSize(_repeated_scratchoffPoolIds_codec);
		num += rewards_.CalculateSize(_map_rewards_codec);
		if (ScratchVoice != 0)
		{
			num += 6;
		}
		num += spends_.CalculateSize(_map_spends_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityScratchoffConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.Bg.Length != 0)
		{
			Bg = other.Bg;
		}
		titleImages_.Add(other.titleImages_);
		mainHeros_.Add(other.mainHeros_);
		mainHerosSFW_.Add(other.mainHerosSFW_);
		if (other.TaskNPC.Length != 0)
		{
			TaskNPC = other.TaskNPC;
		}
		if (other.taskBeginTime_ != null)
		{
			if (taskBeginTime_ == null)
			{
				TaskBeginTime = new Timestamp();
			}
			TaskBeginTime.MergeFrom(other.TaskBeginTime);
		}
		if (other.taskEndTime_ != null)
		{
			if (taskEndTime_ == null)
			{
				TaskEndTime = new Timestamp();
			}
			TaskEndTime.MergeFrom(other.TaskEndTime);
		}
		if (other.EndHero.Length != 0)
		{
			EndHero = other.EndHero;
		}
		if (other.EndHeroSFW.Length != 0)
		{
			EndHeroSFW = other.EndHeroSFW;
		}
		scratchoffHeros_.Add(other.scratchoffHeros_);
		scratchoffHerosSFW_.Add(other.scratchoffHerosSFW_);
		if (other.ScratchoffNPC.Length != 0)
		{
			ScratchoffNPC = other.ScratchoffNPC;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		scratchoffPoolIds_.Add(other.scratchoffPoolIds_);
		rewards_.MergeFrom(other.rewards_);
		if (other.ScratchVoice != 0)
		{
			ScratchVoice = other.ScratchVoice;
		}
		spends_.MergeFrom(other.spends_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				Id = input.ReadSFixed32();
				break;
			case 18u:
				Bg = input.ReadString();
				break;
			case 26u:
				titleImages_.AddEntriesFrom(ref input, _repeated_titleImages_codec);
				break;
			case 34u:
				mainHeros_.AddEntriesFrom(ref input, _repeated_mainHeros_codec);
				break;
			case 42u:
				mainHerosSFW_.AddEntriesFrom(ref input, _repeated_mainHerosSFW_codec);
				break;
			case 50u:
				TaskNPC = input.ReadString();
				break;
			case 58u:
				if (taskBeginTime_ == null)
				{
					TaskBeginTime = new Timestamp();
				}
				input.ReadMessage(TaskBeginTime);
				break;
			case 66u:
				if (taskEndTime_ == null)
				{
					TaskEndTime = new Timestamp();
				}
				input.ReadMessage(TaskEndTime);
				break;
			case 74u:
				EndHero = input.ReadString();
				break;
			case 82u:
				EndHeroSFW = input.ReadString();
				break;
			case 90u:
				scratchoffHeros_.AddEntriesFrom(ref input, _repeated_scratchoffHeros_codec);
				break;
			case 98u:
				scratchoffHerosSFW_.AddEntriesFrom(ref input, _repeated_scratchoffHerosSFW_codec);
				break;
			case 106u:
				ScratchoffNPC = input.ReadString();
				break;
			case 114u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 122u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 130u:
			case 133u:
				scratchoffPoolIds_.AddEntriesFrom(ref input, _repeated_scratchoffPoolIds_codec);
				break;
			case 138u:
				rewards_.AddEntriesFrom(ref input, _map_rewards_codec);
				break;
			case 149u:
				ScratchVoice = input.ReadSFixed32();
				break;
			case 154u:
				spends_.AddEntriesFrom(ref input, _map_spends_codec);
				break;
			}
		}
	}
}
