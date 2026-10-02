using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class GmS2C : IMessage<GmS2C>, IMessage, IEquatable<GmS2C>, IDeepCloneable<GmS2C>, IBufferMessage
{
	private static readonly MessageParser<GmS2C> _parser = new MessageParser<GmS2C>(() => new GmS2C());

	private UnknownFieldSet _unknownFields;

	public const int NoDevEnvFieldNumber = 1;

	private bool noDevEnv_;

	public const int NoCurrHeroFieldNumber = 2;

	private bool noCurrHero_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GmS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[214];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NoDevEnv
	{
		get
		{
			return noDevEnv_;
		}
		set
		{
			noDevEnv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NoCurrHero
	{
		get
		{
			return noCurrHero_;
		}
		set
		{
			noCurrHero_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmS2C(GmS2C other)
		: this()
	{
		noDevEnv_ = other.noDevEnv_;
		noCurrHero_ = other.noCurrHero_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmS2C Clone()
	{
		return new GmS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GmS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GmS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (NoDevEnv != other.NoDevEnv)
		{
			return false;
		}
		if (NoCurrHero != other.NoCurrHero)
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
		if (NoDevEnv)
		{
			num ^= NoDevEnv.GetHashCode();
		}
		if (NoCurrHero)
		{
			num ^= NoCurrHero.GetHashCode();
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
		if (NoDevEnv)
		{
			output.WriteRawTag(8);
			output.WriteBool(NoDevEnv);
		}
		if (NoCurrHero)
		{
			output.WriteRawTag(16);
			output.WriteBool(NoCurrHero);
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
		if (NoDevEnv)
		{
			num += 2;
		}
		if (NoCurrHero)
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
	public void MergeFrom(GmS2C other)
	{
		if (other != null)
		{
			if (other.NoDevEnv)
			{
				NoDevEnv = other.NoDevEnv;
			}
			if (other.NoCurrHero)
			{
				NoCurrHero = other.NoCurrHero;
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
				NoDevEnv = input.ReadBool();
				break;
			case 16u:
				NoCurrHero = input.ReadBool();
				break;
			}
		}
	}
}
