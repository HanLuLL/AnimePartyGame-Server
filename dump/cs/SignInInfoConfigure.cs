using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class SignInInfoConfigure : IMessage<SignInInfoConfigure>, IMessage, IEquatable<SignInInfoConfigure>, IDeepCloneable<SignInInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<SignInInfoConfigure> _parser = new MessageParser<SignInInfoConfigure>(() => new SignInInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BeginTimeFieldNumber = 2;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 3;

	private Timestamp endTime_;

	public const int SigninRewardIDFieldNumber = 4;

	private int signinRewardID_;

	public const int IconFieldNumber = 5;

	private string icon_ = "";

	public const int BgFieldNumber = 6;

	private string bg_ = "";

	public const int TitleFieldNumber = 7;

	private int title_;

	public const int HeadlineFieldNumber = 8;

	private int headline_;

	public const int DescriptionFieldNumber = 9;

	private int description_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SignInInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SignInReflection.Descriptor.MessageTypes[0];

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
	public int SigninRewardID
	{
		get
		{
			return signinRewardID_;
		}
		private set
		{
			signinRewardID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Bg
	{
		get
		{
			return bg_;
		}
		private set
		{
			bg_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Title
	{
		get
		{
			return title_;
		}
		private set
		{
			title_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Headline
	{
		get
		{
			return headline_;
		}
		private set
		{
			headline_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Description
	{
		get
		{
			return description_;
		}
		private set
		{
			description_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInInfoConfigure(SignInInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		signinRewardID_ = other.signinRewardID_;
		icon_ = other.icon_;
		bg_ = other.bg_;
		title_ = other.title_;
		headline_ = other.headline_;
		description_ = other.description_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInInfoConfigure Clone()
	{
		return new SignInInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SignInInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SignInInfoConfigure other)
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
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (SigninRewardID != other.SigninRewardID)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (Bg != other.Bg)
		{
			return false;
		}
		if (Title != other.Title)
		{
			return false;
		}
		if (Headline != other.Headline)
		{
			return false;
		}
		if (Description != other.Description)
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
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (SigninRewardID != 0)
		{
			num ^= SigninRewardID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (Bg.Length != 0)
		{
			num ^= Bg.GetHashCode();
		}
		if (Title != 0)
		{
			num ^= Title.GetHashCode();
		}
		if (Headline != 0)
		{
			num ^= Headline.GetHashCode();
		}
		if (Description != 0)
		{
			num ^= Description.GetHashCode();
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
		if (beginTime_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(EndTime);
		}
		if (SigninRewardID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SigninRewardID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Icon);
		}
		if (Bg.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Bg);
		}
		if (Title != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Title);
		}
		if (Headline != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Headline);
		}
		if (Description != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Description);
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
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (SigninRewardID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (Bg.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Bg);
		}
		if (Title != 0)
		{
			num += 5;
		}
		if (Headline != 0)
		{
			num += 5;
		}
		if (Description != 0)
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
	public void MergeFrom(SignInInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
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
		if (other.SigninRewardID != 0)
		{
			SigninRewardID = other.SigninRewardID;
		}
		if (other.Icon.Length != 0)
		{
			Icon = other.Icon;
		}
		if (other.Bg.Length != 0)
		{
			Bg = other.Bg;
		}
		if (other.Title != 0)
		{
			Title = other.Title;
		}
		if (other.Headline != 0)
		{
			Headline = other.Headline;
		}
		if (other.Description != 0)
		{
			Description = other.Description;
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
			case 18u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 26u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 37u:
				SigninRewardID = input.ReadSFixed32();
				break;
			case 42u:
				Icon = input.ReadString();
				break;
			case 50u:
				Bg = input.ReadString();
				break;
			case 61u:
				Title = input.ReadSFixed32();
				break;
			case 69u:
				Headline = input.ReadSFixed32();
				break;
			case 77u:
				Description = input.ReadSFixed32();
				break;
			}
		}
	}

	public void FixTime(FixSignInInfoConfigure data)
	{
		beginTime_ = data.BeginTime;
		endTime_ = data.EndTime;
	}
}
