using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class EffectInfoConfigure : IMessage<EffectInfoConfigure>, IMessage, IEquatable<EffectInfoConfigure>, IDeepCloneable<EffectInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<EffectInfoConfigure> _parser = new MessageParser<EffectInfoConfigure>(() => new EffectInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int EffectNameFieldNumber = 2;

	private string effectName_ = "";

	public const int IsHeroSupportFieldNumber = 3;

	private bool isHeroSupport_;

	public const int IsCharacterObjFieldNumber = 4;

	private bool isCharacterObj_;

	public const int IsFollowFlipFieldNumber = 5;

	private bool isFollowFlip_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<EffectInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => EffectReflection.Descriptor.MessageTypes[0];

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
	public string EffectName
	{
		get
		{
			return effectName_;
		}
		private set
		{
			effectName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsHeroSupport
	{
		get
		{
			return isHeroSupport_;
		}
		private set
		{
			isHeroSupport_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsCharacterObj
	{
		get
		{
			return isCharacterObj_;
		}
		private set
		{
			isCharacterObj_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsFollowFlip
	{
		get
		{
			return isFollowFlip_;
		}
		private set
		{
			isFollowFlip_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EffectInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EffectInfoConfigure(EffectInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		effectName_ = other.effectName_;
		isHeroSupport_ = other.isHeroSupport_;
		isCharacterObj_ = other.isCharacterObj_;
		isFollowFlip_ = other.isFollowFlip_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EffectInfoConfigure Clone()
	{
		return new EffectInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as EffectInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(EffectInfoConfigure other)
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
		if (EffectName != other.EffectName)
		{
			return false;
		}
		if (IsHeroSupport != other.IsHeroSupport)
		{
			return false;
		}
		if (IsCharacterObj != other.IsCharacterObj)
		{
			return false;
		}
		if (IsFollowFlip != other.IsFollowFlip)
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
		if (EffectName.Length != 0)
		{
			num ^= EffectName.GetHashCode();
		}
		if (IsHeroSupport)
		{
			num ^= IsHeroSupport.GetHashCode();
		}
		if (IsCharacterObj)
		{
			num ^= IsCharacterObj.GetHashCode();
		}
		if (IsFollowFlip)
		{
			num ^= IsFollowFlip.GetHashCode();
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
		if (EffectName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(EffectName);
		}
		if (IsHeroSupport)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsHeroSupport);
		}
		if (IsCharacterObj)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsCharacterObj);
		}
		if (IsFollowFlip)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsFollowFlip);
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
		if (EffectName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(EffectName);
		}
		if (IsHeroSupport)
		{
			num += 2;
		}
		if (IsCharacterObj)
		{
			num += 2;
		}
		if (IsFollowFlip)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(EffectInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.EffectName.Length != 0)
			{
				EffectName = other.EffectName;
			}
			if (other.IsHeroSupport)
			{
				IsHeroSupport = other.IsHeroSupport;
			}
			if (other.IsCharacterObj)
			{
				IsCharacterObj = other.IsCharacterObj;
			}
			if (other.IsFollowFlip)
			{
				IsFollowFlip = other.IsFollowFlip;
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
				EffectName = input.ReadString();
				break;
			case 24u:
				IsHeroSupport = input.ReadBool();
				break;
			case 32u:
				IsCharacterObj = input.ReadBool();
				break;
			case 40u:
				IsFollowFlip = input.ReadBool();
				break;
			}
		}
	}
}
