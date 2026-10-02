using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GuildMemberChangeNotificationConfigure : IMessage<GuildMemberChangeNotificationConfigure>, IMessage, IEquatable<GuildMemberChangeNotificationConfigure>, IDeepCloneable<GuildMemberChangeNotificationConfigure>, IBufferMessage
{
	private static readonly MessageParser<GuildMemberChangeNotificationConfigure> _parser = new MessageParser<GuildMemberChangeNotificationConfigure>(() => new GuildMemberChangeNotificationConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GuildMemberChangeTypeFieldNumber = 1;

	private GuildMemberChangeType guildMemberChangeType_;

	public const int NotificaitonIDFieldNumber = 2;

	private int notificaitonID_;

	public const int NotificaitonFormFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_notificaitonForm_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> notificaitonForm_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuildMemberChangeNotificationConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuildReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberChangeType GuildMemberChangeType
	{
		get
		{
			return guildMemberChangeType_;
		}
		private set
		{
			guildMemberChangeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NotificaitonID
	{
		get
		{
			return notificaitonID_;
		}
		private set
		{
			notificaitonID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> NotificaitonForm => notificaitonForm_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberChangeNotificationConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberChangeNotificationConfigure(GuildMemberChangeNotificationConfigure other)
		: this()
	{
		guildMemberChangeType_ = other.guildMemberChangeType_;
		notificaitonID_ = other.notificaitonID_;
		notificaitonForm_ = other.notificaitonForm_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberChangeNotificationConfigure Clone()
	{
		return new GuildMemberChangeNotificationConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuildMemberChangeNotificationConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuildMemberChangeNotificationConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GuildMemberChangeType != other.GuildMemberChangeType)
		{
			return false;
		}
		if (NotificaitonID != other.NotificaitonID)
		{
			return false;
		}
		if (!notificaitonForm_.Equals(other.notificaitonForm_))
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
		if (GuildMemberChangeType != GuildMemberChangeType.None)
		{
			num ^= GuildMemberChangeType.GetHashCode();
		}
		if (NotificaitonID != 0)
		{
			num ^= NotificaitonID.GetHashCode();
		}
		num ^= notificaitonForm_.GetHashCode();
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
		if (GuildMemberChangeType != GuildMemberChangeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)GuildMemberChangeType);
		}
		if (NotificaitonID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NotificaitonID);
		}
		notificaitonForm_.WriteTo(ref output, _repeated_notificaitonForm_codec);
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
		if (GuildMemberChangeType != GuildMemberChangeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GuildMemberChangeType);
		}
		if (NotificaitonID != 0)
		{
			num += 5;
		}
		num += notificaitonForm_.CalculateSize(_repeated_notificaitonForm_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GuildMemberChangeNotificationConfigure other)
	{
		if (other != null)
		{
			if (other.GuildMemberChangeType != GuildMemberChangeType.None)
			{
				GuildMemberChangeType = other.GuildMemberChangeType;
			}
			if (other.NotificaitonID != 0)
			{
				NotificaitonID = other.NotificaitonID;
			}
			notificaitonForm_.Add(other.notificaitonForm_);
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
			case 8u:
				GuildMemberChangeType = (GuildMemberChangeType)input.ReadEnum();
				break;
			case 21u:
				NotificaitonID = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				notificaitonForm_.AddEntriesFrom(ref input, _repeated_notificaitonForm_codec);
				break;
			}
		}
	}
}
