using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SteamInfo : IMessage<SteamInfo>, IMessage, IEquatable<SteamInfo>, IDeepCloneable<SteamInfo>, IBufferMessage
{
	private static readonly MessageParser<SteamInfo> _parser = new MessageParser<SteamInfo>(() => new SteamInfo());

	private UnknownFieldSet _unknownFields;

	public const int TicketFieldNumber = 1;

	private string ticket_ = "";

	public const int IdentityFieldNumber = 2;

	private string identity_ = "";

	public const int IdFieldNumber = 3;

	private ulong id_;

	public const int NickFieldNumber = 4;

	private string nick_ = "";

	public const int MacFieldNumber = 5;

	private string mac_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SteamInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Ticket
	{
		get
		{
			return ticket_;
		}
		set
		{
			ticket_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Identity
	{
		get
		{
			return identity_;
		}
		set
		{
			identity_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ulong Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Nick
	{
		get
		{
			return nick_;
		}
		set
		{
			nick_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Mac
	{
		get
		{
			return mac_;
		}
		set
		{
			mac_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamInfo(SteamInfo other)
		: this()
	{
		ticket_ = other.ticket_;
		identity_ = other.identity_;
		id_ = other.id_;
		nick_ = other.nick_;
		mac_ = other.mac_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamInfo Clone()
	{
		return new SteamInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SteamInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SteamInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Ticket != other.Ticket)
		{
			return false;
		}
		if (Identity != other.Identity)
		{
			return false;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (Nick != other.Nick)
		{
			return false;
		}
		if (Mac != other.Mac)
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
		if (Ticket.Length != 0)
		{
			num ^= Ticket.GetHashCode();
		}
		if (Identity.Length != 0)
		{
			num ^= Identity.GetHashCode();
		}
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
		}
		if (Nick.Length != 0)
		{
			num ^= Nick.GetHashCode();
		}
		if (Mac.Length != 0)
		{
			num ^= Mac.GetHashCode();
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
		if (Ticket.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Ticket);
		}
		if (Identity.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Identity);
		}
		if (Id != 0L)
		{
			output.WriteRawTag(25);
			output.WriteFixed64(Id);
		}
		if (Nick.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Nick);
		}
		if (Mac.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Mac);
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
		if (Ticket.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Ticket);
		}
		if (Identity.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Identity);
		}
		if (Id != 0L)
		{
			num += 9;
		}
		if (Nick.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Nick);
		}
		if (Mac.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Mac);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SteamInfo other)
	{
		if (other != null)
		{
			if (other.Ticket.Length != 0)
			{
				Ticket = other.Ticket;
			}
			if (other.Identity.Length != 0)
			{
				Identity = other.Identity;
			}
			if (other.Id != 0L)
			{
				Id = other.Id;
			}
			if (other.Nick.Length != 0)
			{
				Nick = other.Nick;
			}
			if (other.Mac.Length != 0)
			{
				Mac = other.Mac;
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
				Ticket = input.ReadString();
				break;
			case 18u:
				Identity = input.ReadString();
				break;
			case 25u:
				Id = input.ReadFixed64();
				break;
			case 34u:
				Nick = input.ReadString();
				break;
			case 42u:
				Mac = input.ReadString();
				break;
			}
		}
	}
}
