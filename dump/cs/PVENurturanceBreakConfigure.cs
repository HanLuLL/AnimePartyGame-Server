using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PVENurturanceBreakConfigure : IMessage<PVENurturanceBreakConfigure>, IMessage, IEquatable<PVENurturanceBreakConfigure>, IDeepCloneable<PVENurturanceBreakConfigure>, IBufferMessage
{
	private static readonly MessageParser<PVENurturanceBreakConfigure> _parser = new MessageParser<PVENurturanceBreakConfigure>(() => new PVENurturanceBreakConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ReplaceActiveSkillFieldNumber = 2;

	private int replaceActiveSkill_;

	public const int DelPassiveSkillsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_delPassiveSkills_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> delPassiveSkills_ = new RepeatedField<int>();

	public const int AddPassiveSkillsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_addPassiveSkills_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> addPassiveSkills_ = new RepeatedField<int>();

	public const int DescriptionIDFieldNumber = 5;

	private int descriptionID_;

	public const int NeedMaterialsFieldNumber = 6;

	private static readonly MapField<int, int>.Codec _map_needMaterials_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<int, int> needMaterials_ = new MapField<int, int>();

	public const int UnlockNeedPVELevelFieldNumber = 7;

	private int unlockNeedPVELevel_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVENurturanceBreakConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PVENurturanceReflection.Descriptor.MessageTypes[4];

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
	public int ReplaceActiveSkill
	{
		get
		{
			return replaceActiveSkill_;
		}
		private set
		{
			replaceActiveSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> DelPassiveSkills => delPassiveSkills_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> AddPassiveSkills => addPassiveSkills_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescriptionID
	{
		get
		{
			return descriptionID_;
		}
		private set
		{
			descriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> NeedMaterials => needMaterials_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UnlockNeedPVELevel
	{
		get
		{
			return unlockNeedPVELevel_;
		}
		private set
		{
			unlockNeedPVELevel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceBreakConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceBreakConfigure(PVENurturanceBreakConfigure other)
		: this()
	{
		id_ = other.id_;
		replaceActiveSkill_ = other.replaceActiveSkill_;
		delPassiveSkills_ = other.delPassiveSkills_.Clone();
		addPassiveSkills_ = other.addPassiveSkills_.Clone();
		descriptionID_ = other.descriptionID_;
		needMaterials_ = other.needMaterials_.Clone();
		unlockNeedPVELevel_ = other.unlockNeedPVELevel_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVENurturanceBreakConfigure Clone()
	{
		return new PVENurturanceBreakConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVENurturanceBreakConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVENurturanceBreakConfigure other)
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
		if (ReplaceActiveSkill != other.ReplaceActiveSkill)
		{
			return false;
		}
		if (!delPassiveSkills_.Equals(other.delPassiveSkills_))
		{
			return false;
		}
		if (!addPassiveSkills_.Equals(other.addPassiveSkills_))
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (!NeedMaterials.Equals(other.NeedMaterials))
		{
			return false;
		}
		if (UnlockNeedPVELevel != other.UnlockNeedPVELevel)
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
		if (ReplaceActiveSkill != 0)
		{
			num ^= ReplaceActiveSkill.GetHashCode();
		}
		num ^= delPassiveSkills_.GetHashCode();
		num ^= addPassiveSkills_.GetHashCode();
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		num ^= NeedMaterials.GetHashCode();
		if (UnlockNeedPVELevel != 0)
		{
			num ^= UnlockNeedPVELevel.GetHashCode();
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
		if (ReplaceActiveSkill != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ReplaceActiveSkill);
		}
		delPassiveSkills_.WriteTo(ref output, _repeated_delPassiveSkills_codec);
		addPassiveSkills_.WriteTo(ref output, _repeated_addPassiveSkills_codec);
		if (DescriptionID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DescriptionID);
		}
		needMaterials_.WriteTo(ref output, _map_needMaterials_codec);
		if (UnlockNeedPVELevel != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(UnlockNeedPVELevel);
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
		if (ReplaceActiveSkill != 0)
		{
			num += 5;
		}
		num += delPassiveSkills_.CalculateSize(_repeated_delPassiveSkills_codec);
		num += addPassiveSkills_.CalculateSize(_repeated_addPassiveSkills_codec);
		if (DescriptionID != 0)
		{
			num += 5;
		}
		num += needMaterials_.CalculateSize(_map_needMaterials_codec);
		if (UnlockNeedPVELevel != 0)
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
	public void MergeFrom(PVENurturanceBreakConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ReplaceActiveSkill != 0)
			{
				ReplaceActiveSkill = other.ReplaceActiveSkill;
			}
			delPassiveSkills_.Add(other.delPassiveSkills_);
			addPassiveSkills_.Add(other.addPassiveSkills_);
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			needMaterials_.MergeFrom(other.needMaterials_);
			if (other.UnlockNeedPVELevel != 0)
			{
				UnlockNeedPVELevel = other.UnlockNeedPVELevel;
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
			case 21u:
				ReplaceActiveSkill = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				delPassiveSkills_.AddEntriesFrom(ref input, _repeated_delPassiveSkills_codec);
				break;
			case 34u:
			case 37u:
				addPassiveSkills_.AddEntriesFrom(ref input, _repeated_addPassiveSkills_codec);
				break;
			case 45u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 50u:
				needMaterials_.AddEntriesFrom(ref input, _map_needMaterials_codec);
				break;
			case 61u:
				UnlockNeedPVELevel = input.ReadSFixed32();
				break;
			}
		}
	}
}
