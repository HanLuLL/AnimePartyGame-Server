using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class QuickCardStack : IMessage<QuickCardStack>, IMessage, IEquatable<QuickCardStack>, IDeepCloneable<QuickCardStack>, IBufferMessage
{
	private static readonly MessageParser<QuickCardStack> _parser = new MessageParser<QuickCardStack>(() => new QuickCardStack());

	private UnknownFieldSet _unknownFields;

	public const int OriginalPlayerIdFieldNumber = 1;

	private long originalPlayerId_;

	public const int OriginalTargetIdsFieldNumber = 2;

	private static readonly MapField<long, bool>.Codec _map_originalTargetIds_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 18u);

	private readonly MapField<long, bool> originalTargetIds_ = new MapField<long, bool>();

	public const int OriginalCardIdFieldNumber = 3;

	private int originalCardId_;

	public const int HistoryFieldNumber = 4;

	private static readonly FieldCodec<QuickCardRecord> _repeated_history_codec = FieldCodec.ForMessage(34u, QuickCardRecord.Parser);

	private readonly RepeatedField<QuickCardRecord> history_ = new RepeatedField<QuickCardRecord>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<QuickCardStack> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[62];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OriginalPlayerId
	{
		get
		{
			return originalPlayerId_;
		}
		set
		{
			originalPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> OriginalTargetIds => originalTargetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriginalCardId
	{
		get
		{
			return originalCardId_;
		}
		set
		{
			originalCardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<QuickCardRecord> History => history_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardStack()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardStack(QuickCardStack other)
		: this()
	{
		originalPlayerId_ = other.originalPlayerId_;
		originalTargetIds_ = other.originalTargetIds_.Clone();
		originalCardId_ = other.originalCardId_;
		history_ = other.history_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardStack Clone()
	{
		return new QuickCardStack(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as QuickCardStack);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(QuickCardStack other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (OriginalPlayerId != other.OriginalPlayerId)
		{
			return false;
		}
		if (!OriginalTargetIds.Equals(other.OriginalTargetIds))
		{
			return false;
		}
		if (OriginalCardId != other.OriginalCardId)
		{
			return false;
		}
		if (!history_.Equals(other.history_))
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
		if (OriginalPlayerId != 0L)
		{
			num ^= OriginalPlayerId.GetHashCode();
		}
		num ^= OriginalTargetIds.GetHashCode();
		if (OriginalCardId != 0)
		{
			num ^= OriginalCardId.GetHashCode();
		}
		num ^= history_.GetHashCode();
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
		if (OriginalPlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(OriginalPlayerId);
		}
		originalTargetIds_.WriteTo(ref output, _map_originalTargetIds_codec);
		if (OriginalCardId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OriginalCardId);
		}
		history_.WriteTo(ref output, _repeated_history_codec);
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
		if (OriginalPlayerId != 0L)
		{
			num += 9;
		}
		num += originalTargetIds_.CalculateSize(_map_originalTargetIds_codec);
		if (OriginalCardId != 0)
		{
			num += 5;
		}
		num += history_.CalculateSize(_repeated_history_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(QuickCardStack other)
	{
		if (other != null)
		{
			if (other.OriginalPlayerId != 0L)
			{
				OriginalPlayerId = other.OriginalPlayerId;
			}
			originalTargetIds_.MergeFrom(other.originalTargetIds_);
			if (other.OriginalCardId != 0)
			{
				OriginalCardId = other.OriginalCardId;
			}
			history_.Add(other.history_);
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
				OriginalPlayerId = input.ReadSFixed64();
				break;
			case 18u:
				originalTargetIds_.AddEntriesFrom(ref input, _map_originalTargetIds_codec);
				break;
			case 29u:
				OriginalCardId = input.ReadSFixed32();
				break;
			case 34u:
				history_.AddEntriesFrom(ref input, _repeated_history_codec);
				break;
			}
		}
	}
}
