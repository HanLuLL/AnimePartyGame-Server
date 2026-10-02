using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ItemConfigure : IMessage<ItemConfigure>, IMessage, IEquatable<ItemConfigure>, IDeepCloneable<ItemConfigure>, IBufferMessage
{
	private static readonly MessageParser<ItemConfigure> _parser = new MessageParser<ItemConfigure>(() => new ItemConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<ItemInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, ItemInfoConfigure.Parser);

	private readonly RepeatedField<ItemInfoConfigure> infos_ = new RepeatedField<ItemInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, ItemInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, ItemInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ItemInfoConfigure.Parser), 18u);

	private readonly MapField<int, ItemInfoConfigure> infoDict_ = new MapField<int, ItemInfoConfigure>();

	public const int TagsFieldNumber = 3;

	private static readonly FieldCodec<ItemTagConfigure> _repeated_tags_codec = FieldCodec.ForMessage(26u, ItemTagConfigure.Parser);

	private readonly RepeatedField<ItemTagConfigure> tags_ = new RepeatedField<ItemTagConfigure>();

	public const int TagDictFieldNumber = 4;

	private static readonly MapField<int, ItemTagConfigure>.Codec _map_tagDict_codec = new MapField<int, ItemTagConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ItemTagConfigure.Parser), 34u);

	private readonly MapField<int, ItemTagConfigure> tagDict_ = new MapField<int, ItemTagConfigure>();

	public const int UIsFieldNumber = 5;

	private static readonly FieldCodec<ItemUIConfigure> _repeated_uIs_codec = FieldCodec.ForMessage(42u, ItemUIConfigure.Parser);

	private readonly RepeatedField<ItemUIConfigure> uIs_ = new RepeatedField<ItemUIConfigure>();

	public const int UIDictFieldNumber = 6;

	private static readonly MapField<int, ItemUIConfigure>.Codec _map_uIDict_codec = new MapField<int, ItemUIConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ItemUIConfigure.Parser), 50u);

	private readonly MapField<int, ItemUIConfigure> uIDict_ = new MapField<int, ItemUIConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ItemConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ItemReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ItemInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemTagConfigure> Tags => tags_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ItemTagConfigure> TagDict => tagDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemUIConfigure> UIs => uIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ItemUIConfigure> UIDict => uIDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemConfigure(ItemConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		tags_ = other.tags_.Clone();
		tagDict_ = other.tagDict_.Clone();
		uIs_ = other.uIs_.Clone();
		uIDict_ = other.uIDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemConfigure Clone()
	{
		return new ItemConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ItemConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ItemConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!tags_.Equals(other.tags_))
		{
			return false;
		}
		if (!TagDict.Equals(other.TagDict))
		{
			return false;
		}
		if (!uIs_.Equals(other.uIs_))
		{
			return false;
		}
		if (!UIDict.Equals(other.UIDict))
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
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= tags_.GetHashCode();
		num ^= TagDict.GetHashCode();
		num ^= uIs_.GetHashCode();
		num ^= UIDict.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		tags_.WriteTo(ref output, _repeated_tags_codec);
		tagDict_.WriteTo(ref output, _map_tagDict_codec);
		uIs_.WriteTo(ref output, _repeated_uIs_codec);
		uIDict_.WriteTo(ref output, _map_uIDict_codec);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += tags_.CalculateSize(_repeated_tags_codec);
		num += tagDict_.CalculateSize(_map_tagDict_codec);
		num += uIs_.CalculateSize(_repeated_uIs_codec);
		num += uIDict_.CalculateSize(_map_uIDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ItemConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			tags_.Add(other.tags_);
			tagDict_.MergeFrom(other.tagDict_);
			uIs_.Add(other.uIs_);
			uIDict_.MergeFrom(other.uIDict_);
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
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				tags_.AddEntriesFrom(ref input, _repeated_tags_codec);
				break;
			case 34u:
				tagDict_.AddEntriesFrom(ref input, _map_tagDict_codec);
				break;
			case 42u:
				uIs_.AddEntriesFrom(ref input, _repeated_uIs_codec);
				break;
			case 50u:
				uIDict_.AddEntriesFrom(ref input, _map_uIDict_codec);
				break;
			}
		}
	}

	public void Fix()
	{
		FixItemConfigure fixItem = StaticConfigure.FixItem;
		if (fixItem == null)
		{
			return;
		}
		MapField<int, FixItemShieldConfigure> shieldDict = fixItem.ShieldDict;
		if (shieldDict != null && shieldDict.Count > 0)
		{
			for (int num = infos_.Count - 1; num >= 0; num--)
			{
				if (shieldDict.TryGetValue(infos_[num].Id, out var value))
				{
					infos_[num].UpdateVailTime(value.LaunchTime, value.EndTime);
					infoDict_[infos_[num].Id].UpdateVailTime(value.LaunchTime, value.EndTime);
				}
			}
		}
		MapField<int, FixItemInfoConfigure> infoDict = fixItem.InfoDict;
		if (infoDict == null || infoDict.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < infos_.Count; i++)
		{
			if (infoDict.TryGetValue(infos_[i].Id, out var value2))
			{
				infos_[i].FixEndTime(value2);
				infoDict_[infos_[i].Id].FixEndTime(value2);
			}
		}
	}
}
