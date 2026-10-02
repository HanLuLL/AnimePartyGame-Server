using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysSyncPlayerMatchPunishmentTimeC2S : IMessage<SysSyncPlayerMatchPunishmentTimeC2S>, IMessage, IEquatable<SysSyncPlayerMatchPunishmentTimeC2S>, IDeepCloneable<SysSyncPlayerMatchPunishmentTimeC2S>, IBufferMessage
{
	private static readonly MessageParser<SysSyncPlayerMatchPunishmentTimeC2S> _parser = new MessageParser<SysSyncPlayerMatchPunishmentTimeC2S>(() => new SysSyncPlayerMatchPunishmentTimeC2S());

	private UnknownFieldSet _unknownFields;

	public const int MatchPunishmentTimeFieldNumber = 1;

	private long matchPunishmentTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysSyncPlayerMatchPunishmentTimeC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[511];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long MatchPunishmentTime
	{
		get
		{
			return matchPunishmentTime_;
		}
		set
		{
			matchPunishmentTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncPlayerMatchPunishmentTimeC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncPlayerMatchPunishmentTimeC2S(SysSyncPlayerMatchPunishmentTimeC2S other)
		: this()
	{
		matchPunishmentTime_ = other.matchPunishmentTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncPlayerMatchPunishmentTimeC2S Clone()
	{
		return new SysSyncPlayerMatchPunishmentTimeC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysSyncPlayerMatchPunishmentTimeC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysSyncPlayerMatchPunishmentTimeC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MatchPunishmentTime != other.MatchPunishmentTime)
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
		if (MatchPunishmentTime != 0L)
		{
			num ^= MatchPunishmentTime.GetHashCode();
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
		if (MatchPunishmentTime != 0L)
		{
			output.WriteRawTag(8);
			output.WriteInt64(MatchPunishmentTime);
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
		if (MatchPunishmentTime != 0L)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(MatchPunishmentTime);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysSyncPlayerMatchPunishmentTimeC2S other)
	{
		if (other != null)
		{
			if (other.MatchPunishmentTime != 0L)
			{
				MatchPunishmentTime = other.MatchPunishmentTime;
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
			if (num != 8)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				MatchPunishmentTime = input.ReadInt64();
			}
		}
	}
}
