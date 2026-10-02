using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class FriendApplyListS2C : IMessage<FriendApplyListS2C>, IMessage, IEquatable<FriendApplyListS2C>, IDeepCloneable<FriendApplyListS2C>, IBufferMessage
{
	private static readonly MessageParser<FriendApplyListS2C> _parser = new MessageParser<FriendApplyListS2C>(() => new FriendApplyListS2C());

	private UnknownFieldSet _unknownFields;

	public const int ApplyFieldNumber = 1;

	private static readonly FieldCodec<FriendShowPlayerInfo> _repeated_apply_codec = FieldCodec.ForMessage(10u, FriendShowPlayerInfo.Parser);

	private readonly RepeatedField<FriendShowPlayerInfo> apply_ = new RepeatedField<FriendShowPlayerInfo>();

	public const int IsEndFieldNumber = 2;

	private bool isEnd_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendApplyListS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[72];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendShowPlayerInfo> Apply => apply_;

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
	public FriendApplyListS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyListS2C(FriendApplyListS2C other)
		: this()
	{
		apply_ = other.apply_.Clone();
		isEnd_ = other.isEnd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyListS2C Clone()
	{
		return new FriendApplyListS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendApplyListS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendApplyListS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!apply_.Equals(other.apply_))
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
		num ^= apply_.GetHashCode();
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
		apply_.WriteTo(ref output, _repeated_apply_codec);
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
		num += apply_.CalculateSize(_repeated_apply_codec);
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
	public void MergeFrom(FriendApplyListS2C other)
	{
		if (other != null)
		{
			apply_.Add(other.apply_);
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
				apply_.AddEntriesFrom(ref input, _repeated_apply_codec);
				break;
			case 16u:
				IsEnd = input.ReadBool();
				break;
			}
		}
	}
}
