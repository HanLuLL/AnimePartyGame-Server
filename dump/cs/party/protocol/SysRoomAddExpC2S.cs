using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysRoomAddExpC2S : IMessage<SysRoomAddExpC2S>, IMessage, IEquatable<SysRoomAddExpC2S>, IDeepCloneable<SysRoomAddExpC2S>, IBufferMessage
{
	private static readonly MessageParser<SysRoomAddExpC2S> _parser = new MessageParser<SysRoomAddExpC2S>(() => new SysRoomAddExpC2S());

	private UnknownFieldSet _unknownFields;

	public const int AwardsFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_awards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<int, int> awards_ = new MapField<int, int>();

	public const int RookieBonusAwardsFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_rookieBonusAwards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> rookieBonusAwards_ = new MapField<int, int>();

	public const int ReturnBonusAwardsFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_returnBonusAwards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> returnBonusAwards_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysRoomAddExpC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[486];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Awards => awards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RookieBonusAwards => rookieBonusAwards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ReturnBonusAwards => returnBonusAwards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomAddExpC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomAddExpC2S(SysRoomAddExpC2S other)
		: this()
	{
		awards_ = other.awards_.Clone();
		rookieBonusAwards_ = other.rookieBonusAwards_.Clone();
		returnBonusAwards_ = other.returnBonusAwards_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomAddExpC2S Clone()
	{
		return new SysRoomAddExpC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysRoomAddExpC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysRoomAddExpC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Awards.Equals(other.Awards))
		{
			return false;
		}
		if (!RookieBonusAwards.Equals(other.RookieBonusAwards))
		{
			return false;
		}
		if (!ReturnBonusAwards.Equals(other.ReturnBonusAwards))
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
		num ^= Awards.GetHashCode();
		num ^= RookieBonusAwards.GetHashCode();
		num ^= ReturnBonusAwards.GetHashCode();
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
		awards_.WriteTo(ref output, _map_awards_codec);
		rookieBonusAwards_.WriteTo(ref output, _map_rookieBonusAwards_codec);
		returnBonusAwards_.WriteTo(ref output, _map_returnBonusAwards_codec);
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
		num += awards_.CalculateSize(_map_awards_codec);
		num += rookieBonusAwards_.CalculateSize(_map_rookieBonusAwards_codec);
		num += returnBonusAwards_.CalculateSize(_map_returnBonusAwards_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysRoomAddExpC2S other)
	{
		if (other != null)
		{
			awards_.MergeFrom(other.awards_);
			rookieBonusAwards_.MergeFrom(other.rookieBonusAwards_);
			returnBonusAwards_.MergeFrom(other.returnBonusAwards_);
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
				awards_.AddEntriesFrom(ref input, _map_awards_codec);
				break;
			case 18u:
				rookieBonusAwards_.AddEntriesFrom(ref input, _map_rookieBonusAwards_codec);
				break;
			case 26u:
				returnBonusAwards_.AddEntriesFrom(ref input, _map_returnBonusAwards_codec);
				break;
			}
		}
	}
}
