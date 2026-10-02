using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RechargeRebateConfigure : IMessage<RechargeRebateConfigure>, IMessage, IEquatable<RechargeRebateConfigure>, IDeepCloneable<RechargeRebateConfigure>, IBufferMessage
{
	private static readonly MessageParser<RechargeRebateConfigure> _parser = new MessageParser<RechargeRebateConfigure>(() => new RechargeRebateConfigure());

	private UnknownFieldSet _unknownFields;

	public const int RebatesFieldNumber = 1;

	private static readonly FieldCodec<RechargeRebateRebateConfigure> _repeated_rebates_codec = FieldCodec.ForMessage(10u, RechargeRebateRebateConfigure.Parser);

	private readonly RepeatedField<RechargeRebateRebateConfigure> rebates_ = new RepeatedField<RechargeRebateRebateConfigure>();

	public const int RebateDictFieldNumber = 2;

	private static readonly MapField<int, RechargeRebateRebateConfigure>.Codec _map_rebateDict_codec = new MapField<int, RechargeRebateRebateConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RechargeRebateRebateConfigure.Parser), 18u);

	private readonly MapField<int, RechargeRebateRebateConfigure> rebateDict_ = new MapField<int, RechargeRebateRebateConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeRebateConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RechargeRebateReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RechargeRebateRebateConfigure> Rebates => rebates_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RechargeRebateRebateConfigure> RebateDict => rebateDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateConfigure(RechargeRebateConfigure other)
		: this()
	{
		rebates_ = other.rebates_.Clone();
		rebateDict_ = other.rebateDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeRebateConfigure Clone()
	{
		return new RechargeRebateConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeRebateConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeRebateConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!rebates_.Equals(other.rebates_))
		{
			return false;
		}
		if (!RebateDict.Equals(other.RebateDict))
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
		num ^= rebates_.GetHashCode();
		num ^= RebateDict.GetHashCode();
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
		rebates_.WriteTo(ref output, _repeated_rebates_codec);
		rebateDict_.WriteTo(ref output, _map_rebateDict_codec);
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
		num += rebates_.CalculateSize(_repeated_rebates_codec);
		num += rebateDict_.CalculateSize(_map_rebateDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeRebateConfigure other)
	{
		if (other != null)
		{
			rebates_.Add(other.rebates_);
			rebateDict_.MergeFrom(other.rebateDict_);
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
				rebates_.AddEntriesFrom(ref input, _repeated_rebates_codec);
				break;
			case 18u:
				rebateDict_.AddEntriesFrom(ref input, _map_rebateDict_codec);
				break;
			}
		}
	}
}
