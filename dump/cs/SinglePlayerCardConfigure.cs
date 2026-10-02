using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerCardConfigure : IMessage<SinglePlayerCardConfigure>, IMessage, IEquatable<SinglePlayerCardConfigure>, IDeepCloneable<SinglePlayerCardConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerCardConfigure> _parser = new MessageParser<SinglePlayerCardConfigure>(() => new SinglePlayerCardConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int IconFieldNumber = 3;

	private string icon_ = "";

	public const int CardPackIDFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_cardPackID_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> cardPackID_ = new RepeatedField<int>();

	public const int RarityFieldNumber = 5;

	private int rarity_;

	public const int CardTagFieldNumber = 6;

	private static readonly FieldCodec<SinglePlayerTagType> _repeated_cardTag_codec = FieldCodec.ForEnum(50u, (SinglePlayerTagType x) => (int)x, (int x) => (SinglePlayerTagType)x);

	private readonly RepeatedField<SinglePlayerTagType> cardTag_ = new RepeatedField<SinglePlayerTagType>();

	public const int TriggerPriorityFieldNumber = 7;

	private int triggerPriority_;

	public const int DesIDFieldNumber = 8;

	private int desID_;

	public const int SinglePlayerCardConfigureItemsFieldNumber = 9;

	private static readonly FieldCodec<SinglePlayerCardConfigureItem> _repeated_singlePlayerCardConfigureItems_codec = FieldCodec.ForMessage(74u, SinglePlayerCardConfigureItem.Parser);

	private readonly RepeatedField<SinglePlayerCardConfigureItem> singlePlayerCardConfigureItems_ = new RepeatedField<SinglePlayerCardConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerCardConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[11];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardPackID => cardPackID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Rarity
	{
		get
		{
			return rarity_;
		}
		private set
		{
			rarity_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerTagType> CardTag => cardTag_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TriggerPriority
	{
		get
		{
			return triggerPriority_;
		}
		private set
		{
			triggerPriority_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DesID
	{
		get
		{
			return desID_;
		}
		private set
		{
			desID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerCardConfigureItem> SinglePlayerCardConfigureItems => singlePlayerCardConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardConfigure(SinglePlayerCardConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		icon_ = other.icon_;
		cardPackID_ = other.cardPackID_.Clone();
		rarity_ = other.rarity_;
		cardTag_ = other.cardTag_.Clone();
		triggerPriority_ = other.triggerPriority_;
		desID_ = other.desID_;
		singlePlayerCardConfigureItems_ = other.singlePlayerCardConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardConfigure Clone()
	{
		return new SinglePlayerCardConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerCardConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerCardConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (!cardPackID_.Equals(other.cardPackID_))
		{
			return false;
		}
		if (Rarity != other.Rarity)
		{
			return false;
		}
		if (!cardTag_.Equals(other.cardTag_))
		{
			return false;
		}
		if (TriggerPriority != other.TriggerPriority)
		{
			return false;
		}
		if (DesID != other.DesID)
		{
			return false;
		}
		if (!singlePlayerCardConfigureItems_.Equals(other.singlePlayerCardConfigureItems_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		num ^= cardPackID_.GetHashCode();
		if (Rarity != 0)
		{
			num ^= Rarity.GetHashCode();
		}
		num ^= cardTag_.GetHashCode();
		if (TriggerPriority != 0)
		{
			num ^= TriggerPriority.GetHashCode();
		}
		if (DesID != 0)
		{
			num ^= DesID.GetHashCode();
		}
		num ^= singlePlayerCardConfigureItems_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Icon);
		}
		cardPackID_.WriteTo(ref output, _repeated_cardPackID_codec);
		if (Rarity != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Rarity);
		}
		cardTag_.WriteTo(ref output, _repeated_cardTag_codec);
		if (TriggerPriority != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(TriggerPriority);
		}
		if (DesID != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(DesID);
		}
		singlePlayerCardConfigureItems_.WriteTo(ref output, _repeated_singlePlayerCardConfigureItems_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		num += cardPackID_.CalculateSize(_repeated_cardPackID_codec);
		if (Rarity != 0)
		{
			num += 5;
		}
		num += cardTag_.CalculateSize(_repeated_cardTag_codec);
		if (TriggerPriority != 0)
		{
			num += 5;
		}
		if (DesID != 0)
		{
			num += 5;
		}
		num += singlePlayerCardConfigureItems_.CalculateSize(_repeated_singlePlayerCardConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerCardConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			cardPackID_.Add(other.cardPackID_);
			if (other.Rarity != 0)
			{
				Rarity = other.Rarity;
			}
			cardTag_.Add(other.cardTag_);
			if (other.TriggerPriority != 0)
			{
				TriggerPriority = other.TriggerPriority;
			}
			if (other.DesID != 0)
			{
				DesID = other.DesID;
			}
			singlePlayerCardConfigureItems_.Add(other.singlePlayerCardConfigureItems_);
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 26u:
				Icon = input.ReadString();
				break;
			case 34u:
			case 37u:
				cardPackID_.AddEntriesFrom(ref input, _repeated_cardPackID_codec);
				break;
			case 45u:
				Rarity = input.ReadSFixed32();
				break;
			case 48u:
			case 50u:
				cardTag_.AddEntriesFrom(ref input, _repeated_cardTag_codec);
				break;
			case 61u:
				TriggerPriority = input.ReadSFixed32();
				break;
			case 69u:
				DesID = input.ReadSFixed32();
				break;
			case 74u:
				singlePlayerCardConfigureItems_.AddEntriesFrom(ref input, _repeated_singlePlayerCardConfigureItems_codec);
				break;
			}
		}
	}
}
