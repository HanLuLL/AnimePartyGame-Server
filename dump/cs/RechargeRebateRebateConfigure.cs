using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeRebateRebateConfigure : IMessage<RechargeRebateRebateConfigure>, IMessage, IEquatable<RechargeRebateRebateConfigure>, IDeepCloneable<RechargeRebateRebateConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeRebateRebateConfigure> _parser = new MessageParser<RechargeRebateRebateConfigure>(() => new RechargeRebateRebateConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int RechargeRebateRebateConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<RechargeRebateRebateConfigureItem> _repeated_rechargeRebateRebateConfigureItems_codec = FieldCodec.ForMessage(18u, RechargeRebateRebateConfigureItem.Parser);

	private readonly RepeatedField<RechargeRebateRebateConfigureItem> rechargeRebateRebateConfigureItems_ = new RepeatedField<RechargeRebateRebateConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeRebateRebateConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeRebateReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<RechargeRebateRebateConfigureItem> RechargeRebateRebateConfigureItems => rechargeRebateRebateConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateRebateConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateRebateConfigure(RechargeRebateRebateConfigure other)
		: this()
	{
		id_ = other.id_;
		rechargeRebateRebateConfigureItems_ = other.rechargeRebateRebateConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateRebateConfigure Clone()
	{
		return new RechargeRebateRebateConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeRebateRebateConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeRebateRebateConfigure other)
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
		if (!rechargeRebateRebateConfigureItems_.Equals(other.rechargeRebateRebateConfigureItems_))
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
		num ^= rechargeRebateRebateConfigureItems_.GetHashCode();
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
		rechargeRebateRebateConfigureItems_.WriteTo(ref output, _repeated_rechargeRebateRebateConfigureItems_codec);
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
		num += rechargeRebateRebateConfigureItems_.CalculateSize(_repeated_rechargeRebateRebateConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeRebateRebateConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			rechargeRebateRebateConfigureItems_.Add(other.rechargeRebateRebateConfigureItems_);
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
				rechargeRebateRebateConfigureItems_.AddEntriesFrom(ref input, _repeated_rechargeRebateRebateConfigureItems_codec);
				break;
			}
		}
	}
}
