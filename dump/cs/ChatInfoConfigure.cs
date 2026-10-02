using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UI;

public sealed class ChatInfoConfigure : IMessage<ChatInfoConfigure>, IMessage, IEquatable<ChatInfoConfigure>, IDeepCloneable<ChatInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChatInfoConfigure> _parser = new MessageParser<ChatInfoConfigure>(() => new ChatInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IDFieldNumber = 1;

	private int iD_;

	public const int ChatIDFieldNumber = 2;

	private int chatID_;

	public const int ChatTypeFieldNumber = 3;

	private static readonly FieldCodec<ChatType> _repeated_chatType_codec = FieldCodec.ForEnum(26u, (ChatType x) => (int)x, (int x) => (ChatType)x);

	private readonly RepeatedField<ChatType> chatType_ = new RepeatedField<ChatType>();

	public const int MapModeTypeFieldNumber = 4;

	private static readonly FieldCodec<MapModeType> _repeated_mapModeType_codec = FieldCodec.ForEnum(34u, (MapModeType x) => (int)x, (int x) => (MapModeType)x);

	private readonly RepeatedField<MapModeType> mapModeType_ = new RepeatedField<MapModeType>();

	public const int MapLimitFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_mapLimit_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> mapLimit_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChatInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChatReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<ChatType> ChatType => chatType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapModeType> MapModeType => mapModeType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapLimit => mapLimit_;

	public string ChatInfo => chatID_.GetLocal(UIStringType.Chat);

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatInfoConfigure(ChatInfoConfigure other)
		: this()
	{
		iD_ = other.iD_;
		chatID_ = other.chatID_;
		chatType_ = other.chatType_.Clone();
		mapModeType_ = other.mapModeType_.Clone();
		mapLimit_ = other.mapLimit_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatInfoConfigure Clone()
	{
		return new ChatInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChatInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChatInfoConfigure other)
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
		if (!chatType_.Equals(other.chatType_))
		{
			return false;
		}
		if (!mapModeType_.Equals(other.mapModeType_))
		{
			return false;
		}
		if (!mapLimit_.Equals(other.mapLimit_))
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
		num ^= chatType_.GetHashCode();
		num ^= mapModeType_.GetHashCode();
		num ^= mapLimit_.GetHashCode();
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
		chatType_.WriteTo(ref output, _repeated_chatType_codec);
		mapModeType_.WriteTo(ref output, _repeated_mapModeType_codec);
		mapLimit_.WriteTo(ref output, _repeated_mapLimit_codec);
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
		num += chatType_.CalculateSize(_repeated_chatType_codec);
		num += mapModeType_.CalculateSize(_repeated_mapModeType_codec);
		num += mapLimit_.CalculateSize(_repeated_mapLimit_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChatInfoConfigure other)
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
			chatType_.Add(other.chatType_);
			mapModeType_.Add(other.mapModeType_);
			mapLimit_.Add(other.mapLimit_);
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
			case 24u:
			case 26u:
				chatType_.AddEntriesFrom(ref input, _repeated_chatType_codec);
				break;
			case 32u:
			case 34u:
				mapModeType_.AddEntriesFrom(ref input, _repeated_mapModeType_codec);
				break;
			case 42u:
			case 45u:
				mapLimit_.AddEntriesFrom(ref input, _repeated_mapLimit_codec);
				break;
			}
		}
	}
}
