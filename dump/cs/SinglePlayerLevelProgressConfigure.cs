using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerLevelProgressConfigure : IMessage<SinglePlayerLevelProgressConfigure>, IMessage, IEquatable<SinglePlayerLevelProgressConfigure>, IDeepCloneable<SinglePlayerLevelProgressConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerLevelProgressConfigure> _parser = new MessageParser<SinglePlayerLevelProgressConfigure>(() => new SinglePlayerLevelProgressConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SinglePlayerLevelProgressConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<SinglePlayerLevelProgressConfigureItem> _repeated_singlePlayerLevelProgressConfigureItems_codec = FieldCodec.ForMessage(18u, SinglePlayerLevelProgressConfigureItem.Parser);

	private readonly RepeatedField<SinglePlayerLevelProgressConfigureItem> singlePlayerLevelProgressConfigureItems_ = new RepeatedField<SinglePlayerLevelProgressConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerLevelProgressConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SinglePlayerLevelProgressConfigureItem> SinglePlayerLevelProgressConfigureItems => singlePlayerLevelProgressConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelProgressConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelProgressConfigure(SinglePlayerLevelProgressConfigure other)
		: this()
	{
		id_ = other.id_;
		singlePlayerLevelProgressConfigureItems_ = other.singlePlayerLevelProgressConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelProgressConfigure Clone()
	{
		return new SinglePlayerLevelProgressConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerLevelProgressConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerLevelProgressConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (!singlePlayerLevelProgressConfigureItems_.Equals(other.singlePlayerLevelProgressConfigureItems_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		num ^= singlePlayerLevelProgressConfigureItems_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		singlePlayerLevelProgressConfigureItems_.WriteTo(ref output, _repeated_singlePlayerLevelProgressConfigureItems_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		num += singlePlayerLevelProgressConfigureItems_.CalculateSize(_repeated_singlePlayerLevelProgressConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerLevelProgressConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			singlePlayerLevelProgressConfigureItems_.Add(other.singlePlayerLevelProgressConfigureItems_);
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
				Id = input.ReadSFixed32();
				break;
			case 18u:
				singlePlayerLevelProgressConfigureItems_.AddEntriesFrom(ref input, _repeated_singlePlayerLevelProgressConfigureItems_codec);
				break;
			}
		}
	}
}
