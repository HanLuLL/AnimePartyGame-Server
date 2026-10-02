using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class NearFightPlayerC2S : IMessage<NearFightPlayerC2S>, IMessage, IEquatable<NearFightPlayerC2S>, IDeepCloneable<NearFightPlayerC2S>, IBufferMessage
{
	private static readonly MessageParser<NearFightPlayerC2S> _parser = new MessageParser<NearFightPlayerC2S>(() => new NearFightPlayerC2S());

	private UnknownFieldSet _unknownFields;

	public const int PageFieldNumber = 1;

	private int page_;

	public const int OnlyOnlineFieldNumber = 2;

	private bool onlyOnline_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<NearFightPlayerC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[100];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Page
	{
		get
		{
			return page_;
		}
		set
		{
			page_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool OnlyOnline
	{
		get
		{
			return onlyOnline_;
		}
		set
		{
			onlyOnline_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerC2S(NearFightPlayerC2S other)
		: this()
	{
		page_ = other.page_;
		onlyOnline_ = other.onlyOnline_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerC2S Clone()
	{
		return new NearFightPlayerC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NearFightPlayerC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(NearFightPlayerC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Page != other.Page)
		{
			return false;
		}
		if (OnlyOnline != other.OnlyOnline)
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
		if (Page != 0)
		{
			num ^= Page.GetHashCode();
		}
		if (OnlyOnline)
		{
			num ^= OnlyOnline.GetHashCode();
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
		if (Page != 0)
		{
			output.WriteRawTag(8);
			output.WriteInt32(Page);
		}
		if (OnlyOnline)
		{
			output.WriteRawTag(16);
			output.WriteBool(OnlyOnline);
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
		if (Page != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Page);
		}
		if (OnlyOnline)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(NearFightPlayerC2S other)
	{
		if (other != null)
		{
			if (other.Page != 0)
			{
				Page = other.Page;
			}
			if (other.OnlyOnline)
			{
				OnlyOnline = other.OnlyOnline;
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
			case 8u:
				Page = input.ReadInt32();
				break;
			case 16u:
				OnlyOnline = input.ReadBool();
				break;
			}
		}
	}
}
