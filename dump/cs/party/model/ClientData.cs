using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ClientData : IMessage<ClientData>, IMessage, IEquatable<ClientData>, IDeepCloneable<ClientData>, IBufferMessage
{
	private static readonly MessageParser<ClientData> _parser = new MessageParser<ClientData>(() => new ClientData());

	private UnknownFieldSet _unknownFields;

	public const int GuideDataFieldNumber = 1;

	private static readonly MapField<int, int>.Codec _map_guideData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 10u);

	private readonly MapField<int, int> guideData_ = new MapField<int, int>();

	public const int DateChangeDataFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_dateChangeData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> dateChangeData_ = new MapField<int, int>();

	public const int SongDataFieldNumber = 3;

	private static readonly MapField<int, SongData>.Codec _map_songData_codec = new MapField<int, SongData>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.SongData.Parser), 26u);

	private readonly MapField<int, SongData> songData_ = new MapField<int, SongData>();

	public const int SettingDataFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_settingData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> settingData_ = new MapField<int, int>();

	public const int NewItemDataFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_newItemData_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> newItemData_ = new RepeatedField<int>();

	public const int CampaignTutorialDataFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_campaignTutorialData_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> campaignTutorialData_ = new RepeatedField<int>();

	public const int StarExpressionFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_starExpression_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> starExpression_ = new RepeatedField<int>();

	public const int TopExpressionFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_topExpression_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> topExpression_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ClientData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[21];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> GuideData => guideData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> DateChangeData => dateChangeData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SongData> SongData => songData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SettingData => settingData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> NewItemData => newItemData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CampaignTutorialData => campaignTutorialData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> StarExpression => starExpression_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TopExpression => topExpression_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientData(ClientData other)
		: this()
	{
		guideData_ = other.guideData_.Clone();
		dateChangeData_ = other.dateChangeData_.Clone();
		songData_ = other.songData_.Clone();
		settingData_ = other.settingData_.Clone();
		newItemData_ = other.newItemData_.Clone();
		campaignTutorialData_ = other.campaignTutorialData_.Clone();
		starExpression_ = other.starExpression_.Clone();
		topExpression_ = other.topExpression_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientData Clone()
	{
		return new ClientData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ClientData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ClientData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!GuideData.Equals(other.GuideData))
		{
			return false;
		}
		if (!DateChangeData.Equals(other.DateChangeData))
		{
			return false;
		}
		if (!SongData.Equals(other.SongData))
		{
			return false;
		}
		if (!SettingData.Equals(other.SettingData))
		{
			return false;
		}
		if (!newItemData_.Equals(other.newItemData_))
		{
			return false;
		}
		if (!campaignTutorialData_.Equals(other.campaignTutorialData_))
		{
			return false;
		}
		if (!starExpression_.Equals(other.starExpression_))
		{
			return false;
		}
		if (!topExpression_.Equals(other.topExpression_))
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
		num ^= GuideData.GetHashCode();
		num ^= DateChangeData.GetHashCode();
		num ^= SongData.GetHashCode();
		num ^= SettingData.GetHashCode();
		num ^= newItemData_.GetHashCode();
		num ^= campaignTutorialData_.GetHashCode();
		num ^= starExpression_.GetHashCode();
		num ^= topExpression_.GetHashCode();
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
		guideData_.WriteTo(ref output, _map_guideData_codec);
		dateChangeData_.WriteTo(ref output, _map_dateChangeData_codec);
		songData_.WriteTo(ref output, _map_songData_codec);
		settingData_.WriteTo(ref output, _map_settingData_codec);
		newItemData_.WriteTo(ref output, _repeated_newItemData_codec);
		campaignTutorialData_.WriteTo(ref output, _repeated_campaignTutorialData_codec);
		starExpression_.WriteTo(ref output, _repeated_starExpression_codec);
		topExpression_.WriteTo(ref output, _repeated_topExpression_codec);
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
		num += guideData_.CalculateSize(_map_guideData_codec);
		num += dateChangeData_.CalculateSize(_map_dateChangeData_codec);
		num += songData_.CalculateSize(_map_songData_codec);
		num += settingData_.CalculateSize(_map_settingData_codec);
		num += newItemData_.CalculateSize(_repeated_newItemData_codec);
		num += campaignTutorialData_.CalculateSize(_repeated_campaignTutorialData_codec);
		num += starExpression_.CalculateSize(_repeated_starExpression_codec);
		num += topExpression_.CalculateSize(_repeated_topExpression_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ClientData other)
	{
		if (other != null)
		{
			guideData_.MergeFrom(other.guideData_);
			dateChangeData_.MergeFrom(other.dateChangeData_);
			songData_.MergeFrom(other.songData_);
			settingData_.MergeFrom(other.settingData_);
			newItemData_.Add(other.newItemData_);
			campaignTutorialData_.Add(other.campaignTutorialData_);
			starExpression_.Add(other.starExpression_);
			topExpression_.Add(other.topExpression_);
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
				guideData_.AddEntriesFrom(ref input, _map_guideData_codec);
				break;
			case 18u:
				dateChangeData_.AddEntriesFrom(ref input, _map_dateChangeData_codec);
				break;
			case 26u:
				songData_.AddEntriesFrom(ref input, _map_songData_codec);
				break;
			case 34u:
				settingData_.AddEntriesFrom(ref input, _map_settingData_codec);
				break;
			case 42u:
			case 45u:
				newItemData_.AddEntriesFrom(ref input, _repeated_newItemData_codec);
				break;
			case 50u:
			case 53u:
				campaignTutorialData_.AddEntriesFrom(ref input, _repeated_campaignTutorialData_codec);
				break;
			case 58u:
			case 61u:
				starExpression_.AddEntriesFrom(ref input, _repeated_starExpression_codec);
				break;
			case 66u:
			case 69u:
				topExpression_.AddEntriesFrom(ref input, _repeated_topExpression_codec);
				break;
			}
		}
	}
}
