using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChoosingTimeLimitConfigure : IMessage<ChoosingTimeLimitConfigure>, IMessage, IEquatable<ChoosingTimeLimitConfigure>, IDeepCloneable<ChoosingTimeLimitConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChoosingTimeLimitConfigure> _parser = new MessageParser<ChoosingTimeLimitConfigure>(() => new ChoosingTimeLimitConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<ChoosingTimeLimitInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, ChoosingTimeLimitInfoConfigure.Parser);

	private readonly RepeatedField<ChoosingTimeLimitInfoConfigure> infos_ = new RepeatedField<ChoosingTimeLimitInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, ChoosingTimeLimitInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, ChoosingTimeLimitInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChoosingTimeLimitInfoConfigure.Parser), 18u);

	private readonly MapField<int, ChoosingTimeLimitInfoConfigure> infoDict_ = new MapField<int, ChoosingTimeLimitInfoConfigure>();

	public const int GamespeedsFieldNumber = 3;

	private static readonly FieldCodec<ChoosingTimeLimitgamespeedConfigure> _repeated_gamespeeds_codec = FieldCodec.ForMessage(26u, ChoosingTimeLimitgamespeedConfigure.Parser);

	private readonly RepeatedField<ChoosingTimeLimitgamespeedConfigure> gamespeeds_ = new RepeatedField<ChoosingTimeLimitgamespeedConfigure>();

	public const int GamespeedDictFieldNumber = 4;

	private static readonly MapField<int, ChoosingTimeLimitgamespeedConfigure>.Codec _map_gamespeedDict_codec = new MapField<int, ChoosingTimeLimitgamespeedConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChoosingTimeLimitgamespeedConfigure.Parser), 34u);

	private readonly MapField<int, ChoosingTimeLimitgamespeedConfigure> gamespeedDict_ = new MapField<int, ChoosingTimeLimitgamespeedConfigure>();

	public const int DifficultysFieldNumber = 5;

	private static readonly FieldCodec<ChoosingTimeLimitdifficultyConfigure> _repeated_difficultys_codec = FieldCodec.ForMessage(42u, ChoosingTimeLimitdifficultyConfigure.Parser);

	private readonly RepeatedField<ChoosingTimeLimitdifficultyConfigure> difficultys_ = new RepeatedField<ChoosingTimeLimitdifficultyConfigure>();

	public const int DifficultyDictFieldNumber = 6;

	private static readonly MapField<int, ChoosingTimeLimitdifficultyConfigure>.Codec _map_difficultyDict_codec = new MapField<int, ChoosingTimeLimitdifficultyConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChoosingTimeLimitdifficultyConfigure.Parser), 50u);

	private readonly MapField<int, ChoosingTimeLimitdifficultyConfigure> difficultyDict_ = new MapField<int, ChoosingTimeLimitdifficultyConfigure>();

	public const int RoomsettingsFieldNumber = 7;

	private static readonly FieldCodec<ChoosingTimeLimitroomsettingConfigure> _repeated_roomsettings_codec = FieldCodec.ForMessage(58u, ChoosingTimeLimitroomsettingConfigure.Parser);

	private readonly RepeatedField<ChoosingTimeLimitroomsettingConfigure> roomsettings_ = new RepeatedField<ChoosingTimeLimitroomsettingConfigure>();

	public const int RoomsettingDictFieldNumber = 8;

	private static readonly MapField<int, ChoosingTimeLimitroomsettingConfigure>.Codec _map_roomsettingDict_codec = new MapField<int, ChoosingTimeLimitroomsettingConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChoosingTimeLimitroomsettingConfigure.Parser), 66u);

	private readonly MapField<int, ChoosingTimeLimitroomsettingConfigure> roomsettingDict_ = new MapField<int, ChoosingTimeLimitroomsettingConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChoosingTimeLimitConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChoosingTimeLimitReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChoosingTimeLimitInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChoosingTimeLimitInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChoosingTimeLimitgamespeedConfigure> Gamespeeds => gamespeeds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChoosingTimeLimitgamespeedConfigure> GamespeedDict => gamespeedDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChoosingTimeLimitdifficultyConfigure> Difficultys => difficultys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChoosingTimeLimitdifficultyConfigure> DifficultyDict => difficultyDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChoosingTimeLimitroomsettingConfigure> Roomsettings => roomsettings_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChoosingTimeLimitroomsettingConfigure> RoomsettingDict => roomsettingDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitConfigure(ChoosingTimeLimitConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		gamespeeds_ = other.gamespeeds_.Clone();
		gamespeedDict_ = other.gamespeedDict_.Clone();
		difficultys_ = other.difficultys_.Clone();
		difficultyDict_ = other.difficultyDict_.Clone();
		roomsettings_ = other.roomsettings_.Clone();
		roomsettingDict_ = other.roomsettingDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitConfigure Clone()
	{
		return new ChoosingTimeLimitConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChoosingTimeLimitConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChoosingTimeLimitConfigure other)
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
		if (!gamespeeds_.Equals(other.gamespeeds_))
		{
			return false;
		}
		if (!GamespeedDict.Equals(other.GamespeedDict))
		{
			return false;
		}
		if (!difficultys_.Equals(other.difficultys_))
		{
			return false;
		}
		if (!DifficultyDict.Equals(other.DifficultyDict))
		{
			return false;
		}
		if (!roomsettings_.Equals(other.roomsettings_))
		{
			return false;
		}
		if (!RoomsettingDict.Equals(other.RoomsettingDict))
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
		num ^= gamespeeds_.GetHashCode();
		num ^= GamespeedDict.GetHashCode();
		num ^= difficultys_.GetHashCode();
		num ^= DifficultyDict.GetHashCode();
		num ^= roomsettings_.GetHashCode();
		num ^= RoomsettingDict.GetHashCode();
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
		gamespeeds_.WriteTo(ref output, _repeated_gamespeeds_codec);
		gamespeedDict_.WriteTo(ref output, _map_gamespeedDict_codec);
		difficultys_.WriteTo(ref output, _repeated_difficultys_codec);
		difficultyDict_.WriteTo(ref output, _map_difficultyDict_codec);
		roomsettings_.WriteTo(ref output, _repeated_roomsettings_codec);
		roomsettingDict_.WriteTo(ref output, _map_roomsettingDict_codec);
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
		num += gamespeeds_.CalculateSize(_repeated_gamespeeds_codec);
		num += gamespeedDict_.CalculateSize(_map_gamespeedDict_codec);
		num += difficultys_.CalculateSize(_repeated_difficultys_codec);
		num += difficultyDict_.CalculateSize(_map_difficultyDict_codec);
		num += roomsettings_.CalculateSize(_repeated_roomsettings_codec);
		num += roomsettingDict_.CalculateSize(_map_roomsettingDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChoosingTimeLimitConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			gamespeeds_.Add(other.gamespeeds_);
			gamespeedDict_.MergeFrom(other.gamespeedDict_);
			difficultys_.Add(other.difficultys_);
			difficultyDict_.MergeFrom(other.difficultyDict_);
			roomsettings_.Add(other.roomsettings_);
			roomsettingDict_.MergeFrom(other.roomsettingDict_);
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
				gamespeeds_.AddEntriesFrom(ref input, _repeated_gamespeeds_codec);
				break;
			case 34u:
				gamespeedDict_.AddEntriesFrom(ref input, _map_gamespeedDict_codec);
				break;
			case 42u:
				difficultys_.AddEntriesFrom(ref input, _repeated_difficultys_codec);
				break;
			case 50u:
				difficultyDict_.AddEntriesFrom(ref input, _map_difficultyDict_codec);
				break;
			case 58u:
				roomsettings_.AddEntriesFrom(ref input, _repeated_roomsettings_codec);
				break;
			case 66u:
				roomsettingDict_.AddEntriesFrom(ref input, _map_roomsettingDict_codec);
				break;
			}
		}
	}
}
