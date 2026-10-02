using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerRelicConfigure : IMessage<SinglePlayerRelicConfigure>, IMessage, IEquatable<SinglePlayerRelicConfigure>, IDeepCloneable<SinglePlayerRelicConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerRelicConfigure> _parser = new MessageParser<SinglePlayerRelicConfigure>(() => new SinglePlayerRelicConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int RarityFieldNumber = 2;

	private int rarity_;

	public const int CardPackIDFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_cardPackID_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> cardPackID_ = new RepeatedField<int>();

	public const int NameIDFieldNumber = 4;

	private int nameID_;

	public const int DesiIDFieldNumber = 5;

	private int desiID_;

	public const int IconFieldNumber = 6;

	private string icon_ = "";

	public const int BuildingFieldNumber = 7;

	private string building_ = "";

	public const int RelicParamFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_relicParam_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> relicParam_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerRelicConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[5];

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
	public RepeatedField<int> CardPackID => cardPackID_;

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
	public int DesiID
	{
		get
		{
			return desiID_;
		}
		private set
		{
			desiID_ = value;
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
	public string Building
	{
		get
		{
			return building_;
		}
		private set
		{
			building_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RelicParam => relicParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerRelicConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerRelicConfigure(SinglePlayerRelicConfigure other)
		: this()
	{
		id_ = other.id_;
		rarity_ = other.rarity_;
		cardPackID_ = other.cardPackID_.Clone();
		nameID_ = other.nameID_;
		desiID_ = other.desiID_;
		icon_ = other.icon_;
		building_ = other.building_;
		relicParam_ = other.relicParam_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerRelicConfigure Clone()
	{
		return new SinglePlayerRelicConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerRelicConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerRelicConfigure other)
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
		if (Rarity != other.Rarity)
		{
			return false;
		}
		if (!cardPackID_.Equals(other.cardPackID_))
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DesiID != other.DesiID)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (Building != other.Building)
		{
			return false;
		}
		if (!relicParam_.Equals(other.relicParam_))
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
		if (Rarity != 0)
		{
			num ^= Rarity.GetHashCode();
		}
		num ^= cardPackID_.GetHashCode();
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DesiID != 0)
		{
			num ^= DesiID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (Building.Length != 0)
		{
			num ^= Building.GetHashCode();
		}
		num ^= relicParam_.GetHashCode();
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
		if (Rarity != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Rarity);
		}
		cardPackID_.WriteTo(ref output, _repeated_cardPackID_codec);
		if (NameID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(NameID);
		}
		if (DesiID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DesiID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Icon);
		}
		if (Building.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Building);
		}
		relicParam_.WriteTo(ref output, _repeated_relicParam_codec);
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
		if (Rarity != 0)
		{
			num += 5;
		}
		num += cardPackID_.CalculateSize(_repeated_cardPackID_codec);
		if (NameID != 0)
		{
			num += 5;
		}
		if (DesiID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (Building.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Building);
		}
		num += relicParam_.CalculateSize(_repeated_relicParam_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerRelicConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Rarity != 0)
			{
				Rarity = other.Rarity;
			}
			cardPackID_.Add(other.cardPackID_);
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.DesiID != 0)
			{
				DesiID = other.DesiID;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			if (other.Building.Length != 0)
			{
				Building = other.Building;
			}
			relicParam_.Add(other.relicParam_);
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
				Rarity = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				cardPackID_.AddEntriesFrom(ref input, _repeated_cardPackID_codec);
				break;
			case 37u:
				NameID = input.ReadSFixed32();
				break;
			case 45u:
				DesiID = input.ReadSFixed32();
				break;
			case 50u:
				Icon = input.ReadString();
				break;
			case 58u:
				Building = input.ReadString();
				break;
			case 66u:
			case 69u:
				relicParam_.AddEntriesFrom(ref input, _repeated_relicParam_codec);
				break;
			}
		}
	}
}
