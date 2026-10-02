using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PVEMissionConfigure : IMessage<PVEMissionConfigure>, IMessage, IEquatable<PVEMissionConfigure>, IDeepCloneable<PVEMissionConfigure>, IBufferMessage
{
	private static readonly MessageParser<PVEMissionConfigure> _parser = new MessageParser<PVEMissionConfigure>(() => new PVEMissionConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<PVEMissionInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, PVEMissionInfoConfigure.Parser);

	private readonly RepeatedField<PVEMissionInfoConfigure> infos_ = new RepeatedField<PVEMissionInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, PVEMissionInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, PVEMissionInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionInfoConfigure.Parser), 18u);

	private readonly MapField<int, PVEMissionInfoConfigure> infoDict_ = new MapField<int, PVEMissionInfoConfigure>();

	public const int VotesFieldNumber = 3;

	private static readonly FieldCodec<PVEMissionVoteConfigure> _repeated_votes_codec = FieldCodec.ForMessage(26u, PVEMissionVoteConfigure.Parser);

	private readonly RepeatedField<PVEMissionVoteConfigure> votes_ = new RepeatedField<PVEMissionVoteConfigure>();

	public const int VoteDictFieldNumber = 4;

	private static readonly MapField<int, PVEMissionVoteConfigure>.Codec _map_voteDict_codec = new MapField<int, PVEMissionVoteConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionVoteConfigure.Parser), 34u);

	private readonly MapField<int, PVEMissionVoteConfigure> voteDict_ = new MapField<int, PVEMissionVoteConfigure>();

	public const int CluesFieldNumber = 5;

	private static readonly FieldCodec<PVEMissionClueConfigure> _repeated_clues_codec = FieldCodec.ForMessage(42u, PVEMissionClueConfigure.Parser);

	private readonly RepeatedField<PVEMissionClueConfigure> clues_ = new RepeatedField<PVEMissionClueConfigure>();

	public const int ClueDictFieldNumber = 6;

	private static readonly MapField<int, PVEMissionClueConfigure>.Codec _map_clueDict_codec = new MapField<int, PVEMissionClueConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionClueConfigure.Parser), 50u);

	private readonly MapField<int, PVEMissionClueConfigure> clueDict_ = new MapField<int, PVEMissionClueConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVEMissionConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PVEMissionReflection.Descriptor.MessageTypes[8];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVEMissionInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVEMissionVoteConfigure> Votes => votes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionVoteConfigure> VoteDict => voteDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVEMissionClueConfigure> Clues => clues_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionClueConfigure> ClueDict => clueDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionConfigure(PVEMissionConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		votes_ = other.votes_.Clone();
		voteDict_ = other.voteDict_.Clone();
		clues_ = other.clues_.Clone();
		clueDict_ = other.clueDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionConfigure Clone()
	{
		return new PVEMissionConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVEMissionConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVEMissionConfigure other)
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
		if (!votes_.Equals(other.votes_))
		{
			return false;
		}
		if (!VoteDict.Equals(other.VoteDict))
		{
			return false;
		}
		if (!clues_.Equals(other.clues_))
		{
			return false;
		}
		if (!ClueDict.Equals(other.ClueDict))
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
		num ^= votes_.GetHashCode();
		num ^= VoteDict.GetHashCode();
		num ^= clues_.GetHashCode();
		num ^= ClueDict.GetHashCode();
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
		votes_.WriteTo(ref output, _repeated_votes_codec);
		voteDict_.WriteTo(ref output, _map_voteDict_codec);
		clues_.WriteTo(ref output, _repeated_clues_codec);
		clueDict_.WriteTo(ref output, _map_clueDict_codec);
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
		num += votes_.CalculateSize(_repeated_votes_codec);
		num += voteDict_.CalculateSize(_map_voteDict_codec);
		num += clues_.CalculateSize(_repeated_clues_codec);
		num += clueDict_.CalculateSize(_map_clueDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PVEMissionConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			votes_.Add(other.votes_);
			voteDict_.MergeFrom(other.voteDict_);
			clues_.Add(other.clues_);
			clueDict_.MergeFrom(other.clueDict_);
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
				votes_.AddEntriesFrom(ref input, _repeated_votes_codec);
				break;
			case 34u:
				voteDict_.AddEntriesFrom(ref input, _map_voteDict_codec);
				break;
			case 42u:
				clues_.AddEntriesFrom(ref input, _repeated_clues_codec);
				break;
			case 50u:
				clueDict_.AddEntriesFrom(ref input, _map_clueDict_codec);
				break;
			}
		}
	}
}
