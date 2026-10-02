using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using UI;
using UnityEngine;

public sealed class MapInfoConfigure : IMessage<MapInfoConfigure>, IMessage, IEquatable<MapInfoConfigure>, IDeepCloneable<MapInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapInfoConfigure> _parser = new MessageParser<MapInfoConfigure>(() => new MapInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int MapNameFieldNumber = 2;

	private int mapName_;

	public const int MapDescriptionFieldNumber = 3;

	private int mapDescription_;

	public const int MapGalleryFieldNumber = 4;

	private int mapGallery_;

	public const int MidsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_mids_codec = FieldCodec.ForSInt32(42u);

	private readonly RepeatedField<int> mids_ = new RepeatedField<int>();

	public const int DifficultyIdsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_difficultyIds_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> difficultyIds_ = new RepeatedField<int>();

	public const int MapeventIDsFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_mapeventIDs_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> mapeventIDs_ = new RepeatedField<int>();

	public const int MapImageFieldNumber = 8;

	private string mapImage_ = "";

	public const int MapSceneImageFieldNumber = 9;

	private string mapSceneImage_ = "";

	public const int TutorialIdFieldNumber = 10;

	private int tutorialId_;

	public const int MapSpecialCardFieldNumber = 11;

	private static readonly FieldCodec<int> _repeated_mapSpecialCard_codec = FieldCodec.ForSFixed32(90u);

	private readonly RepeatedField<int> mapSpecialCard_ = new RepeatedField<int>();

	public const int MapSpecialEventFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_mapSpecialEvent_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> mapSpecialEvent_ = new RepeatedField<int>();

	public const int SpLandFieldNumber = 13;

	private LandType spLand_;

	public const int MapSpecialRelicFieldNumber = 14;

	private static readonly FieldCodec<int> _repeated_mapSpecialRelic_codec = FieldCodec.ForSFixed32(114u);

	private readonly RepeatedField<int> mapSpecialRelic_ = new RepeatedField<int>();

	public const int PreloadCharacterIdsFieldNumber = 15;

	private static readonly FieldCodec<int> _repeated_preloadCharacterIds_codec = FieldCodec.ForSFixed32(122u);

	private readonly RepeatedField<int> preloadCharacterIds_ = new RepeatedField<int>();

	public const int StoryIdsFieldNumber = 16;

	private static readonly FieldCodec<int> _repeated_storyIds_codec = FieldCodec.ForSFixed32(130u);

	private readonly RepeatedField<int> storyIds_ = new RepeatedField<int>();

	public const int BeginTimeFieldNumber = 17;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 18;

	private Timestamp endTime_;

	public const int BeginTimeMapFieldNumber = 19;

	private Timestamp beginTimeMap_;

	public const int EndTimeMapFieldNumber = 20;

	private Timestamp endTimeMap_;

	public const int LabelTypeFieldNumber = 21;

	private int labelType_;

	public const int MapMarkFieldNumber = 22;

	private int mapMark_;

	private readonly List<MapEventInfoConfigure> _mapEventInfoConfigures = new List<MapEventInfoConfigure>();

	private List<int> _PreloadCharacterIds;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[0];

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
	public int MapName
	{
		get
		{
			return mapName_;
		}
		private set
		{
			mapName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapDescription
	{
		get
		{
			return mapDescription_;
		}
		private set
		{
			mapDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapGallery
	{
		get
		{
			return mapGallery_;
		}
		private set
		{
			mapGallery_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Mids => mids_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> DifficultyIds => difficultyIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapeventIDs => mapeventIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string MapImage
	{
		get
		{
			return mapImage_;
		}
		private set
		{
			mapImage_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string MapSceneImage
	{
		get
		{
			return mapSceneImage_;
		}
		private set
		{
			mapSceneImage_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TutorialId
	{
		get
		{
			return tutorialId_;
		}
		private set
		{
			tutorialId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapSpecialCard => mapSpecialCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapSpecialEvent => mapSpecialEvent_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandType SpLand
	{
		get
		{
			return spLand_;
		}
		private set
		{
			spLand_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapSpecialRelic => mapSpecialRelic_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PreloadCharacterIds => preloadCharacterIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> StoryIds => storyIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp BeginTimeMap
	{
		get
		{
			return beginTimeMap_;
		}
		private set
		{
			beginTimeMap_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTimeMap
	{
		get
		{
			return endTimeMap_;
		}
		private set
		{
			endTimeMap_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LabelType
	{
		get
		{
			return labelType_;
		}
		private set
		{
			labelType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapMark
	{
		get
		{
			return mapMark_;
		}
		private set
		{
			mapMark_ = value;
		}
	}

	public List<MapEventInfoConfigure> MapEventInfoConfigures
	{
		get
		{
			if (_mapEventInfoConfigures.Count == 0)
			{
				for (int i = 0; i < MapeventIDs.Count; i++)
				{
					if (!StaticConfigure.MapEvent.InfoDict.TryGetValue(MapeventIDs[i], out var value))
					{
						Debug.LogError("在MapEvent.InfoDict表里并没有找到事件id：" + MapeventIDs[i]);
						return null;
					}
					if (!value.IsHide)
					{
						_mapEventInfoConfigures.Add(value);
					}
				}
			}
			return _mapEventInfoConfigures;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapInfoConfigure(MapInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		mapName_ = other.mapName_;
		mapDescription_ = other.mapDescription_;
		mapGallery_ = other.mapGallery_;
		mids_ = other.mids_.Clone();
		difficultyIds_ = other.difficultyIds_.Clone();
		mapeventIDs_ = other.mapeventIDs_.Clone();
		mapImage_ = other.mapImage_;
		mapSceneImage_ = other.mapSceneImage_;
		tutorialId_ = other.tutorialId_;
		mapSpecialCard_ = other.mapSpecialCard_.Clone();
		mapSpecialEvent_ = other.mapSpecialEvent_.Clone();
		spLand_ = other.spLand_;
		mapSpecialRelic_ = other.mapSpecialRelic_.Clone();
		preloadCharacterIds_ = other.preloadCharacterIds_.Clone();
		storyIds_ = other.storyIds_.Clone();
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		beginTimeMap_ = ((other.beginTimeMap_ != null) ? other.beginTimeMap_.Clone() : null);
		endTimeMap_ = ((other.endTimeMap_ != null) ? other.endTimeMap_.Clone() : null);
		labelType_ = other.labelType_;
		mapMark_ = other.mapMark_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapInfoConfigure Clone()
	{
		return new MapInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapInfoConfigure other)
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
		if (MapName != other.MapName)
		{
			return false;
		}
		if (MapDescription != other.MapDescription)
		{
			return false;
		}
		if (MapGallery != other.MapGallery)
		{
			return false;
		}
		if (!mids_.Equals(other.mids_))
		{
			return false;
		}
		if (!difficultyIds_.Equals(other.difficultyIds_))
		{
			return false;
		}
		if (!mapeventIDs_.Equals(other.mapeventIDs_))
		{
			return false;
		}
		if (MapImage != other.MapImage)
		{
			return false;
		}
		if (MapSceneImage != other.MapSceneImage)
		{
			return false;
		}
		if (TutorialId != other.TutorialId)
		{
			return false;
		}
		if (!mapSpecialCard_.Equals(other.mapSpecialCard_))
		{
			return false;
		}
		if (!mapSpecialEvent_.Equals(other.mapSpecialEvent_))
		{
			return false;
		}
		if (SpLand != other.SpLand)
		{
			return false;
		}
		if (!mapSpecialRelic_.Equals(other.mapSpecialRelic_))
		{
			return false;
		}
		if (!preloadCharacterIds_.Equals(other.preloadCharacterIds_))
		{
			return false;
		}
		if (!storyIds_.Equals(other.storyIds_))
		{
			return false;
		}
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (!object.Equals(BeginTimeMap, other.BeginTimeMap))
		{
			return false;
		}
		if (!object.Equals(EndTimeMap, other.EndTimeMap))
		{
			return false;
		}
		if (LabelType != other.LabelType)
		{
			return false;
		}
		if (MapMark != other.MapMark)
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
		if (MapName != 0)
		{
			num ^= MapName.GetHashCode();
		}
		if (MapDescription != 0)
		{
			num ^= MapDescription.GetHashCode();
		}
		if (MapGallery != 0)
		{
			num ^= MapGallery.GetHashCode();
		}
		num ^= mids_.GetHashCode();
		num ^= difficultyIds_.GetHashCode();
		num ^= mapeventIDs_.GetHashCode();
		if (MapImage.Length != 0)
		{
			num ^= MapImage.GetHashCode();
		}
		if (MapSceneImage.Length != 0)
		{
			num ^= MapSceneImage.GetHashCode();
		}
		if (TutorialId != 0)
		{
			num ^= TutorialId.GetHashCode();
		}
		num ^= mapSpecialCard_.GetHashCode();
		num ^= mapSpecialEvent_.GetHashCode();
		if (SpLand != LandType.None)
		{
			num ^= SpLand.GetHashCode();
		}
		num ^= mapSpecialRelic_.GetHashCode();
		num ^= preloadCharacterIds_.GetHashCode();
		num ^= storyIds_.GetHashCode();
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (beginTimeMap_ != null)
		{
			num ^= BeginTimeMap.GetHashCode();
		}
		if (endTimeMap_ != null)
		{
			num ^= EndTimeMap.GetHashCode();
		}
		if (LabelType != 0)
		{
			num ^= LabelType.GetHashCode();
		}
		if (MapMark != 0)
		{
			num ^= MapMark.GetHashCode();
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
		if (MapName != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MapName);
		}
		if (MapDescription != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MapDescription);
		}
		if (MapGallery != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MapGallery);
		}
		mids_.WriteTo(ref output, _repeated_mids_codec);
		difficultyIds_.WriteTo(ref output, _repeated_difficultyIds_codec);
		mapeventIDs_.WriteTo(ref output, _repeated_mapeventIDs_codec);
		if (MapImage.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(MapImage);
		}
		if (MapSceneImage.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(MapSceneImage);
		}
		if (TutorialId != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(TutorialId);
		}
		mapSpecialCard_.WriteTo(ref output, _repeated_mapSpecialCard_codec);
		mapSpecialEvent_.WriteTo(ref output, _repeated_mapSpecialEvent_codec);
		if (SpLand != LandType.None)
		{
			output.WriteRawTag(104);
			output.WriteEnum((int)SpLand);
		}
		mapSpecialRelic_.WriteTo(ref output, _repeated_mapSpecialRelic_codec);
		preloadCharacterIds_.WriteTo(ref output, _repeated_preloadCharacterIds_codec);
		storyIds_.WriteTo(ref output, _repeated_storyIds_codec);
		if (beginTime_ != null)
		{
			output.WriteRawTag(138, 1);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(146, 1);
			output.WriteMessage(EndTime);
		}
		if (beginTimeMap_ != null)
		{
			output.WriteRawTag(154, 1);
			output.WriteMessage(BeginTimeMap);
		}
		if (endTimeMap_ != null)
		{
			output.WriteRawTag(162, 1);
			output.WriteMessage(EndTimeMap);
		}
		if (LabelType != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(LabelType);
		}
		if (MapMark != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(MapMark);
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
		if (MapName != 0)
		{
			num += 5;
		}
		if (MapDescription != 0)
		{
			num += 5;
		}
		if (MapGallery != 0)
		{
			num += 5;
		}
		num += mids_.CalculateSize(_repeated_mids_codec);
		num += difficultyIds_.CalculateSize(_repeated_difficultyIds_codec);
		num += mapeventIDs_.CalculateSize(_repeated_mapeventIDs_codec);
		if (MapImage.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MapImage);
		}
		if (MapSceneImage.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MapSceneImage);
		}
		if (TutorialId != 0)
		{
			num += 5;
		}
		num += mapSpecialCard_.CalculateSize(_repeated_mapSpecialCard_codec);
		num += mapSpecialEvent_.CalculateSize(_repeated_mapSpecialEvent_codec);
		if (SpLand != LandType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SpLand);
		}
		num += mapSpecialRelic_.CalculateSize(_repeated_mapSpecialRelic_codec);
		num += preloadCharacterIds_.CalculateSize(_repeated_preloadCharacterIds_codec);
		num += storyIds_.CalculateSize(_repeated_storyIds_codec);
		if (beginTime_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (beginTimeMap_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BeginTimeMap);
		}
		if (endTimeMap_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(EndTimeMap);
		}
		if (LabelType != 0)
		{
			num += 6;
		}
		if (MapMark != 0)
		{
			num += 6;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.MapName != 0)
		{
			MapName = other.MapName;
		}
		if (other.MapDescription != 0)
		{
			MapDescription = other.MapDescription;
		}
		if (other.MapGallery != 0)
		{
			MapGallery = other.MapGallery;
		}
		mids_.Add(other.mids_);
		difficultyIds_.Add(other.difficultyIds_);
		mapeventIDs_.Add(other.mapeventIDs_);
		if (other.MapImage.Length != 0)
		{
			MapImage = other.MapImage;
		}
		if (other.MapSceneImage.Length != 0)
		{
			MapSceneImage = other.MapSceneImage;
		}
		if (other.TutorialId != 0)
		{
			TutorialId = other.TutorialId;
		}
		mapSpecialCard_.Add(other.mapSpecialCard_);
		mapSpecialEvent_.Add(other.mapSpecialEvent_);
		if (other.SpLand != LandType.None)
		{
			SpLand = other.SpLand;
		}
		mapSpecialRelic_.Add(other.mapSpecialRelic_);
		preloadCharacterIds_.Add(other.preloadCharacterIds_);
		storyIds_.Add(other.storyIds_);
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		if (other.beginTimeMap_ != null)
		{
			if (beginTimeMap_ == null)
			{
				BeginTimeMap = new Timestamp();
			}
			BeginTimeMap.MergeFrom(other.BeginTimeMap);
		}
		if (other.endTimeMap_ != null)
		{
			if (endTimeMap_ == null)
			{
				EndTimeMap = new Timestamp();
			}
			EndTimeMap.MergeFrom(other.EndTimeMap);
		}
		if (other.LabelType != 0)
		{
			LabelType = other.LabelType;
		}
		if (other.MapMark != 0)
		{
			MapMark = other.MapMark;
		}
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 21u:
				MapName = input.ReadSFixed32();
				break;
			case 29u:
				MapDescription = input.ReadSFixed32();
				break;
			case 37u:
				MapGallery = input.ReadSFixed32();
				break;
			case 40u:
			case 42u:
				mids_.AddEntriesFrom(ref input, _repeated_mids_codec);
				break;
			case 50u:
			case 53u:
				difficultyIds_.AddEntriesFrom(ref input, _repeated_difficultyIds_codec);
				break;
			case 58u:
			case 61u:
				mapeventIDs_.AddEntriesFrom(ref input, _repeated_mapeventIDs_codec);
				break;
			case 66u:
				MapImage = input.ReadString();
				break;
			case 74u:
				MapSceneImage = input.ReadString();
				break;
			case 85u:
				TutorialId = input.ReadSFixed32();
				break;
			case 90u:
			case 93u:
				mapSpecialCard_.AddEntriesFrom(ref input, _repeated_mapSpecialCard_codec);
				break;
			case 98u:
			case 101u:
				mapSpecialEvent_.AddEntriesFrom(ref input, _repeated_mapSpecialEvent_codec);
				break;
			case 104u:
				SpLand = (LandType)input.ReadEnum();
				break;
			case 114u:
			case 117u:
				mapSpecialRelic_.AddEntriesFrom(ref input, _repeated_mapSpecialRelic_codec);
				break;
			case 122u:
			case 125u:
				preloadCharacterIds_.AddEntriesFrom(ref input, _repeated_preloadCharacterIds_codec);
				break;
			case 130u:
			case 133u:
				storyIds_.AddEntriesFrom(ref input, _repeated_storyIds_codec);
				break;
			case 138u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 146u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 154u:
				if (beginTimeMap_ == null)
				{
					BeginTimeMap = new Timestamp();
				}
				input.ReadMessage(BeginTimeMap);
				break;
			case 162u:
				if (endTimeMap_ == null)
				{
					EndTimeMap = new Timestamp();
				}
				input.ReadMessage(EndTimeMap);
				break;
			case 173u:
				LabelType = input.ReadSFixed32();
				break;
			case 181u:
				MapMark = input.ReadSFixed32();
				break;
			}
		}
	}

	public List<int> GetPreloadCharacterIdsInRoom(int difficultyId, int difficultyIndex)
	{
		_PreloadCharacterIds = new List<int>();
		_PreloadCharacterIds.AddRange(preloadCharacterIds_);
		if (difficultyId != 0)
		{
			RepeatedField<MapGameDifficultyConfigureItem> mapGameDifficultyItems = difficultyId.GetMapGameDifficultyItems();
			for (int i = 0; i < mapGameDifficultyItems.Count; i++)
			{
				if (mapGameDifficultyItems[i].Index == difficultyIndex)
				{
					_PreloadCharacterIds.AddRange(mapGameDifficultyItems[i].PreloadCharacterIds);
				}
			}
		}
		return _PreloadCharacterIds;
	}
}
