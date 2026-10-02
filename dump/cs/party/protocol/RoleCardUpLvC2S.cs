using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class RoleCardUpLvC2S : IMessage<RoleCardUpLvC2S>, IMessage, IEquatable<RoleCardUpLvC2S>, IDeepCloneable<RoleCardUpLvC2S>, IBufferMessage
{
	private static readonly MessageParser<RoleCardUpLvC2S> _parser = new MessageParser<RoleCardUpLvC2S>(() => new RoleCardUpLvC2S());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int ItemDefIdFieldNumber = 2;

	private int itemDefId_;

	public const int ItemCountFieldNumber = 3;

	private int itemCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RoleCardUpLvC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[215];

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
	public int ItemDefId
	{
		get
		{
			return itemDefId_;
		}
		set
		{
			itemDefId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemCount
	{
		get
		{
			return itemCount_;
		}
		set
		{
			itemCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardUpLvC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardUpLvC2S(RoleCardUpLvC2S other)
		: this()
	{
		defId_ = other.defId_;
		itemDefId_ = other.itemDefId_;
		itemCount_ = other.itemCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardUpLvC2S Clone()
	{
		return new RoleCardUpLvC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RoleCardUpLvC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RoleCardUpLvC2S other)
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
		if (ItemDefId != other.ItemDefId)
		{
			return false;
		}
		if (ItemCount != other.ItemCount)
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
		if (ItemDefId != 0)
		{
			num ^= ItemDefId.GetHashCode();
		}
		if (ItemCount != 0)
		{
			num ^= ItemCount.GetHashCode();
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
		if (ItemDefId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ItemDefId);
		}
		if (ItemCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ItemCount);
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
		if (ItemDefId != 0)
		{
			num += 5;
		}
		if (ItemCount != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RoleCardUpLvC2S other)
	{
		if (other != null)
		{
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			if (other.ItemDefId != 0)
			{
				ItemDefId = other.ItemDefId;
			}
			if (other.ItemCount != 0)
			{
				ItemCount = other.ItemCount;
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
			case 21u:
				ItemDefId = input.ReadSFixed32();
				break;
			case 29u:
				ItemCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
