using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PraisePlayerS2C : IMessage<PraisePlayerS2C>, IMessage, IEquatable<PraisePlayerS2C>, IDeepCloneable<PraisePlayerS2C>, IBufferMessage
{
	private static readonly MessageParser<PraisePlayerS2C> _parser = new MessageParser<PraisePlayerS2C>(() => new PraisePlayerS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_playerId_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> playerId_ = new RepeatedField<long>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PraisePlayerS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[58];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> PlayerId => playerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PraisePlayerS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PraisePlayerS2C(PraisePlayerS2C other)
		: this()
	{
		playerId_ = other.playerId_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PraisePlayerS2C Clone()
	{
		return new PraisePlayerS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PraisePlayerS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PraisePlayerS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!playerId_.Equals(other.playerId_))
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
		num ^= playerId_.GetHashCode();
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
		playerId_.WriteTo(ref output, _repeated_playerId_codec);
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
		num += playerId_.CalculateSize(_repeated_playerId_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PraisePlayerS2C other)
	{
		if (other != null)
		{
			playerId_.Add(other.playerId_);
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
			if (num - 9 > 1)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				playerId_.AddEntriesFrom(ref input, _repeated_playerId_codec);
			}
		}
	}
}
