using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChooseSkinS2C : IMessage<ChooseSkinS2C>, IMessage, IEquatable<ChooseSkinS2C>, IDeepCloneable<ChooseSkinS2C>, IBufferMessage
{
	private static readonly MessageParser<ChooseSkinS2C> _parser = new MessageParser<ChooseSkinS2C>(() => new ChooseSkinS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int UseAdornFieldNumber = 2;

	private int useAdorn_;

	public const int AffirmedFieldNumber = 3;

	private bool affirmed_;

	public const int HeroIdFieldNumber = 4;

	private int heroId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChooseSkinS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[181];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
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
	public ChooseSkinS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinS2C(ChooseSkinS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		useAdorn_ = other.useAdorn_;
		affirmed_ = other.affirmed_;
		heroId_ = other.heroId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinS2C Clone()
	{
		return new ChooseSkinS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChooseSkinS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChooseSkinS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
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
		if (HeroId != other.HeroId)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (UseAdorn != 0)
		{
			num ^= UseAdorn.GetHashCode();
		}
		if (Affirmed)
		{
			num ^= Affirmed.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
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
		if (HeroId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(HeroId);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (UseAdorn != 0)
		{
			num += 5;
		}
		if (Affirmed)
		{
			num += 2;
		}
		if (HeroId != 0)
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
	public void MergeFrom(ChooseSkinS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.UseAdorn != 0)
			{
				UseAdorn = other.UseAdorn;
			}
			if (other.Affirmed)
			{
				Affirmed = other.Affirmed;
			}
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				UseAdorn = input.ReadSFixed32();
				break;
			case 24u:
				Affirmed = input.ReadBool();
				break;
			case 37u:
				HeroId = input.ReadSFixed32();
				break;
			}
		}
	}
}
