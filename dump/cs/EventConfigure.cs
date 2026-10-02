using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class EventConfigure : IMessage<EventConfigure>, IMessage, IEquatable<EventConfigure>, IDeepCloneable<EventConfigure>, IBufferMessage
{
	private static readonly MessageParser<EventConfigure> _parser = new MessageParser<EventConfigure>(() => new EventConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<EventInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, EventInfoConfigure.Parser);

	private readonly RepeatedField<EventInfoConfigure> infos_ = new RepeatedField<EventInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, EventInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, EventInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, EventInfoConfigure.Parser), 18u);

	private readonly MapField<int, EventInfoConfigure> infoDict_ = new MapField<int, EventInfoConfigure>();

	public const int PeriodsFieldNumber = 3;

	private static readonly FieldCodec<EventPeriodConfigure> _repeated_periods_codec = FieldCodec.ForMessage(26u, EventPeriodConfigure.Parser);

	private readonly RepeatedField<EventPeriodConfigure> periods_ = new RepeatedField<EventPeriodConfigure>();

	public const int PeriodDictFieldNumber = 4;

	private static readonly MapField<int, EventPeriodConfigure>.Codec _map_periodDict_codec = new MapField<int, EventPeriodConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, EventPeriodConfigure.Parser), 34u);

	private readonly MapField<int, EventPeriodConfigure> periodDict_ = new MapField<int, EventPeriodConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<EventConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => EventReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<EventInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, EventInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<EventPeriodConfigure> Periods => periods_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, EventPeriodConfigure> PeriodDict => periodDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventConfigure(EventConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		periods_ = other.periods_.Clone();
		periodDict_ = other.periodDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventConfigure Clone()
	{
		return new EventConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as EventConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(EventConfigure other)
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
		if (!periods_.Equals(other.periods_))
		{
			return false;
		}
		if (!PeriodDict.Equals(other.PeriodDict))
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
		num ^= periods_.GetHashCode();
		num ^= PeriodDict.GetHashCode();
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
		periods_.WriteTo(ref output, _repeated_periods_codec);
		periodDict_.WriteTo(ref output, _map_periodDict_codec);
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
		num += periods_.CalculateSize(_repeated_periods_codec);
		num += periodDict_.CalculateSize(_map_periodDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(EventConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			periods_.Add(other.periods_);
			periodDict_.MergeFrom(other.periodDict_);
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
				periods_.AddEntriesFrom(ref input, _repeated_periods_codec);
				break;
			case 34u:
				periodDict_.AddEntriesFrom(ref input, _map_periodDict_codec);
				break;
			}
		}
	}
}
