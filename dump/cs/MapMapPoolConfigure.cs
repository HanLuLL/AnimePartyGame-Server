using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class MapMapPoolConfigure : IMessage<MapMapPoolConfigure>, IMessage, IEquatable<MapMapPoolConfigure>, IDeepCloneable<MapMapPoolConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapMapPoolConfigure> _parser = new MessageParser<MapMapPoolConfigure>(() => new MapMapPoolConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int EventPoolFieldNumber = 2;

	private int eventPool_;

	public const int EffectCardPoolFieldNumber = 3;

	private int effectCardPool_;

	public const int BattleCardPoolFieldNumber = 4;

	private int battleCardPool_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapMapPoolConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[7];

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
	public int EventPool
	{
		get
		{
			return eventPool_;
		}
		private set
		{
			eventPool_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EffectCardPool
	{
		get
		{
			return effectCardPool_;
		}
		private set
		{
			effectCardPool_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BattleCardPool
	{
		get
		{
			return battleCardPool_;
		}
		private set
		{
			battleCardPool_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapPoolConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapPoolConfigure(MapMapPoolConfigure other)
		: this()
	{
		id_ = other.id_;
		eventPool_ = other.eventPool_;
		effectCardPool_ = other.effectCardPool_;
		battleCardPool_ = other.battleCardPool_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapPoolConfigure Clone()
	{
		return new MapMapPoolConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapMapPoolConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapMapPoolConfigure other)
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
		if (EventPool != other.EventPool)
		{
			return false;
		}
		if (EffectCardPool != other.EffectCardPool)
		{
			return false;
		}
		if (BattleCardPool != other.BattleCardPool)
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
		if (EventPool != 0)
		{
			num ^= EventPool.GetHashCode();
		}
		if (EffectCardPool != 0)
		{
			num ^= EffectCardPool.GetHashCode();
		}
		if (BattleCardPool != 0)
		{
			num ^= BattleCardPool.GetHashCode();
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
		if (EventPool != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(EventPool);
		}
		if (EffectCardPool != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(EffectCardPool);
		}
		if (BattleCardPool != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(BattleCardPool);
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
		if (EventPool != 0)
		{
			num += 5;
		}
		if (EffectCardPool != 0)
		{
			num += 5;
		}
		if (BattleCardPool != 0)
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
	public void MergeFrom(MapMapPoolConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.EventPool != 0)
			{
				EventPool = other.EventPool;
			}
			if (other.EffectCardPool != 0)
			{
				EffectCardPool = other.EffectCardPool;
			}
			if (other.BattleCardPool != 0)
			{
				BattleCardPool = other.BattleCardPool;
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
				EventPool = input.ReadSFixed32();
				break;
			case 29u:
				EffectCardPool = input.ReadSFixed32();
				break;
			case 37u:
				BattleCardPool = input.ReadSFixed32();
				break;
			}
		}
	}
}
