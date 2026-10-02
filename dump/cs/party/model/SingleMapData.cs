using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleMapData : IMessage<SingleMapData>, IMessage, IEquatable<SingleMapData>, IDeepCloneable<SingleMapData>, IBufferMessage
{
	private static readonly MessageParser<SingleMapData> _parser = new MessageParser<SingleMapData>(() => new SingleMapData());

	private UnknownFieldSet _unknownFields;

	public const int CurrentMissionProgressFieldNumber = 1;

	private int currentMissionProgress_;

	public const int FinishMissionIndexFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_finishMissionIndex_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> finishMissionIndex_ = new RepeatedField<int>();

	public const int WastefoundationFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_wastefoundation_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> wastefoundation_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleMapData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[110];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrentMissionProgress
	{
		get
		{
			return currentMissionProgress_;
		}
		set
		{
			currentMissionProgress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> FinishMissionIndex => finishMissionIndex_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Wastefoundation => wastefoundation_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleMapData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleMapData(SingleMapData other)
		: this()
	{
		currentMissionProgress_ = other.currentMissionProgress_;
		finishMissionIndex_ = other.finishMissionIndex_.Clone();
		wastefoundation_ = other.wastefoundation_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleMapData Clone()
	{
		return new SingleMapData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleMapData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleMapData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CurrentMissionProgress != other.CurrentMissionProgress)
		{
			return false;
		}
		if (!finishMissionIndex_.Equals(other.finishMissionIndex_))
		{
			return false;
		}
		if (!wastefoundation_.Equals(other.wastefoundation_))
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
		if (CurrentMissionProgress != 0)
		{
			num ^= CurrentMissionProgress.GetHashCode();
		}
		num ^= finishMissionIndex_.GetHashCode();
		num ^= wastefoundation_.GetHashCode();
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
		if (CurrentMissionProgress != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(CurrentMissionProgress);
		}
		finishMissionIndex_.WriteTo(ref output, _repeated_finishMissionIndex_codec);
		wastefoundation_.WriteTo(ref output, _repeated_wastefoundation_codec);
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
		if (CurrentMissionProgress != 0)
		{
			num += 5;
		}
		num += finishMissionIndex_.CalculateSize(_repeated_finishMissionIndex_codec);
		num += wastefoundation_.CalculateSize(_repeated_wastefoundation_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SingleMapData other)
	{
		if (other != null)
		{
			if (other.CurrentMissionProgress != 0)
			{
				CurrentMissionProgress = other.CurrentMissionProgress;
			}
			finishMissionIndex_.Add(other.finishMissionIndex_);
			wastefoundation_.Add(other.wastefoundation_);
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
				CurrentMissionProgress = input.ReadSFixed32();
				break;
			case 18u:
			case 21u:
				finishMissionIndex_.AddEntriesFrom(ref input, _repeated_finishMissionIndex_codec);
				break;
			case 26u:
			case 29u:
				wastefoundation_.AddEntriesFrom(ref input, _repeated_wastefoundation_codec);
				break;
			}
		}
	}
}
