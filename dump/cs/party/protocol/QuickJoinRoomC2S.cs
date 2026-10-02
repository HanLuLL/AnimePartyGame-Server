using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class QuickJoinRoomC2S : IMessage<QuickJoinRoomC2S>, IMessage, IEquatable<QuickJoinRoomC2S>, IDeepCloneable<QuickJoinRoomC2S>, IBufferMessage
{
	private static readonly MessageParser<QuickJoinRoomC2S> _parser = new MessageParser<QuickJoinRoomC2S>(() => new QuickJoinRoomC2S());

	private UnknownFieldSet _unknownFields;

	public const int MapModFieldNumber = 1;

	private int mapMod_;

	public const int DifficultyFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_difficulty_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> difficulty_ = new RepeatedField<int>();

	public const int SportPassMapIdFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_sportPassMapId_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> sportPassMapId_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<QuickJoinRoomC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[43];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapMod
	{
		get
		{
			return mapMod_;
		}
		set
		{
			mapMod_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Difficulty => difficulty_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SportPassMapId => sportPassMapId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickJoinRoomC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickJoinRoomC2S(QuickJoinRoomC2S other)
		: this()
	{
		mapMod_ = other.mapMod_;
		difficulty_ = other.difficulty_.Clone();
		sportPassMapId_ = other.sportPassMapId_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickJoinRoomC2S Clone()
	{
		return new QuickJoinRoomC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as QuickJoinRoomC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(QuickJoinRoomC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapMod != other.MapMod)
		{
			return false;
		}
		if (!difficulty_.Equals(other.difficulty_))
		{
			return false;
		}
		if (!sportPassMapId_.Equals(other.sportPassMapId_))
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
		if (MapMod != 0)
		{
			num ^= MapMod.GetHashCode();
		}
		num ^= difficulty_.GetHashCode();
		num ^= sportPassMapId_.GetHashCode();
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
		if (MapMod != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MapMod);
		}
		difficulty_.WriteTo(ref output, _repeated_difficulty_codec);
		sportPassMapId_.WriteTo(ref output, _repeated_sportPassMapId_codec);
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
		if (MapMod != 0)
		{
			num += 5;
		}
		num += difficulty_.CalculateSize(_repeated_difficulty_codec);
		num += sportPassMapId_.CalculateSize(_repeated_sportPassMapId_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(QuickJoinRoomC2S other)
	{
		if (other != null)
		{
			if (other.MapMod != 0)
			{
				MapMod = other.MapMod;
			}
			difficulty_.Add(other.difficulty_);
			sportPassMapId_.Add(other.sportPassMapId_);
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
				MapMod = input.ReadSFixed32();
				break;
			case 18u:
			case 21u:
				difficulty_.AddEntriesFrom(ref input, _repeated_difficulty_codec);
				break;
			case 26u:
			case 29u:
				sportPassMapId_.AddEntriesFrom(ref input, _repeated_sportPassMapId_codec);
				break;
			}
		}
	}
}
