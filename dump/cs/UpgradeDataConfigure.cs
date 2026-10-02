using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class UpgradeDataConfigure : IMessage<UpgradeDataConfigure>, IMessage, IEquatable<UpgradeDataConfigure>, IDeepCloneable<UpgradeDataConfigure>, IBufferMessage
{
	private static readonly MessageParser<UpgradeDataConfigure> _parser = new MessageParser<UpgradeDataConfigure>(() => new UpgradeDataConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int GoldSTRidFieldNumber = 2;

	private int goldSTRid_;

	public const int IsDefaultFieldNumber = 3;

	private bool isDefault_;

	public const int UpgradeDataConfigureItemsFieldNumber = 4;

	private static readonly FieldCodec<UpgradeDataConfigureItem> _repeated_upgradeDataConfigureItems_codec = FieldCodec.ForMessage(34u, UpgradeDataConfigureItem.Parser);

	private readonly RepeatedField<UpgradeDataConfigureItem> upgradeDataConfigureItems_ = new RepeatedField<UpgradeDataConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UpgradeDataConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UpgradeReflection.Descriptor.MessageTypes[0];

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
	public int GoldSTRid
	{
		get
		{
			return goldSTRid_;
		}
		private set
		{
			goldSTRid_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDefault
	{
		get
		{
			return isDefault_;
		}
		private set
		{
			isDefault_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<UpgradeDataConfigureItem> UpgradeDataConfigureItems => upgradeDataConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpgradeDataConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpgradeDataConfigure(UpgradeDataConfigure other)
		: this()
	{
		id_ = other.id_;
		goldSTRid_ = other.goldSTRid_;
		isDefault_ = other.isDefault_;
		upgradeDataConfigureItems_ = other.upgradeDataConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpgradeDataConfigure Clone()
	{
		return new UpgradeDataConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UpgradeDataConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UpgradeDataConfigure other)
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
		if (GoldSTRid != other.GoldSTRid)
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (!upgradeDataConfigureItems_.Equals(other.upgradeDataConfigureItems_))
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
		if (GoldSTRid != 0)
		{
			num ^= GoldSTRid.GetHashCode();
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		num ^= upgradeDataConfigureItems_.GetHashCode();
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
		if (GoldSTRid != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(GoldSTRid);
		}
		if (IsDefault)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsDefault);
		}
		upgradeDataConfigureItems_.WriteTo(ref output, _repeated_upgradeDataConfigureItems_codec);
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
		if (GoldSTRid != 0)
		{
			num += 5;
		}
		if (IsDefault)
		{
			num += 2;
		}
		num += upgradeDataConfigureItems_.CalculateSize(_repeated_upgradeDataConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UpgradeDataConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.GoldSTRid != 0)
			{
				GoldSTRid = other.GoldSTRid;
			}
			if (other.IsDefault)
			{
				IsDefault = other.IsDefault;
			}
			upgradeDataConfigureItems_.Add(other.upgradeDataConfigureItems_);
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
				GoldSTRid = input.ReadSFixed32();
				break;
			case 24u:
				IsDefault = input.ReadBool();
				break;
			case 34u:
				upgradeDataConfigureItems_.AddEntriesFrom(ref input, _repeated_upgradeDataConfigureItems_codec);
				break;
			}
		}
	}
}
