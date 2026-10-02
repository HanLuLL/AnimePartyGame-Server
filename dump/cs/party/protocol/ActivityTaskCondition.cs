using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ActivityTaskCondition : IMessage<ActivityTaskCondition>, IMessage, IEquatable<ActivityTaskCondition>, IDeepCloneable<ActivityTaskCondition>, IBufferMessage
{
	private static readonly MessageParser<ActivityTaskCondition> _parser = new MessageParser<ActivityTaskCondition>(() => new ActivityTaskCondition());

	private UnknownFieldSet _unknownFields;

	public const int InfoIdFieldNumber = 1;

	private int infoId_;

	public const int CondFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_cond_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> cond_ = new MapField<int, int>();

	public const int Cond1FieldNumber = 3;

	private ConditionData cond1_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityTaskCondition> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[468];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InfoId
	{
		get
		{
			return infoId_;
		}
		set
		{
			infoId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Cond => cond_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionData Cond1
	{
		get
		{
			return cond1_;
		}
		set
		{
			cond1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskCondition()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskCondition(ActivityTaskCondition other)
		: this()
	{
		infoId_ = other.infoId_;
		cond_ = other.cond_.Clone();
		cond1_ = ((other.cond1_ != null) ? other.cond1_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskCondition Clone()
	{
		return new ActivityTaskCondition(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityTaskCondition);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityTaskCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (InfoId != other.InfoId)
		{
			return false;
		}
		if (!Cond.Equals(other.Cond))
		{
			return false;
		}
		if (!object.Equals(Cond1, other.Cond1))
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
		if (InfoId != 0)
		{
			num ^= InfoId.GetHashCode();
		}
		num ^= Cond.GetHashCode();
		if (cond1_ != null)
		{
			num ^= Cond1.GetHashCode();
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
		if (InfoId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(InfoId);
		}
		cond_.WriteTo(ref output, _map_cond_codec);
		if (cond1_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(Cond1);
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
		if (InfoId != 0)
		{
			num += 5;
		}
		num += cond_.CalculateSize(_map_cond_codec);
		if (cond1_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Cond1);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityTaskCondition other)
	{
		if (other == null)
		{
			return;
		}
		if (other.InfoId != 0)
		{
			InfoId = other.InfoId;
		}
		cond_.MergeFrom(other.cond_);
		if (other.cond1_ != null)
		{
			if (cond1_ == null)
			{
				Cond1 = new ConditionData();
			}
			Cond1.MergeFrom(other.Cond1);
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
			case 13u:
				InfoId = input.ReadSFixed32();
				break;
			case 18u:
				cond_.AddEntriesFrom(ref input, _map_cond_codec);
				break;
			case 26u:
				if (cond1_ == null)
				{
					Cond1 = new ConditionData();
				}
				input.ReadMessage(Cond1);
				break;
			}
		}
	}
}
