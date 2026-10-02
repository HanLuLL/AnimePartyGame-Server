using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ThrowDiceS2C : IMessage<ThrowDiceS2C>, IMessage, IEquatable<ThrowDiceS2C>, IDeepCloneable<ThrowDiceS2C>, IBufferMessage
{
	private static readonly MessageParser<ThrowDiceS2C> _parser = new MessageParser<ThrowDiceS2C>(() => new ThrowDiceS2C());

	private UnknownFieldSet _unknownFields;

	public const int ValsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_vals_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> vals_ = new RepeatedField<int>();

	public const int MovePointFieldNumber = 2;

	private int movePoint_;

	public const int PlayerIdFieldNumber = 3;

	private long playerId_;

	public const int ForceDirFieldNumber = 4;

	private bool forceDir_;

	public const int IsControlMovePointFieldNumber = 5;

	private bool isControlMovePoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ThrowDiceS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[244];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Vals => vals_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MovePoint
	{
		get
		{
			return movePoint_;
		}
		set
		{
			movePoint_ = value;
		}
	}

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
	public bool ForceDir
	{
		get
		{
			return forceDir_;
		}
		set
		{
			forceDir_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsControlMovePoint
	{
		get
		{
			return isControlMovePoint_;
		}
		set
		{
			isControlMovePoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceS2C(ThrowDiceS2C other)
		: this()
	{
		vals_ = other.vals_.Clone();
		movePoint_ = other.movePoint_;
		playerId_ = other.playerId_;
		forceDir_ = other.forceDir_;
		isControlMovePoint_ = other.isControlMovePoint_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceS2C Clone()
	{
		return new ThrowDiceS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ThrowDiceS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ThrowDiceS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!vals_.Equals(other.vals_))
		{
			return false;
		}
		if (MovePoint != other.MovePoint)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (ForceDir != other.ForceDir)
		{
			return false;
		}
		if (IsControlMovePoint != other.IsControlMovePoint)
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
		num ^= vals_.GetHashCode();
		if (MovePoint != 0)
		{
			num ^= MovePoint.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (ForceDir)
		{
			num ^= ForceDir.GetHashCode();
		}
		if (IsControlMovePoint)
		{
			num ^= IsControlMovePoint.GetHashCode();
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
		vals_.WriteTo(ref output, _repeated_vals_codec);
		if (MovePoint != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MovePoint);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(PlayerId);
		}
		if (ForceDir)
		{
			output.WriteRawTag(32);
			output.WriteBool(ForceDir);
		}
		if (IsControlMovePoint)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsControlMovePoint);
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
		num += vals_.CalculateSize(_repeated_vals_codec);
		if (MovePoint != 0)
		{
			num += 5;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (ForceDir)
		{
			num += 2;
		}
		if (IsControlMovePoint)
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
	public void MergeFrom(ThrowDiceS2C other)
	{
		if (other != null)
		{
			vals_.Add(other.vals_);
			if (other.MovePoint != 0)
			{
				MovePoint = other.MovePoint;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.ForceDir)
			{
				ForceDir = other.ForceDir;
			}
			if (other.IsControlMovePoint)
			{
				IsControlMovePoint = other.IsControlMovePoint;
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
				vals_.AddEntriesFrom(ref input, _repeated_vals_codec);
				break;
			case 21u:
				MovePoint = input.ReadSFixed32();
				break;
			case 25u:
				PlayerId = input.ReadSFixed64();
				break;
			case 32u:
				ForceDir = input.ReadBool();
				break;
			case 40u:
				IsControlMovePoint = input.ReadBool();
				break;
			}
		}
	}
}
