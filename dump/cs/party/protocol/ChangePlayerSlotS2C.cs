using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChangePlayerSlotS2C : IMessage<ChangePlayerSlotS2C>, IMessage, IEquatable<ChangePlayerSlotS2C>, IDeepCloneable<ChangePlayerSlotS2C>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public sealed class PlayerSlot : IMessage<PlayerSlot>, IMessage, IEquatable<PlayerSlot>, IDeepCloneable<PlayerSlot>, IBufferMessage
		{
			private static readonly MessageParser<PlayerSlot> _parser = new MessageParser<PlayerSlot>(() => new PlayerSlot());

			private UnknownFieldSet _unknownFields;

			public const int PlayerIdFieldNumber = 1;

			private long playerId_;

			public const int SlotFieldNumber = 2;

			private int slot_;

			public const int ChangeSlotFieldNumber = 3;

			private int changeSlot_;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public static MessageParser<PlayerSlot> Parser => _parser;

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public static MessageDescriptor Descriptor => ChangePlayerSlotS2C.Descriptor.NestedTypes[0];

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
			public int Slot
			{
				get
				{
					return slot_;
				}
				set
				{
					slot_ = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public int ChangeSlot
			{
				get
				{
					return changeSlot_;
				}
				set
				{
					changeSlot_ = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public PlayerSlot()
			{
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public PlayerSlot(PlayerSlot other)
				: this()
			{
				playerId_ = other.playerId_;
				slot_ = other.slot_;
				changeSlot_ = other.changeSlot_;
				_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public PlayerSlot Clone()
			{
				return new PlayerSlot(this);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public override bool Equals(object other)
			{
				return Equals(other as PlayerSlot);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("protoc", null)]
			public bool Equals(PlayerSlot other)
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
				if (Slot != other.Slot)
				{
					return false;
				}
				if (ChangeSlot != other.ChangeSlot)
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
				if (Slot != 0)
				{
					num ^= Slot.GetHashCode();
				}
				if (ChangeSlot != 0)
				{
					num ^= ChangeSlot.GetHashCode();
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
				if (Slot != 0)
				{
					output.WriteRawTag(21);
					output.WriteSFixed32(Slot);
				}
				if (ChangeSlot != 0)
				{
					output.WriteRawTag(29);
					output.WriteSFixed32(ChangeSlot);
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
				if (Slot != 0)
				{
					num += 5;
				}
				if (ChangeSlot != 0)
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
			public void MergeFrom(PlayerSlot other)
			{
				if (other != null)
				{
					if (other.PlayerId != 0L)
					{
						PlayerId = other.PlayerId;
					}
					if (other.Slot != 0)
					{
						Slot = other.Slot;
					}
					if (other.ChangeSlot != 0)
					{
						ChangeSlot = other.ChangeSlot;
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
						Slot = input.ReadSFixed32();
						break;
					case 29u:
						ChangeSlot = input.ReadSFixed32();
						break;
					}
				}
			}
		}
	}

	private static readonly MessageParser<ChangePlayerSlotS2C> _parser = new MessageParser<ChangePlayerSlotS2C>(() => new ChangePlayerSlotS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerSlotFieldNumber = 1;

	private static readonly FieldCodec<Types.PlayerSlot> _repeated_playerSlot_codec = FieldCodec.ForMessage(10u, Types.PlayerSlot.Parser);

	private readonly RepeatedField<Types.PlayerSlot> playerSlot_ = new RepeatedField<Types.PlayerSlot>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChangePlayerSlotS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[367];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Types.PlayerSlot> PlayerSlot => playerSlot_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangePlayerSlotS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangePlayerSlotS2C(ChangePlayerSlotS2C other)
		: this()
	{
		playerSlot_ = other.playerSlot_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangePlayerSlotS2C Clone()
	{
		return new ChangePlayerSlotS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChangePlayerSlotS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChangePlayerSlotS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!playerSlot_.Equals(other.playerSlot_))
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
		num ^= playerSlot_.GetHashCode();
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
		playerSlot_.WriteTo(ref output, _repeated_playerSlot_codec);
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
		num += playerSlot_.CalculateSize(_repeated_playerSlot_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChangePlayerSlotS2C other)
	{
		if (other != null)
		{
			playerSlot_.Add(other.playerSlot_);
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
				playerSlot_.AddEntriesFrom(ref input, _repeated_playerSlot_codec);
			}
		}
	}
}
