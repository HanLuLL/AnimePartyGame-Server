using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class AudioConfigure : IMessage<AudioConfigure>, IMessage, IEquatable<AudioConfigure>, IDeepCloneable<AudioConfigure>, IBufferMessage
{
	private static readonly MessageParser<AudioConfigure> _parser = new MessageParser<AudioConfigure>(() => new AudioConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BanksFieldNumber = 1;

	private static readonly FieldCodec<AudioBankConfigure> _repeated_banks_codec = FieldCodec.ForMessage(10u, AudioBankConfigure.Parser);

	private readonly RepeatedField<AudioBankConfigure> banks_ = new RepeatedField<AudioBankConfigure>();

	public const int BankDictFieldNumber = 2;

	private static readonly MapField<int, AudioBankConfigure>.Codec _map_bankDict_codec = new MapField<int, AudioBankConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AudioBankConfigure.Parser), 18u);

	private readonly MapField<int, AudioBankConfigure> bankDict_ = new MapField<int, AudioBankConfigure>();

	public const int EventsFieldNumber = 3;

	private static readonly FieldCodec<AudioEventConfigure> _repeated_events_codec = FieldCodec.ForMessage(26u, AudioEventConfigure.Parser);

	private readonly RepeatedField<AudioEventConfigure> events_ = new RepeatedField<AudioEventConfigure>();

	public const int EventDictFieldNumber = 4;

	private static readonly MapField<int, AudioEventConfigure>.Codec _map_eventDict_codec = new MapField<int, AudioEventConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AudioEventConfigure.Parser), 34u);

	private readonly MapField<int, AudioEventConfigure> eventDict_ = new MapField<int, AudioEventConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AudioConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AudioReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AudioBankConfigure> Banks => banks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AudioBankConfigure> BankDict => bankDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AudioEventConfigure> Events => events_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AudioEventConfigure> EventDict => eventDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AudioConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AudioConfigure(AudioConfigure other)
		: this()
	{
		banks_ = other.banks_.Clone();
		bankDict_ = other.bankDict_.Clone();
		events_ = other.events_.Clone();
		eventDict_ = other.eventDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AudioConfigure Clone()
	{
		return new AudioConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AudioConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AudioConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!banks_.Equals(other.banks_))
		{
			return false;
		}
		if (!BankDict.Equals(other.BankDict))
		{
			return false;
		}
		if (!events_.Equals(other.events_))
		{
			return false;
		}
		if (!EventDict.Equals(other.EventDict))
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
		num ^= banks_.GetHashCode();
		num ^= BankDict.GetHashCode();
		num ^= events_.GetHashCode();
		num ^= EventDict.GetHashCode();
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
		banks_.WriteTo(ref output, _repeated_banks_codec);
		bankDict_.WriteTo(ref output, _map_bankDict_codec);
		events_.WriteTo(ref output, _repeated_events_codec);
		eventDict_.WriteTo(ref output, _map_eventDict_codec);
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
		num += banks_.CalculateSize(_repeated_banks_codec);
		num += bankDict_.CalculateSize(_map_bankDict_codec);
		num += events_.CalculateSize(_repeated_events_codec);
		num += eventDict_.CalculateSize(_map_eventDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AudioConfigure other)
	{
		if (other != null)
		{
			banks_.Add(other.banks_);
			bankDict_.MergeFrom(other.bankDict_);
			events_.Add(other.events_);
			eventDict_.MergeFrom(other.eventDict_);
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
				banks_.AddEntriesFrom(ref input, _repeated_banks_codec);
				break;
			case 18u:
				bankDict_.AddEntriesFrom(ref input, _map_bankDict_codec);
				break;
			case 26u:
				events_.AddEntriesFrom(ref input, _repeated_events_codec);
				break;
			case 34u:
				eventDict_.AddEntriesFrom(ref input, _map_eventDict_codec);
				break;
			}
		}
	}
}
