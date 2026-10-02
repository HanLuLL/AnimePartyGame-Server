using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class LandChoiceTargetS2C : IMessage<LandChoiceTargetS2C>, IMessage, IEquatable<LandChoiceTargetS2C>, IDeepCloneable<LandChoiceTargetS2C>, IBufferMessage
{
	private static readonly MessageParser<LandChoiceTargetS2C> _parser = new MessageParser<LandChoiceTargetS2C>(() => new LandChoiceTargetS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int TargetIdsFieldNumber = 2;

	private static readonly FieldCodec<long> _repeated_targetIds_codec = FieldCodec.ForSFixed64(18u);

	private readonly RepeatedField<long> targetIds_ = new RepeatedField<long>();

	public const int ExitFieldNumber = 3;

	private bool exit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LandChoiceTargetS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[332];

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
	public RepeatedField<long> TargetIds => targetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Exit
	{
		get
		{
			return exit_;
		}
		set
		{
			exit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetS2C(LandChoiceTargetS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		targetIds_ = other.targetIds_.Clone();
		exit_ = other.exit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetS2C Clone()
	{
		return new LandChoiceTargetS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LandChoiceTargetS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LandChoiceTargetS2C other)
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
		if (!targetIds_.Equals(other.targetIds_))
		{
			return false;
		}
		if (Exit != other.Exit)
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
		num ^= targetIds_.GetHashCode();
		if (Exit)
		{
			num ^= Exit.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		targetIds_.WriteTo(ref output, _repeated_targetIds_codec);
		if (Exit)
		{
			output.WriteRawTag(24);
			output.WriteBool(Exit);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		num += targetIds_.CalculateSize(_repeated_targetIds_codec);
		if (Exit)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LandChoiceTargetS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			targetIds_.Add(other.targetIds_);
			if (other.Exit)
			{
				Exit = other.Exit;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 17u:
			case 18u:
				targetIds_.AddEntriesFrom(ref input, _repeated_targetIds_codec);
				break;
			case 24u:
				Exit = input.ReadBool();
				break;
			}
		}
	}
}
