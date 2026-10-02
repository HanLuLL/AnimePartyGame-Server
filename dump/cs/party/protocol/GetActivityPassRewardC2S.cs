using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GetActivityPassRewardC2S : IMessage<GetActivityPassRewardC2S>, IMessage, IEquatable<GetActivityPassRewardC2S>, IDeepCloneable<GetActivityPassRewardC2S>, IBufferMessage
{
	private static readonly MessageParser<GetActivityPassRewardC2S> _parser = new MessageParser<GetActivityPassRewardC2S>(() => new GetActivityPassRewardC2S());

	private UnknownFieldSet _unknownFields;

	public const int MissionIdsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_missionIds_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> missionIds_ = new RepeatedField<int>();

	public const int DefIdFieldNumber = 2;

	private int defId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GetActivityPassRewardC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[523];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MissionIds => missionIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefId
	{
		get
		{
			return defId_;
		}
		set
		{
			defId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardC2S(GetActivityPassRewardC2S other)
		: this()
	{
		missionIds_ = other.missionIds_.Clone();
		defId_ = other.defId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardC2S Clone()
	{
		return new GetActivityPassRewardC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GetActivityPassRewardC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GetActivityPassRewardC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!missionIds_.Equals(other.missionIds_))
		{
			return false;
		}
		if (DefId != other.DefId)
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
		num ^= missionIds_.GetHashCode();
		if (DefId != 0)
		{
			num ^= DefId.GetHashCode();
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
		missionIds_.WriteTo(ref output, _repeated_missionIds_codec);
		if (DefId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(DefId);
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
		num += missionIds_.CalculateSize(_repeated_missionIds_codec);
		if (DefId != 0)
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
	public void MergeFrom(GetActivityPassRewardC2S other)
	{
		if (other != null)
		{
			missionIds_.Add(other.missionIds_);
			if (other.DefId != 0)
			{
				DefId = other.DefId;
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
			case 13u:
				missionIds_.AddEntriesFrom(ref input, _repeated_missionIds_codec);
				break;
			case 21u:
				DefId = input.ReadSFixed32();
				break;
			}
		}
	}
}
