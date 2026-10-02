using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class CheatItemC2S : IMessage<CheatItemC2S>, IMessage, IEquatable<CheatItemC2S>, IDeepCloneable<CheatItemC2S>, IBufferMessage
{
	private static readonly MessageParser<CheatItemC2S> _parser = new MessageParser<CheatItemC2S>(() => new CheatItemC2S());

	private UnknownFieldSet _unknownFields;

	public const int IsAllFieldNumber = 1;

	private bool isAll_;

	public const int ItemIdFieldNumber = 2;

	private int itemId_;

	public const int ItemCountFieldNumber = 3;

	private int itemCount_;

	public const int IsGachaFieldNumber = 4;

	private bool isGacha_;

	public const int ExpFieldNumber = 5;

	private int exp_;

	public const int IsUnlockSportInfoFieldNumber = 6;

	private bool isUnlockSportInfo_;

	public const int IsUnlockRoleInfoFieldNumber = 7;

	private bool isUnlockRoleInfo_;

	public const int PlayerCreditScoreFieldNumber = 8;

	private int playerCreditScore_;

	public const int PlayerCreditActionFieldNumber = 9;

	private int playerCreditAction_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CheatItemC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[186];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAll
	{
		get
		{
			return isAll_;
		}
		set
		{
			isAll_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemId
	{
		get
		{
			return itemId_;
		}
		set
		{
			itemId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ItemCount
	{
		get
		{
			return itemCount_;
		}
		set
		{
			itemCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsGacha
	{
		get
		{
			return isGacha_;
		}
		set
		{
			isGacha_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Exp
	{
		get
		{
			return exp_;
		}
		set
		{
			exp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsUnlockSportInfo
	{
		get
		{
			return isUnlockSportInfo_;
		}
		set
		{
			isUnlockSportInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsUnlockRoleInfo
	{
		get
		{
			return isUnlockRoleInfo_;
		}
		set
		{
			isUnlockRoleInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerCreditScore
	{
		get
		{
			return playerCreditScore_;
		}
		set
		{
			playerCreditScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerCreditAction
	{
		get
		{
			return playerCreditAction_;
		}
		set
		{
			playerCreditAction_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CheatItemC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CheatItemC2S(CheatItemC2S other)
		: this()
	{
		isAll_ = other.isAll_;
		itemId_ = other.itemId_;
		itemCount_ = other.itemCount_;
		isGacha_ = other.isGacha_;
		exp_ = other.exp_;
		isUnlockSportInfo_ = other.isUnlockSportInfo_;
		isUnlockRoleInfo_ = other.isUnlockRoleInfo_;
		playerCreditScore_ = other.playerCreditScore_;
		playerCreditAction_ = other.playerCreditAction_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CheatItemC2S Clone()
	{
		return new CheatItemC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CheatItemC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CheatItemC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsAll != other.IsAll)
		{
			return false;
		}
		if (ItemId != other.ItemId)
		{
			return false;
		}
		if (ItemCount != other.ItemCount)
		{
			return false;
		}
		if (IsGacha != other.IsGacha)
		{
			return false;
		}
		if (Exp != other.Exp)
		{
			return false;
		}
		if (IsUnlockSportInfo != other.IsUnlockSportInfo)
		{
			return false;
		}
		if (IsUnlockRoleInfo != other.IsUnlockRoleInfo)
		{
			return false;
		}
		if (PlayerCreditScore != other.PlayerCreditScore)
		{
			return false;
		}
		if (PlayerCreditAction != other.PlayerCreditAction)
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
		if (IsAll)
		{
			num ^= IsAll.GetHashCode();
		}
		if (ItemId != 0)
		{
			num ^= ItemId.GetHashCode();
		}
		if (ItemCount != 0)
		{
			num ^= ItemCount.GetHashCode();
		}
		if (IsGacha)
		{
			num ^= IsGacha.GetHashCode();
		}
		if (Exp != 0)
		{
			num ^= Exp.GetHashCode();
		}
		if (IsUnlockSportInfo)
		{
			num ^= IsUnlockSportInfo.GetHashCode();
		}
		if (IsUnlockRoleInfo)
		{
			num ^= IsUnlockRoleInfo.GetHashCode();
		}
		if (PlayerCreditScore != 0)
		{
			num ^= PlayerCreditScore.GetHashCode();
		}
		if (PlayerCreditAction != 0)
		{
			num ^= PlayerCreditAction.GetHashCode();
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
		if (IsAll)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsAll);
		}
		if (ItemId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ItemId);
		}
		if (ItemCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ItemCount);
		}
		if (IsGacha)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsGacha);
		}
		if (Exp != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Exp);
		}
		if (IsUnlockSportInfo)
		{
			output.WriteRawTag(48);
			output.WriteBool(IsUnlockSportInfo);
		}
		if (IsUnlockRoleInfo)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsUnlockRoleInfo);
		}
		if (PlayerCreditScore != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(PlayerCreditScore);
		}
		if (PlayerCreditAction != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(PlayerCreditAction);
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
		if (IsAll)
		{
			num += 2;
		}
		if (ItemId != 0)
		{
			num += 5;
		}
		if (ItemCount != 0)
		{
			num += 5;
		}
		if (IsGacha)
		{
			num += 2;
		}
		if (Exp != 0)
		{
			num += 5;
		}
		if (IsUnlockSportInfo)
		{
			num += 2;
		}
		if (IsUnlockRoleInfo)
		{
			num += 2;
		}
		if (PlayerCreditScore != 0)
		{
			num += 5;
		}
		if (PlayerCreditAction != 0)
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
	public void MergeFrom(CheatItemC2S other)
	{
		if (other != null)
		{
			if (other.IsAll)
			{
				IsAll = other.IsAll;
			}
			if (other.ItemId != 0)
			{
				ItemId = other.ItemId;
			}
			if (other.ItemCount != 0)
			{
				ItemCount = other.ItemCount;
			}
			if (other.IsGacha)
			{
				IsGacha = other.IsGacha;
			}
			if (other.Exp != 0)
			{
				Exp = other.Exp;
			}
			if (other.IsUnlockSportInfo)
			{
				IsUnlockSportInfo = other.IsUnlockSportInfo;
			}
			if (other.IsUnlockRoleInfo)
			{
				IsUnlockRoleInfo = other.IsUnlockRoleInfo;
			}
			if (other.PlayerCreditScore != 0)
			{
				PlayerCreditScore = other.PlayerCreditScore;
			}
			if (other.PlayerCreditAction != 0)
			{
				PlayerCreditAction = other.PlayerCreditAction;
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
				IsAll = input.ReadBool();
				break;
			case 21u:
				ItemId = input.ReadSFixed32();
				break;
			case 29u:
				ItemCount = input.ReadSFixed32();
				break;
			case 32u:
				IsGacha = input.ReadBool();
				break;
			case 45u:
				Exp = input.ReadSFixed32();
				break;
			case 48u:
				IsUnlockSportInfo = input.ReadBool();
				break;
			case 56u:
				IsUnlockRoleInfo = input.ReadBool();
				break;
			case 69u:
				PlayerCreditScore = input.ReadSFixed32();
				break;
			case 77u:
				PlayerCreditAction = input.ReadSFixed32();
				break;
			}
		}
	}
}
