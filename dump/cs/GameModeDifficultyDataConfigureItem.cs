using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GameModeDifficultyDataConfigureItem : IMessage<GameModeDifficultyDataConfigureItem>, IMessage, IEquatable<GameModeDifficultyDataConfigureItem>, IDeepCloneable<GameModeDifficultyDataConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<GameModeDifficultyDataConfigureItem> _parser = new MessageParser<GameModeDifficultyDataConfigureItem>(() => new GameModeDifficultyDataConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int DistributeResourcesFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_distributeResources_codec = FieldCodec.ForSInt32(18u);

	private readonly RepeatedField<int> distributeResources_ = new RepeatedField<int>();

	public const int UpgradeIdFieldNumber = 3;

	private int upgradeId_;

	public const int DifficultyDescIdFieldNumber = 4;

	private int difficultyDescId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeDifficultyDataConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> DistributeResources => distributeResources_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpgradeId
	{
		get
		{
			return upgradeId_;
		}
		private set
		{
			upgradeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DifficultyDescId
	{
		get
		{
			return difficultyDescId_;
		}
		private set
		{
			difficultyDescId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeDifficultyDataConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeDifficultyDataConfigureItem(GameModeDifficultyDataConfigureItem other)
		: this()
	{
		index_ = other.index_;
		distributeResources_ = other.distributeResources_.Clone();
		upgradeId_ = other.upgradeId_;
		difficultyDescId_ = other.difficultyDescId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeDifficultyDataConfigureItem Clone()
	{
		return new GameModeDifficultyDataConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeDifficultyDataConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeDifficultyDataConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (!distributeResources_.Equals(other.distributeResources_))
		{
			return false;
		}
		if (UpgradeId != other.UpgradeId)
		{
			return false;
		}
		if (DifficultyDescId != other.DifficultyDescId)
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		num ^= distributeResources_.GetHashCode();
		if (UpgradeId != 0)
		{
			num ^= UpgradeId.GetHashCode();
		}
		if (DifficultyDescId != 0)
		{
			num ^= DifficultyDescId.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		distributeResources_.WriteTo(ref output, _repeated_distributeResources_codec);
		if (UpgradeId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(UpgradeId);
		}
		if (DifficultyDescId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DifficultyDescId);
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
		if (Index != 0)
		{
			num += 5;
		}
		num += distributeResources_.CalculateSize(_repeated_distributeResources_codec);
		if (UpgradeId != 0)
		{
			num += 5;
		}
		if (DifficultyDescId != 0)
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
	public void MergeFrom(GameModeDifficultyDataConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			distributeResources_.Add(other.distributeResources_);
			if (other.UpgradeId != 0)
			{
				UpgradeId = other.UpgradeId;
			}
			if (other.DifficultyDescId != 0)
			{
				DifficultyDescId = other.DifficultyDescId;
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
				Index = input.ReadSFixed32();
				break;
			case 16u:
			case 18u:
				distributeResources_.AddEntriesFrom(ref input, _repeated_distributeResources_codec);
				break;
			case 29u:
				UpgradeId = input.ReadSFixed32();
				break;
			case 37u:
				DifficultyDescId = input.ReadSFixed32();
				break;
			}
		}
	}
}
