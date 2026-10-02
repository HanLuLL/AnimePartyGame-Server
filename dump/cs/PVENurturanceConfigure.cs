using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PVENurturanceConfigure : IMessage<PVENurturanceConfigure>, IMessage, IEquatable<PVENurturanceConfigure>, IDeepCloneable<PVENurturanceConfigure>, IBufferMessage
{
	private static readonly MessageParser<PVENurturanceConfigure> _parser = new MessageParser<PVENurturanceConfigure>(() => new PVENurturanceConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LevelupsFieldNumber = 1;

	private static readonly FieldCodec<PVENurturanceLevelupConfigure> _repeated_levelups_codec = FieldCodec.ForMessage(10u, PVENurturanceLevelupConfigure.Parser);

	private readonly RepeatedField<PVENurturanceLevelupConfigure> levelups_ = new RepeatedField<PVENurturanceLevelupConfigure>();

	public const int LevelupDictFieldNumber = 2;

	private static readonly MapField<int, PVENurturanceLevelupConfigure>.Codec _map_levelupDict_codec = new MapField<int, PVENurturanceLevelupConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVENurturanceLevelupConfigure.Parser), 18u);

	private readonly MapField<int, PVENurturanceLevelupConfigure> levelupDict_ = new MapField<int, PVENurturanceLevelupConfigure>();

	public const int EnhancementsFieldNumber = 3;

	private static readonly FieldCodec<PVENurturanceEnhancementConfigure> _repeated_enhancements_codec = FieldCodec.ForMessage(26u, PVENurturanceEnhancementConfigure.Parser);

	private readonly RepeatedField<PVENurturanceEnhancementConfigure> enhancements_ = new RepeatedField<PVENurturanceEnhancementConfigure>();

	public const int EnhancementDictFieldNumber = 4;

	private static readonly MapField<int, PVENurturanceEnhancementConfigure>.Codec _map_enhancementDict_codec = new MapField<int, PVENurturanceEnhancementConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVENurturanceEnhancementConfigure.Parser), 34u);

	private readonly MapField<int, PVENurturanceEnhancementConfigure> enhancementDict_ = new MapField<int, PVENurturanceEnhancementConfigure>();

	public const int ItemExpsFieldNumber = 5;

	private static readonly FieldCodec<PVENurturanceItemExpConfigure> _repeated_itemExps_codec = FieldCodec.ForMessage(42u, PVENurturanceItemExpConfigure.Parser);

	private readonly RepeatedField<PVENurturanceItemExpConfigure> itemExps_ = new RepeatedField<PVENurturanceItemExpConfigure>();

	public const int ItemExpDictFieldNumber = 6;

	private static readonly MapField<int, PVENurturanceItemExpConfigure>.Codec _map_itemExpDict_codec = new MapField<int, PVENurturanceItemExpConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVENurturanceItemExpConfigure.Parser), 50u);

	private readonly MapField<int, PVENurturanceItemExpConfigure> itemExpDict_ = new MapField<int, PVENurturanceItemExpConfigure>();

	public const int BreaksFieldNumber = 7;

	private static readonly FieldCodec<PVENurturanceBreakConfigure> _repeated_breaks_codec = FieldCodec.ForMessage(58u, PVENurturanceBreakConfigure.Parser);

	private readonly RepeatedField<PVENurturanceBreakConfigure> breaks_ = new RepeatedField<PVENurturanceBreakConfigure>();

	public const int BreakDictFieldNumber = 8;

	private static readonly MapField<int, PVENurturanceBreakConfigure>.Codec _map_breakDict_codec = new MapField<int, PVENurturanceBreakConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVENurturanceBreakConfigure.Parser), 66u);

	private readonly MapField<int, PVENurturanceBreakConfigure> breakDict_ = new MapField<int, PVENurturanceBreakConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVENurturanceConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PVENurturanceReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVENurturanceLevelupConfigure> Levelups => levelups_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVENurturanceLevelupConfigure> LevelupDict => levelupDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVENurturanceEnhancementConfigure> Enhancements => enhancements_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVENurturanceEnhancementConfigure> EnhancementDict => enhancementDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVENurturanceItemExpConfigure> ItemExps => itemExps_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVENurturanceItemExpConfigure> ItemExpDict => itemExpDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVENurturanceBreakConfigure> Breaks => breaks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVENurturanceBreakConfigure> BreakDict => breakDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceConfigure(PVENurturanceConfigure other)
		: this()
	{
		levelups_ = other.levelups_.Clone();
		levelupDict_ = other.levelupDict_.Clone();
		enhancements_ = other.enhancements_.Clone();
		enhancementDict_ = other.enhancementDict_.Clone();
		itemExps_ = other.itemExps_.Clone();
		itemExpDict_ = other.itemExpDict_.Clone();
		breaks_ = other.breaks_.Clone();
		breakDict_ = other.breakDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceConfigure Clone()
	{
		return new PVENurturanceConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVENurturanceConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVENurturanceConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!levelups_.Equals(other.levelups_))
		{
			return false;
		}
		if (!LevelupDict.Equals(other.LevelupDict))
		{
			return false;
		}
		if (!enhancements_.Equals(other.enhancements_))
		{
			return false;
		}
		if (!EnhancementDict.Equals(other.EnhancementDict))
		{
			return false;
		}
		if (!itemExps_.Equals(other.itemExps_))
		{
			return false;
		}
		if (!ItemExpDict.Equals(other.ItemExpDict))
		{
			return false;
		}
		if (!breaks_.Equals(other.breaks_))
		{
			return false;
		}
		if (!BreakDict.Equals(other.BreakDict))
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
		num ^= levelups_.GetHashCode();
		num ^= LevelupDict.GetHashCode();
		num ^= enhancements_.GetHashCode();
		num ^= EnhancementDict.GetHashCode();
		num ^= itemExps_.GetHashCode();
		num ^= ItemExpDict.GetHashCode();
		num ^= breaks_.GetHashCode();
		num ^= BreakDict.GetHashCode();
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
		levelups_.WriteTo(ref output, _repeated_levelups_codec);
		levelupDict_.WriteTo(ref output, _map_levelupDict_codec);
		enhancements_.WriteTo(ref output, _repeated_enhancements_codec);
		enhancementDict_.WriteTo(ref output, _map_enhancementDict_codec);
		itemExps_.WriteTo(ref output, _repeated_itemExps_codec);
		itemExpDict_.WriteTo(ref output, _map_itemExpDict_codec);
		breaks_.WriteTo(ref output, _repeated_breaks_codec);
		breakDict_.WriteTo(ref output, _map_breakDict_codec);
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
		num += levelups_.CalculateSize(_repeated_levelups_codec);
		num += levelupDict_.CalculateSize(_map_levelupDict_codec);
		num += enhancements_.CalculateSize(_repeated_enhancements_codec);
		num += enhancementDict_.CalculateSize(_map_enhancementDict_codec);
		num += itemExps_.CalculateSize(_repeated_itemExps_codec);
		num += itemExpDict_.CalculateSize(_map_itemExpDict_codec);
		num += breaks_.CalculateSize(_repeated_breaks_codec);
		num += breakDict_.CalculateSize(_map_breakDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PVENurturanceConfigure other)
	{
		if (other != null)
		{
			levelups_.Add(other.levelups_);
			levelupDict_.MergeFrom(other.levelupDict_);
			enhancements_.Add(other.enhancements_);
			enhancementDict_.MergeFrom(other.enhancementDict_);
			itemExps_.Add(other.itemExps_);
			itemExpDict_.MergeFrom(other.itemExpDict_);
			breaks_.Add(other.breaks_);
			breakDict_.MergeFrom(other.breakDict_);
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
			case 10u:
				levelups_.AddEntriesFrom(ref input, _repeated_levelups_codec);
				break;
			case 18u:
				levelupDict_.AddEntriesFrom(ref input, _map_levelupDict_codec);
				break;
			case 26u:
				enhancements_.AddEntriesFrom(ref input, _repeated_enhancements_codec);
				break;
			case 34u:
				enhancementDict_.AddEntriesFrom(ref input, _map_enhancementDict_codec);
				break;
			case 42u:
				itemExps_.AddEntriesFrom(ref input, _repeated_itemExps_codec);
				break;
			case 50u:
				itemExpDict_.AddEntriesFrom(ref input, _map_itemExpDict_codec);
				break;
			case 58u:
				breaks_.AddEntriesFrom(ref input, _repeated_breaks_codec);
				break;
			case 66u:
				breakDict_.AddEntriesFrom(ref input, _map_breakDict_codec);
				break;
			}
		}
	}
}
