using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CampaignTriggerConfigure : IMessage<CampaignTriggerConfigure>, IMessage, IEquatable<CampaignTriggerConfigure>, IDeepCloneable<CampaignTriggerConfigure>, IBufferMessage
{
	private static readonly MessageParser<CampaignTriggerConfigure> _parser = new MessageParser<CampaignTriggerConfigure>(() => new CampaignTriggerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int CampaignTriggerTypeFieldNumber = 2;

	private static readonly FieldCodec<CampaignTriggerType> _repeated_campaignTriggerType_codec = FieldCodec.ForEnum(18u, (CampaignTriggerType x) => (int)x, (int x) => (CampaignTriggerType)x);

	private readonly RepeatedField<CampaignTriggerType> campaignTriggerType_ = new RepeatedField<CampaignTriggerType>();

	public const int CampaignTriggerParamsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_campaignTriggerParams_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> campaignTriggerParams_ = new RepeatedField<int>();

	public const int TutorialIDFieldNumber = 4;

	private int tutorialID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CampaignTriggerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CampaignReflection.Descriptor.MessageTypes[3];

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
	public RepeatedField<CampaignTriggerType> CampaignTriggerType => campaignTriggerType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CampaignTriggerParams => campaignTriggerParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TutorialID
	{
		get
		{
			return tutorialID_;
		}
		private set
		{
			tutorialID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignTriggerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignTriggerConfigure(CampaignTriggerConfigure other)
		: this()
	{
		id_ = other.id_;
		campaignTriggerType_ = other.campaignTriggerType_.Clone();
		campaignTriggerParams_ = other.campaignTriggerParams_.Clone();
		tutorialID_ = other.tutorialID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignTriggerConfigure Clone()
	{
		return new CampaignTriggerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CampaignTriggerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CampaignTriggerConfigure other)
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
		if (!campaignTriggerType_.Equals(other.campaignTriggerType_))
		{
			return false;
		}
		if (!campaignTriggerParams_.Equals(other.campaignTriggerParams_))
		{
			return false;
		}
		if (TutorialID != other.TutorialID)
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
		num ^= campaignTriggerType_.GetHashCode();
		num ^= campaignTriggerParams_.GetHashCode();
		if (TutorialID != 0)
		{
			num ^= TutorialID.GetHashCode();
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
		campaignTriggerType_.WriteTo(ref output, _repeated_campaignTriggerType_codec);
		campaignTriggerParams_.WriteTo(ref output, _repeated_campaignTriggerParams_codec);
		if (TutorialID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TutorialID);
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
		num += campaignTriggerType_.CalculateSize(_repeated_campaignTriggerType_codec);
		num += campaignTriggerParams_.CalculateSize(_repeated_campaignTriggerParams_codec);
		if (TutorialID != 0)
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
	public void MergeFrom(CampaignTriggerConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			campaignTriggerType_.Add(other.campaignTriggerType_);
			campaignTriggerParams_.Add(other.campaignTriggerParams_);
			if (other.TutorialID != 0)
			{
				TutorialID = other.TutorialID;
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
			case 18u:
				campaignTriggerType_.AddEntriesFrom(ref input, _repeated_campaignTriggerType_codec);
				break;
			case 26u:
			case 29u:
				campaignTriggerParams_.AddEntriesFrom(ref input, _repeated_campaignTriggerParams_codec);
				break;
			case 37u:
				TutorialID = input.ReadSFixed32();
				break;
			}
		}
	}
}
