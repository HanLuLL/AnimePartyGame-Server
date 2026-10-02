using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class RoleCardBreakThroughC2S : IMessage<RoleCardBreakThroughC2S>, IMessage, IEquatable<RoleCardBreakThroughC2S>, IDeepCloneable<RoleCardBreakThroughC2S>, IBufferMessage
{
	private static readonly MessageParser<RoleCardBreakThroughC2S> _parser = new MessageParser<RoleCardBreakThroughC2S>(() => new RoleCardBreakThroughC2S());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int IsSuperFieldNumber = 2;

	private bool isSuper_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RoleCardBreakThroughC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[223];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefId
	{
		get
		{
			return defId_;
		}
		set
		{
			defId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsSuper
	{
		get
		{
			return isSuper_;
		}
		set
		{
			isSuper_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardBreakThroughC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardBreakThroughC2S(RoleCardBreakThroughC2S other)
		: this()
	{
		defId_ = other.defId_;
		isSuper_ = other.isSuper_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardBreakThroughC2S Clone()
	{
		return new RoleCardBreakThroughC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RoleCardBreakThroughC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RoleCardBreakThroughC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DefId != other.DefId)
		{
			return false;
		}
		if (IsSuper != other.IsSuper)
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
		if (DefId != 0)
		{
			num ^= DefId.GetHashCode();
		}
		if (IsSuper)
		{
			num ^= IsSuper.GetHashCode();
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
		if (DefId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DefId);
		}
		if (IsSuper)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsSuper);
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
		if (DefId != 0)
		{
			num += 5;
		}
		if (IsSuper)
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
	public void MergeFrom(RoleCardBreakThroughC2S other)
	{
		if (other != null)
		{
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			if (other.IsSuper)
			{
				IsSuper = other.IsSuper;
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
				DefId = input.ReadSFixed32();
				break;
			case 16u:
				IsSuper = input.ReadBool();
				break;
			}
		}
	}
}
