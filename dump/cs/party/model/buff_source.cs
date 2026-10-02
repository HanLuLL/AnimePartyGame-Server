using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class buff_source : IMessage<buff_source>, IMessage, IEquatable<buff_source>, IDeepCloneable<buff_source>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum source
		{
			[OriginalName("unknown")]
			Unknown,
			[OriginalName("skill")]
			Skill,
			[OriginalName("card")]
			Card,
			[OriginalName("event")]
			Event,
			[OriginalName("summon")]
			Summon,
			[OriginalName("destiny")]
			Destiny,
			[OriginalName("relic")]
			Relic,
			[OriginalName("mission")]
			Mission,
			[OriginalName("gameMode")]
			GameMode,
			[OriginalName("luckyStar")]
			LuckyStar,
			[OriginalName("terms")]
			Terms,
			[OriginalName("level")]
			Level
		}
	}

	private static readonly MessageParser<buff_source> _parser = new MessageParser<buff_source>(() => new buff_source());

	private UnknownFieldSet _unknownFields;

	public const int SFieldNumber = 1;

	private Types.source s_;

	public const int IdFieldNumber = 2;

	private int id_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<buff_source> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[85];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.source S
	{
		get
		{
			return s_;
		}
		set
		{
			s_ = value;
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
	public buff_source()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public buff_source(buff_source other)
		: this()
	{
		s_ = other.s_;
		id_ = other.id_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public buff_source Clone()
	{
		return new buff_source(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as buff_source);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(buff_source other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (S != other.S)
		{
			return false;
		}
		if (Id != other.Id)
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
		if (S != Types.source.Unknown)
		{
			num ^= S.GetHashCode();
		}
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
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
		if (S != Types.source.Unknown)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)S);
		}
		if (Id != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Id);
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
		if (S != Types.source.Unknown)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)S);
		}
		if (Id != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(buff_source other)
	{
		if (other != null)
		{
			if (other.S != Types.source.Unknown)
			{
				S = other.S;
			}
			if (other.Id != 0)
			{
				Id = other.Id;
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
			case 8u:
				S = (Types.source)input.ReadEnum();
				break;
			case 21u:
				Id = input.ReadSFixed32();
				break;
			}
		}
	}
}
