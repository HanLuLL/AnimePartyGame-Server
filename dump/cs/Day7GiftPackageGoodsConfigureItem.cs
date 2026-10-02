using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class Day7GiftPackageGoodsConfigureItem : IMessage<Day7GiftPackageGoodsConfigureItem>, IMessage, IEquatable<Day7GiftPackageGoodsConfigureItem>, IDeepCloneable<Day7GiftPackageGoodsConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<Day7GiftPackageGoodsConfigureItem> _parser = new MessageParser<Day7GiftPackageGoodsConfigureItem>(() => new Day7GiftPackageGoodsConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int DayNumbFieldNumber = 1;

	private int dayNumb_;

	public const int RewardFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Day7GiftPackageGoodsConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => Day7GiftPackageReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DayNumb
	{
		get
		{
			return dayNumb_;
		}
		private set
		{
			dayNumb_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageGoodsConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageGoodsConfigureItem(Day7GiftPackageGoodsConfigureItem other)
		: this()
	{
		dayNumb_ = other.dayNumb_;
		reward_ = other.reward_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7GiftPackageGoodsConfigureItem Clone()
	{
		return new Day7GiftPackageGoodsConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Day7GiftPackageGoodsConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Day7GiftPackageGoodsConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DayNumb != other.DayNumb)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
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
		if (DayNumb != 0)
		{
			num ^= DayNumb.GetHashCode();
		}
		num ^= Reward.GetHashCode();
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
		if (DayNumb != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DayNumb);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
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
		if (DayNumb != 0)
		{
			num += 5;
		}
		num += reward_.CalculateSize(_map_reward_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(Day7GiftPackageGoodsConfigureItem other)
	{
		if (other != null)
		{
			if (other.DayNumb != 0)
			{
				DayNumb = other.DayNumb;
			}
			reward_.MergeFrom(other.reward_);
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
				DayNumb = input.ReadSFixed32();
				break;
			case 18u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			}
		}
	}
}
