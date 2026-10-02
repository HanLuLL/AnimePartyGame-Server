using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GachaConfigure : IMessage<GachaConfigure>, IMessage, IEquatable<GachaConfigure>, IDeepCloneable<GachaConfigure>, IBufferMessage
{
	private static readonly MessageParser<GachaConfigure> _parser = new MessageParser<GachaConfigure>(() => new GachaConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BackstagesFieldNumber = 1;

	private static readonly FieldCodec<GachaBackstageConfigure> _repeated_backstages_codec = FieldCodec.ForMessage(10u, GachaBackstageConfigure.Parser);

	private readonly RepeatedField<GachaBackstageConfigure> backstages_ = new RepeatedField<GachaBackstageConfigure>();

	public const int BackstageDictFieldNumber = 2;

	private static readonly MapField<int, GachaBackstageConfigure>.Codec _map_backstageDict_codec = new MapField<int, GachaBackstageConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GachaBackstageConfigure.Parser), 18u);

	private readonly MapField<int, GachaBackstageConfigure> backstageDict_ = new MapField<int, GachaBackstageConfigure>();

	public const int PoolsFieldNumber = 3;

	private static readonly FieldCodec<GachaPoolConfigure> _repeated_pools_codec = FieldCodec.ForMessage(26u, GachaPoolConfigure.Parser);

	private readonly RepeatedField<GachaPoolConfigure> pools_ = new RepeatedField<GachaPoolConfigure>();

	public const int PoolDictFieldNumber = 4;

	private static readonly MapField<int, GachaPoolConfigure>.Codec _map_poolDict_codec = new MapField<int, GachaPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GachaPoolConfigure.Parser), 34u);

	private readonly MapField<int, GachaPoolConfigure> poolDict_ = new MapField<int, GachaPoolConfigure>();

	public const int CombsFieldNumber = 5;

	private static readonly FieldCodec<GachaCombConfigure> _repeated_combs_codec = FieldCodec.ForMessage(42u, GachaCombConfigure.Parser);

	private readonly RepeatedField<GachaCombConfigure> combs_ = new RepeatedField<GachaCombConfigure>();

	public const int CombDictFieldNumber = 6;

	private static readonly MapField<int, GachaCombConfigure>.Codec _map_combDict_codec = new MapField<int, GachaCombConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GachaCombConfigure.Parser), 50u);

	private readonly MapField<int, GachaCombConfigure> combDict_ = new MapField<int, GachaCombConfigure>();

	public const int GroupsFieldNumber = 7;

	private static readonly FieldCodec<GachaGroupConfigure> _repeated_groups_codec = FieldCodec.ForMessage(58u, GachaGroupConfigure.Parser);

	private readonly RepeatedField<GachaGroupConfigure> groups_ = new RepeatedField<GachaGroupConfigure>();

	public const int GroupDictFieldNumber = 8;

	private static readonly MapField<int, GachaGroupConfigure>.Codec _map_groupDict_codec = new MapField<int, GachaGroupConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GachaGroupConfigure.Parser), 66u);

	private readonly MapField<int, GachaGroupConfigure> groupDict_ = new MapField<int, GachaGroupConfigure>();

	public const int ProgresssFieldNumber = 9;

	private static readonly FieldCodec<GachaProgressConfigure> _repeated_progresss_codec = FieldCodec.ForMessage(74u, GachaProgressConfigure.Parser);

	private readonly RepeatedField<GachaProgressConfigure> progresss_ = new RepeatedField<GachaProgressConfigure>();

	public const int ProgressDictFieldNumber = 10;

	private static readonly MapField<int, GachaProgressConfigure>.Codec _map_progressDict_codec = new MapField<int, GachaProgressConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GachaProgressConfigure.Parser), 82u);

	private readonly MapField<int, GachaProgressConfigure> progressDict_ = new MapField<int, GachaProgressConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[8];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaBackstageConfigure> Backstages => backstages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaBackstageConfigure> BackstageDict => backstageDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaPoolConfigure> Pools => pools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaPoolConfigure> PoolDict => poolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaCombConfigure> Combs => combs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaCombConfigure> CombDict => combDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaGroupConfigure> Groups => groups_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaGroupConfigure> GroupDict => groupDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaProgressConfigure> Progresss => progresss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaProgressConfigure> ProgressDict => progressDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaConfigure(GachaConfigure other)
		: this()
	{
		backstages_ = other.backstages_.Clone();
		backstageDict_ = other.backstageDict_.Clone();
		pools_ = other.pools_.Clone();
		poolDict_ = other.poolDict_.Clone();
		combs_ = other.combs_.Clone();
		combDict_ = other.combDict_.Clone();
		groups_ = other.groups_.Clone();
		groupDict_ = other.groupDict_.Clone();
		progresss_ = other.progresss_.Clone();
		progressDict_ = other.progressDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaConfigure Clone()
	{
		return new GachaConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!backstages_.Equals(other.backstages_))
		{
			return false;
		}
		if (!BackstageDict.Equals(other.BackstageDict))
		{
			return false;
		}
		if (!pools_.Equals(other.pools_))
		{
			return false;
		}
		if (!PoolDict.Equals(other.PoolDict))
		{
			return false;
		}
		if (!combs_.Equals(other.combs_))
		{
			return false;
		}
		if (!CombDict.Equals(other.CombDict))
		{
			return false;
		}
		if (!groups_.Equals(other.groups_))
		{
			return false;
		}
		if (!GroupDict.Equals(other.GroupDict))
		{
			return false;
		}
		if (!progresss_.Equals(other.progresss_))
		{
			return false;
		}
		if (!ProgressDict.Equals(other.ProgressDict))
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
		num ^= backstages_.GetHashCode();
		num ^= BackstageDict.GetHashCode();
		num ^= pools_.GetHashCode();
		num ^= PoolDict.GetHashCode();
		num ^= combs_.GetHashCode();
		num ^= CombDict.GetHashCode();
		num ^= groups_.GetHashCode();
		num ^= GroupDict.GetHashCode();
		num ^= progresss_.GetHashCode();
		num ^= ProgressDict.GetHashCode();
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
		backstages_.WriteTo(ref output, _repeated_backstages_codec);
		backstageDict_.WriteTo(ref output, _map_backstageDict_codec);
		pools_.WriteTo(ref output, _repeated_pools_codec);
		poolDict_.WriteTo(ref output, _map_poolDict_codec);
		combs_.WriteTo(ref output, _repeated_combs_codec);
		combDict_.WriteTo(ref output, _map_combDict_codec);
		groups_.WriteTo(ref output, _repeated_groups_codec);
		groupDict_.WriteTo(ref output, _map_groupDict_codec);
		progresss_.WriteTo(ref output, _repeated_progresss_codec);
		progressDict_.WriteTo(ref output, _map_progressDict_codec);
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
		num += backstages_.CalculateSize(_repeated_backstages_codec);
		num += backstageDict_.CalculateSize(_map_backstageDict_codec);
		num += pools_.CalculateSize(_repeated_pools_codec);
		num += poolDict_.CalculateSize(_map_poolDict_codec);
		num += combs_.CalculateSize(_repeated_combs_codec);
		num += combDict_.CalculateSize(_map_combDict_codec);
		num += groups_.CalculateSize(_repeated_groups_codec);
		num += groupDict_.CalculateSize(_map_groupDict_codec);
		num += progresss_.CalculateSize(_repeated_progresss_codec);
		num += progressDict_.CalculateSize(_map_progressDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GachaConfigure other)
	{
		if (other != null)
		{
			backstages_.Add(other.backstages_);
			backstageDict_.MergeFrom(other.backstageDict_);
			pools_.Add(other.pools_);
			poolDict_.MergeFrom(other.poolDict_);
			combs_.Add(other.combs_);
			combDict_.MergeFrom(other.combDict_);
			groups_.Add(other.groups_);
			groupDict_.MergeFrom(other.groupDict_);
			progresss_.Add(other.progresss_);
			progressDict_.MergeFrom(other.progressDict_);
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
				backstages_.AddEntriesFrom(ref input, _repeated_backstages_codec);
				break;
			case 18u:
				backstageDict_.AddEntriesFrom(ref input, _map_backstageDict_codec);
				break;
			case 26u:
				pools_.AddEntriesFrom(ref input, _repeated_pools_codec);
				break;
			case 34u:
				poolDict_.AddEntriesFrom(ref input, _map_poolDict_codec);
				break;
			case 42u:
				combs_.AddEntriesFrom(ref input, _repeated_combs_codec);
				break;
			case 50u:
				combDict_.AddEntriesFrom(ref input, _map_combDict_codec);
				break;
			case 58u:
				groups_.AddEntriesFrom(ref input, _repeated_groups_codec);
				break;
			case 66u:
				groupDict_.AddEntriesFrom(ref input, _map_groupDict_codec);
				break;
			case 74u:
				progresss_.AddEntriesFrom(ref input, _repeated_progresss_codec);
				break;
			case 82u:
				progressDict_.AddEntriesFrom(ref input, _map_progressDict_codec);
				break;
			}
		}
	}
}
