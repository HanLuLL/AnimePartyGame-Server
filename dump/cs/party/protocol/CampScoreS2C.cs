using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class CampScoreS2C : IMessage<CampScoreS2C>, IMessage, IEquatable<CampScoreS2C>, IDeepCloneable<CampScoreS2C>, IBufferMessage
{
	private static readonly MessageParser<CampScoreS2C> _parser = new MessageParser<CampScoreS2C>(() => new CampScoreS2C());

	private UnknownFieldSet _unknownFields;

	public const int RefreshTimeFieldNumber = 1;

	private int refreshTime_;

	public const int CampIdFieldNumber = 2;

	private int campId_;

	public const int CampScorePercentFieldNumber = 3;

	private int campScorePercent_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CampScoreS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[543];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RefreshTime
	{
		get
		{
			return refreshTime_;
		}
		set
		{
			refreshTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CampId
	{
		get
		{
			return campId_;
		}
		set
		{
			campId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CampScorePercent
	{
		get
		{
			return campScorePercent_;
		}
		set
		{
			campScorePercent_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampScoreS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampScoreS2C(CampScoreS2C other)
		: this()
	{
		refreshTime_ = other.refreshTime_;
		campId_ = other.campId_;
		campScorePercent_ = other.campScorePercent_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampScoreS2C Clone()
	{
		return new CampScoreS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CampScoreS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CampScoreS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RefreshTime != other.RefreshTime)
		{
			return false;
		}
		if (CampId != other.CampId)
		{
			return false;
		}
		if (CampScorePercent != other.CampScorePercent)
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
		if (RefreshTime != 0)
		{
			num ^= RefreshTime.GetHashCode();
		}
		if (CampId != 0)
		{
			num ^= CampId.GetHashCode();
		}
		if (CampScorePercent != 0)
		{
			num ^= CampScorePercent.GetHashCode();
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
		if (RefreshTime != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(RefreshTime);
		}
		if (CampId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CampId);
		}
		if (CampScorePercent != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CampScorePercent);
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
		if (RefreshTime != 0)
		{
			num += 5;
		}
		if (CampId != 0)
		{
			num += 5;
		}
		if (CampScorePercent != 0)
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
	public void MergeFrom(CampScoreS2C other)
	{
		if (other != null)
		{
			if (other.RefreshTime != 0)
			{
				RefreshTime = other.RefreshTime;
			}
			if (other.CampId != 0)
			{
				CampId = other.CampId;
			}
			if (other.CampScorePercent != 0)
			{
				CampScorePercent = other.CampScorePercent;
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
				RefreshTime = input.ReadSFixed32();
				break;
			case 21u:
				CampId = input.ReadSFixed32();
				break;
			case 29u:
				CampScorePercent = input.ReadSFixed32();
				break;
			}
		}
	}
}
