using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ThrowDiceC2S : IMessage<ThrowDiceC2S>, IMessage, IEquatable<ThrowDiceC2S>, IDeepCloneable<ThrowDiceC2S>, IBufferMessage
{
	private static readonly MessageParser<ThrowDiceC2S> _parser = new MessageParser<ThrowDiceC2S>(() => new ThrowDiceC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int DevPointFieldNumber = 2;

	private int devPoint_;

	public const int IsNoOperFieldNumber = 3;

	private bool isNoOper_;

	public const int IsMoveNowFieldNumber = 4;

	private bool isMoveNow_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ThrowDiceC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[243];

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
	public bool IsNoOper
	{
		get
		{
			return isNoOper_;
		}
		set
		{
			isNoOper_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsMoveNow
	{
		get
		{
			return isMoveNow_;
		}
		set
		{
			isMoveNow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceC2S(ThrowDiceC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		devPoint_ = other.devPoint_;
		isNoOper_ = other.isNoOper_;
		isMoveNow_ = other.isMoveNow_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceC2S Clone()
	{
		return new ThrowDiceC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ThrowDiceC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ThrowDiceC2S other)
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
		if (IsNoOper != other.IsNoOper)
		{
			return false;
		}
		if (IsMoveNow != other.IsMoveNow)
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
		if (IsNoOper)
		{
			num ^= IsNoOper.GetHashCode();
		}
		if (IsMoveNow)
		{
			num ^= IsMoveNow.GetHashCode();
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
		if (IsNoOper)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsNoOper);
		}
		if (IsMoveNow)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsMoveNow);
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
		if (IsNoOper)
		{
			num += 2;
		}
		if (IsMoveNow)
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
	public void MergeFrom(ThrowDiceC2S other)
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
		if (other.IsNoOper)
		{
			IsNoOper = other.IsNoOper;
		}
		if (other.IsMoveNow)
		{
			IsMoveNow = other.IsMoveNow;
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
				IsNoOper = input.ReadBool();
				break;
			case 32u:
				IsMoveNow = input.ReadBool();
				break;
			}
		}
	}
}
