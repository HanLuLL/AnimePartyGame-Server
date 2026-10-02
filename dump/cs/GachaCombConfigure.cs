using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GachaCombConfigure : IMessage<GachaCombConfigure>, IMessage, IEquatable<GachaCombConfigure>, IDeepCloneable<GachaCombConfigure>, IBufferMessage
{
	private static readonly MessageParser<GachaCombConfigure> _parser = new MessageParser<GachaCombConfigure>(() => new GachaCombConfigure());

	private UnknownFieldSet _unknownFields;

	public const int CombIDFieldNumber = 1;

	private int combID_;

	public const int GachaCombConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<GachaCombConfigureItem> _repeated_gachaCombConfigureItems_codec = FieldCodec.ForMessage(18u, GachaCombConfigureItem.Parser);

	private readonly RepeatedField<GachaCombConfigureItem> gachaCombConfigureItems_ = new RepeatedField<GachaCombConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaCombConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CombID
	{
		get
		{
			return combID_;
		}
		private set
		{
			combID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaCombConfigureItem> GachaCombConfigureItems => gachaCombConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCombConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCombConfigure(GachaCombConfigure other)
		: this()
	{
		combID_ = other.combID_;
		gachaCombConfigureItems_ = other.gachaCombConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCombConfigure Clone()
	{
		return new GachaCombConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaCombConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaCombConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CombID != other.CombID)
		{
			return false;
		}
		if (!gachaCombConfigureItems_.Equals(other.gachaCombConfigureItems_))
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
		if (CombID != 0)
		{
			num ^= CombID.GetHashCode();
		}
		num ^= gachaCombConfigureItems_.GetHashCode();
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
		if (CombID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(CombID);
		}
		gachaCombConfigureItems_.WriteTo(ref output, _repeated_gachaCombConfigureItems_codec);
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
		if (CombID != 0)
		{
			num += 5;
		}
		num += gachaCombConfigureItems_.CalculateSize(_repeated_gachaCombConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GachaCombConfigure other)
	{
		if (other != null)
		{
			if (other.CombID != 0)
			{
				CombID = other.CombID;
			}
			gachaCombConfigureItems_.Add(other.gachaCombConfigureItems_);
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
				CombID = input.ReadSFixed32();
				break;
			case 18u:
				gachaCombConfigureItems_.AddEntriesFrom(ref input, _repeated_gachaCombConfigureItems_codec);
				break;
			}
		}
	}
}
