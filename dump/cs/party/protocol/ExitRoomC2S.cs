using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ExitRoomC2S : IMessage<ExitRoomC2S>, IMessage, IEquatable<ExitRoomC2S>, IDeepCloneable<ExitRoomC2S>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum ForceExitType
		{
			[OriginalName("none")]
			None,
			[OriginalName("timeOut")]
			TimeOut,
			[OriginalName("quickClick")]
			QuickClick,
			[OriginalName("voluntaryExit")]
			VoluntaryExit
		}
	}

	private static readonly MessageParser<ExitRoomC2S> _parser = new MessageParser<ExitRoomC2S>(() => new ExitRoomC2S());

	private UnknownFieldSet _unknownFields;

	public const int IsOfflineFieldNumber = 1;

	private bool isOffline_;

	public const int IsWaitOfflineFieldNumber = 2;

	private bool isWaitOffline_;

	public const int ExitTypeFieldNumber = 3;

	private Types.ForceExitType exitType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ExitRoomC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[151];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsOffline
	{
		get
		{
			return isOffline_;
		}
		set
		{
			isOffline_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsWaitOffline
	{
		get
		{
			return isWaitOffline_;
		}
		set
		{
			isWaitOffline_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.ForceExitType ExitType
	{
		get
		{
			return exitType_;
		}
		set
		{
			exitType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomC2S(ExitRoomC2S other)
		: this()
	{
		isOffline_ = other.isOffline_;
		isWaitOffline_ = other.isWaitOffline_;
		exitType_ = other.exitType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomC2S Clone()
	{
		return new ExitRoomC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ExitRoomC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ExitRoomC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsOffline != other.IsOffline)
		{
			return false;
		}
		if (IsWaitOffline != other.IsWaitOffline)
		{
			return false;
		}
		if (ExitType != other.ExitType)
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
		if (IsOffline)
		{
			num ^= IsOffline.GetHashCode();
		}
		if (IsWaitOffline)
		{
			num ^= IsWaitOffline.GetHashCode();
		}
		if (ExitType != Types.ForceExitType.None)
		{
			num ^= ExitType.GetHashCode();
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
		if (IsOffline)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsOffline);
		}
		if (IsWaitOffline)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsWaitOffline);
		}
		if (ExitType != Types.ForceExitType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)ExitType);
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
		if (IsOffline)
		{
			num += 2;
		}
		if (IsWaitOffline)
		{
			num += 2;
		}
		if (ExitType != Types.ForceExitType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ExitType);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ExitRoomC2S other)
	{
		if (other != null)
		{
			if (other.IsOffline)
			{
				IsOffline = other.IsOffline;
			}
			if (other.IsWaitOffline)
			{
				IsWaitOffline = other.IsWaitOffline;
			}
			if (other.ExitType != Types.ForceExitType.None)
			{
				ExitType = other.ExitType;
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
				IsOffline = input.ReadBool();
				break;
			case 16u:
				IsWaitOffline = input.ReadBool();
				break;
			case 24u:
				ExitType = (Types.ForceExitType)input.ReadEnum();
				break;
			}
		}
	}
}
