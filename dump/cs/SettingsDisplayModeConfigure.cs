using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class SettingsDisplayModeConfigure : IMessage<SettingsDisplayModeConfigure>, IMessage, IEquatable<SettingsDisplayModeConfigure>, IDeepCloneable<SettingsDisplayModeConfigure>, IBufferMessage
{
	private static readonly MessageParser<SettingsDisplayModeConfigure> _parser = new MessageParser<SettingsDisplayModeConfigure>(() => new SettingsDisplayModeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ScreenModeFieldNumber = 2;

	private int screenMode_;

	public const int DescriptionIDFieldNumber = 3;

	private int descriptionID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SettingsDisplayModeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SettingsReflection.Descriptor.MessageTypes[2];

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
	public int ScreenMode
	{
		get
		{
			return screenMode_;
		}
		private set
		{
			screenMode_ = value;
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
	public SettingsDisplayModeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsDisplayModeConfigure(SettingsDisplayModeConfigure other)
		: this()
	{
		id_ = other.id_;
		screenMode_ = other.screenMode_;
		descriptionID_ = other.descriptionID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SettingsDisplayModeConfigure Clone()
	{
		return new SettingsDisplayModeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SettingsDisplayModeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SettingsDisplayModeConfigure other)
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
		if (ScreenMode != other.ScreenMode)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
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
		if (ScreenMode != 0)
		{
			num ^= ScreenMode.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
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
		if (ScreenMode != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ScreenMode);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescriptionID);
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
		if (ScreenMode != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
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
	public void MergeFrom(SettingsDisplayModeConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ScreenMode != 0)
			{
				ScreenMode = other.ScreenMode;
			}
			if (other.DescriptionID != 0)
			{
				DescriptionID = other.DescriptionID;
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
				ScreenMode = input.ReadSFixed32();
				break;
			case 29u:
				DescriptionID = input.ReadSFixed32();
				break;
			}
		}
	}
}
