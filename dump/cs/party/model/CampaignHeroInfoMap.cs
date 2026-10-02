using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class CampaignHeroInfoMap : IMessage<CampaignHeroInfoMap>, IMessage, IEquatable<CampaignHeroInfoMap>, IDeepCloneable<CampaignHeroInfoMap>, IBufferMessage
{
	private static readonly MessageParser<CampaignHeroInfoMap> _parser = new MessageParser<CampaignHeroInfoMap>(() => new CampaignHeroInfoMap());

	private UnknownFieldSet _unknownFields;

	public const int HeroInfoMapFieldNumber = 1;

	private static readonly MapField<int, DefaultHeroInfo>.Codec _map_heroInfoMap_codec = new MapField<int, DefaultHeroInfo>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, DefaultHeroInfo.Parser), 10u);

	private readonly MapField<int, DefaultHeroInfo> heroInfoMap_ = new MapField<int, DefaultHeroInfo>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CampaignHeroInfoMap> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[100];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, DefaultHeroInfo> HeroInfoMap => heroInfoMap_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignHeroInfoMap()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignHeroInfoMap(CampaignHeroInfoMap other)
		: this()
	{
		heroInfoMap_ = other.heroInfoMap_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignHeroInfoMap Clone()
	{
		return new CampaignHeroInfoMap(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CampaignHeroInfoMap);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CampaignHeroInfoMap other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!HeroInfoMap.Equals(other.HeroInfoMap))
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
		num ^= HeroInfoMap.GetHashCode();
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
		heroInfoMap_.WriteTo(ref output, _map_heroInfoMap_codec);
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
		num += heroInfoMap_.CalculateSize(_map_heroInfoMap_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CampaignHeroInfoMap other)
	{
		if (other != null)
		{
			heroInfoMap_.MergeFrom(other.heroInfoMap_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				heroInfoMap_.AddEntriesFrom(ref input, _map_heroInfoMap_codec);
			}
		}
	}
}
