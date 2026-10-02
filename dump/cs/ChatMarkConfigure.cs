using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using UI;

public sealed class ChatMarkConfigure : IMessage<ChatMarkConfigure>, IMessage, IEquatable<ChatMarkConfigure>, IDeepCloneable<ChatMarkConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChatMarkConfigure> _parser = new MessageParser<ChatMarkConfigure>(() => new ChatMarkConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IDFieldNumber = 1;

	private int iD_;

	public const int ChatIDFieldNumber = 2;

	private int chatID_;

	public const int EffctIDFieldNumber = 3;

	private int effctID_;

	public const int IconFieldNumber = 4;

	private string icon_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChatMarkConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChatReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ID
	{
		get
		{
			return iD_;
		}
		private set
		{
			iD_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChatID
	{
		get
		{
			return chatID_;
		}
		private set
		{
			chatID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EffctID
	{
		get
		{
			return effctID_;
		}
		private set
		{
			effctID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	public string ChatInfo => chatID_.GetLocal(UIStringType.Chat);

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMarkConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMarkConfigure(ChatMarkConfigure other)
		: this()
	{
		iD_ = other.iD_;
		chatID_ = other.chatID_;
		effctID_ = other.effctID_;
		icon_ = other.icon_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMarkConfigure Clone()
	{
		return new ChatMarkConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChatMarkConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChatMarkConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ID != other.ID)
		{
			return false;
		}
		if (ChatID != other.ChatID)
		{
			return false;
		}
		if (EffctID != other.EffctID)
		{
			return false;
		}
		if (Icon != other.Icon)
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
		if (ID != 0)
		{
			num ^= ID.GetHashCode();
		}
		if (ChatID != 0)
		{
			num ^= ChatID.GetHashCode();
		}
		if (EffctID != 0)
		{
			num ^= EffctID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
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
		if (ID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ID);
		}
		if (ChatID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ChatID);
		}
		if (EffctID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(EffctID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Icon);
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
		if (ID != 0)
		{
			num += 5;
		}
		if (ChatID != 0)
		{
			num += 5;
		}
		if (EffctID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChatMarkConfigure other)
	{
		if (other != null)
		{
			if (other.ID != 0)
			{
				ID = other.ID;
			}
			if (other.ChatID != 0)
			{
				ChatID = other.ChatID;
			}
			if (other.EffctID != 0)
			{
				EffctID = other.EffctID;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
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
				ID = input.ReadSFixed32();
				break;
			case 21u:
				ChatID = input.ReadSFixed32();
				break;
			case 29u:
				EffctID = input.ReadSFixed32();
				break;
			case 34u:
				Icon = input.ReadString();
				break;
			}
		}
	}
}
