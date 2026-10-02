using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class BattleChoiceC2S : IMessage<BattleChoiceC2S>, IMessage, IEquatable<BattleChoiceC2S>, IDeepCloneable<BattleChoiceC2S>, IBufferMessage
{
	private static readonly MessageParser<BattleChoiceC2S> _parser = new MessageParser<BattleChoiceC2S>(() => new BattleChoiceC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int DevPointFieldNumber = 2;

	private int devPoint_;

	public const int DodgeFieldNumber = 3;

	private bool dodge_;

	public const int NoDodgeFieldNumber = 4;

	private bool noDodge_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattleChoiceC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[283];

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
	public int DevPoint
	{
		get
		{
			return devPoint_;
		}
		set
		{
			devPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Dodge
	{
		get
		{
			return dodge_;
		}
		set
		{
			dodge_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NoDodge
	{
		get
		{
			return noDodge_;
		}
		set
		{
			noDodge_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceC2S(BattleChoiceC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		devPoint_ = other.devPoint_;
		dodge_ = other.dodge_;
		noDodge_ = other.noDodge_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceC2S Clone()
	{
		return new BattleChoiceC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattleChoiceC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattleChoiceC2S other)
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
		if (DevPoint != other.DevPoint)
		{
			return false;
		}
		if (Dodge != other.Dodge)
		{
			return false;
		}
		if (NoDodge != other.NoDodge)
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
		if (DevPoint != 0)
		{
			num ^= DevPoint.GetHashCode();
		}
		if (Dodge)
		{
			num ^= Dodge.GetHashCode();
		}
		if (NoDodge)
		{
			num ^= NoDodge.GetHashCode();
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
		if (DevPoint != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(DevPoint);
		}
		if (Dodge)
		{
			output.WriteRawTag(24);
			output.WriteBool(Dodge);
		}
		if (NoDodge)
		{
			output.WriteRawTag(32);
			output.WriteBool(NoDodge);
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
		if (DevPoint != 0)
		{
			num += 5;
		}
		if (Dodge)
		{
			num += 2;
		}
		if (NoDodge)
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
	public void MergeFrom(BattleChoiceC2S other)
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
		if (other.DevPoint != 0)
		{
			DevPoint = other.DevPoint;
		}
		if (other.Dodge)
		{
			Dodge = other.Dodge;
		}
		if (other.NoDodge)
		{
			NoDodge = other.NoDodge;
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
				DevPoint = input.ReadSFixed32();
				break;
			case 24u:
				Dodge = input.ReadBool();
				break;
			case 32u:
				NoDodge = input.ReadBool();
				break;
			}
		}
	}
}
