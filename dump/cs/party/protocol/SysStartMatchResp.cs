using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.code;

namespace party.protocol;

public sealed class SysStartMatchResp : IMessage<SysStartMatchResp>, IMessage, IEquatable<SysStartMatchResp>, IDeepCloneable<SysStartMatchResp>, IBufferMessage
{
	private static readonly MessageParser<SysStartMatchResp> _parser = new MessageParser<SysStartMatchResp>(() => new SysStartMatchResp());

	private UnknownFieldSet _unknownFields;

	public const int CodeFieldNumber = 1;

	private Code code_;

	public const int TeamIdFieldNumber = 2;

	private long teamId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysStartMatchResp> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => InteriorReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Code Code
	{
		get
		{
			return code_;
		}
		set
		{
			code_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TeamId
	{
		get
		{
			return teamId_;
		}
		set
		{
			teamId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysStartMatchResp()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysStartMatchResp(SysStartMatchResp other)
		: this()
	{
		code_ = other.code_;
		teamId_ = other.teamId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysStartMatchResp Clone()
	{
		return new SysStartMatchResp(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysStartMatchResp);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysStartMatchResp other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Code != other.Code)
		{
			return false;
		}
		if (TeamId != other.TeamId)
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
		if (Code != Code.Succ)
		{
			num ^= Code.GetHashCode();
		}
		if (TeamId != 0L)
		{
			num ^= TeamId.GetHashCode();
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
		if (Code != Code.Succ)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)Code);
		}
		if (TeamId != 0L)
		{
			output.WriteRawTag(16);
			output.WriteInt64(TeamId);
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
		if (Code != Code.Succ)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Code);
		}
		if (TeamId != 0L)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(TeamId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysStartMatchResp other)
	{
		if (other != null)
		{
			if (other.Code != Code.Succ)
			{
				Code = other.Code;
			}
			if (other.TeamId != 0L)
			{
				TeamId = other.TeamId;
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
			case 8u:
				Code = (Code)input.ReadEnum();
				break;
			case 16u:
				TeamId = input.ReadInt64();
				break;
			}
		}
	}
}
