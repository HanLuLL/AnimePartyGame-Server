using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PkAfterVoteS2C : IMessage<PkAfterVoteS2C>, IMessage, IEquatable<PkAfterVoteS2C>, IDeepCloneable<PkAfterVoteS2C>, IBufferMessage
{
	private static readonly MessageParser<PkAfterVoteS2C> _parser = new MessageParser<PkAfterVoteS2C>(() => new PkAfterVoteS2C());

	private UnknownFieldSet _unknownFields;

	public const int DicePointFieldNumber = 1;

	private static readonly MapField<long, int>.Codec _map_dicePoint_codec = new MapField<long, int>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<long, int> dicePoint_ = new MapField<long, int>();

	public const int CampIdFieldNumber = 2;

	private int campId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PkAfterVoteS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[265];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, int> DicePoint => dicePoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CampId
	{
		get
		{
			return campId_;
		}
		set
		{
			campId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PkAfterVoteS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PkAfterVoteS2C(PkAfterVoteS2C other)
		: this()
	{
		dicePoint_ = other.dicePoint_.Clone();
		campId_ = other.campId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PkAfterVoteS2C Clone()
	{
		return new PkAfterVoteS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PkAfterVoteS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PkAfterVoteS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!DicePoint.Equals(other.DicePoint))
		{
			return false;
		}
		if (CampId != other.CampId)
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
		num ^= DicePoint.GetHashCode();
		if (CampId != 0)
		{
			num ^= CampId.GetHashCode();
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
		dicePoint_.WriteTo(ref output, _map_dicePoint_codec);
		if (CampId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CampId);
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
		num += dicePoint_.CalculateSize(_map_dicePoint_codec);
		if (CampId != 0)
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
	public void MergeFrom(PkAfterVoteS2C other)
	{
		if (other != null)
		{
			dicePoint_.MergeFrom(other.dicePoint_);
			if (other.CampId != 0)
			{
				CampId = other.CampId;
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
			case 10u:
				dicePoint_.AddEntriesFrom(ref input, _map_dicePoint_codec);
				break;
			case 21u:
				CampId = input.ReadSFixed32();
				break;
			}
		}
	}
}
