using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ScratchCardRecord : IMessage<ScratchCardRecord>, IMessage, IEquatable<ScratchCardRecord>, IDeepCloneable<ScratchCardRecord>, IBufferMessage
{
	private static readonly MessageParser<ScratchCardRecord> _parser = new MessageParser<ScratchCardRecord>(() => new ScratchCardRecord());

	private UnknownFieldSet _unknownFields;

	public const int PoolFieldNumber = 1;

	private static readonly MapField<int, ScratchCardPool>.Codec _map_pool_codec = new MapField<int, ScratchCardPool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ScratchCardPool.Parser), 10u);

	private readonly MapField<int, ScratchCardPool> pool_ = new MapField<int, ScratchCardPool>();

	public const int CurrentPoolIdFieldNumber = 2;

	private int currentPoolId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ScratchCardRecord> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[39];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ScratchCardPool> Pool => pool_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrentPoolId
	{
		get
		{
			return currentPoolId_;
		}
		set
		{
			currentPoolId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ScratchCardRecord()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ScratchCardRecord(ScratchCardRecord other)
		: this()
	{
		pool_ = other.pool_.Clone();
		currentPoolId_ = other.currentPoolId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ScratchCardRecord Clone()
	{
		return new ScratchCardRecord(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ScratchCardRecord);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ScratchCardRecord other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Pool.Equals(other.Pool))
		{
			return false;
		}
		if (CurrentPoolId != other.CurrentPoolId)
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
		num ^= Pool.GetHashCode();
		if (CurrentPoolId != 0)
		{
			num ^= CurrentPoolId.GetHashCode();
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
		pool_.WriteTo(ref output, _map_pool_codec);
		if (CurrentPoolId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CurrentPoolId);
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
		num += pool_.CalculateSize(_map_pool_codec);
		if (CurrentPoolId != 0)
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
	public void MergeFrom(ScratchCardRecord other)
	{
		if (other != null)
		{
			pool_.MergeFrom(other.pool_);
			if (other.CurrentPoolId != 0)
			{
				CurrentPoolId = other.CurrentPoolId;
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
			case 10u:
				pool_.AddEntriesFrom(ref input, _map_pool_codec);
				break;
			case 21u:
				CurrentPoolId = input.ReadSFixed32();
				break;
			}
		}
	}
}
