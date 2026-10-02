using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GameModeDifficultyDataConfigure : IMessage<GameModeDifficultyDataConfigure>, IMessage, IEquatable<GameModeDifficultyDataConfigure>, IDeepCloneable<GameModeDifficultyDataConfigure>, IBufferMessage
{
	private static readonly MessageParser<GameModeDifficultyDataConfigure> _parser = new MessageParser<GameModeDifficultyDataConfigure>(() => new GameModeDifficultyDataConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapModeTypeFieldNumber = 1;

	private MapModeType mapModeType_;

	public const int GameModeDifficultyDataConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<GameModeDifficultyDataConfigureItem> _repeated_gameModeDifficultyDataConfigureItems_codec = FieldCodec.ForMessage(18u, GameModeDifficultyDataConfigureItem.Parser);

	private readonly RepeatedField<GameModeDifficultyDataConfigureItem> gameModeDifficultyDataConfigureItems_ = new RepeatedField<GameModeDifficultyDataConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeDifficultyDataConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapModeType MapModeType
	{
		get
		{
			return mapModeType_;
		}
		private set
		{
			mapModeType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeDifficultyDataConfigureItem> GameModeDifficultyDataConfigureItems => gameModeDifficultyDataConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeDifficultyDataConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeDifficultyDataConfigure(GameModeDifficultyDataConfigure other)
		: this()
	{
		mapModeType_ = other.mapModeType_;
		gameModeDifficultyDataConfigureItems_ = other.gameModeDifficultyDataConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeDifficultyDataConfigure Clone()
	{
		return new GameModeDifficultyDataConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeDifficultyDataConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeDifficultyDataConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapModeType != other.MapModeType)
		{
			return false;
		}
		if (!gameModeDifficultyDataConfigureItems_.Equals(other.gameModeDifficultyDataConfigureItems_))
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
		if (MapModeType != MapModeType.None)
		{
			num ^= MapModeType.GetHashCode();
		}
		num ^= gameModeDifficultyDataConfigureItems_.GetHashCode();
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
		if (MapModeType != MapModeType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)MapModeType);
		}
		gameModeDifficultyDataConfigureItems_.WriteTo(ref output, _repeated_gameModeDifficultyDataConfigureItems_codec);
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
		if (MapModeType != MapModeType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MapModeType);
		}
		num += gameModeDifficultyDataConfigureItems_.CalculateSize(_repeated_gameModeDifficultyDataConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameModeDifficultyDataConfigure other)
	{
		if (other != null)
		{
			if (other.MapModeType != MapModeType.None)
			{
				MapModeType = other.MapModeType;
			}
			gameModeDifficultyDataConfigureItems_.Add(other.gameModeDifficultyDataConfigureItems_);
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
				MapModeType = (MapModeType)input.ReadEnum();
				break;
			case 18u:
				gameModeDifficultyDataConfigureItems_.AddEntriesFrom(ref input, _repeated_gameModeDifficultyDataConfigureItems_codec);
				break;
			}
		}
	}
}
