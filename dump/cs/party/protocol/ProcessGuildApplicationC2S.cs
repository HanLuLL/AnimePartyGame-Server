using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ProcessGuildApplicationC2S : IMessage<ProcessGuildApplicationC2S>, IMessage, IEquatable<ProcessGuildApplicationC2S>, IDeepCloneable<ProcessGuildApplicationC2S>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum Action
		{
			[OriginalName("Accept")]
			Accept,
			[OriginalName("Reject")]
			Reject
		}
	}

	private static readonly MessageParser<ProcessGuildApplicationC2S> _parser = new MessageParser<ProcessGuildApplicationC2S>(() => new ProcessGuildApplicationC2S());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdsFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_playerIds_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> playerIds_ = new RepeatedField<long>();

	public const int ActionFieldNumber = 2;

	private Types.Action action_;

	public const int PlayerNameFieldNumber = 3;

	private string playerName_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ProcessGuildApplicationC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[567];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> PlayerIds => playerIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.Action Action
	{
		get
		{
			return action_;
		}
		set
		{
			action_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PlayerName
	{
		get
		{
			return playerName_;
		}
		set
		{
			playerName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildApplicationC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildApplicationC2S(ProcessGuildApplicationC2S other)
		: this()
	{
		playerIds_ = other.playerIds_.Clone();
		action_ = other.action_;
		playerName_ = other.playerName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildApplicationC2S Clone()
	{
		return new ProcessGuildApplicationC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ProcessGuildApplicationC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ProcessGuildApplicationC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!playerIds_.Equals(other.playerIds_))
		{
			return false;
		}
		if (Action != other.Action)
		{
			return false;
		}
		if (PlayerName != other.PlayerName)
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
		num ^= playerIds_.GetHashCode();
		if (Action != Types.Action.Accept)
		{
			num ^= Action.GetHashCode();
		}
		if (PlayerName.Length != 0)
		{
			num ^= PlayerName.GetHashCode();
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
		playerIds_.WriteTo(ref output, _repeated_playerIds_codec);
		if (Action != Types.Action.Accept)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)Action);
		}
		if (PlayerName.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(PlayerName);
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
		num += playerIds_.CalculateSize(_repeated_playerIds_codec);
		if (Action != Types.Action.Accept)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Action);
		}
		if (PlayerName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PlayerName);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ProcessGuildApplicationC2S other)
	{
		if (other != null)
		{
			playerIds_.Add(other.playerIds_);
			if (other.Action != Types.Action.Accept)
			{
				Action = other.Action;
			}
			if (other.PlayerName.Length != 0)
			{
				PlayerName = other.PlayerName;
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
			case 10u:
				playerIds_.AddEntriesFrom(ref input, _repeated_playerIds_codec);
				break;
			case 16u:
				Action = (Types.Action)input.ReadEnum();
				break;
			case 26u:
				PlayerName = input.ReadString();
				break;
			}
		}
	}
}
