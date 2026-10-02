using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class CharacterExpressionPackConfigureItem : IMessage<CharacterExpressionPackConfigureItem>, IMessage, IEquatable<CharacterExpressionPackConfigureItem>, IDeepCloneable<CharacterExpressionPackConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<CharacterExpressionPackConfigureItem> _parser = new MessageParser<CharacterExpressionPackConfigureItem>(() => new CharacterExpressionPackConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int ItemIDFieldNumber = 1;

	private int itemID_;

	public const int VideoKeyFieldNumber = 2;

	private string videoKey_ = "";

	public const int IsDefaultFieldNumber = 3;

	private bool isDefault_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CharacterExpressionPackConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CharacterReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemID
	{
		get
		{
			return itemID_;
		}
		private set
		{
			itemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string VideoKey
	{
		get
		{
			return videoKey_;
		}
		private set
		{
			videoKey_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDefault
	{
		get
		{
			return isDefault_;
		}
		private set
		{
			isDefault_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterExpressionPackConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterExpressionPackConfigureItem(CharacterExpressionPackConfigureItem other)
		: this()
	{
		itemID_ = other.itemID_;
		videoKey_ = other.videoKey_;
		isDefault_ = other.isDefault_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterExpressionPackConfigureItem Clone()
	{
		return new CharacterExpressionPackConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CharacterExpressionPackConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CharacterExpressionPackConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ItemID != other.ItemID)
		{
			return false;
		}
		if (VideoKey != other.VideoKey)
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
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
		if (ItemID != 0)
		{
			num ^= ItemID.GetHashCode();
		}
		if (VideoKey.Length != 0)
		{
			num ^= VideoKey.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
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
		if (ItemID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ItemID);
		}
		if (VideoKey.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(VideoKey);
		}
		if (IsDefault)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsDefault);
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
		if (ItemID != 0)
		{
			num += 5;
		}
		if (VideoKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(VideoKey);
		}
		if (IsDefault)
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
	public void MergeFrom(CharacterExpressionPackConfigureItem other)
	{
		if (other != null)
		{
			if (other.ItemID != 0)
			{
				ItemID = other.ItemID;
			}
			if (other.VideoKey.Length != 0)
			{
				VideoKey = other.VideoKey;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
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
				ItemID = input.ReadSFixed32();
				break;
			case 18u:
				VideoKey = input.ReadString();
				break;
			case 24u:
				IsDefault = input.ReadBool();
				break;
			}
		}
	}
}
