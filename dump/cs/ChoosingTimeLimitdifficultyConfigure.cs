using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class ChoosingTimeLimitdifficultyConfigure : IMessage<ChoosingTimeLimitdifficultyConfigure>, IMessage, IEquatable<ChoosingTimeLimitdifficultyConfigure>, IDeepCloneable<ChoosingTimeLimitdifficultyConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChoosingTimeLimitdifficultyConfigure> _parser = new MessageParser<ChoosingTimeLimitdifficultyConfigure>(() => new ChoosingTimeLimitdifficultyConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GameDifficultyTypeFieldNumber = 1;

	private GameDifficultyType gameDifficultyType_;

	public const int IsDefaultFieldNumber = 2;

	private bool isDefault_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	public const int TipsIDFieldNumber = 4;

	private int tipsID_;

	public const int WarningIDFieldNumber = 5;

	private int warningID_;

	public const int PicFieldNumber = 6;

	private string pic_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChoosingTimeLimitdifficultyConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChoosingTimeLimitReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameDifficultyType GameDifficultyType
	{
		get
		{
			return gameDifficultyType_;
		}
		private set
		{
			gameDifficultyType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDefault
	{
		get
		{
			return isDefault_;
		}
		private set
		{
			isDefault_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescriptionID
	{
		get
		{
			return descriptionID_;
		}
		private set
		{
			descriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TipsID
	{
		get
		{
			return tipsID_;
		}
		private set
		{
			tipsID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WarningID
	{
		get
		{
			return warningID_;
		}
		private set
		{
			warningID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Pic
	{
		get
		{
			return pic_;
		}
		private set
		{
			pic_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitdifficultyConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitdifficultyConfigure(ChoosingTimeLimitdifficultyConfigure other)
		: this()
	{
		gameDifficultyType_ = other.gameDifficultyType_;
		isDefault_ = other.isDefault_;
		descriptionID_ = other.descriptionID_;
		tipsID_ = other.tipsID_;
		warningID_ = other.warningID_;
		pic_ = other.pic_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoosingTimeLimitdifficultyConfigure Clone()
	{
		return new ChoosingTimeLimitdifficultyConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChoosingTimeLimitdifficultyConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChoosingTimeLimitdifficultyConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GameDifficultyType != other.GameDifficultyType)
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (TipsID != other.TipsID)
		{
			return false;
		}
		if (WarningID != other.WarningID)
		{
			return false;
		}
		if (Pic != other.Pic)
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
		if (GameDifficultyType != GameDifficultyType.Easy)
		{
			num ^= GameDifficultyType.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (TipsID != 0)
		{
			num ^= TipsID.GetHashCode();
		}
		if (WarningID != 0)
		{
			num ^= WarningID.GetHashCode();
		}
		if (Pic.Length != 0)
		{
			num ^= Pic.GetHashCode();
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
		if (GameDifficultyType != GameDifficultyType.Easy)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)GameDifficultyType);
		}
		if (IsDefault)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsDefault);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescriptionID);
		}
		if (TipsID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TipsID);
		}
		if (WarningID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(WarningID);
		}
		if (Pic.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Pic);
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
		if (GameDifficultyType != GameDifficultyType.Easy)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GameDifficultyType);
		}
		if (IsDefault)
		{
			num += 2;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (TipsID != 0)
		{
			num += 5;
		}
		if (WarningID != 0)
		{
			num += 5;
		}
		if (Pic.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pic);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChoosingTimeLimitdifficultyConfigure other)
	{
		if (other != null)
		{
			if (other.GameDifficultyType != GameDifficultyType.Easy)
			{
				GameDifficultyType = other.GameDifficultyType;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.TipsID != 0)
			{
				TipsID = other.TipsID;
			}
			if (other.WarningID != 0)
			{
				WarningID = other.WarningID;
			}
			if (other.Pic.Length != 0)
			{
				Pic = other.Pic;
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
			case 8u:
				GameDifficultyType = (GameDifficultyType)input.ReadEnum();
				break;
			case 16u:
				IsDefault = input.ReadBool();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 37u:
				TipsID = input.ReadSFixed32();
				break;
			case 45u:
				WarningID = input.ReadSFixed32();
				break;
			case 50u:
				Pic = input.ReadString();
				break;
			}
		}
	}
}
