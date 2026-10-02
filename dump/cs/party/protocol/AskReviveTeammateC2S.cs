using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class AskReviveTeammateC2S : IMessage<AskReviveTeammateC2S>, IMessage, IEquatable<AskReviveTeammateC2S>, IDeepCloneable<AskReviveTeammateC2S>, IBufferMessage
{
	private static readonly MessageParser<AskReviveTeammateC2S> _parser = new MessageParser<AskReviveTeammateC2S>(() => new AskReviveTeammateC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int AskPlayerIdFieldNumber = 2;

	private long askPlayerId_;

	public const int IsReviveFieldNumber = 3;

	private bool isRevive_;

	public const int GoldFieldNumber = 4;

	private int gold_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AskReviveTeammateC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[291];

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
	public long AskPlayerId
	{
		get
		{
			return askPlayerId_;
		}
		set
		{
			askPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRevive
	{
		get
		{
			return isRevive_;
		}
		set
		{
			isRevive_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Gold
	{
		get
		{
			return gold_;
		}
		set
		{
			gold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskReviveTeammateC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskReviveTeammateC2S(AskReviveTeammateC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		askPlayerId_ = other.askPlayerId_;
		isRevive_ = other.isRevive_;
		gold_ = other.gold_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskReviveTeammateC2S Clone()
	{
		return new AskReviveTeammateC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AskReviveTeammateC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AskReviveTeammateC2S other)
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
		if (AskPlayerId != other.AskPlayerId)
		{
			return false;
		}
		if (IsRevive != other.IsRevive)
		{
			return false;
		}
		if (Gold != other.Gold)
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
		if (AskPlayerId != 0L)
		{
			num ^= AskPlayerId.GetHashCode();
		}
		if (IsRevive)
		{
			num ^= IsRevive.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
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
		if (AskPlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(AskPlayerId);
		}
		if (IsRevive)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsRevive);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Gold);
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
		if (AskPlayerId != 0L)
		{
			num += 9;
		}
		if (IsRevive)
		{
			num += 2;
		}
		if (Gold != 0)
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
	public void MergeFrom(AskReviveTeammateC2S other)
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
		if (other.AskPlayerId != 0L)
		{
			AskPlayerId = other.AskPlayerId;
		}
		if (other.IsRevive)
		{
			IsRevive = other.IsRevive;
		}
		if (other.Gold != 0)
		{
			Gold = other.Gold;
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
			case 17u:
				AskPlayerId = input.ReadSFixed64();
				break;
			case 24u:
				IsRevive = input.ReadBool();
				break;
			case 37u:
				Gold = input.ReadSFixed32();
				break;
			}
		}
	}
}
