using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ActivityLightingConfigure : IMessage<ActivityLightingConfigure>, IMessage, IEquatable<ActivityLightingConfigure>, IDeepCloneable<ActivityLightingConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityLightingConfigure> _parser = new MessageParser<ActivityLightingConfigure>(() => new ActivityLightingConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ScratchoffPoolIdFieldNumber = 2;

	private int scratchoffPoolId_;

	public const int SpendsFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_spends_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> spends_ = new MapField<int, int>();

	public const int TargetItemIDFieldNumber = 4;

	private int targetItemID_;

	public const int SkinIDFieldNumber = 5;

	private int skinID_;

	public const int AccountBackgroundIDFieldNumber = 6;

	private int accountBackgroundID_;

	public const int DecriptionIDFieldNumber = 7;

	private int decriptionID_;

	public const int RuleTitleIDFieldNumber = 8;

	private int ruleTitleID_;

	public const int RuleDecriptionIDFieldNumber = 9;

	private int ruleDecriptionID_;

	public const int RuleSheetField1FieldNumber = 10;

	private int ruleSheetField1_;

	public const int RuleSheetField2FieldNumber = 11;

	private int ruleSheetField2_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityLightingConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[4];

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
	public int ScratchoffPoolId
	{
		get
		{
			return scratchoffPoolId_;
		}
		private set
		{
			scratchoffPoolId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Spends => spends_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TargetItemID
	{
		get
		{
			return targetItemID_;
		}
		private set
		{
			targetItemID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinID
	{
		get
		{
			return skinID_;
		}
		private set
		{
			skinID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AccountBackgroundID
	{
		get
		{
			return accountBackgroundID_;
		}
		private set
		{
			accountBackgroundID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DecriptionID
	{
		get
		{
			return decriptionID_;
		}
		private set
		{
			decriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RuleTitleID
	{
		get
		{
			return ruleTitleID_;
		}
		private set
		{
			ruleTitleID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RuleDecriptionID
	{
		get
		{
			return ruleDecriptionID_;
		}
		private set
		{
			ruleDecriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RuleSheetField1
	{
		get
		{
			return ruleSheetField1_;
		}
		private set
		{
			ruleSheetField1_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RuleSheetField2
	{
		get
		{
			return ruleSheetField2_;
		}
		private set
		{
			ruleSheetField2_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityLightingConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityLightingConfigure(ActivityLightingConfigure other)
		: this()
	{
		id_ = other.id_;
		scratchoffPoolId_ = other.scratchoffPoolId_;
		spends_ = other.spends_.Clone();
		targetItemID_ = other.targetItemID_;
		skinID_ = other.skinID_;
		accountBackgroundID_ = other.accountBackgroundID_;
		decriptionID_ = other.decriptionID_;
		ruleTitleID_ = other.ruleTitleID_;
		ruleDecriptionID_ = other.ruleDecriptionID_;
		ruleSheetField1_ = other.ruleSheetField1_;
		ruleSheetField2_ = other.ruleSheetField2_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityLightingConfigure Clone()
	{
		return new ActivityLightingConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityLightingConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityLightingConfigure other)
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
		if (ScratchoffPoolId != other.ScratchoffPoolId)
		{
			return false;
		}
		if (!Spends.Equals(other.Spends))
		{
			return false;
		}
		if (TargetItemID != other.TargetItemID)
		{
			return false;
		}
		if (SkinID != other.SkinID)
		{
			return false;
		}
		if (AccountBackgroundID != other.AccountBackgroundID)
		{
			return false;
		}
		if (DecriptionID != other.DecriptionID)
		{
			return false;
		}
		if (RuleTitleID != other.RuleTitleID)
		{
			return false;
		}
		if (RuleDecriptionID != other.RuleDecriptionID)
		{
			return false;
		}
		if (RuleSheetField1 != other.RuleSheetField1)
		{
			return false;
		}
		if (RuleSheetField2 != other.RuleSheetField2)
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
		if (ScratchoffPoolId != 0)
		{
			num ^= ScratchoffPoolId.GetHashCode();
		}
		num ^= Spends.GetHashCode();
		if (TargetItemID != 0)
		{
			num ^= TargetItemID.GetHashCode();
		}
		if (SkinID != 0)
		{
			num ^= SkinID.GetHashCode();
		}
		if (AccountBackgroundID != 0)
		{
			num ^= AccountBackgroundID.GetHashCode();
		}
		if (DecriptionID != 0)
		{
			num ^= DecriptionID.GetHashCode();
		}
		if (RuleTitleID != 0)
		{
			num ^= RuleTitleID.GetHashCode();
		}
		if (RuleDecriptionID != 0)
		{
			num ^= RuleDecriptionID.GetHashCode();
		}
		if (RuleSheetField1 != 0)
		{
			num ^= RuleSheetField1.GetHashCode();
		}
		if (RuleSheetField2 != 0)
		{
			num ^= RuleSheetField2.GetHashCode();
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
		if (ScratchoffPoolId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ScratchoffPoolId);
		}
		spends_.WriteTo(ref output, _map_spends_codec);
		if (TargetItemID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TargetItemID);
		}
		if (SkinID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(SkinID);
		}
		if (AccountBackgroundID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(AccountBackgroundID);
		}
		if (DecriptionID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(DecriptionID);
		}
		if (RuleTitleID != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(RuleTitleID);
		}
		if (RuleDecriptionID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(RuleDecriptionID);
		}
		if (RuleSheetField1 != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(RuleSheetField1);
		}
		if (RuleSheetField2 != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(RuleSheetField2);
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
		if (ScratchoffPoolId != 0)
		{
			num += 5;
		}
		num += spends_.CalculateSize(_map_spends_codec);
		if (TargetItemID != 0)
		{
			num += 5;
		}
		if (SkinID != 0)
		{
			num += 5;
		}
		if (AccountBackgroundID != 0)
		{
			num += 5;
		}
		if (DecriptionID != 0)
		{
			num += 5;
		}
		if (RuleTitleID != 0)
		{
			num += 5;
		}
		if (RuleDecriptionID != 0)
		{
			num += 5;
		}
		if (RuleSheetField1 != 0)
		{
			num += 5;
		}
		if (RuleSheetField2 != 0)
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
	public void MergeFrom(ActivityLightingConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ScratchoffPoolId != 0)
			{
				ScratchoffPoolId = other.ScratchoffPoolId;
			}
			spends_.MergeFrom(other.spends_);
			if (other.TargetItemID != 0)
			{
				TargetItemID = other.TargetItemID;
			}
			if (other.SkinID != 0)
			{
				SkinID = other.SkinID;
			}
			if (other.AccountBackgroundID != 0)
			{
				AccountBackgroundID = other.AccountBackgroundID;
			}
			if (other.DecriptionID != 0)
			{
				DecriptionID = other.DecriptionID;
			}
			if (other.RuleTitleID != 0)
			{
				RuleTitleID = other.RuleTitleID;
			}
			if (other.RuleDecriptionID != 0)
			{
				RuleDecriptionID = other.RuleDecriptionID;
			}
			if (other.RuleSheetField1 != 0)
			{
				RuleSheetField1 = other.RuleSheetField1;
			}
			if (other.RuleSheetField2 != 0)
			{
				RuleSheetField2 = other.RuleSheetField2;
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
				ScratchoffPoolId = input.ReadSFixed32();
				break;
			case 26u:
				spends_.AddEntriesFrom(ref input, _map_spends_codec);
				break;
			case 37u:
				TargetItemID = input.ReadSFixed32();
				break;
			case 45u:
				SkinID = input.ReadSFixed32();
				break;
			case 53u:
				AccountBackgroundID = input.ReadSFixed32();
				break;
			case 61u:
				DecriptionID = input.ReadSFixed32();
				break;
			case 69u:
				RuleTitleID = input.ReadSFixed32();
				break;
			case 77u:
				RuleDecriptionID = input.ReadSFixed32();
				break;
			case 85u:
				RuleSheetField1 = input.ReadSFixed32();
				break;
			case 93u:
				RuleSheetField2 = input.ReadSFixed32();
				break;
			}
		}
	}
}
