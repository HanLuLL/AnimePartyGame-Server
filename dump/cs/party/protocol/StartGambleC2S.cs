using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class StartGambleC2S : IMessage<StartGambleC2S>, IMessage, IEquatable<StartGambleC2S>, IDeepCloneable<StartGambleC2S>, IBufferMessage
{
	private static readonly MessageParser<StartGambleC2S> _parser = new MessageParser<StartGambleC2S>(() => new StartGambleC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int HallFieldNumber = 2;

	private Gamble hall_;

	public const int IsExecFieldNumber = 3;

	private bool isExec_;

	public const int GuessCodeFieldNumber = 4;

	private int guessCode_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<StartGambleC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[341];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Gamble Hall
	{
		get
		{
			return hall_;
		}
		set
		{
			hall_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsExec
	{
		get
		{
			return isExec_;
		}
		set
		{
			isExec_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GuessCode
	{
		get
		{
			return guessCode_;
		}
		set
		{
			guessCode_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGambleC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGambleC2S(StartGambleC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		hall_ = ((other.hall_ != null) ? other.hall_.Clone() : null);
		isExec_ = other.isExec_;
		guessCode_ = other.guessCode_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGambleC2S Clone()
	{
		return new StartGambleC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as StartGambleC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(StartGambleC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (!object.Equals(Hall, other.Hall))
		{
			return false;
		}
		if (IsExec != other.IsExec)
		{
			return false;
		}
		if (GuessCode != other.GuessCode)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (hall_ != null)
		{
			num ^= Hall.GetHashCode();
		}
		if (IsExec)
		{
			num ^= IsExec.GetHashCode();
		}
		if (GuessCode != 0)
		{
			num ^= GuessCode.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (hall_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Hall);
		}
		if (IsExec)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsExec);
		}
		if (GuessCode != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(GuessCode);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (hall_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Hall);
		}
		if (IsExec)
		{
			num += 2;
		}
		if (GuessCode != 0)
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
	public void MergeFrom(StartGambleC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.hall_ != null)
		{
			if (hall_ == null)
			{
				Hall = new Gamble();
			}
			Hall.MergeFrom(other.Hall);
		}
		if (other.IsExec)
		{
			IsExec = other.IsExec;
		}
		if (other.GuessCode != 0)
		{
			GuessCode = other.GuessCode;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 18u:
				if (hall_ == null)
				{
					Hall = new Gamble();
				}
				input.ReadMessage(Hall);
				break;
			case 24u:
				IsExec = input.ReadBool();
				break;
			case 37u:
				GuessCode = input.ReadSFixed32();
				break;
			}
		}
	}
}
