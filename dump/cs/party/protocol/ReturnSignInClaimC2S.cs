using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ReturnSignInClaimC2S : IMessage<ReturnSignInClaimC2S>, IMessage, IEquatable<ReturnSignInClaimC2S>, IDeepCloneable<ReturnSignInClaimC2S>, IBufferMessage
{
	private static readonly MessageParser<ReturnSignInClaimC2S> _parser = new MessageParser<ReturnSignInClaimC2S>(() => new ReturnSignInClaimC2S());

	private UnknownFieldSet _unknownFields;

	public const int ActIdFieldNumber = 1;

	private int actId_;

	public const int IsAdvancedFieldNumber = 2;

	private bool isAdvanced_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ReturnSignInClaimC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[310];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActId
	{
		get
		{
			return actId_;
		}
		set
		{
			actId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAdvanced
	{
		get
		{
			return isAdvanced_;
		}
		set
		{
			isAdvanced_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimC2S(ReturnSignInClaimC2S other)
		: this()
	{
		actId_ = other.actId_;
		isAdvanced_ = other.isAdvanced_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimC2S Clone()
	{
		return new ReturnSignInClaimC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ReturnSignInClaimC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ReturnSignInClaimC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ActId != other.ActId)
		{
			return false;
		}
		if (IsAdvanced != other.IsAdvanced)
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
		if (ActId != 0)
		{
			num ^= ActId.GetHashCode();
		}
		if (IsAdvanced)
		{
			num ^= IsAdvanced.GetHashCode();
		}
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
		if (ActId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ActId);
		}
		if (IsAdvanced)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsAdvanced);
		}
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
		if (ActId != 0)
		{
			num += 5;
		}
		if (IsAdvanced)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ReturnSignInClaimC2S other)
	{
		if (other != null)
		{
			if (other.ActId != 0)
			{
				ActId = other.ActId;
			}
			if (other.IsAdvanced)
			{
				IsAdvanced = other.IsAdvanced;
			}
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
				ActId = input.ReadSFixed32();
				break;
			case 16u:
				IsAdvanced = input.ReadBool();
				break;
			}
		}
	}
}
