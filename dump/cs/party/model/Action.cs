using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Action : IMessage<Action>, IMessage, IEquatable<Action>, IDeepCloneable<Action>, IBufferMessage
{
	private static readonly MessageParser<Action> _parser = new MessageParser<Action>(() => new Action());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PlayerIdFieldNumber = 2;

	private long playerId_;

	public const int DataFieldNumber = 3;

	private ByteString data_ = ByteString.Empty;

	public const int SnFieldNumber = 4;

	private long sn_;

	public const int NotBroFieldNumber = 5;

	private bool notBro_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Action> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[83];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
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
	public ByteString Data
	{
		get
		{
			return data_;
		}
		set
		{
			data_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Sn
	{
		get
		{
			return sn_;
		}
		set
		{
			sn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NotBro
	{
		get
		{
			return notBro_;
		}
		set
		{
			notBro_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Action()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Action(Action other)
		: this()
	{
		id_ = other.id_;
		playerId_ = other.playerId_;
		data_ = other.data_;
		sn_ = other.sn_;
		notBro_ = other.notBro_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Action Clone()
	{
		return new Action(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Action);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Action other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (Data != other.Data)
		{
			return false;
		}
		if (Sn != other.Sn)
		{
			return false;
		}
		if (NotBro != other.NotBro)
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (Data.Length != 0)
		{
			num ^= Data.GetHashCode();
		}
		if (Sn != 0L)
		{
			num ^= Sn.GetHashCode();
		}
		if (NotBro)
		{
			num ^= NotBro.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PlayerId);
		}
		if (Data.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteBytes(Data);
		}
		if (Sn != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(Sn);
		}
		if (NotBro)
		{
			output.WriteRawTag(40);
			output.WriteBool(NotBro);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (Data.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeBytesSize(Data);
		}
		if (Sn != 0L)
		{
			num += 9;
		}
		if (NotBro)
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
	public void MergeFrom(Action other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Data.Length != 0)
			{
				Data = other.Data;
			}
			if (other.Sn != 0L)
			{
				Sn = other.Sn;
			}
			if (other.NotBro)
			{
				NotBro = other.NotBro;
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
				Id = input.ReadSFixed32();
				break;
			case 17u:
				PlayerId = input.ReadSFixed64();
				break;
			case 26u:
				Data = input.ReadBytes();
				break;
			case 33u:
				Sn = input.ReadSFixed64();
				break;
			case 40u:
				NotBro = input.ReadBool();
				break;
			}
		}
	}
}
