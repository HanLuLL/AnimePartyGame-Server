using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class InviteInfo : IMessage<InviteInfo>, IMessage, IEquatable<InviteInfo>, IDeepCloneable<InviteInfo>, IBufferMessage
{
	private static readonly MessageParser<InviteInfo> _parser = new MessageParser<InviteInfo>(() => new InviteInfo());

	private UnknownFieldSet _unknownFields;

	public const int InviterFieldNumber = 1;

	private long inviter_;

	public const int TaskFinishIdsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_taskFinishIds_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> taskFinishIds_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<InviteInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[11];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Inviter
	{
		get
		{
			return inviter_;
		}
		set
		{
			inviter_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TaskFinishIds => taskFinishIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfo(InviteInfo other)
		: this()
	{
		inviter_ = other.inviter_;
		taskFinishIds_ = other.taskFinishIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfo Clone()
	{
		return new InviteInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as InviteInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(InviteInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Inviter != other.Inviter)
		{
			return false;
		}
		if (!taskFinishIds_.Equals(other.taskFinishIds_))
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
		if (Inviter != 0L)
		{
			num ^= Inviter.GetHashCode();
		}
		num ^= taskFinishIds_.GetHashCode();
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
		if (Inviter != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Inviter);
		}
		taskFinishIds_.WriteTo(ref output, _repeated_taskFinishIds_codec);
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
		if (Inviter != 0L)
		{
			num += 9;
		}
		num += taskFinishIds_.CalculateSize(_repeated_taskFinishIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(InviteInfo other)
	{
		if (other != null)
		{
			if (other.Inviter != 0L)
			{
				Inviter = other.Inviter;
			}
			taskFinishIds_.Add(other.taskFinishIds_);
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
				Inviter = input.ReadSFixed64();
				break;
			case 18u:
			case 21u:
				taskFinishIds_.AddEntriesFrom(ref input, _repeated_taskFinishIds_codec);
				break;
			}
		}
	}
}
