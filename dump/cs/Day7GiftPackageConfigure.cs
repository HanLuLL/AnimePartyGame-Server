using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class Day7GiftPackageConfigure : IMessage<Day7GiftPackageConfigure>, IMessage, IEquatable<Day7GiftPackageConfigure>, IDeepCloneable<Day7GiftPackageConfigure>, IBufferMessage
{
	private static readonly MessageParser<Day7GiftPackageConfigure> _parser = new MessageParser<Day7GiftPackageConfigure>(() => new Day7GiftPackageConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GoodssFieldNumber = 1;

	private static readonly FieldCodec<Day7GiftPackageGoodsConfigure> _repeated_goodss_codec = FieldCodec.ForMessage(10u, Day7GiftPackageGoodsConfigure.Parser);

	private readonly RepeatedField<Day7GiftPackageGoodsConfigure> goodss_ = new RepeatedField<Day7GiftPackageGoodsConfigure>();

	public const int GoodsDictFieldNumber = 2;

	private static readonly MapField<int, Day7GiftPackageGoodsConfigure>.Codec _map_goodsDict_codec = new MapField<int, Day7GiftPackageGoodsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, Day7GiftPackageGoodsConfigure.Parser), 18u);

	private readonly MapField<int, Day7GiftPackageGoodsConfigure> goodsDict_ = new MapField<int, Day7GiftPackageGoodsConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Day7GiftPackageConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => Day7GiftPackageReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Day7GiftPackageGoodsConfigure> Goodss => goodss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, Day7GiftPackageGoodsConfigure> GoodsDict => goodsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageConfigure(Day7GiftPackageConfigure other)
		: this()
	{
		goodss_ = other.goodss_.Clone();
		goodsDict_ = other.goodsDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageConfigure Clone()
	{
		return new Day7GiftPackageConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Day7GiftPackageConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Day7GiftPackageConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
	public void MergeFrom(Day7GiftPackageConfigure other)
	{
		if (other != null)
		{
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
				goodss_.AddEntriesFrom(ref input, _repeated_goodss_codec);
				break;
			case 18u:
				goodsDict_.AddEntriesFrom(ref input, _map_goodsDict_codec);
				break;
			}
		}
	}
}
