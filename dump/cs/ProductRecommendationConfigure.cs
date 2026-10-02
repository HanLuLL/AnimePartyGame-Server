using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ProductRecommendationConfigure : IMessage<ProductRecommendationConfigure>, IMessage, IEquatable<ProductRecommendationConfigure>, IDeepCloneable<ProductRecommendationConfigure>, IBufferMessage
{
	private static readonly MessageParser<ProductRecommendationConfigure> _parser = new MessageParser<ProductRecommendationConfigure>(() => new ProductRecommendationConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BannersFieldNumber = 1;

	private static readonly FieldCodec<ProductRecommendationBannerConfigure> _repeated_banners_codec = FieldCodec.ForMessage(10u, ProductRecommendationBannerConfigure.Parser);

	private readonly RepeatedField<ProductRecommendationBannerConfigure> banners_ = new RepeatedField<ProductRecommendationBannerConfigure>();

	public const int BannerDictFieldNumber = 2;

	private static readonly MapField<int, ProductRecommendationBannerConfigure>.Codec _map_bannerDict_codec = new MapField<int, ProductRecommendationBannerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ProductRecommendationBannerConfigure.Parser), 18u);

	private readonly MapField<int, ProductRecommendationBannerConfigure> bannerDict_ = new MapField<int, ProductRecommendationBannerConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ProductRecommendationConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProductRecommendationReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ProductRecommendationBannerConfigure> Banners => banners_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ProductRecommendationBannerConfigure> BannerDict => bannerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProductRecommendationConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProductRecommendationConfigure(ProductRecommendationConfigure other)
		: this()
	{
		banners_ = other.banners_.Clone();
		bannerDict_ = other.bannerDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProductRecommendationConfigure Clone()
	{
		return new ProductRecommendationConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ProductRecommendationConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ProductRecommendationConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!banners_.Equals(other.banners_))
		{
			return false;
		}
		if (!BannerDict.Equals(other.BannerDict))
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
		num ^= banners_.GetHashCode();
		num ^= BannerDict.GetHashCode();
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
		banners_.WriteTo(ref output, _repeated_banners_codec);
		bannerDict_.WriteTo(ref output, _map_bannerDict_codec);
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
		num += banners_.CalculateSize(_repeated_banners_codec);
		num += bannerDict_.CalculateSize(_map_bannerDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ProductRecommendationConfigure other)
	{
		if (other != null)
		{
			banners_.Add(other.banners_);
			bannerDict_.MergeFrom(other.bannerDict_);
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
			case 10u:
				banners_.AddEntriesFrom(ref input, _repeated_banners_codec);
				break;
			case 18u:
				bannerDict_.AddEntriesFrom(ref input, _map_bannerDict_codec);
				break;
			}
		}
	}

	public void Fix(FixProductRecommendationConfigure FixProductRecommendation)
	{
		if (FixProductRecommendation == null)
		{
			return;
		}
		MapField<int, FixProductRecommendationBannerConfigure> bannerDict = FixProductRecommendation.BannerDict;
		if (bannerDict == null || bannerDict.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < banners_.Count; i++)
		{
			if (bannerDict.TryGetValue(banners_[i].BannerID, out var value))
			{
				banners_[i].FixTime(value);
				bannerDict_[banners_[i].BannerID].FixTime(value);
			}
		}
	}
}
