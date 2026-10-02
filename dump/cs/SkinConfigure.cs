using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SkinConfigure : IMessage<SkinConfigure>, IMessage, IEquatable<SkinConfigure>, IDeepCloneable<SkinConfigure>, IBufferMessage
{
	private static readonly MessageParser<SkinConfigure> _parser = new MessageParser<SkinConfigure>(() => new SkinConfigure());

	private UnknownFieldSet _unknownFields;

	public const int StandingPaintingsFieldNumber = 1;

	private static readonly FieldCodec<SkinStandingPaintingConfigure> _repeated_standingPaintings_codec = FieldCodec.ForMessage(10u, SkinStandingPaintingConfigure.Parser);

	private readonly RepeatedField<SkinStandingPaintingConfigure> standingPaintings_ = new RepeatedField<SkinStandingPaintingConfigure>();

	public const int StandingPaintingDictFieldNumber = 2;

	private static readonly MapField<int, SkinStandingPaintingConfigure>.Codec _map_standingPaintingDict_codec = new MapField<int, SkinStandingPaintingConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SkinStandingPaintingConfigure.Parser), 18u);

	private readonly MapField<int, SkinStandingPaintingConfigure> standingPaintingDict_ = new MapField<int, SkinStandingPaintingConfigure>();

	public const int OffsetsFieldNumber = 3;

	private static readonly FieldCodec<SkinOffsetConfigure> _repeated_offsets_codec = FieldCodec.ForMessage(26u, SkinOffsetConfigure.Parser);

	private readonly RepeatedField<SkinOffsetConfigure> offsets_ = new RepeatedField<SkinOffsetConfigure>();

	public const int OffsetDictFieldNumber = 4;

	private static readonly MapField<int, SkinOffsetConfigure>.Codec _map_offsetDict_codec = new MapField<int, SkinOffsetConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SkinOffsetConfigure.Parser), 34u);

	private readonly MapField<int, SkinOffsetConfigure> offsetDict_ = new MapField<int, SkinOffsetConfigure>();

	public const int SkinPendantsFieldNumber = 5;

	private static readonly FieldCodec<SkinSkinPendantConfigure> _repeated_skinPendants_codec = FieldCodec.ForMessage(42u, SkinSkinPendantConfigure.Parser);

	private readonly RepeatedField<SkinSkinPendantConfigure> skinPendants_ = new RepeatedField<SkinSkinPendantConfigure>();

	public const int SkinPendantDictFieldNumber = 6;

	private static readonly MapField<int, SkinSkinPendantConfigure>.Codec _map_skinPendantDict_codec = new MapField<int, SkinSkinPendantConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SkinSkinPendantConfigure.Parser), 50u);

	private readonly MapField<int, SkinSkinPendantConfigure> skinPendantDict_ = new MapField<int, SkinSkinPendantConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SkinStandingPaintingConfigure> StandingPaintings => standingPaintings_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SkinStandingPaintingConfigure> StandingPaintingDict => standingPaintingDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SkinOffsetConfigure> Offsets => offsets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SkinOffsetConfigure> OffsetDict => offsetDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SkinSkinPendantConfigure> SkinPendants => skinPendants_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SkinSkinPendantConfigure> SkinPendantDict => skinPendantDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinConfigure(SkinConfigure other)
		: this()
	{
		standingPaintings_ = other.standingPaintings_.Clone();
		standingPaintingDict_ = other.standingPaintingDict_.Clone();
		offsets_ = other.offsets_.Clone();
		offsetDict_ = other.offsetDict_.Clone();
		skinPendants_ = other.skinPendants_.Clone();
		skinPendantDict_ = other.skinPendantDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinConfigure Clone()
	{
		return new SkinConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!standingPaintings_.Equals(other.standingPaintings_))
		{
			return false;
		}
		if (!StandingPaintingDict.Equals(other.StandingPaintingDict))
		{
			return false;
		}
		if (!offsets_.Equals(other.offsets_))
		{
			return false;
		}
		if (!OffsetDict.Equals(other.OffsetDict))
		{
			return false;
		}
		if (!skinPendants_.Equals(other.skinPendants_))
		{
			return false;
		}
		if (!SkinPendantDict.Equals(other.SkinPendantDict))
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
		num ^= standingPaintings_.GetHashCode();
		num ^= StandingPaintingDict.GetHashCode();
		num ^= offsets_.GetHashCode();
		num ^= OffsetDict.GetHashCode();
		num ^= skinPendants_.GetHashCode();
		num ^= SkinPendantDict.GetHashCode();
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
		standingPaintings_.WriteTo(ref output, _repeated_standingPaintings_codec);
		standingPaintingDict_.WriteTo(ref output, _map_standingPaintingDict_codec);
		offsets_.WriteTo(ref output, _repeated_offsets_codec);
		offsetDict_.WriteTo(ref output, _map_offsetDict_codec);
		skinPendants_.WriteTo(ref output, _repeated_skinPendants_codec);
		skinPendantDict_.WriteTo(ref output, _map_skinPendantDict_codec);
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
		num += standingPaintings_.CalculateSize(_repeated_standingPaintings_codec);
		num += standingPaintingDict_.CalculateSize(_map_standingPaintingDict_codec);
		num += offsets_.CalculateSize(_repeated_offsets_codec);
		num += offsetDict_.CalculateSize(_map_offsetDict_codec);
		num += skinPendants_.CalculateSize(_repeated_skinPendants_codec);
		num += skinPendantDict_.CalculateSize(_map_skinPendantDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SkinConfigure other)
	{
		if (other != null)
		{
			standingPaintings_.Add(other.standingPaintings_);
			standingPaintingDict_.MergeFrom(other.standingPaintingDict_);
			offsets_.Add(other.offsets_);
			offsetDict_.MergeFrom(other.offsetDict_);
			skinPendants_.Add(other.skinPendants_);
			skinPendantDict_.MergeFrom(other.skinPendantDict_);
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
				standingPaintings_.AddEntriesFrom(ref input, _repeated_standingPaintings_codec);
				break;
			case 18u:
				standingPaintingDict_.AddEntriesFrom(ref input, _map_standingPaintingDict_codec);
				break;
			case 26u:
				offsets_.AddEntriesFrom(ref input, _repeated_offsets_codec);
				break;
			case 34u:
				offsetDict_.AddEntriesFrom(ref input, _map_offsetDict_codec);
				break;
			case 42u:
				skinPendants_.AddEntriesFrom(ref input, _repeated_skinPendants_codec);
				break;
			case 50u:
				skinPendantDict_.AddEntriesFrom(ref input, _map_skinPendantDict_codec);
				break;
			}
		}
	}

	public void Fix()
	{
	}
}
