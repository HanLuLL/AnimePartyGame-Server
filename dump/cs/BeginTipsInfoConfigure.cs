using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BeginTipsInfoConfigure : IMessage<BeginTipsInfoConfigure>, IMessage, IEquatable<BeginTipsInfoConfigure>, IDeepCloneable<BeginTipsInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<BeginTipsInfoConfigure> _parser = new MessageParser<BeginTipsInfoConfigure>(() => new BeginTipsInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IDFieldNumber = 1;

	private int iD_;

	public const int TipsIDFieldNumber = 2;

	private int tipsID_;

	public const int MapModeTypeFieldNumber = 3;

	private static readonly FieldCodec<MapModeType> _repeated_mapModeType_codec = FieldCodec.ForEnum(26u, (MapModeType x) => (int)x, (int x) => (MapModeType)x);

	private readonly RepeatedField<MapModeType> mapModeType_ = new RepeatedField<MapModeType>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BeginTipsInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BeginTipsReflection.Descriptor.MessageTypes[0];

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
	public int TipsID
	{
		get
		{
			return tipsID_;
		}
		private set
		{
			tipsID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapModeType> MapModeType => mapModeType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BeginTipsInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BeginTipsInfoConfigure(BeginTipsInfoConfigure other)
		: this()
	{
		iD_ = other.iD_;
		tipsID_ = other.tipsID_;
		mapModeType_ = other.mapModeType_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BeginTipsInfoConfigure Clone()
	{
		return new BeginTipsInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BeginTipsInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BeginTipsInfoConfigure other)
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
		if (TipsID != other.TipsID)
		{
			return false;
		}
		if (!mapModeType_.Equals(other.mapModeType_))
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
		if (TipsID != 0)
		{
			num ^= TipsID.GetHashCode();
		}
		num ^= mapModeType_.GetHashCode();
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
		if (TipsID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TipsID);
		}
		mapModeType_.WriteTo(ref output, _repeated_mapModeType_codec);
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
		if (TipsID != 0)
		{
			num += 5;
		}
		num += mapModeType_.CalculateSize(_repeated_mapModeType_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BeginTipsInfoConfigure other)
	{
		if (other != null)
		{
			if (other.ID != 0)
			{
				ID = other.ID;
			}
			if (other.TipsID != 0)
			{
				TipsID = other.TipsID;
			}
			mapModeType_.Add(other.mapModeType_);
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
				TipsID = input.ReadSFixed32();
				break;
			case 24u:
			case 26u:
				mapModeType_.AddEntriesFrom(ref input, _repeated_mapModeType_codec);
				break;
			}
		}
	}
}
