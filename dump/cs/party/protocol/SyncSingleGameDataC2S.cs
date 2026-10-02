using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SyncSingleGameDataC2S : IMessage<SyncSingleGameDataC2S>, IMessage, IEquatable<SyncSingleGameDataC2S>, IDeepCloneable<SyncSingleGameDataC2S>, IBufferMessage
{
	private static readonly MessageParser<SyncSingleGameDataC2S> _parser = new MessageParser<SyncSingleGameDataC2S>(() => new SyncSingleGameDataC2S());

	private UnknownFieldSet _unknownFields;

	public const int SingleGameInfoFieldNumber = 1;

	private SingleGameData singleGameInfo_;

	public const int LevelIdFieldNumber = 2;

	private int levelId_;

	public const int ScoreFieldNumber = 3;

	private int score_;

	public const int StageIdFieldNumber = 4;

	private int stageId_;

	public const int StageLevelIdFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_stageLevelId_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> stageLevelId_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncSingleGameDataC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[353];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameData SingleGameInfo
	{
		get
		{
			return singleGameInfo_;
		}
		set
		{
			singleGameInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LevelId
	{
		get
		{
			return levelId_;
		}
		set
		{
			levelId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Score
	{
		get
		{
			return score_;
		}
		set
		{
			score_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StageId
	{
		get
		{
			return stageId_;
		}
		set
		{
			stageId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> StageLevelId => stageLevelId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncSingleGameDataC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncSingleGameDataC2S(SyncSingleGameDataC2S other)
		: this()
	{
		singleGameInfo_ = ((other.singleGameInfo_ != null) ? other.singleGameInfo_.Clone() : null);
		levelId_ = other.levelId_;
		score_ = other.score_;
		stageId_ = other.stageId_;
		stageLevelId_ = other.stageLevelId_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncSingleGameDataC2S Clone()
	{
		return new SyncSingleGameDataC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncSingleGameDataC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncSingleGameDataC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(SingleGameInfo, other.SingleGameInfo))
		{
			return false;
		}
		if (LevelId != other.LevelId)
		{
			return false;
		}
		if (Score != other.Score)
		{
			return false;
		}
		if (StageId != other.StageId)
		{
			return false;
		}
		if (!StageLevelId.Equals(other.StageLevelId))
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
		if (singleGameInfo_ != null)
		{
			num ^= SingleGameInfo.GetHashCode();
		}
		if (LevelId != 0)
		{
			num ^= LevelId.GetHashCode();
		}
		if (Score != 0)
		{
			num ^= Score.GetHashCode();
		}
		if (StageId != 0)
		{
			num ^= StageId.GetHashCode();
		}
		num ^= StageLevelId.GetHashCode();
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
		if (singleGameInfo_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(SingleGameInfo);
		}
		if (LevelId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(LevelId);
		}
		if (Score != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Score);
		}
		if (StageId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(StageId);
		}
		stageLevelId_.WriteTo(ref output, _map_stageLevelId_codec);
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
		if (singleGameInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SingleGameInfo);
		}
		if (LevelId != 0)
		{
			num += 5;
		}
		if (Score != 0)
		{
			num += 5;
		}
		if (StageId != 0)
		{
			num += 5;
		}
		num += stageLevelId_.CalculateSize(_map_stageLevelId_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SyncSingleGameDataC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.singleGameInfo_ != null)
		{
			if (singleGameInfo_ == null)
			{
				SingleGameInfo = new SingleGameData();
			}
			SingleGameInfo.MergeFrom(other.SingleGameInfo);
		}
		if (other.LevelId != 0)
		{
			LevelId = other.LevelId;
		}
		if (other.Score != 0)
		{
			Score = other.Score;
		}
		if (other.StageId != 0)
		{
			StageId = other.StageId;
		}
		stageLevelId_.MergeFrom(other.stageLevelId_);
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
				if (singleGameInfo_ == null)
				{
					SingleGameInfo = new SingleGameData();
				}
				input.ReadMessage(SingleGameInfo);
				break;
			case 21u:
				LevelId = input.ReadSFixed32();
				break;
			case 29u:
				Score = input.ReadSFixed32();
				break;
			case 37u:
				StageId = input.ReadSFixed32();
				break;
			case 42u:
				stageLevelId_.AddEntriesFrom(ref input, _map_stageLevelId_codec);
				break;
			}
		}
	}
}
