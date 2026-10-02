using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MonthlyCardGoodsConfigure : IMessage<MonthlyCardGoodsConfigure>, IMessage, IEquatable<MonthlyCardGoodsConfigure>, IDeepCloneable<MonthlyCardGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<MonthlyCardGoodsConfigure> _parser = new MessageParser<MonthlyCardGoodsConfigure>(() => new MonthlyCardGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIDFieldNumber = 1;

	private int goodsID_;

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

	public const int ImmediatelyRewardFieldNumber = 8;

	private static readonly MapField<int, int>.Codec _map_immediatelyReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 66u);

	private readonly MapField<int, int> immediatelyReward_ = new MapField<int, int>();

	public const int DailyRewardFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_dailyReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> dailyReward_ = new MapField<int, int>();

	public const int MonthlyDaysFieldNumber = 10;

	private int monthlyDays_;

	public const int SubscribeLimitFieldNumber = 11;

	private int subscribeLimit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MonthlyCardGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MonthlyCardReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsID
	{
		get
		{
			return goodsID_;
		}
		private set
		{
			goodsID_ = value;
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
	public MapField<int, int> ImmediatelyReward => immediatelyReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> DailyReward => dailyReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MonthlyDays
	{
		get
		{
			return monthlyDays_;
		}
		private set
		{
			monthlyDays_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SubscribeLimit
	{
		get
		{
			return subscribeLimit_;
		}
		private set
		{
			subscribeLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonthlyCardGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonthlyCardGoodsConfigure(MonthlyCardGoodsConfigure other)
		: this()
	{
		goodsID_ = other.goodsID_;
		titleID_ = other.titleID_;
		descriptionID_ = other.descriptionID_;
		backgroundCN_ = other.backgroundCN_;
		backgroundEN_ = other.backgroundEN_;
		backgroundJP_ = other.backgroundJP_;
		backgroundTC_ = other.backgroundTC_;
		immediatelyReward_ = other.immediatelyReward_.Clone();
		dailyReward_ = other.dailyReward_.Clone();
		monthlyDays_ = other.monthlyDays_;
		subscribeLimit_ = other.subscribeLimit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonthlyCardGoodsConfigure Clone()
	{
		return new MonthlyCardGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MonthlyCardGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MonthlyCardGoodsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GoodsID != other.GoodsID)
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
		if (!ImmediatelyReward.Equals(other.ImmediatelyReward))
		{
			return false;
		}
		if (!DailyReward.Equals(other.DailyReward))
		{
			return false;
		}
		if (MonthlyDays != other.MonthlyDays)
		{
			return false;
		}
		if (SubscribeLimit != other.SubscribeLimit)
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
		if (GoodsID != 0)
		{
			num ^= GoodsID.GetHashCode();
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
		num ^= ImmediatelyReward.GetHashCode();
		num ^= DailyReward.GetHashCode();
		if (MonthlyDays != 0)
		{
			num ^= MonthlyDays.GetHashCode();
		}
		if (SubscribeLimit != 0)
		{
			num ^= SubscribeLimit.GetHashCode();
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
		if (GoodsID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsID);
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
		immediatelyReward_.WriteTo(ref output, _map_immediatelyReward_codec);
		dailyReward_.WriteTo(ref output, _map_dailyReward_codec);
		if (MonthlyDays != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(MonthlyDays);
		}
		if (SubscribeLimit != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(SubscribeLimit);
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
		if (GoodsID != 0)
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
		num += immediatelyReward_.CalculateSize(_map_immediatelyReward_codec);
		num += dailyReward_.CalculateSize(_map_dailyReward_codec);
		if (MonthlyDays != 0)
		{
			num += 5;
		}
		if (SubscribeLimit != 0)
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
	public void MergeFrom(MonthlyCardGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.GoodsID != 0)
			{
				GoodsID = other.GoodsID;
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
			immediatelyReward_.MergeFrom(other.immediatelyReward_);
			dailyReward_.MergeFrom(other.dailyReward_);
			if (other.MonthlyDays != 0)
			{
				MonthlyDays = other.MonthlyDays;
			}
			if (other.SubscribeLimit != 0)
			{
				SubscribeLimit = other.SubscribeLimit;
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
				GoodsID = input.ReadSFixed32();
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
			case 66u:
				immediatelyReward_.AddEntriesFrom(ref input, _map_immediatelyReward_codec);
				break;
			case 74u:
				dailyReward_.AddEntriesFrom(ref input, _map_dailyReward_codec);
				break;
			case 85u:
				MonthlyDays = input.ReadSFixed32();
				break;
			case 93u:
				SubscribeLimit = input.ReadSFixed32();
				break;
			}
		}
	}
}
