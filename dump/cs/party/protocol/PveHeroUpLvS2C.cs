using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PveHeroUpLvS2C : IMessage<PveHeroUpLvS2C>, IMessage, IEquatable<PveHeroUpLvS2C>, IDeepCloneable<PveHeroUpLvS2C>, IBufferMessage
{
	private static readonly MessageParser<PveHeroUpLvS2C> _parser = new MessageParser<PveHeroUpLvS2C>(() => new PveHeroUpLvS2C());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int LvFieldNumber = 2;

	private int lv_;

	public const int ExpFieldNumber = 3;

	private int exp_;

	public const int ReturnItemsFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_returnItems_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> returnItems_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PveHeroUpLvS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[513];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefId
	{
		get
		{
			return defId_;
		}
		set
		{
			defId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Exp
	{
		get
		{
			return exp_;
		}
		set
		{
			exp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ReturnItems => returnItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvS2C(PveHeroUpLvS2C other)
		: this()
	{
		defId_ = other.defId_;
		lv_ = other.lv_;
		exp_ = other.exp_;
		returnItems_ = other.returnItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvS2C Clone()
	{
		return new PveHeroUpLvS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PveHeroUpLvS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PveHeroUpLvS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DefId != other.DefId)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (Exp != other.Exp)
		{
			return false;
		}
		if (!ReturnItems.Equals(other.ReturnItems))
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
		if (DefId != 0)
		{
			num ^= DefId.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (Exp != 0)
		{
			num ^= Exp.GetHashCode();
		}
		num ^= ReturnItems.GetHashCode();
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
		if (DefId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DefId);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Lv);
		}
		if (Exp != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Exp);
		}
		returnItems_.WriteTo(ref output, _map_returnItems_codec);
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
		if (DefId != 0)
		{
			num += 5;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (Exp != 0)
		{
			num += 5;
		}
		num += returnItems_.CalculateSize(_map_returnItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PveHeroUpLvS2C other)
	{
		if (other != null)
		{
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			if (other.Lv != 0)
			{
				Lv = other.Lv;
			}
			if (other.Exp != 0)
			{
				Exp = other.Exp;
			}
			returnItems_.MergeFrom(other.returnItems_);
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
			case 13u:
				DefId = input.ReadSFixed32();
				break;
			case 21u:
				Lv = input.ReadSFixed32();
				break;
			case 29u:
				Exp = input.ReadSFixed32();
				break;
			case 34u:
				returnItems_.AddEntriesFrom(ref input, _map_returnItems_codec);
				break;
			}
		}
	}
}
