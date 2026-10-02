using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class ActivityActivityEntrance2Configure : IMessage<ActivityActivityEntrance2Configure>, IMessage, IEquatable<ActivityActivityEntrance2Configure>, IDeepCloneable<ActivityActivityEntrance2Configure>, IBufferMessage
{
	private static readonly MessageParser<ActivityActivityEntrance2Configure> _parser = new MessageParser<ActivityActivityEntrance2Configure>(() => new ActivityActivityEntrance2Configure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int LocationNumbFieldNumber = 2;

	private int locationNumb_;

	public const int ActivityHubOrderFieldNumber = 3;

	private int activityHubOrder_;

	public const int ActivityIDFieldNumber = 4;

	private int activityID_;

	public const int IconFieldNumber = 5;

	private string icon_ = "";

	public const int TitleFieldNumber = 6;

	private int title_;

	public const int LanguageTypeFieldNumber = 7;

	private static readonly FieldCodec<LanguageType> _repeated_languageType_codec = FieldCodec.ForEnum(58u, (LanguageType x) => (int)x, (int x) => (LanguageType)x);

	private readonly RepeatedField<LanguageType> languageType_ = new RepeatedField<LanguageType>();

	private ActivityInfoConfigure _InfoConfig;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityActivityEntrance2Configure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[0];

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
	public int LocationNumb
	{
		get
		{
			return locationNumb_;
		}
		private set
		{
			locationNumb_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActivityHubOrder
	{
		get
		{
			return activityHubOrder_;
		}
		private set
		{
			activityHubOrder_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActivityID
	{
		get
		{
			return activityID_;
		}
		private set
		{
			activityID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Title
	{
		get
		{
			return title_;
		}
		private set
		{
			title_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<LanguageType> LanguageType => languageType_;

	public ActivityInfoConfigure InfoConfig
	{
		get
		{
			if (_InfoConfig == null && !StaticConfigure.Activity.InfoDict.TryGetValue(activityID_, out _InfoConfig))
			{
				Debug.LogError($"无法从Activity.InfoDict中取出ID:{activityID_}的数据");
			}
			return _InfoConfig;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityActivityEntrance2Configure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityActivityEntrance2Configure(ActivityActivityEntrance2Configure other)
		: this()
	{
		id_ = other.id_;
		locationNumb_ = other.locationNumb_;
		activityHubOrder_ = other.activityHubOrder_;
		activityID_ = other.activityID_;
		icon_ = other.icon_;
		title_ = other.title_;
		languageType_ = other.languageType_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityActivityEntrance2Configure Clone()
	{
		return new ActivityActivityEntrance2Configure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityActivityEntrance2Configure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityActivityEntrance2Configure other)
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
		if (LocationNumb != other.LocationNumb)
		{
			return false;
		}
		if (ActivityHubOrder != other.ActivityHubOrder)
		{
			return false;
		}
		if (ActivityID != other.ActivityID)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (Title != other.Title)
		{
			return false;
		}
		if (!languageType_.Equals(other.languageType_))
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
		if (LocationNumb != 0)
		{
			num ^= LocationNumb.GetHashCode();
		}
		if (ActivityHubOrder != 0)
		{
			num ^= ActivityHubOrder.GetHashCode();
		}
		if (ActivityID != 0)
		{
			num ^= ActivityID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (Title != 0)
		{
			num ^= Title.GetHashCode();
		}
		num ^= languageType_.GetHashCode();
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
		if (LocationNumb != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(LocationNumb);
		}
		if (ActivityHubOrder != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ActivityHubOrder);
		}
		if (ActivityID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ActivityID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Icon);
		}
		if (Title != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Title);
		}
		languageType_.WriteTo(ref output, _repeated_languageType_codec);
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
		if (LocationNumb != 0)
		{
			num += 5;
		}
		if (ActivityHubOrder != 0)
		{
			num += 5;
		}
		if (ActivityID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (Title != 0)
		{
			num += 5;
		}
		num += languageType_.CalculateSize(_repeated_languageType_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActivityActivityEntrance2Configure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.LocationNumb != 0)
			{
				LocationNumb = other.LocationNumb;
			}
			if (other.ActivityHubOrder != 0)
			{
				ActivityHubOrder = other.ActivityHubOrder;
			}
			if (other.ActivityID != 0)
			{
				ActivityID = other.ActivityID;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			if (other.Title != 0)
			{
				Title = other.Title;
			}
			languageType_.Add(other.languageType_);
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
				LocationNumb = input.ReadSFixed32();
				break;
			case 29u:
				ActivityHubOrder = input.ReadSFixed32();
				break;
			case 37u:
				ActivityID = input.ReadSFixed32();
				break;
			case 42u:
				Icon = input.ReadString();
				break;
			case 53u:
				Title = input.ReadSFixed32();
				break;
			case 56u:
			case 58u:
				languageType_.AddEntriesFrom(ref input, _repeated_languageType_codec);
				break;
			}
		}
	}
}
