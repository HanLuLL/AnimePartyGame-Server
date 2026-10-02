using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ServerConfigure : IMessage<ServerConfigure>, IMessage, IEquatable<ServerConfigure>, IDeepCloneable<ServerConfigure>, IBufferMessage
{
	private static readonly MessageParser<ServerConfigure> _parser = new MessageParser<ServerConfigure>(() => new ServerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ErrorsFieldNumber = 1;

	private static readonly FieldCodec<ServerErrorConfigure> _repeated_errors_codec = FieldCodec.ForMessage(10u, ServerErrorConfigure.Parser);

	private readonly RepeatedField<ServerErrorConfigure> errors_ = new RepeatedField<ServerErrorConfigure>();

	public const int ErrorDictFieldNumber = 2;

	private static readonly MapField<int, ServerErrorConfigure>.Codec _map_errorDict_codec = new MapField<int, ServerErrorConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ServerErrorConfigure.Parser), 18u);

	private readonly MapField<int, ServerErrorConfigure> errorDict_ = new MapField<int, ServerErrorConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ServerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ServerReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ServerErrorConfigure> Errors => errors_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ServerErrorConfigure> ErrorDict => errorDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerConfigure(ServerConfigure other)
		: this()
	{
		errors_ = other.errors_.Clone();
		errorDict_ = other.errorDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ServerConfigure Clone()
	{
		return new ServerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ServerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ServerConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!errors_.Equals(other.errors_))
		{
			return false;
		}
		if (!ErrorDict.Equals(other.ErrorDict))
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
		num ^= errors_.GetHashCode();
		num ^= ErrorDict.GetHashCode();
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
		errors_.WriteTo(ref output, _repeated_errors_codec);
		errorDict_.WriteTo(ref output, _map_errorDict_codec);
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
		num += errors_.CalculateSize(_repeated_errors_codec);
		num += errorDict_.CalculateSize(_map_errorDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ServerConfigure other)
	{
		if (other != null)
		{
			errors_.Add(other.errors_);
			errorDict_.MergeFrom(other.errorDict_);
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
				errors_.AddEntriesFrom(ref input, _repeated_errors_codec);
				break;
			case 18u:
				errorDict_.AddEntriesFrom(ref input, _map_errorDict_codec);
				break;
			}
		}
	}
}
