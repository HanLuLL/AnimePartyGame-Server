using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class DiceActivityDataConfigureItem : IMessage<DiceActivityDataConfigureItem>, IMessage, IEquatable<DiceActivityDataConfigureItem>, IDeepCloneable<DiceActivityDataConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<DiceActivityDataConfigureItem> _parser = new MessageParser<DiceActivityDataConfigureItem>(() => new DiceActivityDataConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int DiceLandIDFieldNumber = 1;

	private int diceLandID_;

	public const int LandTypeFieldNumber = 2;

	private DiceLandType landType_;

	public const int IconFieldNumber = 3;

	private string icon_ = "";

	public const int RewardFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DiceActivityDataConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DiceActivityReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceLandID
	{
		get
		{
			return diceLandID_;
		}
		private set
		{
			diceLandID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceLandType LandType
	{
		get
		{
			return landType_;
		}
		private set
		{
			landType_ = value;
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
	public MapField<int, int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityDataConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityDataConfigureItem(DiceActivityDataConfigureItem other)
		: this()
	{
		diceLandID_ = other.diceLandID_;
		landType_ = other.landType_;
		icon_ = other.icon_;
		reward_ = other.reward_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityDataConfigureItem Clone()
	{
		return new DiceActivityDataConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DiceActivityDataConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DiceActivityDataConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DiceLandID != other.DiceLandID)
		{
			return false;
		}
		if (LandType != other.LandType)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
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
		if (DiceLandID != 0)
		{
			num ^= DiceLandID.GetHashCode();
		}
		if (LandType != DiceLandType.None)
		{
			num ^= LandType.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		num ^= Reward.GetHashCode();
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
		if (DiceLandID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DiceLandID);
		}
		if (LandType != DiceLandType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)LandType);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Icon);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
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
		if (DiceLandID != 0)
		{
			num += 5;
		}
		if (LandType != DiceLandType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)LandType);
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		num += reward_.CalculateSize(_map_reward_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DiceActivityDataConfigureItem other)
	{
		if (other != null)
		{
			if (other.DiceLandID != 0)
			{
				DiceLandID = other.DiceLandID;
			}
			if (other.LandType != DiceLandType.None)
			{
				LandType = other.LandType;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			reward_.MergeFrom(other.reward_);
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
				DiceLandID = input.ReadSFixed32();
				break;
			case 16u:
				LandType = (DiceLandType)input.ReadEnum();
				break;
			case 26u:
				Icon = input.ReadString();
				break;
			case 34u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			}
		}
	}
}
