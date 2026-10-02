using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysCampaignFinishC2S : IMessage<SysCampaignFinishC2S>, IMessage, IEquatable<SysCampaignFinishC2S>, IDeepCloneable<SysCampaignFinishC2S>, IBufferMessage
{
	private static readonly MessageParser<SysCampaignFinishC2S> _parser = new MessageParser<SysCampaignFinishC2S>(() => new SysCampaignFinishC2S());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int MapTypeFieldNumber = 4;

	private int mapType_;

	public const int LevelIdFieldNumber = 2;

	private int levelId_;

	public const int CondFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_cond_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> cond_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysCampaignFinishC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[484];

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
	public int LevelId
	{
		get
		{
			return levelId_;
		}
		set
		{
			levelId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Cond => cond_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysCampaignFinishC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysCampaignFinishC2S(SysCampaignFinishC2S other)
		: this()
	{
		playerId_ = other.playerId_;
		mapType_ = other.mapType_;
		levelId_ = other.levelId_;
		cond_ = other.cond_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysCampaignFinishC2S Clone()
	{
		return new SysCampaignFinishC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysCampaignFinishC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysCampaignFinishC2S other)
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
		if (MapType != other.MapType)
		{
			return false;
		}
		if (LevelId != other.LevelId)
		{
			return false;
		}
		if (!Cond.Equals(other.Cond))
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
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (LevelId != 0)
		{
			num ^= LevelId.GetHashCode();
		}
		num ^= Cond.GetHashCode();
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
		if (LevelId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(LevelId);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MapType);
		}
		cond_.WriteTo(ref output, _map_cond_codec);
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
		if (MapType != 0)
		{
			num += 5;
		}
		if (LevelId != 0)
		{
			num += 5;
		}
		num += cond_.CalculateSize(_map_cond_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysCampaignFinishC2S other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.MapType != 0)
			{
				MapType = other.MapType;
			}
			if (other.LevelId != 0)
			{
				LevelId = other.LevelId;
			}
			cond_.MergeFrom(other.cond_);
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
			case 21u:
				LevelId = input.ReadSFixed32();
				break;
			case 37u:
				MapType = input.ReadSFixed32();
				break;
			case 82u:
				cond_.AddEntriesFrom(ref input, _map_cond_codec);
				break;
			}
		}
	}
}
