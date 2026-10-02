using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class DeveloperConfigure : IMessage<DeveloperConfigure>, IMessage, IEquatable<DeveloperConfigure>, IDeepCloneable<DeveloperConfigure>, IBufferMessage
{
	private static readonly MessageParser<DeveloperConfigure> _parser = new MessageParser<DeveloperConfigure>(() => new DeveloperConfigure());

	private UnknownFieldSet _unknownFields;

	public const int DevelopersFieldNumber = 1;

	private static readonly FieldCodec<DeveloperDeveloperConfigure> _repeated_developers_codec = FieldCodec.ForMessage(10u, DeveloperDeveloperConfigure.Parser);

	private readonly RepeatedField<DeveloperDeveloperConfigure> developers_ = new RepeatedField<DeveloperDeveloperConfigure>();

	public const int DeveloperDictFieldNumber = 2;

	private static readonly MapField<int, DeveloperDeveloperConfigure>.Codec _map_developerDict_codec = new MapField<int, DeveloperDeveloperConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, DeveloperDeveloperConfigure.Parser), 18u);

	private readonly MapField<int, DeveloperDeveloperConfigure> developerDict_ = new MapField<int, DeveloperDeveloperConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DeveloperConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DeveloperReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<DeveloperDeveloperConfigure> Developers => developers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, DeveloperDeveloperConfigure> DeveloperDict => developerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DeveloperConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DeveloperConfigure(DeveloperConfigure other)
		: this()
	{
		developers_ = other.developers_.Clone();
		developerDict_ = other.developerDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DeveloperConfigure Clone()
	{
		return new DeveloperConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DeveloperConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DeveloperConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!developers_.Equals(other.developers_))
		{
			return false;
		}
		if (!DeveloperDict.Equals(other.DeveloperDict))
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
		num ^= developers_.GetHashCode();
		num ^= DeveloperDict.GetHashCode();
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
		developers_.WriteTo(ref output, _repeated_developers_codec);
		developerDict_.WriteTo(ref output, _map_developerDict_codec);
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
		num += developers_.CalculateSize(_repeated_developers_codec);
		num += developerDict_.CalculateSize(_map_developerDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DeveloperConfigure other)
	{
		if (other != null)
		{
			developers_.Add(other.developers_);
			developerDict_.MergeFrom(other.developerDict_);
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
				developers_.AddEntriesFrom(ref input, _repeated_developers_codec);
				break;
			case 18u:
				developerDict_.AddEntriesFrom(ref input, _map_developerDict_codec);
				break;
			}
		}
	}
}
