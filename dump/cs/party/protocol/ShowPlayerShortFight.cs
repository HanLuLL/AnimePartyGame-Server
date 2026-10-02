using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ShowPlayerShortFight : IMessage<ShowPlayerShortFight>, IMessage, IEquatable<ShowPlayerShortFight>, IDeepCloneable<ShowPlayerShortFight>, IBufferMessage
{
	private static readonly MessageParser<ShowPlayerShortFight> _parser = new MessageParser<ShowPlayerShortFight>(() => new ShowPlayerShortFight());

	private UnknownFieldSet _unknownFields;

	public const int TimeFieldNumber = 1;

	private long time_;

	public const int RankFieldNumber = 2;

	private int rank_;

	public const int HeroIdFieldNumber = 3;

	private int heroId_;

	public const int IndexFieldNumber = 4;

	private int index_;

	public const int MapTypeFieldNumber = 5;

	private int mapType_;

	public const int ReplayIdFieldNumber = 6;

	private string replayId_ = "";

	public const int VersionFieldNumber = 7;

	private string version_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShowPlayerShortFight> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[106];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Time
	{
		get
		{
			return time_;
		}
		set
		{
			time_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Rank
	{
		get
		{
			return rank_;
		}
		set
		{
			rank_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeroId
	{
		get
		{
			return heroId_;
		}
		set
		{
			heroId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapType
	{
		get
		{
			return mapType_;
		}
		set
		{
			mapType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ReplayId
	{
		get
		{
			return replayId_;
		}
		set
		{
			replayId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Version
	{
		get
		{
			return version_;
		}
		set
		{
			version_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerShortFight()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerShortFight(ShowPlayerShortFight other)
		: this()
	{
		time_ = other.time_;
		rank_ = other.rank_;
		heroId_ = other.heroId_;
		index_ = other.index_;
		mapType_ = other.mapType_;
		replayId_ = other.replayId_;
		version_ = other.version_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerShortFight Clone()
	{
		return new ShowPlayerShortFight(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShowPlayerShortFight);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShowPlayerShortFight other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Time != other.Time)
		{
			return false;
		}
		if (Rank != other.Rank)
		{
			return false;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (ReplayId != other.ReplayId)
		{
			return false;
		}
		if (Version != other.Version)
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
		if (Time != 0L)
		{
			num ^= Time.GetHashCode();
		}
		if (Rank != 0)
		{
			num ^= Rank.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (ReplayId.Length != 0)
		{
			num ^= ReplayId.GetHashCode();
		}
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
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
		if (Time != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Time);
		}
		if (Rank != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Rank);
		}
		if (HeroId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(HeroId);
		}
		if (Index != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Index);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(MapType);
		}
		if (ReplayId.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(ReplayId);
		}
		if (Version.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Version);
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
		if (Time != 0L)
		{
			num += 9;
		}
		if (Rank != 0)
		{
			num += 5;
		}
		if (HeroId != 0)
		{
			num += 5;
		}
		if (Index != 0)
		{
			num += 5;
		}
		if (MapType != 0)
		{
			num += 5;
		}
		if (ReplayId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ReplayId);
		}
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ShowPlayerShortFight other)
	{
		if (other != null)
		{
			if (other.Time != 0L)
			{
				Time = other.Time;
			}
			if (other.Rank != 0)
			{
				Rank = other.Rank;
			}
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.MapType != 0)
			{
				MapType = other.MapType;
			}
			if (other.ReplayId.Length != 0)
			{
				ReplayId = other.ReplayId;
			}
			if (other.Version.Length != 0)
			{
				Version = other.Version;
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
				Time = input.ReadSFixed64();
				break;
			case 21u:
				Rank = input.ReadSFixed32();
				break;
			case 29u:
				HeroId = input.ReadSFixed32();
				break;
			case 37u:
				Index = input.ReadSFixed32();
				break;
			case 45u:
				MapType = input.ReadSFixed32();
				break;
			case 50u:
				ReplayId = input.ReadString();
				break;
			case 58u:
				Version = input.ReadString();
				break;
			}
		}
	}
}
