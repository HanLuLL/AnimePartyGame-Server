using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class LuckyStarMissionInfo : IMessage<LuckyStarMissionInfo>, IMessage, IEquatable<LuckyStarMissionInfo>, IDeepCloneable<LuckyStarMissionInfo>, IBufferMessage
{
	private static readonly MessageParser<LuckyStarMissionInfo> _parser = new MessageParser<LuckyStarMissionInfo>(() => new LuckyStarMissionInfo());

	private UnknownFieldSet _unknownFields;

	public const int LuckyStarMissionFieldNumber = 1;

	private static readonly MapField<long, LuckyStarMission>.Codec _map_luckyStarMission_codec = new MapField<long, LuckyStarMission>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForMessage(18u, party.model.LuckyStarMission.Parser), 10u);

	private readonly MapField<long, LuckyStarMission> luckyStarMission_ = new MapField<long, LuckyStarMission>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LuckyStarMissionInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[74];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, LuckyStarMission> LuckyStarMission => luckyStarMission_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMissionInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMissionInfo(LuckyStarMissionInfo other)
		: this()
	{
		luckyStarMission_ = other.luckyStarMission_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMissionInfo Clone()
	{
		return new LuckyStarMissionInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LuckyStarMissionInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LuckyStarMissionInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!LuckyStarMission.Equals(other.LuckyStarMission))
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
		num ^= LuckyStarMission.GetHashCode();
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
		luckyStarMission_.WriteTo(ref output, _map_luckyStarMission_codec);
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
		num += luckyStarMission_.CalculateSize(_map_luckyStarMission_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LuckyStarMissionInfo other)
	{
		if (other != null)
		{
			luckyStarMission_.MergeFrom(other.luckyStarMission_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				luckyStarMission_.AddEntriesFrom(ref input, _map_luckyStarMission_codec);
			}
		}
	}
}
