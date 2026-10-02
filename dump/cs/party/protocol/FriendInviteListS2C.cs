using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class FriendInviteListS2C : IMessage<FriendInviteListS2C>, IMessage, IEquatable<FriendInviteListS2C>, IDeepCloneable<FriendInviteListS2C>, IBufferMessage
{
	private static readonly MessageParser<FriendInviteListS2C> _parser = new MessageParser<FriendInviteListS2C>(() => new FriendInviteListS2C());

	private UnknownFieldSet _unknownFields;

	public const int InfFieldNumber = 1;

	private static readonly FieldCodec<FriendInviteInfo> _repeated_inf_codec = FieldCodec.ForMessage(10u, FriendInviteInfo.Parser);

	private readonly RepeatedField<FriendInviteInfo> inf_ = new RepeatedField<FriendInviteInfo>();

	public const int IsEndFieldNumber = 2;

	private bool isEnd_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendInviteListS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[80];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendInviteInfo> Inf => inf_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsEnd
	{
		get
		{
			return isEnd_;
		}
		set
		{
			isEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteListS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteListS2C(FriendInviteListS2C other)
		: this()
	{
		inf_ = other.inf_.Clone();
		isEnd_ = other.isEnd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteListS2C Clone()
	{
		return new FriendInviteListS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendInviteListS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendInviteListS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!inf_.Equals(other.inf_))
		{
			return false;
		}
		if (IsEnd != other.IsEnd)
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
		num ^= inf_.GetHashCode();
		if (IsEnd)
		{
			num ^= IsEnd.GetHashCode();
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
		inf_.WriteTo(ref output, _repeated_inf_codec);
		if (IsEnd)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsEnd);
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
		num += inf_.CalculateSize(_repeated_inf_codec);
		if (IsEnd)
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
	public void MergeFrom(FriendInviteListS2C other)
	{
		if (other != null)
		{
			inf_.Add(other.inf_);
			if (other.IsEnd)
			{
				IsEnd = other.IsEnd;
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
			case 10u:
				inf_.AddEntriesFrom(ref input, _repeated_inf_codec);
				break;
			case 16u:
				IsEnd = input.ReadBool();
				break;
			}
		}
	}
}
