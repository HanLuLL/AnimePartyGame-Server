using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class CollaborationInfoConfigure : IMessage<CollaborationInfoConfigure>, IMessage, IEquatable<CollaborationInfoConfigure>, IDeepCloneable<CollaborationInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<CollaborationInfoConfigure> _parser = new MessageParser<CollaborationInfoConfigure>(() => new CollaborationInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int UIWindowTypeFieldNumber = 2;

	private UIWindowType uIWindowType_;

	public const int BeginTimeFieldNumber = 3;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 4;

	private Timestamp endTime_;

	public const int WayFieldNumber = 5;

	private int way_;

	public const int PreviewIndexFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_previewIndex_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> previewIndex_ = new RepeatedField<int>();

	public const int EntranceFieldNumber = 7;

	private string entrance_ = "";

	public const int EntranceTitleFieldNumber = 8;

	private int entranceTitle_;

	public const int HeroIDFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_heroID_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> heroID_ = new RepeatedField<int>();

	public const int BgListFieldNumber = 10;

	private static readonly FieldCodec<string> _repeated_bgList_codec = FieldCodec.ForString(82u);

	private readonly RepeatedField<string> bgList_ = new RepeatedField<string>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CollaborationInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CollaborationReflection.Descriptor.MessageTypes[0];

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
	public UIWindowType UIWindowType
	{
		get
		{
			return uIWindowType_;
		}
		private set
		{
			uIWindowType_ = value;
		}
	}

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
	public int Way
	{
		get
		{
			return way_;
		}
		private set
		{
			way_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PreviewIndex => previewIndex_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Entrance
	{
		get
		{
			return entrance_;
		}
		private set
		{
			entrance_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EntranceTitle
	{
		get
		{
			return entranceTitle_;
		}
		private set
		{
			entranceTitle_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> HeroID => heroID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> BgList => bgList_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationInfoConfigure(CollaborationInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		uIWindowType_ = other.uIWindowType_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		way_ = other.way_;
		previewIndex_ = other.previewIndex_.Clone();
		entrance_ = other.entrance_;
		entranceTitle_ = other.entranceTitle_;
		heroID_ = other.heroID_.Clone();
		bgList_ = other.bgList_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CollaborationInfoConfigure Clone()
	{
		return new CollaborationInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CollaborationInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CollaborationInfoConfigure other)
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
		if (UIWindowType != other.UIWindowType)
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
		if (Way != other.Way)
		{
			return false;
		}
		if (!previewIndex_.Equals(other.previewIndex_))
		{
			return false;
		}
		if (Entrance != other.Entrance)
		{
			return false;
		}
		if (EntranceTitle != other.EntranceTitle)
		{
			return false;
		}
		if (!heroID_.Equals(other.heroID_))
		{
			return false;
		}
		if (!bgList_.Equals(other.bgList_))
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
		if (UIWindowType != UIWindowType.None)
		{
			num ^= UIWindowType.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
		}
		num ^= previewIndex_.GetHashCode();
		if (Entrance.Length != 0)
		{
			num ^= Entrance.GetHashCode();
		}
		if (EntranceTitle != 0)
		{
			num ^= EntranceTitle.GetHashCode();
		}
		num ^= heroID_.GetHashCode();
		num ^= bgList_.GetHashCode();
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
		if (UIWindowType != UIWindowType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)UIWindowType);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(EndTime);
		}
		if (Way != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Way);
		}
		previewIndex_.WriteTo(ref output, _repeated_previewIndex_codec);
		if (Entrance.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(Entrance);
		}
		if (EntranceTitle != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(EntranceTitle);
		}
		heroID_.WriteTo(ref output, _repeated_heroID_codec);
		bgList_.WriteTo(ref output, _repeated_bgList_codec);
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
		if (UIWindowType != UIWindowType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)UIWindowType);
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (Way != 0)
		{
			num += 5;
		}
		num += previewIndex_.CalculateSize(_repeated_previewIndex_codec);
		if (Entrance.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Entrance);
		}
		if (EntranceTitle != 0)
		{
			num += 5;
		}
		num += heroID_.CalculateSize(_repeated_heroID_codec);
		num += bgList_.CalculateSize(_repeated_bgList_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CollaborationInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.UIWindowType != UIWindowType.None)
		{
			UIWindowType = other.UIWindowType;
		}
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
		if (other.Way != 0)
		{
			Way = other.Way;
		}
		previewIndex_.Add(other.previewIndex_);
		if (other.Entrance.Length != 0)
		{
			Entrance = other.Entrance;
		}
		if (other.EntranceTitle != 0)
		{
			EntranceTitle = other.EntranceTitle;
		}
		heroID_.Add(other.heroID_);
		bgList_.Add(other.bgList_);
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
			case 16u:
				UIWindowType = (UIWindowType)input.ReadEnum();
				break;
			case 26u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 34u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 45u:
				Way = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				previewIndex_.AddEntriesFrom(ref input, _repeated_previewIndex_codec);
				break;
			case 58u:
				Entrance = input.ReadString();
				break;
			case 69u:
				EntranceTitle = input.ReadSFixed32();
				break;
			case 74u:
			case 77u:
				heroID_.AddEntriesFrom(ref input, _repeated_heroID_codec);
				break;
			case 82u:
				bgList_.AddEntriesFrom(ref input, _repeated_bgList_codec);
				break;
			}
		}
	}

	public void FixTime(FixCollaborationInfoConfigure data)
	{
		beginTime_ = data.BeginTime;
		endTime_ = data.EndTime;
	}
}
