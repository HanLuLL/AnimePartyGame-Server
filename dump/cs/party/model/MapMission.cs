using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class MapMission : IMessage<MapMission>, IMessage, IEquatable<MapMission>, IDeepCloneable<MapMission>, IBufferMessage
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
			[OriginalName("remove")]
			Remove,
			[OriginalName("failed")]
			Failed
		}
	}

	private static readonly MessageParser<MapMission> _parser = new MessageParser<MapMission>(() => new MapMission());

	private UnknownFieldSet _unknownFields;

	public const int MissionIdFieldNumber = 1;

	private int missionId_;

	public const int TargetsFieldNumber = 2;

	private static readonly FieldCodec<MapMissionTarget> _repeated_targets_codec = FieldCodec.ForMessage(18u, MapMissionTarget.Parser);

	private readonly RepeatedField<MapMissionTarget> targets_ = new RepeatedField<MapMissionTarget>();

	public const int MissionStateFieldNumber = 3;

	private Types.State missionState_;

	public const int ExtraParamFieldNumber = 4;

	private long extraParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapMission> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[68];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MissionId
	{
		get
		{
			return missionId_;
		}
		set
		{
			missionId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapMissionTarget> Targets => targets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.State MissionState
	{
		get
		{
			return missionState_;
		}
		set
		{
			missionState_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ExtraParam
	{
		get
		{
			return extraParam_;
		}
		set
		{
			extraParam_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMission()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMission(MapMission other)
		: this()
	{
		missionId_ = other.missionId_;
		targets_ = other.targets_.Clone();
		missionState_ = other.missionState_;
		extraParam_ = other.extraParam_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMission Clone()
	{
		return new MapMission(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapMission);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapMission other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MissionId != other.MissionId)
		{
			return false;
		}
		if (!targets_.Equals(other.targets_))
		{
			return false;
		}
		if (MissionState != other.MissionState)
		{
			return false;
		}
		if (ExtraParam != other.ExtraParam)
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
		if (MissionId != 0)
		{
			num ^= MissionId.GetHashCode();
		}
		num ^= targets_.GetHashCode();
		if (MissionState != Types.State.None)
		{
			num ^= MissionState.GetHashCode();
		}
		if (ExtraParam != 0L)
		{
			num ^= ExtraParam.GetHashCode();
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
		if (MissionId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MissionId);
		}
		targets_.WriteTo(ref output, _repeated_targets_codec);
		if (MissionState != Types.State.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)MissionState);
		}
		if (ExtraParam != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(ExtraParam);
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
		if (MissionId != 0)
		{
			num += 5;
		}
		num += targets_.CalculateSize(_repeated_targets_codec);
		if (MissionState != Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MissionState);
		}
		if (ExtraParam != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapMission other)
	{
		if (other != null)
		{
			if (other.MissionId != 0)
			{
				MissionId = other.MissionId;
			}
			targets_.Add(other.targets_);
			if (other.MissionState != Types.State.None)
			{
				MissionState = other.MissionState;
			}
			if (other.ExtraParam != 0L)
			{
				ExtraParam = other.ExtraParam;
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
				MissionId = input.ReadSFixed32();
				break;
			case 18u:
				targets_.AddEntriesFrom(ref input, _repeated_targets_codec);
				break;
			case 24u:
				MissionState = (Types.State)input.ReadEnum();
				break;
			case 33u:
				ExtraParam = input.ReadSFixed64();
				break;
			}
		}
	}
}
