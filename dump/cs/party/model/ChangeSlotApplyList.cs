using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ChangeSlotApplyList : IMessage<ChangeSlotApplyList>, IMessage, IEquatable<ChangeSlotApplyList>, IDeepCloneable<ChangeSlotApplyList>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum ApplyStatus
		{
			[OriginalName("ApplyStatus_Process")]
			Process,
			[OriginalName("ApplyStatus_Cancel")]
			Cancel,
			[OriginalName("ApplyStatus_Agree")]
			Agree,
			[OriginalName("ApplyStatus_Reject")]
			Reject
		}
	}

	private static readonly MessageParser<ChangeSlotApplyList> _parser = new MessageParser<ChangeSlotApplyList>(() => new ChangeSlotApplyList());

	private UnknownFieldSet _unknownFields;

	public const int ApplyIdFieldNumber = 1;

	private long applyId_;

	public const int TargetIdFieldNumber = 2;

	private long targetId_;

	public const int ApplyStatusFieldNumber = 3;

	private Types.ApplyStatus applyStatus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChangeSlotApplyList> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[76];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ApplyId
	{
		get
		{
			return applyId_;
		}
		set
		{
			applyId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TargetId
	{
		get
		{
			return targetId_;
		}
		set
		{
			targetId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.ApplyStatus ApplyStatus
	{
		get
		{
			return applyStatus_;
		}
		set
		{
			applyStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeSlotApplyList()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeSlotApplyList(ChangeSlotApplyList other)
		: this()
	{
		applyId_ = other.applyId_;
		targetId_ = other.targetId_;
		applyStatus_ = other.applyStatus_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeSlotApplyList Clone()
	{
		return new ChangeSlotApplyList(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChangeSlotApplyList);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChangeSlotApplyList other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ApplyId != other.ApplyId)
		{
			return false;
		}
		if (TargetId != other.TargetId)
		{
			return false;
		}
		if (ApplyStatus != other.ApplyStatus)
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
		if (ApplyId != 0L)
		{
			num ^= ApplyId.GetHashCode();
		}
		if (TargetId != 0L)
		{
			num ^= TargetId.GetHashCode();
		}
		if (ApplyStatus != Types.ApplyStatus.Process)
		{
			num ^= ApplyStatus.GetHashCode();
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
		if (ApplyId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(ApplyId);
		}
		if (TargetId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(TargetId);
		}
		if (ApplyStatus != Types.ApplyStatus.Process)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)ApplyStatus);
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
		if (ApplyId != 0L)
		{
			num += 9;
		}
		if (TargetId != 0L)
		{
			num += 9;
		}
		if (ApplyStatus != Types.ApplyStatus.Process)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ApplyStatus);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChangeSlotApplyList other)
	{
		if (other != null)
		{
			if (other.ApplyId != 0L)
			{
				ApplyId = other.ApplyId;
			}
			if (other.TargetId != 0L)
			{
				TargetId = other.TargetId;
			}
			if (other.ApplyStatus != Types.ApplyStatus.Process)
			{
				ApplyStatus = other.ApplyStatus;
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
				ApplyId = input.ReadSFixed64();
				break;
			case 17u:
				TargetId = input.ReadSFixed64();
				break;
			case 24u:
				ApplyStatus = (Types.ApplyStatus)input.ReadEnum();
				break;
			}
		}
	}
}
