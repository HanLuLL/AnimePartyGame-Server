using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class SettingsLanguageConfigure : IMessage<SettingsLanguageConfigure>, IMessage, IEquatable<SettingsLanguageConfigure>, IDeepCloneable<SettingsLanguageConfigure>, IBufferMessage
{
	private static readonly MessageParser<SettingsLanguageConfigure> _parser = new MessageParser<SettingsLanguageConfigure>(() => new SettingsLanguageConfigure());

	private UnknownFieldSet _unknownFields;

	public const int LanguageTypeFieldNumber = 1;

	private LanguageType languageType_;

	public const int DataIndexFieldNumber = 2;

	private int dataIndex_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	public const int FileContentKeyFieldNumber = 4;

	private string fileContentKey_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SettingsLanguageConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SettingsReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LanguageType LanguageType
	{
		get
		{
			return languageType_;
		}
		private set
		{
			languageType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DataIndex
	{
		get
		{
			return dataIndex_;
		}
		private set
		{
			dataIndex_ = value;
		}
	}

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
	public string FileContentKey
	{
		get
		{
			return fileContentKey_;
		}
		private set
		{
			fileContentKey_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsLanguageConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsLanguageConfigure(SettingsLanguageConfigure other)
		: this()
	{
		languageType_ = other.languageType_;
		dataIndex_ = other.dataIndex_;
		descriptionID_ = other.descriptionID_;
		fileContentKey_ = other.fileContentKey_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsLanguageConfigure Clone()
	{
		return new SettingsLanguageConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SettingsLanguageConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SettingsLanguageConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (LanguageType != other.LanguageType)
		{
			return false;
		}
		if (DataIndex != other.DataIndex)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (FileContentKey != other.FileContentKey)
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
		if (LanguageType != LanguageType.None)
		{
			num ^= LanguageType.GetHashCode();
		}
		if (DataIndex != 0)
		{
			num ^= DataIndex.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (FileContentKey.Length != 0)
		{
			num ^= FileContentKey.GetHashCode();
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
		if (LanguageType != LanguageType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)LanguageType);
		}
		if (DataIndex != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(DataIndex);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescriptionID);
		}
		if (FileContentKey.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(FileContentKey);
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
		if (LanguageType != LanguageType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)LanguageType);
		}
		if (DataIndex != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (FileContentKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(FileContentKey);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SettingsLanguageConfigure other)
	{
		if (other != null)
		{
			if (other.LanguageType != LanguageType.None)
			{
				LanguageType = other.LanguageType;
			}
			if (other.DataIndex != 0)
			{
				DataIndex = other.DataIndex;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
			}
			if (other.FileContentKey.Length != 0)
			{
				FileContentKey = other.FileContentKey;
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
			case 8u:
				LanguageType = (LanguageType)input.ReadEnum();
				break;
			case 21u:
				DataIndex = input.ReadSFixed32();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 34u:
				FileContentKey = input.ReadString();
				break;
			}
		}
	}
}
