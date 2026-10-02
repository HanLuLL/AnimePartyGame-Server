using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class UpdateGuildInAnnouncementC2S : IMessage<UpdateGuildInAnnouncementC2S>, IMessage, IEquatable<UpdateGuildInAnnouncementC2S>, IDeepCloneable<UpdateGuildInAnnouncementC2S>, IBufferMessage
{
	private static readonly MessageParser<UpdateGuildInAnnouncementC2S> _parser = new MessageParser<UpdateGuildInAnnouncementC2S>(() => new UpdateGuildInAnnouncementC2S());

	private UnknownFieldSet _unknownFields;

	public const int InAnnouncementFieldNumber = 1;

	private string inAnnouncement_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UpdateGuildInAnnouncementC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[577];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string InAnnouncement
	{
		get
		{
			return inAnnouncement_;
		}
		set
		{
			inAnnouncement_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildInAnnouncementC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildInAnnouncementC2S(UpdateGuildInAnnouncementC2S other)
		: this()
	{
		inAnnouncement_ = other.inAnnouncement_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildInAnnouncementC2S Clone()
	{
		return new UpdateGuildInAnnouncementC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UpdateGuildInAnnouncementC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UpdateGuildInAnnouncementC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (InAnnouncement != other.InAnnouncement)
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
		if (InAnnouncement.Length != 0)
		{
			num ^= InAnnouncement.GetHashCode();
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
		if (InAnnouncement.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(InAnnouncement);
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
		if (InAnnouncement.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InAnnouncement);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UpdateGuildInAnnouncementC2S other)
	{
		if (other != null)
		{
			if (other.InAnnouncement.Length != 0)
			{
				InAnnouncement = other.InAnnouncement;
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				InAnnouncement = input.ReadString();
			}
		}
	}
}
