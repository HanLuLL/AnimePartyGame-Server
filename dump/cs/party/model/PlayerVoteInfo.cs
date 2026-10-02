using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class PlayerVoteInfo : IMessage<PlayerVoteInfo>, IMessage, IEquatable<PlayerVoteInfo>, IDeepCloneable<PlayerVoteInfo>, IBufferMessage
{
	private static readonly MessageParser<PlayerVoteInfo> _parser = new MessageParser<PlayerVoteInfo>(() => new PlayerVoteInfo());

	private UnknownFieldSet _unknownFields;

	public const int SelectIdFieldNumber = 1;

	private int selectId_;

	public const int AffirmFieldNumber = 2;

	private bool affirm_;

	public const int PointFieldNumber = 3;

	private int point_;

	public const int PlayerIdFieldNumber = 4;

	private long playerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerVoteInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[60];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SelectId
	{
		get
		{
			return selectId_;
		}
		set
		{
			selectId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Affirm
	{
		get
		{
			return affirm_;
		}
		set
		{
			affirm_ = value;
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
	public PlayerVoteInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerVoteInfo(PlayerVoteInfo other)
		: this()
	{
		selectId_ = other.selectId_;
		affirm_ = other.affirm_;
		point_ = other.point_;
		playerId_ = other.playerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerVoteInfo Clone()
	{
		return new PlayerVoteInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerVoteInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerVoteInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SelectId != other.SelectId)
		{
			return false;
		}
		if (Affirm != other.Affirm)
		{
			return false;
		}
		if (Point != other.Point)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
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
		if (SelectId != 0)
		{
			num ^= SelectId.GetHashCode();
		}
		if (Affirm)
		{
			num ^= Affirm.GetHashCode();
		}
		if (Point != 0)
		{
			num ^= Point.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
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
		if (SelectId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(SelectId);
		}
		if (Affirm)
		{
			output.WriteRawTag(16);
			output.WriteBool(Affirm);
		}
		if (Point != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Point);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(PlayerId);
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
		if (SelectId != 0)
		{
			num += 5;
		}
		if (Affirm)
		{
			num += 2;
		}
		if (Point != 0)
		{
			num += 5;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PlayerVoteInfo other)
	{
		if (other != null)
		{
			if (other.SelectId != 0)
			{
				SelectId = other.SelectId;
			}
			if (other.Affirm)
			{
				Affirm = other.Affirm;
			}
			if (other.Point != 0)
			{
				Point = other.Point;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
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
				SelectId = input.ReadSFixed32();
				break;
			case 16u:
				Affirm = input.ReadBool();
				break;
			case 29u:
				Point = input.ReadSFixed32();
				break;
			case 33u:
				PlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
