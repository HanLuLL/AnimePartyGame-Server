using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class MapMissionTarget : IMessage<MapMissionTarget>, IMessage, IEquatable<MapMissionTarget>, IDeepCloneable<MapMissionTarget>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum Type
		{
			[OriginalName("none")]
			None,
			[OriginalName("kill_monster")]
			KillMonster,
			[OriginalName("collect_summon")]
			CollectSummon,
			[OriginalName("become_dragon")]
			BecomeDragon,
			[OriginalName("rising_star")]
			RisingStar,
			[OriginalName("boss_hp_changed")]
			BossHpChanged,
			[OriginalName("buff_progress")]
			BuffProgress
		}
	}

	private static readonly MessageParser<MapMissionTarget> _parser = new MessageParser<MapMissionTarget>(() => new MapMissionTarget());

	private UnknownFieldSet _unknownFields;

	public const int TargetTypeFieldNumber = 1;

	private Types.Type targetType_;

	public const int TargetIdFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_targetId_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> targetId_ = new RepeatedField<int>();

	public const int TargetNumFieldNumber = 3;

	private int targetNum_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapMissionTarget> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[67];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.Type TargetType
	{
		get
		{
			return targetType_;
		}
		set
		{
			targetType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TargetId => targetId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TargetNum
	{
		get
		{
			return targetNum_;
		}
		set
		{
			targetNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMissionTarget()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMissionTarget(MapMissionTarget other)
		: this()
	{
		targetType_ = other.targetType_;
		targetId_ = other.targetId_.Clone();
		targetNum_ = other.targetNum_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMissionTarget Clone()
	{
		return new MapMissionTarget(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapMissionTarget);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapMissionTarget other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TargetType != other.TargetType)
		{
			return false;
		}
		if (!targetId_.Equals(other.targetId_))
		{
			return false;
		}
		if (TargetNum != other.TargetNum)
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
		if (TargetType != Types.Type.None)
		{
			num ^= TargetType.GetHashCode();
		}
		num ^= targetId_.GetHashCode();
		if (TargetNum != 0)
		{
			num ^= TargetNum.GetHashCode();
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
		if (TargetType != Types.Type.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)TargetType);
		}
		targetId_.WriteTo(ref output, _repeated_targetId_codec);
		if (TargetNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(TargetNum);
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
		if (TargetType != Types.Type.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)TargetType);
		}
		num += targetId_.CalculateSize(_repeated_targetId_codec);
		if (TargetNum != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapMissionTarget other)
	{
		if (other != null)
		{
			if (other.TargetType != Types.Type.None)
			{
				TargetType = other.TargetType;
			}
			targetId_.Add(other.targetId_);
			if (other.TargetNum != 0)
			{
				TargetNum = other.TargetNum;
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
			case 8u:
				TargetType = (Types.Type)input.ReadEnum();
				break;
			case 18u:
			case 21u:
				targetId_.AddEntriesFrom(ref input, _repeated_targetId_codec);
				break;
			case 29u:
				TargetNum = input.ReadSFixed32();
				break;
			}
		}
	}
}
