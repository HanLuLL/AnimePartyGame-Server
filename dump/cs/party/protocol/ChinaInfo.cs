using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChinaInfo : IMessage<ChinaInfo>, IMessage, IEquatable<ChinaInfo>, IDeepCloneable<ChinaInfo>, IBufferMessage
{
	private static readonly MessageParser<ChinaInfo> _parser = new MessageParser<ChinaInfo>(() => new ChinaInfo());

	private UnknownFieldSet _unknownFields;

	public const int GameIdFieldNumber = 1;

	private string gameId_ = "";

	public const int ChannelIdFieldNumber = 2;

	private string channelId_ = "";

	public const int AppIdFieldNumber = 3;

	private string appId_ = "";

	public const int SidFieldNumber = 4;

	private string sid_ = "";

	public const int ExtraFieldNumber = 5;

	private string extra_ = "";

	public const int DeviceIdFieldNumber = 6;

	private string deviceId_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChinaInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string GameId
	{
		get
		{
			return gameId_;
		}
		set
		{
			gameId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ChannelId
	{
		get
		{
			return channelId_;
		}
		set
		{
			channelId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AppId
	{
		get
		{
			return appId_;
		}
		set
		{
			appId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Sid
	{
		get
		{
			return sid_;
		}
		set
		{
			sid_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Extra
	{
		get
		{
			return extra_;
		}
		set
		{
			extra_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaInfo(ChinaInfo other)
		: this()
	{
		gameId_ = other.gameId_;
		channelId_ = other.channelId_;
		appId_ = other.appId_;
		sid_ = other.sid_;
		extra_ = other.extra_;
		deviceId_ = other.deviceId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaInfo Clone()
	{
		return new ChinaInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChinaInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChinaInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GameId != other.GameId)
		{
			return false;
		}
		if (ChannelId != other.ChannelId)
		{
			return false;
		}
		if (AppId != other.AppId)
		{
			return false;
		}
		if (Sid != other.Sid)
		{
			return false;
		}
		if (Extra != other.Extra)
		{
			return false;
		}
		if (DeviceId != other.DeviceId)
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
		if (GameId.Length != 0)
		{
			num ^= GameId.GetHashCode();
		}
		if (ChannelId.Length != 0)
		{
			num ^= ChannelId.GetHashCode();
		}
		if (AppId.Length != 0)
		{
			num ^= AppId.GetHashCode();
		}
		if (Sid.Length != 0)
		{
			num ^= Sid.GetHashCode();
		}
		if (Extra.Length != 0)
		{
			num ^= Extra.GetHashCode();
		}
		if (DeviceId.Length != 0)
		{
			num ^= DeviceId.GetHashCode();
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
		if (GameId.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(GameId);
		}
		if (ChannelId.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(ChannelId);
		}
		if (AppId.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(AppId);
		}
		if (Sid.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Sid);
		}
		if (Extra.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Extra);
		}
		if (DeviceId.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(DeviceId);
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
		if (GameId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(GameId);
		}
		if (ChannelId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ChannelId);
		}
		if (AppId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AppId);
		}
		if (Sid.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Sid);
		}
		if (Extra.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Extra);
		}
		if (DeviceId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DeviceId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChinaInfo other)
	{
		if (other != null)
		{
			if (other.GameId.Length != 0)
			{
				GameId = other.GameId;
			}
			if (other.ChannelId.Length != 0)
			{
				ChannelId = other.ChannelId;
			}
			if (other.AppId.Length != 0)
			{
				AppId = other.AppId;
			}
			if (other.Sid.Length != 0)
			{
				Sid = other.Sid;
			}
			if (other.Extra.Length != 0)
			{
				Extra = other.Extra;
			}
			if (other.DeviceId.Length != 0)
			{
				DeviceId = other.DeviceId;
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
				GameId = input.ReadString();
				break;
			case 18u:
				ChannelId = input.ReadString();
				break;
			case 26u:
				AppId = input.ReadString();
				break;
			case 34u:
				Sid = input.ReadString();
				break;
			case 42u:
				Extra = input.ReadString();
				break;
			case 50u:
				DeviceId = input.ReadString();
				break;
			}
		}
	}
}
