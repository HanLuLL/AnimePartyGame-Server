using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SqlPlayerDataByte1 : IMessage<SqlPlayerDataByte1>, IMessage, IEquatable<SqlPlayerDataByte1>, IDeepCloneable<SqlPlayerDataByte1>, IBufferMessage
{
	private static readonly MessageParser<SqlPlayerDataByte1> _parser = new MessageParser<SqlPlayerDataByte1>(() => new SqlPlayerDataByte1());

	private UnknownFieldSet _unknownFields;

	public const int CdkFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_cdk_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> cdk_ = new RepeatedField<int>();

	public const int MailFieldNumber = 2;

	private static readonly FieldCodec<MailData> _repeated_mail_codec = FieldCodec.ForMessage(18u, MailData.Parser);

	private readonly RepeatedField<MailData> mail_ = new RepeatedField<MailData>();

	public const int RechargeFieldNumber = 3;

	private static readonly MapField<string, RechargeInfo>.Codec _map_recharge_codec = new MapField<string, RechargeInfo>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForMessage(18u, RechargeInfo.Parser), 26u);

	private readonly MapField<string, RechargeInfo> recharge_ = new MapField<string, RechargeInfo>();

	public const int SingleInfoFieldNumber = 4;

	private SingleInfo singleInfo_;

	public const int ActivityPassFieldNumber = 5;

	private static readonly MapField<int, ActivityPass>.Codec _map_activityPass_codec = new MapField<int, ActivityPass>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.ActivityPass.Parser), 42u);

	private readonly MapField<int, ActivityPass> activityPass_ = new MapField<int, ActivityPass>();

	public const int OnlineStatusFieldNumber = 6;

	private int onlineStatus_;

	public const int AltArtCardsFieldNumber = 7;

	private static readonly MapField<int, AltArtCardInfo>.Codec _map_altArtCards_codec = new MapField<int, AltArtCardInfo>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AltArtCardInfo.Parser), 58u);

	private readonly MapField<int, AltArtCardInfo> altArtCards_ = new MapField<int, AltArtCardInfo>();

	public const int CreditInfoFieldNumber = 8;

	private CreditInfo creditInfo_;

	public const int SportsMeetInfoFieldNumber = 9;

	private SportsMeetInfo sportsMeetInfo_;

	public const int MuteTimeFieldNumber = 10;

	private long muteTime_;

	public const int FlipCardFieldNumber = 11;

	private static readonly MapField<int, FlipCardActivity>.Codec _map_flipCard_codec = new MapField<int, FlipCardActivity>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FlipCardActivity.Parser), 90u);

	private readonly MapField<int, FlipCardActivity> flipCard_ = new MapField<int, FlipCardActivity>();

	public const int GuildInfoFieldNumber = 12;

	private PlayerGuildInfo guildInfo_;

	public const int QuestionInfoFieldNumber = 13;

	private static readonly MapField<int, QuestionModel>.Codec _map_questionInfo_codec = new MapField<int, QuestionModel>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, QuestionModel.Parser), 106u);

	private readonly MapField<int, QuestionModel> questionInfo_ = new MapField<int, QuestionModel>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SqlPlayerDataByte1> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[32];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Cdk => cdk_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MailData> Mail => mail_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, RechargeInfo> Recharge => recharge_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleInfo SingleInfo
	{
		get
		{
			return singleInfo_;
		}
		set
		{
			singleInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ActivityPass> ActivityPass => activityPass_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OnlineStatus
	{
		get
		{
			return onlineStatus_;
		}
		set
		{
			onlineStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AltArtCardInfo> AltArtCards => altArtCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreditInfo CreditInfo
	{
		get
		{
			return creditInfo_;
		}
		set
		{
			creditInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SportsMeetInfo SportsMeetInfo
	{
		get
		{
			return sportsMeetInfo_;
		}
		set
		{
			sportsMeetInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MuteTime
	{
		get
		{
			return muteTime_;
		}
		set
		{
			muteTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FlipCardActivity> FlipCard => flipCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerGuildInfo GuildInfo
	{
		get
		{
			return guildInfo_;
		}
		set
		{
			guildInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, QuestionModel> QuestionInfo => questionInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerDataByte1()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerDataByte1(SqlPlayerDataByte1 other)
		: this()
	{
		cdk_ = other.cdk_.Clone();
		mail_ = other.mail_.Clone();
		recharge_ = other.recharge_.Clone();
		singleInfo_ = ((other.singleInfo_ != null) ? other.singleInfo_.Clone() : null);
		activityPass_ = other.activityPass_.Clone();
		onlineStatus_ = other.onlineStatus_;
		altArtCards_ = other.altArtCards_.Clone();
		creditInfo_ = ((other.creditInfo_ != null) ? other.creditInfo_.Clone() : null);
		sportsMeetInfo_ = ((other.sportsMeetInfo_ != null) ? other.sportsMeetInfo_.Clone() : null);
		muteTime_ = other.muteTime_;
		flipCard_ = other.flipCard_.Clone();
		guildInfo_ = ((other.guildInfo_ != null) ? other.guildInfo_.Clone() : null);
		questionInfo_ = other.questionInfo_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerDataByte1 Clone()
	{
		return new SqlPlayerDataByte1(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SqlPlayerDataByte1);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SqlPlayerDataByte1 other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!cdk_.Equals(other.cdk_))
		{
			return false;
		}
		if (!mail_.Equals(other.mail_))
		{
			return false;
		}
		if (!Recharge.Equals(other.Recharge))
		{
			return false;
		}
		if (!object.Equals(SingleInfo, other.SingleInfo))
		{
			return false;
		}
		if (!ActivityPass.Equals(other.ActivityPass))
		{
			return false;
		}
		if (OnlineStatus != other.OnlineStatus)
		{
			return false;
		}
		if (!AltArtCards.Equals(other.AltArtCards))
		{
			return false;
		}
		if (!object.Equals(CreditInfo, other.CreditInfo))
		{
			return false;
		}
		if (!object.Equals(SportsMeetInfo, other.SportsMeetInfo))
		{
			return false;
		}
		if (MuteTime != other.MuteTime)
		{
			return false;
		}
		if (!FlipCard.Equals(other.FlipCard))
		{
			return false;
		}
		if (!object.Equals(GuildInfo, other.GuildInfo))
		{
			return false;
		}
		if (!QuestionInfo.Equals(other.QuestionInfo))
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
		num ^= cdk_.GetHashCode();
		num ^= mail_.GetHashCode();
		num ^= Recharge.GetHashCode();
		if (singleInfo_ != null)
		{
			num ^= SingleInfo.GetHashCode();
		}
		num ^= ActivityPass.GetHashCode();
		if (OnlineStatus != 0)
		{
			num ^= OnlineStatus.GetHashCode();
		}
		num ^= AltArtCards.GetHashCode();
		if (creditInfo_ != null)
		{
			num ^= CreditInfo.GetHashCode();
		}
		if (sportsMeetInfo_ != null)
		{
			num ^= SportsMeetInfo.GetHashCode();
		}
		if (MuteTime != 0L)
		{
			num ^= MuteTime.GetHashCode();
		}
		num ^= FlipCard.GetHashCode();
		if (guildInfo_ != null)
		{
			num ^= GuildInfo.GetHashCode();
		}
		num ^= QuestionInfo.GetHashCode();
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
		cdk_.WriteTo(ref output, _repeated_cdk_codec);
		mail_.WriteTo(ref output, _repeated_mail_codec);
		recharge_.WriteTo(ref output, _map_recharge_codec);
		if (singleInfo_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(SingleInfo);
		}
		activityPass_.WriteTo(ref output, _map_activityPass_codec);
		if (OnlineStatus != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(OnlineStatus);
		}
		altArtCards_.WriteTo(ref output, _map_altArtCards_codec);
		if (creditInfo_ != null)
		{
			output.WriteRawTag(66);
			output.WriteMessage(CreditInfo);
		}
		if (sportsMeetInfo_ != null)
		{
			output.WriteRawTag(74);
			output.WriteMessage(SportsMeetInfo);
		}
		if (MuteTime != 0L)
		{
			output.WriteRawTag(81);
			output.WriteSFixed64(MuteTime);
		}
		flipCard_.WriteTo(ref output, _map_flipCard_codec);
		if (guildInfo_ != null)
		{
			output.WriteRawTag(98);
			output.WriteMessage(GuildInfo);
		}
		questionInfo_.WriteTo(ref output, _map_questionInfo_codec);
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
		num += cdk_.CalculateSize(_repeated_cdk_codec);
		num += mail_.CalculateSize(_repeated_mail_codec);
		num += recharge_.CalculateSize(_map_recharge_codec);
		if (singleInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SingleInfo);
		}
		num += activityPass_.CalculateSize(_map_activityPass_codec);
		if (OnlineStatus != 0)
		{
			num += 5;
		}
		num += altArtCards_.CalculateSize(_map_altArtCards_codec);
		if (creditInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreditInfo);
		}
		if (sportsMeetInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SportsMeetInfo);
		}
		if (MuteTime != 0L)
		{
			num += 9;
		}
		num += flipCard_.CalculateSize(_map_flipCard_codec);
		if (guildInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(GuildInfo);
		}
		num += questionInfo_.CalculateSize(_map_questionInfo_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SqlPlayerDataByte1 other)
	{
		if (other == null)
		{
			return;
		}
		cdk_.Add(other.cdk_);
		mail_.Add(other.mail_);
		recharge_.MergeFrom(other.recharge_);
		if (other.singleInfo_ != null)
		{
			if (singleInfo_ == null)
			{
				SingleInfo = new SingleInfo();
			}
			SingleInfo.MergeFrom(other.SingleInfo);
		}
		activityPass_.MergeFrom(other.activityPass_);
		if (other.OnlineStatus != 0)
		{
			OnlineStatus = other.OnlineStatus;
		}
		altArtCards_.MergeFrom(other.altArtCards_);
		if (other.creditInfo_ != null)
		{
			if (creditInfo_ == null)
			{
				CreditInfo = new CreditInfo();
			}
			CreditInfo.MergeFrom(other.CreditInfo);
		}
		if (other.sportsMeetInfo_ != null)
		{
			if (sportsMeetInfo_ == null)
			{
				SportsMeetInfo = new SportsMeetInfo();
			}
			SportsMeetInfo.MergeFrom(other.SportsMeetInfo);
		}
		if (other.MuteTime != 0L)
		{
			MuteTime = other.MuteTime;
		}
		flipCard_.MergeFrom(other.flipCard_);
		if (other.guildInfo_ != null)
		{
			if (guildInfo_ == null)
			{
				GuildInfo = new PlayerGuildInfo();
			}
			GuildInfo.MergeFrom(other.GuildInfo);
		}
		questionInfo_.MergeFrom(other.questionInfo_);
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
			case 13u:
				cdk_.AddEntriesFrom(ref input, _repeated_cdk_codec);
				break;
			case 18u:
				mail_.AddEntriesFrom(ref input, _repeated_mail_codec);
				break;
			case 26u:
				recharge_.AddEntriesFrom(ref input, _map_recharge_codec);
				break;
			case 34u:
				if (singleInfo_ == null)
				{
					SingleInfo = new SingleInfo();
				}
				input.ReadMessage(SingleInfo);
				break;
			case 42u:
				activityPass_.AddEntriesFrom(ref input, _map_activityPass_codec);
				break;
			case 53u:
				OnlineStatus = input.ReadSFixed32();
				break;
			case 58u:
				altArtCards_.AddEntriesFrom(ref input, _map_altArtCards_codec);
				break;
			case 66u:
				if (creditInfo_ == null)
				{
					CreditInfo = new CreditInfo();
				}
				input.ReadMessage(CreditInfo);
				break;
			case 74u:
				if (sportsMeetInfo_ == null)
				{
					SportsMeetInfo = new SportsMeetInfo();
				}
				input.ReadMessage(SportsMeetInfo);
				break;
			case 81u:
				MuteTime = input.ReadSFixed64();
				break;
			case 90u:
				flipCard_.AddEntriesFrom(ref input, _map_flipCard_codec);
				break;
			case 98u:
				if (guildInfo_ == null)
				{
					GuildInfo = new PlayerGuildInfo();
				}
				input.ReadMessage(GuildInfo);
				break;
			case 106u:
				questionInfo_.AddEntriesFrom(ref input, _map_questionInfo_codec);
				break;
			}
		}
	}
}
