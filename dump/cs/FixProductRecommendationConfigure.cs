using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixProductRecommendationConfigure : IMessage<FixProductRecommendationConfigure>, IMessage, IEquatable<FixProductRecommendationConfigure>, IDeepCloneable<FixProductRecommendationConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixProductRecommendationConfigure> _parser = new MessageParser<FixProductRecommendationConfigure>(() => new FixProductRecommendationConfigure());

	private UnknownFieldSet _unknownFields;

	public const int BannersFieldNumber = 1;

	private static readonly FieldCodec<FixProductRecommendationBannerConfigure> _repeated_banners_codec = FieldCodec.ForMessage(10u, FixProductRecommendationBannerConfigure.Parser);

	private readonly RepeatedField<FixProductRecommendationBannerConfigure> banners_ = new RepeatedField<FixProductRecommendationBannerConfigure>();

	public const int BannerDictFieldNumber = 2;

	private static readonly MapField<int, FixProductRecommendationBannerConfigure>.Codec _map_bannerDict_codec = new MapField<int, FixProductRecommendationBannerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixProductRecommendationBannerConfigure.Parser), 18u);

	private readonly MapField<int, FixProductRecommendationBannerConfigure> bannerDict_ = new MapField<int, FixProductRecommendationBannerConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixProductRecommendationConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixProductRecommendationReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixProductRecommendationBannerConfigure> Banners => banners_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixProductRecommendationBannerConfigure> BannerDict => bannerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixProductRecommendationConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixProductRecommendationConfigure(FixProductRecommendationConfigure other)
		: this()
	{
		banners_ = other.banners_.Clone();
		bannerDict_ = other.bannerDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixProductRecommendationConfigure Clone()
	{
		return new FixProductRecommendationConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixProductRecommendationConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixProductRecommendationConfigure other)
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
	public void MergeFrom(FixProductRecommendationConfigure other)
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
}
