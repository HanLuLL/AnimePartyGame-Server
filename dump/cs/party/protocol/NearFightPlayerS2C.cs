using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class NearFightPlayerS2C : IMessage<NearFightPlayerS2C>, IMessage, IEquatable<NearFightPlayerS2C>, IDeepCloneable<NearFightPlayerS2C>, IBufferMessage
{
	private static readonly MessageParser<NearFightPlayerS2C> _parser = new MessageParser<NearFightPlayerS2C>(() => new NearFightPlayerS2C());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<FriendShowPlayerInfo> _repeated_infos_codec = FieldCodec.ForMessage(10u, FriendShowPlayerInfo.Parser);

	private readonly RepeatedField<FriendShowPlayerInfo> infos_ = new RepeatedField<FriendShowPlayerInfo>();

	public const int IsEndFieldNumber = 2;

	private bool isEnd_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<NearFightPlayerS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[101];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendShowPlayerInfo> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsEnd
	{
		get
		{
			return isEnd_;
		}
		set
		{
			isEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerS2C(NearFightPlayerS2C other)
		: this()
	{
		infos_ = other.infos_.Clone();
		isEnd_ = other.isEnd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerS2C Clone()
	{
		return new NearFightPlayerS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NearFightPlayerS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(NearFightPlayerS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (IsEnd != other.IsEnd)
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
		num ^= infos_.GetHashCode();
		if (IsEnd)
		{
			num ^= IsEnd.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		if (IsEnd)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsEnd);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		if (IsEnd)
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
	public void MergeFrom(NearFightPlayerS2C other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			if (other.IsEnd)
			{
				IsEnd = other.IsEnd;
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
			case 10u:
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 16u:
				IsEnd = input.ReadBool();
				break;
			}
		}
	}
}
