using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class AudioEventConfigure : IMessage<AudioEventConfigure>, IMessage, IEquatable<AudioEventConfigure>, IDeepCloneable<AudioEventConfigure>, IBufferMessage
{
	private static readonly MessageParser<AudioEventConfigure> _parser = new MessageParser<AudioEventConfigure>(() => new AudioEventConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int EventNameFieldNumber = 2;

	private string eventName_ = "";

	public const int EventIDFieldNumber = 3;

	private uint eventID_;

	public const int IsBlockFieldNumber = 4;

	private bool isBlock_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AudioEventConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AudioReflection.Descriptor.MessageTypes[1];

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
	public string EventName
	{
		get
		{
			return eventName_;
		}
		private set
		{
			eventName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public uint EventID
	{
		get
		{
			return eventID_;
		}
		private set
		{
			eventID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBlock
	{
		get
		{
			return isBlock_;
		}
		private set
		{
			isBlock_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AudioEventConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AudioEventConfigure(AudioEventConfigure other)
		: this()
	{
		id_ = other.id_;
		eventName_ = other.eventName_;
		eventID_ = other.eventID_;
		isBlock_ = other.isBlock_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AudioEventConfigure Clone()
	{
		return new AudioEventConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AudioEventConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AudioEventConfigure other)
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
		if (EventName != other.EventName)
		{
			return false;
		}
		if (EventID != other.EventID)
		{
			return false;
		}
		if (IsBlock != other.IsBlock)
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
		if (EventName.Length != 0)
		{
			num ^= EventName.GetHashCode();
		}
		if (EventID != 0)
		{
			num ^= EventID.GetHashCode();
		}
		if (IsBlock)
		{
			num ^= IsBlock.GetHashCode();
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
		if (EventName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(EventName);
		}
		if (EventID != 0)
		{
			output.WriteRawTag(29);
			output.WriteFixed32(EventID);
		}
		if (IsBlock)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsBlock);
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
		if (EventName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(EventName);
		}
		if (EventID != 0)
		{
			num += 5;
		}
		if (IsBlock)
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
	public void MergeFrom(AudioEventConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.EventName.Length != 0)
			{
				EventName = other.EventName;
			}
			if (other.EventID != 0)
			{
				EventID = other.EventID;
			}
			if (other.IsBlock)
			{
				IsBlock = other.IsBlock;
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
				EventName = input.ReadString();
				break;
			case 29u:
				EventID = input.ReadFixed32();
				break;
			case 32u:
				IsBlock = input.ReadBool();
				break;
			}
		}
	}
}
