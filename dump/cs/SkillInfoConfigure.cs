using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SkillInfoConfigure : IMessage<SkillInfoConfigure>, IMessage, IEquatable<SkillInfoConfigure>, IDeepCloneable<SkillInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<SkillInfoConfigure> _parser = new MessageParser<SkillInfoConfigure>(() => new SkillInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SkillTypeFieldNumber = 2;

	private SkillType skillType_;

	public const int IsShowFieldNumber = 3;

	private bool isShow_;

	public const int NameIDFieldNumber = 4;

	private int nameID_;

	public const int DescIDFieldNumber = 5;

	private int descID_;

	public const int RoundFieldNumber = 6;

	private int round_;

	public const int ParamsFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(58u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_buffId_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> buffId_ = new RepeatedField<int>();

	public const int PerformTargetsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_performTargets_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> performTargets_ = new RepeatedField<int>();

	public const int PerformSelfsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_performSelfs_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> performSelfs_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkillInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkillReflection.Descriptor.MessageTypes[0];

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
	public SkillType SkillType
	{
		get
		{
			return skillType_;
		}
		private set
		{
			skillType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShow
	{
		get
		{
			return isShow_;
		}
		private set
		{
			isShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescID
	{
		get
		{
			return descID_;
		}
		private set
		{
			descID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Round
	{
		get
		{
			return round_;
		}
		private set
		{
			round_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffId => buffId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PerformTargets => performTargets_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PerformSelfs => performSelfs_;

	public int PerformTarget
	{
		get
		{
			if (PerformTargets.Count > 0)
			{
				return PerformTargets[0];
			}
			return 0;
		}
	}

	public int PerformSelf
	{
		get
		{
			if (PerformSelfs.Count > 0)
			{
				return PerformSelfs[0];
			}
			return 0;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkillInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkillInfoConfigure(SkillInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		skillType_ = other.skillType_;
		isShow_ = other.isShow_;
		nameID_ = other.nameID_;
		descID_ = other.descID_;
		round_ = other.round_;
		params_ = other.params_.Clone();
		buffId_ = other.buffId_.Clone();
		performTargets_ = other.performTargets_.Clone();
		performSelfs_ = other.performSelfs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkillInfoConfigure Clone()
	{
		return new SkillInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkillInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkillInfoConfigure other)
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
		if (SkillType != other.SkillType)
		{
			return false;
		}
		if (IsShow != other.IsShow)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DescID != other.DescID)
		{
			return false;
		}
		if (Round != other.Round)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (!buffId_.Equals(other.buffId_))
		{
			return false;
		}
		if (!performTargets_.Equals(other.performTargets_))
		{
			return false;
		}
		if (!performSelfs_.Equals(other.performSelfs_))
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
		if (SkillType != SkillType.None)
		{
			num ^= SkillType.GetHashCode();
		}
		if (IsShow)
		{
			num ^= IsShow.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DescID != 0)
		{
			num ^= DescID.GetHashCode();
		}
		if (Round != 0)
		{
			num ^= Round.GetHashCode();
		}
		num ^= params_.GetHashCode();
		num ^= buffId_.GetHashCode();
		num ^= performTargets_.GetHashCode();
		num ^= performSelfs_.GetHashCode();
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
		if (SkillType != SkillType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)SkillType);
		}
		if (IsShow)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsShow);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(NameID);
		}
		if (DescID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DescID);
		}
		if (Round != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Round);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		buffId_.WriteTo(ref output, _repeated_buffId_codec);
		performTargets_.WriteTo(ref output, _repeated_performTargets_codec);
		performSelfs_.WriteTo(ref output, _repeated_performSelfs_codec);
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
		if (SkillType != SkillType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SkillType);
		}
		if (IsShow)
		{
			num += 2;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (DescID != 0)
		{
			num += 5;
		}
		if (Round != 0)
		{
			num += 5;
		}
		num += params_.CalculateSize(_repeated_params_codec);
		num += buffId_.CalculateSize(_repeated_buffId_codec);
		num += performTargets_.CalculateSize(_repeated_performTargets_codec);
		num += performSelfs_.CalculateSize(_repeated_performSelfs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SkillInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.SkillType != SkillType.None)
			{
				SkillType = other.SkillType;
			}
			if (other.IsShow)
			{
				IsShow = other.IsShow;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.DescID != 0)
			{
				DescID = other.DescID;
			}
			if (other.Round != 0)
			{
				Round = other.Round;
			}
			params_.Add(other.params_);
			buffId_.Add(other.buffId_);
			performTargets_.Add(other.performTargets_);
			performSelfs_.Add(other.performSelfs_);
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
			case 16u:
				SkillType = (SkillType)input.ReadEnum();
				break;
			case 24u:
				IsShow = input.ReadBool();
				break;
			case 37u:
				NameID = input.ReadSFixed32();
				break;
			case 45u:
				DescID = input.ReadSFixed32();
				break;
			case 53u:
				Round = input.ReadSFixed32();
				break;
			case 56u:
			case 58u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 66u:
			case 69u:
				buffId_.AddEntriesFrom(ref input, _repeated_buffId_codec);
				break;
			case 74u:
			case 77u:
				performTargets_.AddEntriesFrom(ref input, _repeated_performTargets_codec);
				break;
			case 82u:
			case 85u:
				performSelfs_.AddEntriesFrom(ref input, _repeated_performSelfs_codec);
				break;
			}
		}
	}
}
