using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Clue : IMessage<Clue>, IMessage, IEquatable<Clue>, IDeepCloneable<Clue>, IBufferMessage
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
			Complete
		}
	}

	private static readonly MessageParser<Clue> _parser = new MessageParser<Clue>(() => new Clue());

	private UnknownFieldSet _unknownFields;

	public const int ClueIdFieldNumber = 1;

	private int clueId_;

	public const int TargetsFieldNumber = 2;

	private static readonly FieldCodec<MapMissionTarget> _repeated_targets_codec = FieldCodec.ForMessage(18u, MapMissionTarget.Parser);

	private readonly RepeatedField<MapMissionTarget> targets_ = new RepeatedField<MapMissionTarget>();

	public const int StateFieldNumber = 3;

	private Types.State state_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Clue> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[71];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ClueId
	{
		get
		{
			return clueId_;
		}
		set
		{
			clueId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapMissionTarget> Targets => targets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.State State
	{
		get
		{
			return state_;
		}
		set
		{
			state_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Clue()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Clue(Clue other)
		: this()
	{
		clueId_ = other.clueId_;
		targets_ = other.targets_.Clone();
		state_ = other.state_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Clue Clone()
	{
		return new Clue(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Clue);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Clue other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ClueId != other.ClueId)
		{
			return false;
		}
		if (!targets_.Equals(other.targets_))
		{
			return false;
		}
		if (State != other.State)
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
		if (ClueId != 0)
		{
			num ^= ClueId.GetHashCode();
		}
		num ^= targets_.GetHashCode();
		if (State != Types.State.None)
		{
			num ^= State.GetHashCode();
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
		if (ClueId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ClueId);
		}
		targets_.WriteTo(ref output, _repeated_targets_codec);
		if (State != Types.State.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)State);
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
		if (ClueId != 0)
		{
			num += 5;
		}
		num += targets_.CalculateSize(_repeated_targets_codec);
		if (State != Types.State.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)State);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(Clue other)
	{
		if (other != null)
		{
			if (other.ClueId != 0)
			{
				ClueId = other.ClueId;
			}
			targets_.Add(other.targets_);
			if (other.State != Types.State.None)
			{
				State = other.State;
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
				ClueId = input.ReadSFixed32();
				break;
			case 18u:
				targets_.AddEntriesFrom(ref input, _repeated_targets_codec);
				break;
			case 24u:
				State = (Types.State)input.ReadEnum();
				break;
			}
		}
	}
}
