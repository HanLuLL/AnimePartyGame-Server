using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SqlScratchCard : IMessage<SqlScratchCard>, IMessage, IEquatable<SqlScratchCard>, IDeepCloneable<SqlScratchCard>, IBufferMessage
{
	private static readonly MessageParser<SqlScratchCard> _parser = new MessageParser<SqlScratchCard>(() => new SqlScratchCard());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private static readonly MapField<int, ScratchCardRecord>.Codec _map_info_codec = new MapField<int, ScratchCardRecord>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ScratchCardRecord.Parser), 10u);

	private readonly MapField<int, ScratchCardRecord> info_ = new MapField<int, ScratchCardRecord>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SqlScratchCard> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[38];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ScratchCardRecord> Info => info_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlScratchCard()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlScratchCard(SqlScratchCard other)
		: this()
	{
		info_ = other.info_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlScratchCard Clone()
	{
		return new SqlScratchCard(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SqlScratchCard);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SqlScratchCard other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Info.Equals(other.Info))
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
		num ^= Info.GetHashCode();
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
		info_.WriteTo(ref output, _map_info_codec);
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
		num += info_.CalculateSize(_map_info_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SqlScratchCard other)
	{
		if (other != null)
		{
			info_.MergeFrom(other.info_);
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
				info_.AddEntriesFrom(ref input, _map_info_codec);
			}
		}
	}
}
