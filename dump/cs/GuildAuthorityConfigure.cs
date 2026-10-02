using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class GuildAuthorityConfigure : IMessage<GuildAuthorityConfigure>, IMessage, IEquatable<GuildAuthorityConfigure>, IDeepCloneable<GuildAuthorityConfigure>, IBufferMessage
{
	private static readonly MessageParser<GuildAuthorityConfigure> _parser = new MessageParser<GuildAuthorityConfigure>(() => new GuildAuthorityConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GuildTitleTypeFieldNumber = 1;

	private GuildTitleType guildTitleType_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int CanGuildSettingFieldNumber = 3;

	private bool canGuildSetting_;

	public const int CanInternalAnnouncementFieldNumber = 4;

	private bool canInternalAnnouncement_;

	public const int CanApproveApplicationsFieldNumber = 5;

	private bool canApproveApplications_;

	public const int CanInviteToGuildFieldNumber = 6;

	private bool canInviteToGuild_;

	public const int CanTransferFieldNumber = 7;

	private bool canTransfer_;

	public const int CanPromoteOrDemoteFieldNumber = 8;

	private bool canPromoteOrDemote_;

	public const int CanImpeachFieldNumber = 9;

	private bool canImpeach_;

	public const int CanKickFromGuildFieldNumber = 10;

	private bool canKickFromGuild_;

	public const int CanDisbandGuildFieldNumber = 11;

	private bool canDisbandGuild_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuildAuthorityConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuildReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildTitleType GuildTitleType
	{
		get
		{
			return guildTitleType_;
		}
		private set
		{
			guildTitleType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanGuildSetting
	{
		get
		{
			return canGuildSetting_;
		}
		private set
		{
			canGuildSetting_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanInternalAnnouncement
	{
		get
		{
			return canInternalAnnouncement_;
		}
		private set
		{
			canInternalAnnouncement_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanApproveApplications
	{
		get
		{
			return canApproveApplications_;
		}
		private set
		{
			canApproveApplications_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanInviteToGuild
	{
		get
		{
			return canInviteToGuild_;
		}
		private set
		{
			canInviteToGuild_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanTransfer
	{
		get
		{
			return canTransfer_;
		}
		private set
		{
			canTransfer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanPromoteOrDemote
	{
		get
		{
			return canPromoteOrDemote_;
		}
		private set
		{
			canPromoteOrDemote_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanImpeach
	{
		get
		{
			return canImpeach_;
		}
		private set
		{
			canImpeach_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanKickFromGuild
	{
		get
		{
			return canKickFromGuild_;
		}
		private set
		{
			canKickFromGuild_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanDisbandGuild
	{
		get
		{
			return canDisbandGuild_;
		}
		private set
		{
			canDisbandGuild_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildAuthorityConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildAuthorityConfigure(GuildAuthorityConfigure other)
		: this()
	{
		guildTitleType_ = other.guildTitleType_;
		nameID_ = other.nameID_;
		canGuildSetting_ = other.canGuildSetting_;
		canInternalAnnouncement_ = other.canInternalAnnouncement_;
		canApproveApplications_ = other.canApproveApplications_;
		canInviteToGuild_ = other.canInviteToGuild_;
		canTransfer_ = other.canTransfer_;
		canPromoteOrDemote_ = other.canPromoteOrDemote_;
		canImpeach_ = other.canImpeach_;
		canKickFromGuild_ = other.canKickFromGuild_;
		canDisbandGuild_ = other.canDisbandGuild_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildAuthorityConfigure Clone()
	{
		return new GuildAuthorityConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuildAuthorityConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuildAuthorityConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GuildTitleType != other.GuildTitleType)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (CanGuildSetting != other.CanGuildSetting)
		{
			return false;
		}
		if (CanInternalAnnouncement != other.CanInternalAnnouncement)
		{
			return false;
		}
		if (CanApproveApplications != other.CanApproveApplications)
		{
			return false;
		}
		if (CanInviteToGuild != other.CanInviteToGuild)
		{
			return false;
		}
		if (CanTransfer != other.CanTransfer)
		{
			return false;
		}
		if (CanPromoteOrDemote != other.CanPromoteOrDemote)
		{
			return false;
		}
		if (CanImpeach != other.CanImpeach)
		{
			return false;
		}
		if (CanKickFromGuild != other.CanKickFromGuild)
		{
			return false;
		}
		if (CanDisbandGuild != other.CanDisbandGuild)
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
		if (GuildTitleType != GuildTitleType.None)
		{
			num ^= GuildTitleType.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (CanGuildSetting)
		{
			num ^= CanGuildSetting.GetHashCode();
		}
		if (CanInternalAnnouncement)
		{
			num ^= CanInternalAnnouncement.GetHashCode();
		}
		if (CanApproveApplications)
		{
			num ^= CanApproveApplications.GetHashCode();
		}
		if (CanInviteToGuild)
		{
			num ^= CanInviteToGuild.GetHashCode();
		}
		if (CanTransfer)
		{
			num ^= CanTransfer.GetHashCode();
		}
		if (CanPromoteOrDemote)
		{
			num ^= CanPromoteOrDemote.GetHashCode();
		}
		if (CanImpeach)
		{
			num ^= CanImpeach.GetHashCode();
		}
		if (CanKickFromGuild)
		{
			num ^= CanKickFromGuild.GetHashCode();
		}
		if (CanDisbandGuild)
		{
			num ^= CanDisbandGuild.GetHashCode();
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
		if (GuildTitleType != GuildTitleType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)GuildTitleType);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (CanGuildSetting)
		{
			output.WriteRawTag(24);
			output.WriteBool(CanGuildSetting);
		}
		if (CanInternalAnnouncement)
		{
			output.WriteRawTag(32);
			output.WriteBool(CanInternalAnnouncement);
		}
		if (CanApproveApplications)
		{
			output.WriteRawTag(40);
			output.WriteBool(CanApproveApplications);
		}
		if (CanInviteToGuild)
		{
			output.WriteRawTag(48);
			output.WriteBool(CanInviteToGuild);
		}
		if (CanTransfer)
		{
			output.WriteRawTag(56);
			output.WriteBool(CanTransfer);
		}
		if (CanPromoteOrDemote)
		{
			output.WriteRawTag(64);
			output.WriteBool(CanPromoteOrDemote);
		}
		if (CanImpeach)
		{
			output.WriteRawTag(72);
			output.WriteBool(CanImpeach);
		}
		if (CanKickFromGuild)
		{
			output.WriteRawTag(80);
			output.WriteBool(CanKickFromGuild);
		}
		if (CanDisbandGuild)
		{
			output.WriteRawTag(88);
			output.WriteBool(CanDisbandGuild);
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
		if (GuildTitleType != GuildTitleType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GuildTitleType);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (CanGuildSetting)
		{
			num += 2;
		}
		if (CanInternalAnnouncement)
		{
			num += 2;
		}
		if (CanApproveApplications)
		{
			num += 2;
		}
		if (CanInviteToGuild)
		{
			num += 2;
		}
		if (CanTransfer)
		{
			num += 2;
		}
		if (CanPromoteOrDemote)
		{
			num += 2;
		}
		if (CanImpeach)
		{
			num += 2;
		}
		if (CanKickFromGuild)
		{
			num += 2;
		}
		if (CanDisbandGuild)
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
	public void MergeFrom(GuildAuthorityConfigure other)
	{
		if (other != null)
		{
			if (other.GuildTitleType != GuildTitleType.None)
			{
				GuildTitleType = other.GuildTitleType;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.CanGuildSetting)
			{
				CanGuildSetting = other.CanGuildSetting;
			}
			if (other.CanInternalAnnouncement)
			{
				CanInternalAnnouncement = other.CanInternalAnnouncement;
			}
			if (other.CanApproveApplications)
			{
				CanApproveApplications = other.CanApproveApplications;
			}
			if (other.CanInviteToGuild)
			{
				CanInviteToGuild = other.CanInviteToGuild;
			}
			if (other.CanTransfer)
			{
				CanTransfer = other.CanTransfer;
			}
			if (other.CanPromoteOrDemote)
			{
				CanPromoteOrDemote = other.CanPromoteOrDemote;
			}
			if (other.CanImpeach)
			{
				CanImpeach = other.CanImpeach;
			}
			if (other.CanKickFromGuild)
			{
				CanKickFromGuild = other.CanKickFromGuild;
			}
			if (other.CanDisbandGuild)
			{
				CanDisbandGuild = other.CanDisbandGuild;
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
				GuildTitleType = (GuildTitleType)input.ReadEnum();
				break;
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 24u:
				CanGuildSetting = input.ReadBool();
				break;
			case 32u:
				CanInternalAnnouncement = input.ReadBool();
				break;
			case 40u:
				CanApproveApplications = input.ReadBool();
				break;
			case 48u:
				CanInviteToGuild = input.ReadBool();
				break;
			case 56u:
				CanTransfer = input.ReadBool();
				break;
			case 64u:
				CanPromoteOrDemote = input.ReadBool();
				break;
			case 72u:
				CanImpeach = input.ReadBool();
				break;
			case 80u:
				CanKickFromGuild = input.ReadBool();
				break;
			case 88u:
				CanDisbandGuild = input.ReadBool();
				break;
			}
		}
	}
}
