using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class BattlePassTaskInfoS2C : IMessage<BattlePassTaskInfoS2C>, IMessage, IEquatable<BattlePassTaskInfoS2C>, IDeepCloneable<BattlePassTaskInfoS2C>, IBufferMessage
{
	private static readonly MessageParser<BattlePassTaskInfoS2C> _parser = new MessageParser<BattlePassTaskInfoS2C>(() => new BattlePassTaskInfoS2C());

	private UnknownFieldSet _unknownFields;

	public const int TaskFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_task_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<int, int> task_ = new MapField<int, int>();

	public const int TaskRewardIsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_taskRewardIs_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> taskRewardIs_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassTaskInfoS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[146];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Task => task_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TaskRewardIs => taskRewardIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskInfoS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskInfoS2C(BattlePassTaskInfoS2C other)
		: this()
	{
		task_ = other.task_.Clone();
		taskRewardIs_ = other.taskRewardIs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskInfoS2C Clone()
	{
		return new BattlePassTaskInfoS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassTaskInfoS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassTaskInfoS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Task.Equals(other.Task))
		{
			return false;
		}
		if (!taskRewardIs_.Equals(other.taskRewardIs_))
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
		num ^= Task.GetHashCode();
		num ^= taskRewardIs_.GetHashCode();
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
		task_.WriteTo(ref output, _map_task_codec);
		taskRewardIs_.WriteTo(ref output, _repeated_taskRewardIs_codec);
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
		num += task_.CalculateSize(_map_task_codec);
		num += taskRewardIs_.CalculateSize(_repeated_taskRewardIs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassTaskInfoS2C other)
	{
		if (other != null)
		{
			task_.MergeFrom(other.task_);
			taskRewardIs_.Add(other.taskRewardIs_);
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
				task_.AddEntriesFrom(ref input, _map_task_codec);
				break;
			case 18u:
			case 21u:
				taskRewardIs_.AddEntriesFrom(ref input, _repeated_taskRewardIs_codec);
				break;
			}
		}
	}
}
