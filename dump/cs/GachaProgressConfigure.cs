using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GachaProgressConfigure : IMessage<GachaProgressConfigure>, IMessage, IEquatable<GachaProgressConfigure>, IDeepCloneable<GachaProgressConfigure>, IBufferMessage
{
	private static readonly MessageParser<GachaProgressConfigure> _parser = new MessageParser<GachaProgressConfigure>(() => new GachaProgressConfigure());

	private UnknownFieldSet _unknownFields;

	public const int PoolIDFieldNumber = 1;

	private int poolID_;

	public const int GachaProgressConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<GachaProgressConfigureItem> _repeated_gachaProgressConfigureItems_codec = FieldCodec.ForMessage(18u, GachaProgressConfigureItem.Parser);

	private readonly RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems_ = new RepeatedField<GachaProgressConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaProgressConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PoolID
	{
		get
		{
			return poolID_;
		}
		private set
		{
			poolID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GachaProgressConfigureItem> GachaProgressConfigureItems => gachaProgressConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaProgressConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaProgressConfigure(GachaProgressConfigure other)
		: this()
	{
		poolID_ = other.poolID_;
		gachaProgressConfigureItems_ = other.gachaProgressConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaProgressConfigure Clone()
	{
		return new GachaProgressConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaProgressConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaProgressConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PoolID != other.PoolID)
		{
			return false;
		}
		if (!gachaProgressConfigureItems_.Equals(other.gachaProgressConfigureItems_))
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
		if (PoolID != 0)
		{
			num ^= PoolID.GetHashCode();
		}
		num ^= gachaProgressConfigureItems_.GetHashCode();
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
		if (PoolID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(PoolID);
		}
		gachaProgressConfigureItems_.WriteTo(ref output, _repeated_gachaProgressConfigureItems_codec);
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
		if (PoolID != 0)
		{
			num += 5;
		}
		num += gachaProgressConfigureItems_.CalculateSize(_repeated_gachaProgressConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GachaProgressConfigure other)
	{
		if (other != null)
		{
			if (other.PoolID != 0)
			{
				PoolID = other.PoolID;
			}
			gachaProgressConfigureItems_.Add(other.gachaProgressConfigureItems_);
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
				PoolID = input.ReadSFixed32();
				break;
			case 18u:
				gachaProgressConfigureItems_.AddEntriesFrom(ref input, _repeated_gachaProgressConfigureItems_codec);
				break;
			}
		}
	}
}
