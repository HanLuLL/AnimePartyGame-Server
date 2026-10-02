using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class RechargeInfo : IMessage<RechargeInfo>, IMessage, IEquatable<RechargeInfo>, IDeepCloneable<RechargeInfo>, IBufferMessage
{
	private static readonly MessageParser<RechargeInfo> _parser = new MessageParser<RechargeInfo>(() => new RechargeInfo());

	private UnknownFieldSet _unknownFields;

	public const int AppidFieldNumber = 1;

	private string appid_ = "";

	public const int GoodsIdFieldNumber = 2;

	private int goodsId_;

	public const int GoodsCountFieldNumber = 3;

	private int goodsCount_;

	public const int ItemIdFieldNumber = 4;

	private int itemId_;

	public const int NumFieldNumber = 5;

	private int num_;

	public const int TypeDefIdFieldNumber = 6;

	private int typeDefId_;

	public const int OperationGoodsIDFieldNumber = 7;

	private string operationGoodsID_ = "";

	public const int CouponsFieldNumber = 8;

	private int coupons_;

	public const int PlatFieldNumber = 9;

	private string plat_ = "";

	public const int CreateTimeFieldNumber = 10;

	private long createTime_;

	public const int DeviceIdFieldNumber = 11;

	private string deviceId_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RechargeInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[30];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Appid
	{
		get
		{
			return appid_;
		}
		set
		{
			appid_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsId
	{
		get
		{
			return goodsId_;
		}
		set
		{
			goodsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsCount
	{
		get
		{
			return goodsCount_;
		}
		set
		{
			goodsCount_ = value;
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
	public int Num
	{
		get
		{
			return num_;
		}
		set
		{
			num_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TypeDefId
	{
		get
		{
			return typeDefId_;
		}
		set
		{
			typeDefId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string OperationGoodsID
	{
		get
		{
			return operationGoodsID_;
		}
		set
		{
			operationGoodsID_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Coupons
	{
		get
		{
			return coupons_;
		}
		set
		{
			coupons_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Plat
	{
		get
		{
			return plat_;
		}
		set
		{
			plat_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CreateTime
	{
		get
		{
			return createTime_;
		}
		set
		{
			createTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeInfo(RechargeInfo other)
		: this()
	{
		appid_ = other.appid_;
		goodsId_ = other.goodsId_;
		goodsCount_ = other.goodsCount_;
		itemId_ = other.itemId_;
		num_ = other.num_;
		typeDefId_ = other.typeDefId_;
		operationGoodsID_ = other.operationGoodsID_;
		coupons_ = other.coupons_;
		plat_ = other.plat_;
		createTime_ = other.createTime_;
		deviceId_ = other.deviceId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RechargeInfo Clone()
	{
		return new RechargeInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RechargeInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RechargeInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Appid != other.Appid)
		{
			return false;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (GoodsCount != other.GoodsCount)
		{
			return false;
		}
		if (ItemId != other.ItemId)
		{
			return false;
		}
		if (Num != other.Num)
		{
			return false;
		}
		if (TypeDefId != other.TypeDefId)
		{
			return false;
		}
		if (OperationGoodsID != other.OperationGoodsID)
		{
			return false;
		}
		if (Coupons != other.Coupons)
		{
			return false;
		}
		if (Plat != other.Plat)
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (DeviceId != other.DeviceId)
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
		if (Appid.Length != 0)
		{
			num ^= Appid.GetHashCode();
		}
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (GoodsCount != 0)
		{
			num ^= GoodsCount.GetHashCode();
		}
		if (ItemId != 0)
		{
			num ^= ItemId.GetHashCode();
		}
		if (Num != 0)
		{
			num ^= Num.GetHashCode();
		}
		if (TypeDefId != 0)
		{
			num ^= TypeDefId.GetHashCode();
		}
		if (OperationGoodsID.Length != 0)
		{
			num ^= OperationGoodsID.GetHashCode();
		}
		if (Coupons != 0)
		{
			num ^= Coupons.GetHashCode();
		}
		if (Plat.Length != 0)
		{
			num ^= Plat.GetHashCode();
		}
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		if (DeviceId.Length != 0)
		{
			num ^= DeviceId.GetHashCode();
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
		if (Appid.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Appid);
		}
		if (GoodsId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(GoodsId);
		}
		if (GoodsCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(GoodsCount);
		}
		if (ItemId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ItemId);
		}
		if (Num != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Num);
		}
		if (TypeDefId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TypeDefId);
		}
		if (OperationGoodsID.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(OperationGoodsID);
		}
		if (Coupons != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Coupons);
		}
		if (Plat.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(Plat);
		}
		if (CreateTime != 0L)
		{
			output.WriteRawTag(81);
			output.WriteSFixed64(CreateTime);
		}
		if (DeviceId.Length != 0)
		{
			output.WriteRawTag(90);
			output.WriteString(DeviceId);
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
		if (Appid.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Appid);
		}
		if (GoodsId != 0)
		{
			num += 5;
		}
		if (GoodsCount != 0)
		{
			num += 5;
		}
		if (ItemId != 0)
		{
			num += 5;
		}
		if (Num != 0)
		{
			num += 5;
		}
		if (TypeDefId != 0)
		{
			num += 5;
		}
		if (OperationGoodsID.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(OperationGoodsID);
		}
		if (Coupons != 0)
		{
			num += 5;
		}
		if (Plat.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Plat);
		}
		if (CreateTime != 0L)
		{
			num += 9;
		}
		if (DeviceId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DeviceId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RechargeInfo other)
	{
		if (other != null)
		{
			if (other.Appid.Length != 0)
			{
				Appid = other.Appid;
			}
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.GoodsCount != 0)
			{
				GoodsCount = other.GoodsCount;
			}
			if (other.ItemId != 0)
			{
				ItemId = other.ItemId;
			}
			if (other.Num != 0)
			{
				Num = other.Num;
			}
			if (other.TypeDefId != 0)
			{
				TypeDefId = other.TypeDefId;
			}
			if (other.OperationGoodsID.Length != 0)
			{
				OperationGoodsID = other.OperationGoodsID;
			}
			if (other.Coupons != 0)
			{
				Coupons = other.Coupons;
			}
			if (other.Plat.Length != 0)
			{
				Plat = other.Plat;
			}
			if (other.CreateTime != 0L)
			{
				CreateTime = other.CreateTime;
			}
			if (other.DeviceId.Length != 0)
			{
				DeviceId = other.DeviceId;
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
			case 10u:
				Appid = input.ReadString();
				break;
			case 21u:
				GoodsId = input.ReadSFixed32();
				break;
			case 29u:
				GoodsCount = input.ReadSFixed32();
				break;
			case 37u:
				ItemId = input.ReadSFixed32();
				break;
			case 45u:
				Num = input.ReadSFixed32();
				break;
			case 53u:
				TypeDefId = input.ReadSFixed32();
				break;
			case 58u:
				OperationGoodsID = input.ReadString();
				break;
			case 69u:
				Coupons = input.ReadSFixed32();
				break;
			case 74u:
				Plat = input.ReadString();
				break;
			case 81u:
				CreateTime = input.ReadSFixed64();
				break;
			case 90u:
				DeviceId = input.ReadString();
				break;
			}
		}
	}
}
