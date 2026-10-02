using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ActionStartNotifyS2C : IMessage<ActionStartNotifyS2C>, IMessage, IEquatable<ActionStartNotifyS2C>, IDeepCloneable<ActionStartNotifyS2C>, IBufferMessage
{
	private static readonly MessageParser<ActionStartNotifyS2C> _parser = new MessageParser<ActionStartNotifyS2C>(() => new ActionStartNotifyS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int IsDieFieldNumber = 2;

	private bool isDie_;

	public const int IsHospitalFieldNumber = 3;

	private bool isHospital_;

	public const int IsStopRoundFieldNumber = 4;

	private bool isStopRound_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActionStartNotifyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[400];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDie
	{
		get
		{
			return isDie_;
		}
		set
		{
			isDie_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsHospital
	{
		get
		{
			return isHospital_;
		}
		set
		{
			isHospital_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsStopRound
	{
		get
		{
			return isStopRound_;
		}
		set
		{
			isStopRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionStartNotifyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionStartNotifyS2C(ActionStartNotifyS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		isDie_ = other.isDie_;
		isHospital_ = other.isHospital_;
		isStopRound_ = other.isStopRound_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionStartNotifyS2C Clone()
	{
		return new ActionStartNotifyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActionStartNotifyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActionStartNotifyS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (IsDie != other.IsDie)
		{
			return false;
		}
		if (IsHospital != other.IsHospital)
		{
			return false;
		}
		if (IsStopRound != other.IsStopRound)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (IsDie)
		{
			num ^= IsDie.GetHashCode();
		}
		if (IsHospital)
		{
			num ^= IsHospital.GetHashCode();
		}
		if (IsStopRound)
		{
			num ^= IsStopRound.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (IsDie)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsDie);
		}
		if (IsHospital)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsHospital);
		}
		if (IsStopRound)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsStopRound);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (IsDie)
		{
			num += 2;
		}
		if (IsHospital)
		{
			num += 2;
		}
		if (IsStopRound)
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
	public void MergeFrom(ActionStartNotifyS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.IsDie)
			{
				IsDie = other.IsDie;
			}
			if (other.IsHospital)
			{
				IsHospital = other.IsHospital;
			}
			if (other.IsStopRound)
			{
				IsStopRound = other.IsStopRound;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 16u:
				IsDie = input.ReadBool();
				break;
			case 24u:
				IsHospital = input.ReadBool();
				break;
			case 32u:
				IsStopRound = input.ReadBool();
				break;
			}
		}
	}
}
