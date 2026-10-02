using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MatchConfigure : IMessage<MatchConfigure>, IMessage, IEquatable<MatchConfigure>, IDeepCloneable<MatchConfigure>, IBufferMessage
{
	private static readonly MessageParser<MatchConfigure> _parser = new MessageParser<MatchConfigure>(() => new MatchConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<MatchInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, MatchInfoConfigure.Parser);

	private readonly RepeatedField<MatchInfoConfigure> infos_ = new RepeatedField<MatchInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, MatchInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, MatchInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MatchInfoConfigure.Parser), 18u);

	private readonly MapField<int, MatchInfoConfigure> infoDict_ = new MapField<int, MatchInfoConfigure>();

	public const int ParmssFieldNumber = 3;

	private static readonly FieldCodec<MatchParmsConfigure> _repeated_parmss_codec = FieldCodec.ForMessage(26u, MatchParmsConfigure.Parser);

	private readonly RepeatedField<MatchParmsConfigure> parmss_ = new RepeatedField<MatchParmsConfigure>();

	public const int ParmsDictFieldNumber = 4;

	private static readonly MapField<int, MatchParmsConfigure>.Codec _map_parmsDict_codec = new MapField<int, MatchParmsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MatchParmsConfigure.Parser), 34u);

	private readonly MapField<int, MatchParmsConfigure> parmsDict_ = new MapField<int, MatchParmsConfigure>();

	public const int CreditTiersFieldNumber = 5;

	private static readonly FieldCodec<MatchCreditTierConfigure> _repeated_creditTiers_codec = FieldCodec.ForMessage(42u, MatchCreditTierConfigure.Parser);

	private readonly RepeatedField<MatchCreditTierConfigure> creditTiers_ = new RepeatedField<MatchCreditTierConfigure>();

	public const int CreditTierDictFieldNumber = 6;

	private static readonly MapField<int, MatchCreditTierConfigure>.Codec _map_creditTierDict_codec = new MapField<int, MatchCreditTierConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MatchCreditTierConfigure.Parser), 50u);

	private readonly MapField<int, MatchCreditTierConfigure> creditTierDict_ = new MapField<int, MatchCreditTierConfigure>();

	public const int CreditActionsFieldNumber = 7;

	private static readonly FieldCodec<MatchCreditActionConfigure> _repeated_creditActions_codec = FieldCodec.ForMessage(58u, MatchCreditActionConfigure.Parser);

	private readonly RepeatedField<MatchCreditActionConfigure> creditActions_ = new RepeatedField<MatchCreditActionConfigure>();

	public const int CreditActionDictFieldNumber = 8;

	private static readonly MapField<int, MatchCreditActionConfigure>.Codec _map_creditActionDict_codec = new MapField<int, MatchCreditActionConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MatchCreditActionConfigure.Parser), 66u);

	private readonly MapField<int, MatchCreditActionConfigure> creditActionDict_ = new MapField<int, MatchCreditActionConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MatchReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MatchInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MatchInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MatchParmsConfigure> Parmss => parmss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MatchParmsConfigure> ParmsDict => parmsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MatchCreditTierConfigure> CreditTiers => creditTiers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MatchCreditTierConfigure> CreditTierDict => creditTierDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MatchCreditActionConfigure> CreditActions => creditActions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MatchCreditActionConfigure> CreditActionDict => creditActionDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchConfigure(MatchConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		parmss_ = other.parmss_.Clone();
		parmsDict_ = other.parmsDict_.Clone();
		creditTiers_ = other.creditTiers_.Clone();
		creditTierDict_ = other.creditTierDict_.Clone();
		creditActions_ = other.creditActions_.Clone();
		creditActionDict_ = other.creditActionDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchConfigure Clone()
	{
		return new MatchConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchConfigure other)
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
		if (!parmss_.Equals(other.parmss_))
		{
			return false;
		}
		if (!ParmsDict.Equals(other.ParmsDict))
		{
			return false;
		}
		if (!creditTiers_.Equals(other.creditTiers_))
		{
			return false;
		}
		if (!CreditTierDict.Equals(other.CreditTierDict))
		{
			return false;
		}
		if (!creditActions_.Equals(other.creditActions_))
		{
			return false;
		}
		if (!CreditActionDict.Equals(other.CreditActionDict))
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
		num ^= parmss_.GetHashCode();
		num ^= ParmsDict.GetHashCode();
		num ^= creditTiers_.GetHashCode();
		num ^= CreditTierDict.GetHashCode();
		num ^= creditActions_.GetHashCode();
		num ^= CreditActionDict.GetHashCode();
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
		parmss_.WriteTo(ref output, _repeated_parmss_codec);
		parmsDict_.WriteTo(ref output, _map_parmsDict_codec);
		creditTiers_.WriteTo(ref output, _repeated_creditTiers_codec);
		creditTierDict_.WriteTo(ref output, _map_creditTierDict_codec);
		creditActions_.WriteTo(ref output, _repeated_creditActions_codec);
		creditActionDict_.WriteTo(ref output, _map_creditActionDict_codec);
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
		num += parmss_.CalculateSize(_repeated_parmss_codec);
		num += parmsDict_.CalculateSize(_map_parmsDict_codec);
		num += creditTiers_.CalculateSize(_repeated_creditTiers_codec);
		num += creditTierDict_.CalculateSize(_map_creditTierDict_codec);
		num += creditActions_.CalculateSize(_repeated_creditActions_codec);
		num += creditActionDict_.CalculateSize(_map_creditActionDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MatchConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			parmss_.Add(other.parmss_);
			parmsDict_.MergeFrom(other.parmsDict_);
			creditTiers_.Add(other.creditTiers_);
			creditTierDict_.MergeFrom(other.creditTierDict_);
			creditActions_.Add(other.creditActions_);
			creditActionDict_.MergeFrom(other.creditActionDict_);
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
				parmss_.AddEntriesFrom(ref input, _repeated_parmss_codec);
				break;
			case 34u:
				parmsDict_.AddEntriesFrom(ref input, _map_parmsDict_codec);
				break;
			case 42u:
				creditTiers_.AddEntriesFrom(ref input, _repeated_creditTiers_codec);
				break;
			case 50u:
				creditTierDict_.AddEntriesFrom(ref input, _map_creditTierDict_codec);
				break;
			case 58u:
				creditActions_.AddEntriesFrom(ref input, _repeated_creditActions_codec);
				break;
			case 66u:
				creditActionDict_.AddEntriesFrom(ref input, _map_creditActionDict_codec);
				break;
			}
		}
	}

	public void Fix(FixMatchConfigure fixMatch)
	{
		if (fixMatch == null || fixMatch.InfoDict == null || fixMatch.InfoDict.Count <= 0)
		{
			return;
		}
		for (int num = infos_.Count - 1; num >= 0; num--)
		{
			if (fixMatch.InfoDict.TryGetValue(infos_[num].Id, out var value))
			{
				infoDict_[infos_[num].Id].Maps.Clear();
				infoDict_[infos_[num].Id].Maps.AddRange(value.Maps);
				infos_[num].Maps.Clear();
				infos_[num].Maps.AddRange(value.Maps);
			}
		}
	}
}
