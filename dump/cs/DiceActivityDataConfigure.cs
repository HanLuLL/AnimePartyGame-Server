using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class DiceActivityDataConfigure : IMessage<DiceActivityDataConfigure>, IMessage, IEquatable<DiceActivityDataConfigure>, IDeepCloneable<DiceActivityDataConfigure>, IBufferMessage
{
	private static readonly MessageParser<DiceActivityDataConfigure> _parser = new MessageParser<DiceActivityDataConfigure>(() => new DiceActivityDataConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapIDFieldNumber = 1;

	private int mapID_;

	public const int DiceActivityDataConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<DiceActivityDataConfigureItem> _repeated_diceActivityDataConfigureItems_codec = FieldCodec.ForMessage(18u, DiceActivityDataConfigureItem.Parser);

	private readonly RepeatedField<DiceActivityDataConfigureItem> diceActivityDataConfigureItems_ = new RepeatedField<DiceActivityDataConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DiceActivityDataConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DiceActivityReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapID
	{
		get
		{
			return mapID_;
		}
		private set
		{
			mapID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<DiceActivityDataConfigureItem> DiceActivityDataConfigureItems => diceActivityDataConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityDataConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityDataConfigure(DiceActivityDataConfigure other)
		: this()
	{
		mapID_ = other.mapID_;
		diceActivityDataConfigureItems_ = other.diceActivityDataConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityDataConfigure Clone()
	{
		return new DiceActivityDataConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DiceActivityDataConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DiceActivityDataConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapID != other.MapID)
		{
			return false;
		}
		if (!diceActivityDataConfigureItems_.Equals(other.diceActivityDataConfigureItems_))
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
		if (MapID != 0)
		{
			num ^= MapID.GetHashCode();
		}
		num ^= diceActivityDataConfigureItems_.GetHashCode();
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
		if (MapID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MapID);
		}
		diceActivityDataConfigureItems_.WriteTo(ref output, _repeated_diceActivityDataConfigureItems_codec);
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
		if (MapID != 0)
		{
			num += 5;
		}
		num += diceActivityDataConfigureItems_.CalculateSize(_repeated_diceActivityDataConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DiceActivityDataConfigure other)
	{
		if (other != null)
		{
			if (other.MapID != 0)
			{
				MapID = other.MapID;
			}
			diceActivityDataConfigureItems_.Add(other.diceActivityDataConfigureItems_);
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
				MapID = input.ReadSFixed32();
				break;
			case 18u:
				diceActivityDataConfigureItems_.AddEntriesFrom(ref input, _repeated_diceActivityDataConfigureItems_codec);
				break;
			}
		}
	}
}
