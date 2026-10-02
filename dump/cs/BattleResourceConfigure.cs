using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattleResourceConfigure : IMessage<BattleResourceConfigure>, IMessage, IEquatable<BattleResourceConfigure>, IDeepCloneable<BattleResourceConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattleResourceConfigure> _parser = new MessageParser<BattleResourceConfigure>(() => new BattleResourceConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<BattleResourceInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, BattleResourceInfoConfigure.Parser);

	private readonly RepeatedField<BattleResourceInfoConfigure> infos_ = new RepeatedField<BattleResourceInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, BattleResourceInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, BattleResourceInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattleResourceInfoConfigure.Parser), 18u);

	private readonly MapField<int, BattleResourceInfoConfigure> infoDict_ = new MapField<int, BattleResourceInfoConfigure>();

	public const int PostProcesssFieldNumber = 3;

	private static readonly FieldCodec<BattleResourcePostProcessConfigure> _repeated_postProcesss_codec = FieldCodec.ForMessage(26u, BattleResourcePostProcessConfigure.Parser);

	private readonly RepeatedField<BattleResourcePostProcessConfigure> postProcesss_ = new RepeatedField<BattleResourcePostProcessConfigure>();

	public const int PostProcessDictFieldNumber = 4;

	private static readonly MapField<int, BattleResourcePostProcessConfigure>.Codec _map_postProcessDict_codec = new MapField<int, BattleResourcePostProcessConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattleResourcePostProcessConfigure.Parser), 34u);

	private readonly MapField<int, BattleResourcePostProcessConfigure> postProcessDict_ = new MapField<int, BattleResourcePostProcessConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattleResourceConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattleResourceReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattleResourceInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattleResourceInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattleResourcePostProcessConfigure> PostProcesss => postProcesss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattleResourcePostProcessConfigure> PostProcessDict => postProcessDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourceConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourceConfigure(BattleResourceConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		postProcesss_ = other.postProcesss_.Clone();
		postProcessDict_ = other.postProcessDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourceConfigure Clone()
	{
		return new BattleResourceConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattleResourceConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattleResourceConfigure other)
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
		if (!postProcesss_.Equals(other.postProcesss_))
		{
			return false;
		}
		if (!PostProcessDict.Equals(other.PostProcessDict))
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
		num ^= postProcesss_.GetHashCode();
		num ^= PostProcessDict.GetHashCode();
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
		postProcesss_.WriteTo(ref output, _repeated_postProcesss_codec);
		postProcessDict_.WriteTo(ref output, _map_postProcessDict_codec);
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
		num += postProcesss_.CalculateSize(_repeated_postProcesss_codec);
		num += postProcessDict_.CalculateSize(_map_postProcessDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattleResourceConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			postProcesss_.Add(other.postProcesss_);
			postProcessDict_.MergeFrom(other.postProcessDict_);
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
				postProcesss_.AddEntriesFrom(ref input, _repeated_postProcesss_codec);
				break;
			case 34u:
				postProcessDict_.AddEntriesFrom(ref input, _map_postProcessDict_codec);
				break;
			}
		}
	}
}
