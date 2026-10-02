using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class STRGameModeConfigure : IMessage<STRGameModeConfigure>, IMessage, IEquatable<STRGameModeConfigure>, IDeepCloneable<STRGameModeConfigure>, IBufferMessage
{
	private static readonly MessageParser<STRGameModeConfigure> _parser = new MessageParser<STRGameModeConfigure>(() => new STRGameModeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LocalsFieldNumber = 1;

	private static readonly FieldCodec<STRGameModeLocalConfigure> _repeated_locals_codec = FieldCodec.ForMessage(10u, STRGameModeLocalConfigure.Parser);

	private readonly RepeatedField<STRGameModeLocalConfigure> locals_ = new RepeatedField<STRGameModeLocalConfigure>();

	public const int LocalDictFieldNumber = 2;

	private static readonly MapField<int, STRGameModeLocalConfigure>.Codec _map_localDict_codec = new MapField<int, STRGameModeLocalConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, STRGameModeLocalConfigure.Parser), 18u);

	private readonly MapField<int, STRGameModeLocalConfigure> localDict_ = new MapField<int, STRGameModeLocalConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<STRGameModeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => STRGameModeReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<STRGameModeLocalConfigure> Locals => locals_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, STRGameModeLocalConfigure> LocalDict => localDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public STRGameModeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public STRGameModeConfigure(STRGameModeConfigure other)
		: this()
	{
		locals_ = other.locals_.Clone();
		localDict_ = other.localDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public STRGameModeConfigure Clone()
	{
		return new STRGameModeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as STRGameModeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(STRGameModeConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!locals_.Equals(other.locals_))
		{
			return false;
		}
		if (!LocalDict.Equals(other.LocalDict))
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
		num ^= locals_.GetHashCode();
		num ^= LocalDict.GetHashCode();
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
		locals_.WriteTo(ref output, _repeated_locals_codec);
		localDict_.WriteTo(ref output, _map_localDict_codec);
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
		num += locals_.CalculateSize(_repeated_locals_codec);
		num += localDict_.CalculateSize(_map_localDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(STRGameModeConfigure other)
	{
		if (other != null)
		{
			locals_.Add(other.locals_);
			localDict_.MergeFrom(other.localDict_);
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
				locals_.AddEntriesFrom(ref input, _repeated_locals_codec);
				break;
			case 18u:
				localDict_.AddEntriesFrom(ref input, _map_localDict_codec);
				break;
			}
		}
	}
}
