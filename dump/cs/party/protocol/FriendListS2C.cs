using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class FriendListS2C : IMessage<FriendListS2C>, IMessage, IEquatable<FriendListS2C>, IDeepCloneable<FriendListS2C>, IBufferMessage
{
	private static readonly MessageParser<FriendListS2C> _parser = new MessageParser<FriendListS2C>(() => new FriendListS2C());

	private UnknownFieldSet _unknownFields;

	public const int FriendsFieldNumber = 1;

	private static readonly FieldCodec<FriendInfo> _repeated_friends_codec = FieldCodec.ForMessage(10u, FriendInfo.Parser);

	private readonly RepeatedField<FriendInfo> friends_ = new RepeatedField<FriendInfo>();

	public const int IsEndFieldNumber = 2;

	private bool isEnd_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendListS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[64];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendInfo> Friends => friends_;

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
	public FriendListS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendListS2C(FriendListS2C other)
		: this()
	{
		friends_ = other.friends_.Clone();
		isEnd_ = other.isEnd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendListS2C Clone()
	{
		return new FriendListS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendListS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendListS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!friends_.Equals(other.friends_))
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
		num ^= friends_.GetHashCode();
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
		friends_.WriteTo(ref output, _repeated_friends_codec);
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
		num += friends_.CalculateSize(_repeated_friends_codec);
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
	public void MergeFrom(FriendListS2C other)
	{
		if (other != null)
		{
			friends_.Add(other.friends_);
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
				friends_.AddEntriesFrom(ref input, _repeated_friends_codec);
				break;
			case 16u:
				IsEnd = input.ReadBool();
				break;
			}
		}
	}
}
