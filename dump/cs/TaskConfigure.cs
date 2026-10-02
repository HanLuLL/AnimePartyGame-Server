using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class TaskConfigure : IMessage<TaskConfigure>, IMessage, IEquatable<TaskConfigure>, IDeepCloneable<TaskConfigure>, IBufferMessage
{
	private static readonly MessageParser<TaskConfigure> _parser = new MessageParser<TaskConfigure>(() => new TaskConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BeginnersFieldNumber = 1;

	private static readonly FieldCodec<TaskBeginnerConfigure> _repeated_beginners_codec = FieldCodec.ForMessage(10u, TaskBeginnerConfigure.Parser);

	private readonly RepeatedField<TaskBeginnerConfigure> beginners_ = new RepeatedField<TaskBeginnerConfigure>();

	public const int BeginnerDictFieldNumber = 2;

	private static readonly MapField<int, TaskBeginnerConfigure>.Codec _map_beginnerDict_codec = new MapField<int, TaskBeginnerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TaskBeginnerConfigure.Parser), 18u);

	private readonly MapField<int, TaskBeginnerConfigure> beginnerDict_ = new MapField<int, TaskBeginnerConfigure>();

	public const int WeeklysFieldNumber = 3;

	private static readonly FieldCodec<TaskWeeklyConfigure> _repeated_weeklys_codec = FieldCodec.ForMessage(26u, TaskWeeklyConfigure.Parser);

	private readonly RepeatedField<TaskWeeklyConfigure> weeklys_ = new RepeatedField<TaskWeeklyConfigure>();

	public const int WeeklyDictFieldNumber = 4;

	private static readonly MapField<int, TaskWeeklyConfigure>.Codec _map_weeklyDict_codec = new MapField<int, TaskWeeklyConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TaskWeeklyConfigure.Parser), 34u);

	private readonly MapField<int, TaskWeeklyConfigure> weeklyDict_ = new MapField<int, TaskWeeklyConfigure>();

	public const int WeeklyProgresssFieldNumber = 5;

	private static readonly FieldCodec<TaskWeeklyProgressConfigure> _repeated_weeklyProgresss_codec = FieldCodec.ForMessage(42u, TaskWeeklyProgressConfigure.Parser);

	private readonly RepeatedField<TaskWeeklyProgressConfigure> weeklyProgresss_ = new RepeatedField<TaskWeeklyProgressConfigure>();

	public const int WeeklyProgressDictFieldNumber = 6;

	private static readonly MapField<int, TaskWeeklyProgressConfigure>.Codec _map_weeklyProgressDict_codec = new MapField<int, TaskWeeklyProgressConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TaskWeeklyProgressConfigure.Parser), 50u);

	private readonly MapField<int, TaskWeeklyProgressConfigure> weeklyProgressDict_ = new MapField<int, TaskWeeklyProgressConfigure>();

	public const int Day7SFieldNumber = 7;

	private static readonly FieldCodec<TaskDay7Configure> _repeated_day7S_codec = FieldCodec.ForMessage(58u, TaskDay7Configure.Parser);

	private readonly RepeatedField<TaskDay7Configure> day7S_ = new RepeatedField<TaskDay7Configure>();

	public const int Day7DictFieldNumber = 8;

	private static readonly MapField<int, TaskDay7Configure>.Codec _map_day7Dict_codec = new MapField<int, TaskDay7Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TaskDay7Configure.Parser), 66u);

	private readonly MapField<int, TaskDay7Configure> day7Dict_ = new MapField<int, TaskDay7Configure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TaskConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TaskReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskBeginnerConfigure> Beginners => beginners_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TaskBeginnerConfigure> BeginnerDict => beginnerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskWeeklyConfigure> Weeklys => weeklys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TaskWeeklyConfigure> WeeklyDict => weeklyDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskWeeklyProgressConfigure> WeeklyProgresss => weeklyProgresss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TaskWeeklyProgressConfigure> WeeklyProgressDict => weeklyProgressDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskDay7Configure> Day7S => day7S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TaskDay7Configure> Day7Dict => day7Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskConfigure(TaskConfigure other)
		: this()
	{
		beginners_ = other.beginners_.Clone();
		beginnerDict_ = other.beginnerDict_.Clone();
		weeklys_ = other.weeklys_.Clone();
		weeklyDict_ = other.weeklyDict_.Clone();
		weeklyProgresss_ = other.weeklyProgresss_.Clone();
		weeklyProgressDict_ = other.weeklyProgressDict_.Clone();
		day7S_ = other.day7S_.Clone();
		day7Dict_ = other.day7Dict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskConfigure Clone()
	{
		return new TaskConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TaskConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TaskConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!beginners_.Equals(other.beginners_))
		{
			return false;
		}
		if (!BeginnerDict.Equals(other.BeginnerDict))
		{
			return false;
		}
		if (!weeklys_.Equals(other.weeklys_))
		{
			return false;
		}
		if (!WeeklyDict.Equals(other.WeeklyDict))
		{
			return false;
		}
		if (!weeklyProgresss_.Equals(other.weeklyProgresss_))
		{
			return false;
		}
		if (!WeeklyProgressDict.Equals(other.WeeklyProgressDict))
		{
			return false;
		}
		if (!day7S_.Equals(other.day7S_))
		{
			return false;
		}
		if (!Day7Dict.Equals(other.Day7Dict))
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
		num ^= beginners_.GetHashCode();
		num ^= BeginnerDict.GetHashCode();
		num ^= weeklys_.GetHashCode();
		num ^= WeeklyDict.GetHashCode();
		num ^= weeklyProgresss_.GetHashCode();
		num ^= WeeklyProgressDict.GetHashCode();
		num ^= day7S_.GetHashCode();
		num ^= Day7Dict.GetHashCode();
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
		beginners_.WriteTo(ref output, _repeated_beginners_codec);
		beginnerDict_.WriteTo(ref output, _map_beginnerDict_codec);
		weeklys_.WriteTo(ref output, _repeated_weeklys_codec);
		weeklyDict_.WriteTo(ref output, _map_weeklyDict_codec);
		weeklyProgresss_.WriteTo(ref output, _repeated_weeklyProgresss_codec);
		weeklyProgressDict_.WriteTo(ref output, _map_weeklyProgressDict_codec);
		day7S_.WriteTo(ref output, _repeated_day7S_codec);
		day7Dict_.WriteTo(ref output, _map_day7Dict_codec);
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
		num += beginners_.CalculateSize(_repeated_beginners_codec);
		num += beginnerDict_.CalculateSize(_map_beginnerDict_codec);
		num += weeklys_.CalculateSize(_repeated_weeklys_codec);
		num += weeklyDict_.CalculateSize(_map_weeklyDict_codec);
		num += weeklyProgresss_.CalculateSize(_repeated_weeklyProgresss_codec);
		num += weeklyProgressDict_.CalculateSize(_map_weeklyProgressDict_codec);
		num += day7S_.CalculateSize(_repeated_day7S_codec);
		num += day7Dict_.CalculateSize(_map_day7Dict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TaskConfigure other)
	{
		if (other != null)
		{
			beginners_.Add(other.beginners_);
			beginnerDict_.MergeFrom(other.beginnerDict_);
			weeklys_.Add(other.weeklys_);
			weeklyDict_.MergeFrom(other.weeklyDict_);
			weeklyProgresss_.Add(other.weeklyProgresss_);
			weeklyProgressDict_.MergeFrom(other.weeklyProgressDict_);
			day7S_.Add(other.day7S_);
			day7Dict_.MergeFrom(other.day7Dict_);
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
				beginners_.AddEntriesFrom(ref input, _repeated_beginners_codec);
				break;
			case 18u:
				beginnerDict_.AddEntriesFrom(ref input, _map_beginnerDict_codec);
				break;
			case 26u:
				weeklys_.AddEntriesFrom(ref input, _repeated_weeklys_codec);
				break;
			case 34u:
				weeklyDict_.AddEntriesFrom(ref input, _map_weeklyDict_codec);
				break;
			case 42u:
				weeklyProgresss_.AddEntriesFrom(ref input, _repeated_weeklyProgresss_codec);
				break;
			case 50u:
				weeklyProgressDict_.AddEntriesFrom(ref input, _map_weeklyProgressDict_codec);
				break;
			case 58u:
				day7S_.AddEntriesFrom(ref input, _repeated_day7S_codec);
				break;
			case 66u:
				day7Dict_.AddEntriesFrom(ref input, _map_day7Dict_codec);
				break;
			}
		}
	}
}
