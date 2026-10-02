using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class HeroBuffChangeS2C : IMessage<HeroBuffChangeS2C>, IMessage, IEquatable<HeroBuffChangeS2C>, IDeepCloneable<HeroBuffChangeS2C>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum Oper
		{
			[OriginalName("NOOP")]
			Noop,
			[OriginalName("Insert")]
			Insert,
			[OriginalName("Delete")]
			Delete,
			[OriginalName("Update")]
			Update
		}
	}

	private static readonly MessageParser<HeroBuffChangeS2C> _parser = new MessageParser<HeroBuffChangeS2C>(() => new HeroBuffChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int BuffFieldNumber = 2;

	private Buff buff_;

	public const int OpFieldNumber = 3;

	private Types.Oper op_;

	public const int BuffsFieldNumber = 4;

	private static readonly MapField<long, Buff>.Codec _map_buffs_codec = new MapField<long, Buff>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, Buff.Parser), 34u);

	private readonly MapField<long, Buff> buffs_ = new MapField<long, Buff>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroBuffChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[387];

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
	public Buff Buff
	{
		get
		{
			return buff_;
		}
		set
		{
			buff_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.Oper Op
	{
		get
		{
			return op_;
		}
		set
		{
			op_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, Buff> Buffs => buffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBuffChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBuffChangeS2C(HeroBuffChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		buff_ = ((other.buff_ != null) ? other.buff_.Clone() : null);
		op_ = other.op_;
		buffs_ = other.buffs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBuffChangeS2C Clone()
	{
		return new HeroBuffChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroBuffChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroBuffChangeS2C other)
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
		if (!object.Equals(Buff, other.Buff))
		{
			return false;
		}
		if (Op != other.Op)
		{
			return false;
		}
		if (!Buffs.Equals(other.Buffs))
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
		if (buff_ != null)
		{
			num ^= Buff.GetHashCode();
		}
		if (Op != Types.Oper.Noop)
		{
			num ^= Op.GetHashCode();
		}
		num ^= Buffs.GetHashCode();
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
		if (buff_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Buff);
		}
		if (Op != Types.Oper.Noop)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)Op);
		}
		buffs_.WriteTo(ref output, _map_buffs_codec);
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
		if (buff_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Buff);
		}
		if (Op != Types.Oper.Noop)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Op);
		}
		num += buffs_.CalculateSize(_map_buffs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(HeroBuffChangeS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
		}
		if (other.buff_ != null)
		{
			if (buff_ == null)
			{
				Buff = new Buff();
			}
			Buff.MergeFrom(other.Buff);
		}
		if (other.Op != Types.Oper.Noop)
		{
			Op = other.Op;
		}
		buffs_.MergeFrom(other.buffs_);
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 18u:
				if (buff_ == null)
				{
					Buff = new Buff();
				}
				input.ReadMessage(Buff);
				break;
			case 24u:
				Op = (Types.Oper)input.ReadEnum();
				break;
			case 34u:
				buffs_.AddEntriesFrom(ref input, _map_buffs_codec);
				break;
			}
		}
	}
}
