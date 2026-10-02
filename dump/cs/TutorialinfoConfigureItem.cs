using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class TutorialinfoConfigureItem : IMessage<TutorialinfoConfigureItem>, IMessage, IEquatable<TutorialinfoConfigureItem>, IDeepCloneable<TutorialinfoConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<TutorialinfoConfigureItem> _parser = new MessageParser<TutorialinfoConfigureItem>(() => new TutorialinfoConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int MsgIDFieldNumber = 2;

	private int msgID_;

	public const int MsgIDMobileFieldNumber = 3;

	private int msgIDMobile_;

	public const int ImageNameFieldNumber = 4;

	private int imageName_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TutorialinfoConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TutorialReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MsgID
	{
		get
		{
			return msgID_;
		}
		private set
		{
			msgID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MsgIDMobile
	{
		get
		{
			return msgIDMobile_;
		}
		private set
		{
			msgIDMobile_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ImageName
	{
		get
		{
			return imageName_;
		}
		private set
		{
			imageName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialinfoConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialinfoConfigureItem(TutorialinfoConfigureItem other)
		: this()
	{
		index_ = other.index_;
		msgID_ = other.msgID_;
		msgIDMobile_ = other.msgIDMobile_;
		imageName_ = other.imageName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialinfoConfigureItem Clone()
	{
		return new TutorialinfoConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TutorialinfoConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TutorialinfoConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (MsgID != other.MsgID)
		{
			return false;
		}
		if (MsgIDMobile != other.MsgIDMobile)
		{
			return false;
		}
		if (ImageName != other.ImageName)
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (MsgID != 0)
		{
			num ^= MsgID.GetHashCode();
		}
		if (MsgIDMobile != 0)
		{
			num ^= MsgIDMobile.GetHashCode();
		}
		if (ImageName != 0)
		{
			num ^= ImageName.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (MsgID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MsgID);
		}
		if (MsgIDMobile != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MsgIDMobile);
		}
		if (ImageName != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ImageName);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (MsgID != 0)
		{
			num += 5;
		}
		if (MsgIDMobile != 0)
		{
			num += 5;
		}
		if (ImageName != 0)
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
	public void MergeFrom(TutorialinfoConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.MsgID != 0)
			{
				MsgID = other.MsgID;
			}
			if (other.MsgIDMobile != 0)
			{
				MsgIDMobile = other.MsgIDMobile;
			}
			if (other.ImageName != 0)
			{
				ImageName = other.ImageName;
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
				Index = input.ReadSFixed32();
				break;
			case 21u:
				MsgID = input.ReadSFixed32();
				break;
			case 29u:
				MsgIDMobile = input.ReadSFixed32();
				break;
			case 37u:
				ImageName = input.ReadSFixed32();
				break;
			}
		}
	}
}
