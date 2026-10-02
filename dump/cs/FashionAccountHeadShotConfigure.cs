using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FashionAccountHeadShotConfigure : IMessage<FashionAccountHeadShotConfigure>, IMessage, IEquatable<FashionAccountHeadShotConfigure>, IDeepCloneable<FashionAccountHeadShotConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionAccountHeadShotConfigure> _parser = new MessageParser<FashionAccountHeadShotConfigure>(() => new FashionAccountHeadShotConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ProfilePictureFieldNumber = 2;

	private string profilePicture_ = "";

	public const int ProfilePictureSfwFieldNumber = 3;

	private string profilePictureSfw_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionAccountHeadShotConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[0];

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
	public string ProfilePicture
	{
		get
		{
			return profilePicture_;
		}
		private set
		{
			profilePicture_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ProfilePictureSfw
	{
		get
		{
			return profilePictureSfw_;
		}
		private set
		{
			profilePictureSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionAccountHeadShotConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionAccountHeadShotConfigure(FashionAccountHeadShotConfigure other)
		: this()
	{
		id_ = other.id_;
		profilePicture_ = other.profilePicture_;
		profilePictureSfw_ = other.profilePictureSfw_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionAccountHeadShotConfigure Clone()
	{
		return new FashionAccountHeadShotConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionAccountHeadShotConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionAccountHeadShotConfigure other)
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
		if (ProfilePicture != other.ProfilePicture)
		{
			return false;
		}
		if (ProfilePictureSfw != other.ProfilePictureSfw)
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
		if (ProfilePicture.Length != 0)
		{
			num ^= ProfilePicture.GetHashCode();
		}
		if (ProfilePictureSfw.Length != 0)
		{
			num ^= ProfilePictureSfw.GetHashCode();
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
		if (ProfilePicture.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(ProfilePicture);
		}
		if (ProfilePictureSfw.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(ProfilePictureSfw);
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
		if (ProfilePicture.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfilePicture);
		}
		if (ProfilePictureSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfilePictureSfw);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionAccountHeadShotConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.ProfilePicture.Length != 0)
			{
				ProfilePicture = other.ProfilePicture;
			}
			if (other.ProfilePictureSfw.Length != 0)
			{
				ProfilePictureSfw = other.ProfilePictureSfw;
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
				ProfilePicture = input.ReadString();
				break;
			case 26u:
				ProfilePictureSfw = input.ReadString();
				break;
			}
		}
	}

	public string GetHeadShot()
	{
		string text = (GameSettings.angelMode ? ProfilePictureSfw : ProfilePicture);
		if (GameSettings.angelMode && string.IsNullOrEmpty(text))
		{
			text = ProfilePicture;
		}
		return text;
	}
}
