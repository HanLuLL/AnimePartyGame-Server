using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class STRSkillLocalConfigure : IMessage<STRSkillLocalConfigure>, IMessage, IEquatable<STRSkillLocalConfigure>, IDeepCloneable<STRSkillLocalConfigure>, IBufferMessage
{
	private static readonly MessageParser<STRSkillLocalConfigure> _parser = new MessageParser<STRSkillLocalConfigure>(() => new STRSkillLocalConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SimplifiedFieldNumber = 2;

	private string simplified_ = "";

	public const int EnglishFieldNumber = 3;

	private string english_ = "";

	public const int JapaneseFieldNumber = 4;

	private string japanese_ = "";

	public const int TraditionalFieldNumber = 5;

	private string traditional_ = "";

	public const int KoreanFieldNumber = 6;

	private string korean_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<STRSkillLocalConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => STRSkillReflection.Descriptor.MessageTypes[0];

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
	public string Simplified
	{
		get
		{
			return simplified_;
		}
		private set
		{
			simplified_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string English
	{
		get
		{
			return english_;
		}
		private set
		{
			english_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Japanese
	{
		get
		{
			return japanese_;
		}
		private set
		{
			japanese_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Traditional
	{
		get
		{
			return traditional_;
		}
		private set
		{
			traditional_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Korean
	{
		get
		{
			return korean_;
		}
		private set
		{
			korean_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public STRSkillLocalConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public STRSkillLocalConfigure(STRSkillLocalConfigure other)
		: this()
	{
		id_ = other.id_;
		simplified_ = other.simplified_;
		english_ = other.english_;
		japanese_ = other.japanese_;
		traditional_ = other.traditional_;
		korean_ = other.korean_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public STRSkillLocalConfigure Clone()
	{
		return new STRSkillLocalConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as STRSkillLocalConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(STRSkillLocalConfigure other)
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
		if (Simplified != other.Simplified)
		{
			return false;
		}
		if (English != other.English)
		{
			return false;
		}
		if (Japanese != other.Japanese)
		{
			return false;
		}
		if (Traditional != other.Traditional)
		{
			return false;
		}
		if (Korean != other.Korean)
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
		if (Simplified.Length != 0)
		{
			num ^= Simplified.GetHashCode();
		}
		if (English.Length != 0)
		{
			num ^= English.GetHashCode();
		}
		if (Japanese.Length != 0)
		{
			num ^= Japanese.GetHashCode();
		}
		if (Traditional.Length != 0)
		{
			num ^= Traditional.GetHashCode();
		}
		if (Korean.Length != 0)
		{
			num ^= Korean.GetHashCode();
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
		if (Simplified.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Simplified);
		}
		if (English.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(English);
		}
		if (Japanese.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Japanese);
		}
		if (Traditional.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Traditional);
		}
		if (Korean.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Korean);
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
		if (Simplified.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Simplified);
		}
		if (English.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(English);
		}
		if (Japanese.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Japanese);
		}
		if (Traditional.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Traditional);
		}
		if (Korean.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Korean);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(STRSkillLocalConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Simplified.Length != 0)
			{
				Simplified = other.Simplified;
			}
			if (other.English.Length != 0)
			{
				English = other.English;
			}
			if (other.Japanese.Length != 0)
			{
				Japanese = other.Japanese;
			}
			if (other.Traditional.Length != 0)
			{
				Traditional = other.Traditional;
			}
			if (other.Korean.Length != 0)
			{
				Korean = other.Korean;
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
				Simplified = input.ReadString();
				break;
			case 26u:
				English = input.ReadString();
				break;
			case 34u:
				Japanese = input.ReadString();
				break;
			case 42u:
				Traditional = input.ReadString();
				break;
			case 50u:
				Korean = input.ReadString();
				break;
			}
		}
	}

	public string GetLocal()
	{
		return GameSettings.languageType switch
		{
			LanguageType.SimplifiedChinese => Simplified, 
			LanguageType.Japanese => Japanese, 
			LanguageType.TraditionalChinese => Traditional, 
			LanguageType.Korean => Korean, 
			LanguageType.English => English, 
			_ => "", 
		};
	}
}
