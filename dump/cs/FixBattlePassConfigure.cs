using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixBattlePassConfigure : IMessage<FixBattlePassConfigure>, IMessage, IEquatable<FixBattlePassConfigure>, IDeepCloneable<FixBattlePassConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixBattlePassConfigure> _parser = new MessageParser<FixBattlePassConfigure>(() => new FixBattlePassConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<FixBattlePassInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, FixBattlePassInfoConfigure.Parser);

	private readonly RepeatedField<FixBattlePassInfoConfigure> infos_ = new RepeatedField<FixBattlePassInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, FixBattlePassInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, FixBattlePassInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixBattlePassInfoConfigure.Parser), 18u);

	private readonly MapField<int, FixBattlePassInfoConfigure> infoDict_ = new MapField<int, FixBattlePassInfoConfigure>();

	public const int TasksFieldNumber = 3;

	private static readonly FieldCodec<FixBattlePassTaskConfigure> _repeated_tasks_codec = FieldCodec.ForMessage(26u, FixBattlePassTaskConfigure.Parser);

	private readonly RepeatedField<FixBattlePassTaskConfigure> tasks_ = new RepeatedField<FixBattlePassTaskConfigure>();

	public const int TaskDictFieldNumber = 4;

	private static readonly MapField<int, FixBattlePassTaskConfigure>.Codec _map_taskDict_codec = new MapField<int, FixBattlePassTaskConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixBattlePassTaskConfigure.Parser), 34u);

	private readonly MapField<int, FixBattlePassTaskConfigure> taskDict_ = new MapField<int, FixBattlePassTaskConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixBattlePassConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixBattlePassReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixBattlePassInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixBattlePassInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixBattlePassTaskConfigure> Tasks => tasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixBattlePassTaskConfigure> TaskDict => taskDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixBattlePassConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixBattlePassConfigure(FixBattlePassConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		tasks_ = other.tasks_.Clone();
		taskDict_ = other.taskDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixBattlePassConfigure Clone()
	{
		return new FixBattlePassConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixBattlePassConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixBattlePassConfigure other)
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
		if (!tasks_.Equals(other.tasks_))
		{
			return false;
		}
		if (!TaskDict.Equals(other.TaskDict))
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
		num ^= tasks_.GetHashCode();
		num ^= TaskDict.GetHashCode();
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
		tasks_.WriteTo(ref output, _repeated_tasks_codec);
		taskDict_.WriteTo(ref output, _map_taskDict_codec);
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
		num += tasks_.CalculateSize(_repeated_tasks_codec);
		num += taskDict_.CalculateSize(_map_taskDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixBattlePassConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			tasks_.Add(other.tasks_);
			taskDict_.MergeFrom(other.taskDict_);
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
				tasks_.AddEntriesFrom(ref input, _repeated_tasks_codec);
				break;
			case 34u:
				taskDict_.AddEntriesFrom(ref input, _map_taskDict_codec);
				break;
			}
		}
	}
}
