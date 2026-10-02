using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GuideInfoConfigure : IMessage<GuideInfoConfigure>, IMessage, IEquatable<GuideInfoConfigure>, IDeepCloneable<GuideInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<GuideInfoConfigure> _parser = new MessageParser<GuideInfoConfigure>(() => new GuideInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GuideIdFieldNumber = 1;

	private int guideId_;

	public const int GuideInfoConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<GuideInfoConfigureItem> _repeated_guideInfoConfigureItems_codec = FieldCodec.ForMessage(18u, GuideInfoConfigureItem.Parser);

	private readonly RepeatedField<GuideInfoConfigureItem> guideInfoConfigureItems_ = new RepeatedField<GuideInfoConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuideInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuideReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GuideId
	{
		get
		{
			return guideId_;
		}
		private set
		{
			guideId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GuideInfoConfigureItem> GuideInfoConfigureItems => guideInfoConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideInfoConfigure(GuideInfoConfigure other)
		: this()
	{
		guideId_ = other.guideId_;
		guideInfoConfigureItems_ = other.guideInfoConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideInfoConfigure Clone()
	{
		return new GuideInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuideInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuideInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GuideId != other.GuideId)
		{
			return false;
		}
		if (!guideInfoConfigureItems_.Equals(other.guideInfoConfigureItems_))
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
		if (GuideId != 0)
		{
			num ^= GuideId.GetHashCode();
		}
		num ^= guideInfoConfigureItems_.GetHashCode();
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
		if (GuideId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GuideId);
		}
		guideInfoConfigureItems_.WriteTo(ref output, _repeated_guideInfoConfigureItems_codec);
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
		if (GuideId != 0)
		{
			num += 5;
		}
		num += guideInfoConfigureItems_.CalculateSize(_repeated_guideInfoConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GuideInfoConfigure other)
	{
		if (other != null)
		{
			if (other.GuideId != 0)
			{
				GuideId = other.GuideId;
			}
			guideInfoConfigureItems_.Add(other.guideInfoConfigureItems_);
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
			case 13u:
				GuideId = input.ReadSFixed32();
				break;
			case 18u:
				guideInfoConfigureItems_.AddEntriesFrom(ref input, _repeated_guideInfoConfigureItems_codec);
				break;
			}
		}
	}
}
