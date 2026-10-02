using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MutatorInfoConfigure : IMessage<MutatorInfoConfigure>, IMessage, IEquatable<MutatorInfoConfigure>, IDeepCloneable<MutatorInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<MutatorInfoConfigure> _parser = new MessageParser<MutatorInfoConfigure>(() => new MutatorInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int DescIdFieldNumber = 3;

	private int descId_;

	public const int MutatorTypeFieldNumber = 4;

	private static readonly FieldCodec<MutatorType> _repeated_mutatorType_codec = FieldCodec.ForEnum(34u, (MutatorType x) => (int)x, (int x) => (MutatorType)x);

	private readonly RepeatedField<MutatorType> mutatorType_ = new RepeatedField<MutatorType>();

	public const int IconFieldNumber = 5;

	private string icon_ = "";

	public const int ParamsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(50u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_buffId_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> buffId_ = new RepeatedField<int>();

	public const int PreloadCharacterIdsFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_preloadCharacterIds_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> preloadCharacterIds_ = new RepeatedField<int>();

	public const int PerformsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_performs_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> performs_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MutatorInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MutatorReflection.Descriptor.MessageTypes[0];

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
	public int DescId
	{
		get
		{
			return descId_;
		}
		private set
		{
			descId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MutatorType> MutatorType => mutatorType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public RepeatedField<int> PreloadCharacterIds => preloadCharacterIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Performs => performs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorInfoConfigure(MutatorInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		descId_ = other.descId_;
		mutatorType_ = other.mutatorType_.Clone();
		icon_ = other.icon_;
		params_ = other.params_.Clone();
		buffId_ = other.buffId_.Clone();
		preloadCharacterIds_ = other.preloadCharacterIds_.Clone();
		performs_ = other.performs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorInfoConfigure Clone()
	{
		return new MutatorInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MutatorInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MutatorInfoConfigure other)
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
		if (DescId != other.DescId)
		{
			return false;
		}
		if (!mutatorType_.Equals(other.mutatorType_))
		{
			return false;
		}
		if (Icon != other.Icon)
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
		if (!preloadCharacterIds_.Equals(other.preloadCharacterIds_))
		{
			return false;
		}
		if (!performs_.Equals(other.performs_))
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
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		num ^= mutatorType_.GetHashCode();
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		num ^= params_.GetHashCode();
		num ^= buffId_.GetHashCode();
		num ^= preloadCharacterIds_.GetHashCode();
		num ^= performs_.GetHashCode();
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
		if (DescId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescId);
		}
		mutatorType_.WriteTo(ref output, _repeated_mutatorType_codec);
		if (Icon.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Icon);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		buffId_.WriteTo(ref output, _repeated_buffId_codec);
		preloadCharacterIds_.WriteTo(ref output, _repeated_preloadCharacterIds_codec);
		performs_.WriteTo(ref output, _repeated_performs_codec);
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
		if (DescId != 0)
		{
			num += 5;
		}
		num += mutatorType_.CalculateSize(_repeated_mutatorType_codec);
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		num += params_.CalculateSize(_repeated_params_codec);
		num += buffId_.CalculateSize(_repeated_buffId_codec);
		num += preloadCharacterIds_.CalculateSize(_repeated_preloadCharacterIds_codec);
		num += performs_.CalculateSize(_repeated_performs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MutatorInfoConfigure other)
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
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			mutatorType_.Add(other.mutatorType_);
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			params_.Add(other.params_);
			buffId_.Add(other.buffId_);
			preloadCharacterIds_.Add(other.preloadCharacterIds_);
			performs_.Add(other.performs_);
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
				DescId = input.ReadSFixed32();
				break;
			case 32u:
			case 34u:
				mutatorType_.AddEntriesFrom(ref input, _repeated_mutatorType_codec);
				break;
			case 42u:
				Icon = input.ReadString();
				break;
			case 48u:
			case 50u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 58u:
			case 61u:
				buffId_.AddEntriesFrom(ref input, _repeated_buffId_codec);
				break;
			case 66u:
			case 69u:
				preloadCharacterIds_.AddEntriesFrom(ref input, _repeated_preloadCharacterIds_codec);
				break;
			case 74u:
			case 77u:
				performs_.AddEntriesFrom(ref input, _repeated_performs_codec);
				break;
			}
		}
	}
}
