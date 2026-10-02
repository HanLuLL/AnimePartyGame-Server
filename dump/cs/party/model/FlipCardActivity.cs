using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class FlipCardActivity : IMessage<FlipCardActivity>, IMessage, IEquatable<FlipCardActivity>, IDeepCloneable<FlipCardActivity>, IBufferMessage
{
	private static readonly MessageParser<FlipCardActivity> _parser = new MessageParser<FlipCardActivity>(() => new FlipCardActivity());

	private UnknownFieldSet _unknownFields;

	public const int RecordsFieldNumber = 1;

	private static readonly MapField<int, bool>.Codec _map_records_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 10u);

	private readonly MapField<int, bool> records_ = new MapField<int, bool>();

	public const int ProgressRewardFieldNumber = 2;

	private static readonly MapField<int, bool>.Codec _map_progressReward_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 18u);

	private readonly MapField<int, bool> progressReward_ = new MapField<int, bool>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FlipCardActivity> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[41];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> Records => records_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> ProgressReward => progressReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardActivity()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardActivity(FlipCardActivity other)
		: this()
	{
		records_ = other.records_.Clone();
		progressReward_ = other.progressReward_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardActivity Clone()
	{
		return new FlipCardActivity(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FlipCardActivity);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FlipCardActivity other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Records.Equals(other.Records))
		{
			return false;
		}
		if (!ProgressReward.Equals(other.ProgressReward))
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
		num ^= Records.GetHashCode();
		num ^= ProgressReward.GetHashCode();
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
		records_.WriteTo(ref output, _map_records_codec);
		progressReward_.WriteTo(ref output, _map_progressReward_codec);
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
		num += records_.CalculateSize(_map_records_codec);
		num += progressReward_.CalculateSize(_map_progressReward_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FlipCardActivity other)
	{
		if (other != null)
		{
			records_.MergeFrom(other.records_);
			progressReward_.MergeFrom(other.progressReward_);
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
			case 10u:
				records_.AddEntriesFrom(ref input, _map_records_codec);
				break;
			case 18u:
				progressReward_.AddEntriesFrom(ref input, _map_progressReward_codec);
				break;
			}
		}
	}
}
