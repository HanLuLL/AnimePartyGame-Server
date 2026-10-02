using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class MatchParmsConfigure : IMessage<MatchParmsConfigure>, IMessage, IEquatable<MatchParmsConfigure>, IDeepCloneable<MatchParmsConfigure>, IBufferMessage
{
	private static readonly MessageParser<MatchParmsConfigure> _parser = new MessageParser<MatchParmsConfigure>(() => new MatchParmsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BanTimeFieldNumber = 2;

	private int banTime_;

	public const int ReadyTimeFieldNumber = 3;

	private int readyTime_;

	public const int AfkLimitFieldNumber = 4;

	private int afkLimit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchParmsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MatchReflection.Descriptor.MessageTypes[1];

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
	public int BanTime
	{
		get
		{
			return banTime_;
		}
		private set
		{
			banTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ReadyTime
	{
		get
		{
			return readyTime_;
		}
		private set
		{
			readyTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AfkLimit
	{
		get
		{
			return afkLimit_;
		}
		private set
		{
			afkLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchParmsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchParmsConfigure(MatchParmsConfigure other)
		: this()
	{
		id_ = other.id_;
		banTime_ = other.banTime_;
		readyTime_ = other.readyTime_;
		afkLimit_ = other.afkLimit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchParmsConfigure Clone()
	{
		return new MatchParmsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchParmsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchParmsConfigure other)
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
		if (BanTime != other.BanTime)
		{
			return false;
		}
		if (ReadyTime != other.ReadyTime)
		{
			return false;
		}
		if (AfkLimit != other.AfkLimit)
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
		if (BanTime != 0)
		{
			num ^= BanTime.GetHashCode();
		}
		if (ReadyTime != 0)
		{
			num ^= ReadyTime.GetHashCode();
		}
		if (AfkLimit != 0)
		{
			num ^= AfkLimit.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (BanTime != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(BanTime);
		}
		if (ReadyTime != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ReadyTime);
		}
		if (AfkLimit != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(AfkLimit);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (BanTime != 0)
		{
			num += 5;
		}
		if (ReadyTime != 0)
		{
			num += 5;
		}
		if (AfkLimit != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MatchParmsConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.BanTime != 0)
			{
				BanTime = other.BanTime;
			}
			if (other.ReadyTime != 0)
			{
				ReadyTime = other.ReadyTime;
			}
			if (other.AfkLimit != 0)
			{
				AfkLimit = other.AfkLimit;
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 21u:
				BanTime = input.ReadSFixed32();
				break;
			case 29u:
				ReadyTime = input.ReadSFixed32();
				break;
			case 37u:
				AfkLimit = input.ReadSFixed32();
				break;
			}
		}
	}
}
