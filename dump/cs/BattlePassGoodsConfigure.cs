using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class BattlePassGoodsConfigure : IMessage<BattlePassGoodsConfigure>, IMessage, IEquatable<BattlePassGoodsConfigure>, IDeepCloneable<BattlePassGoodsConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattlePassGoodsConfigure> _parser = new MessageParser<BattlePassGoodsConfigure>(() => new BattlePassGoodsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NormalGoodsFieldNumber = 2;

	private int normalGoods_;

	public const int PremiumGoodsFieldNumber = 3;

	private int premiumGoods_;

	public const int UpgradeGoodsFieldNumber = 4;

	private int upgradeGoods_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassGoodsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[1];

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
	public int NormalGoods
	{
		get
		{
			return normalGoods_;
		}
		private set
		{
			normalGoods_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumGoods
	{
		get
		{
			return premiumGoods_;
		}
		private set
		{
			premiumGoods_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpgradeGoods
	{
		get
		{
			return upgradeGoods_;
		}
		private set
		{
			upgradeGoods_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassGoodsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassGoodsConfigure(BattlePassGoodsConfigure other)
		: this()
	{
		id_ = other.id_;
		normalGoods_ = other.normalGoods_;
		premiumGoods_ = other.premiumGoods_;
		upgradeGoods_ = other.upgradeGoods_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassGoodsConfigure Clone()
	{
		return new BattlePassGoodsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassGoodsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassGoodsConfigure other)
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
		if (NormalGoods != other.NormalGoods)
		{
			return false;
		}
		if (PremiumGoods != other.PremiumGoods)
		{
			return false;
		}
		if (UpgradeGoods != other.UpgradeGoods)
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
		if (NormalGoods != 0)
		{
			num ^= NormalGoods.GetHashCode();
		}
		if (PremiumGoods != 0)
		{
			num ^= PremiumGoods.GetHashCode();
		}
		if (UpgradeGoods != 0)
		{
			num ^= UpgradeGoods.GetHashCode();
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
		if (NormalGoods != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NormalGoods);
		}
		if (PremiumGoods != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PremiumGoods);
		}
		if (UpgradeGoods != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(UpgradeGoods);
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
		if (NormalGoods != 0)
		{
			num += 5;
		}
		if (PremiumGoods != 0)
		{
			num += 5;
		}
		if (UpgradeGoods != 0)
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
	public void MergeFrom(BattlePassGoodsConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NormalGoods != 0)
			{
				NormalGoods = other.NormalGoods;
			}
			if (other.PremiumGoods != 0)
			{
				PremiumGoods = other.PremiumGoods;
			}
			if (other.UpgradeGoods != 0)
			{
				UpgradeGoods = other.UpgradeGoods;
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
				NormalGoods = input.ReadSFixed32();
				break;
			case 29u:
				PremiumGoods = input.ReadSFixed32();
				break;
			case 37u:
				UpgradeGoods = input.ReadSFixed32();
				break;
			}
		}
	}
}
