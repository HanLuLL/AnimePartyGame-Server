using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerBulletConfigure : IMessage<SinglePlayerBulletConfigure>, IMessage, IEquatable<SinglePlayerBulletConfigure>, IDeepCloneable<SinglePlayerBulletConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerBulletConfigure> _parser = new MessageParser<SinglePlayerBulletConfigure>(() => new SinglePlayerBulletConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BulletEffectidFieldNumber = 2;

	private int bulletEffectid_;

	public const int HitEffectIdFieldNumber = 3;

	private int hitEffectId_;

	public const int InitialSpeedFieldNumber = 4;

	private float initialSpeed_;

	public const int MaxSpeedFieldNumber = 5;

	private float maxSpeed_;

	public const int AccelerationFieldNumber = 6;

	private float acceleration_;

	public const int InitialTurnSpeedFieldNumber = 7;

	private float initialTurnSpeed_;

	public const int TurnAccelerationFieldNumber = 8;

	private float turnAcceleration_;

	public const int HitSoundctIdFieldNumber = 9;

	private int hitSoundctId_;

	public const int ScreenShakeFieldNumber = 10;

	private string screenShake_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerBulletConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[14];

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
	public int BulletEffectid
	{
		get
		{
			return bulletEffectid_;
		}
		private set
		{
			bulletEffectid_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HitEffectId
	{
		get
		{
			return hitEffectId_;
		}
		private set
		{
			hitEffectId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float InitialSpeed
	{
		get
		{
			return initialSpeed_;
		}
		private set
		{
			initialSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float MaxSpeed
	{
		get
		{
			return maxSpeed_;
		}
		private set
		{
			maxSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float Acceleration
	{
		get
		{
			return acceleration_;
		}
		private set
		{
			acceleration_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float InitialTurnSpeed
	{
		get
		{
			return initialTurnSpeed_;
		}
		private set
		{
			initialTurnSpeed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float TurnAcceleration
	{
		get
		{
			return turnAcceleration_;
		}
		private set
		{
			turnAcceleration_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HitSoundctId
	{
		get
		{
			return hitSoundctId_;
		}
		private set
		{
			hitSoundctId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ScreenShake
	{
		get
		{
			return screenShake_;
		}
		private set
		{
			screenShake_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerBulletConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerBulletConfigure(SinglePlayerBulletConfigure other)
		: this()
	{
		id_ = other.id_;
		bulletEffectid_ = other.bulletEffectid_;
		hitEffectId_ = other.hitEffectId_;
		initialSpeed_ = other.initialSpeed_;
		maxSpeed_ = other.maxSpeed_;
		acceleration_ = other.acceleration_;
		initialTurnSpeed_ = other.initialTurnSpeed_;
		turnAcceleration_ = other.turnAcceleration_;
		hitSoundctId_ = other.hitSoundctId_;
		screenShake_ = other.screenShake_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerBulletConfigure Clone()
	{
		return new SinglePlayerBulletConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerBulletConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerBulletConfigure other)
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
		if (BulletEffectid != other.BulletEffectid)
		{
			return false;
		}
		if (HitEffectId != other.HitEffectId)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(InitialSpeed, other.InitialSpeed))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MaxSpeed, other.MaxSpeed))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Acceleration, other.Acceleration))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(InitialTurnSpeed, other.InitialTurnSpeed))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(TurnAcceleration, other.TurnAcceleration))
		{
			return false;
		}
		if (HitSoundctId != other.HitSoundctId)
		{
			return false;
		}
		if (ScreenShake != other.ScreenShake)
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
		if (BulletEffectid != 0)
		{
			num ^= BulletEffectid.GetHashCode();
		}
		if (HitEffectId != 0)
		{
			num ^= HitEffectId.GetHashCode();
		}
		if (InitialSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(InitialSpeed);
		}
		if (MaxSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MaxSpeed);
		}
		if (Acceleration != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Acceleration);
		}
		if (InitialTurnSpeed != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(InitialTurnSpeed);
		}
		if (TurnAcceleration != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(TurnAcceleration);
		}
		if (HitSoundctId != 0)
		{
			num ^= HitSoundctId.GetHashCode();
		}
		if (ScreenShake.Length != 0)
		{
			num ^= ScreenShake.GetHashCode();
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
		if (BulletEffectid != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(BulletEffectid);
		}
		if (HitEffectId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(HitEffectId);
		}
		if (InitialSpeed != 0f)
		{
			output.WriteRawTag(37);
			output.WriteFloat(InitialSpeed);
		}
		if (MaxSpeed != 0f)
		{
			output.WriteRawTag(45);
			output.WriteFloat(MaxSpeed);
		}
		if (Acceleration != 0f)
		{
			output.WriteRawTag(53);
			output.WriteFloat(Acceleration);
		}
		if (InitialTurnSpeed != 0f)
		{
			output.WriteRawTag(61);
			output.WriteFloat(InitialTurnSpeed);
		}
		if (TurnAcceleration != 0f)
		{
			output.WriteRawTag(69);
			output.WriteFloat(TurnAcceleration);
		}
		if (HitSoundctId != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(HitSoundctId);
		}
		if (ScreenShake.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(ScreenShake);
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
		if (BulletEffectid != 0)
		{
			num += 5;
		}
		if (HitEffectId != 0)
		{
			num += 5;
		}
		if (InitialSpeed != 0f)
		{
			num += 5;
		}
		if (MaxSpeed != 0f)
		{
			num += 5;
		}
		if (Acceleration != 0f)
		{
			num += 5;
		}
		if (InitialTurnSpeed != 0f)
		{
			num += 5;
		}
		if (TurnAcceleration != 0f)
		{
			num += 5;
		}
		if (HitSoundctId != 0)
		{
			num += 5;
		}
		if (ScreenShake.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ScreenShake);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerBulletConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.BulletEffectid != 0)
			{
				BulletEffectid = other.BulletEffectid;
			}
			if (other.HitEffectId != 0)
			{
				HitEffectId = other.HitEffectId;
			}
			if (other.InitialSpeed != 0f)
			{
				InitialSpeed = other.InitialSpeed;
			}
			if (other.MaxSpeed != 0f)
			{
				MaxSpeed = other.MaxSpeed;
			}
			if (other.Acceleration != 0f)
			{
				Acceleration = other.Acceleration;
			}
			if (other.InitialTurnSpeed != 0f)
			{
				InitialTurnSpeed = other.InitialTurnSpeed;
			}
			if (other.TurnAcceleration != 0f)
			{
				TurnAcceleration = other.TurnAcceleration;
			}
			if (other.HitSoundctId != 0)
			{
				HitSoundctId = other.HitSoundctId;
			}
			if (other.ScreenShake.Length != 0)
			{
				ScreenShake = other.ScreenShake;
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
			case 21u:
				BulletEffectid = input.ReadSFixed32();
				break;
			case 29u:
				HitEffectId = input.ReadSFixed32();
				break;
			case 37u:
				InitialSpeed = input.ReadFloat();
				break;
			case 45u:
				MaxSpeed = input.ReadFloat();
				break;
			case 53u:
				Acceleration = input.ReadFloat();
				break;
			case 61u:
				InitialTurnSpeed = input.ReadFloat();
				break;
			case 69u:
				TurnAcceleration = input.ReadFloat();
				break;
			case 77u:
				HitSoundctId = input.ReadSFixed32();
				break;
			case 82u:
				ScreenShake = input.ReadString();
				break;
			}
		}
	}
}
