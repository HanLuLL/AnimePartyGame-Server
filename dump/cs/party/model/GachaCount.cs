using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GachaCount : IMessage<GachaCount>, IMessage, IEquatable<GachaCount>, IDeepCloneable<GachaCount>, IBufferMessage
{
	private static readonly MessageParser<GachaCount> _parser = new MessageParser<GachaCount>(() => new GachaCount());

	private UnknownFieldSet _unknownFields;

	public const int GuaranteeFieldNumber = 1;

	private int guarantee_;

	public const int RoleFieldNumber = 2;

	private int role_;

	public const int RoleUpFieldNumber = 3;

	private int roleUp_;

	public const int CountFieldNumber = 4;

	private int count_;

	public const int PoolIdFieldNumber = 5;

	private int poolId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaCount> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[47];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Guarantee
	{
		get
		{
			return guarantee_;
		}
		set
		{
			guarantee_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Role
	{
		get
		{
			return role_;
		}
		set
		{
			role_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoleUp
	{
		get
		{
			return roleUp_;
		}
		set
		{
			roleUp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Count
	{
		get
		{
			return count_;
		}
		set
		{
			count_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PoolId
	{
		get
		{
			return poolId_;
		}
		set
		{
			poolId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCount()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCount(GachaCount other)
		: this()
	{
		guarantee_ = other.guarantee_;
		role_ = other.role_;
		roleUp_ = other.roleUp_;
		count_ = other.count_;
		poolId_ = other.poolId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCount Clone()
	{
		return new GachaCount(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaCount);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaCount other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Guarantee != other.Guarantee)
		{
			return false;
		}
		if (Role != other.Role)
		{
			return false;
		}
		if (RoleUp != other.RoleUp)
		{
			return false;
		}
		if (Count != other.Count)
		{
			return false;
		}
		if (PoolId != other.PoolId)
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
		if (Guarantee != 0)
		{
			num ^= Guarantee.GetHashCode();
		}
		if (Role != 0)
		{
			num ^= Role.GetHashCode();
		}
		if (RoleUp != 0)
		{
			num ^= RoleUp.GetHashCode();
		}
		if (Count != 0)
		{
			num ^= Count.GetHashCode();
		}
		if (PoolId != 0)
		{
			num ^= PoolId.GetHashCode();
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
		if (Guarantee != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Guarantee);
		}
		if (Role != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Role);
		}
		if (RoleUp != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(RoleUp);
		}
		if (Count != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Count);
		}
		if (PoolId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(PoolId);
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
		if (Guarantee != 0)
		{
			num += 5;
		}
		if (Role != 0)
		{
			num += 5;
		}
		if (RoleUp != 0)
		{
			num += 5;
		}
		if (Count != 0)
		{
			num += 5;
		}
		if (PoolId != 0)
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
	public void MergeFrom(GachaCount other)
	{
		if (other != null)
		{
			if (other.Guarantee != 0)
			{
				Guarantee = other.Guarantee;
			}
			if (other.Role != 0)
			{
				Role = other.Role;
			}
			if (other.RoleUp != 0)
			{
				RoleUp = other.RoleUp;
			}
			if (other.Count != 0)
			{
				Count = other.Count;
			}
			if (other.PoolId != 0)
			{
				PoolId = other.PoolId;
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
				Guarantee = input.ReadSFixed32();
				break;
			case 21u:
				Role = input.ReadSFixed32();
				break;
			case 29u:
				RoleUp = input.ReadSFixed32();
				break;
			case 37u:
				Count = input.ReadSFixed32();
				break;
			case 45u:
				PoolId = input.ReadSFixed32();
				break;
			}
		}
	}
}
