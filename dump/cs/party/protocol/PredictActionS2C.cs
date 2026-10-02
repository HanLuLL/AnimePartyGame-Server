using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class PredictActionS2C : IMessage<PredictActionS2C>, IMessage, IEquatable<PredictActionS2C>, IDeepCloneable<PredictActionS2C>, IBufferMessage
{
	private static readonly MessageParser<PredictActionS2C> _parser = new MessageParser<PredictActionS2C>(() => new PredictActionS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoomRoundFieldNumber = 1;

	private int roomRound_;

	public const int ActionsFieldNumber = 2;

	private static readonly FieldCodec<party.model.Action> _repeated_actions_codec = FieldCodec.ForMessage(18u, party.model.Action.Parser);

	private readonly RepeatedField<party.model.Action> actions_ = new RepeatedField<party.model.Action>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PredictActionS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[379];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomRound
	{
		get
		{
			return roomRound_;
		}
		set
		{
			roomRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<party.model.Action> Actions => actions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PredictActionS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PredictActionS2C(PredictActionS2C other)
		: this()
	{
		roomRound_ = other.roomRound_;
		actions_ = other.actions_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PredictActionS2C Clone()
	{
		return new PredictActionS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PredictActionS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PredictActionS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RoomRound != other.RoomRound)
		{
			return false;
		}
		if (!actions_.Equals(other.actions_))
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
		if (RoomRound != 0)
		{
			num ^= RoomRound.GetHashCode();
		}
		num ^= actions_.GetHashCode();
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
		if (RoomRound != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(RoomRound);
		}
		actions_.WriteTo(ref output, _repeated_actions_codec);
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
		if (RoomRound != 0)
		{
			num += 5;
		}
		num += actions_.CalculateSize(_repeated_actions_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PredictActionS2C other)
	{
		if (other != null)
		{
			if (other.RoomRound != 0)
			{
				RoomRound = other.RoomRound;
			}
			actions_.Add(other.actions_);
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
				RoomRound = input.ReadSFixed32();
				break;
			case 18u:
				actions_.AddEntriesFrom(ref input, _repeated_actions_codec);
				break;
			}
		}
	}
}
