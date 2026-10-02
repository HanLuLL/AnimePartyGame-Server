using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ReturnSignInClaimS2C : IMessage<ReturnSignInClaimS2C>, IMessage, IEquatable<ReturnSignInClaimS2C>, IDeepCloneable<ReturnSignInClaimS2C>, IBufferMessage
{
	private static readonly MessageParser<ReturnSignInClaimS2C> _parser = new MessageParser<ReturnSignInClaimS2C>(() => new ReturnSignInClaimS2C());

	private UnknownFieldSet _unknownFields;

	public const int SignInFieldNumber = 1;

	private ReturnSignIn signIn_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ReturnSignInClaimS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[311];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignIn SignIn
	{
		get
		{
			return signIn_;
		}
		set
		{
			signIn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimS2C(ReturnSignInClaimS2C other)
		: this()
	{
		signIn_ = ((other.signIn_ != null) ? other.signIn_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimS2C Clone()
	{
		return new ReturnSignInClaimS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ReturnSignInClaimS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ReturnSignInClaimS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(SignIn, other.SignIn))
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
		if (signIn_ != null)
		{
			num ^= SignIn.GetHashCode();
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
		if (signIn_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(SignIn);
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
		if (signIn_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SignIn);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ReturnSignInClaimS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.signIn_ != null)
		{
			if (signIn_ == null)
			{
				SignIn = new ReturnSignIn();
			}
			SignIn.MergeFrom(other.SignIn);
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				continue;
			}
			if (signIn_ == null)
			{
				SignIn = new ReturnSignIn();
			}
			input.ReadMessage(SignIn);
		}
	}
}
