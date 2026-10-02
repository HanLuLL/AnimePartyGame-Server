using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ShowPlayer : IMessage<ShowPlayer>, IMessage, IEquatable<ShowPlayer>, IDeepCloneable<ShowPlayer>, IBufferMessage
{
	private static readonly MessageParser<ShowPlayer> _parser = new MessageParser<ShowPlayer>(() => new ShowPlayer());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int StandingPaintingFieldNumber = 5;

	private int standingPainting_;

	public const int AchieveIdFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_achieveId_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> achieveId_ = new RepeatedField<int>();

	public const int IsShowDataFieldNumber = 7;

	private bool isShowData_;

	public const int IsShowFightFieldNumber = 8;

	private bool isShowFight_;

	public const int RecordFieldNumber = 9;

	private static readonly FieldCodec<ShowPlayerShortFight> _repeated_record_codec = FieldCodec.ForMessage(74u, ShowPlayerShortFight.Parser);

	private readonly RepeatedField<ShowPlayerShortFight> record_ = new RepeatedField<ShowPlayerShortFight>();

	public const int StatisticsFieldNumber = 10;

	private ShowPlayerStatistics statistics_;

	public const int PraiseNumFieldNumber = 11;

	private int praiseNum_;

	public const int ReplayRecordFieldNumber = 12;

	private static readonly FieldCodec<ShowPlayerShortFight> _repeated_replayRecord_codec = FieldCodec.ForMessage(98u, ShowPlayerShortFight.Parser);

	private readonly RepeatedField<ShowPlayerShortFight> replayRecord_ = new RepeatedField<ShowPlayerShortFight>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShowPlayer> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[105];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StandingPainting
	{
		get
		{
			return standingPainting_;
		}
		set
		{
			standingPainting_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> AchieveId => achieveId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShowData
	{
		get
		{
			return isShowData_;
		}
		set
		{
			isShowData_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShowFight
	{
		get
		{
			return isShowFight_;
		}
		set
		{
			isShowFight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ShowPlayerShortFight> Record => record_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerStatistics Statistics
	{
		get
		{
			return statistics_;
		}
		set
		{
			statistics_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PraiseNum
	{
		get
		{
			return praiseNum_;
		}
		set
		{
			praiseNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ShowPlayerShortFight> ReplayRecord => replayRecord_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayer()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayer(ShowPlayer other)
		: this()
	{
		playerId_ = other.playerId_;
		standingPainting_ = other.standingPainting_;
		achieveId_ = other.achieveId_.Clone();
		isShowData_ = other.isShowData_;
		isShowFight_ = other.isShowFight_;
		record_ = other.record_.Clone();
		statistics_ = ((other.statistics_ != null) ? other.statistics_.Clone() : null);
		praiseNum_ = other.praiseNum_;
		replayRecord_ = other.replayRecord_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayer Clone()
	{
		return new ShowPlayer(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShowPlayer);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShowPlayer other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (StandingPainting != other.StandingPainting)
		{
			return false;
		}
		if (!achieveId_.Equals(other.achieveId_))
		{
			return false;
		}
		if (IsShowData != other.IsShowData)
		{
			return false;
		}
		if (IsShowFight != other.IsShowFight)
		{
			return false;
		}
		if (!record_.Equals(other.record_))
		{
			return false;
		}
		if (!object.Equals(Statistics, other.Statistics))
		{
			return false;
		}
		if (PraiseNum != other.PraiseNum)
		{
			return false;
		}
		if (!replayRecord_.Equals(other.replayRecord_))
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (StandingPainting != 0)
		{
			num ^= StandingPainting.GetHashCode();
		}
		num ^= achieveId_.GetHashCode();
		if (IsShowData)
		{
			num ^= IsShowData.GetHashCode();
		}
		if (IsShowFight)
		{
			num ^= IsShowFight.GetHashCode();
		}
		num ^= record_.GetHashCode();
		if (statistics_ != null)
		{
			num ^= Statistics.GetHashCode();
		}
		if (PraiseNum != 0)
		{
			num ^= PraiseNum.GetHashCode();
		}
		num ^= replayRecord_.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (StandingPainting != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(StandingPainting);
		}
		achieveId_.WriteTo(ref output, _repeated_achieveId_codec);
		if (IsShowData)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsShowData);
		}
		if (IsShowFight)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsShowFight);
		}
		record_.WriteTo(ref output, _repeated_record_codec);
		if (statistics_ != null)
		{
			output.WriteRawTag(82);
			output.WriteMessage(Statistics);
		}
		if (PraiseNum != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(PraiseNum);
		}
		replayRecord_.WriteTo(ref output, _repeated_replayRecord_codec);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (StandingPainting != 0)
		{
			num += 5;
		}
		num += achieveId_.CalculateSize(_repeated_achieveId_codec);
		if (IsShowData)
		{
			num += 2;
		}
		if (IsShowFight)
		{
			num += 2;
		}
		num += record_.CalculateSize(_repeated_record_codec);
		if (statistics_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Statistics);
		}
		if (PraiseNum != 0)
		{
			num += 5;
		}
		num += replayRecord_.CalculateSize(_repeated_replayRecord_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ShowPlayer other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
		}
		if (other.StandingPainting != 0)
		{
			StandingPainting = other.StandingPainting;
		}
		achieveId_.Add(other.achieveId_);
		if (other.IsShowData)
		{
			IsShowData = other.IsShowData;
		}
		if (other.IsShowFight)
		{
			IsShowFight = other.IsShowFight;
		}
		record_.Add(other.record_);
		if (other.statistics_ != null)
		{
			if (statistics_ == null)
			{
				Statistics = new ShowPlayerStatistics();
			}
			Statistics.MergeFrom(other.Statistics);
		}
		if (other.PraiseNum != 0)
		{
			PraiseNum = other.PraiseNum;
		}
		replayRecord_.Add(other.replayRecord_);
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 45u:
				StandingPainting = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				achieveId_.AddEntriesFrom(ref input, _repeated_achieveId_codec);
				break;
			case 56u:
				IsShowData = input.ReadBool();
				break;
			case 64u:
				IsShowFight = input.ReadBool();
				break;
			case 74u:
				record_.AddEntriesFrom(ref input, _repeated_record_codec);
				break;
			case 82u:
				if (statistics_ == null)
				{
					Statistics = new ShowPlayerStatistics();
				}
				input.ReadMessage(Statistics);
				break;
			case 93u:
				PraiseNum = input.ReadSFixed32();
				break;
			case 98u:
				replayRecord_.AddEntriesFrom(ref input, _repeated_replayRecord_codec);
				break;
			}
		}
	}
}
