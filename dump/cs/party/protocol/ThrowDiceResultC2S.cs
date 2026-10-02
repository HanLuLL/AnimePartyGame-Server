using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ThrowDiceResultC2S : IMessage<ThrowDiceResultC2S>, IMessage, IEquatable<ThrowDiceResultC2S>, IDeepCloneable<ThrowDiceResultC2S>, IBufferMessage
{
	private static readonly MessageParser<ThrowDiceResultC2S> _parser = new MessageParser<ThrowDiceResultC2S>(() => new ThrowDiceResultC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int MaxPointFieldNumber = 2;

	private int maxPoint_;

	public const int PointFieldNumber = 3;

	private int point_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ThrowDiceResultC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[325];

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
	public int MaxPoint
	{
		get
		{
			return maxPoint_;
		}
		set
		{
			maxPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Point
	{
		get
		{
			return point_;
		}
		set
		{
			point_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceResultC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceResultC2S(ThrowDiceResultC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		maxPoint_ = other.maxPoint_;
		point_ = other.point_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceResultC2S Clone()
	{
		return new ThrowDiceResultC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ThrowDiceResultC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ThrowDiceResultC2S other)
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
		if (MaxPoint != other.MaxPoint)
		{
			return false;
		}
		if (Point != other.Point)
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
		if (MaxPoint != 0)
		{
			num ^= MaxPoint.GetHashCode();
		}
		if (Point != 0)
		{
			num ^= Point.GetHashCode();
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
		if (MaxPoint != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MaxPoint);
		}
		if (Point != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Point);
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
		if (MaxPoint != 0)
		{
			num += 5;
		}
		if (Point != 0)
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
	public void MergeFrom(ThrowDiceResultC2S other)
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
		if (other.MaxPoint != 0)
		{
			MaxPoint = other.MaxPoint;
		}
		if (other.Point != 0)
		{
			Point = other.Point;
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
				MaxPoint = input.ReadSFixed32();
				break;
			case 29u:
				Point = input.ReadSFixed32();
				break;
			}
		}
	}
}
