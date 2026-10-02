using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ChallengeData : IMessage<ChallengeData>, IMessage, IEquatable<ChallengeData>, IDeepCloneable<ChallengeData>, IBufferMessage
{
	private static readonly MessageParser<ChallengeData> _parser = new MessageParser<ChallengeData>(() => new ChallengeData());

	private UnknownFieldSet _unknownFields;

	public const int GameDataFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_gameData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<int, int> gameData_ = new MapField<int, int>();

	public const int KillDataFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_killData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> killData_ = new MapField<int, int>();

	public const int GameStatsFieldNumber = 3;

	private GameStats gameStats_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChallengeData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> GameData => gameData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> KillData => killData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameStats GameStats
	{
		get
		{
			return gameStats_;
		}
		set
		{
			gameStats_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChallengeData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChallengeData(ChallengeData other)
		: this()
	{
		gameData_ = other.gameData_.Clone();
		killData_ = other.killData_.Clone();
		gameStats_ = ((other.gameStats_ != null) ? other.gameStats_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChallengeData Clone()
	{
		return new ChallengeData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChallengeData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChallengeData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!GameData.Equals(other.GameData))
		{
			return false;
		}
		if (!KillData.Equals(other.KillData))
		{
			return false;
		}
		if (!object.Equals(GameStats, other.GameStats))
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
		num ^= GameData.GetHashCode();
		num ^= KillData.GetHashCode();
		if (gameStats_ != null)
		{
			num ^= GameStats.GetHashCode();
		}
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
		gameData_.WriteTo(ref output, _map_gameData_codec);
		killData_.WriteTo(ref output, _map_killData_codec);
		if (gameStats_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(GameStats);
		}
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
		num += gameData_.CalculateSize(_map_gameData_codec);
		num += killData_.CalculateSize(_map_killData_codec);
		if (gameStats_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(GameStats);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChallengeData other)
	{
		if (other == null)
		{
			return;
		}
		gameData_.MergeFrom(other.gameData_);
		killData_.MergeFrom(other.killData_);
		if (other.gameStats_ != null)
		{
			if (gameStats_ == null)
			{
				GameStats = new GameStats();
			}
			GameStats.MergeFrom(other.GameStats);
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				gameData_.AddEntriesFrom(ref input, _map_gameData_codec);
				break;
			case 18u:
				killData_.AddEntriesFrom(ref input, _map_killData_codec);
				break;
			case 26u:
				if (gameStats_ == null)
				{
					GameStats = new GameStats();
				}
				input.ReadMessage(GameStats);
				break;
			}
		}
	}
}
