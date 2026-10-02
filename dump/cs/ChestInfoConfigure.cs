using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChestInfoConfigure : IMessage<ChestInfoConfigure>, IMessage, IEquatable<ChestInfoConfigure>, IDeepCloneable<ChestInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChestInfoConfigure> _parser = new MessageParser<ChestInfoConfigure>(() => new ChestInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int IsOptionalFieldNumber = 2;

	private bool isOptional_;

	public const int RandomRewardFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_randomReward_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> randomReward_ = new RepeatedField<int>();

	public const int OptionalRewardFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_optionalReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> optionalReward_ = new MapField<int, int>();

	public const int ContentTypeFieldNumber = 5;

	private int contentType_;

	public const int UseMaxFieldNumber = 6;

	private int useMax_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChestInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChestReflection.Descriptor.MessageTypes[0];

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
	public bool IsOptional
	{
		get
		{
			return isOptional_;
		}
		private set
		{
			isOptional_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RandomReward => randomReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> OptionalReward => optionalReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ContentType
	{
		get
		{
			return contentType_;
		}
		private set
		{
			contentType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseMax
	{
		get
		{
			return useMax_;
		}
		private set
		{
			useMax_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestInfoConfigure(ChestInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		isOptional_ = other.isOptional_;
		randomReward_ = other.randomReward_.Clone();
		optionalReward_ = other.optionalReward_.Clone();
		contentType_ = other.contentType_;
		useMax_ = other.useMax_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChestInfoConfigure Clone()
	{
		return new ChestInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChestInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChestInfoConfigure other)
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
		if (IsOptional != other.IsOptional)
		{
			return false;
		}
		if (!randomReward_.Equals(other.randomReward_))
		{
			return false;
		}
		if (!OptionalReward.Equals(other.OptionalReward))
		{
			return false;
		}
		if (ContentType != other.ContentType)
		{
			return false;
		}
		if (UseMax != other.UseMax)
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
		if (IsOptional)
		{
			num ^= IsOptional.GetHashCode();
		}
		num ^= randomReward_.GetHashCode();
		num ^= OptionalReward.GetHashCode();
		if (ContentType != 0)
		{
			num ^= ContentType.GetHashCode();
		}
		if (UseMax != 0)
		{
			num ^= UseMax.GetHashCode();
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
		if (IsOptional)
		{
			output.WriteRawTag(16);
			output.WriteBool(IsOptional);
		}
		randomReward_.WriteTo(ref output, _repeated_randomReward_codec);
		optionalReward_.WriteTo(ref output, _map_optionalReward_codec);
		if (ContentType != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(ContentType);
		}
		if (UseMax != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(UseMax);
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
		if (IsOptional)
		{
			num += 2;
		}
		num += randomReward_.CalculateSize(_repeated_randomReward_codec);
		num += optionalReward_.CalculateSize(_map_optionalReward_codec);
		if (ContentType != 0)
		{
			num += 5;
		}
		if (UseMax != 0)
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
	public void MergeFrom(ChestInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.IsOptional)
			{
				IsOptional = other.IsOptional;
			}
			randomReward_.Add(other.randomReward_);
			optionalReward_.MergeFrom(other.optionalReward_);
			if (other.ContentType != 0)
			{
				ContentType = other.ContentType;
			}
			if (other.UseMax != 0)
			{
				UseMax = other.UseMax;
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
			case 16u:
				IsOptional = input.ReadBool();
				break;
			case 26u:
			case 29u:
				randomReward_.AddEntriesFrom(ref input, _repeated_randomReward_codec);
				break;
			case 34u:
				optionalReward_.AddEntriesFrom(ref input, _map_optionalReward_codec);
				break;
			case 45u:
				ContentType = input.ReadSFixed32();
				break;
			case 53u:
				UseMax = input.ReadSFixed32();
				break;
			}
		}
	}
}
