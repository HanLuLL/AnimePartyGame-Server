using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class MonsterPursuitS2C : IMessage<MonsterPursuitS2C>, IMessage, IEquatable<MonsterPursuitS2C>, IDeepCloneable<MonsterPursuitS2C>, IBufferMessage
{
	private static readonly MessageParser<MonsterPursuitS2C> _parser = new MessageParser<MonsterPursuitS2C>(() => new MonsterPursuitS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int NodeIdFieldNumber = 2;

	private int nodeId_;

	public const int FrontIdsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_frontIds_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> frontIds_ = new RepeatedField<int>();

	public const int BackIdFieldNumber = 4;

	private int backId_;

	public const int ExitFieldNumber = 5;

	private bool exit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MonsterPursuitS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[272];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

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
	public RepeatedField<int> FrontIds => frontIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BackId
	{
		get
		{
			return backId_;
		}
		set
		{
			backId_ = value;
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
	public MonsterPursuitS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterPursuitS2C(MonsterPursuitS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		nodeId_ = other.nodeId_;
		frontIds_ = other.frontIds_.Clone();
		backId_ = other.backId_;
		exit_ = other.exit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterPursuitS2C Clone()
	{
		return new MonsterPursuitS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MonsterPursuitS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MonsterPursuitS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (NodeId != other.NodeId)
		{
			return false;
		}
		if (!frontIds_.Equals(other.frontIds_))
		{
			return false;
		}
		if (BackId != other.BackId)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (NodeId != 0)
		{
			num ^= NodeId.GetHashCode();
		}
		num ^= frontIds_.GetHashCode();
		if (BackId != 0)
		{
			num ^= BackId.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (NodeId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NodeId);
		}
		frontIds_.WriteTo(ref output, _repeated_frontIds_codec);
		if (BackId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(BackId);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (NodeId != 0)
		{
			num += 5;
		}
		num += frontIds_.CalculateSize(_repeated_frontIds_codec);
		if (BackId != 0)
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
	public void MergeFrom(MonsterPursuitS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.NodeId != 0)
			{
				NodeId = other.NodeId;
			}
			frontIds_.Add(other.frontIds_);
			if (other.BackId != 0)
			{
				BackId = other.BackId;
			}
			if (other.Exit)
			{
				Exit = other.Exit;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				NodeId = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				frontIds_.AddEntriesFrom(ref input, _repeated_frontIds_codec);
				break;
			case 37u:
				BackId = input.ReadSFixed32();
				break;
			case 40u:
				Exit = input.ReadBool();
				break;
			}
		}
	}
}
