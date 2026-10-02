using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class LandConfigure : IMessage<LandConfigure>, IMessage, IEquatable<LandConfigure>, IDeepCloneable<LandConfigure>, IBufferMessage
{
	private static readonly MessageParser<LandConfigure> _parser = new MessageParser<LandConfigure>(() => new LandConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<LandInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, LandInfoConfigure.Parser);

	private readonly RepeatedField<LandInfoConfigure> infos_ = new RepeatedField<LandInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, LandInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, LandInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, LandInfoConfigure.Parser), 18u);

	private readonly MapField<int, LandInfoConfigure> infoDict_ = new MapField<int, LandInfoConfigure>();

	public const int RollGoldsFieldNumber = 3;

	private static readonly FieldCodec<LandRollGoldConfigure> _repeated_rollGolds_codec = FieldCodec.ForMessage(26u, LandRollGoldConfigure.Parser);

	private readonly RepeatedField<LandRollGoldConfigure> rollGolds_ = new RepeatedField<LandRollGoldConfigure>();

	public const int RollGoldDictFieldNumber = 4;

	private static readonly MapField<int, LandRollGoldConfigure>.Codec _map_rollGoldDict_codec = new MapField<int, LandRollGoldConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, LandRollGoldConfigure.Parser), 34u);

	private readonly MapField<int, LandRollGoldConfigure> rollGoldDict_ = new MapField<int, LandRollGoldConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LandConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LandReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<LandInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LandInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<LandRollGoldConfigure> RollGolds => rollGolds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LandRollGoldConfigure> RollGoldDict => rollGoldDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandConfigure(LandConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		rollGolds_ = other.rollGolds_.Clone();
		rollGoldDict_ = other.rollGoldDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandConfigure Clone()
	{
		return new LandConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LandConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LandConfigure other)
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
		if (!rollGolds_.Equals(other.rollGolds_))
		{
			return false;
		}
		if (!RollGoldDict.Equals(other.RollGoldDict))
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
		num ^= rollGolds_.GetHashCode();
		num ^= RollGoldDict.GetHashCode();
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
		rollGolds_.WriteTo(ref output, _repeated_rollGolds_codec);
		rollGoldDict_.WriteTo(ref output, _map_rollGoldDict_codec);
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
		num += rollGolds_.CalculateSize(_repeated_rollGolds_codec);
		num += rollGoldDict_.CalculateSize(_map_rollGoldDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LandConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			rollGolds_.Add(other.rollGolds_);
			rollGoldDict_.MergeFrom(other.rollGoldDict_);
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
				rollGolds_.AddEntriesFrom(ref input, _repeated_rollGolds_codec);
				break;
			case 34u:
				rollGoldDict_.AddEntriesFrom(ref input, _map_rollGoldDict_codec);
				break;
			}
		}
	}
}
