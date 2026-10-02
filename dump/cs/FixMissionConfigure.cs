using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixMissionConfigure : IMessage<FixMissionConfigure>, IMessage, IEquatable<FixMissionConfigure>, IDeepCloneable<FixMissionConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixMissionConfigure> _parser = new MessageParser<FixMissionConfigure>(() => new FixMissionConfigure());

	private UnknownFieldSet _unknownFields;

	public const int DatasFieldNumber = 1;

	private static readonly FieldCodec<FixMissionDataConfigure> _repeated_datas_codec = FieldCodec.ForMessage(10u, FixMissionDataConfigure.Parser);

	private readonly RepeatedField<FixMissionDataConfigure> datas_ = new RepeatedField<FixMissionDataConfigure>();

	public const int DataDictFieldNumber = 2;

	private static readonly MapField<int, FixMissionDataConfigure>.Codec _map_dataDict_codec = new MapField<int, FixMissionDataConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixMissionDataConfigure.Parser), 18u);

	private readonly MapField<int, FixMissionDataConfigure> dataDict_ = new MapField<int, FixMissionDataConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixMissionConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixMissionReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixMissionDataConfigure> Datas => datas_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixMissionDataConfigure> DataDict => dataDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMissionConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMissionConfigure(FixMissionConfigure other)
		: this()
	{
		datas_ = other.datas_.Clone();
		dataDict_ = other.dataDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMissionConfigure Clone()
	{
		return new FixMissionConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixMissionConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixMissionConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!datas_.Equals(other.datas_))
		{
			return false;
		}
		if (!DataDict.Equals(other.DataDict))
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
		num ^= datas_.GetHashCode();
		num ^= DataDict.GetHashCode();
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
		datas_.WriteTo(ref output, _repeated_datas_codec);
		dataDict_.WriteTo(ref output, _map_dataDict_codec);
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
		num += datas_.CalculateSize(_repeated_datas_codec);
		num += dataDict_.CalculateSize(_map_dataDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixMissionConfigure other)
	{
		if (other != null)
		{
			datas_.Add(other.datas_);
			dataDict_.MergeFrom(other.dataDict_);
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
				datas_.AddEntriesFrom(ref input, _repeated_datas_codec);
				break;
			case 18u:
				dataDict_.AddEntriesFrom(ref input, _map_dataDict_codec);
				break;
			}
		}
	}
}
