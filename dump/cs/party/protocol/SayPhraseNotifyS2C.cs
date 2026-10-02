using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SayPhraseNotifyS2C : IMessage<SayPhraseNotifyS2C>, IMessage, IEquatable<SayPhraseNotifyS2C>, IDeepCloneable<SayPhraseNotifyS2C>, IBufferMessage
{
	private static readonly MessageParser<SayPhraseNotifyS2C> _parser = new MessageParser<SayPhraseNotifyS2C>(() => new SayPhraseNotifyS2C());

	private UnknownFieldSet _unknownFields;

	public const int PhraseFieldNumber = 1;

	private TriggerPhrase phrase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SayPhraseNotifyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[394];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerPhrase Phrase
	{
		get
		{
			return phrase_;
		}
		set
		{
			phrase_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SayPhraseNotifyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SayPhraseNotifyS2C(SayPhraseNotifyS2C other)
		: this()
	{
		phrase_ = ((other.phrase_ != null) ? other.phrase_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SayPhraseNotifyS2C Clone()
	{
		return new SayPhraseNotifyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SayPhraseNotifyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SayPhraseNotifyS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Phrase, other.Phrase))
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
		if (phrase_ != null)
		{
			num ^= Phrase.GetHashCode();
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
		if (phrase_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Phrase);
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
		if (phrase_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Phrase);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SayPhraseNotifyS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.phrase_ != null)
		{
			if (phrase_ == null)
			{
				Phrase = new TriggerPhrase();
			}
			Phrase.MergeFrom(other.Phrase);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				continue;
			}
			if (phrase_ == null)
			{
				Phrase = new TriggerPhrase();
			}
			input.ReadMessage(Phrase);
		}
	}
}
