using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class MailData : IMessage<MailData>, IMessage, IEquatable<MailData>, IDeepCloneable<MailData>, IBufferMessage
{
	private static readonly MessageParser<MailData> _parser = new MessageParser<MailData>(() => new MailData());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int TitleFieldNumber = 2;

	private string title_ = "";

	public const int ContextFieldNumber = 3;

	private string context_ = "";

	public const int SendNameFieldNumber = 4;

	private string sendName_ = "";

	public const int IsReadFieldNumber = 5;

	private bool isRead_;

	public const int IsGetRewardFieldNumber = 6;

	private bool isGetReward_;

	public const int CreateTimeFieldNumber = 7;

	private long createTime_;

	public const int RewardsFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_rewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> rewards_ = new MapField<int, int>();

	public const int ExpireTimeFieldNumber = 11;

	private long expireTime_;

	public const int StartTimeFieldNumber = 12;

	private long startTime_;

	public const int IsStarMailFieldNumber = 13;

	private bool isStarMail_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MailData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[29];

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
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Title
	{
		get
		{
			return title_;
		}
		set
		{
			title_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string SendName
	{
		get
		{
			return sendName_;
		}
		set
		{
			sendName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRead
	{
		get
		{
			return isRead_;
		}
		set
		{
			isRead_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsGetReward
	{
		get
		{
			return isGetReward_;
		}
		set
		{
			isGetReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CreateTime
	{
		get
		{
			return createTime_;
		}
		set
		{
			createTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Rewards => rewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ExpireTime
	{
		get
		{
			return expireTime_;
		}
		set
		{
			expireTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long StartTime
	{
		get
		{
			return startTime_;
		}
		set
		{
			startTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsStarMail
	{
		get
		{
			return isStarMail_;
		}
		set
		{
			isStarMail_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailData(MailData other)
		: this()
	{
		id_ = other.id_;
		title_ = other.title_;
		context_ = other.context_;
		sendName_ = other.sendName_;
		isRead_ = other.isRead_;
		isGetReward_ = other.isGetReward_;
		createTime_ = other.createTime_;
		rewards_ = other.rewards_.Clone();
		expireTime_ = other.expireTime_;
		startTime_ = other.startTime_;
		isStarMail_ = other.isStarMail_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailData Clone()
	{
		return new MailData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MailData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MailData other)
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
		if (Title != other.Title)
		{
			return false;
		}
		if (Context != other.Context)
		{
			return false;
		}
		if (SendName != other.SendName)
		{
			return false;
		}
		if (IsRead != other.IsRead)
		{
			return false;
		}
		if (IsGetReward != other.IsGetReward)
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (!Rewards.Equals(other.Rewards))
		{
			return false;
		}
		if (ExpireTime != other.ExpireTime)
		{
			return false;
		}
		if (StartTime != other.StartTime)
		{
			return false;
		}
		if (IsStarMail != other.IsStarMail)
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
		if (Title.Length != 0)
		{
			num ^= Title.GetHashCode();
		}
		if (Context.Length != 0)
		{
			num ^= Context.GetHashCode();
		}
		if (SendName.Length != 0)
		{
			num ^= SendName.GetHashCode();
		}
		if (IsRead)
		{
			num ^= IsRead.GetHashCode();
		}
		if (IsGetReward)
		{
			num ^= IsGetReward.GetHashCode();
		}
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		num ^= Rewards.GetHashCode();
		if (ExpireTime != 0L)
		{
			num ^= ExpireTime.GetHashCode();
		}
		if (StartTime != 0L)
		{
			num ^= StartTime.GetHashCode();
		}
		if (IsStarMail)
		{
			num ^= IsStarMail.GetHashCode();
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
		if (Title.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Title);
		}
		if (Context.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Context);
		}
		if (SendName.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(SendName);
		}
		if (IsRead)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsRead);
		}
		if (IsGetReward)
		{
			output.WriteRawTag(48);
			output.WriteBool(IsGetReward);
		}
		if (CreateTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(CreateTime);
		}
		rewards_.WriteTo(ref output, _map_rewards_codec);
		if (ExpireTime != 0L)
		{
			output.WriteRawTag(89);
			output.WriteSFixed64(ExpireTime);
		}
		if (StartTime != 0L)
		{
			output.WriteRawTag(97);
			output.WriteSFixed64(StartTime);
		}
		if (IsStarMail)
		{
			output.WriteRawTag(104);
			output.WriteBool(IsStarMail);
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
		if (Title.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Title);
		}
		if (Context.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Context);
		}
		if (SendName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SendName);
		}
		if (IsRead)
		{
			num += 2;
		}
		if (IsGetReward)
		{
			num += 2;
		}
		if (CreateTime != 0L)
		{
			num += 9;
		}
		num += rewards_.CalculateSize(_map_rewards_codec);
		if (ExpireTime != 0L)
		{
			num += 9;
		}
		if (StartTime != 0L)
		{
			num += 9;
		}
		if (IsStarMail)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MailData other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Title.Length != 0)
			{
				Title = other.Title;
			}
			if (other.Context.Length != 0)
			{
				Context = other.Context;
			}
			if (other.SendName.Length != 0)
			{
				SendName = other.SendName;
			}
			if (other.IsRead)
			{
				IsRead = other.IsRead;
			}
			if (other.IsGetReward)
			{
				IsGetReward = other.IsGetReward;
			}
			if (other.CreateTime != 0L)
			{
				CreateTime = other.CreateTime;
			}
			rewards_.MergeFrom(other.rewards_);
			if (other.ExpireTime != 0L)
			{
				ExpireTime = other.ExpireTime;
			}
			if (other.StartTime != 0L)
			{
				StartTime = other.StartTime;
			}
			if (other.IsStarMail)
			{
				IsStarMail = other.IsStarMail;
			}
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
				Title = input.ReadString();
				break;
			case 26u:
				Context = input.ReadString();
				break;
			case 34u:
				SendName = input.ReadString();
				break;
			case 40u:
				IsRead = input.ReadBool();
				break;
			case 48u:
				IsGetReward = input.ReadBool();
				break;
			case 57u:
				CreateTime = input.ReadSFixed64();
				break;
			case 82u:
				rewards_.AddEntriesFrom(ref input, _map_rewards_codec);
				break;
			case 89u:
				ExpireTime = input.ReadSFixed64();
				break;
			case 97u:
				StartTime = input.ReadSFixed64();
				break;
			case 104u:
				IsStarMail = input.ReadBool();
				break;
			}
		}
	}
}
