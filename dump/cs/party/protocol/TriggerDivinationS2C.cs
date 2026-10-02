using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class TriggerDivinationS2C : IMessage<TriggerDivinationS2C>, IMessage, IEquatable<TriggerDivinationS2C>, IDeepCloneable<TriggerDivinationS2C>, IBufferMessage
{
	private static readonly MessageParser<TriggerDivinationS2C> _parser = new MessageParser<TriggerDivinationS2C>(() => new TriggerDivinationS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int IdFieldNumber = 2;

	private int id_;

	public const int TargetTypeFieldNumber = 3;

	private int targetType_;

	public const int TargetIdsFieldNumber = 4;

	private static readonly FieldCodec<long> _repeated_targetIds_codec = FieldCodec.ForSFixed64(34u);

	private readonly RepeatedField<long> targetIds_ = new RepeatedField<long>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TriggerDivinationS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[334];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TargetType
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
	public RepeatedField<long> TargetIds => targetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationS2C(TriggerDivinationS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		id_ = other.id_;
		targetType_ = other.targetType_;
		targetIds_ = other.targetIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationS2C Clone()
	{
		return new TriggerDivinationS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TriggerDivinationS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TriggerDivinationS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (TargetType != other.TargetType)
		{
			return false;
		}
		if (!targetIds_.Equals(other.targetIds_))
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (TargetType != 0)
		{
			num ^= TargetType.GetHashCode();
		}
		num ^= targetIds_.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (Id != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Id);
		}
		if (TargetType != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(TargetType);
		}
		targetIds_.WriteTo(ref output, _repeated_targetIds_codec);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Id != 0)
		{
			num += 5;
		}
		if (TargetType != 0)
		{
			num += 5;
		}
		num += targetIds_.CalculateSize(_repeated_targetIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TriggerDivinationS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.TargetType != 0)
			{
				TargetType = other.TargetType;
			}
			targetIds_.Add(other.targetIds_);
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				Id = input.ReadSFixed32();
				break;
			case 29u:
				TargetType = input.ReadSFixed32();
				break;
			case 33u:
			case 34u:
				targetIds_.AddEntriesFrom(ref input, _repeated_targetIds_codec);
				break;
			}
		}
	}
}
