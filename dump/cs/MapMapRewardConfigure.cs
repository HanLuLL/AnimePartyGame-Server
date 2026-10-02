using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MapMapRewardConfigure : IMessage<MapMapRewardConfigure>, IMessage, IEquatable<MapMapRewardConfigure>, IDeepCloneable<MapMapRewardConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapMapRewardConfigure> _parser = new MessageParser<MapMapRewardConfigure>(() => new MapMapRewardConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int VictoryRewardFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_victoryReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> victoryReward_ = new MapField<int, int>();

	public const int LoseRewardFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_loseReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> loseReward_ = new MapField<int, int>();

	public const int VictoryRewardAdditionFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_victoryRewardAddition_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> victoryRewardAddition_ = new MapField<int, int>();

	public const int ComebackRewardAdditionFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_comebackRewardAddition_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> comebackRewardAddition_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapMapRewardConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> VictoryReward => victoryReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> LoseReward => loseReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> VictoryRewardAddition => victoryRewardAddition_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ComebackRewardAddition => comebackRewardAddition_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapRewardConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapRewardConfigure(MapMapRewardConfigure other)
		: this()
	{
		id_ = other.id_;
		victoryReward_ = other.victoryReward_.Clone();
		loseReward_ = other.loseReward_.Clone();
		victoryRewardAddition_ = other.victoryRewardAddition_.Clone();
		comebackRewardAddition_ = other.comebackRewardAddition_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapRewardConfigure Clone()
	{
		return new MapMapRewardConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapMapRewardConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapMapRewardConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (!VictoryReward.Equals(other.VictoryReward))
		{
			return false;
		}
		if (!LoseReward.Equals(other.LoseReward))
		{
			return false;
		}
		if (!VictoryRewardAddition.Equals(other.VictoryRewardAddition))
		{
			return false;
		}
		if (!ComebackRewardAddition.Equals(other.ComebackRewardAddition))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		num ^= VictoryReward.GetHashCode();
		num ^= LoseReward.GetHashCode();
		num ^= VictoryRewardAddition.GetHashCode();
		num ^= ComebackRewardAddition.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		victoryReward_.WriteTo(ref output, _map_victoryReward_codec);
		loseReward_.WriteTo(ref output, _map_loseReward_codec);
		victoryRewardAddition_.WriteTo(ref output, _map_victoryRewardAddition_codec);
		comebackRewardAddition_.WriteTo(ref output, _map_comebackRewardAddition_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		num += victoryReward_.CalculateSize(_map_victoryReward_codec);
		num += loseReward_.CalculateSize(_map_loseReward_codec);
		num += victoryRewardAddition_.CalculateSize(_map_victoryRewardAddition_codec);
		num += comebackRewardAddition_.CalculateSize(_map_comebackRewardAddition_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapMapRewardConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			victoryReward_.MergeFrom(other.victoryReward_);
			loseReward_.MergeFrom(other.loseReward_);
			victoryRewardAddition_.MergeFrom(other.victoryRewardAddition_);
			comebackRewardAddition_.MergeFrom(other.comebackRewardAddition_);
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 18u:
				victoryReward_.AddEntriesFrom(ref input, _map_victoryReward_codec);
				break;
			case 26u:
				loseReward_.AddEntriesFrom(ref input, _map_loseReward_codec);
				break;
			case 34u:
				victoryRewardAddition_.AddEntriesFrom(ref input, _map_victoryRewardAddition_codec);
				break;
			case 42u:
				comebackRewardAddition_.AddEntriesFrom(ref input, _map_comebackRewardAddition_codec);
				break;
			}
		}
	}
}
