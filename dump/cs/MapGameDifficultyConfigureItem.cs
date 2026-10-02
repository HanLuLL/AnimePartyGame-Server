using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class MapGameDifficultyConfigureItem : IMessage<MapGameDifficultyConfigureItem>, IMessage, IEquatable<MapGameDifficultyConfigureItem>, IDeepCloneable<MapGameDifficultyConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<MapGameDifficultyConfigureItem> _parser = new MessageParser<MapGameDifficultyConfigureItem>(() => new MapGameDifficultyConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int ProgressLimitFieldNumber = 2;

	private int progressLimit_;

	public const int ProgressEventsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_progressEvents_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> progressEvents_ = new RepeatedField<int>();

	public const int PveMissionsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_pveMissions_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> pveMissions_ = new RepeatedField<int>();

	public const int MonsterIdsFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_monsterIds_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> monsterIds_ = new MapField<int, int>();

	public const int PreloadCharacterIdsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_preloadCharacterIds_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> preloadCharacterIds_ = new RepeatedField<int>();

	public const int ExtraModesFieldNumber = 7;

	private static readonly FieldCodec<PVEExtraModeType> _repeated_extraModes_codec = FieldCodec.ForEnum(58u, (PVEExtraModeType x) => (int)x, (int x) => (PVEExtraModeType)x);

	private readonly RepeatedField<PVEExtraModeType> extraModes_ = new RepeatedField<PVEExtraModeType>();

	public const int MutatorPoolFieldNumber = 8;

	private int mutatorPool_;

	public const int PerformTriggerSetIdFieldNumber = 9;

	private int performTriggerSetId_;

	private readonly List<MapEventInfoConfigure> _mapEventInfoConfigures = new List<MapEventInfoConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapGameDifficultyConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[3];

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
	public int ProgressLimit
	{
		get
		{
			return progressLimit_;
		}
		private set
		{
			progressLimit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ProgressEvents => progressEvents_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PveMissions => pveMissions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MonsterIds => monsterIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PreloadCharacterIds => preloadCharacterIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PVEExtraModeType> ExtraModes => extraModes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MutatorPool
	{
		get
		{
			return mutatorPool_;
		}
		private set
		{
			mutatorPool_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformTriggerSetId
	{
		get
		{
			return performTriggerSetId_;
		}
		private set
		{
			performTriggerSetId_ = value;
		}
	}

	public List<MapEventInfoConfigure> MapEventInfoConfigures
	{
		get
		{
			if (_mapEventInfoConfigures.Count == 0)
			{
				for (int i = 0; i < ProgressEvents.Count; i++)
				{
					if (!StaticConfigure.MapEvent.InfoDict.TryGetValue(ProgressEvents[i], out var value))
					{
						Debug.LogError("在MapEvent.InfoDict表里并没有找到事件id：" + ProgressEvents[i]);
						return null;
					}
					_mapEventInfoConfigures.Add(value);
				}
			}
			return _mapEventInfoConfigures;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapGameDifficultyConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapGameDifficultyConfigureItem(MapGameDifficultyConfigureItem other)
		: this()
	{
		index_ = other.index_;
		progressLimit_ = other.progressLimit_;
		progressEvents_ = other.progressEvents_.Clone();
		pveMissions_ = other.pveMissions_.Clone();
		monsterIds_ = other.monsterIds_.Clone();
		preloadCharacterIds_ = other.preloadCharacterIds_.Clone();
		extraModes_ = other.extraModes_.Clone();
		mutatorPool_ = other.mutatorPool_;
		performTriggerSetId_ = other.performTriggerSetId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapGameDifficultyConfigureItem Clone()
	{
		return new MapGameDifficultyConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapGameDifficultyConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapGameDifficultyConfigureItem other)
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
		if (ProgressLimit != other.ProgressLimit)
		{
			return false;
		}
		if (!progressEvents_.Equals(other.progressEvents_))
		{
			return false;
		}
		if (!pveMissions_.Equals(other.pveMissions_))
		{
			return false;
		}
		if (!MonsterIds.Equals(other.MonsterIds))
		{
			return false;
		}
		if (!preloadCharacterIds_.Equals(other.preloadCharacterIds_))
		{
			return false;
		}
		if (!extraModes_.Equals(other.extraModes_))
		{
			return false;
		}
		if (MutatorPool != other.MutatorPool)
		{
			return false;
		}
		if (PerformTriggerSetId != other.PerformTriggerSetId)
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
		if (ProgressLimit != 0)
		{
			num ^= ProgressLimit.GetHashCode();
		}
		num ^= progressEvents_.GetHashCode();
		num ^= pveMissions_.GetHashCode();
		num ^= MonsterIds.GetHashCode();
		num ^= preloadCharacterIds_.GetHashCode();
		num ^= extraModes_.GetHashCode();
		if (MutatorPool != 0)
		{
			num ^= MutatorPool.GetHashCode();
		}
		if (PerformTriggerSetId != 0)
		{
			num ^= PerformTriggerSetId.GetHashCode();
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
		if (ProgressLimit != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ProgressLimit);
		}
		progressEvents_.WriteTo(ref output, _repeated_progressEvents_codec);
		pveMissions_.WriteTo(ref output, _repeated_pveMissions_codec);
		monsterIds_.WriteTo(ref output, _map_monsterIds_codec);
		preloadCharacterIds_.WriteTo(ref output, _repeated_preloadCharacterIds_codec);
		extraModes_.WriteTo(ref output, _repeated_extraModes_codec);
		if (MutatorPool != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(MutatorPool);
		}
		if (PerformTriggerSetId != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(PerformTriggerSetId);
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
		if (ProgressLimit != 0)
		{
			num += 5;
		}
		num += progressEvents_.CalculateSize(_repeated_progressEvents_codec);
		num += pveMissions_.CalculateSize(_repeated_pveMissions_codec);
		num += monsterIds_.CalculateSize(_map_monsterIds_codec);
		num += preloadCharacterIds_.CalculateSize(_repeated_preloadCharacterIds_codec);
		num += extraModes_.CalculateSize(_repeated_extraModes_codec);
		if (MutatorPool != 0)
		{
			num += 5;
		}
		if (PerformTriggerSetId != 0)
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
	public void MergeFrom(MapGameDifficultyConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.ProgressLimit != 0)
			{
				ProgressLimit = other.ProgressLimit;
			}
			progressEvents_.Add(other.progressEvents_);
			pveMissions_.Add(other.pveMissions_);
			monsterIds_.MergeFrom(other.monsterIds_);
			preloadCharacterIds_.Add(other.preloadCharacterIds_);
			extraModes_.Add(other.extraModes_);
			if (other.MutatorPool != 0)
			{
				MutatorPool = other.MutatorPool;
			}
			if (other.PerformTriggerSetId != 0)
			{
				PerformTriggerSetId = other.PerformTriggerSetId;
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
			case 21u:
				ProgressLimit = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				progressEvents_.AddEntriesFrom(ref input, _repeated_progressEvents_codec);
				break;
			case 34u:
			case 37u:
				pveMissions_.AddEntriesFrom(ref input, _repeated_pveMissions_codec);
				break;
			case 42u:
				monsterIds_.AddEntriesFrom(ref input, _map_monsterIds_codec);
				break;
			case 50u:
			case 53u:
				preloadCharacterIds_.AddEntriesFrom(ref input, _repeated_preloadCharacterIds_codec);
				break;
			case 56u:
			case 58u:
				extraModes_.AddEntriesFrom(ref input, _repeated_extraModes_codec);
				break;
			case 69u:
				MutatorPool = input.ReadSFixed32();
				break;
			case 77u:
				PerformTriggerSetId = input.ReadSFixed32();
				break;
			}
		}
	}
}
