using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PVEShopBuyS2C : IMessage<PVEShopBuyS2C>, IMessage, IEquatable<PVEShopBuyS2C>, IDeepCloneable<PVEShopBuyS2C>, IBufferMessage
{
	private static readonly MessageParser<PVEShopBuyS2C> _parser = new MessageParser<PVEShopBuyS2C>(() => new PVEShopBuyS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int BuyCardsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_buyCards_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> buyCards_ = new RepeatedField<int>();

	public const int AssistPlayerFieldNumber = 3;

	private long assistPlayer_;

	public const int CardsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_cards_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> cards_ = new RepeatedField<int>();

	public const int AlreadysFieldNumber = 5;

	private static readonly FieldCodec<bool> _repeated_alreadys_codec = FieldCodec.ForBool(42u);

	private readonly RepeatedField<bool> alreadys_ = new RepeatedField<bool>();

	public const int IsCloseFieldNumber = 6;

	private bool isClose_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVEShopBuyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[256];

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
	public RepeatedField<int> BuyCards => buyCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long AssistPlayer
	{
		get
		{
			return assistPlayer_;
		}
		set
		{
			assistPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Cards => cards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<bool> Alreadys => alreadys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsClose
	{
		get
		{
			return isClose_;
		}
		set
		{
			isClose_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyS2C(PVEShopBuyS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		buyCards_ = other.buyCards_.Clone();
		assistPlayer_ = other.assistPlayer_;
		cards_ = other.cards_.Clone();
		alreadys_ = other.alreadys_.Clone();
		isClose_ = other.isClose_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyS2C Clone()
	{
		return new PVEShopBuyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVEShopBuyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVEShopBuyS2C other)
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
		if (!buyCards_.Equals(other.buyCards_))
		{
			return false;
		}
		if (AssistPlayer != other.AssistPlayer)
		{
			return false;
		}
		if (!cards_.Equals(other.cards_))
		{
			return false;
		}
		if (!alreadys_.Equals(other.alreadys_))
		{
			return false;
		}
		if (IsClose != other.IsClose)
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
		num ^= buyCards_.GetHashCode();
		if (AssistPlayer != 0L)
		{
			num ^= AssistPlayer.GetHashCode();
		}
		num ^= cards_.GetHashCode();
		num ^= alreadys_.GetHashCode();
		if (IsClose)
		{
			num ^= IsClose.GetHashCode();
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
		buyCards_.WriteTo(ref output, _repeated_buyCards_codec);
		if (AssistPlayer != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(AssistPlayer);
		}
		cards_.WriteTo(ref output, _repeated_cards_codec);
		alreadys_.WriteTo(ref output, _repeated_alreadys_codec);
		if (IsClose)
		{
			output.WriteRawTag(48);
			output.WriteBool(IsClose);
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
		num += buyCards_.CalculateSize(_repeated_buyCards_codec);
		if (AssistPlayer != 0L)
		{
			num += 9;
		}
		num += cards_.CalculateSize(_repeated_cards_codec);
		num += alreadys_.CalculateSize(_repeated_alreadys_codec);
		if (IsClose)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PVEShopBuyS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			buyCards_.Add(other.buyCards_);
			if (other.AssistPlayer != 0L)
			{
				AssistPlayer = other.AssistPlayer;
			}
			cards_.Add(other.cards_);
			alreadys_.Add(other.alreadys_);
			if (other.IsClose)
			{
				IsClose = other.IsClose;
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
			case 18u:
			case 21u:
				buyCards_.AddEntriesFrom(ref input, _repeated_buyCards_codec);
				break;
			case 25u:
				AssistPlayer = input.ReadSFixed64();
				break;
			case 34u:
			case 37u:
				cards_.AddEntriesFrom(ref input, _repeated_cards_codec);
				break;
			case 40u:
			case 42u:
				alreadys_.AddEntriesFrom(ref input, _repeated_alreadys_codec);
				break;
			case 48u:
				IsClose = input.ReadBool();
				break;
			}
		}
	}
}
