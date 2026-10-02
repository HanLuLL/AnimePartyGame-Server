using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PerformConfigure : IMessage<PerformConfigure>, IMessage, IEquatable<PerformConfigure>, IDeepCloneable<PerformConfigure>, IBufferMessage
{
	private static readonly MessageParser<PerformConfigure> _parser = new MessageParser<PerformConfigure>(() => new PerformConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<PerformInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, PerformInfoConfigure.Parser);

	private readonly RepeatedField<PerformInfoConfigure> infos_ = new RepeatedField<PerformInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, PerformInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, PerformInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PerformInfoConfigure.Parser), 18u);

	private readonly MapField<int, PerformInfoConfigure> infoDict_ = new MapField<int, PerformInfoConfigure>();

	public const int TriggersFieldNumber = 3;

	private static readonly FieldCodec<PerformTriggerConfigure> _repeated_triggers_codec = FieldCodec.ForMessage(26u, PerformTriggerConfigure.Parser);

	private readonly RepeatedField<PerformTriggerConfigure> triggers_ = new RepeatedField<PerformTriggerConfigure>();

	public const int TriggerDictFieldNumber = 4;

	private static readonly MapField<int, PerformTriggerConfigure>.Codec _map_triggerDict_codec = new MapField<int, PerformTriggerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PerformTriggerConfigure.Parser), 34u);

	private readonly MapField<int, PerformTriggerConfigure> triggerDict_ = new MapField<int, PerformTriggerConfigure>();

	public const int TriggerSetsFieldNumber = 5;

	private static readonly FieldCodec<PerformTriggerSetConfigure> _repeated_triggerSets_codec = FieldCodec.ForMessage(42u, PerformTriggerSetConfigure.Parser);

	private readonly RepeatedField<PerformTriggerSetConfigure> triggerSets_ = new RepeatedField<PerformTriggerSetConfigure>();

	public const int TriggerSetDictFieldNumber = 6;

	private static readonly MapField<int, PerformTriggerSetConfigure>.Codec _map_triggerSetDict_codec = new MapField<int, PerformTriggerSetConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PerformTriggerSetConfigure.Parser), 50u);

	private readonly MapField<int, PerformTriggerSetConfigure> triggerSetDict_ = new MapField<int, PerformTriggerSetConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PerformConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PerformReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PerformInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PerformInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PerformTriggerConfigure> Triggers => triggers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PerformTriggerConfigure> TriggerDict => triggerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PerformTriggerSetConfigure> TriggerSets => triggerSets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PerformTriggerSetConfigure> TriggerSetDict => triggerSetDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformConfigure(PerformConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		triggers_ = other.triggers_.Clone();
		triggerDict_ = other.triggerDict_.Clone();
		triggerSets_ = other.triggerSets_.Clone();
		triggerSetDict_ = other.triggerSetDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformConfigure Clone()
	{
		return new PerformConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PerformConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PerformConfigure other)
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
		if (!triggers_.Equals(other.triggers_))
		{
			return false;
		}
		if (!TriggerDict.Equals(other.TriggerDict))
		{
			return false;
		}
		if (!triggerSets_.Equals(other.triggerSets_))
		{
			return false;
		}
		if (!TriggerSetDict.Equals(other.TriggerSetDict))
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
		num ^= triggers_.GetHashCode();
		num ^= TriggerDict.GetHashCode();
		num ^= triggerSets_.GetHashCode();
		num ^= TriggerSetDict.GetHashCode();
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
		triggers_.WriteTo(ref output, _repeated_triggers_codec);
		triggerDict_.WriteTo(ref output, _map_triggerDict_codec);
		triggerSets_.WriteTo(ref output, _repeated_triggerSets_codec);
		triggerSetDict_.WriteTo(ref output, _map_triggerSetDict_codec);
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
		num += triggers_.CalculateSize(_repeated_triggers_codec);
		num += triggerDict_.CalculateSize(_map_triggerDict_codec);
		num += triggerSets_.CalculateSize(_repeated_triggerSets_codec);
		num += triggerSetDict_.CalculateSize(_map_triggerSetDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PerformConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			triggers_.Add(other.triggers_);
			triggerDict_.MergeFrom(other.triggerDict_);
			triggerSets_.Add(other.triggerSets_);
			triggerSetDict_.MergeFrom(other.triggerSetDict_);
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
				triggers_.AddEntriesFrom(ref input, _repeated_triggers_codec);
				break;
			case 34u:
				triggerDict_.AddEntriesFrom(ref input, _map_triggerDict_codec);
				break;
			case 42u:
				triggerSets_.AddEntriesFrom(ref input, _repeated_triggerSets_codec);
				break;
			case 50u:
				triggerSetDict_.AddEntriesFrom(ref input, _map_triggerSetDict_codec);
				break;
			}
		}
	}
}
