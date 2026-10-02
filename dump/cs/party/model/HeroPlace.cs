using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class HeroPlace : IMessage<HeroPlace>, IMessage, IEquatable<HeroPlace>, IDeepCloneable<HeroPlace>, IBufferMessage
{
	private static readonly MessageParser<HeroPlace> _parser = new MessageParser<HeroPlace>(() => new HeroPlace());

	private UnknownFieldSet _unknownFields;

	public const int NodeIdFieldNumber = 1;

	private int nodeId_;

	public const int FrontNodeIdsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_frontNodeIds_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> frontNodeIds_ = new RepeatedField<int>();

	public const int BackNodeIdFieldNumber = 3;

	private int backNodeId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroPlace> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[55];

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
	public RepeatedField<int> FrontNodeIds => frontNodeIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BackNodeId
	{
		get
		{
			return backNodeId_;
		}
		set
		{
			backNodeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroPlace()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroPlace(HeroPlace other)
		: this()
	{
		nodeId_ = other.nodeId_;
		frontNodeIds_ = other.frontNodeIds_.Clone();
		backNodeId_ = other.backNodeId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroPlace Clone()
	{
		return new HeroPlace(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroPlace);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroPlace other)
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
		if (!frontNodeIds_.Equals(other.frontNodeIds_))
		{
			return false;
		}
		if (BackNodeId != other.BackNodeId)
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
		num ^= frontNodeIds_.GetHashCode();
		if (BackNodeId != 0)
		{
			num ^= BackNodeId.GetHashCode();
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
		frontNodeIds_.WriteTo(ref output, _repeated_frontNodeIds_codec);
		if (BackNodeId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(BackNodeId);
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
		num += frontNodeIds_.CalculateSize(_repeated_frontNodeIds_codec);
		if (BackNodeId != 0)
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
	public void MergeFrom(HeroPlace other)
	{
		if (other != null)
		{
			if (other.NodeId != 0)
			{
				NodeId = other.NodeId;
			}
			frontNodeIds_.Add(other.frontNodeIds_);
			if (other.BackNodeId != 0)
			{
				BackNodeId = other.BackNodeId;
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
			case 18u:
			case 21u:
				frontNodeIds_.AddEntriesFrom(ref input, _repeated_frontNodeIds_codec);
				break;
			case 29u:
				BackNodeId = input.ReadSFixed32();
				break;
			}
		}
	}
}
