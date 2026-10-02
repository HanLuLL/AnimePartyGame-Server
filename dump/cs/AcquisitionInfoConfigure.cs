using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class AcquisitionInfoConfigure : IMessage<AcquisitionInfoConfigure>, IMessage, IEquatable<AcquisitionInfoConfigure>, IDeepCloneable<AcquisitionInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<AcquisitionInfoConfigure> _parser = new MessageParser<AcquisitionInfoConfigure>(() => new AcquisitionInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int InviteTaskIDsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_inviteTaskIDs_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> inviteTaskIDs_ = new RepeatedField<int>();

	public const int CaptchaValidLvFieldNumber = 3;

	private int captchaValidLv_;

	public const int RewardFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int InvitedTaskIDsFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_invitedTaskIDs_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> invitedTaskIDs_ = new RepeatedField<int>();

	public const int CharacterImageFieldNumber = 6;

	private string characterImage_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AcquisitionInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AcquisitionReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<int> InviteTaskIDs => inviteTaskIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CaptchaValidLv
	{
		get
		{
			return captchaValidLv_;
		}
		private set
		{
			captchaValidLv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> InvitedTaskIDs => invitedTaskIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string CharacterImage
	{
		get
		{
			return characterImage_;
		}
		private set
		{
			characterImage_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionInfoConfigure(AcquisitionInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		inviteTaskIDs_ = other.inviteTaskIDs_.Clone();
		captchaValidLv_ = other.captchaValidLv_;
		reward_ = other.reward_.Clone();
		invitedTaskIDs_ = other.invitedTaskIDs_.Clone();
		characterImage_ = other.characterImage_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionInfoConfigure Clone()
	{
		return new AcquisitionInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AcquisitionInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AcquisitionInfoConfigure other)
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
		if (!inviteTaskIDs_.Equals(other.inviteTaskIDs_))
		{
			return false;
		}
		if (CaptchaValidLv != other.CaptchaValidLv)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
		{
			return false;
		}
		if (!invitedTaskIDs_.Equals(other.invitedTaskIDs_))
		{
			return false;
		}
		if (CharacterImage != other.CharacterImage)
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
		num ^= inviteTaskIDs_.GetHashCode();
		if (CaptchaValidLv != 0)
		{
			num ^= CaptchaValidLv.GetHashCode();
		}
		num ^= Reward.GetHashCode();
		num ^= invitedTaskIDs_.GetHashCode();
		if (CharacterImage.Length != 0)
		{
			num ^= CharacterImage.GetHashCode();
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
		inviteTaskIDs_.WriteTo(ref output, _repeated_inviteTaskIDs_codec);
		if (CaptchaValidLv != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CaptchaValidLv);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		invitedTaskIDs_.WriteTo(ref output, _repeated_invitedTaskIDs_codec);
		if (CharacterImage.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(CharacterImage);
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
		num += inviteTaskIDs_.CalculateSize(_repeated_inviteTaskIDs_codec);
		if (CaptchaValidLv != 0)
		{
			num += 5;
		}
		num += reward_.CalculateSize(_map_reward_codec);
		num += invitedTaskIDs_.CalculateSize(_repeated_invitedTaskIDs_codec);
		if (CharacterImage.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CharacterImage);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AcquisitionInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			inviteTaskIDs_.Add(other.inviteTaskIDs_);
			if (other.CaptchaValidLv != 0)
			{
				CaptchaValidLv = other.CaptchaValidLv;
			}
			reward_.MergeFrom(other.reward_);
			invitedTaskIDs_.Add(other.invitedTaskIDs_);
			if (other.CharacterImage.Length != 0)
			{
				CharacterImage = other.CharacterImage;
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
			case 21u:
				inviteTaskIDs_.AddEntriesFrom(ref input, _repeated_inviteTaskIDs_codec);
				break;
			case 29u:
				CaptchaValidLv = input.ReadSFixed32();
				break;
			case 34u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 42u:
			case 45u:
				invitedTaskIDs_.AddEntriesFrom(ref input, _repeated_invitedTaskIDs_codec);
				break;
			case 50u:
				CharacterImage = input.ReadString();
				break;
			}
		}
	}
}
