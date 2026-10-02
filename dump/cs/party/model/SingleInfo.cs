using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleInfo : IMessage<SingleInfo>, IMessage, IEquatable<SingleInfo>, IDeepCloneable<SingleInfo>, IBufferMessage
{
	private static readonly MessageParser<SingleInfo> _parser = new MessageParser<SingleInfo>(() => new SingleInfo());

	private UnknownFieldSet _unknownFields;

	public const int LevelPassIdsFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_levelPassIds_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<int, int> levelPassIds_ = new MapField<int, int>();

	public const int MaxScoreFieldNumber = 2;

	private int maxScore_;

	public const int StageLevelIdFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_stageLevelId_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> stageLevelId_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[35];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> LevelPassIds => levelPassIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxScore
	{
		get
		{
			return maxScore_;
		}
		set
		{
			maxScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> StageLevelId => stageLevelId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleInfo(SingleInfo other)
		: this()
	{
		levelPassIds_ = other.levelPassIds_.Clone();
		maxScore_ = other.maxScore_;
		stageLevelId_ = other.stageLevelId_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleInfo Clone()
	{
		return new SingleInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!LevelPassIds.Equals(other.LevelPassIds))
		{
			return false;
		}
		if (MaxScore != other.MaxScore)
		{
			return false;
		}
		if (!StageLevelId.Equals(other.StageLevelId))
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
		num ^= LevelPassIds.GetHashCode();
		if (MaxScore != 0)
		{
			num ^= MaxScore.GetHashCode();
		}
		num ^= StageLevelId.GetHashCode();
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
		levelPassIds_.WriteTo(ref output, _map_levelPassIds_codec);
		if (MaxScore != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MaxScore);
		}
		stageLevelId_.WriteTo(ref output, _map_stageLevelId_codec);
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
		num += levelPassIds_.CalculateSize(_map_levelPassIds_codec);
		if (MaxScore != 0)
		{
			num += 5;
		}
		num += stageLevelId_.CalculateSize(_map_stageLevelId_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SingleInfo other)
	{
		if (other != null)
		{
			levelPassIds_.MergeFrom(other.levelPassIds_);
			if (other.MaxScore != 0)
			{
				MaxScore = other.MaxScore;
			}
			stageLevelId_.MergeFrom(other.stageLevelId_);
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
				levelPassIds_.AddEntriesFrom(ref input, _map_levelPassIds_codec);
				break;
			case 21u:
				MaxScore = input.ReadSFixed32();
				break;
			case 26u:
				stageLevelId_.AddEntriesFrom(ref input, _map_stageLevelId_codec);
				break;
			}
		}
	}
}
