using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class RechargeStoreAdsAdsConfigureItem : IMessage<RechargeStoreAdsAdsConfigureItem>, IMessage, IEquatable<RechargeStoreAdsAdsConfigureItem>, IDeepCloneable<RechargeStoreAdsAdsConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreAdsAdsConfigureItem> _parser = new MessageParser<RechargeStoreAdsAdsConfigureItem>(() => new RechargeStoreAdsAdsConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int TitleIDFieldNumber = 2;

	private int titleID_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	public const int BackgroundCNFieldNumber = 4;

	private string backgroundCN_ = "";

	public const int BackgroundENFieldNumber = 5;

	private string backgroundEN_ = "";

	public const int BackgroundJPFieldNumber = 6;

	private string backgroundJP_ = "";

	public const int BackgroundTCFieldNumber = 7;

	private string backgroundTC_ = "";

	public const int WayFieldNumber = 8;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreAdsAdsConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreAdsReflection.Descriptor.MessageTypes[1];

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
	public int TitleID
	{
		get
		{
			return titleID_;
		}
		private set
		{
			titleID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescriptionID
	{
		get
		{
			return descriptionID_;
		}
		private set
		{
			descriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundCN
	{
		get
		{
			return backgroundCN_;
		}
		private set
		{
			backgroundCN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundEN
	{
		get
		{
			return backgroundEN_;
		}
		private set
		{
			backgroundEN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundJP
	{
		get
		{
			return backgroundJP_;
		}
		private set
		{
			backgroundJP_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundTC
	{
		get
		{
			return backgroundTC_;
		}
		private set
		{
			backgroundTC_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Way
	{
		get
		{
			return way_;
		}
		private set
		{
			way_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsAdsConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsAdsConfigureItem(RechargeStoreAdsAdsConfigureItem other)
		: this()
	{
		index_ = other.index_;
		titleID_ = other.titleID_;
		descriptionID_ = other.descriptionID_;
		backgroundCN_ = other.backgroundCN_;
		backgroundEN_ = other.backgroundEN_;
		backgroundJP_ = other.backgroundJP_;
		backgroundTC_ = other.backgroundTC_;
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsAdsConfigureItem Clone()
	{
		return new RechargeStoreAdsAdsConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreAdsAdsConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreAdsAdsConfigureItem other)
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
		if (TitleID != other.TitleID)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (BackgroundCN != other.BackgroundCN)
		{
			return false;
		}
		if (BackgroundEN != other.BackgroundEN)
		{
			return false;
		}
		if (BackgroundJP != other.BackgroundJP)
		{
			return false;
		}
		if (BackgroundTC != other.BackgroundTC)
		{
			return false;
		}
		if (Way != other.Way)
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
		if (TitleID != 0)
		{
			num ^= TitleID.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (BackgroundCN.Length != 0)
		{
			num ^= BackgroundCN.GetHashCode();
		}
		if (BackgroundEN.Length != 0)
		{
			num ^= BackgroundEN.GetHashCode();
		}
		if (BackgroundJP.Length != 0)
		{
			num ^= BackgroundJP.GetHashCode();
		}
		if (BackgroundTC.Length != 0)
		{
			num ^= BackgroundTC.GetHashCode();
		}
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
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
		if (TitleID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TitleID);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescriptionID);
		}
		if (BackgroundCN.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(BackgroundCN);
		}
		if (BackgroundEN.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(BackgroundEN);
		}
		if (BackgroundJP.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(BackgroundJP);
		}
		if (BackgroundTC.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(BackgroundTC);
		}
		if (Way != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Way);
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
		if (TitleID != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (BackgroundCN.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BackgroundCN);
		}
		if (BackgroundEN.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BackgroundEN);
		}
		if (BackgroundJP.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BackgroundJP);
		}
		if (BackgroundTC.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BackgroundTC);
		}
		if (Way != 0)
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
	public void MergeFrom(RechargeStoreAdsAdsConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.TitleID != 0)
			{
				TitleID = other.TitleID;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.BackgroundCN.Length != 0)
			{
				BackgroundCN = other.BackgroundCN;
			}
			if (other.BackgroundEN.Length != 0)
			{
				BackgroundEN = other.BackgroundEN;
			}
			if (other.BackgroundJP.Length != 0)
			{
				BackgroundJP = other.BackgroundJP;
			}
			if (other.BackgroundTC.Length != 0)
			{
				BackgroundTC = other.BackgroundTC;
			}
			if (other.Way != 0)
			{
				Way = other.Way;
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
				TitleID = input.ReadSFixed32();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 34u:
				BackgroundCN = input.ReadString();
				break;
			case 42u:
				BackgroundEN = input.ReadString();
				break;
			case 50u:
				BackgroundJP = input.ReadString();
				break;
			case 58u:
				BackgroundTC = input.ReadString();
				break;
			case 69u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
