using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class MissionMod : IMessage<MissionMod>, IMessage, IEquatable<MissionMod>, IDeepCloneable<MissionMod>, IBufferMessage
{
	private static readonly MessageParser<MissionMod> _parser = new MessageParser<MissionMod>(() => new MissionMod());

	private UnknownFieldSet _unknownFields;

	public const int ActivityTasksFieldNumber = 1;

	private static readonly FieldCodec<TaskDSO> _repeated_activityTasks_codec = FieldCodec.ForMessage(10u, TaskDSO.Parser);

	private readonly RepeatedField<TaskDSO> activityTasks_ = new RepeatedField<TaskDSO>();

	public const int LaborActDiceInfoFieldNumber = 2;

	private LaborActDiceInfo laborActDiceInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MissionMod> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[103];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskDSO> ActivityTasks => activityTasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LaborActDiceInfo LaborActDiceInfo
	{
		get
		{
			return laborActDiceInfo_;
		}
		set
		{
			laborActDiceInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionMod()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionMod(MissionMod other)
		: this()
	{
		activityTasks_ = other.activityTasks_.Clone();
		laborActDiceInfo_ = ((other.laborActDiceInfo_ != null) ? other.laborActDiceInfo_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionMod Clone()
	{
		return new MissionMod(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MissionMod);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MissionMod other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!activityTasks_.Equals(other.activityTasks_))
		{
			return false;
		}
		if (!object.Equals(LaborActDiceInfo, other.LaborActDiceInfo))
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
		num ^= activityTasks_.GetHashCode();
		if (laborActDiceInfo_ != null)
		{
			num ^= LaborActDiceInfo.GetHashCode();
		}
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
		activityTasks_.WriteTo(ref output, _repeated_activityTasks_codec);
		if (laborActDiceInfo_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(LaborActDiceInfo);
		}
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
		num += activityTasks_.CalculateSize(_repeated_activityTasks_codec);
		if (laborActDiceInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LaborActDiceInfo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MissionMod other)
	{
		if (other == null)
		{
			return;
		}
		activityTasks_.Add(other.activityTasks_);
		if (other.laborActDiceInfo_ != null)
		{
			if (laborActDiceInfo_ == null)
			{
				LaborActDiceInfo = new LaborActDiceInfo();
			}
			LaborActDiceInfo.MergeFrom(other.LaborActDiceInfo);
		}
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
			case 10u:
				activityTasks_.AddEntriesFrom(ref input, _repeated_activityTasks_codec);
				break;
			case 18u:
				if (laborActDiceInfo_ == null)
				{
					LaborActDiceInfo = new LaborActDiceInfo();
				}
				input.ReadMessage(LaborActDiceInfo);
				break;
			}
		}
	}
}
