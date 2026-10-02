using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GachaRecord : IMessage<GachaRecord>, IMessage, IEquatable<GachaRecord>, IDeepCloneable<GachaRecord>, IBufferMessage
{
	private static readonly MessageParser<GachaRecord> _parser = new MessageParser<GachaRecord>(() => new GachaRecord());

	private UnknownFieldSet _unknownFields;

	public const int ItemIdFieldNumber = 1;

	private int itemId_;

	public const int ItemCountFieldNumber = 2;

	private int itemCount_;

	public const int IsConvertFieldNumber = 3;

	private bool isConvert_;

	public const int TimeFieldNumber = 4;

	private long time_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaRecord> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[28];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemId
	{
		get
		{
			return itemId_;
		}
		set
		{
			itemId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemCount
	{
		get
		{
			return itemCount_;
		}
		set
		{
			itemCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsConvert
	{
		get
		{
			return isConvert_;
		}
		set
		{
			isConvert_ = value;
		}
	}

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
	public GachaRecord()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecord(GachaRecord other)
		: this()
	{
		itemId_ = other.itemId_;
		itemCount_ = other.itemCount_;
		isConvert_ = other.isConvert_;
		time_ = other.time_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecord Clone()
	{
		return new GachaRecord(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaRecord);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaRecord other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ItemId != other.ItemId)
		{
			return false;
		}
		if (ItemCount != other.ItemCount)
		{
			return false;
		}
		if (IsConvert != other.IsConvert)
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
		if (ItemId != 0)
		{
			num ^= ItemId.GetHashCode();
		}
		if (ItemCount != 0)
		{
			num ^= ItemCount.GetHashCode();
		}
		if (IsConvert)
		{
			num ^= IsConvert.GetHashCode();
		}
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
		if (ItemId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ItemId);
		}
		if (ItemCount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ItemCount);
		}
		if (IsConvert)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsConvert);
		}
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
		if (ItemId != 0)
		{
			num += 5;
		}
		if (ItemCount != 0)
		{
			num += 5;
		}
		if (IsConvert)
		{
			num += 2;
		}
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
	public void MergeFrom(GachaRecord other)
	{
		if (other != null)
		{
			if (other.ItemId != 0)
			{
				ItemId = other.ItemId;
			}
			if (other.ItemCount != 0)
			{
				ItemCount = other.ItemCount;
			}
			if (other.IsConvert)
			{
				IsConvert = other.IsConvert;
			}
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
			case 13u:
				ItemId = input.ReadSFixed32();
				break;
			case 21u:
				ItemCount = input.ReadSFixed32();
				break;
			case 24u:
				IsConvert = input.ReadBool();
				break;
			case 33u:
				Time = input.ReadSFixed64();
				break;
			}
		}
	}
}
