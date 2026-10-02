using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ImageLocalizationConfigure : IMessage<ImageLocalizationConfigure>, IMessage, IEquatable<ImageLocalizationConfigure>, IDeepCloneable<ImageLocalizationConfigure>, IBufferMessage
{
	private static readonly MessageParser<ImageLocalizationConfigure> _parser = new MessageParser<ImageLocalizationConfigure>(() => new ImageLocalizationConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LocalsFieldNumber = 1;

	private static readonly FieldCodec<ImageLocalizationLocalConfigure> _repeated_locals_codec = FieldCodec.ForMessage(10u, ImageLocalizationLocalConfigure.Parser);

	private readonly RepeatedField<ImageLocalizationLocalConfigure> locals_ = new RepeatedField<ImageLocalizationLocalConfigure>();

	public const int LocalDictFieldNumber = 2;

	private static readonly MapField<int, ImageLocalizationLocalConfigure>.Codec _map_localDict_codec = new MapField<int, ImageLocalizationLocalConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ImageLocalizationLocalConfigure.Parser), 18u);

	private readonly MapField<int, ImageLocalizationLocalConfigure> localDict_ = new MapField<int, ImageLocalizationLocalConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ImageLocalizationConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ImageLocalizationReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ImageLocalizationLocalConfigure> Locals => locals_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ImageLocalizationLocalConfigure> LocalDict => localDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ImageLocalizationConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ImageLocalizationConfigure(ImageLocalizationConfigure other)
		: this()
	{
		locals_ = other.locals_.Clone();
		localDict_ = other.localDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ImageLocalizationConfigure Clone()
	{
		return new ImageLocalizationConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ImageLocalizationConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ImageLocalizationConfigure other)
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
	public void MergeFrom(ImageLocalizationConfigure other)
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
