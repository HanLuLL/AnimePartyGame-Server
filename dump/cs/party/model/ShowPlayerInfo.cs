using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ShowPlayerInfo : IMessage<ShowPlayerInfo>, IMessage, IEquatable<ShowPlayerInfo>, IDeepCloneable<ShowPlayerInfo>, IBufferMessage
{
	private static readonly MessageParser<ShowPlayerInfo> _parser = new MessageParser<ShowPlayerInfo>(() => new ShowPlayerInfo());

	private UnknownFieldSet _unknownFields;

	public const int StandingPaintingFieldNumber = 1;

	private int standingPainting_;

	public const int AchieveIdFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_achieveId_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> achieveId_ = new RepeatedField<int>();

	public const int IsShowDataFieldNumber = 3;

	private bool isShowData_;

	public const int IsShowFightFieldNumber = 4;

	private bool isShowFight_;

	public const int RecordFieldNumber = 5;

	private static readonly FieldCodec<PlayerFightRecord> _repeated_record_codec = FieldCodec.ForMessage(42u, PlayerFightRecord.Parser);

	private readonly RepeatedField<PlayerFightRecord> record_ = new RepeatedField<PlayerFightRecord>();

	public const int PraiseNumFieldNumber = 6;

	private int praiseNum_;

	public const int ReplayRecordFieldNumber = 7;

	private static readonly FieldCodec<PlayerFightRecord> _repeated_replayRecord_codec = FieldCodec.ForMessage(58u, PlayerFightRecord.Parser);

	private readonly RepeatedField<PlayerFightRecord> replayRecord_ = new RepeatedField<PlayerFightRecord>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShowPlayerInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[25];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public RepeatedField<PlayerFightRecord> Record => record_;

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
	public RepeatedField<PlayerFightRecord> ReplayRecord => replayRecord_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerInfo(ShowPlayerInfo other)
		: this()
	{
		standingPainting_ = other.standingPainting_;
		achieveId_ = other.achieveId_.Clone();
		isShowData_ = other.isShowData_;
		isShowFight_ = other.isShowFight_;
		record_ = other.record_.Clone();
		praiseNum_ = other.praiseNum_;
		replayRecord_ = other.replayRecord_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerInfo Clone()
	{
		return new ShowPlayerInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShowPlayerInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShowPlayerInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
		if (StandingPainting != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(StandingPainting);
		}
		achieveId_.WriteTo(ref output, _repeated_achieveId_codec);
		if (IsShowData)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsShowData);
		}
		if (IsShowFight)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsShowFight);
		}
		record_.WriteTo(ref output, _repeated_record_codec);
		if (PraiseNum != 0)
		{
			output.WriteRawTag(53);
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
	public void MergeFrom(ShowPlayerInfo other)
	{
		if (other != null)
		{
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
			if (other.PraiseNum != 0)
			{
				PraiseNum = other.PraiseNum;
			}
			replayRecord_.Add(other.replayRecord_);
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
				StandingPainting = input.ReadSFixed32();
				break;
			case 18u:
			case 21u:
				achieveId_.AddEntriesFrom(ref input, _repeated_achieveId_codec);
				break;
			case 24u:
				IsShowData = input.ReadBool();
				break;
			case 32u:
				IsShowFight = input.ReadBool();
				break;
			case 42u:
				record_.AddEntriesFrom(ref input, _repeated_record_codec);
				break;
			case 53u:
				PraiseNum = input.ReadSFixed32();
				break;
			case 58u:
				replayRecord_.AddEntriesFrom(ref input, _repeated_replayRecord_codec);
				break;
			}
		}
	}
}
