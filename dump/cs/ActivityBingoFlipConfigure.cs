using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ActivityBingoFlipConfigure : IMessage<ActivityBingoFlipConfigure>, IMessage, IEquatable<ActivityBingoFlipConfigure>, IDeepCloneable<ActivityBingoFlipConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityBingoFlipConfigure> _parser = new MessageParser<ActivityBingoFlipConfigure>(() => new ActivityBingoFlipConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BgFieldNumber = 2;

	private string bg_ = "";

	public const int SpendItemIDFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_spendItemID_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> spendItemID_ = new MapField<int, int>();

	public const int RoundCountFieldNumber = 4;

	private int roundCount_;

	public const int RowCountFieldNumber = 5;

	private int rowCount_;

	public const int ColCountFieldNumber = 6;

	private int colCount_;

	public const int GridPoolIdsFieldNumber = 7;

	private int gridPoolIds_;

	public const int ProgressPoolIdFieldNumber = 8;

	private int progressPoolId_;

	public const int ActivityTitleIDFieldNumber = 9;

	private int activityTitleID_;

	public const int RuleDescriptionIDFieldNumber = 10;

	private int ruleDescriptionID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityBingoFlipConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[8];

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
	public string Bg
	{
		get
		{
			return bg_;
		}
		private set
		{
			bg_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SpendItemID => spendItemID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoundCount
	{
		get
		{
			return roundCount_;
		}
		private set
		{
			roundCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RowCount
	{
		get
		{
			return rowCount_;
		}
		private set
		{
			rowCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ColCount
	{
		get
		{
			return colCount_;
		}
		private set
		{
			colCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GridPoolIds
	{
		get
		{
			return gridPoolIds_;
		}
		private set
		{
			gridPoolIds_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProgressPoolId
	{
		get
		{
			return progressPoolId_;
		}
		private set
		{
			progressPoolId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActivityTitleID
	{
		get
		{
			return activityTitleID_;
		}
		private set
		{
			activityTitleID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RuleDescriptionID
	{
		get
		{
			return ruleDescriptionID_;
		}
		private set
		{
			ruleDescriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityBingoFlipConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityBingoFlipConfigure(ActivityBingoFlipConfigure other)
		: this()
	{
		id_ = other.id_;
		bg_ = other.bg_;
		spendItemID_ = other.spendItemID_.Clone();
		roundCount_ = other.roundCount_;
		rowCount_ = other.rowCount_;
		colCount_ = other.colCount_;
		gridPoolIds_ = other.gridPoolIds_;
		progressPoolId_ = other.progressPoolId_;
		activityTitleID_ = other.activityTitleID_;
		ruleDescriptionID_ = other.ruleDescriptionID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityBingoFlipConfigure Clone()
	{
		return new ActivityBingoFlipConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityBingoFlipConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityBingoFlipConfigure other)
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
		if (Bg != other.Bg)
		{
			return false;
		}
		if (!SpendItemID.Equals(other.SpendItemID))
		{
			return false;
		}
		if (RoundCount != other.RoundCount)
		{
			return false;
		}
		if (RowCount != other.RowCount)
		{
			return false;
		}
		if (ColCount != other.ColCount)
		{
			return false;
		}
		if (GridPoolIds != other.GridPoolIds)
		{
			return false;
		}
		if (ProgressPoolId != other.ProgressPoolId)
		{
			return false;
		}
		if (ActivityTitleID != other.ActivityTitleID)
		{
			return false;
		}
		if (RuleDescriptionID != other.RuleDescriptionID)
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
		if (Bg.Length != 0)
		{
			num ^= Bg.GetHashCode();
		}
		num ^= SpendItemID.GetHashCode();
		if (RoundCount != 0)
		{
			num ^= RoundCount.GetHashCode();
		}
		if (RowCount != 0)
		{
			num ^= RowCount.GetHashCode();
		}
		if (ColCount != 0)
		{
			num ^= ColCount.GetHashCode();
		}
		if (GridPoolIds != 0)
		{
			num ^= GridPoolIds.GetHashCode();
		}
		if (ProgressPoolId != 0)
		{
			num ^= ProgressPoolId.GetHashCode();
		}
		if (ActivityTitleID != 0)
		{
			num ^= ActivityTitleID.GetHashCode();
		}
		if (RuleDescriptionID != 0)
		{
			num ^= RuleDescriptionID.GetHashCode();
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
		if (Bg.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Bg);
		}
		spendItemID_.WriteTo(ref output, _map_spendItemID_codec);
		if (RoundCount != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(RoundCount);
		}
		if (RowCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(RowCount);
		}
		if (ColCount != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(ColCount);
		}
		if (GridPoolIds != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(GridPoolIds);
		}
		if (ProgressPoolId != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(ProgressPoolId);
		}
		if (ActivityTitleID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(ActivityTitleID);
		}
		if (RuleDescriptionID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(RuleDescriptionID);
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
		if (Bg.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Bg);
		}
		num += spendItemID_.CalculateSize(_map_spendItemID_codec);
		if (RoundCount != 0)
		{
			num += 5;
		}
		if (RowCount != 0)
		{
			num += 5;
		}
		if (ColCount != 0)
		{
			num += 5;
		}
		if (GridPoolIds != 0)
		{
			num += 5;
		}
		if (ProgressPoolId != 0)
		{
			num += 5;
		}
		if (ActivityTitleID != 0)
		{
			num += 5;
		}
		if (RuleDescriptionID != 0)
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
	public void MergeFrom(ActivityBingoFlipConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Bg.Length != 0)
			{
				Bg = other.Bg;
			}
			spendItemID_.MergeFrom(other.spendItemID_);
			if (other.RoundCount != 0)
			{
				RoundCount = other.RoundCount;
			}
			if (other.RowCount != 0)
			{
				RowCount = other.RowCount;
			}
			if (other.ColCount != 0)
			{
				ColCount = other.ColCount;
			}
			if (other.GridPoolIds != 0)
			{
				GridPoolIds = other.GridPoolIds;
			}
			if (other.ProgressPoolId != 0)
			{
				ProgressPoolId = other.ProgressPoolId;
			}
			if (other.ActivityTitleID != 0)
			{
				ActivityTitleID = other.ActivityTitleID;
			}
			if (other.RuleDescriptionID != 0)
			{
				RuleDescriptionID = other.RuleDescriptionID;
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
			case 18u:
				Bg = input.ReadString();
				break;
			case 26u:
				spendItemID_.AddEntriesFrom(ref input, _map_spendItemID_codec);
				break;
			case 37u:
				RoundCount = input.ReadSFixed32();
				break;
			case 45u:
				RowCount = input.ReadSFixed32();
				break;
			case 53u:
				ColCount = input.ReadSFixed32();
				break;
			case 61u:
				GridPoolIds = input.ReadSFixed32();
				break;
			case 69u:
				ProgressPoolId = input.ReadSFixed32();
				break;
			case 77u:
				ActivityTitleID = input.ReadSFixed32();
				break;
			case 85u:
				RuleDescriptionID = input.ReadSFixed32();
				break;
			}
		}
	}
}
