using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CollaborationConfigure : IMessage<CollaborationConfigure>, IMessage, IEquatable<CollaborationConfigure>, IDeepCloneable<CollaborationConfigure>, IBufferMessage
{
	private static readonly MessageParser<CollaborationConfigure> _parser = new MessageParser<CollaborationConfigure>(() => new CollaborationConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<CollaborationInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, CollaborationInfoConfigure.Parser);

	private readonly RepeatedField<CollaborationInfoConfigure> infos_ = new RepeatedField<CollaborationInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, CollaborationInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, CollaborationInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CollaborationInfoConfigure.Parser), 18u);

	private readonly MapField<int, CollaborationInfoConfigure> infoDict_ = new MapField<int, CollaborationInfoConfigure>();

	public const int GoodssFieldNumber = 3;

	private static readonly FieldCodec<CollaborationGoodsConfigure> _repeated_goodss_codec = FieldCodec.ForMessage(26u, CollaborationGoodsConfigure.Parser);

	private readonly RepeatedField<CollaborationGoodsConfigure> goodss_ = new RepeatedField<CollaborationGoodsConfigure>();

	public const int GoodsDictFieldNumber = 4;

	private static readonly MapField<int, CollaborationGoodsConfigure>.Codec _map_goodsDict_codec = new MapField<int, CollaborationGoodsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, CollaborationGoodsConfigure.Parser), 34u);

	private readonly MapField<int, CollaborationGoodsConfigure> goodsDict_ = new MapField<int, CollaborationGoodsConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CollaborationConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CollaborationReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CollaborationInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CollaborationInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CollaborationGoodsConfigure> Goodss => goodss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, CollaborationGoodsConfigure> GoodsDict => goodsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationConfigure(CollaborationConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		goodss_ = other.goodss_.Clone();
		goodsDict_ = other.goodsDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationConfigure Clone()
	{
		return new CollaborationConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CollaborationConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CollaborationConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!goodss_.Equals(other.goodss_))
		{
			return false;
		}
		if (!GoodsDict.Equals(other.GoodsDict))
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
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= goodss_.GetHashCode();
		num ^= GoodsDict.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		goodss_.WriteTo(ref output, _repeated_goodss_codec);
		goodsDict_.WriteTo(ref output, _map_goodsDict_codec);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += goodss_.CalculateSize(_repeated_goodss_codec);
		num += goodsDict_.CalculateSize(_map_goodsDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CollaborationConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			goodss_.Add(other.goodss_);
			goodsDict_.MergeFrom(other.goodsDict_);
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
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				goodss_.AddEntriesFrom(ref input, _repeated_goodss_codec);
				break;
			case 34u:
				goodsDict_.AddEntriesFrom(ref input, _map_goodsDict_codec);
				break;
			}
		}
	}

	public void Fix(FixCollaborationConfigure FixCollaboration)
	{
		if (FixCollaboration == null)
		{
			return;
		}
		MapField<int, FixCollaborationInfoConfigure> infoDict = FixCollaboration.InfoDict;
		if (infoDict == null || infoDict.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < infos_.Count; i++)
		{
			if (infoDict.TryGetValue(infos_[i].Id, out var value))
			{
				infos_[i].FixTime(value);
				infoDict_[infos_[i].Id].FixTime(value);
			}
		}
	}
}
