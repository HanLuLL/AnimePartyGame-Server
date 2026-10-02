using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ConditionData : IMessage<ConditionData>, IMessage, IEquatable<ConditionData>, IDeepCloneable<ConditionData>, IBufferMessage
{
	private static readonly MessageParser<ConditionData> _parser = new MessageParser<ConditionData>(() => new ConditionData());

	private UnknownFieldSet _unknownFields;

	public const int CondTypeFieldNumber = 1;

	private int condType_;

	public const int ParamsFieldNumber = 2;

	private static readonly FieldCodec<ConditionParams> _repeated_params_codec = FieldCodec.ForMessage(18u, ConditionParams.Parser);

	private readonly RepeatedField<ConditionParams> params_ = new RepeatedField<ConditionParams>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ConditionData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[43];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CondType
	{
		get
		{
			return condType_;
		}
		set
		{
			condType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ConditionParams> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionData(ConditionData other)
		: this()
	{
		condType_ = other.condType_;
		params_ = other.params_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionData Clone()
	{
		return new ConditionData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ConditionData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ConditionData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CondType != other.CondType)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
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
		if (CondType != 0)
		{
			num ^= CondType.GetHashCode();
		}
		num ^= params_.GetHashCode();
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
		if (CondType != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(CondType);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
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
		if (CondType != 0)
		{
			num += 5;
		}
		num += params_.CalculateSize(_repeated_params_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ConditionData other)
	{
		if (other != null)
		{
			if (other.CondType != 0)
			{
				CondType = other.CondType;
			}
			params_.Add(other.params_);
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
				CondType = input.ReadSFixed32();
				break;
			case 18u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			}
		}
	}
}
