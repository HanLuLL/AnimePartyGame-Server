using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class StorySpeakerConfigure : IMessage<StorySpeakerConfigure>, IMessage, IEquatable<StorySpeakerConfigure>, IDeepCloneable<StorySpeakerConfigure>, IBufferMessage
{
	private static readonly MessageParser<StorySpeakerConfigure> _parser = new MessageParser<StorySpeakerConfigure>(() => new StorySpeakerConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IDFieldNumber = 1;

	private int iD_;

	public const int CharacterIDFieldNumber = 2;

	private int characterID_;

	public const int ProfilePhotoFieldNumber = 3;

	private string profilePhoto_ = "";

	public const int ProfileColorFieldNumber = 4;

	private string profileColor_ = "";

	public const int FrameIndexFieldNumber = 5;

	private int frameIndex_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<StorySpeakerConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => StoryReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ID
	{
		get
		{
			return iD_;
		}
		private set
		{
			iD_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CharacterID
	{
		get
		{
			return characterID_;
		}
		private set
		{
			characterID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ProfilePhoto
	{
		get
		{
			return profilePhoto_;
		}
		private set
		{
			profilePhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ProfileColor
	{
		get
		{
			return profileColor_;
		}
		private set
		{
			profileColor_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FrameIndex
	{
		get
		{
			return frameIndex_;
		}
		private set
		{
			frameIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StorySpeakerConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StorySpeakerConfigure(StorySpeakerConfigure other)
		: this()
	{
		iD_ = other.iD_;
		characterID_ = other.characterID_;
		profilePhoto_ = other.profilePhoto_;
		profileColor_ = other.profileColor_;
		frameIndex_ = other.frameIndex_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StorySpeakerConfigure Clone()
	{
		return new StorySpeakerConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as StorySpeakerConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(StorySpeakerConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ID != other.ID)
		{
			return false;
		}
		if (CharacterID != other.CharacterID)
		{
			return false;
		}
		if (ProfilePhoto != other.ProfilePhoto)
		{
			return false;
		}
		if (ProfileColor != other.ProfileColor)
		{
			return false;
		}
		if (FrameIndex != other.FrameIndex)
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
		if (ID != 0)
		{
			num ^= ID.GetHashCode();
		}
		if (CharacterID != 0)
		{
			num ^= CharacterID.GetHashCode();
		}
		if (ProfilePhoto.Length != 0)
		{
			num ^= ProfilePhoto.GetHashCode();
		}
		if (ProfileColor.Length != 0)
		{
			num ^= ProfileColor.GetHashCode();
		}
		if (FrameIndex != 0)
		{
			num ^= FrameIndex.GetHashCode();
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
		if (ID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ID);
		}
		if (CharacterID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CharacterID);
		}
		if (ProfilePhoto.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(ProfilePhoto);
		}
		if (ProfileColor.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(ProfileColor);
		}
		if (FrameIndex != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(FrameIndex);
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
		if (ID != 0)
		{
			num += 5;
		}
		if (CharacterID != 0)
		{
			num += 5;
		}
		if (ProfilePhoto.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfilePhoto);
		}
		if (ProfileColor.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfileColor);
		}
		if (FrameIndex != 0)
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
	public void MergeFrom(StorySpeakerConfigure other)
	{
		if (other != null)
		{
			if (other.ID != 0)
			{
				ID = other.ID;
			}
			if (other.CharacterID != 0)
			{
				CharacterID = other.CharacterID;
			}
			if (other.ProfilePhoto.Length != 0)
			{
				ProfilePhoto = other.ProfilePhoto;
			}
			if (other.ProfileColor.Length != 0)
			{
				ProfileColor = other.ProfileColor;
			}
			if (other.FrameIndex != 0)
			{
				FrameIndex = other.FrameIndex;
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
				ID = input.ReadSFixed32();
				break;
			case 21u:
				CharacterID = input.ReadSFixed32();
				break;
			case 26u:
				ProfilePhoto = input.ReadString();
				break;
			case 34u:
				ProfileColor = input.ReadString();
				break;
			case 45u:
				FrameIndex = input.ReadSFixed32();
				break;
			}
		}
	}
}
