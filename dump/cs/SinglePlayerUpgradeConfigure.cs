using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerUpgradeConfigure : IMessage<SinglePlayerUpgradeConfigure>, IMessage, IEquatable<SinglePlayerUpgradeConfigure>, IDeepCloneable<SinglePlayerUpgradeConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerUpgradeConfigure> _parser = new MessageParser<SinglePlayerUpgradeConfigure>(() => new SinglePlayerUpgradeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SinglePlayerUpgradeConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<SinglePlayerUpgradeConfigureItem> _repeated_singlePlayerUpgradeConfigureItems_codec = FieldCodec.ForMessage(18u, SinglePlayerUpgradeConfigureItem.Parser);

	private readonly RepeatedField<SinglePlayerUpgradeConfigureItem> singlePlayerUpgradeConfigureItems_ = new RepeatedField<SinglePlayerUpgradeConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerUpgradeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<SinglePlayerUpgradeConfigureItem> SinglePlayerUpgradeConfigureItems => singlePlayerUpgradeConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerUpgradeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerUpgradeConfigure(SinglePlayerUpgradeConfigure other)
		: this()
	{
		id_ = other.id_;
		singlePlayerUpgradeConfigureItems_ = other.singlePlayerUpgradeConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerUpgradeConfigure Clone()
	{
		return new SinglePlayerUpgradeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerUpgradeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerUpgradeConfigure other)
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
		if (!singlePlayerUpgradeConfigureItems_.Equals(other.singlePlayerUpgradeConfigureItems_))
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
		num ^= singlePlayerUpgradeConfigureItems_.GetHashCode();
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
		singlePlayerUpgradeConfigureItems_.WriteTo(ref output, _repeated_singlePlayerUpgradeConfigureItems_codec);
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
		num += singlePlayerUpgradeConfigureItems_.CalculateSize(_repeated_singlePlayerUpgradeConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerUpgradeConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			singlePlayerUpgradeConfigureItems_.Add(other.singlePlayerUpgradeConfigureItems_);
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
				singlePlayerUpgradeConfigureItems_.AddEntriesFrom(ref input, _repeated_singlePlayerUpgradeConfigureItems_codec);
				break;
			}
		}
	}
}
