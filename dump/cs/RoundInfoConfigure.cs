using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RoundInfoConfigure : IMessage<RoundInfoConfigure>, IMessage, IEquatable<RoundInfoConfigure>, IDeepCloneable<RoundInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<RoundInfoConfigure> _parser = new MessageParser<RoundInfoConfigure>(() => new RoundInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int RoundTypeFieldNumber = 1;

	private RoundType roundType_;

	public const int ParamsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(18u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RoundInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RoundReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundType RoundType
	{
		get
		{
			return roundType_;
		}
		private set
		{
			roundType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundInfoConfigure(RoundInfoConfigure other)
		: this()
	{
		roundType_ = other.roundType_;
		params_ = other.params_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundInfoConfigure Clone()
	{
		return new RoundInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RoundInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RoundInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RoundType != other.RoundType)
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
		if (RoundType != RoundType.None)
		{
			num ^= RoundType.GetHashCode();
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
		if (RoundType != RoundType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)RoundType);
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
		if (RoundType != RoundType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)RoundType);
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
	public void MergeFrom(RoundInfoConfigure other)
	{
		if (other != null)
		{
			if (other.RoundType != RoundType.None)
			{
				RoundType = other.RoundType;
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
			case 8u:
				RoundType = (RoundType)input.ReadEnum();
				break;
			case 16u:
			case 18u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			}
		}
	}
}
