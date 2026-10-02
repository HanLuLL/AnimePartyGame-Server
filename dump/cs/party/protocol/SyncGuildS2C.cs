using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SyncGuildS2C : IMessage<SyncGuildS2C>, IMessage, IEquatable<SyncGuildS2C>, IDeepCloneable<SyncGuildS2C>, IBufferMessage
{
	private static readonly MessageParser<SyncGuildS2C> _parser = new MessageParser<SyncGuildS2C>(() => new SyncGuildS2C());

	private UnknownFieldSet _unknownFields;

	public const int TagIdsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_tagIds_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> tagIds_ = new RepeatedField<int>();

	public const int ExAnnouncementFieldNumber = 2;

	private string exAnnouncement_ = "";

	public const int LastExternalEditTimeFieldNumber = 3;

	private long lastExternalEditTime_;

	public const int LastExternalEditorIdFieldNumber = 4;

	private long lastExternalEditorId_;

	public const int ExternalEditCountFieldNumber = 5;

	private int externalEditCount_;

	public const int InAnnouncementFieldNumber = 6;

	private string inAnnouncement_ = "";

	public const int LastInternalEditTimeFieldNumber = 7;

	private long lastInternalEditTime_;

	public const int LastInternalEditorIdFieldNumber = 8;

	private long lastInternalEditorId_;

	public const int InternalEditCountFieldNumber = 9;

	private int internalEditCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncGuildS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[557];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TagIds => tagIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ExAnnouncement
	{
		get
		{
			return exAnnouncement_;
		}
		set
		{
			exAnnouncement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastExternalEditTime
	{
		get
		{
			return lastExternalEditTime_;
		}
		set
		{
			lastExternalEditTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastExternalEditorId
	{
		get
		{
			return lastExternalEditorId_;
		}
		set
		{
			lastExternalEditorId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExternalEditCount
	{
		get
		{
			return externalEditCount_;
		}
		set
		{
			externalEditCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string InAnnouncement
	{
		get
		{
			return inAnnouncement_;
		}
		set
		{
			inAnnouncement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastInternalEditTime
	{
		get
		{
			return lastInternalEditTime_;
		}
		set
		{
			lastInternalEditTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastInternalEditorId
	{
		get
		{
			return lastInternalEditorId_;
		}
		set
		{
			lastInternalEditorId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int InternalEditCount
	{
		get
		{
			return internalEditCount_;
		}
		set
		{
			internalEditCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildS2C(SyncGuildS2C other)
		: this()
	{
		tagIds_ = other.tagIds_.Clone();
		exAnnouncement_ = other.exAnnouncement_;
		lastExternalEditTime_ = other.lastExternalEditTime_;
		lastExternalEditorId_ = other.lastExternalEditorId_;
		externalEditCount_ = other.externalEditCount_;
		inAnnouncement_ = other.inAnnouncement_;
		lastInternalEditTime_ = other.lastInternalEditTime_;
		lastInternalEditorId_ = other.lastInternalEditorId_;
		internalEditCount_ = other.internalEditCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildS2C Clone()
	{
		return new SyncGuildS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncGuildS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncGuildS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!tagIds_.Equals(other.tagIds_))
		{
			return false;
		}
		if (ExAnnouncement != other.ExAnnouncement)
		{
			return false;
		}
		if (LastExternalEditTime != other.LastExternalEditTime)
		{
			return false;
		}
		if (LastExternalEditorId != other.LastExternalEditorId)
		{
			return false;
		}
		if (ExternalEditCount != other.ExternalEditCount)
		{
			return false;
		}
		if (InAnnouncement != other.InAnnouncement)
		{
			return false;
		}
		if (LastInternalEditTime != other.LastInternalEditTime)
		{
			return false;
		}
		if (LastInternalEditorId != other.LastInternalEditorId)
		{
			return false;
		}
		if (InternalEditCount != other.InternalEditCount)
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
		num ^= tagIds_.GetHashCode();
		if (ExAnnouncement.Length != 0)
		{
			num ^= ExAnnouncement.GetHashCode();
		}
		if (LastExternalEditTime != 0L)
		{
			num ^= LastExternalEditTime.GetHashCode();
		}
		if (LastExternalEditorId != 0L)
		{
			num ^= LastExternalEditorId.GetHashCode();
		}
		if (ExternalEditCount != 0)
		{
			num ^= ExternalEditCount.GetHashCode();
		}
		if (InAnnouncement.Length != 0)
		{
			num ^= InAnnouncement.GetHashCode();
		}
		if (LastInternalEditTime != 0L)
		{
			num ^= LastInternalEditTime.GetHashCode();
		}
		if (LastInternalEditorId != 0L)
		{
			num ^= LastInternalEditorId.GetHashCode();
		}
		if (InternalEditCount != 0)
		{
			num ^= InternalEditCount.GetHashCode();
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
		tagIds_.WriteTo(ref output, _repeated_tagIds_codec);
		if (ExAnnouncement.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(ExAnnouncement);
		}
		if (LastExternalEditTime != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(LastExternalEditTime);
		}
		if (LastExternalEditorId != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(LastExternalEditorId);
		}
		if (ExternalEditCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(ExternalEditCount);
		}
		if (InAnnouncement.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(InAnnouncement);
		}
		if (LastInternalEditTime != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(LastInternalEditTime);
		}
		if (LastInternalEditorId != 0L)
		{
			output.WriteRawTag(65);
			output.WriteSFixed64(LastInternalEditorId);
		}
		if (InternalEditCount != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(InternalEditCount);
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
		num += tagIds_.CalculateSize(_repeated_tagIds_codec);
		if (ExAnnouncement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ExAnnouncement);
		}
		if (LastExternalEditTime != 0L)
		{
			num += 9;
		}
		if (LastExternalEditorId != 0L)
		{
			num += 9;
		}
		if (ExternalEditCount != 0)
		{
			num += 5;
		}
		if (InAnnouncement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InAnnouncement);
		}
		if (LastInternalEditTime != 0L)
		{
			num += 9;
		}
		if (LastInternalEditorId != 0L)
		{
			num += 9;
		}
		if (InternalEditCount != 0)
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
	public void MergeFrom(SyncGuildS2C other)
	{
		if (other != null)
		{
			tagIds_.Add(other.tagIds_);
			if (other.ExAnnouncement.Length != 0)
			{
				ExAnnouncement = other.ExAnnouncement;
			}
			if (other.LastExternalEditTime != 0L)
			{
				LastExternalEditTime = other.LastExternalEditTime;
			}
			if (other.LastExternalEditorId != 0L)
			{
				LastExternalEditorId = other.LastExternalEditorId;
			}
			if (other.ExternalEditCount != 0)
			{
				ExternalEditCount = other.ExternalEditCount;
			}
			if (other.InAnnouncement.Length != 0)
			{
				InAnnouncement = other.InAnnouncement;
			}
			if (other.LastInternalEditTime != 0L)
			{
				LastInternalEditTime = other.LastInternalEditTime;
			}
			if (other.LastInternalEditorId != 0L)
			{
				LastInternalEditorId = other.LastInternalEditorId;
			}
			if (other.InternalEditCount != 0)
			{
				InternalEditCount = other.InternalEditCount;
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
			case 13u:
				tagIds_.AddEntriesFrom(ref input, _repeated_tagIds_codec);
				break;
			case 18u:
				ExAnnouncement = input.ReadString();
				break;
			case 25u:
				LastExternalEditTime = input.ReadSFixed64();
				break;
			case 33u:
				LastExternalEditorId = input.ReadSFixed64();
				break;
			case 45u:
				ExternalEditCount = input.ReadSFixed32();
				break;
			case 50u:
				InAnnouncement = input.ReadString();
				break;
			case 57u:
				LastInternalEditTime = input.ReadSFixed64();
				break;
			case 65u:
				LastInternalEditorId = input.ReadSFixed64();
				break;
			case 77u:
				InternalEditCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
