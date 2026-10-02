using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class MapEventInfoConfigure : IMessage<MapEventInfoConfigure>, IMessage, IEquatable<MapEventInfoConfigure>, IDeepCloneable<MapEventInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapEventInfoConfigure> _parser = new MessageParser<MapEventInfoConfigure>(() => new MapEventInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int CardIDFieldNumber = 2;

	private int cardID_;

	public const int IsHideFieldNumber = 3;

	private bool isHide_;

	public const int TriggerparamsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_triggerparams_codec = FieldCodec.ForSInt32(34u);

	private readonly RepeatedField<int> triggerparams_ = new RepeatedField<int>();

	public const int TriggerLandUnitsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_triggerLandUnits_codec = FieldCodec.ForSInt32(42u);

	private readonly RepeatedField<int> triggerLandUnits_ = new RepeatedField<int>();

	public const int MapEventEffectConfigsFieldNumber = 6;

	private static readonly MapField<int, MapEventInfoConfigureMapEventEffectConfigss>.Codec _map_mapEventEffectConfigs_codec = new MapField<int, MapEventInfoConfigureMapEventEffectConfigss>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapEventInfoConfigureMapEventEffectConfigss.Parser), 50u);

	private readonly MapField<int, MapEventInfoConfigureMapEventEffectConfigss> mapEventEffectConfigs_ = new MapField<int, MapEventInfoConfigureMapEventEffectConfigss>();

	public const int Params1FieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_params1_codec = FieldCodec.ForSInt32(58u);

	private readonly RepeatedField<int> params1_ = new RepeatedField<int>();

	public const int Params2FieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_params2_codec = FieldCodec.ForSInt32(66u);

	private readonly RepeatedField<int> params2_ = new RepeatedField<int>();

	public const int Params3FieldNumber = 9;

	private string params3_ = "";

	public const int BuffIdsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	public const int Perform1FieldNumber = 11;

	private int perform1_;

	public const int Perform2FieldNumber = 12;

	private int perform2_;

	private MapEventMapEventCardConfigure _mapEventCardConfigure;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapEventInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapEventReflection.Descriptor.MessageTypes[0];

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
	public int CardID
	{
		get
		{
			return cardID_;
		}
		private set
		{
			cardID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsHide
	{
		get
		{
			return isHide_;
		}
		private set
		{
			isHide_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Triggerparams => triggerparams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TriggerLandUnits => triggerLandUnits_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapEventInfoConfigureMapEventEffectConfigss> MapEventEffectConfigs => mapEventEffectConfigs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params1 => params1_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params2 => params2_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Params3
	{
		get
		{
			return params3_;
		}
		private set
		{
			params3_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffIds => buffIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Perform1
	{
		get
		{
			return perform1_;
		}
		private set
		{
			perform1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Perform2
	{
		get
		{
			return perform2_;
		}
		private set
		{
			perform2_ = value;
		}
	}

	public MapEventMapEventCardConfigure MapEventCardConfigure
	{
		get
		{
			if (_mapEventCardConfigure == null && !StaticConfigure.MapEvent.MapEventCardDict.TryGetValue(CardID, out _mapEventCardConfigure))
			{
				Debug.LogError("在MapEvent.MapEventCardDict表里并没有找到事件卡id：" + CardID);
			}
			return _mapEventCardConfigure;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventInfoConfigure(MapEventInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		cardID_ = other.cardID_;
		isHide_ = other.isHide_;
		triggerparams_ = other.triggerparams_.Clone();
		triggerLandUnits_ = other.triggerLandUnits_.Clone();
		mapEventEffectConfigs_ = other.mapEventEffectConfigs_.Clone();
		params1_ = other.params1_.Clone();
		params2_ = other.params2_.Clone();
		params3_ = other.params3_;
		buffIds_ = other.buffIds_.Clone();
		perform1_ = other.perform1_;
		perform2_ = other.perform2_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventInfoConfigure Clone()
	{
		return new MapEventInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapEventInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapEventInfoConfigure other)
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
		if (CardID != other.CardID)
		{
			return false;
		}
		if (IsHide != other.IsHide)
		{
			return false;
		}
		if (!triggerparams_.Equals(other.triggerparams_))
		{
			return false;
		}
		if (!triggerLandUnits_.Equals(other.triggerLandUnits_))
		{
			return false;
		}
		if (!MapEventEffectConfigs.Equals(other.MapEventEffectConfigs))
		{
			return false;
		}
		if (!params1_.Equals(other.params1_))
		{
			return false;
		}
		if (!params2_.Equals(other.params2_))
		{
			return false;
		}
		if (Params3 != other.Params3)
		{
			return false;
		}
		if (!buffIds_.Equals(other.buffIds_))
		{
			return false;
		}
		if (Perform1 != other.Perform1)
		{
			return false;
		}
		if (Perform2 != other.Perform2)
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
		if (CardID != 0)
		{
			num ^= CardID.GetHashCode();
		}
		if (IsHide)
		{
			num ^= IsHide.GetHashCode();
		}
		num ^= triggerparams_.GetHashCode();
		num ^= triggerLandUnits_.GetHashCode();
		num ^= MapEventEffectConfigs.GetHashCode();
		num ^= params1_.GetHashCode();
		num ^= params2_.GetHashCode();
		if (Params3.Length != 0)
		{
			num ^= Params3.GetHashCode();
		}
		num ^= buffIds_.GetHashCode();
		if (Perform1 != 0)
		{
			num ^= Perform1.GetHashCode();
		}
		if (Perform2 != 0)
		{
			num ^= Perform2.GetHashCode();
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
		if (CardID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CardID);
		}
		if (IsHide)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsHide);
		}
		triggerparams_.WriteTo(ref output, _repeated_triggerparams_codec);
		triggerLandUnits_.WriteTo(ref output, _repeated_triggerLandUnits_codec);
		mapEventEffectConfigs_.WriteTo(ref output, _map_mapEventEffectConfigs_codec);
		params1_.WriteTo(ref output, _repeated_params1_codec);
		params2_.WriteTo(ref output, _repeated_params2_codec);
		if (Params3.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(Params3);
		}
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
		if (Perform1 != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Perform1);
		}
		if (Perform2 != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(Perform2);
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
		if (CardID != 0)
		{
			num += 5;
		}
		if (IsHide)
		{
			num += 2;
		}
		num += triggerparams_.CalculateSize(_repeated_triggerparams_codec);
		num += triggerLandUnits_.CalculateSize(_repeated_triggerLandUnits_codec);
		num += mapEventEffectConfigs_.CalculateSize(_map_mapEventEffectConfigs_codec);
		num += params1_.CalculateSize(_repeated_params1_codec);
		num += params2_.CalculateSize(_repeated_params2_codec);
		if (Params3.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Params3);
		}
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		if (Perform1 != 0)
		{
			num += 5;
		}
		if (Perform2 != 0)
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
	public void MergeFrom(MapEventInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.CardID != 0)
			{
				CardID = other.CardID;
			}
			if (other.IsHide)
			{
				IsHide = other.IsHide;
			}
			triggerparams_.Add(other.triggerparams_);
			triggerLandUnits_.Add(other.triggerLandUnits_);
			mapEventEffectConfigs_.MergeFrom(other.mapEventEffectConfigs_);
			params1_.Add(other.params1_);
			params2_.Add(other.params2_);
			if (other.Params3.Length != 0)
			{
				Params3 = other.Params3;
			}
			buffIds_.Add(other.buffIds_);
			if (other.Perform1 != 0)
			{
				Perform1 = other.Perform1;
			}
			if (other.Perform2 != 0)
			{
				Perform2 = other.Perform2;
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
				CardID = input.ReadSFixed32();
				break;
			case 24u:
				IsHide = input.ReadBool();
				break;
			case 32u:
			case 34u:
				triggerparams_.AddEntriesFrom(ref input, _repeated_triggerparams_codec);
				break;
			case 40u:
			case 42u:
				triggerLandUnits_.AddEntriesFrom(ref input, _repeated_triggerLandUnits_codec);
				break;
			case 50u:
				mapEventEffectConfigs_.AddEntriesFrom(ref input, _map_mapEventEffectConfigs_codec);
				break;
			case 56u:
			case 58u:
				params1_.AddEntriesFrom(ref input, _repeated_params1_codec);
				break;
			case 64u:
			case 66u:
				params2_.AddEntriesFrom(ref input, _repeated_params2_codec);
				break;
			case 74u:
				Params3 = input.ReadString();
				break;
			case 82u:
			case 85u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			case 93u:
				Perform1 = input.ReadSFixed32();
				break;
			case 101u:
				Perform2 = input.ReadSFixed32();
				break;
			}
		}
	}
}
