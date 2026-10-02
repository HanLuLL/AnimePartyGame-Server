using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class LuckyStarMissionChangeS2C : IMessage<LuckyStarMissionChangeS2C>, IMessage, IEquatable<LuckyStarMissionChangeS2C>, IDeepCloneable<LuckyStarMissionChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<LuckyStarMissionChangeS2C> _parser = new MessageParser<LuckyStarMissionChangeS2C>(() => new LuckyStarMissionChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int LuckyStarMissionFieldNumber = 1;

	private LuckyStarMission luckyStarMission_;

	public const int PlayerIdFieldNumber = 2;

	private long playerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LuckyStarMissionChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[462];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMission LuckyStarMission
	{
		get
		{
			return luckyStarMission_;
		}
		set
		{
			luckyStarMission_ = value;
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
	public LuckyStarMissionChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMissionChangeS2C(LuckyStarMissionChangeS2C other)
		: this()
	{
		luckyStarMission_ = ((other.luckyStarMission_ != null) ? other.luckyStarMission_.Clone() : null);
		playerId_ = other.playerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMissionChangeS2C Clone()
	{
		return new LuckyStarMissionChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LuckyStarMissionChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LuckyStarMissionChangeS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(LuckyStarMission, other.LuckyStarMission))
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
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
		if (luckyStarMission_ != null)
		{
			num ^= LuckyStarMission.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
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
		if (luckyStarMission_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(LuckyStarMission);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PlayerId);
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
		if (luckyStarMission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LuckyStarMission);
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LuckyStarMissionChangeS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.luckyStarMission_ != null)
		{
			if (luckyStarMission_ == null)
			{
				LuckyStarMission = new LuckyStarMission();
			}
			LuckyStarMission.MergeFrom(other.LuckyStarMission);
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
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
				if (luckyStarMission_ == null)
				{
					LuckyStarMission = new LuckyStarMission();
				}
				input.ReadMessage(LuckyStarMission);
				break;
			case 17u:
				PlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
