using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class LuckyStarMission : IMessage<LuckyStarMission>, IMessage, IEquatable<LuckyStarMission>, IDeepCloneable<LuckyStarMission>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum State
		{
			[OriginalName("none")]
			None,
			[OriginalName("accept")]
			Accept,
			[OriginalName("complete")]
			Complete,
			[OriginalName("fail")]
			Fail
		}
	}

	private static readonly MessageParser<LuckyStarMission> _parser = new MessageParser<LuckyStarMission>(() => new LuckyStarMission());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int TargetsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_targets_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> targets_ = new RepeatedField<int>();

	public const int LuckyStarMissionStateFieldNumber = 3;

	private Types.State luckyStarMissionState_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LuckyStarMission> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[75];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefId
	{
		get
		{
			return defId_;
		}
		set
		{
			defId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Targets => targets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.State LuckyStarMissionState
	{
		get
		{
			return luckyStarMissionState_;
		}
		set
		{
			luckyStarMissionState_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMission()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMission(LuckyStarMission other)
		: this()
	{
		defId_ = other.defId_;
		targets_ = other.targets_.Clone();
		luckyStarMissionState_ = other.luckyStarMissionState_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMission Clone()
	{
		return new LuckyStarMission(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LuckyStarMission);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LuckyStarMission other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DefId != other.DefId)
		{
			return false;
		}
		if (!targets_.Equals(other.targets_))
		{
			return false;
		}
		if (LuckyStarMissionState != other.LuckyStarMissionState)
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
		if (DefId != 0)
		{
			num ^= DefId.GetHashCode();
		}
		num ^= targets_.GetHashCode();
		if (LuckyStarMissionState != Types.State.None)
		{
			num ^= LuckyStarMissionState.GetHashCode();
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
		if (DefId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DefId);
		}
		targets_.WriteTo(ref output, _repeated_targets_codec);
		if (LuckyStarMissionState != Types.State.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)LuckyStarMissionState);
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
		if (DefId != 0)
		{
			num += 5;
		}
		num += targets_.CalculateSize(_repeated_targets_codec);
		if (LuckyStarMissionState != Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)LuckyStarMissionState);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LuckyStarMission other)
	{
		if (other != null)
		{
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			targets_.Add(other.targets_);
			if (other.LuckyStarMissionState != Types.State.None)
			{
				LuckyStarMissionState = other.LuckyStarMissionState;
			}
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
			case 13u:
				DefId = input.ReadSFixed32();
				break;
			case 18u:
			case 21u:
				targets_.AddEntriesFrom(ref input, _repeated_targets_codec);
				break;
			case 24u:
				LuckyStarMissionState = (Types.State)input.ReadEnum();
				break;
			}
		}
	}
}
