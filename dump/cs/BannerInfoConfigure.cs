using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class BannerInfoConfigure : IMessage<BannerInfoConfigure>, IMessage, IEquatable<BannerInfoConfigure>, IDeepCloneable<BannerInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<BannerInfoConfigure> _parser = new MessageParser<BannerInfoConfigure>(() => new BannerInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BannerIDFieldNumber = 1;

	private int bannerID_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int LanguageTypeFieldNumber = 3;

	private static readonly FieldCodec<LanguageType> _repeated_languageType_codec = FieldCodec.ForEnum(26u, (LanguageType x) => (int)x, (int x) => (LanguageType)x);

	private readonly RepeatedField<LanguageType> languageType_ = new RepeatedField<LanguageType>();

	public const int DescId1FieldNumber = 4;

	private int descId1_;

	public const int DescId2FieldNumber = 5;

	private int descId2_;

	public const int BeginTimeFieldNumber = 6;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 7;

	private Timestamp endTime_;

	public const int StyleFieldNumber = 8;

	private int style_;

	public const int BannerImagesFieldNumber = 9;

	private static readonly FieldCodec<string> _repeated_bannerImages_codec = FieldCodec.ForString(74u);

	private readonly RepeatedField<string> bannerImages_ = new RepeatedField<string>();

	public const int BannerImagesSFWFieldNumber = 10;

	private static readonly FieldCodec<string> _repeated_bannerImagesSFW_codec = FieldCodec.ForString(82u);

	private readonly RepeatedField<string> bannerImagesSFW_ = new RepeatedField<string>();

	public const int WayFieldNumber = 11;

	private int way_;

	public const int MoneySwitchFieldNumber = 12;

	private bool moneySwitch_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BannerInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BannerReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BannerID
	{
		get
		{
			return bannerID_;
		}
		private set
		{
			bannerID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OrderWeight
	{
		get
		{
			return orderWeight_;
		}
		private set
		{
			orderWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<LanguageType> LanguageType => languageType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescId1
	{
		get
		{
			return descId1_;
		}
		private set
		{
			descId1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescId2
	{
		get
		{
			return descId2_;
		}
		private set
		{
			descId2_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Style
	{
		get
		{
			return style_;
		}
		private set
		{
			style_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> BannerImages => bannerImages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> BannerImagesSFW => bannerImagesSFW_;

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
	public bool MoneySwitch
	{
		get
		{
			return moneySwitch_;
		}
		private set
		{
			moneySwitch_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BannerInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BannerInfoConfigure(BannerInfoConfigure other)
		: this()
	{
		bannerID_ = other.bannerID_;
		orderWeight_ = other.orderWeight_;
		languageType_ = other.languageType_.Clone();
		descId1_ = other.descId1_;
		descId2_ = other.descId2_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		style_ = other.style_;
		bannerImages_ = other.bannerImages_.Clone();
		bannerImagesSFW_ = other.bannerImagesSFW_.Clone();
		way_ = other.way_;
		moneySwitch_ = other.moneySwitch_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BannerInfoConfigure Clone()
	{
		return new BannerInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BannerInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BannerInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (BannerID != other.BannerID)
		{
			return false;
		}
		if (OrderWeight != other.OrderWeight)
		{
			return false;
		}
		if (!languageType_.Equals(other.languageType_))
		{
			return false;
		}
		if (DescId1 != other.DescId1)
		{
			return false;
		}
		if (DescId2 != other.DescId2)
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (Style != other.Style)
		{
			return false;
		}
		if (!bannerImages_.Equals(other.bannerImages_))
		{
			return false;
		}
		if (!bannerImagesSFW_.Equals(other.bannerImagesSFW_))
		{
			return false;
		}
		if (Way != other.Way)
		{
			return false;
		}
		if (MoneySwitch != other.MoneySwitch)
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
		if (BannerID != 0)
		{
			num ^= BannerID.GetHashCode();
		}
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		num ^= languageType_.GetHashCode();
		if (DescId1 != 0)
		{
			num ^= DescId1.GetHashCode();
		}
		if (DescId2 != 0)
		{
			num ^= DescId2.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (Style != 0)
		{
			num ^= Style.GetHashCode();
		}
		num ^= bannerImages_.GetHashCode();
		num ^= bannerImagesSFW_.GetHashCode();
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
		}
		if (MoneySwitch)
		{
			num ^= MoneySwitch.GetHashCode();
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
		if (BannerID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(BannerID);
		}
		if (OrderWeight != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(OrderWeight);
		}
		languageType_.WriteTo(ref output, _repeated_languageType_codec);
		if (DescId1 != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DescId1);
		}
		if (DescId2 != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DescId2);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(50);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(58);
			output.WriteMessage(EndTime);
		}
		if (Style != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Style);
		}
		bannerImages_.WriteTo(ref output, _repeated_bannerImages_codec);
		bannerImagesSFW_.WriteTo(ref output, _repeated_bannerImagesSFW_codec);
		if (Way != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Way);
		}
		if (MoneySwitch)
		{
			output.WriteRawTag(96);
			output.WriteBool(MoneySwitch);
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
		if (BannerID != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
		{
			num += 5;
		}
		num += languageType_.CalculateSize(_repeated_languageType_codec);
		if (DescId1 != 0)
		{
			num += 5;
		}
		if (DescId2 != 0)
		{
			num += 5;
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (Style != 0)
		{
			num += 5;
		}
		num += bannerImages_.CalculateSize(_repeated_bannerImages_codec);
		num += bannerImagesSFW_.CalculateSize(_repeated_bannerImagesSFW_codec);
		if (Way != 0)
		{
			num += 5;
		}
		if (MoneySwitch)
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
	public void MergeFrom(BannerInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.BannerID != 0)
		{
			BannerID = other.BannerID;
		}
		if (other.OrderWeight != 0)
		{
			OrderWeight = other.OrderWeight;
		}
		languageType_.Add(other.languageType_);
		if (other.DescId1 != 0)
		{
			DescId1 = other.DescId1;
		}
		if (other.DescId2 != 0)
		{
			DescId2 = other.DescId2;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		if (other.Style != 0)
		{
			Style = other.Style;
		}
		bannerImages_.Add(other.bannerImages_);
		bannerImagesSFW_.Add(other.bannerImagesSFW_);
		if (other.Way != 0)
		{
			Way = other.Way;
		}
		if (other.MoneySwitch)
		{
			MoneySwitch = other.MoneySwitch;
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
			case 13u:
				BannerID = input.ReadSFixed32();
				break;
			case 21u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 24u:
			case 26u:
				languageType_.AddEntriesFrom(ref input, _repeated_languageType_codec);
				break;
			case 37u:
				DescId1 = input.ReadSFixed32();
				break;
			case 45u:
				DescId2 = input.ReadSFixed32();
				break;
			case 50u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 58u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 69u:
				Style = input.ReadSFixed32();
				break;
			case 74u:
				bannerImages_.AddEntriesFrom(ref input, _repeated_bannerImages_codec);
				break;
			case 82u:
				bannerImagesSFW_.AddEntriesFrom(ref input, _repeated_bannerImagesSFW_codec);
				break;
			case 93u:
				Way = input.ReadSFixed32();
				break;
			case 96u:
				MoneySwitch = input.ReadBool();
				break;
			}
		}
	}

	public void FixTime(Timestamp beginTime, Timestamp endTime)
	{
		beginTime_ = beginTime;
		endTime_ = endTime;
	}
}
