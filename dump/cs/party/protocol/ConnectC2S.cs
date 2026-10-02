using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ConnectC2S : IMessage<ConnectC2S>, IMessage, IEquatable<ConnectC2S>, IDeepCloneable<ConnectC2S>, IBufferMessage
{
	public enum AuthInfoOneofCase
	{
		None = 0,
		Dev = 4,
		Steam = 5,
		Taptap = 6,
		Abroad = 7,
		China = 8
	}

	private static readonly MessageParser<ConnectC2S> _parser = new MessageParser<ConnectC2S>(() => new ConnectC2S());

	private UnknownFieldSet _unknownFields;

	public const int PublicKeyFieldNumber = 1;

	private string publicKey_ = "";

	public const int AuthFieldNumber = 2;

	private AuthType auth_;

	public const int ClientVerFieldNumber = 3;

	private string clientVer_ = "";

	public const int DevFieldNumber = 4;

	public const int SteamFieldNumber = 5;

	public const int TaptapFieldNumber = 6;

	public const int AbroadFieldNumber = 7;

	public const int ChinaFieldNumber = 8;

	private object authInfo_;

	private AuthInfoOneofCase authInfoCase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ConnectC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PublicKey
	{
		get
		{
			return publicKey_;
		}
		set
		{
			publicKey_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AuthType Auth
	{
		get
		{
			return auth_;
		}
		set
		{
			auth_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ClientVer
	{
		get
		{
			return clientVer_;
		}
		set
		{
			clientVer_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DevInfo Dev
	{
		get
		{
			if (authInfoCase_ != AuthInfoOneofCase.Dev)
			{
				return null;
			}
			return (DevInfo)authInfo_;
		}
		set
		{
			authInfo_ = value;
			authInfoCase_ = ((value != null) ? AuthInfoOneofCase.Dev : AuthInfoOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamInfo Steam
	{
		get
		{
			if (authInfoCase_ != AuthInfoOneofCase.Steam)
			{
				return null;
			}
			return (SteamInfo)authInfo_;
		}
		set
		{
			authInfo_ = value;
			authInfoCase_ = ((value != null) ? AuthInfoOneofCase.Steam : AuthInfoOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaptapInfo Taptap
	{
		get
		{
			if (authInfoCase_ != AuthInfoOneofCase.Taptap)
			{
				return null;
			}
			return (TaptapInfo)authInfo_;
		}
		set
		{
			authInfo_ = value;
			authInfoCase_ = ((value != null) ? AuthInfoOneofCase.Taptap : AuthInfoOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AbroadInfo Abroad
	{
		get
		{
			if (authInfoCase_ != AuthInfoOneofCase.Abroad)
			{
				return null;
			}
			return (AbroadInfo)authInfo_;
		}
		set
		{
			authInfo_ = value;
			authInfoCase_ = ((value != null) ? AuthInfoOneofCase.Abroad : AuthInfoOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaInfo China
	{
		get
		{
			if (authInfoCase_ != AuthInfoOneofCase.China)
			{
				return null;
			}
			return (ChinaInfo)authInfo_;
		}
		set
		{
			authInfo_ = value;
			authInfoCase_ = ((value != null) ? AuthInfoOneofCase.China : AuthInfoOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AuthInfoOneofCase AuthInfoCase => authInfoCase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectC2S(ConnectC2S other)
		: this()
	{
		publicKey_ = other.publicKey_;
		auth_ = other.auth_;
		clientVer_ = other.clientVer_;
		switch (other.AuthInfoCase)
		{
		case AuthInfoOneofCase.Dev:
			Dev = other.Dev.Clone();
			break;
		case AuthInfoOneofCase.Steam:
			Steam = other.Steam.Clone();
			break;
		case AuthInfoOneofCase.Taptap:
			Taptap = other.Taptap.Clone();
			break;
		case AuthInfoOneofCase.Abroad:
			Abroad = other.Abroad.Clone();
			break;
		case AuthInfoOneofCase.China:
			China = other.China.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectC2S Clone()
	{
		return new ConnectC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void ClearAuthInfo()
	{
		authInfoCase_ = AuthInfoOneofCase.None;
		authInfo_ = null;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ConnectC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ConnectC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PublicKey != other.PublicKey)
		{
			return false;
		}
		if (Auth != other.Auth)
		{
			return false;
		}
		if (ClientVer != other.ClientVer)
		{
			return false;
		}
		if (!object.Equals(Dev, other.Dev))
		{
			return false;
		}
		if (!object.Equals(Steam, other.Steam))
		{
			return false;
		}
		if (!object.Equals(Taptap, other.Taptap))
		{
			return false;
		}
		if (!object.Equals(Abroad, other.Abroad))
		{
			return false;
		}
		if (!object.Equals(China, other.China))
		{
			return false;
		}
		if (AuthInfoCase != other.AuthInfoCase)
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
		if (PublicKey.Length != 0)
		{
			num ^= PublicKey.GetHashCode();
		}
		if (Auth != AuthType.Dev)
		{
			num ^= Auth.GetHashCode();
		}
		if (ClientVer.Length != 0)
		{
			num ^= ClientVer.GetHashCode();
		}
		if (authInfoCase_ == AuthInfoOneofCase.Dev)
		{
			num ^= Dev.GetHashCode();
		}
		if (authInfoCase_ == AuthInfoOneofCase.Steam)
		{
			num ^= Steam.GetHashCode();
		}
		if (authInfoCase_ == AuthInfoOneofCase.Taptap)
		{
			num ^= Taptap.GetHashCode();
		}
		if (authInfoCase_ == AuthInfoOneofCase.Abroad)
		{
			num ^= Abroad.GetHashCode();
		}
		if (authInfoCase_ == AuthInfoOneofCase.China)
		{
			num ^= China.GetHashCode();
		}
		num ^= (int)authInfoCase_;
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
		if (PublicKey.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(PublicKey);
		}
		if (Auth != AuthType.Dev)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)Auth);
		}
		if (ClientVer.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(ClientVer);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Dev)
		{
			output.WriteRawTag(34);
			output.WriteMessage(Dev);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Steam)
		{
			output.WriteRawTag(42);
			output.WriteMessage(Steam);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Taptap)
		{
			output.WriteRawTag(50);
			output.WriteMessage(Taptap);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Abroad)
		{
			output.WriteRawTag(58);
			output.WriteMessage(Abroad);
		}
		if (authInfoCase_ == AuthInfoOneofCase.China)
		{
			output.WriteRawTag(66);
			output.WriteMessage(China);
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
		if (PublicKey.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PublicKey);
		}
		if (Auth != AuthType.Dev)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Auth);
		}
		if (ClientVer.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ClientVer);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Dev)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Dev);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Steam)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Steam);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Taptap)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Taptap);
		}
		if (authInfoCase_ == AuthInfoOneofCase.Abroad)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Abroad);
		}
		if (authInfoCase_ == AuthInfoOneofCase.China)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(China);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ConnectC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PublicKey.Length != 0)
		{
			PublicKey = other.PublicKey;
		}
		if (other.Auth != AuthType.Dev)
		{
			Auth = other.Auth;
		}
		if (other.ClientVer.Length != 0)
		{
			ClientVer = other.ClientVer;
		}
		switch (other.AuthInfoCase)
		{
		case AuthInfoOneofCase.Dev:
			if (Dev == null)
			{
				Dev = new DevInfo();
			}
			Dev.MergeFrom(other.Dev);
			break;
		case AuthInfoOneofCase.Steam:
			if (Steam == null)
			{
				Steam = new SteamInfo();
			}
			Steam.MergeFrom(other.Steam);
			break;
		case AuthInfoOneofCase.Taptap:
			if (Taptap == null)
			{
				Taptap = new TaptapInfo();
			}
			Taptap.MergeFrom(other.Taptap);
			break;
		case AuthInfoOneofCase.Abroad:
			if (Abroad == null)
			{
				Abroad = new AbroadInfo();
			}
			Abroad.MergeFrom(other.Abroad);
			break;
		case AuthInfoOneofCase.China:
			if (China == null)
			{
				China = new ChinaInfo();
			}
			China.MergeFrom(other.China);
			break;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 10u:
				PublicKey = input.ReadString();
				break;
			case 16u:
				Auth = (AuthType)input.ReadEnum();
				break;
			case 26u:
				ClientVer = input.ReadString();
				break;
			case 34u:
			{
				DevInfo devInfo = new DevInfo();
				if (authInfoCase_ == AuthInfoOneofCase.Dev)
				{
					devInfo.MergeFrom(Dev);
				}
				input.ReadMessage(devInfo);
				Dev = devInfo;
				break;
			}
			case 42u:
			{
				SteamInfo steamInfo = new SteamInfo();
				if (authInfoCase_ == AuthInfoOneofCase.Steam)
				{
					steamInfo.MergeFrom(Steam);
				}
				input.ReadMessage(steamInfo);
				Steam = steamInfo;
				break;
			}
			case 50u:
			{
				TaptapInfo taptapInfo = new TaptapInfo();
				if (authInfoCase_ == AuthInfoOneofCase.Taptap)
				{
					taptapInfo.MergeFrom(Taptap);
				}
				input.ReadMessage(taptapInfo);
				Taptap = taptapInfo;
				break;
			}
			case 58u:
			{
				AbroadInfo abroadInfo = new AbroadInfo();
				if (authInfoCase_ == AuthInfoOneofCase.Abroad)
				{
					abroadInfo.MergeFrom(Abroad);
				}
				input.ReadMessage(abroadInfo);
				Abroad = abroadInfo;
				break;
			}
			case 66u:
			{
				ChinaInfo chinaInfo = new ChinaInfo();
				if (authInfoCase_ == AuthInfoOneofCase.China)
				{
					chinaInfo.MergeFrom(China);
				}
				input.ReadMessage(chinaInfo);
				China = chinaInfo;
				break;
			}
			}
		}
	}
}
