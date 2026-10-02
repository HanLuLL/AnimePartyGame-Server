using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GachaGroupConfigure : IMessage<GachaGroupConfigure>, IMessage, IEquatable<GachaGroupConfigure>, IDeepCloneable<GachaGroupConfigure>, IBufferMessage
{
	private static readonly MessageParser<GachaGroupConfigure> _parser = new MessageParser<GachaGroupConfigure>(() => new GachaGroupConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GroupIDFieldNumber = 1;

	private int groupID_;

	public const int GachaItemSortTypeFieldNumber = 2;

	private GachaItemSortType gachaItemSortType_;

	public const int GachaGroupConfigureItemsFieldNumber = 3;

	private static readonly FieldCodec<GachaGroupConfigureItem> _repeated_gachaGroupConfigureItems_codec = FieldCodec.ForMessage(26u, GachaGroupConfigureItem.Parser);

	private readonly RepeatedField<GachaGroupConfigureItem> gachaGroupConfigureItems_ = new RepeatedField<GachaGroupConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaGroupConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GroupID
	{
		get
		{
			return groupID_;
		}
		private set
		{
			groupID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaItemSortType GachaItemSortType
	{
		get
		{
			return gachaItemSortType_;
		}
		private set
		{
			gachaItemSortType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaGroupConfigureItem> GachaGroupConfigureItems => gachaGroupConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaGroupConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaGroupConfigure(GachaGroupConfigure other)
		: this()
	{
		groupID_ = other.groupID_;
		gachaItemSortType_ = other.gachaItemSortType_;
		gachaGroupConfigureItems_ = other.gachaGroupConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaGroupConfigure Clone()
	{
		return new GachaGroupConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaGroupConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaGroupConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GroupID != other.GroupID)
		{
			return false;
		}
		if (GachaItemSortType != other.GachaItemSortType)
		{
			return false;
		}
		if (!gachaGroupConfigureItems_.Equals(other.gachaGroupConfigureItems_))
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
		if (GroupID != 0)
		{
			num ^= GroupID.GetHashCode();
		}
		if (GachaItemSortType != GachaItemSortType.None)
		{
			num ^= GachaItemSortType.GetHashCode();
		}
		num ^= gachaGroupConfigureItems_.GetHashCode();
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
		if (GroupID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GroupID);
		}
		if (GachaItemSortType != GachaItemSortType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)GachaItemSortType);
		}
		gachaGroupConfigureItems_.WriteTo(ref output, _repeated_gachaGroupConfigureItems_codec);
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
		if (GroupID != 0)
		{
			num += 5;
		}
		if (GachaItemSortType != GachaItemSortType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GachaItemSortType);
		}
		num += gachaGroupConfigureItems_.CalculateSize(_repeated_gachaGroupConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GachaGroupConfigure other)
	{
		if (other != null)
		{
			if (other.GroupID != 0)
			{
				GroupID = other.GroupID;
			}
			if (other.GachaItemSortType != GachaItemSortType.None)
			{
				GachaItemSortType = other.GachaItemSortType;
			}
			gachaGroupConfigureItems_.Add(other.gachaGroupConfigureItems_);
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
				GroupID = input.ReadSFixed32();
				break;
			case 16u:
				GachaItemSortType = (GachaItemSortType)input.ReadEnum();
				break;
			case 26u:
				gachaGroupConfigureItems_.AddEntriesFrom(ref input, _repeated_gachaGroupConfigureItems_codec);
				break;
			}
		}
	}
}
