using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SearchGuildS2C : IMessage<SearchGuildS2C>, IMessage, IEquatable<SearchGuildS2C>, IDeepCloneable<SearchGuildS2C>, IBufferMessage
{
	private static readonly MessageParser<SearchGuildS2C> _parser = new MessageParser<SearchGuildS2C>(() => new SearchGuildS2C());

	private UnknownFieldSet _unknownFields;

	public const int IsEndFieldNumber = 1;

	private bool isEnd_;

	public const int GuildsFieldNumber = 2;

	private static readonly FieldCodec<Guild> _repeated_guilds_codec = FieldCodec.ForMessage(18u, Guild.Parser);

	private readonly RepeatedField<Guild> guilds_ = new RepeatedField<Guild>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SearchGuildS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[564];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsEnd
	{
		get
		{
			return isEnd_;
		}
		set
		{
			isEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Guild> Guilds => guilds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchGuildS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchGuildS2C(SearchGuildS2C other)
		: this()
	{
		isEnd_ = other.isEnd_;
		guilds_ = other.guilds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchGuildS2C Clone()
	{
		return new SearchGuildS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SearchGuildS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SearchGuildS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsEnd != other.IsEnd)
		{
			return false;
		}
		if (!guilds_.Equals(other.guilds_))
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
		if (IsEnd)
		{
			num ^= IsEnd.GetHashCode();
		}
		num ^= guilds_.GetHashCode();
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
		if (IsEnd)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsEnd);
		}
		guilds_.WriteTo(ref output, _repeated_guilds_codec);
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
		if (IsEnd)
		{
			num += 2;
		}
		num += guilds_.CalculateSize(_repeated_guilds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SearchGuildS2C other)
	{
		if (other != null)
		{
			if (other.IsEnd)
			{
				IsEnd = other.IsEnd;
			}
			guilds_.Add(other.guilds_);
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
				IsEnd = input.ReadBool();
				break;
			case 18u:
				guilds_.AddEntriesFrom(ref input, _repeated_guilds_codec);
				break;
			}
		}
	}
}
