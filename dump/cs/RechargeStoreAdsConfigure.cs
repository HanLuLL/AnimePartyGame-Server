using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class RechargeStoreAdsConfigure : IMessage<RechargeStoreAdsConfigure>, IMessage, IEquatable<RechargeStoreAdsConfigure>, IDeepCloneable<RechargeStoreAdsConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeStoreAdsConfigure> _parser = new MessageParser<RechargeStoreAdsConfigure>(() => new RechargeStoreAdsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int AdssFieldNumber = 1;

	private static readonly FieldCodec<RechargeStoreAdsAdsConfigure> _repeated_adss_codec = FieldCodec.ForMessage(10u, RechargeStoreAdsAdsConfigure.Parser);

	private readonly RepeatedField<RechargeStoreAdsAdsConfigure> adss_ = new RepeatedField<RechargeStoreAdsAdsConfigure>();

	public const int AdsDictFieldNumber = 2;

	private static readonly MapField<int, RechargeStoreAdsAdsConfigure>.Codec _map_adsDict_codec = new MapField<int, RechargeStoreAdsAdsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeStoreAdsAdsConfigure.Parser), 18u);

	private readonly MapField<int, RechargeStoreAdsAdsConfigure> adsDict_ = new MapField<int, RechargeStoreAdsAdsConfigure>();

	private RechargeStoreAdsAdsConfigureItem _noviceGiftPackage;

	private RechargeStoreAdsAdsConfigure _RecommendPackage;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeStoreAdsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeStoreAdsReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeStoreAdsAdsConfigure> Adss => adss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeStoreAdsAdsConfigure> AdsDict => adsDict_;

	public RechargeStoreAdsAdsConfigureItem NoviceGiftPackage
	{
		get
		{
			if (_noviceGiftPackage == null)
			{
				if (!StaticConfigure.RechargeStoreAds.AdsDict.TryGetValue(22, out var value))
				{
					Debug.LogError("在RechargeStoreAds.AdsDict表里并没有找到新手礼包商品");
					return null;
				}
				_noviceGiftPackage = value.RechargeStoreAdsAdsConfigureItems[0];
			}
			return _noviceGiftPackage;
		}
	}

	public RechargeStoreAdsAdsConfigure RecommendPackage
	{
		get
		{
			if (_RecommendPackage == null && !StaticConfigure.RechargeStoreAds.AdsDict.TryGetValue(24, out _RecommendPackage))
			{
				Debug.LogError("在RechargeStoreAds.AdsDict表里并没有找到新手礼包商品");
				return null;
			}
			return _RecommendPackage;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsConfigure(RechargeStoreAdsConfigure other)
		: this()
	{
		adss_ = other.adss_.Clone();
		adsDict_ = other.adsDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeStoreAdsConfigure Clone()
	{
		return new RechargeStoreAdsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeStoreAdsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeStoreAdsConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!adss_.Equals(other.adss_))
		{
			return false;
		}
		if (!AdsDict.Equals(other.AdsDict))
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
		num ^= adss_.GetHashCode();
		num ^= AdsDict.GetHashCode();
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
		adss_.WriteTo(ref output, _repeated_adss_codec);
		adsDict_.WriteTo(ref output, _map_adsDict_codec);
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
		num += adss_.CalculateSize(_repeated_adss_codec);
		num += adsDict_.CalculateSize(_map_adsDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeStoreAdsConfigure other)
	{
		if (other != null)
		{
			adss_.Add(other.adss_);
			adsDict_.MergeFrom(other.adsDict_);
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
				adss_.AddEntriesFrom(ref input, _repeated_adss_codec);
				break;
			case 18u:
				adsDict_.AddEntriesFrom(ref input, _map_adsDict_codec);
				break;
			}
		}
	}
}
