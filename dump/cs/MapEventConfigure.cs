using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MapEventConfigure : IMessage<MapEventConfigure>, IMessage, IEquatable<MapEventConfigure>, IDeepCloneable<MapEventConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapEventConfigure> _parser = new MessageParser<MapEventConfigure>(() => new MapEventConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<MapEventInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, MapEventInfoConfigure.Parser);

	private readonly RepeatedField<MapEventInfoConfigure> infos_ = new RepeatedField<MapEventInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, MapEventInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, MapEventInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapEventInfoConfigure.Parser), 18u);

	private readonly MapField<int, MapEventInfoConfigure> infoDict_ = new MapField<int, MapEventInfoConfigure>();

	public const int MapEventCardsFieldNumber = 3;

	private static readonly FieldCodec<MapEventMapEventCardConfigure> _repeated_mapEventCards_codec = FieldCodec.ForMessage(26u, MapEventMapEventCardConfigure.Parser);

	private readonly RepeatedField<MapEventMapEventCardConfigure> mapEventCards_ = new RepeatedField<MapEventMapEventCardConfigure>();

	public const int MapEventCardDictFieldNumber = 4;

	private static readonly MapField<int, MapEventMapEventCardConfigure>.Codec _map_mapEventCardDict_codec = new MapField<int, MapEventMapEventCardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapEventMapEventCardConfigure.Parser), 34u);

	private readonly MapField<int, MapEventMapEventCardConfigure> mapEventCardDict_ = new MapField<int, MapEventMapEventCardConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapEventConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapEventReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapEventInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapEventInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapEventMapEventCardConfigure> MapEventCards => mapEventCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapEventMapEventCardConfigure> MapEventCardDict => mapEventCardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventConfigure(MapEventConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		mapEventCards_ = other.mapEventCards_.Clone();
		mapEventCardDict_ = other.mapEventCardDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventConfigure Clone()
	{
		return new MapEventConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapEventConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapEventConfigure other)
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
		if (!mapEventCards_.Equals(other.mapEventCards_))
		{
			return false;
		}
		if (!MapEventCardDict.Equals(other.MapEventCardDict))
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
		num ^= mapEventCards_.GetHashCode();
		num ^= MapEventCardDict.GetHashCode();
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
		mapEventCards_.WriteTo(ref output, _repeated_mapEventCards_codec);
		mapEventCardDict_.WriteTo(ref output, _map_mapEventCardDict_codec);
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
		num += mapEventCards_.CalculateSize(_repeated_mapEventCards_codec);
		num += mapEventCardDict_.CalculateSize(_map_mapEventCardDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapEventConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			mapEventCards_.Add(other.mapEventCards_);
			mapEventCardDict_.MergeFrom(other.mapEventCardDict_);
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
				mapEventCards_.AddEntriesFrom(ref input, _repeated_mapEventCards_codec);
				break;
			case 34u:
				mapEventCardDict_.AddEntriesFrom(ref input, _map_mapEventCardDict_codec);
				break;
			}
		}
	}

	public void Fix()
	{
	}
}
