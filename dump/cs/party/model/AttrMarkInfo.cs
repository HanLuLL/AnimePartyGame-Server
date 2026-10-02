using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class AttrMarkInfo : IMessage<AttrMarkInfo>, IMessage, IEquatable<AttrMarkInfo>, IDeepCloneable<AttrMarkInfo>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum MarkType
		{
			[OriginalName("XS")]
			Xs,
			[OriginalName("BH")]
			Bh,
			[OriginalName("DC")]
			Dc,
			[OriginalName("HT")]
			Ht
		}
	}

	private static readonly MessageParser<AttrMarkInfo> _parser = new MessageParser<AttrMarkInfo>(() => new AttrMarkInfo());

	private UnknownFieldSet _unknownFields;

	public const int TypeFieldNumber = 1;

	private Types.MarkType type_;

	public const int Mark1FieldNumber = 2;

	private int mark1_;

	public const int MarkByHeroIdFieldNumber = 3;

	private long markByHeroId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AttrMarkInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[64];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.MarkType Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Mark1
	{
		get
		{
			return mark1_;
		}
		set
		{
			mark1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MarkByHeroId
	{
		get
		{
			return markByHeroId_;
		}
		set
		{
			markByHeroId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AttrMarkInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AttrMarkInfo(AttrMarkInfo other)
		: this()
	{
		type_ = other.type_;
		mark1_ = other.mark1_;
		markByHeroId_ = other.markByHeroId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AttrMarkInfo Clone()
	{
		return new AttrMarkInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AttrMarkInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AttrMarkInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Type != other.Type)
		{
			return false;
		}
		if (Mark1 != other.Mark1)
		{
			return false;
		}
		if (MarkByHeroId != other.MarkByHeroId)
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
		if (Type != Types.MarkType.Xs)
		{
			num ^= Type.GetHashCode();
		}
		if (Mark1 != 0)
		{
			num ^= Mark1.GetHashCode();
		}
		if (MarkByHeroId != 0L)
		{
			num ^= MarkByHeroId.GetHashCode();
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
		if (Type != Types.MarkType.Xs)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)Type);
		}
		if (Mark1 != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Mark1);
		}
		if (MarkByHeroId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(MarkByHeroId);
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
		if (Type != Types.MarkType.Xs)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Type);
		}
		if (Mark1 != 0)
		{
			num += 5;
		}
		if (MarkByHeroId != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AttrMarkInfo other)
	{
		if (other != null)
		{
			if (other.Type != Types.MarkType.Xs)
			{
				Type = other.Type;
			}
			if (other.Mark1 != 0)
			{
				Mark1 = other.Mark1;
			}
			if (other.MarkByHeroId != 0L)
			{
				MarkByHeroId = other.MarkByHeroId;
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
				Type = (Types.MarkType)input.ReadEnum();
				break;
			case 21u:
				Mark1 = input.ReadSFixed32();
				break;
			case 25u:
				MarkByHeroId = input.ReadSFixed64();
				break;
			}
		}
	}
}
