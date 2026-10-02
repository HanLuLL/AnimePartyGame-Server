using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GuildMemberChangeMsg : IMessage<GuildMemberChangeMsg>, IMessage, IEquatable<GuildMemberChangeMsg>, IDeepCloneable<GuildMemberChangeMsg>, IBufferMessage
{
	private static readonly MessageParser<GuildMemberChangeMsg> _parser = new MessageParser<GuildMemberChangeMsg>(() => new GuildMemberChangeMsg());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private long id_;

	public const int NotificationIdFieldNumber = 2;

	private int notificationId_;

	public const int ParamFieldNumber = 3;

	private static readonly FieldCodec<string> _repeated_param_codec = FieldCodec.ForString(26u);

	private readonly RepeatedField<string> param_ = new RepeatedField<string>();

	public const int TimeFieldNumber = 4;

	private long time_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuildMemberChangeMsg> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[117];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Id
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
	public int NotificationId
	{
		get
		{
			return notificationId_;
		}
		set
		{
			notificationId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Param => param_;

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
	public GuildMemberChangeMsg()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberChangeMsg(GuildMemberChangeMsg other)
		: this()
	{
		id_ = other.id_;
		notificationId_ = other.notificationId_;
		param_ = other.param_.Clone();
		time_ = other.time_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberChangeMsg Clone()
	{
		return new GuildMemberChangeMsg(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuildMemberChangeMsg);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuildMemberChangeMsg other)
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
		if (NotificationId != other.NotificationId)
		{
			return false;
		}
		if (!param_.Equals(other.param_))
		{
			return false;
		}
		if (Time != other.Time)
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
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
		}
		if (NotificationId != 0)
		{
			num ^= NotificationId.GetHashCode();
		}
		num ^= param_.GetHashCode();
		if (Time != 0L)
		{
			num ^= Time.GetHashCode();
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
		if (Id != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Id);
		}
		if (NotificationId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NotificationId);
		}
		param_.WriteTo(ref output, _repeated_param_codec);
		if (Time != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(Time);
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
		if (Id != 0L)
		{
			num += 9;
		}
		if (NotificationId != 0)
		{
			num += 5;
		}
		num += param_.CalculateSize(_repeated_param_codec);
		if (Time != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GuildMemberChangeMsg other)
	{
		if (other != null)
		{
			if (other.Id != 0L)
			{
				Id = other.Id;
			}
			if (other.NotificationId != 0)
			{
				NotificationId = other.NotificationId;
			}
			param_.Add(other.param_);
			if (other.Time != 0L)
			{
				Time = other.Time;
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
				Id = input.ReadSFixed64();
				break;
			case 21u:
				NotificationId = input.ReadSFixed32();
				break;
			case 26u:
				param_.AddEntriesFrom(ref input, _repeated_param_codec);
				break;
			case 33u:
				Time = input.ReadSFixed64();
				break;
			}
		}
	}
}
