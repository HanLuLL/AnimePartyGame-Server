using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixItemConfigure : IMessage<FixItemConfigure>, IMessage, IEquatable<FixItemConfigure>, IDeepCloneable<FixItemConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixItemConfigure> _parser = new MessageParser<FixItemConfigure>(() => new FixItemConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<FixItemInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, FixItemInfoConfigure.Parser);

	private readonly RepeatedField<FixItemInfoConfigure> infos_ = new RepeatedField<FixItemInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, FixItemInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, FixItemInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixItemInfoConfigure.Parser), 18u);

	private readonly MapField<int, FixItemInfoConfigure> infoDict_ = new MapField<int, FixItemInfoConfigure>();

	public const int ShieldsFieldNumber = 3;

	private static readonly FieldCodec<FixItemShieldConfigure> _repeated_shields_codec = FieldCodec.ForMessage(26u, FixItemShieldConfigure.Parser);

	private readonly RepeatedField<FixItemShieldConfigure> shields_ = new RepeatedField<FixItemShieldConfigure>();

	public const int ShieldDictFieldNumber = 4;

	private static readonly MapField<int, FixItemShieldConfigure>.Codec _map_shieldDict_codec = new MapField<int, FixItemShieldConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixItemShieldConfigure.Parser), 34u);

	private readonly MapField<int, FixItemShieldConfigure> shieldDict_ = new MapField<int, FixItemShieldConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixItemConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixItemReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixItemInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixItemInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixItemShieldConfigure> Shields => shields_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixItemShieldConfigure> ShieldDict => shieldDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixItemConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixItemConfigure(FixItemConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		shields_ = other.shields_.Clone();
		shieldDict_ = other.shieldDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixItemConfigure Clone()
	{
		return new FixItemConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixItemConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixItemConfigure other)
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
		if (!shields_.Equals(other.shields_))
		{
			return false;
		}
		if (!ShieldDict.Equals(other.ShieldDict))
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
		num ^= shields_.GetHashCode();
		num ^= ShieldDict.GetHashCode();
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
		shields_.WriteTo(ref output, _repeated_shields_codec);
		shieldDict_.WriteTo(ref output, _map_shieldDict_codec);
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
		num += shields_.CalculateSize(_repeated_shields_codec);
		num += shieldDict_.CalculateSize(_map_shieldDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixItemConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			shields_.Add(other.shields_);
			shieldDict_.MergeFrom(other.shieldDict_);
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
				shields_.AddEntriesFrom(ref input, _repeated_shields_codec);
				break;
			case 34u:
				shieldDict_.AddEntriesFrom(ref input, _map_shieldDict_codec);
				break;
			}
		}
	}
}
