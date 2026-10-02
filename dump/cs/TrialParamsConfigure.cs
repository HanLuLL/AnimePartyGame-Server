using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class TrialParamsConfigure : IMessage<TrialParamsConfigure>, IMessage, IEquatable<TrialParamsConfigure>, IDeepCloneable<TrialParamsConfigure>, IBufferMessage
{
	private static readonly MessageParser<TrialParamsConfigure> _parser = new MessageParser<TrialParamsConfigure>(() => new TrialParamsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PveLevelFieldNumber = 2;

	private int pveLevel_;

	public const int PlayerLevelFieldNumber = 3;

	private int playerLevel_;

	public const int HeroIDsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_heroIDs_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> heroIDs_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TrialParamsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TrialReflection.Descriptor.MessageTypes[1];

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
	public int PveLevel
	{
		get
		{
			return pveLevel_;
		}
		private set
		{
			pveLevel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PlayerLevel
	{
		get
		{
			return playerLevel_;
		}
		private set
		{
			playerLevel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> HeroIDs => heroIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TrialParamsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TrialParamsConfigure(TrialParamsConfigure other)
		: this()
	{
		id_ = other.id_;
		pveLevel_ = other.pveLevel_;
		playerLevel_ = other.playerLevel_;
		heroIDs_ = other.heroIDs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TrialParamsConfigure Clone()
	{
		return new TrialParamsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TrialParamsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TrialParamsConfigure other)
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
		if (PveLevel != other.PveLevel)
		{
			return false;
		}
		if (PlayerLevel != other.PlayerLevel)
		{
			return false;
		}
		if (!heroIDs_.Equals(other.heroIDs_))
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
		if (PveLevel != 0)
		{
			num ^= PveLevel.GetHashCode();
		}
		if (PlayerLevel != 0)
		{
			num ^= PlayerLevel.GetHashCode();
		}
		num ^= heroIDs_.GetHashCode();
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
		if (PveLevel != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(PveLevel);
		}
		if (PlayerLevel != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PlayerLevel);
		}
		heroIDs_.WriteTo(ref output, _repeated_heroIDs_codec);
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
		if (PveLevel != 0)
		{
			num += 5;
		}
		if (PlayerLevel != 0)
		{
			num += 5;
		}
		num += heroIDs_.CalculateSize(_repeated_heroIDs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TrialParamsConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PveLevel != 0)
			{
				PveLevel = other.PveLevel;
			}
			if (other.PlayerLevel != 0)
			{
				PlayerLevel = other.PlayerLevel;
			}
			heroIDs_.Add(other.heroIDs_);
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
				PveLevel = input.ReadSFixed32();
				break;
			case 29u:
				PlayerLevel = input.ReadSFixed32();
				break;
			case 34u:
			case 37u:
				heroIDs_.AddEntriesFrom(ref input, _repeated_heroIDs_codec);
				break;
			}
		}
	}
}
