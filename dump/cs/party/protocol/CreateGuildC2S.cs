using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class CreateGuildC2S : IMessage<CreateGuildC2S>, IMessage, IEquatable<CreateGuildC2S>, IDeepCloneable<CreateGuildC2S>, IBufferMessage
{
	private static readonly MessageParser<CreateGuildC2S> _parser = new MessageParser<CreateGuildC2S>(() => new CreateGuildC2S());

	private UnknownFieldSet _unknownFields;

	public const int NameFieldNumber = 1;

	private string name_ = "";

	public const int TagIdsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_tagIds_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> tagIds_ = new RepeatedField<int>();

	public const int ExAnnouncementFieldNumber = 3;

	private string exAnnouncement_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CreateGuildC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[561];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TagIds => tagIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ExAnnouncement
	{
		get
		{
			return exAnnouncement_;
		}
		set
		{
			exAnnouncement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildC2S(CreateGuildC2S other)
		: this()
	{
		name_ = other.name_;
		tagIds_ = other.tagIds_.Clone();
		exAnnouncement_ = other.exAnnouncement_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildC2S Clone()
	{
		return new CreateGuildC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CreateGuildC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CreateGuildC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (!tagIds_.Equals(other.tagIds_))
		{
			return false;
		}
		if (ExAnnouncement != other.ExAnnouncement)
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		num ^= tagIds_.GetHashCode();
		if (ExAnnouncement.Length != 0)
		{
			num ^= ExAnnouncement.GetHashCode();
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
		if (Name.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Name);
		}
		tagIds_.WriteTo(ref output, _repeated_tagIds_codec);
		if (ExAnnouncement.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(ExAnnouncement);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		num += tagIds_.CalculateSize(_repeated_tagIds_codec);
		if (ExAnnouncement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ExAnnouncement);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CreateGuildC2S other)
	{
		if (other != null)
		{
			if (other.Name.Length != 0)
			{
				Name = other.Name;
			}
			tagIds_.Add(other.tagIds_);
			if (other.ExAnnouncement.Length != 0)
			{
				ExAnnouncement = other.ExAnnouncement;
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
				Name = input.ReadString();
				break;
			case 18u:
			case 21u:
				tagIds_.AddEntriesFrom(ref input, _repeated_tagIds_codec);
				break;
			case 26u:
				ExAnnouncement = input.ReadString();
				break;
			}
		}
	}
}
