using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class AcquisitionConfigure : IMessage<AcquisitionConfigure>, IMessage, IEquatable<AcquisitionConfigure>, IDeepCloneable<AcquisitionConfigure>, IBufferMessage
{
	private static readonly MessageParser<AcquisitionConfigure> _parser = new MessageParser<AcquisitionConfigure>(() => new AcquisitionConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<AcquisitionInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, AcquisitionInfoConfigure.Parser);

	private readonly RepeatedField<AcquisitionInfoConfigure> infos_ = new RepeatedField<AcquisitionInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, AcquisitionInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, AcquisitionInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AcquisitionInfoConfigure.Parser), 18u);

	private readonly MapField<int, AcquisitionInfoConfigure> infoDict_ = new MapField<int, AcquisitionInfoConfigure>();

	public const int TasksFieldNumber = 3;

	private static readonly FieldCodec<AcquisitionTaskConfigure> _repeated_tasks_codec = FieldCodec.ForMessage(26u, AcquisitionTaskConfigure.Parser);

	private readonly RepeatedField<AcquisitionTaskConfigure> tasks_ = new RepeatedField<AcquisitionTaskConfigure>();

	public const int TaskDictFieldNumber = 4;

	private static readonly MapField<int, AcquisitionTaskConfigure>.Codec _map_taskDict_codec = new MapField<int, AcquisitionTaskConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AcquisitionTaskConfigure.Parser), 34u);

	private readonly MapField<int, AcquisitionTaskConfigure> taskDict_ = new MapField<int, AcquisitionTaskConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AcquisitionConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AcquisitionReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AcquisitionInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AcquisitionInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AcquisitionTaskConfigure> Tasks => tasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AcquisitionTaskConfigure> TaskDict => taskDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionConfigure(AcquisitionConfigure other)
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
	public AcquisitionConfigure Clone()
	{
		return new AcquisitionConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AcquisitionConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AcquisitionConfigure other)
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
	public void MergeFrom(AcquisitionConfigure other)
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
