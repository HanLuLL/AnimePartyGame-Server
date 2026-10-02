using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerCardUpgradeConfigure : IMessage<SinglePlayerCardUpgradeConfigure>, IMessage, IEquatable<SinglePlayerCardUpgradeConfigure>, IDeepCloneable<SinglePlayerCardUpgradeConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerCardUpgradeConfigure> _parser = new MessageParser<SinglePlayerCardUpgradeConfigure>(() => new SinglePlayerCardUpgradeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SinglePlayerCardUpgradeConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<SinglePlayerCardUpgradeConfigureItem> _repeated_singlePlayerCardUpgradeConfigureItems_codec = FieldCodec.ForMessage(18u, SinglePlayerCardUpgradeConfigureItem.Parser);

	private readonly RepeatedField<SinglePlayerCardUpgradeConfigureItem> singlePlayerCardUpgradeConfigureItems_ = new RepeatedField<SinglePlayerCardUpgradeConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerCardUpgradeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[2];

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
	public RepeatedField<SinglePlayerCardUpgradeConfigureItem> SinglePlayerCardUpgradeConfigureItems => singlePlayerCardUpgradeConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardUpgradeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardUpgradeConfigure(SinglePlayerCardUpgradeConfigure other)
		: this()
	{
		id_ = other.id_;
		singlePlayerCardUpgradeConfigureItems_ = other.singlePlayerCardUpgradeConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardUpgradeConfigure Clone()
	{
		return new SinglePlayerCardUpgradeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerCardUpgradeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerCardUpgradeConfigure other)
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
		if (!singlePlayerCardUpgradeConfigureItems_.Equals(other.singlePlayerCardUpgradeConfigureItems_))
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
		num ^= singlePlayerCardUpgradeConfigureItems_.GetHashCode();
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
		singlePlayerCardUpgradeConfigureItems_.WriteTo(ref output, _repeated_singlePlayerCardUpgradeConfigureItems_codec);
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
		num += singlePlayerCardUpgradeConfigureItems_.CalculateSize(_repeated_singlePlayerCardUpgradeConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerCardUpgradeConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			singlePlayerCardUpgradeConfigureItems_.Add(other.singlePlayerCardUpgradeConfigureItems_);
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
				singlePlayerCardUpgradeConfigureItems_.AddEntriesFrom(ref input, _repeated_singlePlayerCardUpgradeConfigureItems_codec);
				break;
			}
		}
	}
}
