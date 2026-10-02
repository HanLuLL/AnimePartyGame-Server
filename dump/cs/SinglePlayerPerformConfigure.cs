using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerPerformConfigure : IMessage<SinglePlayerPerformConfigure>, IMessage, IEquatable<SinglePlayerPerformConfigure>, IDeepCloneable<SinglePlayerPerformConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerPerformConfigure> _parser = new MessageParser<SinglePlayerPerformConfigure>(() => new SinglePlayerPerformConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int TriggerTLFieldNumber = 2;

	private string triggerTL_ = "";

	public const int EffectsFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_effects_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> effects_ = new MapField<int, int>();

	public const int BulletFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_bullet_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> bullet_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerPerformConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[15];

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
	public string TriggerTL
	{
		get
		{
			return triggerTL_;
		}
		private set
		{
			triggerTL_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Effects => effects_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Bullet => bullet_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerPerformConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerPerformConfigure(SinglePlayerPerformConfigure other)
		: this()
	{
		id_ = other.id_;
		triggerTL_ = other.triggerTL_;
		effects_ = other.effects_.Clone();
		bullet_ = other.bullet_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerPerformConfigure Clone()
	{
		return new SinglePlayerPerformConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerPerformConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerPerformConfigure other)
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
		if (TriggerTL != other.TriggerTL)
		{
			return false;
		}
		if (!Effects.Equals(other.Effects))
		{
			return false;
		}
		if (!Bullet.Equals(other.Bullet))
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
		if (TriggerTL.Length != 0)
		{
			num ^= TriggerTL.GetHashCode();
		}
		num ^= Effects.GetHashCode();
		num ^= Bullet.GetHashCode();
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
		if (TriggerTL.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(TriggerTL);
		}
		effects_.WriteTo(ref output, _map_effects_codec);
		bullet_.WriteTo(ref output, _map_bullet_codec);
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
		if (TriggerTL.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TriggerTL);
		}
		num += effects_.CalculateSize(_map_effects_codec);
		num += bullet_.CalculateSize(_map_bullet_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerPerformConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.TriggerTL.Length != 0)
			{
				TriggerTL = other.TriggerTL;
			}
			effects_.MergeFrom(other.effects_);
			bullet_.MergeFrom(other.bullet_);
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
				TriggerTL = input.ReadString();
				break;
			case 26u:
				effects_.AddEntriesFrom(ref input, _map_effects_codec);
				break;
			case 34u:
				bullet_.AddEntriesFrom(ref input, _map_bullet_codec);
				break;
			}
		}
	}
}
