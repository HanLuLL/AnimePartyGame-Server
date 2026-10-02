using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class VictoryCondition : IMessage<VictoryCondition>, IMessage, IEquatable<VictoryCondition>, IDeepCloneable<VictoryCondition>, IBufferMessage
{
	private static readonly MessageParser<VictoryCondition> _parser = new MessageParser<VictoryCondition>(() => new VictoryCondition());

	private UnknownFieldSet _unknownFields;

	public const int VictoryTypeFieldNumber = 1;

	private int victoryType_;

	public const int VictoryParamsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_victoryParams_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> victoryParams_ = new RepeatedField<int>();

	public const int KillRecordFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_killRecord_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> killRecord_ = new MapField<int, int>();

	public const int StarFieldNumber = 4;

	private int star_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<VictoryCondition> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[77];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int VictoryType
	{
		get
		{
			return victoryType_;
		}
		set
		{
			victoryType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> VictoryParams => victoryParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> KillRecord => killRecord_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Star
	{
		get
		{
			return star_;
		}
		set
		{
			star_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VictoryCondition()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VictoryCondition(VictoryCondition other)
		: this()
	{
		victoryType_ = other.victoryType_;
		victoryParams_ = other.victoryParams_.Clone();
		killRecord_ = other.killRecord_.Clone();
		star_ = other.star_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VictoryCondition Clone()
	{
		return new VictoryCondition(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as VictoryCondition);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(VictoryCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (VictoryType != other.VictoryType)
		{
			return false;
		}
		if (!victoryParams_.Equals(other.victoryParams_))
		{
			return false;
		}
		if (!KillRecord.Equals(other.KillRecord))
		{
			return false;
		}
		if (Star != other.Star)
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
		if (VictoryType != 0)
		{
			num ^= VictoryType.GetHashCode();
		}
		num ^= victoryParams_.GetHashCode();
		num ^= KillRecord.GetHashCode();
		if (Star != 0)
		{
			num ^= Star.GetHashCode();
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
		if (VictoryType != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(VictoryType);
		}
		victoryParams_.WriteTo(ref output, _repeated_victoryParams_codec);
		killRecord_.WriteTo(ref output, _map_killRecord_codec);
		if (Star != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Star);
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
		if (VictoryType != 0)
		{
			num += 5;
		}
		num += victoryParams_.CalculateSize(_repeated_victoryParams_codec);
		num += killRecord_.CalculateSize(_map_killRecord_codec);
		if (Star != 0)
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
	public void MergeFrom(VictoryCondition other)
	{
		if (other != null)
		{
			if (other.VictoryType != 0)
			{
				VictoryType = other.VictoryType;
			}
			victoryParams_.Add(other.victoryParams_);
			killRecord_.MergeFrom(other.killRecord_);
			if (other.Star != 0)
			{
				Star = other.Star;
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
				VictoryType = input.ReadSFixed32();
				break;
			case 18u:
			case 21u:
				victoryParams_.AddEntriesFrom(ref input, _repeated_victoryParams_codec);
				break;
			case 26u:
				killRecord_.AddEntriesFrom(ref input, _map_killRecord_codec);
				break;
			case 37u:
				Star = input.ReadSFixed32();
				break;
			}
		}
	}
}
