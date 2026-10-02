using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class LuckyStarBattleParamConfigure : IMessage<LuckyStarBattleParamConfigure>, IMessage, IEquatable<LuckyStarBattleParamConfigure>, IDeepCloneable<LuckyStarBattleParamConfigure>, IBufferMessage
{
	private static readonly MessageParser<LuckyStarBattleParamConfigure> _parser = new MessageParser<LuckyStarBattleParamConfigure>(() => new LuckyStarBattleParamConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NeedLuckyStarFieldNumber = 2;

	private int needLuckyStar_;

	public const int MissionCountFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_missionCount_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> missionCount_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LuckyStarBattleParamConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LuckyStarBattleReflection.Descriptor.MessageTypes[1];

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
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NeedLuckyStar
	{
		get
		{
			return needLuckyStar_;
		}
		private set
		{
			needLuckyStar_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MissionCount => missionCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleParamConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleParamConfigure(LuckyStarBattleParamConfigure other)
		: this()
	{
		id_ = other.id_;
		needLuckyStar_ = other.needLuckyStar_;
		missionCount_ = other.missionCount_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleParamConfigure Clone()
	{
		return new LuckyStarBattleParamConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LuckyStarBattleParamConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LuckyStarBattleParamConfigure other)
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
		if (NeedLuckyStar != other.NeedLuckyStar)
		{
			return false;
		}
		if (!missionCount_.Equals(other.missionCount_))
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
		if (NeedLuckyStar != 0)
		{
			num ^= NeedLuckyStar.GetHashCode();
		}
		num ^= missionCount_.GetHashCode();
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
		if (NeedLuckyStar != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NeedLuckyStar);
		}
		missionCount_.WriteTo(ref output, _repeated_missionCount_codec);
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
		if (NeedLuckyStar != 0)
		{
			num += 5;
		}
		num += missionCount_.CalculateSize(_repeated_missionCount_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LuckyStarBattleParamConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NeedLuckyStar != 0)
			{
				NeedLuckyStar = other.NeedLuckyStar;
			}
			missionCount_.Add(other.missionCount_);
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
			case 21u:
				NeedLuckyStar = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				missionCount_.AddEntriesFrom(ref input, _repeated_missionCount_codec);
				break;
			}
		}
	}
}
