using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class BaseLand : IMessage<BaseLand>, IMessage, IEquatable<BaseLand>, IDeepCloneable<BaseLand>, IBufferMessage
{
	private static readonly MessageParser<BaseLand> _parser = new MessageParser<BaseLand>(() => new BaseLand());

	private UnknownFieldSet _unknownFields;

	public const int NodeIdFieldNumber = 1;

	private int nodeId_;

	public const int LandTypeFieldNumber = 2;

	private int landType_;

	public const int IsCloudPointFieldNumber = 3;

	private bool isCloudPoint_;

	public const int IsRemoteFieldNumber = 4;

	private bool isRemote_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BaseLand> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[89];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NodeId
	{
		get
		{
			return nodeId_;
		}
		set
		{
			nodeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LandType
	{
		get
		{
			return landType_;
		}
		set
		{
			landType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsCloudPoint
	{
		get
		{
			return isCloudPoint_;
		}
		set
		{
			isCloudPoint_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRemote
	{
		get
		{
			return isRemote_;
		}
		set
		{
			isRemote_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BaseLand()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BaseLand(BaseLand other)
		: this()
	{
		nodeId_ = other.nodeId_;
		landType_ = other.landType_;
		isCloudPoint_ = other.isCloudPoint_;
		isRemote_ = other.isRemote_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BaseLand Clone()
	{
		return new BaseLand(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BaseLand);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BaseLand other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (NodeId != other.NodeId)
		{
			return false;
		}
		if (LandType != other.LandType)
		{
			return false;
		}
		if (IsCloudPoint != other.IsCloudPoint)
		{
			return false;
		}
		if (IsRemote != other.IsRemote)
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
		if (NodeId != 0)
		{
			num ^= NodeId.GetHashCode();
		}
		if (LandType != 0)
		{
			num ^= LandType.GetHashCode();
		}
		if (IsCloudPoint)
		{
			num ^= IsCloudPoint.GetHashCode();
		}
		if (IsRemote)
		{
			num ^= IsRemote.GetHashCode();
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
		if (NodeId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(NodeId);
		}
		if (LandType != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(LandType);
		}
		if (IsCloudPoint)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsCloudPoint);
		}
		if (IsRemote)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsRemote);
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
		if (NodeId != 0)
		{
			num += 5;
		}
		if (LandType != 0)
		{
			num += 5;
		}
		if (IsCloudPoint)
		{
			num += 2;
		}
		if (IsRemote)
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
	public void MergeFrom(BaseLand other)
	{
		if (other != null)
		{
			if (other.NodeId != 0)
			{
				NodeId = other.NodeId;
			}
			if (other.LandType != 0)
			{
				LandType = other.LandType;
			}
			if (other.IsCloudPoint)
			{
				IsCloudPoint = other.IsCloudPoint;
			}
			if (other.IsRemote)
			{
				IsRemote = other.IsRemote;
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
				NodeId = input.ReadSFixed32();
				break;
			case 21u:
				LandType = input.ReadSFixed32();
				break;
			case 24u:
				IsCloudPoint = input.ReadBool();
				break;
			case 32u:
				IsRemote = input.ReadBool();
				break;
			}
		}
	}
}
