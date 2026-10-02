using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class GuideDialogConfigure : IMessage<GuideDialogConfigure>, IMessage, IEquatable<GuideDialogConfigure>, IDeepCloneable<GuideDialogConfigure>, IBufferMessage
{
	private static readonly MessageParser<GuideDialogConfigure> _parser = new MessageParser<GuideDialogConfigure>(() => new GuideDialogConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PcDialogIdFieldNumber = 2;

	private int pcDialogId_;

	public const int MobDialogIdFieldNumber = 3;

	private int mobDialogId_;

	public const int DirFieldNumber = 4;

	private string dir_ = "";

	public const int RoleSpriteFieldNumber = 5;

	private string roleSprite_ = "";

	public const int ShowButtonFieldNumber = 6;

	private bool showButton_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuideDialogConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuideReflection.Descriptor.MessageTypes[2];

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
	public int PcDialogId
	{
		get
		{
			return pcDialogId_;
		}
		private set
		{
			pcDialogId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MobDialogId
	{
		get
		{
			return mobDialogId_;
		}
		private set
		{
			mobDialogId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Dir
	{
		get
		{
			return dir_;
		}
		private set
		{
			dir_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string RoleSprite
	{
		get
		{
			return roleSprite_;
		}
		private set
		{
			roleSprite_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool ShowButton
	{
		get
		{
			return showButton_;
		}
		private set
		{
			showButton_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideDialogConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideDialogConfigure(GuideDialogConfigure other)
		: this()
	{
		id_ = other.id_;
		pcDialogId_ = other.pcDialogId_;
		mobDialogId_ = other.mobDialogId_;
		dir_ = other.dir_;
		roleSprite_ = other.roleSprite_;
		showButton_ = other.showButton_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideDialogConfigure Clone()
	{
		return new GuideDialogConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuideDialogConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuideDialogConfigure other)
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
		if (PcDialogId != other.PcDialogId)
		{
			return false;
		}
		if (MobDialogId != other.MobDialogId)
		{
			return false;
		}
		if (Dir != other.Dir)
		{
			return false;
		}
		if (RoleSprite != other.RoleSprite)
		{
			return false;
		}
		if (ShowButton != other.ShowButton)
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
		if (PcDialogId != 0)
		{
			num ^= PcDialogId.GetHashCode();
		}
		if (MobDialogId != 0)
		{
			num ^= MobDialogId.GetHashCode();
		}
		if (Dir.Length != 0)
		{
			num ^= Dir.GetHashCode();
		}
		if (RoleSprite.Length != 0)
		{
			num ^= RoleSprite.GetHashCode();
		}
		if (ShowButton)
		{
			num ^= ShowButton.GetHashCode();
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
		if (PcDialogId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(PcDialogId);
		}
		if (MobDialogId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MobDialogId);
		}
		if (Dir.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Dir);
		}
		if (RoleSprite.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(RoleSprite);
		}
		if (ShowButton)
		{
			output.WriteRawTag(48);
			output.WriteBool(ShowButton);
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
		if (PcDialogId != 0)
		{
			num += 5;
		}
		if (MobDialogId != 0)
		{
			num += 5;
		}
		if (Dir.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Dir);
		}
		if (RoleSprite.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RoleSprite);
		}
		if (ShowButton)
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
	public void MergeFrom(GuideDialogConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PcDialogId != 0)
			{
				PcDialogId = other.PcDialogId;
			}
			if (other.MobDialogId != 0)
			{
				MobDialogId = other.MobDialogId;
			}
			if (other.Dir.Length != 0)
			{
				Dir = other.Dir;
			}
			if (other.RoleSprite.Length != 0)
			{
				RoleSprite = other.RoleSprite;
			}
			if (other.ShowButton)
			{
				ShowButton = other.ShowButton;
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
				PcDialogId = input.ReadSFixed32();
				break;
			case 29u:
				MobDialogId = input.ReadSFixed32();
				break;
			case 34u:
				Dir = input.ReadString();
				break;
			case 42u:
				RoleSprite = input.ReadString();
				break;
			case 48u:
				ShowButton = input.ReadBool();
				break;
			}
		}
	}
}
