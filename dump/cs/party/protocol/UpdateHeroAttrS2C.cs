using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class UpdateHeroAttrS2C : IMessage<UpdateHeroAttrS2C>, IMessage, IEquatable<UpdateHeroAttrS2C>, IDeepCloneable<UpdateHeroAttrS2C>, IBufferMessage
{
	private static readonly MessageParser<UpdateHeroAttrS2C> _parser = new MessageParser<UpdateHeroAttrS2C>(() => new UpdateHeroAttrS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int CauseFieldNumber = 2;

	private CauseOrigin cause_;

	public const int EffectDatasFieldNumber = 4;

	private static readonly FieldCodec<HeroAttrEffect> _repeated_effectDatas_codec = FieldCodec.ForMessage(34u, HeroAttrEffect.Parser);

	private readonly RepeatedField<HeroAttrEffect> effectDatas_ = new RepeatedField<HeroAttrEffect>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UpdateHeroAttrS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[445];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CauseOrigin Cause
	{
		get
		{
			return cause_;
		}
		set
		{
			cause_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<HeroAttrEffect> EffectDatas => effectDatas_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateHeroAttrS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateHeroAttrS2C(UpdateHeroAttrS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		cause_ = ((other.cause_ != null) ? other.cause_.Clone() : null);
		effectDatas_ = other.effectDatas_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateHeroAttrS2C Clone()
	{
		return new UpdateHeroAttrS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UpdateHeroAttrS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UpdateHeroAttrS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (!object.Equals(Cause, other.Cause))
		{
			return false;
		}
		if (!effectDatas_.Equals(other.effectDatas_))
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (cause_ != null)
		{
			num ^= Cause.GetHashCode();
		}
		num ^= effectDatas_.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (cause_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Cause);
		}
		effectDatas_.WriteTo(ref output, _repeated_effectDatas_codec);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (cause_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Cause);
		}
		num += effectDatas_.CalculateSize(_repeated_effectDatas_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UpdateHeroAttrS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
		}
		if (other.cause_ != null)
		{
			if (cause_ == null)
			{
				Cause = new CauseOrigin();
			}
			Cause.MergeFrom(other.Cause);
		}
		effectDatas_.Add(other.effectDatas_);
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 18u:
				if (cause_ == null)
				{
					Cause = new CauseOrigin();
				}
				input.ReadMessage(Cause);
				break;
			case 34u:
				effectDatas_.AddEntriesFrom(ref input, _repeated_effectDatas_codec);
				break;
			}
		}
	}
}
