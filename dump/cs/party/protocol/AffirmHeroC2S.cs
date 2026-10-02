using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class AffirmHeroC2S : IMessage<AffirmHeroC2S>, IMessage, IEquatable<AffirmHeroC2S>, IDeepCloneable<AffirmHeroC2S>, IBufferMessage
{
	private static readonly MessageParser<AffirmHeroC2S> _parser = new MessageParser<AffirmHeroC2S>(() => new AffirmHeroC2S());

	private UnknownFieldSet _unknownFields;

	public const int AutoFieldNumber = 1;

	private bool auto_;

	public const int DefaultHeroInfosFieldNumber = 2;

	private static readonly MapField<int, DefaultHeroInfo>.Codec _map_defaultHeroInfos_codec = new MapField<int, DefaultHeroInfo>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, DefaultHeroInfo.Parser), 18u);

	private readonly MapField<int, DefaultHeroInfo> defaultHeroInfos_ = new MapField<int, DefaultHeroInfo>();

	public const int CampaignHeroInfoMapFieldNumber = 3;

	private static readonly MapField<int, CampaignHeroInfoMap>.Codec _map_campaignHeroInfoMap_codec = new MapField<int, CampaignHeroInfoMap>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.CampaignHeroInfoMap.Parser), 26u);

	private readonly MapField<int, CampaignHeroInfoMap> campaignHeroInfoMap_ = new MapField<int, CampaignHeroInfoMap>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AffirmHeroC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[178];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Auto
	{
		get
		{
			return auto_;
		}
		set
		{
			auto_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, DefaultHeroInfo> DefaultHeroInfos => defaultHeroInfos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CampaignHeroInfoMap> CampaignHeroInfoMap => campaignHeroInfoMap_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AffirmHeroC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AffirmHeroC2S(AffirmHeroC2S other)
		: this()
	{
		auto_ = other.auto_;
		defaultHeroInfos_ = other.defaultHeroInfos_.Clone();
		campaignHeroInfoMap_ = other.campaignHeroInfoMap_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AffirmHeroC2S Clone()
	{
		return new AffirmHeroC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AffirmHeroC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AffirmHeroC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Auto != other.Auto)
		{
			return false;
		}
		if (!DefaultHeroInfos.Equals(other.DefaultHeroInfos))
		{
			return false;
		}
		if (!CampaignHeroInfoMap.Equals(other.CampaignHeroInfoMap))
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
		if (Auto)
		{
			num ^= Auto.GetHashCode();
		}
		num ^= DefaultHeroInfos.GetHashCode();
		num ^= CampaignHeroInfoMap.GetHashCode();
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
		if (Auto)
		{
			output.WriteRawTag(8);
			output.WriteBool(Auto);
		}
		defaultHeroInfos_.WriteTo(ref output, _map_defaultHeroInfos_codec);
		campaignHeroInfoMap_.WriteTo(ref output, _map_campaignHeroInfoMap_codec);
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
		if (Auto)
		{
			num += 2;
		}
		num += defaultHeroInfos_.CalculateSize(_map_defaultHeroInfos_codec);
		num += campaignHeroInfoMap_.CalculateSize(_map_campaignHeroInfoMap_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AffirmHeroC2S other)
	{
		if (other != null)
		{
			if (other.Auto)
			{
				Auto = other.Auto;
			}
			defaultHeroInfos_.MergeFrom(other.defaultHeroInfos_);
			campaignHeroInfoMap_.MergeFrom(other.campaignHeroInfoMap_);
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
			case 8u:
				Auto = input.ReadBool();
				break;
			case 18u:
				defaultHeroInfos_.AddEntriesFrom(ref input, _map_defaultHeroInfos_codec);
				break;
			case 26u:
				campaignHeroInfoMap_.AddEntriesFrom(ref input, _map_campaignHeroInfoMap_codec);
				break;
			}
		}
	}
}
