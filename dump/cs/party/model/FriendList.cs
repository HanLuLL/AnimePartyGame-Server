using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class FriendList : IMessage<FriendList>, IMessage, IEquatable<FriendList>, IDeepCloneable<FriendList>, IBufferMessage
{
	private static readonly MessageParser<FriendList> _parser = new MessageParser<FriendList>(() => new FriendList());

	private UnknownFieldSet _unknownFields;

	public const int FriendIdsFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_friendIds_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> friendIds_ = new RepeatedField<long>();

	public const int BlacksFieldNumber = 2;

	private static readonly FieldCodec<long> _repeated_blacks_codec = FieldCodec.ForSFixed64(18u);

	private readonly RepeatedField<long> blacks_ = new RepeatedField<long>();

	public const int ApplyFieldNumber = 3;

	private static readonly FieldCodec<FriendApply> _repeated_apply_codec = FieldCodec.ForMessage(26u, FriendApply.Parser);

	private readonly RepeatedField<FriendApply> apply_ = new RepeatedField<FriendApply>();

	public const int SelfApplyFieldNumber = 4;

	private static readonly FieldCodec<FriendSelfApply> _repeated_selfApply_codec = FieldCodec.ForMessage(34u, FriendSelfApply.Parser);

	private readonly RepeatedField<FriendSelfApply> selfApply_ = new RepeatedField<FriendSelfApply>();

	public const int DayApplyCountFieldNumber = 5;

	private int dayApplyCount_;

	public const int FriendNotesFieldNumber = 6;

	private static readonly MapField<long, string>.Codec _map_friendNotes_codec = new MapField<long, string>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForString(18u, ""), 50u);

	private readonly MapField<long, string> friendNotes_ = new MapField<long, string>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendList> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[18];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> FriendIds => friendIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> Blacks => blacks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendApply> Apply => apply_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendSelfApply> SelfApply => selfApply_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DayApplyCount
	{
		get
		{
			return dayApplyCount_;
		}
		set
		{
			dayApplyCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, string> FriendNotes => friendNotes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendList()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendList(FriendList other)
		: this()
	{
		friendIds_ = other.friendIds_.Clone();
		blacks_ = other.blacks_.Clone();
		apply_ = other.apply_.Clone();
		selfApply_ = other.selfApply_.Clone();
		dayApplyCount_ = other.dayApplyCount_;
		friendNotes_ = other.friendNotes_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendList Clone()
	{
		return new FriendList(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendList);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FriendList other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!friendIds_.Equals(other.friendIds_))
		{
			return false;
		}
		if (!blacks_.Equals(other.blacks_))
		{
			return false;
		}
		if (!apply_.Equals(other.apply_))
		{
			return false;
		}
		if (!selfApply_.Equals(other.selfApply_))
		{
			return false;
		}
		if (DayApplyCount != other.DayApplyCount)
		{
			return false;
		}
		if (!FriendNotes.Equals(other.FriendNotes))
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
		num ^= friendIds_.GetHashCode();
		num ^= blacks_.GetHashCode();
		num ^= apply_.GetHashCode();
		num ^= selfApply_.GetHashCode();
		if (DayApplyCount != 0)
		{
			num ^= DayApplyCount.GetHashCode();
		}
		num ^= FriendNotes.GetHashCode();
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
		friendIds_.WriteTo(ref output, _repeated_friendIds_codec);
		blacks_.WriteTo(ref output, _repeated_blacks_codec);
		apply_.WriteTo(ref output, _repeated_apply_codec);
		selfApply_.WriteTo(ref output, _repeated_selfApply_codec);
		if (DayApplyCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DayApplyCount);
		}
		friendNotes_.WriteTo(ref output, _map_friendNotes_codec);
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
		num += friendIds_.CalculateSize(_repeated_friendIds_codec);
		num += blacks_.CalculateSize(_repeated_blacks_codec);
		num += apply_.CalculateSize(_repeated_apply_codec);
		num += selfApply_.CalculateSize(_repeated_selfApply_codec);
		if (DayApplyCount != 0)
		{
			num += 5;
		}
		num += friendNotes_.CalculateSize(_map_friendNotes_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendList other)
	{
		if (other != null)
		{
			friendIds_.Add(other.friendIds_);
			blacks_.Add(other.blacks_);
			apply_.Add(other.apply_);
			selfApply_.Add(other.selfApply_);
			if (other.DayApplyCount != 0)
			{
				DayApplyCount = other.DayApplyCount;
			}
			friendNotes_.MergeFrom(other.friendNotes_);
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
			case 10u:
				friendIds_.AddEntriesFrom(ref input, _repeated_friendIds_codec);
				break;
			case 17u:
			case 18u:
				blacks_.AddEntriesFrom(ref input, _repeated_blacks_codec);
				break;
			case 26u:
				apply_.AddEntriesFrom(ref input, _repeated_apply_codec);
				break;
			case 34u:
				selfApply_.AddEntriesFrom(ref input, _repeated_selfApply_codec);
				break;
			case 45u:
				DayApplyCount = input.ReadSFixed32();
				break;
			case 50u:
				friendNotes_.AddEntriesFrom(ref input, _map_friendNotes_codec);
				break;
			}
		}
	}
}
