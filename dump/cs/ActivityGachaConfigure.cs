using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class ActivityGachaConfigure : IMessage<ActivityGachaConfigure>, IMessage, IEquatable<ActivityGachaConfigure>, IDeepCloneable<ActivityGachaConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityGachaConfigure> _parser = new MessageParser<ActivityGachaConfigure>(() => new ActivityGachaConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int GachaTypeFieldNumber = 2;

	private GachaType gachaType_;

	public const int SkinIDFieldNumber = 3;

	private int skinID_;

	public const int GiftIDFieldNumber = 4;

	private int giftID_;

	public const int WayFieldNumber = 5;

	private int way_;

	private GachaBackstageConfigure _GachaBackstageConfig;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityGachaConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[7];

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
	public GachaType GachaType
	{
		get
		{
			return gachaType_;
		}
		private set
		{
			gachaType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinID
	{
		get
		{
			return skinID_;
		}
		private set
		{
			skinID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GiftID
	{
		get
		{
			return giftID_;
		}
		private set
		{
			giftID_ = value;
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

	public GachaBackstageConfigure GachaBackstageConfig
	{
		get
		{
			if (_GachaBackstageConfig == null && !StaticConfigure.Gacha.BackstageDict.TryGetValue((int)GachaType, out _GachaBackstageConfig))
			{
				Debug.LogError($"无法从Gacha.BackstageDict中取出ID:{GachaType}的数据");
			}
			return _GachaBackstageConfig;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityGachaConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityGachaConfigure(ActivityGachaConfigure other)
		: this()
	{
		id_ = other.id_;
		gachaType_ = other.gachaType_;
		skinID_ = other.skinID_;
		giftID_ = other.giftID_;
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityGachaConfigure Clone()
	{
		return new ActivityGachaConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityGachaConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityGachaConfigure other)
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
		if (GachaType != other.GachaType)
		{
			return false;
		}
		if (SkinID != other.SkinID)
		{
			return false;
		}
		if (GiftID != other.GiftID)
		{
			return false;
		}
		if (Way != other.Way)
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
		if (GachaType != GachaType.None)
		{
			num ^= GachaType.GetHashCode();
		}
		if (SkinID != 0)
		{
			num ^= SkinID.GetHashCode();
		}
		if (GiftID != 0)
		{
			num ^= GiftID.GetHashCode();
		}
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
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
		if (GachaType != GachaType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)GachaType);
		}
		if (SkinID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(SkinID);
		}
		if (GiftID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(GiftID);
		}
		if (Way != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Way);
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
		if (GachaType != GachaType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GachaType);
		}
		if (SkinID != 0)
		{
			num += 5;
		}
		if (GiftID != 0)
		{
			num += 5;
		}
		if (Way != 0)
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
	public void MergeFrom(ActivityGachaConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.GachaType != GachaType.None)
			{
				GachaType = other.GachaType;
			}
			if (other.SkinID != 0)
			{
				SkinID = other.SkinID;
			}
			if (other.GiftID != 0)
			{
				GiftID = other.GiftID;
			}
			if (other.Way != 0)
			{
				Way = other.Way;
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
			case 16u:
				GachaType = (GachaType)input.ReadEnum();
				break;
			case 29u:
				SkinID = input.ReadSFixed32();
				break;
			case 37u:
				GiftID = input.ReadSFixed32();
				break;
			case 45u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
