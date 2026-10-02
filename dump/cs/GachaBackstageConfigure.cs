using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class GachaBackstageConfigure : IMessage<GachaBackstageConfigure>, IMessage, IEquatable<GachaBackstageConfigure>, IDeepCloneable<GachaBackstageConfigure>, IBufferMessage
{
	private static readonly MessageParser<GachaBackstageConfigure> _parser = new MessageParser<GachaBackstageConfigure>(() => new GachaBackstageConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GachaTypeFieldNumber = 1;

	private GachaType gachaType_;

	public const int GachaTableTypeFieldNumber = 2;

	private GachaTableType gachaTableType_;

	public const int UiPanelTypeFieldNumber = 3;

	private UIPanelType uiPanelType_;

	public const int PoolIDFieldNumber = 4;

	private int poolID_;

	public const int CurrencyBarFieldNumber = 5;

	private int currencyBar_;

	public const int CostItemFieldNumber = 6;

	private int costItem_;

	public const int TimeLimitFieldNumber = 7;

	private int timeLimit_;

	public const int CostOnceFieldNumber = 8;

	private int costOnce_;

	public const int LeastTimesFieldNumber = 9;

	private int leastTimes_;

	public const int NTimesFieldNumber = 10;

	private int nTimes_;

	public const int GiftIDFieldNumber = 11;

	private int giftID_;

	public const int WayFieldNumber = 12;

	private int way_;

	public const int BeginDateTimeFieldNumber = 13;

	private Timestamp beginDateTime_;

	public const int EndDateTimeFieldNumber = 14;

	private Timestamp endDateTime_;

	public const int TabOrderFieldNumber = 15;

	private int tabOrder_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaBackstageConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaType GachaType
	{
		get
		{
			return gachaType_;
		}
		private set
		{
			gachaType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaTableType GachaTableType
	{
		get
		{
			return gachaTableType_;
		}
		private set
		{
			gachaTableType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIPanelType UiPanelType
	{
		get
		{
			return uiPanelType_;
		}
		private set
		{
			uiPanelType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PoolID
	{
		get
		{
			return poolID_;
		}
		private set
		{
			poolID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrencyBar
	{
		get
		{
			return currencyBar_;
		}
		private set
		{
			currencyBar_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CostItem
	{
		get
		{
			return costItem_;
		}
		private set
		{
			costItem_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TimeLimit
	{
		get
		{
			return timeLimit_;
		}
		private set
		{
			timeLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CostOnce
	{
		get
		{
			return costOnce_;
		}
		private set
		{
			costOnce_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LeastTimes
	{
		get
		{
			return leastTimes_;
		}
		private set
		{
			leastTimes_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NTimes
	{
		get
		{
			return nTimes_;
		}
		private set
		{
			nTimes_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GiftID
	{
		get
		{
			return giftID_;
		}
		private set
		{
			giftID_ = value;
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
	public Timestamp BeginDateTime
	{
		get
		{
			return beginDateTime_;
		}
		private set
		{
			beginDateTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndDateTime
	{
		get
		{
			return endDateTime_;
		}
		private set
		{
			endDateTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TabOrder
	{
		get
		{
			return tabOrder_;
		}
		private set
		{
			tabOrder_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaBackstageConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaBackstageConfigure(GachaBackstageConfigure other)
		: this()
	{
		gachaType_ = other.gachaType_;
		gachaTableType_ = other.gachaTableType_;
		uiPanelType_ = other.uiPanelType_;
		poolID_ = other.poolID_;
		currencyBar_ = other.currencyBar_;
		costItem_ = other.costItem_;
		timeLimit_ = other.timeLimit_;
		costOnce_ = other.costOnce_;
		leastTimes_ = other.leastTimes_;
		nTimes_ = other.nTimes_;
		giftID_ = other.giftID_;
		way_ = other.way_;
		beginDateTime_ = ((other.beginDateTime_ != null) ? other.beginDateTime_.Clone() : null);
		endDateTime_ = ((other.endDateTime_ != null) ? other.endDateTime_.Clone() : null);
		tabOrder_ = other.tabOrder_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaBackstageConfigure Clone()
	{
		return new GachaBackstageConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaBackstageConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaBackstageConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GachaType != other.GachaType)
		{
			return false;
		}
		if (GachaTableType != other.GachaTableType)
		{
			return false;
		}
		if (UiPanelType != other.UiPanelType)
		{
			return false;
		}
		if (PoolID != other.PoolID)
		{
			return false;
		}
		if (CurrencyBar != other.CurrencyBar)
		{
			return false;
		}
		if (CostItem != other.CostItem)
		{
			return false;
		}
		if (TimeLimit != other.TimeLimit)
		{
			return false;
		}
		if (CostOnce != other.CostOnce)
		{
			return false;
		}
		if (LeastTimes != other.LeastTimes)
		{
			return false;
		}
		if (NTimes != other.NTimes)
		{
			return false;
		}
		if (GiftID != other.GiftID)
		{
			return false;
		}
		if (Way != other.Way)
		{
			return false;
		}
		if (!object.Equals(BeginDateTime, other.BeginDateTime))
		{
			return false;
		}
		if (!object.Equals(EndDateTime, other.EndDateTime))
		{
			return false;
		}
		if (TabOrder != other.TabOrder)
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
		if (GachaType != GachaType.None)
		{
			num ^= GachaType.GetHashCode();
		}
		if (GachaTableType != GachaTableType.None)
		{
			num ^= GachaTableType.GetHashCode();
		}
		if (UiPanelType != UIPanelType.None)
		{
			num ^= UiPanelType.GetHashCode();
		}
		if (PoolID != 0)
		{
			num ^= PoolID.GetHashCode();
		}
		if (CurrencyBar != 0)
		{
			num ^= CurrencyBar.GetHashCode();
		}
		if (CostItem != 0)
		{
			num ^= CostItem.GetHashCode();
		}
		if (TimeLimit != 0)
		{
			num ^= TimeLimit.GetHashCode();
		}
		if (CostOnce != 0)
		{
			num ^= CostOnce.GetHashCode();
		}
		if (LeastTimes != 0)
		{
			num ^= LeastTimes.GetHashCode();
		}
		if (NTimes != 0)
		{
			num ^= NTimes.GetHashCode();
		}
		if (GiftID != 0)
		{
			num ^= GiftID.GetHashCode();
		}
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
		}
		if (beginDateTime_ != null)
		{
			num ^= BeginDateTime.GetHashCode();
		}
		if (endDateTime_ != null)
		{
			num ^= EndDateTime.GetHashCode();
		}
		if (TabOrder != 0)
		{
			num ^= TabOrder.GetHashCode();
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
		if (GachaType != GachaType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)GachaType);
		}
		if (GachaTableType != GachaTableType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)GachaTableType);
		}
		if (UiPanelType != UIPanelType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)UiPanelType);
		}
		if (PoolID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(PoolID);
		}
		if (CurrencyBar != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CurrencyBar);
		}
		if (CostItem != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(CostItem);
		}
		if (TimeLimit != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(TimeLimit);
		}
		if (CostOnce != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(CostOnce);
		}
		if (LeastTimes != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(LeastTimes);
		}
		if (NTimes != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(NTimes);
		}
		if (GiftID != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(GiftID);
		}
		if (Way != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(Way);
		}
		if (beginDateTime_ != null)
		{
			output.WriteRawTag(106);
			output.WriteMessage(BeginDateTime);
		}
		if (endDateTime_ != null)
		{
			output.WriteRawTag(114);
			output.WriteMessage(EndDateTime);
		}
		if (TabOrder != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(TabOrder);
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
		if (GachaType != GachaType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GachaType);
		}
		if (GachaTableType != GachaTableType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GachaTableType);
		}
		if (UiPanelType != UIPanelType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)UiPanelType);
		}
		if (PoolID != 0)
		{
			num += 5;
		}
		if (CurrencyBar != 0)
		{
			num += 5;
		}
		if (CostItem != 0)
		{
			num += 5;
		}
		if (TimeLimit != 0)
		{
			num += 5;
		}
		if (CostOnce != 0)
		{
			num += 5;
		}
		if (LeastTimes != 0)
		{
			num += 5;
		}
		if (NTimes != 0)
		{
			num += 5;
		}
		if (GiftID != 0)
		{
			num += 5;
		}
		if (Way != 0)
		{
			num += 5;
		}
		if (beginDateTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginDateTime);
		}
		if (endDateTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndDateTime);
		}
		if (TabOrder != 0)
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
	public void MergeFrom(GachaBackstageConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.GachaType != GachaType.None)
		{
			GachaType = other.GachaType;
		}
		if (other.GachaTableType != GachaTableType.None)
		{
			GachaTableType = other.GachaTableType;
		}
		if (other.UiPanelType != UIPanelType.None)
		{
			UiPanelType = other.UiPanelType;
		}
		if (other.PoolID != 0)
		{
			PoolID = other.PoolID;
		}
		if (other.CurrencyBar != 0)
		{
			CurrencyBar = other.CurrencyBar;
		}
		if (other.CostItem != 0)
		{
			CostItem = other.CostItem;
		}
		if (other.TimeLimit != 0)
		{
			TimeLimit = other.TimeLimit;
		}
		if (other.CostOnce != 0)
		{
			CostOnce = other.CostOnce;
		}
		if (other.LeastTimes != 0)
		{
			LeastTimes = other.LeastTimes;
		}
		if (other.NTimes != 0)
		{
			NTimes = other.NTimes;
		}
		if (other.GiftID != 0)
		{
			GiftID = other.GiftID;
		}
		if (other.Way != 0)
		{
			Way = other.Way;
		}
		if (other.beginDateTime_ != null)
		{
			if (beginDateTime_ == null)
			{
				BeginDateTime = new Timestamp();
			}
			BeginDateTime.MergeFrom(other.BeginDateTime);
		}
		if (other.endDateTime_ != null)
		{
			if (endDateTime_ == null)
			{
				EndDateTime = new Timestamp();
			}
			EndDateTime.MergeFrom(other.EndDateTime);
		}
		if (other.TabOrder != 0)
		{
			TabOrder = other.TabOrder;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 8u:
				GachaType = (GachaType)input.ReadEnum();
				break;
			case 16u:
				GachaTableType = (GachaTableType)input.ReadEnum();
				break;
			case 24u:
				UiPanelType = (UIPanelType)input.ReadEnum();
				break;
			case 37u:
				PoolID = input.ReadSFixed32();
				break;
			case 45u:
				CurrencyBar = input.ReadSFixed32();
				break;
			case 53u:
				CostItem = input.ReadSFixed32();
				break;
			case 61u:
				TimeLimit = input.ReadSFixed32();
				break;
			case 69u:
				CostOnce = input.ReadSFixed32();
				break;
			case 77u:
				LeastTimes = input.ReadSFixed32();
				break;
			case 85u:
				NTimes = input.ReadSFixed32();
				break;
			case 93u:
				GiftID = input.ReadSFixed32();
				break;
			case 101u:
				Way = input.ReadSFixed32();
				break;
			case 106u:
				if (beginDateTime_ == null)
				{
					BeginDateTime = new Timestamp();
				}
				input.ReadMessage(BeginDateTime);
				break;
			case 114u:
				if (endDateTime_ == null)
				{
					EndDateTime = new Timestamp();
				}
				input.ReadMessage(EndDateTime);
				break;
			case 125u:
				TabOrder = input.ReadSFixed32();
				break;
			}
		}
	}
}
