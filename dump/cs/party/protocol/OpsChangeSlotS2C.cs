using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class OpsChangeSlotS2C : IMessage<OpsChangeSlotS2C>, IMessage, IEquatable<OpsChangeSlotS2C>, IDeepCloneable<OpsChangeSlotS2C>, IBufferMessage
{
	private static readonly MessageParser<OpsChangeSlotS2C> _parser = new MessageParser<OpsChangeSlotS2C>(() => new OpsChangeSlotS2C());

	private UnknownFieldSet _unknownFields;

	public const int IsAgreeFieldNumber = 1;

	private bool isAgree_;

	public const int ApplyIdFieldNumber = 2;

	private long applyId_;

	public const int OpsPlayerIdFieldNumber = 3;

	private long opsPlayerId_;

	public const int FinalSlotFieldNumber = 4;

	private static readonly MapField<long, int>.Codec _map_finalSlot_codec = new MapField<long, int>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<long, int> finalSlot_ = new MapField<long, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<OpsChangeSlotS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[528];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAgree
	{
		get
		{
			return isAgree_;
		}
		set
		{
			isAgree_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ApplyId
	{
		get
		{
			return applyId_;
		}
		set
		{
			applyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OpsPlayerId
	{
		get
		{
			return opsPlayerId_;
		}
		set
		{
			opsPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, int> FinalSlot => finalSlot_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotS2C(OpsChangeSlotS2C other)
		: this()
	{
		isAgree_ = other.isAgree_;
		applyId_ = other.applyId_;
		opsPlayerId_ = other.opsPlayerId_;
		finalSlot_ = other.finalSlot_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotS2C Clone()
	{
		return new OpsChangeSlotS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as OpsChangeSlotS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(OpsChangeSlotS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsAgree != other.IsAgree)
		{
			return false;
		}
		if (ApplyId != other.ApplyId)
		{
			return false;
		}
		if (OpsPlayerId != other.OpsPlayerId)
		{
			return false;
		}
		if (!FinalSlot.Equals(other.FinalSlot))
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
		if (IsAgree)
		{
			num ^= IsAgree.GetHashCode();
		}
		if (ApplyId != 0L)
		{
			num ^= ApplyId.GetHashCode();
		}
		if (OpsPlayerId != 0L)
		{
			num ^= OpsPlayerId.GetHashCode();
		}
		num ^= FinalSlot.GetHashCode();
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
		if (IsAgree)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsAgree);
		}
		if (ApplyId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(ApplyId);
		}
		if (OpsPlayerId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(OpsPlayerId);
		}
		finalSlot_.WriteTo(ref output, _map_finalSlot_codec);
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
		if (IsAgree)
		{
			num += 2;
		}
		if (ApplyId != 0L)
		{
			num += 9;
		}
		if (OpsPlayerId != 0L)
		{
			num += 9;
		}
		num += finalSlot_.CalculateSize(_map_finalSlot_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(OpsChangeSlotS2C other)
	{
		if (other != null)
		{
			if (other.IsAgree)
			{
				IsAgree = other.IsAgree;
			}
			if (other.ApplyId != 0L)
			{
				ApplyId = other.ApplyId;
			}
			if (other.OpsPlayerId != 0L)
			{
				OpsPlayerId = other.OpsPlayerId;
			}
			finalSlot_.MergeFrom(other.finalSlot_);
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
				IsAgree = input.ReadBool();
				break;
			case 17u:
				ApplyId = input.ReadSFixed64();
				break;
			case 25u:
				OpsPlayerId = input.ReadSFixed64();
				break;
			case 34u:
				finalSlot_.AddEntriesFrom(ref input, _map_finalSlot_codec);
				break;
			}
		}
	}
}
