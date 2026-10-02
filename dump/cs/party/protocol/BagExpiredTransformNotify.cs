using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class BagExpiredTransformNotify : IMessage<BagExpiredTransformNotify>, IMessage, IEquatable<BagExpiredTransformNotify>, IDeepCloneable<BagExpiredTransformNotify>, IBufferMessage
{
	private static readonly MessageParser<BagExpiredTransformNotify> _parser = new MessageParser<BagExpiredTransformNotify>(() => new BagExpiredTransformNotify());

	private UnknownFieldSet _unknownFields;

	public const int ExpiredItemsFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_expiredItems_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8u, 0), FieldCodec.ForInt32(16u, 0), 10u);

	private readonly MapField<int, int> expiredItems_ = new MapField<int, int>();

	public const int RewardItemsFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_rewardItems_codec = new MapField<int, int>.Codec(FieldCodec.ForInt32(8u, 0), FieldCodec.ForInt32(16u, 0), 18u);

	private readonly MapField<int, int> rewardItems_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BagExpiredTransformNotify> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[449];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ExpiredItems => expiredItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RewardItems => rewardItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagExpiredTransformNotify()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagExpiredTransformNotify(BagExpiredTransformNotify other)
		: this()
	{
		expiredItems_ = other.expiredItems_.Clone();
		rewardItems_ = other.rewardItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagExpiredTransformNotify Clone()
	{
		return new BagExpiredTransformNotify(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BagExpiredTransformNotify);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BagExpiredTransformNotify other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!ExpiredItems.Equals(other.ExpiredItems))
		{
			return false;
		}
		if (!RewardItems.Equals(other.RewardItems))
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
		num ^= ExpiredItems.GetHashCode();
		num ^= RewardItems.GetHashCode();
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
		expiredItems_.WriteTo(ref output, _map_expiredItems_codec);
		rewardItems_.WriteTo(ref output, _map_rewardItems_codec);
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
		num += expiredItems_.CalculateSize(_map_expiredItems_codec);
		num += rewardItems_.CalculateSize(_map_rewardItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BagExpiredTransformNotify other)
	{
		if (other != null)
		{
			expiredItems_.MergeFrom(other.expiredItems_);
			rewardItems_.MergeFrom(other.rewardItems_);
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
				expiredItems_.AddEntriesFrom(ref input, _map_expiredItems_codec);
				break;
			case 18u:
				rewardItems_.AddEntriesFrom(ref input, _map_rewardItems_codec);
				break;
			}
		}
	}
}
