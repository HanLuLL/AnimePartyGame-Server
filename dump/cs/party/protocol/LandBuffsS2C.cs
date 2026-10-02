using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class LandBuffsS2C : IMessage<LandBuffsS2C>, IMessage, IEquatable<LandBuffsS2C>, IDeepCloneable<LandBuffsS2C>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public sealed class LandBuffsWrap : IMessage<LandBuffsWrap>, IMessage, IEquatable<LandBuffsWrap>, IDeepCloneable<LandBuffsWrap>, IBufferMessage
		{
			private static readonly MessageParser<LandBuffsWrap> _parser = new MessageParser<LandBuffsWrap>(() => new LandBuffsWrap());

			private UnknownFieldSet _unknownFields;

			public const int NodeIdFieldNumber = 1;

			private int nodeId_;

			public const int BuffArrFieldNumber = 2;

			private BuffArray buffArr_;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public static MessageParser<LandBuffsWrap> Parser => _parser;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public static MessageDescriptor Descriptor => LandBuffsS2C.Descriptor.NestedTypes[0];

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
			public BuffArray BuffArr
			{
				get
				{
					return buffArr_;
				}
				set
				{
					buffArr_ = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public LandBuffsWrap()
			{
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public LandBuffsWrap(LandBuffsWrap other)
				: this()
			{
				nodeId_ = other.nodeId_;
				buffArr_ = ((other.buffArr_ != null) ? other.buffArr_.Clone() : null);
				_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public LandBuffsWrap Clone()
			{
				return new LandBuffsWrap(this);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public override bool Equals(object other)
			{
				return Equals(other as LandBuffsWrap);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public bool Equals(LandBuffsWrap other)
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
				if (!object.Equals(BuffArr, other.BuffArr))
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
				if (buffArr_ != null)
				{
					num ^= BuffArr.GetHashCode();
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
				if (buffArr_ != null)
				{
					output.WriteRawTag(18);
					output.WriteMessage(BuffArr);
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
				if (buffArr_ != null)
				{
					num += 1 + CodedOutputStream.ComputeMessageSize(BuffArr);
				}
				if (_unknownFields != null)
				{
					num += _unknownFields.CalculateSize();
				}
				return num;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public void MergeFrom(LandBuffsWrap other)
			{
				if (other == null)
				{
					return;
				}
				if (other.NodeId != 0)
				{
					NodeId = other.NodeId;
				}
				if (other.buffArr_ != null)
				{
					if (buffArr_ == null)
					{
						BuffArr = new BuffArray();
					}
					BuffArr.MergeFrom(other.BuffArr);
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
					case 13u:
						NodeId = input.ReadSFixed32();
						break;
					case 18u:
						if (buffArr_ == null)
						{
							BuffArr = new BuffArray();
						}
						input.ReadMessage(BuffArr);
						break;
					}
				}
			}
		}
	}

	private static readonly MessageParser<LandBuffsS2C> _parser = new MessageParser<LandBuffsS2C>(() => new LandBuffsS2C());

	private UnknownFieldSet _unknownFields;

	public const int BuffsFieldNumber = 1;

	private static readonly FieldCodec<Types.LandBuffsWrap> _repeated_buffs_codec = FieldCodec.ForMessage(10u, Types.LandBuffsWrap.Parser);

	private readonly RepeatedField<Types.LandBuffsWrap> buffs_ = new RepeatedField<Types.LandBuffsWrap>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LandBuffsS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[382];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Types.LandBuffsWrap> Buffs => buffs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandBuffsS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandBuffsS2C(LandBuffsS2C other)
		: this()
	{
		buffs_ = other.buffs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandBuffsS2C Clone()
	{
		return new LandBuffsS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LandBuffsS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LandBuffsS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!buffs_.Equals(other.buffs_))
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
		num ^= buffs_.GetHashCode();
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
		buffs_.WriteTo(ref output, _repeated_buffs_codec);
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
		num += buffs_.CalculateSize(_repeated_buffs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LandBuffsS2C other)
	{
		if (other != null)
		{
			buffs_.Add(other.buffs_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				buffs_.AddEntriesFrom(ref input, _repeated_buffs_codec);
			}
		}
	}
}
