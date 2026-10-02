using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattlePassNewConfigure : IMessage<BattlePassNewConfigure>, IMessage, IEquatable<BattlePassNewConfigure>, IDeepCloneable<BattlePassNewConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattlePassNewConfigure> _parser = new MessageParser<BattlePassNewConfigure>(() => new BattlePassNewConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<BattlePassNewInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, BattlePassNewInfoConfigure.Parser);

	private readonly RepeatedField<BattlePassNewInfoConfigure> infos_ = new RepeatedField<BattlePassNewInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, BattlePassNewInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, BattlePassNewInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassNewInfoConfigure.Parser), 18u);

	private readonly MapField<int, BattlePassNewInfoConfigure> infoDict_ = new MapField<int, BattlePassNewInfoConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassNewConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassNewReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassNewInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassNewInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewConfigure(BattlePassNewConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewConfigure Clone()
	{
		return new BattlePassNewConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassNewConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassNewConfigure other)
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassNewConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
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
			}
		}
	}
}
