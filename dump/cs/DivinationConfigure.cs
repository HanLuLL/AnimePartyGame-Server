using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class DivinationConfigure : IMessage<DivinationConfigure>, IMessage, IEquatable<DivinationConfigure>, IDeepCloneable<DivinationConfigure>, IBufferMessage
{
	private static readonly MessageParser<DivinationConfigure> _parser = new MessageParser<DivinationConfigure>(() => new DivinationConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<DivinationInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, DivinationInfoConfigure.Parser);

	private readonly RepeatedField<DivinationInfoConfigure> infos_ = new RepeatedField<DivinationInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, DivinationInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, DivinationInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, DivinationInfoConfigure.Parser), 18u);

	private readonly MapField<int, DivinationInfoConfigure> infoDict_ = new MapField<int, DivinationInfoConfigure>();

	public const int TargetsFieldNumber = 3;

	private static readonly FieldCodec<DivinationTargetConfigure> _repeated_targets_codec = FieldCodec.ForMessage(26u, DivinationTargetConfigure.Parser);

	private readonly RepeatedField<DivinationTargetConfigure> targets_ = new RepeatedField<DivinationTargetConfigure>();

	public const int TargetDictFieldNumber = 4;

	private static readonly MapField<int, DivinationTargetConfigure>.Codec _map_targetDict_codec = new MapField<int, DivinationTargetConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, DivinationTargetConfigure.Parser), 34u);

	private readonly MapField<int, DivinationTargetConfigure> targetDict_ = new MapField<int, DivinationTargetConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DivinationConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DivinationReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<DivinationInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, DivinationInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<DivinationTargetConfigure> Targets => targets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, DivinationTargetConfigure> TargetDict => targetDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DivinationConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DivinationConfigure(DivinationConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		targets_ = other.targets_.Clone();
		targetDict_ = other.targetDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DivinationConfigure Clone()
	{
		return new DivinationConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DivinationConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DivinationConfigure other)
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
		if (!targets_.Equals(other.targets_))
		{
			return false;
		}
		if (!TargetDict.Equals(other.TargetDict))
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
		num ^= targets_.GetHashCode();
		num ^= TargetDict.GetHashCode();
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
		targets_.WriteTo(ref output, _repeated_targets_codec);
		targetDict_.WriteTo(ref output, _map_targetDict_codec);
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
		num += targets_.CalculateSize(_repeated_targets_codec);
		num += targetDict_.CalculateSize(_map_targetDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DivinationConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			targets_.Add(other.targets_);
			targetDict_.MergeFrom(other.targetDict_);
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
				targets_.AddEntriesFrom(ref input, _repeated_targets_codec);
				break;
			case 34u:
				targetDict_.AddEntriesFrom(ref input, _map_targetDict_codec);
				break;
			}
		}
	}
}
