using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixSteamConfigure : IMessage<FixSteamConfigure>, IMessage, IEquatable<FixSteamConfigure>, IDeepCloneable<FixSteamConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixSteamConfigure> _parser = new MessageParser<FixSteamConfigure>(() => new FixSteamConfigure());

	private UnknownFieldSet _unknownFields;

	public const int Thank2588SFieldNumber = 1;

	private static readonly FieldCodec<FixSteamThank2588Configure> _repeated_thank2588S_codec = FieldCodec.ForMessage(10u, FixSteamThank2588Configure.Parser);

	private readonly RepeatedField<FixSteamThank2588Configure> thank2588S_ = new RepeatedField<FixSteamThank2588Configure>();

	public const int Thank2588DictFieldNumber = 2;

	private static readonly MapField<int, FixSteamThank2588Configure>.Codec _map_thank2588Dict_codec = new MapField<int, FixSteamThank2588Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank2588Configure.Parser), 18u);

	private readonly MapField<int, FixSteamThank2588Configure> thank2588Dict_ = new MapField<int, FixSteamThank2588Configure>();

	public const int Thank688SFieldNumber = 3;

	private static readonly FieldCodec<FixSteamThank688Configure> _repeated_thank688S_codec = FieldCodec.ForMessage(26u, FixSteamThank688Configure.Parser);

	private readonly RepeatedField<FixSteamThank688Configure> thank688S_ = new RepeatedField<FixSteamThank688Configure>();

	public const int Thank688DictFieldNumber = 4;

	private static readonly MapField<int, FixSteamThank688Configure>.Codec _map_thank688Dict_codec = new MapField<int, FixSteamThank688Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank688Configure.Parser), 34u);

	private readonly MapField<int, FixSteamThank688Configure> thank688Dict_ = new MapField<int, FixSteamThank688Configure>();

	public const int Thank388SFieldNumber = 5;

	private static readonly FieldCodec<FixSteamThank388Configure> _repeated_thank388S_codec = FieldCodec.ForMessage(42u, FixSteamThank388Configure.Parser);

	private readonly RepeatedField<FixSteamThank388Configure> thank388S_ = new RepeatedField<FixSteamThank388Configure>();

	public const int Thank388DictFieldNumber = 6;

	private static readonly MapField<int, FixSteamThank388Configure>.Codec _map_thank388Dict_codec = new MapField<int, FixSteamThank388Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank388Configure.Parser), 50u);

	private readonly MapField<int, FixSteamThank388Configure> thank388Dict_ = new MapField<int, FixSteamThank388Configure>();

	public const int Thank138SFieldNumber = 7;

	private static readonly FieldCodec<FixSteamThank138Configure> _repeated_thank138S_codec = FieldCodec.ForMessage(58u, FixSteamThank138Configure.Parser);

	private readonly RepeatedField<FixSteamThank138Configure> thank138S_ = new RepeatedField<FixSteamThank138Configure>();

	public const int Thank138DictFieldNumber = 8;

	private static readonly MapField<int, FixSteamThank138Configure>.Codec _map_thank138Dict_codec = new MapField<int, FixSteamThank138Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank138Configure.Parser), 66u);

	private readonly MapField<int, FixSteamThank138Configure> thank138Dict_ = new MapField<int, FixSteamThank138Configure>();

	public const int Thank68SFieldNumber = 9;

	private static readonly FieldCodec<FixSteamThank68Configure> _repeated_thank68S_codec = FieldCodec.ForMessage(74u, FixSteamThank68Configure.Parser);

	private readonly RepeatedField<FixSteamThank68Configure> thank68S_ = new RepeatedField<FixSteamThank68Configure>();

	public const int Thank68DictFieldNumber = 10;

	private static readonly MapField<int, FixSteamThank68Configure>.Codec _map_thank68Dict_codec = new MapField<int, FixSteamThank68Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank68Configure.Parser), 82u);

	private readonly MapField<int, FixSteamThank68Configure> thank68Dict_ = new MapField<int, FixSteamThank68Configure>();

	public const int Thank28SFieldNumber = 11;

	private static readonly FieldCodec<FixSteamThank28Configure> _repeated_thank28S_codec = FieldCodec.ForMessage(90u, FixSteamThank28Configure.Parser);

	private readonly RepeatedField<FixSteamThank28Configure> thank28S_ = new RepeatedField<FixSteamThank28Configure>();

	public const int Thank28DictFieldNumber = 12;

	private static readonly MapField<int, FixSteamThank28Configure>.Codec _map_thank28Dict_codec = new MapField<int, FixSteamThank28Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank28Configure.Parser), 98u);

	private readonly MapField<int, FixSteamThank28Configure> thank28Dict_ = new MapField<int, FixSteamThank28Configure>();

	public const int Thank0SFieldNumber = 13;

	private static readonly FieldCodec<FixSteamThank0Configure> _repeated_thank0S_codec = FieldCodec.ForMessage(106u, FixSteamThank0Configure.Parser);

	private readonly RepeatedField<FixSteamThank0Configure> thank0S_ = new RepeatedField<FixSteamThank0Configure>();

	public const int Thank0DictFieldNumber = 14;

	private static readonly MapField<int, FixSteamThank0Configure>.Codec _map_thank0Dict_codec = new MapField<int, FixSteamThank0Configure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixSteamThank0Configure.Parser), 114u);

	private readonly MapField<int, FixSteamThank0Configure> thank0Dict_ = new MapField<int, FixSteamThank0Configure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixSteamConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixSteamReflection.Descriptor.MessageTypes[7];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank2588Configure> Thank2588S => thank2588S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank2588Configure> Thank2588Dict => thank2588Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank688Configure> Thank688S => thank688S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank688Configure> Thank688Dict => thank688Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank388Configure> Thank388S => thank388S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank388Configure> Thank388Dict => thank388Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank138Configure> Thank138S => thank138S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank138Configure> Thank138Dict => thank138Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank68Configure> Thank68S => thank68S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank68Configure> Thank68Dict => thank68Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank28Configure> Thank28S => thank28S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank28Configure> Thank28Dict => thank28Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixSteamThank0Configure> Thank0S => thank0S_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixSteamThank0Configure> Thank0Dict => thank0Dict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixSteamConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixSteamConfigure(FixSteamConfigure other)
		: this()
	{
		thank2588S_ = other.thank2588S_.Clone();
		thank2588Dict_ = other.thank2588Dict_.Clone();
		thank688S_ = other.thank688S_.Clone();
		thank688Dict_ = other.thank688Dict_.Clone();
		thank388S_ = other.thank388S_.Clone();
		thank388Dict_ = other.thank388Dict_.Clone();
		thank138S_ = other.thank138S_.Clone();
		thank138Dict_ = other.thank138Dict_.Clone();
		thank68S_ = other.thank68S_.Clone();
		thank68Dict_ = other.thank68Dict_.Clone();
		thank28S_ = other.thank28S_.Clone();
		thank28Dict_ = other.thank28Dict_.Clone();
		thank0S_ = other.thank0S_.Clone();
		thank0Dict_ = other.thank0Dict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixSteamConfigure Clone()
	{
		return new FixSteamConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixSteamConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixSteamConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!thank2588S_.Equals(other.thank2588S_))
		{
			return false;
		}
		if (!Thank2588Dict.Equals(other.Thank2588Dict))
		{
			return false;
		}
		if (!thank688S_.Equals(other.thank688S_))
		{
			return false;
		}
		if (!Thank688Dict.Equals(other.Thank688Dict))
		{
			return false;
		}
		if (!thank388S_.Equals(other.thank388S_))
		{
			return false;
		}
		if (!Thank388Dict.Equals(other.Thank388Dict))
		{
			return false;
		}
		if (!thank138S_.Equals(other.thank138S_))
		{
			return false;
		}
		if (!Thank138Dict.Equals(other.Thank138Dict))
		{
			return false;
		}
		if (!thank68S_.Equals(other.thank68S_))
		{
			return false;
		}
		if (!Thank68Dict.Equals(other.Thank68Dict))
		{
			return false;
		}
		if (!thank28S_.Equals(other.thank28S_))
		{
			return false;
		}
		if (!Thank28Dict.Equals(other.Thank28Dict))
		{
			return false;
		}
		if (!thank0S_.Equals(other.thank0S_))
		{
			return false;
		}
		if (!Thank0Dict.Equals(other.Thank0Dict))
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
		num ^= thank2588S_.GetHashCode();
		num ^= Thank2588Dict.GetHashCode();
		num ^= thank688S_.GetHashCode();
		num ^= Thank688Dict.GetHashCode();
		num ^= thank388S_.GetHashCode();
		num ^= Thank388Dict.GetHashCode();
		num ^= thank138S_.GetHashCode();
		num ^= Thank138Dict.GetHashCode();
		num ^= thank68S_.GetHashCode();
		num ^= Thank68Dict.GetHashCode();
		num ^= thank28S_.GetHashCode();
		num ^= Thank28Dict.GetHashCode();
		num ^= thank0S_.GetHashCode();
		num ^= Thank0Dict.GetHashCode();
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
		thank2588S_.WriteTo(ref output, _repeated_thank2588S_codec);
		thank2588Dict_.WriteTo(ref output, _map_thank2588Dict_codec);
		thank688S_.WriteTo(ref output, _repeated_thank688S_codec);
		thank688Dict_.WriteTo(ref output, _map_thank688Dict_codec);
		thank388S_.WriteTo(ref output, _repeated_thank388S_codec);
		thank388Dict_.WriteTo(ref output, _map_thank388Dict_codec);
		thank138S_.WriteTo(ref output, _repeated_thank138S_codec);
		thank138Dict_.WriteTo(ref output, _map_thank138Dict_codec);
		thank68S_.WriteTo(ref output, _repeated_thank68S_codec);
		thank68Dict_.WriteTo(ref output, _map_thank68Dict_codec);
		thank28S_.WriteTo(ref output, _repeated_thank28S_codec);
		thank28Dict_.WriteTo(ref output, _map_thank28Dict_codec);
		thank0S_.WriteTo(ref output, _repeated_thank0S_codec);
		thank0Dict_.WriteTo(ref output, _map_thank0Dict_codec);
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
		num += thank2588S_.CalculateSize(_repeated_thank2588S_codec);
		num += thank2588Dict_.CalculateSize(_map_thank2588Dict_codec);
		num += thank688S_.CalculateSize(_repeated_thank688S_codec);
		num += thank688Dict_.CalculateSize(_map_thank688Dict_codec);
		num += thank388S_.CalculateSize(_repeated_thank388S_codec);
		num += thank388Dict_.CalculateSize(_map_thank388Dict_codec);
		num += thank138S_.CalculateSize(_repeated_thank138S_codec);
		num += thank138Dict_.CalculateSize(_map_thank138Dict_codec);
		num += thank68S_.CalculateSize(_repeated_thank68S_codec);
		num += thank68Dict_.CalculateSize(_map_thank68Dict_codec);
		num += thank28S_.CalculateSize(_repeated_thank28S_codec);
		num += thank28Dict_.CalculateSize(_map_thank28Dict_codec);
		num += thank0S_.CalculateSize(_repeated_thank0S_codec);
		num += thank0Dict_.CalculateSize(_map_thank0Dict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixSteamConfigure other)
	{
		if (other != null)
		{
			thank2588S_.Add(other.thank2588S_);
			thank2588Dict_.MergeFrom(other.thank2588Dict_);
			thank688S_.Add(other.thank688S_);
			thank688Dict_.MergeFrom(other.thank688Dict_);
			thank388S_.Add(other.thank388S_);
			thank388Dict_.MergeFrom(other.thank388Dict_);
			thank138S_.Add(other.thank138S_);
			thank138Dict_.MergeFrom(other.thank138Dict_);
			thank68S_.Add(other.thank68S_);
			thank68Dict_.MergeFrom(other.thank68Dict_);
			thank28S_.Add(other.thank28S_);
			thank28Dict_.MergeFrom(other.thank28Dict_);
			thank0S_.Add(other.thank0S_);
			thank0Dict_.MergeFrom(other.thank0Dict_);
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
				thank2588S_.AddEntriesFrom(ref input, _repeated_thank2588S_codec);
				break;
			case 18u:
				thank2588Dict_.AddEntriesFrom(ref input, _map_thank2588Dict_codec);
				break;
			case 26u:
				thank688S_.AddEntriesFrom(ref input, _repeated_thank688S_codec);
				break;
			case 34u:
				thank688Dict_.AddEntriesFrom(ref input, _map_thank688Dict_codec);
				break;
			case 42u:
				thank388S_.AddEntriesFrom(ref input, _repeated_thank388S_codec);
				break;
			case 50u:
				thank388Dict_.AddEntriesFrom(ref input, _map_thank388Dict_codec);
				break;
			case 58u:
				thank138S_.AddEntriesFrom(ref input, _repeated_thank138S_codec);
				break;
			case 66u:
				thank138Dict_.AddEntriesFrom(ref input, _map_thank138Dict_codec);
				break;
			case 74u:
				thank68S_.AddEntriesFrom(ref input, _repeated_thank68S_codec);
				break;
			case 82u:
				thank68Dict_.AddEntriesFrom(ref input, _map_thank68Dict_codec);
				break;
			case 90u:
				thank28S_.AddEntriesFrom(ref input, _repeated_thank28S_codec);
				break;
			case 98u:
				thank28Dict_.AddEntriesFrom(ref input, _map_thank28Dict_codec);
				break;
			case 106u:
				thank0S_.AddEntriesFrom(ref input, _repeated_thank0S_codec);
				break;
			case 114u:
				thank0Dict_.AddEntriesFrom(ref input, _map_thank0Dict_codec);
				break;
			}
		}
	}
}
