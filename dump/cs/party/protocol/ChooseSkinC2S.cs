using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChooseSkinC2S : IMessage<ChooseSkinC2S>, IMessage, IEquatable<ChooseSkinC2S>, IDeepCloneable<ChooseSkinC2S>, IBufferMessage
{
	private static readonly MessageParser<ChooseSkinC2S> _parser = new MessageParser<ChooseSkinC2S>(() => new ChooseSkinC2S());

	private UnknownFieldSet _unknownFields;

	public const int HeroIdFieldNumber = 1;

	private int heroId_;

	public const int UseAdornFieldNumber = 2;

	private int useAdorn_;

	public const int AffirmedFieldNumber = 3;

	private bool affirmed_;

	public const int SkinPendantFieldNumber = 4;

	private int skinPendant_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChooseSkinC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[180];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeroId
	{
		get
		{
			return heroId_;
		}
		set
		{
			heroId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseAdorn
	{
		get
		{
			return useAdorn_;
		}
		set
		{
			useAdorn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Affirmed
	{
		get
		{
			return affirmed_;
		}
		set
		{
			affirmed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinPendant
	{
		get
		{
			return skinPendant_;
		}
		set
		{
			skinPendant_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinC2S(ChooseSkinC2S other)
		: this()
	{
		heroId_ = other.heroId_;
		useAdorn_ = other.useAdorn_;
		affirmed_ = other.affirmed_;
		skinPendant_ = other.skinPendant_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinC2S Clone()
	{
		return new ChooseSkinC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChooseSkinC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChooseSkinC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (UseAdorn != other.UseAdorn)
		{
			return false;
		}
		if (Affirmed != other.Affirmed)
		{
			return false;
		}
		if (SkinPendant != other.SkinPendant)
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
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (UseAdorn != 0)
		{
			num ^= UseAdorn.GetHashCode();
		}
		if (Affirmed)
		{
			num ^= Affirmed.GetHashCode();
		}
		if (SkinPendant != 0)
		{
			num ^= SkinPendant.GetHashCode();
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
		if (HeroId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(HeroId);
		}
		if (UseAdorn != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(UseAdorn);
		}
		if (Affirmed)
		{
			output.WriteRawTag(24);
			output.WriteBool(Affirmed);
		}
		if (SkinPendant != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SkinPendant);
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
		if (HeroId != 0)
		{
			num += 5;
		}
		if (UseAdorn != 0)
		{
			num += 5;
		}
		if (Affirmed)
		{
			num += 2;
		}
		if (SkinPendant != 0)
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
	public void MergeFrom(ChooseSkinC2S other)
	{
		if (other != null)
		{
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.UseAdorn != 0)
			{
				UseAdorn = other.UseAdorn;
			}
			if (other.Affirmed)
			{
				Affirmed = other.Affirmed;
			}
			if (other.SkinPendant != 0)
			{
				SkinPendant = other.SkinPendant;
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
			case 13u:
				HeroId = input.ReadSFixed32();
				break;
			case 21u:
				UseAdorn = input.ReadSFixed32();
				break;
			case 24u:
				Affirmed = input.ReadBool();
				break;
			case 37u:
				SkinPendant = input.ReadSFixed32();
				break;
			}
		}
	}
}
