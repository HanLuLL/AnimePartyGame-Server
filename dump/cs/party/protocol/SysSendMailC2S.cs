using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysSendMailC2S : IMessage<SysSendMailC2S>, IMessage, IEquatable<SysSendMailC2S>, IDeepCloneable<SysSendMailC2S>, IBufferMessage
{
	private static readonly MessageParser<SysSendMailC2S> _parser = new MessageParser<SysSendMailC2S>(() => new SysSendMailC2S());

	private UnknownFieldSet _unknownFields;

	public const int TitleFieldNumber = 1;

	private string title_ = "";

	public const int SendNameFieldNumber = 2;

	private string sendName_ = "";

	public const int KeyFieldNumber = 3;

	private string key_ = "";

	public const int ContextFieldNumber = 4;

	private string context_ = "";

	public const int PlayerIdFieldNumber = 5;

	private long playerId_;

	public const int ServerAllIdFieldNumber = 6;

	private long serverAllId_;

	public const int RewardsFieldNumber = 7;

	private static readonly MapField<int, int>.Codec _map_rewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 58u);

	private readonly MapField<int, int> rewards_ = new MapField<int, int>();

	public const int SendTimeFieldNumber = 8;

	private long sendTime_;

	public const int ExpireTimeFieldNumber = 9;

	private long expireTime_;

	public const int StartTimeFieldNumber = 10;

	private long startTime_;

	public const int IsStarMailFieldNumber = 11;

	private bool isStarMail_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysSendMailC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[472];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string Key
	{
		get
		{
			return key_;
		}
		set
		{
			key_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public long ServerAllId
	{
		get
		{
			return serverAllId_;
		}
		set
		{
			serverAllId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Rewards => rewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long SendTime
	{
		get
		{
			return sendTime_;
		}
		set
		{
			sendTime_ = value;
		}
	}

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
	public SysSendMailC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSendMailC2S(SysSendMailC2S other)
		: this()
	{
		title_ = other.title_;
		sendName_ = other.sendName_;
		key_ = other.key_;
		context_ = other.context_;
		playerId_ = other.playerId_;
		serverAllId_ = other.serverAllId_;
		rewards_ = other.rewards_.Clone();
		sendTime_ = other.sendTime_;
		expireTime_ = other.expireTime_;
		startTime_ = other.startTime_;
		isStarMail_ = other.isStarMail_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSendMailC2S Clone()
	{
		return new SysSendMailC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysSendMailC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysSendMailC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Title != other.Title)
		{
			return false;
		}
		if (SendName != other.SendName)
		{
			return false;
		}
		if (Key != other.Key)
		{
			return false;
		}
		if (Context != other.Context)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (ServerAllId != other.ServerAllId)
		{
			return false;
		}
		if (!Rewards.Equals(other.Rewards))
		{
			return false;
		}
		if (SendTime != other.SendTime)
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
		if (Title.Length != 0)
		{
			num ^= Title.GetHashCode();
		}
		if (SendName.Length != 0)
		{
			num ^= SendName.GetHashCode();
		}
		if (Key.Length != 0)
		{
			num ^= Key.GetHashCode();
		}
		if (Context.Length != 0)
		{
			num ^= Context.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (ServerAllId != 0L)
		{
			num ^= ServerAllId.GetHashCode();
		}
		num ^= Rewards.GetHashCode();
		if (SendTime != 0L)
		{
			num ^= SendTime.GetHashCode();
		}
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
		if (Title.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Title);
		}
		if (SendName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(SendName);
		}
		if (Key.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Key);
		}
		if (Context.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Context);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(PlayerId);
		}
		if (ServerAllId != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(ServerAllId);
		}
		rewards_.WriteTo(ref output, _map_rewards_codec);
		if (SendTime != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(SendTime);
		}
		if (ExpireTime != 0L)
		{
			output.WriteRawTag(73);
			output.WriteSFixed64(ExpireTime);
		}
		if (StartTime != 0L)
		{
			output.WriteRawTag(81);
			output.WriteSFixed64(StartTime);
		}
		if (IsStarMail)
		{
			output.WriteRawTag(88);
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
		if (Title.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Title);
		}
		if (SendName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SendName);
		}
		if (Key.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Key);
		}
		if (Context.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Context);
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (ServerAllId != 0L)
		{
			num += 9;
		}
		num += rewards_.CalculateSize(_map_rewards_codec);
		if (SendTime != 0L)
		{
			num += 9;
		}
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
	public void MergeFrom(SysSendMailC2S other)
	{
		if (other != null)
		{
			if (other.Title.Length != 0)
			{
				Title = other.Title;
			}
			if (other.SendName.Length != 0)
			{
				SendName = other.SendName;
			}
			if (other.Key.Length != 0)
			{
				Key = other.Key;
			}
			if (other.Context.Length != 0)
			{
				Context = other.Context;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.ServerAllId != 0L)
			{
				ServerAllId = other.ServerAllId;
			}
			rewards_.MergeFrom(other.rewards_);
			if (other.SendTime != 0L)
			{
				SendTime = other.SendTime;
			}
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
			case 10u:
				Title = input.ReadString();
				break;
			case 18u:
				SendName = input.ReadString();
				break;
			case 26u:
				Key = input.ReadString();
				break;
			case 34u:
				Context = input.ReadString();
				break;
			case 41u:
				PlayerId = input.ReadSFixed64();
				break;
			case 49u:
				ServerAllId = input.ReadSFixed64();
				break;
			case 58u:
				rewards_.AddEntriesFrom(ref input, _map_rewards_codec);
				break;
			case 65u:
				SendTime = input.ReadSFixed64();
				break;
			case 73u:
				ExpireTime = input.ReadSFixed64();
				break;
			case 81u:
				StartTime = input.ReadSFixed64();
				break;
			case 88u:
				IsStarMail = input.ReadBool();
				break;
			}
		}
	}
}
