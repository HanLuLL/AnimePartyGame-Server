using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class ProductRecommendationBannerConfigure : IMessage<ProductRecommendationBannerConfigure>, IMessage, IEquatable<ProductRecommendationBannerConfigure>, IDeepCloneable<ProductRecommendationBannerConfigure>, IBufferMessage
{
	private static readonly MessageParser<ProductRecommendationBannerConfigure> _parser = new MessageParser<ProductRecommendationBannerConfigure>(() => new ProductRecommendationBannerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BannerIDFieldNumber = 1;

	private int bannerID_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int DescId1FieldNumber = 3;

	private int descId1_;

	public const int DescId2FieldNumber = 4;

	private int descId2_;

	public const int BeginTimeFieldNumber = 5;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 6;

	private Timestamp endTime_;

	public const int StyleFieldNumber = 7;

	private int style_;

	public const int BannerImagesFieldNumber = 8;

	private static readonly FieldCodec<string> _repeated_bannerImages_codec = FieldCodec.ForString(66u);

	private readonly RepeatedField<string> bannerImages_ = new RepeatedField<string>();

	public const int BannerImagesSfwFieldNumber = 9;

	private static readonly FieldCodec<string> _repeated_bannerImagesSfw_codec = FieldCodec.ForString(74u);

	private readonly RepeatedField<string> bannerImagesSfw_ = new RepeatedField<string>();

	public const int WayFieldNumber = 10;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ProductRecommendationBannerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProductRecommendationReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<string> BannerImagesSfw => bannerImagesSfw_;

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
	public ProductRecommendationBannerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProductRecommendationBannerConfigure(ProductRecommendationBannerConfigure other)
		: this()
	{
		bannerID_ = other.bannerID_;
		orderWeight_ = other.orderWeight_;
		descId1_ = other.descId1_;
		descId2_ = other.descId2_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		style_ = other.style_;
		bannerImages_ = other.bannerImages_.Clone();
		bannerImagesSfw_ = other.bannerImagesSfw_.Clone();
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProductRecommendationBannerConfigure Clone()
	{
		return new ProductRecommendationBannerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ProductRecommendationBannerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ProductRecommendationBannerConfigure other)
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
		if (!bannerImagesSfw_.Equals(other.bannerImagesSfw_))
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
		if (BannerID != 0)
		{
			num ^= BannerID.GetHashCode();
		}
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
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
		num ^= bannerImagesSfw_.GetHashCode();
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
		if (DescId1 != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescId1);
		}
		if (DescId2 != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DescId2);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(42);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(50);
			output.WriteMessage(EndTime);
		}
		if (Style != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Style);
		}
		bannerImages_.WriteTo(ref output, _repeated_bannerImages_codec);
		bannerImagesSfw_.WriteTo(ref output, _repeated_bannerImagesSfw_codec);
		if (Way != 0)
		{
			output.WriteRawTag(85);
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
		if (BannerID != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
		{
			num += 5;
		}
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
		num += bannerImagesSfw_.CalculateSize(_repeated_bannerImagesSfw_codec);
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
	public void MergeFrom(ProductRecommendationBannerConfigure other)
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
		bannerImagesSfw_.Add(other.bannerImagesSfw_);
		if (other.Way != 0)
		{
			Way = other.Way;
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
			case 29u:
				DescId1 = input.ReadSFixed32();
				break;
			case 37u:
				DescId2 = input.ReadSFixed32();
				break;
			case 42u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 50u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 61u:
				Style = input.ReadSFixed32();
				break;
			case 66u:
				bannerImages_.AddEntriesFrom(ref input, _repeated_bannerImages_codec);
				break;
			case 74u:
				bannerImagesSfw_.AddEntriesFrom(ref input, _repeated_bannerImagesSfw_codec);
				break;
			case 85u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}

	public void FixTime(FixProductRecommendationBannerConfigure data)
	{
		beginTime_ = data.BeginTime;
		endTime_ = data.EndTime;
	}
}
