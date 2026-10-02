using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ClientHarmonyC2S : IMessage<ClientHarmonyC2S>, IMessage, IEquatable<ClientHarmonyC2S>, IDeepCloneable<ClientHarmonyC2S>, IBufferMessage
{
	private static readonly MessageParser<ClientHarmonyC2S> _parser = new MessageParser<ClientHarmonyC2S>(() => new ClientHarmonyC2S());

	private UnknownFieldSet _unknownFields;

	public const int IsHarmonyFieldNumber = 1;

	private bool isHarmony_;

	public const int HarmonyTypeFieldNumber = 2;

	private int harmonyType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ClientHarmonyC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[277];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsHarmony
	{
		get
		{
			return isHarmony_;
		}
		set
		{
			isHarmony_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HarmonyType
	{
		get
		{
			return harmonyType_;
		}
		set
		{
			harmonyType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientHarmonyC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientHarmonyC2S(ClientHarmonyC2S other)
		: this()
	{
		isHarmony_ = other.isHarmony_;
		harmonyType_ = other.harmonyType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientHarmonyC2S Clone()
	{
		return new ClientHarmonyC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ClientHarmonyC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ClientHarmonyC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (IsHarmony != other.IsHarmony)
		{
			return false;
		}
		if (HarmonyType != other.HarmonyType)
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
		if (IsHarmony)
		{
			num ^= IsHarmony.GetHashCode();
		}
		if (HarmonyType != 0)
		{
			num ^= HarmonyType.GetHashCode();
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
		if (IsHarmony)
		{
			output.WriteRawTag(8);
			output.WriteBool(IsHarmony);
		}
		if (HarmonyType != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(HarmonyType);
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
		if (IsHarmony)
		{
			num += 2;
		}
		if (HarmonyType != 0)
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
	public void MergeFrom(ClientHarmonyC2S other)
	{
		if (other != null)
		{
			if (other.IsHarmony)
			{
				IsHarmony = other.IsHarmony;
			}
			if (other.HarmonyType != 0)
			{
				HarmonyType = other.HarmonyType;
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
				IsHarmony = input.ReadBool();
				break;
			case 21u:
				HarmonyType = input.ReadSFixed32();
				break;
			}
		}
	}
}
