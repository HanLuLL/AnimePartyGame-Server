using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class NotifyQuestionS2C : IMessage<NotifyQuestionS2C>, IMessage, IEquatable<NotifyQuestionS2C>, IDeepCloneable<NotifyQuestionS2C>, IBufferMessage
{
	private static readonly MessageParser<NotifyQuestionS2C> _parser = new MessageParser<NotifyQuestionS2C>(() => new NotifyQuestionS2C());

	private UnknownFieldSet _unknownFields;

	public const int QuestionInfoFieldNumber = 1;

	private static readonly MapField<int, QuestionModel>.Codec _map_questionInfo_codec = new MapField<int, QuestionModel>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, QuestionModel.Parser), 10u);

	private readonly MapField<int, QuestionModel> questionInfo_ = new MapField<int, QuestionModel>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<NotifyQuestionS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[426];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, QuestionModel> QuestionInfo => questionInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NotifyQuestionS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NotifyQuestionS2C(NotifyQuestionS2C other)
		: this()
	{
		questionInfo_ = other.questionInfo_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NotifyQuestionS2C Clone()
	{
		return new NotifyQuestionS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotifyQuestionS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(NotifyQuestionS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!QuestionInfo.Equals(other.QuestionInfo))
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
		num ^= QuestionInfo.GetHashCode();
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
		questionInfo_.WriteTo(ref output, _map_questionInfo_codec);
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
		num += questionInfo_.CalculateSize(_map_questionInfo_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotifyQuestionS2C other)
	{
		if (other != null)
		{
			questionInfo_.MergeFrom(other.questionInfo_);
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
				questionInfo_.AddEntriesFrom(ref input, _map_questionInfo_codec);
			}
		}
	}
}
