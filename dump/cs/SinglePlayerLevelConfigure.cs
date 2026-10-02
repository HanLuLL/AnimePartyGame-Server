using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerLevelConfigure : IMessage<SinglePlayerLevelConfigure>, IMessage, IEquatable<SinglePlayerLevelConfigure>, IDeepCloneable<SinglePlayerLevelConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerLevelConfigure> _parser = new MessageParser<SinglePlayerLevelConfigure>(() => new SinglePlayerLevelConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int BuildingHeightFieldNumber = 3;

	private float buildingHeight_;

	public const int PlinthFieldNumber = 4;

	private static readonly FieldCodec<string> _repeated_plinth_codec = FieldCodec.ForString(34u);

	private readonly RepeatedField<string> plinth_ = new RepeatedField<string>();

	public const int DiceLevelProgressFieldNumber = 5;

	private int diceLevelProgress_;

	public const int BgmFieldNumber = 6;

	private int bgm_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerLevelConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[7];

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
	public float BuildingHeight
	{
		get
		{
			return buildingHeight_;
		}
		private set
		{
			buildingHeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Plinth => plinth_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceLevelProgress
	{
		get
		{
			return diceLevelProgress_;
		}
		private set
		{
			diceLevelProgress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Bgm
	{
		get
		{
			return bgm_;
		}
		private set
		{
			bgm_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelConfigure(SinglePlayerLevelConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		buildingHeight_ = other.buildingHeight_;
		plinth_ = other.plinth_.Clone();
		diceLevelProgress_ = other.diceLevelProgress_;
		bgm_ = other.bgm_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLevelConfigure Clone()
	{
		return new SinglePlayerLevelConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerLevelConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerLevelConfigure other)
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
		if (NameID != other.NameID)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(BuildingHeight, other.BuildingHeight))
		{
			return false;
		}
		if (!plinth_.Equals(other.plinth_))
		{
			return false;
		}
		if (DiceLevelProgress != other.DiceLevelProgress)
		{
			return false;
		}
		if (Bgm != other.Bgm)
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
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (BuildingHeight != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(BuildingHeight);
		}
		num ^= plinth_.GetHashCode();
		if (DiceLevelProgress != 0)
		{
			num ^= DiceLevelProgress.GetHashCode();
		}
		if (Bgm != 0)
		{
			num ^= Bgm.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (BuildingHeight != 0f)
		{
			output.WriteRawTag(29);
			output.WriteFloat(BuildingHeight);
		}
		plinth_.WriteTo(ref output, _repeated_plinth_codec);
		if (DiceLevelProgress != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DiceLevelProgress);
		}
		if (Bgm != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Bgm);
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
		if (NameID != 0)
		{
			num += 5;
		}
		if (BuildingHeight != 0f)
		{
			num += 5;
		}
		num += plinth_.CalculateSize(_repeated_plinth_codec);
		if (DiceLevelProgress != 0)
		{
			num += 5;
		}
		if (Bgm != 0)
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
	public void MergeFrom(SinglePlayerLevelConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.BuildingHeight != 0f)
			{
				BuildingHeight = other.BuildingHeight;
			}
			plinth_.Add(other.plinth_);
			if (other.DiceLevelProgress != 0)
			{
				DiceLevelProgress = other.DiceLevelProgress;
			}
			if (other.Bgm != 0)
			{
				Bgm = other.Bgm;
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
				NameID = input.ReadSFixed32();
				break;
			case 29u:
				BuildingHeight = input.ReadFloat();
				break;
			case 34u:
				plinth_.AddEntriesFrom(ref input, _repeated_plinth_codec);
				break;
			case 45u:
				DiceLevelProgress = input.ReadSFixed32();
				break;
			case 53u:
				Bgm = input.ReadSFixed32();
				break;
			}
		}
	}
}
