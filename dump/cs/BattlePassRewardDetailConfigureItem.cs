using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class BattlePassRewardDetailConfigureItem : IMessage<BattlePassRewardDetailConfigureItem>, IMessage, IEquatable<BattlePassRewardDetailConfigureItem>, IDeepCloneable<BattlePassRewardDetailConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<BattlePassRewardDetailConfigureItem> _parser = new MessageParser<BattlePassRewardDetailConfigureItem>(() => new BattlePassRewardDetailConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int NormalIconFieldNumber = 2;

	private string normalIcon_ = "";

	public const int NormalDescriptionFieldNumber = 3;

	private int normalDescription_;

	public const int IsNormalSpecialFieldNumber = 4;

	private bool isNormalSpecial_;

	public const int PremiumIconFieldNumber = 5;

	private string premiumIcon_ = "";

	public const int PremiumDescriptionFieldNumber = 6;

	private int premiumDescription_;

	public const int IsPremiumSpecialFieldNumber = 7;

	private bool isPremiumSpecial_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassRewardDetailConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[9];

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
	public string NormalIcon
	{
		get
		{
			return normalIcon_;
		}
		private set
		{
			normalIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NormalDescription
	{
		get
		{
			return normalDescription_;
		}
		private set
		{
			normalDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsNormalSpecial
	{
		get
		{
			return isNormalSpecial_;
		}
		private set
		{
			isNormalSpecial_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PremiumIcon
	{
		get
		{
			return premiumIcon_;
		}
		private set
		{
			premiumIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumDescription
	{
		get
		{
			return premiumDescription_;
		}
		private set
		{
			premiumDescription_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsPremiumSpecial
	{
		get
		{
			return isPremiumSpecial_;
		}
		private set
		{
			isPremiumSpecial_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardDetailConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardDetailConfigureItem(BattlePassRewardDetailConfigureItem other)
		: this()
	{
		index_ = other.index_;
		normalIcon_ = other.normalIcon_;
		normalDescription_ = other.normalDescription_;
		isNormalSpecial_ = other.isNormalSpecial_;
		premiumIcon_ = other.premiumIcon_;
		premiumDescription_ = other.premiumDescription_;
		isPremiumSpecial_ = other.isPremiumSpecial_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardDetailConfigureItem Clone()
	{
		return new BattlePassRewardDetailConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassRewardDetailConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassRewardDetailConfigureItem other)
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
		if (NormalIcon != other.NormalIcon)
		{
			return false;
		}
		if (NormalDescription != other.NormalDescription)
		{
			return false;
		}
		if (IsNormalSpecial != other.IsNormalSpecial)
		{
			return false;
		}
		if (PremiumIcon != other.PremiumIcon)
		{
			return false;
		}
		if (PremiumDescription != other.PremiumDescription)
		{
			return false;
		}
		if (IsPremiumSpecial != other.IsPremiumSpecial)
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
		if (NormalIcon.Length != 0)
		{
			num ^= NormalIcon.GetHashCode();
		}
		if (NormalDescription != 0)
		{
			num ^= NormalDescription.GetHashCode();
		}
		if (IsNormalSpecial)
		{
			num ^= IsNormalSpecial.GetHashCode();
		}
		if (PremiumIcon.Length != 0)
		{
			num ^= PremiumIcon.GetHashCode();
		}
		if (PremiumDescription != 0)
		{
			num ^= PremiumDescription.GetHashCode();
		}
		if (IsPremiumSpecial)
		{
			num ^= IsPremiumSpecial.GetHashCode();
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
		if (NormalIcon.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(NormalIcon);
		}
		if (NormalDescription != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NormalDescription);
		}
		if (IsNormalSpecial)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsNormalSpecial);
		}
		if (PremiumIcon.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(PremiumIcon);
		}
		if (PremiumDescription != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(PremiumDescription);
		}
		if (IsPremiumSpecial)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsPremiumSpecial);
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
		if (NormalIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(NormalIcon);
		}
		if (NormalDescription != 0)
		{
			num += 5;
		}
		if (IsNormalSpecial)
		{
			num += 2;
		}
		if (PremiumIcon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PremiumIcon);
		}
		if (PremiumDescription != 0)
		{
			num += 5;
		}
		if (IsPremiumSpecial)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassRewardDetailConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.NormalIcon.Length != 0)
			{
				NormalIcon = other.NormalIcon;
			}
			if (other.NormalDescription != 0)
			{
				NormalDescription = other.NormalDescription;
			}
			if (other.IsNormalSpecial)
			{
				IsNormalSpecial = other.IsNormalSpecial;
			}
			if (other.PremiumIcon.Length != 0)
			{
				PremiumIcon = other.PremiumIcon;
			}
			if (other.PremiumDescription != 0)
			{
				PremiumDescription = other.PremiumDescription;
			}
			if (other.IsPremiumSpecial)
			{
				IsPremiumSpecial = other.IsPremiumSpecial;
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
			case 18u:
				NormalIcon = input.ReadString();
				break;
			case 29u:
				NormalDescription = input.ReadSFixed32();
				break;
			case 32u:
				IsNormalSpecial = input.ReadBool();
				break;
			case 42u:
				PremiumIcon = input.ReadString();
				break;
			case 53u:
				PremiumDescription = input.ReadSFixed32();
				break;
			case 56u:
				IsPremiumSpecial = input.ReadBool();
				break;
			}
		}
	}
}
