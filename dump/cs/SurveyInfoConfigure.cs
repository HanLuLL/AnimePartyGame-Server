using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class SurveyInfoConfigure : IMessage<SurveyInfoConfigure>, IMessage, IEquatable<SurveyInfoConfigure>, IDeepCloneable<SurveyInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<SurveyInfoConfigure> _parser = new MessageParser<SurveyInfoConfigure>(() => new SurveyInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SurveyTypeFieldNumber = 2;

	private SurveyType surveyType_;

	public const int NameIDFieldNumber = 3;

	private int nameID_;

	public const int TitleIDFieldNumber = 4;

	private int titleID_;

	public const int SurveyImageFieldNumber = 5;

	private string surveyImage_ = "";

	public const int TextIDFieldNumber = 6;

	private int textID_;

	public const int MailContentFieldNumber = 7;

	private int mailContent_;

	public const int SurveyLinkFieldNumber = 8;

	private string surveyLink_ = "";

	public const int RewardFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int BeginTimeFieldNumber = 10;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 11;

	private Timestamp endTime_;

	public const int DurationFieldNumber = 12;

	private int duration_;

	public const int MailTitleFieldNumber = 13;

	private int mailTitle_;

	public const int MailTextFieldNumber = 14;

	private int mailText_;

	public const int MailSenderFieldNumber = 15;

	private int mailSender_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SurveyInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SurveyReflection.Descriptor.MessageTypes[0];

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
	public SurveyType SurveyType
	{
		get
		{
			return surveyType_;
		}
		private set
		{
			surveyType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TitleID
	{
		get
		{
			return titleID_;
		}
		private set
		{
			titleID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SurveyImage
	{
		get
		{
			return surveyImage_;
		}
		private set
		{
			surveyImage_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TextID
	{
		get
		{
			return textID_;
		}
		private set
		{
			textID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MailContent
	{
		get
		{
			return mailContent_;
		}
		private set
		{
			mailContent_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SurveyLink
	{
		get
		{
			return surveyLink_;
		}
		private set
		{
			surveyLink_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Duration
	{
		get
		{
			return duration_;
		}
		private set
		{
			duration_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MailTitle
	{
		get
		{
			return mailTitle_;
		}
		private set
		{
			mailTitle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MailText
	{
		get
		{
			return mailText_;
		}
		private set
		{
			mailText_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MailSender
	{
		get
		{
			return mailSender_;
		}
		private set
		{
			mailSender_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SurveyInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SurveyInfoConfigure(SurveyInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		surveyType_ = other.surveyType_;
		nameID_ = other.nameID_;
		titleID_ = other.titleID_;
		surveyImage_ = other.surveyImage_;
		textID_ = other.textID_;
		mailContent_ = other.mailContent_;
		surveyLink_ = other.surveyLink_;
		reward_ = other.reward_.Clone();
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		duration_ = other.duration_;
		mailTitle_ = other.mailTitle_;
		mailText_ = other.mailText_;
		mailSender_ = other.mailSender_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SurveyInfoConfigure Clone()
	{
		return new SurveyInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SurveyInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SurveyInfoConfigure other)
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
		if (SurveyType != other.SurveyType)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (TitleID != other.TitleID)
		{
			return false;
		}
		if (SurveyImage != other.SurveyImage)
		{
			return false;
		}
		if (TextID != other.TextID)
		{
			return false;
		}
		if (MailContent != other.MailContent)
		{
			return false;
		}
		if (SurveyLink != other.SurveyLink)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (Duration != other.Duration)
		{
			return false;
		}
		if (MailTitle != other.MailTitle)
		{
			return false;
		}
		if (MailText != other.MailText)
		{
			return false;
		}
		if (MailSender != other.MailSender)
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
		if (SurveyType != SurveyType.Noob)
		{
			num ^= SurveyType.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (TitleID != 0)
		{
			num ^= TitleID.GetHashCode();
		}
		if (SurveyImage.Length != 0)
		{
			num ^= SurveyImage.GetHashCode();
		}
		if (TextID != 0)
		{
			num ^= TextID.GetHashCode();
		}
		if (MailContent != 0)
		{
			num ^= MailContent.GetHashCode();
		}
		if (SurveyLink.Length != 0)
		{
			num ^= SurveyLink.GetHashCode();
		}
		num ^= Reward.GetHashCode();
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (Duration != 0)
		{
			num ^= Duration.GetHashCode();
		}
		if (MailTitle != 0)
		{
			num ^= MailTitle.GetHashCode();
		}
		if (MailText != 0)
		{
			num ^= MailText.GetHashCode();
		}
		if (MailSender != 0)
		{
			num ^= MailSender.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (SurveyType != SurveyType.Noob)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)SurveyType);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NameID);
		}
		if (TitleID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TitleID);
		}
		if (SurveyImage.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(SurveyImage);
		}
		if (TextID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TextID);
		}
		if (MailContent != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(MailContent);
		}
		if (SurveyLink.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(SurveyLink);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		if (beginTime_ != null)
		{
			output.WriteRawTag(82);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(90);
			output.WriteMessage(EndTime);
		}
		if (Duration != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(Duration);
		}
		if (MailTitle != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(MailTitle);
		}
		if (MailText != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(MailText);
		}
		if (MailSender != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(MailSender);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (SurveyType != SurveyType.Noob)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SurveyType);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (TitleID != 0)
		{
			num += 5;
		}
		if (SurveyImage.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SurveyImage);
		}
		if (TextID != 0)
		{
			num += 5;
		}
		if (MailContent != 0)
		{
			num += 5;
		}
		if (SurveyLink.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SurveyLink);
		}
		num += reward_.CalculateSize(_map_reward_codec);
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (Duration != 0)
		{
			num += 5;
		}
		if (MailTitle != 0)
		{
			num += 5;
		}
		if (MailText != 0)
		{
			num += 5;
		}
		if (MailSender != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SurveyInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.SurveyType != SurveyType.Noob)
		{
			SurveyType = other.SurveyType;
		}
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
		if (other.TitleID != 0)
		{
			TitleID = other.TitleID;
		}
		if (other.SurveyImage.Length != 0)
		{
			SurveyImage = other.SurveyImage;
		}
		if (other.TextID != 0)
		{
			TextID = other.TextID;
		}
		if (other.MailContent != 0)
		{
			MailContent = other.MailContent;
		}
		if (other.SurveyLink.Length != 0)
		{
			SurveyLink = other.SurveyLink;
		}
		reward_.MergeFrom(other.reward_);
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		if (other.Duration != 0)
		{
			Duration = other.Duration;
		}
		if (other.MailTitle != 0)
		{
			MailTitle = other.MailTitle;
		}
		if (other.MailText != 0)
		{
			MailText = other.MailText;
		}
		if (other.MailSender != 0)
		{
			MailSender = other.MailSender;
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 16u:
				SurveyType = (SurveyType)input.ReadEnum();
				break;
			case 29u:
				NameID = input.ReadSFixed32();
				break;
			case 37u:
				TitleID = input.ReadSFixed32();
				break;
			case 42u:
				SurveyImage = input.ReadString();
				break;
			case 53u:
				TextID = input.ReadSFixed32();
				break;
			case 61u:
				MailContent = input.ReadSFixed32();
				break;
			case 66u:
				SurveyLink = input.ReadString();
				break;
			case 74u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 82u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 90u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 101u:
				Duration = input.ReadSFixed32();
				break;
			case 109u:
				MailTitle = input.ReadSFixed32();
				break;
			case 117u:
				MailText = input.ReadSFixed32();
				break;
			case 125u:
				MailSender = input.ReadSFixed32();
				break;
			}
		}
	}
}
