using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class FriendInviteC2S : IMessage<FriendInviteC2S>, IMessage, IEquatable<FriendInviteC2S>, IDeepCloneable<FriendInviteC2S>, IBufferMessage
{
	private static readonly MessageParser<FriendInviteC2S> _parser = new MessageParser<FriendInviteC2S>(() => new FriendInviteC2S());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_playerId_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> playerId_ = new RepeatedField<long>();

	public const int PwdFieldNumber = 2;

	private string pwd_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendInviteC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[77];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> PlayerId => playerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Pwd
	{
		get
		{
			return pwd_;
		}
		set
		{
			pwd_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteC2S(FriendInviteC2S other)
		: this()
	{
		playerId_ = other.playerId_.Clone();
		pwd_ = other.pwd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteC2S Clone()
	{
		return new FriendInviteC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendInviteC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendInviteC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!playerId_.Equals(other.playerId_))
		{
			return false;
		}
		if (Pwd != other.Pwd)
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
		num ^= playerId_.GetHashCode();
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
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
		playerId_.WriteTo(ref output, _repeated_playerId_codec);
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Pwd);
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
		num += playerId_.CalculateSize(_repeated_playerId_codec);
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendInviteC2S other)
	{
		if (other != null)
		{
			playerId_.Add(other.playerId_);
			if (other.Pwd.Length != 0)
			{
				Pwd = other.Pwd;
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
			case 9u:
			case 10u:
				playerId_.AddEntriesFrom(ref input, _repeated_playerId_codec);
				break;
			case 18u:
				Pwd = input.ReadString();
				break;
			}
		}
	}
}
