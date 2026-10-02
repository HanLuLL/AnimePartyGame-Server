using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PveHeroUpLvC2S : IMessage<PveHeroUpLvC2S>, IMessage, IEquatable<PveHeroUpLvC2S>, IDeepCloneable<PveHeroUpLvC2S>, IBufferMessage
{
	private static readonly MessageParser<PveHeroUpLvC2S> _parser = new MessageParser<PveHeroUpLvC2S>(() => new PveHeroUpLvC2S());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int UseItemsFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_useItems_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> useItems_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PveHeroUpLvC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[512];

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
	public MapField<int, int> UseItems => useItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvC2S(PveHeroUpLvC2S other)
		: this()
	{
		defId_ = other.defId_;
		useItems_ = other.useItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvC2S Clone()
	{
		return new PveHeroUpLvC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PveHeroUpLvC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PveHeroUpLvC2S other)
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
		if (!UseItems.Equals(other.UseItems))
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
		num ^= UseItems.GetHashCode();
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
		useItems_.WriteTo(ref output, _map_useItems_codec);
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
		num += useItems_.CalculateSize(_map_useItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PveHeroUpLvC2S other)
	{
		if (other != null)
		{
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			useItems_.MergeFrom(other.useItems_);
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
			case 18u:
				useItems_.AddEntriesFrom(ref input, _map_useItems_codec);
				break;
			}
		}
	}
}
