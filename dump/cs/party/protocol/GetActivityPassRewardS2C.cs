using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GetActivityPassRewardS2C : IMessage<GetActivityPassRewardS2C>, IMessage, IEquatable<GetActivityPassRewardS2C>, IDeepCloneable<GetActivityPassRewardS2C>, IBufferMessage
{
	private static readonly MessageParser<GetActivityPassRewardS2C> _parser = new MessageParser<GetActivityPassRewardS2C>(() => new GetActivityPassRewardS2C());

	private UnknownFieldSet _unknownFields;

	public const int MissionIdsFieldNumber = 1;

	private static readonly FieldCodec<int> _repeated_missionIds_codec = FieldCodec.ForSFixed32(10u);

	private readonly RepeatedField<int> missionIds_ = new RepeatedField<int>();

	public const int DefIdFieldNumber = 2;

	private int defId_;

	public const int GearFieldNumber = 3;

	private int gear_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GetActivityPassRewardS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[524];

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
	public int Gear
	{
		get
		{
			return gear_;
		}
		set
		{
			gear_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardS2C(GetActivityPassRewardS2C other)
		: this()
	{
		missionIds_ = other.missionIds_.Clone();
		defId_ = other.defId_;
		gear_ = other.gear_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardS2C Clone()
	{
		return new GetActivityPassRewardS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GetActivityPassRewardS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GetActivityPassRewardS2C other)
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
		if (Gear != other.Gear)
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
		if (Gear != 0)
		{
			num ^= Gear.GetHashCode();
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
		if (Gear != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Gear);
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
		if (Gear != 0)
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
	public void MergeFrom(GetActivityPassRewardS2C other)
	{
		if (other != null)
		{
			missionIds_.Add(other.missionIds_);
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			if (other.Gear != 0)
			{
				Gear = other.Gear;
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
			case 29u:
				Gear = input.ReadSFixed32();
				break;
			}
		}
	}
}
