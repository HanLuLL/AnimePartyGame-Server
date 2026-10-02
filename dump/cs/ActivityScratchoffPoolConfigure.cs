using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ActivityScratchoffPoolConfigure : IMessage<ActivityScratchoffPoolConfigure>, IMessage, IEquatable<ActivityScratchoffPoolConfigure>, IDeepCloneable<ActivityScratchoffPoolConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityScratchoffPoolConfigure> _parser = new MessageParser<ActivityScratchoffPoolConfigure>(() => new ActivityScratchoffPoolConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ActivityScratchoffPoolConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<ActivityScratchoffPoolConfigureItem> _repeated_activityScratchoffPoolConfigureItems_codec = FieldCodec.ForMessage(18u, ActivityScratchoffPoolConfigureItem.Parser);

	private readonly RepeatedField<ActivityScratchoffPoolConfigureItem> activityScratchoffPoolConfigureItems_ = new RepeatedField<ActivityScratchoffPoolConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityScratchoffPoolConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[5];

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
	public RepeatedField<ActivityScratchoffPoolConfigureItem> ActivityScratchoffPoolConfigureItems => activityScratchoffPoolConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffPoolConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffPoolConfigure(ActivityScratchoffPoolConfigure other)
		: this()
	{
		id_ = other.id_;
		activityScratchoffPoolConfigureItems_ = other.activityScratchoffPoolConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityScratchoffPoolConfigure Clone()
	{
		return new ActivityScratchoffPoolConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityScratchoffPoolConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityScratchoffPoolConfigure other)
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
		if (!activityScratchoffPoolConfigureItems_.Equals(other.activityScratchoffPoolConfigureItems_))
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
		num ^= activityScratchoffPoolConfigureItems_.GetHashCode();
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
		activityScratchoffPoolConfigureItems_.WriteTo(ref output, _repeated_activityScratchoffPoolConfigureItems_codec);
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
		num += activityScratchoffPoolConfigureItems_.CalculateSize(_repeated_activityScratchoffPoolConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityScratchoffPoolConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			activityScratchoffPoolConfigureItems_.Add(other.activityScratchoffPoolConfigureItems_);
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
				activityScratchoffPoolConfigureItems_.AddEntriesFrom(ref input, _repeated_activityScratchoffPoolConfigureItems_codec);
				break;
			}
		}
	}
}
