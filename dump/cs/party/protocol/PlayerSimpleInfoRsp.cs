using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class PlayerSimpleInfoRsp : IMessage<PlayerSimpleInfoRsp>, IMessage, IEquatable<PlayerSimpleInfoRsp>, IDeepCloneable<PlayerSimpleInfoRsp>, IBufferMessage
{
	private static readonly MessageParser<PlayerSimpleInfoRsp> _parser = new MessageParser<PlayerSimpleInfoRsp>(() => new PlayerSimpleInfoRsp());

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

	public const int IsOnlineFieldNumber = 7;

	private bool isOnline_;

	public const int IsBusyFieldNumber = 8;

	private bool isBusy_;

	public const int SignalScoreFieldNumber = 9;

	private int signalScore_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerSimpleInfoRsp> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[67];

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
	public PlayerSimpleInfoRsp()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerSimpleInfoRsp(PlayerSimpleInfoRsp other)
		: this()
	{
		playerId_ = other.playerId_;
		name_ = other.name_;
		offlineTime_ = other.offlineTime_;
		headIcon_ = other.headIcon_;
		lv_ = other.lv_;
		background_ = other.background_;
		isOnline_ = other.isOnline_;
		isBusy_ = other.isBusy_;
		signalScore_ = other.signalScore_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerSimpleInfoRsp Clone()
	{
		return new PlayerSimpleInfoRsp(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerSimpleInfoRsp);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerSimpleInfoRsp other)
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
		if (IsOnline != other.IsOnline)
		{
			return false;
		}
		if (IsBusy != other.IsBusy)
		{
			return false;
		}
		if (SignalScore != other.SignalScore)
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
		if (IsOnline)
		{
			num ^= IsOnline.GetHashCode();
		}
		if (IsBusy)
		{
			num ^= IsBusy.GetHashCode();
		}
		if (SignalScore != 0)
		{
			num ^= SignalScore.GetHashCode();
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
			output.WriteRawTag(37);
			output.WriteSFixed32(HeadIcon);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Lv);
		}
		if (Background != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Background);
		}
		if (IsOnline)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsOnline);
		}
		if (IsBusy)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsBusy);
		}
		if (SignalScore != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(SignalScore);
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
		if (IsOnline)
		{
			num += 2;
		}
		if (IsBusy)
		{
			num += 2;
		}
		if (SignalScore != 0)
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
	public void MergeFrom(PlayerSimpleInfoRsp other)
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
			if (other.IsOnline)
			{
				IsOnline = other.IsOnline;
			}
			if (other.IsBusy)
			{
				IsBusy = other.IsBusy;
			}
			if (other.SignalScore != 0)
			{
				SignalScore = other.SignalScore;
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
			case 37u:
				HeadIcon = input.ReadSFixed32();
				break;
			case 45u:
				Lv = input.ReadSFixed32();
				break;
			case 53u:
				Background = input.ReadSFixed32();
				break;
			case 56u:
				IsOnline = input.ReadBool();
				break;
			case 64u:
				IsBusy = input.ReadBool();
				break;
			case 77u:
				SignalScore = input.ReadSFixed32();
				break;
			}
		}
	}
}
