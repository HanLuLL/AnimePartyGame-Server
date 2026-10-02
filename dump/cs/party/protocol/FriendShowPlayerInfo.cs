using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class FriendShowPlayerInfo : IMessage<FriendShowPlayerInfo>, IMessage, IEquatable<FriendShowPlayerInfo>, IDeepCloneable<FriendShowPlayerInfo>, IBufferMessage
{
	private static readonly MessageParser<FriendShowPlayerInfo> _parser = new MessageParser<FriendShowPlayerInfo>(() => new FriendShowPlayerInfo());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int NameFieldNumber = 2;

	private string name_ = "";

	public const int HeadIconFieldNumber = 3;

	private int headIcon_;

	public const int LvFieldNumber = 4;

	private int lv_;

	public const int BackgroundFieldNumber = 5;

	private int background_;

	public const int TimeFieldNumber = 6;

	private long time_;

	public const int OfflineTimeFieldNumber = 7;

	private long offlineTime_;

	public const int IsOnlineFieldNumber = 8;

	private bool isOnline_;

	public const int IsBusyFieldNumber = 9;

	private bool isBusy_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendShowPlayerInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[104];

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
	public long Time
	{
		get
		{
			return time_;
		}
		set
		{
			time_ = value;
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
	public bool IsOnline
	{
		get
		{
			return isOnline_;
		}
		set
		{
			isOnline_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBusy
	{
		get
		{
			return isBusy_;
		}
		set
		{
			isBusy_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendShowPlayerInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendShowPlayerInfo(FriendShowPlayerInfo other)
		: this()
	{
		playerId_ = other.playerId_;
		name_ = other.name_;
		headIcon_ = other.headIcon_;
		lv_ = other.lv_;
		background_ = other.background_;
		time_ = other.time_;
		offlineTime_ = other.offlineTime_;
		isOnline_ = other.isOnline_;
		isBusy_ = other.isBusy_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendShowPlayerInfo Clone()
	{
		return new FriendShowPlayerInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendShowPlayerInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendShowPlayerInfo other)
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
		if (Time != other.Time)
		{
			return false;
		}
		if (OfflineTime != other.OfflineTime)
		{
			return false;
		}
		if (IsOnline != other.IsOnline)
		{
			return false;
		}
		if (IsBusy != other.IsBusy)
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
		if (Time != 0L)
		{
			num ^= Time.GetHashCode();
		}
		if (OfflineTime != 0L)
		{
			num ^= OfflineTime.GetHashCode();
		}
		if (IsOnline)
		{
			num ^= IsOnline.GetHashCode();
		}
		if (IsBusy)
		{
			num ^= IsBusy.GetHashCode();
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
		if (HeadIcon != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(HeadIcon);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Lv);
		}
		if (Background != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Background);
		}
		if (Time != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(Time);
		}
		if (OfflineTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(OfflineTime);
		}
		if (IsOnline)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsOnline);
		}
		if (IsBusy)
		{
			output.WriteRawTag(72);
			output.WriteBool(IsBusy);
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
		if (HeadIcon != 0)
		{
			num += 5;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (Background != 0)
		{
			num += 5;
		}
		if (Time != 0L)
		{
			num += 9;
		}
		if (OfflineTime != 0L)
		{
			num += 9;
		}
		if (IsOnline)
		{
			num += 2;
		}
		if (IsBusy)
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
	public void MergeFrom(FriendShowPlayerInfo other)
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
			if (other.Time != 0L)
			{
				Time = other.Time;
			}
			if (other.OfflineTime != 0L)
			{
				OfflineTime = other.OfflineTime;
			}
			if (other.IsOnline)
			{
				IsOnline = other.IsOnline;
			}
			if (other.IsBusy)
			{
				IsBusy = other.IsBusy;
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
			case 29u:
				HeadIcon = input.ReadSFixed32();
				break;
			case 37u:
				Lv = input.ReadSFixed32();
				break;
			case 45u:
				Background = input.ReadSFixed32();
				break;
			case 49u:
				Time = input.ReadSFixed64();
				break;
			case 57u:
				OfflineTime = input.ReadSFixed64();
				break;
			case 64u:
				IsOnline = input.ReadBool();
				break;
			case 72u:
				IsBusy = input.ReadBool();
				break;
			}
		}
	}
}
