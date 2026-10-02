using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SqlPlayerActivityTaskList : IMessage<SqlPlayerActivityTaskList>, IMessage, IEquatable<SqlPlayerActivityTaskList>, IDeepCloneable<SqlPlayerActivityTaskList>, IBufferMessage
{
	private static readonly MessageParser<SqlPlayerActivityTaskList> _parser = new MessageParser<SqlPlayerActivityTaskList>(() => new SqlPlayerActivityTaskList());

	private UnknownFieldSet _unknownFields;

	public const int ActivityTasksFieldNumber = 1;

	private static readonly FieldCodec<TaskDSO> _repeated_activityTasks_codec = FieldCodec.ForMessage(10u, TaskDSO.Parser);

	private readonly RepeatedField<TaskDSO> activityTasks_ = new RepeatedField<TaskDSO>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SqlPlayerActivityTaskList> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TaskDSO> ActivityTasks => activityTasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerActivityTaskList()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerActivityTaskList(SqlPlayerActivityTaskList other)
		: this()
	{
		activityTasks_ = other.activityTasks_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerActivityTaskList Clone()
	{
		return new SqlPlayerActivityTaskList(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SqlPlayerActivityTaskList);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SqlPlayerActivityTaskList other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!activityTasks_.Equals(other.activityTasks_))
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
		num ^= activityTasks_.GetHashCode();
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
		activityTasks_.WriteTo(ref output, _repeated_activityTasks_codec);
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
		num += activityTasks_.CalculateSize(_repeated_activityTasks_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SqlPlayerActivityTaskList other)
	{
		if (other != null)
		{
			activityTasks_.Add(other.activityTasks_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				activityTasks_.AddEntriesFrom(ref input, _repeated_activityTasks_codec);
			}
		}
	}
}
