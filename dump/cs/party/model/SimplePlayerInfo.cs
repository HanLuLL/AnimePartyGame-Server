using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SimplePlayerInfo : IMessage<SimplePlayerInfo>, IMessage, IEquatable<SimplePlayerInfo>, IDeepCloneable<SimplePlayerInfo>, IBufferMessage
{
	private static readonly MessageParser<SimplePlayerInfo> _parser = new MessageParser<SimplePlayerInfo>(() => new SimplePlayerInfo());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int OfflineTimeFieldNumber = 3;

	private long offlineTime_;

	public const int HeadIconFieldNumber = 4;

	private int headIcon_;

	public const int LvFieldNumber = 5;

	private int lv_;

	public const int BackgroundFieldNumber = 6;

	private int background_;

	public const int SignalScoreFieldNumber = 7;

	private int signalScore_;

	public const int UpdateTimeFieldNumber = 8;

	private long updateTime_;

	public const int OnlineStatusFieldNumber = 9;

	private int onlineStatus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SimplePlayerInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[101];

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
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OfflineTime
	{
		get
		{
			return offlineTime_;
		}
		set
		{
			offlineTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeadIcon
	{
		get
		{
			return headIcon_;
		}
		set
		{
			headIcon_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Background
	{
		get
		{
			return background_;
		}
		set
		{
			background_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SignalScore
	{
		get
		{
			return signalScore_;
		}
		set
		{
			signalScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long UpdateTime
	{
		get
		{
			return updateTime_;
		}
		set
		{
			updateTime_ = value;
		}
	}

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
	public SimplePlayerInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SimplePlayerInfo(SimplePlayerInfo other)
		: this()
	{
		playerId_ = other.playerId_;
		name_ = other.name_;
		offlineTime_ = other.offlineTime_;
		headIcon_ = other.headIcon_;
		lv_ = other.lv_;
		background_ = other.background_;
		signalScore_ = other.signalScore_;
		updateTime_ = other.updateTime_;
		onlineStatus_ = other.onlineStatus_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SimplePlayerInfo Clone()
	{
		return new SimplePlayerInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SimplePlayerInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SimplePlayerInfo other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (OfflineTime != other.OfflineTime)
		{
			return false;
		}
		if (HeadIcon != other.HeadIcon)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (Background != other.Background)
		{
			return false;
		}
		if (SignalScore != other.SignalScore)
		{
			return false;
		}
		if (UpdateTime != other.UpdateTime)
		{
			return false;
		}
		if (OnlineStatus != other.OnlineStatus)
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (OfflineTime != 0L)
		{
			num ^= OfflineTime.GetHashCode();
		}
		if (HeadIcon != 0)
		{
			num ^= HeadIcon.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (Background != 0)
		{
			num ^= Background.GetHashCode();
		}
		if (SignalScore != 0)
		{
			num ^= SignalScore.GetHashCode();
		}
		if (UpdateTime != 0L)
		{
			num ^= UpdateTime.GetHashCode();
		}
		if (OnlineStatus != 0)
		{
			num ^= OnlineStatus.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (Name.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Name);
		}
		if (OfflineTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(OfflineTime);
		}
		if (HeadIcon != 0)
		{
			output.WriteRawTag(32);
			output.WriteInt32(HeadIcon);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(40);
			output.WriteInt32(Lv);
		}
		if (Background != 0)
		{
			output.WriteRawTag(48);
			output.WriteInt32(Background);
		}
		if (SignalScore != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(SignalScore);
		}
		if (UpdateTime != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(UpdateTime);
		}
		if (OnlineStatus != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(OnlineStatus);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (OfflineTime != 0L)
		{
			num += 9;
		}
		if (HeadIcon != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(HeadIcon);
		}
		if (Lv != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Lv);
		}
		if (Background != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Background);
		}
		if (SignalScore != 0)
		{
			num += 5;
		}
		if (UpdateTime != 0L)
		{
			num += 9;
		}
		if (OnlineStatus != 0)
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
	public void MergeFrom(SimplePlayerInfo other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Name.Length != 0)
			{
				Name = other.Name;
			}
			if (other.OfflineTime != 0L)
			{
				OfflineTime = other.OfflineTime;
			}
			if (other.HeadIcon != 0)
			{
				HeadIcon = other.HeadIcon;
			}
			if (other.Lv != 0)
			{
				Lv = other.Lv;
			}
			if (other.Background != 0)
			{
				Background = other.Background;
			}
			if (other.SignalScore != 0)
			{
				SignalScore = other.SignalScore;
			}
			if (other.UpdateTime != 0L)
			{
				UpdateTime = other.UpdateTime;
			}
			if (other.OnlineStatus != 0)
			{
				OnlineStatus = other.OnlineStatus;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 18u:
				Name = input.ReadString();
				break;
			case 25u:
				OfflineTime = input.ReadSFixed64();
				break;
			case 32u:
				HeadIcon = input.ReadInt32();
				break;
			case 40u:
				Lv = input.ReadInt32();
				break;
			case 48u:
				Background = input.ReadInt32();
				break;
			case 61u:
				SignalScore = input.ReadSFixed32();
				break;
			case 65u:
				UpdateTime = input.ReadSFixed64();
				break;
			case 77u:
				OnlineStatus = input.ReadSFixed32();
				break;
			}
		}
	}
}
