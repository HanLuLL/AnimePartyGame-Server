using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ActivityBingoFlipPoolConfigure : IMessage<ActivityBingoFlipPoolConfigure>, IMessage, IEquatable<ActivityBingoFlipPoolConfigure>, IDeepCloneable<ActivityBingoFlipPoolConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityBingoFlipPoolConfigure> _parser = new MessageParser<ActivityBingoFlipPoolConfigure>(() => new ActivityBingoFlipPoolConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ActivityBingoFlipPoolConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<ActivityBingoFlipPoolConfigureItem> _repeated_activityBingoFlipPoolConfigureItems_codec = FieldCodec.ForMessage(18u, ActivityBingoFlipPoolConfigureItem.Parser);

	private readonly RepeatedField<ActivityBingoFlipPoolConfigureItem> activityBingoFlipPoolConfigureItems_ = new RepeatedField<ActivityBingoFlipPoolConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityBingoFlipPoolConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[9];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityBingoFlipPoolConfigureItem> ActivityBingoFlipPoolConfigureItems => activityBingoFlipPoolConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityBingoFlipPoolConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityBingoFlipPoolConfigure(ActivityBingoFlipPoolConfigure other)
		: this()
	{
		id_ = other.id_;
		activityBingoFlipPoolConfigureItems_ = other.activityBingoFlipPoolConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityBingoFlipPoolConfigure Clone()
	{
		return new ActivityBingoFlipPoolConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityBingoFlipPoolConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityBingoFlipPoolConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (!activityBingoFlipPoolConfigureItems_.Equals(other.activityBingoFlipPoolConfigureItems_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		num ^= activityBingoFlipPoolConfigureItems_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		activityBingoFlipPoolConfigureItems_.WriteTo(ref output, _repeated_activityBingoFlipPoolConfigureItems_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		num += activityBingoFlipPoolConfigureItems_.CalculateSize(_repeated_activityBingoFlipPoolConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityBingoFlipPoolConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			activityBingoFlipPoolConfigureItems_.Add(other.activityBingoFlipPoolConfigureItems_);
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
				Id = input.ReadSFixed32();
				break;
			case 18u:
				activityBingoFlipPoolConfigureItems_.AddEntriesFrom(ref input, _repeated_activityBingoFlipPoolConfigureItems_codec);
				break;
			}
		}
	}
}
