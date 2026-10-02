using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class UpdateGuildSettingsC2S : IMessage<UpdateGuildSettingsC2S>, IMessage, IEquatable<UpdateGuildSettingsC2S>, IDeepCloneable<UpdateGuildSettingsC2S>, IBufferMessage
{
	private static readonly MessageParser<UpdateGuildSettingsC2S> _parser = new MessageParser<UpdateGuildSettingsC2S>(() => new UpdateGuildSettingsC2S());

	private UnknownFieldSet _unknownFields;

	public const int TagIdsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_tagIds_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> tagIds_ = new RepeatedField<int>();

	public const int ExAnnouncementFieldNumber = 2;

	private string exAnnouncement_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UpdateGuildSettingsC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[575];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public UpdateGuildSettingsC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildSettingsC2S(UpdateGuildSettingsC2S other)
		: this()
	{
		tagIds_ = other.tagIds_.Clone();
		exAnnouncement_ = other.exAnnouncement_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildSettingsC2S Clone()
	{
		return new UpdateGuildSettingsC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UpdateGuildSettingsC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UpdateGuildSettingsC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
		tagIds_.WriteTo(ref output, _repeated_tagIds_codec);
		if (ExAnnouncement.Length != 0)
		{
			output.WriteRawTag(18);
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
	public void MergeFrom(UpdateGuildSettingsC2S other)
	{
		if (other != null)
		{
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
			case 13u:
				tagIds_.AddEntriesFrom(ref input, _repeated_tagIds_codec);
				break;
			case 18u:
				ExAnnouncement = input.ReadString();
				break;
			}
		}
	}
}
