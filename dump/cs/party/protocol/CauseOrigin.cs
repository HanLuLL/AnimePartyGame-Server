using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class CauseOrigin : IMessage<CauseOrigin>, IMessage, IEquatable<CauseOrigin>, IDeepCloneable<CauseOrigin>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum source
		{
			[OriginalName("unknown")]
			Unknown = 0,
			[OriginalName("skill")]
			Skill = 1,
			[OriginalName("card")]
			Card = 2,
			[OriginalName("event")]
			Event = 3,
			[OriginalName("destiny")]
			Destiny = 5,
			[OriginalName("divination")]
			Divination = 6,
			[OriginalName("heroBuff")]
			HeroBuff = 7,
			[OriginalName("landBuff")]
			LandBuff = 8,
			[OriginalName("land")]
			Land = 9,
			[OriginalName("bomb_die")]
			BombDie = 10,
			[OriginalName("round_award")]
			RoundAward = 11,
			[OriginalName("battle")]
			Battle = 12,
			[OriginalName("shop_open")]
			ShopOpen = 13,
			[OriginalName("shop_buy")]
			ShopBuy = 14,
			[OriginalName("map_event")]
			MapEvent = 15,
			[OriginalName("round_end")]
			RoundEnd = 16,
			[OriginalName("select_relic")]
			SelectRelic = 17,
			[OriginalName("mission")]
			Mission = 18,
			[OriginalName("bounty_kill")]
			BountyKill = 19,
			[OriginalName("bounty_defend")]
			BountyDefend = 20,
			[OriginalName("pve_boss_combine")]
			PveBossCombine = 21,
			[OriginalName("room_terms")]
			RoomTerms = 22,
			[OriginalName("lucky_star")]
			LuckyStar = 23
		}
	}

	private static readonly MessageParser<CauseOrigin> _parser = new MessageParser<CauseOrigin>(() => new CauseOrigin());

	private UnknownFieldSet _unknownFields;

	public const int SFieldNumber = 1;

	private Types.source s_;

	public const int IdFieldNumber = 3;

	private long id_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CauseOrigin> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[443];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.source S
	{
		get
		{
			return s_;
		}
		set
		{
			s_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Id
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
	public CauseOrigin()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CauseOrigin(CauseOrigin other)
		: this()
	{
		s_ = other.s_;
		id_ = other.id_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CauseOrigin Clone()
	{
		return new CauseOrigin(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CauseOrigin);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CauseOrigin other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (S != other.S)
		{
			return false;
		}
		if (Id != other.Id)
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
		if (S != Types.source.Unknown)
		{
			num ^= S.GetHashCode();
		}
		if (Id != 0L)
		{
			num ^= Id.GetHashCode();
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
		if (S != Types.source.Unknown)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)S);
		}
		if (Id != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(Id);
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
		if (S != Types.source.Unknown)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)S);
		}
		if (Id != 0L)
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
	public void MergeFrom(CauseOrigin other)
	{
		if (other != null)
		{
			if (other.S != Types.source.Unknown)
			{
				S = other.S;
			}
			if (other.Id != 0L)
			{
				Id = other.Id;
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
				S = (Types.source)input.ReadEnum();
				break;
			case 25u:
				Id = input.ReadSFixed64();
				break;
			}
		}
	}
}
