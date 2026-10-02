using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FashionAccountBackgroundConfigure : IMessage<FashionAccountBackgroundConfigure>, IMessage, IEquatable<FashionAccountBackgroundConfigure>, IDeepCloneable<FashionAccountBackgroundConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionAccountBackgroundConfigure> _parser = new MessageParser<FashionAccountBackgroundConfigure>(() => new FashionAccountBackgroundConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int AccountBackgroundFieldNumber = 2;

	private string accountBackground_ = "";

	public const int AccountBackgroundSfwFieldNumber = 3;

	private string accountBackgroundSfw_ = "";

	public const int AccountBackgroundVideoFieldNumber = 4;

	private string accountBackgroundVideo_ = "";

	public const int AccountBackgroundVideoSfwFieldNumber = 5;

	private string accountBackgroundVideoSfw_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionAccountBackgroundConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[1];

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
	public string AccountBackground
	{
		get
		{
			return accountBackground_;
		}
		private set
		{
			accountBackground_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackgroundSfw
	{
		get
		{
			return accountBackgroundSfw_;
		}
		private set
		{
			accountBackgroundSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackgroundVideo
	{
		get
		{
			return accountBackgroundVideo_;
		}
		private set
		{
			accountBackgroundVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string AccountBackgroundVideoSfw
	{
		get
		{
			return accountBackgroundVideoSfw_;
		}
		private set
		{
			accountBackgroundVideoSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionAccountBackgroundConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionAccountBackgroundConfigure(FashionAccountBackgroundConfigure other)
		: this()
	{
		id_ = other.id_;
		accountBackground_ = other.accountBackground_;
		accountBackgroundSfw_ = other.accountBackgroundSfw_;
		accountBackgroundVideo_ = other.accountBackgroundVideo_;
		accountBackgroundVideoSfw_ = other.accountBackgroundVideoSfw_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionAccountBackgroundConfigure Clone()
	{
		return new FashionAccountBackgroundConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionAccountBackgroundConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionAccountBackgroundConfigure other)
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
		if (AccountBackground != other.AccountBackground)
		{
			return false;
		}
		if (AccountBackgroundSfw != other.AccountBackgroundSfw)
		{
			return false;
		}
		if (AccountBackgroundVideo != other.AccountBackgroundVideo)
		{
			return false;
		}
		if (AccountBackgroundVideoSfw != other.AccountBackgroundVideoSfw)
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
		if (AccountBackground.Length != 0)
		{
			num ^= AccountBackground.GetHashCode();
		}
		if (AccountBackgroundSfw.Length != 0)
		{
			num ^= AccountBackgroundSfw.GetHashCode();
		}
		if (AccountBackgroundVideo.Length != 0)
		{
			num ^= AccountBackgroundVideo.GetHashCode();
		}
		if (AccountBackgroundVideoSfw.Length != 0)
		{
			num ^= AccountBackgroundVideoSfw.GetHashCode();
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
		if (AccountBackground.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(AccountBackground);
		}
		if (AccountBackgroundSfw.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(AccountBackgroundSfw);
		}
		if (AccountBackgroundVideo.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(AccountBackgroundVideo);
		}
		if (AccountBackgroundVideoSfw.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(AccountBackgroundVideoSfw);
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
		if (AccountBackground.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackground);
		}
		if (AccountBackgroundSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackgroundSfw);
		}
		if (AccountBackgroundVideo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackgroundVideo);
		}
		if (AccountBackgroundVideoSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccountBackgroundVideoSfw);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionAccountBackgroundConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.AccountBackground.Length != 0)
			{
				AccountBackground = other.AccountBackground;
			}
			if (other.AccountBackgroundSfw.Length != 0)
			{
				AccountBackgroundSfw = other.AccountBackgroundSfw;
			}
			if (other.AccountBackgroundVideo.Length != 0)
			{
				AccountBackgroundVideo = other.AccountBackgroundVideo;
			}
			if (other.AccountBackgroundVideoSfw.Length != 0)
			{
				AccountBackgroundVideoSfw = other.AccountBackgroundVideoSfw;
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
				AccountBackground = input.ReadString();
				break;
			case 26u:
				AccountBackgroundSfw = input.ReadString();
				break;
			case 34u:
				AccountBackgroundVideo = input.ReadString();
				break;
			case 42u:
				AccountBackgroundVideoSfw = input.ReadString();
				break;
			}
		}
	}

	public (string, bool) GetPlayerLabel()
	{
		string text = (GameSettings.angelMode ? AccountBackgroundVideoSfw : AccountBackgroundVideo);
		string text2 = (GameSettings.angelMode ? AccountBackgroundSfw : AccountBackground);
		if (GameSettings.angelMode)
		{
			if (string.IsNullOrEmpty(text))
			{
				text = AccountBackgroundVideo;
			}
			if (string.IsNullOrEmpty(text2))
			{
				text2 = AccountBackground;
			}
		}
		bool flag = !string.IsNullOrEmpty(text);
		return (flag ? text : text2, flag);
	}

	public void FixImage(string image)
	{
		accountBackground_ = image;
	}
}
