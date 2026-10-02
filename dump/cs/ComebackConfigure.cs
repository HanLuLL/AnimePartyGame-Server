using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ComebackConfigure : IMessage<ComebackConfigure>, IMessage, IEquatable<ComebackConfigure>, IDeepCloneable<ComebackConfigure>, IBufferMessage
{
	private static readonly MessageParser<ComebackConfigure> _parser = new MessageParser<ComebackConfigure>(() => new ComebackConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ParamssFieldNumber = 1;

	private static readonly FieldCodec<ComebackParamsConfigure> _repeated_paramss_codec = FieldCodec.ForMessage(10u, ComebackParamsConfigure.Parser);

	private readonly RepeatedField<ComebackParamsConfigure> paramss_ = new RepeatedField<ComebackParamsConfigure>();

	public const int ParamsDictFieldNumber = 2;

	private static readonly MapField<int, ComebackParamsConfigure>.Codec _map_paramsDict_codec = new MapField<int, ComebackParamsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ComebackParamsConfigure.Parser), 18u);

	private readonly MapField<int, ComebackParamsConfigure> paramsDict_ = new MapField<int, ComebackParamsConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ComebackConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ComebackReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ComebackParamsConfigure> Paramss => paramss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ComebackParamsConfigure> ParamsDict => paramsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ComebackConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ComebackConfigure(ComebackConfigure other)
		: this()
	{
		paramss_ = other.paramss_.Clone();
		paramsDict_ = other.paramsDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ComebackConfigure Clone()
	{
		return new ComebackConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ComebackConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ComebackConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!paramss_.Equals(other.paramss_))
		{
			return false;
		}
		if (!ParamsDict.Equals(other.ParamsDict))
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
		num ^= paramss_.GetHashCode();
		num ^= ParamsDict.GetHashCode();
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
		paramss_.WriteTo(ref output, _repeated_paramss_codec);
		paramsDict_.WriteTo(ref output, _map_paramsDict_codec);
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
		num += paramss_.CalculateSize(_repeated_paramss_codec);
		num += paramsDict_.CalculateSize(_map_paramsDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ComebackConfigure other)
	{
		if (other != null)
		{
			paramss_.Add(other.paramss_);
			paramsDict_.MergeFrom(other.paramsDict_);
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
				paramss_.AddEntriesFrom(ref input, _repeated_paramss_codec);
				break;
			case 18u:
				paramsDict_.AddEntriesFrom(ref input, _map_paramsDict_codec);
				break;
			}
		}
	}
}
