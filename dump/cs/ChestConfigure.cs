using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChestConfigure : IMessage<ChestConfigure>, IMessage, IEquatable<ChestConfigure>, IDeepCloneable<ChestConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChestConfigure> _parser = new MessageParser<ChestConfigure>(() => new ChestConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<ChestInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, ChestInfoConfigure.Parser);

	private readonly RepeatedField<ChestInfoConfigure> infos_ = new RepeatedField<ChestInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, ChestInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, ChestInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChestInfoConfigure.Parser), 18u);

	private readonly MapField<int, ChestInfoConfigure> infoDict_ = new MapField<int, ChestInfoConfigure>();

	public const int RandomRewardsFieldNumber = 3;

	private static readonly FieldCodec<ChestRandomRewardConfigure> _repeated_randomRewards_codec = FieldCodec.ForMessage(26u, ChestRandomRewardConfigure.Parser);

	private readonly RepeatedField<ChestRandomRewardConfigure> randomRewards_ = new RepeatedField<ChestRandomRewardConfigure>();

	public const int RandomRewardDictFieldNumber = 4;

	private static readonly MapField<int, ChestRandomRewardConfigure>.Codec _map_randomRewardDict_codec = new MapField<int, ChestRandomRewardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChestRandomRewardConfigure.Parser), 34u);

	private readonly MapField<int, ChestRandomRewardConfigure> randomRewardDict_ = new MapField<int, ChestRandomRewardConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChestConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChestReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChestInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChestInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChestRandomRewardConfigure> RandomRewards => randomRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChestRandomRewardConfigure> RandomRewardDict => randomRewardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestConfigure(ChestConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		randomRewards_ = other.randomRewards_.Clone();
		randomRewardDict_ = other.randomRewardDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestConfigure Clone()
	{
		return new ChestConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChestConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChestConfigure other)
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
		if (!randomRewards_.Equals(other.randomRewards_))
		{
			return false;
		}
		if (!RandomRewardDict.Equals(other.RandomRewardDict))
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
		num ^= randomRewards_.GetHashCode();
		num ^= RandomRewardDict.GetHashCode();
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
		randomRewards_.WriteTo(ref output, _repeated_randomRewards_codec);
		randomRewardDict_.WriteTo(ref output, _map_randomRewardDict_codec);
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
		num += randomRewards_.CalculateSize(_repeated_randomRewards_codec);
		num += randomRewardDict_.CalculateSize(_map_randomRewardDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChestConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			randomRewards_.Add(other.randomRewards_);
			randomRewardDict_.MergeFrom(other.randomRewardDict_);
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
				randomRewards_.AddEntriesFrom(ref input, _repeated_randomRewards_codec);
				break;
			case 34u:
				randomRewardDict_.AddEntriesFrom(ref input, _map_randomRewardDict_codec);
				break;
			}
		}
	}
}
