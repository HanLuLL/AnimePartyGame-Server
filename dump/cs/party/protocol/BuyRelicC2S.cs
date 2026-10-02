using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class BuyRelicC2S : IMessage<BuyRelicC2S>, IMessage, IEquatable<BuyRelicC2S>, IDeepCloneable<BuyRelicC2S>, IBufferMessage
{
	private static readonly MessageParser<BuyRelicC2S> _parser = new MessageParser<BuyRelicC2S>(() => new BuyRelicC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int DivinationGoldFieldNumber = 2;

	private int divinationGold_;

	public const int RelicGoldFieldNumber = 3;

	private int relicGold_;

	public const int SelectFieldNumber = 4;

	private int select_;

	public const int ExitFieldNumber = 5;

	private bool exit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BuyRelicC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[273];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DivinationGold
	{
		get
		{
			return divinationGold_;
		}
		set
		{
			divinationGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicGold
	{
		get
		{
			return relicGold_;
		}
		set
		{
			relicGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Select
	{
		get
		{
			return select_;
		}
		set
		{
			select_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Exit
	{
		get
		{
			return exit_;
		}
		set
		{
			exit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyRelicC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyRelicC2S(BuyRelicC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		divinationGold_ = other.divinationGold_;
		relicGold_ = other.relicGold_;
		select_ = other.select_;
		exit_ = other.exit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyRelicC2S Clone()
	{
		return new BuyRelicC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BuyRelicC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BuyRelicC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (DivinationGold != other.DivinationGold)
		{
			return false;
		}
		if (RelicGold != other.RelicGold)
		{
			return false;
		}
		if (Select != other.Select)
		{
			return false;
		}
		if (Exit != other.Exit)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (DivinationGold != 0)
		{
			num ^= DivinationGold.GetHashCode();
		}
		if (RelicGold != 0)
		{
			num ^= RelicGold.GetHashCode();
		}
		if (Select != 0)
		{
			num ^= Select.GetHashCode();
		}
		if (Exit)
		{
			num ^= Exit.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (DivinationGold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(DivinationGold);
		}
		if (RelicGold != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(RelicGold);
		}
		if (Select != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Select);
		}
		if (Exit)
		{
			output.WriteRawTag(40);
			output.WriteBool(Exit);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (DivinationGold != 0)
		{
			num += 5;
		}
		if (RelicGold != 0)
		{
			num += 5;
		}
		if (Select != 0)
		{
			num += 5;
		}
		if (Exit)
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
	public void MergeFrom(BuyRelicC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.DivinationGold != 0)
		{
			DivinationGold = other.DivinationGold;
		}
		if (other.RelicGold != 0)
		{
			RelicGold = other.RelicGold;
		}
		if (other.Select != 0)
		{
			Select = other.Select;
		}
		if (other.Exit)
		{
			Exit = other.Exit;
		}
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
			case 10u:
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 21u:
				DivinationGold = input.ReadSFixed32();
				break;
			case 29u:
				RelicGold = input.ReadSFixed32();
				break;
			case 37u:
				Select = input.ReadSFixed32();
				break;
			case 40u:
				Exit = input.ReadBool();
				break;
			}
		}
	}
}
