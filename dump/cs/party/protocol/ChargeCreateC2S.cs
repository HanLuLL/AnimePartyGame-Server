using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChargeCreateC2S : IMessage<ChargeCreateC2S>, IMessage, IEquatable<ChargeCreateC2S>, IDeepCloneable<ChargeCreateC2S>, IBufferMessage
{
	private static readonly MessageParser<ChargeCreateC2S> _parser = new MessageParser<ChargeCreateC2S>(() => new ChargeCreateC2S());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIdFieldNumber = 1;

	private int goodsId_;

	public const int CountFieldNumber = 2;

	private int count_;

	public const int SteamidFieldNumber = 3;

	private ulong steamid_;

	public const int AppidFieldNumber = 4;

	private uint appid_;

	public const int LanguageFieldNumber = 5;

	private string language_ = "";

	public const int DescriptionFieldNumber = 6;

	private string description_ = "";

	public const int TypeFieldNumber = 7;

	private int type_;

	public const int CouponsFieldNumber = 8;

	private int coupons_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChargeCreateC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[118];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int Count
	{
		get
		{
			return count_;
		}
		set
		{
			count_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ulong Steamid
	{
		get
		{
			return steamid_;
		}
		set
		{
			steamid_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public uint Appid
	{
		get
		{
			return appid_;
		}
		set
		{
			appid_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Language
	{
		get
		{
			return language_;
		}
		set
		{
			language_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Description
	{
		get
		{
			return description_;
		}
		set
		{
			description_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
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
	public ChargeCreateC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChargeCreateC2S(ChargeCreateC2S other)
		: this()
	{
		goodsId_ = other.goodsId_;
		count_ = other.count_;
		steamid_ = other.steamid_;
		appid_ = other.appid_;
		language_ = other.language_;
		description_ = other.description_;
		type_ = other.type_;
		coupons_ = other.coupons_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChargeCreateC2S Clone()
	{
		return new ChargeCreateC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChargeCreateC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChargeCreateC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (Count != other.Count)
		{
			return false;
		}
		if (Steamid != other.Steamid)
		{
			return false;
		}
		if (Appid != other.Appid)
		{
			return false;
		}
		if (Language != other.Language)
		{
			return false;
		}
		if (Description != other.Description)
		{
			return false;
		}
		if (Type != other.Type)
		{
			return false;
		}
		if (Coupons != other.Coupons)
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
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (Count != 0)
		{
			num ^= Count.GetHashCode();
		}
		if (Steamid != 0L)
		{
			num ^= Steamid.GetHashCode();
		}
		if (Appid != 0)
		{
			num ^= Appid.GetHashCode();
		}
		if (Language.Length != 0)
		{
			num ^= Language.GetHashCode();
		}
		if (Description.Length != 0)
		{
			num ^= Description.GetHashCode();
		}
		if (Type != 0)
		{
			num ^= Type.GetHashCode();
		}
		if (Coupons != 0)
		{
			num ^= Coupons.GetHashCode();
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
		if (GoodsId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsId);
		}
		if (Count != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Count);
		}
		if (Steamid != 0L)
		{
			output.WriteRawTag(25);
			output.WriteFixed64(Steamid);
		}
		if (Appid != 0)
		{
			output.WriteRawTag(37);
			output.WriteFixed32(Appid);
		}
		if (Language.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Language);
		}
		if (Description.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Description);
		}
		if (Type != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Type);
		}
		if (Coupons != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Coupons);
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
		if (GoodsId != 0)
		{
			num += 5;
		}
		if (Count != 0)
		{
			num += 5;
		}
		if (Steamid != 0L)
		{
			num += 9;
		}
		if (Appid != 0)
		{
			num += 5;
		}
		if (Language.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Language);
		}
		if (Description.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Description);
		}
		if (Type != 0)
		{
			num += 5;
		}
		if (Coupons != 0)
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
	public void MergeFrom(ChargeCreateC2S other)
	{
		if (other != null)
		{
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.Count != 0)
			{
				Count = other.Count;
			}
			if (other.Steamid != 0L)
			{
				Steamid = other.Steamid;
			}
			if (other.Appid != 0)
			{
				Appid = other.Appid;
			}
			if (other.Language.Length != 0)
			{
				Language = other.Language;
			}
			if (other.Description.Length != 0)
			{
				Description = other.Description;
			}
			if (other.Type != 0)
			{
				Type = other.Type;
			}
			if (other.Coupons != 0)
			{
				Coupons = other.Coupons;
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
				GoodsId = input.ReadSFixed32();
				break;
			case 21u:
				Count = input.ReadSFixed32();
				break;
			case 25u:
				Steamid = input.ReadFixed64();
				break;
			case 37u:
				Appid = input.ReadFixed32();
				break;
			case 42u:
				Language = input.ReadString();
				break;
			case 50u:
				Description = input.ReadString();
				break;
			case 61u:
				Type = input.ReadSFixed32();
				break;
			case 69u:
				Coupons = input.ReadSFixed32();
				break;
			}
		}
	}
}
