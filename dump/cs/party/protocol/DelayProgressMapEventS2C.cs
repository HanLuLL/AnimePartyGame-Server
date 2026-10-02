using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class DelayProgressMapEventS2C : IMessage<DelayProgressMapEventS2C>, IMessage, IEquatable<DelayProgressMapEventS2C>, IDeepCloneable<DelayProgressMapEventS2C>, IBufferMessage
{
	private static readonly MessageParser<DelayProgressMapEventS2C> _parser = new MessageParser<DelayProgressMapEventS2C>(() => new DelayProgressMapEventS2C());

	private UnknownFieldSet _unknownFields;

	public const int DelayProgressMapEventFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_delayProgressMapEvent_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<int, int> delayProgressMapEvent_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DelayProgressMapEventS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[356];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> DelayProgressMapEvent => delayProgressMapEvent_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DelayProgressMapEventS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DelayProgressMapEventS2C(DelayProgressMapEventS2C other)
		: this()
	{
		delayProgressMapEvent_ = other.delayProgressMapEvent_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DelayProgressMapEventS2C Clone()
	{
		return new DelayProgressMapEventS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DelayProgressMapEventS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DelayProgressMapEventS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!DelayProgressMapEvent.Equals(other.DelayProgressMapEvent))
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
		num ^= DelayProgressMapEvent.GetHashCode();
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
		delayProgressMapEvent_.WriteTo(ref output, _map_delayProgressMapEvent_codec);
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
		num += delayProgressMapEvent_.CalculateSize(_map_delayProgressMapEvent_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DelayProgressMapEventS2C other)
	{
		if (other != null)
		{
			delayProgressMapEvent_.MergeFrom(other.delayProgressMapEvent_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				delayProgressMapEvent_.AddEntriesFrom(ref input, _map_delayProgressMapEvent_codec);
			}
		}
	}
}
