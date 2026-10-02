using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class MoveC2S : IMessage<MoveC2S>, IMessage, IEquatable<MoveC2S>, IDeepCloneable<MoveC2S>, IBufferMessage
{
	private static readonly MessageParser<MoveC2S> _parser = new MessageParser<MoveC2S>(() => new MoveC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int DirectionFieldNumber = 2;

	private int direction_;

	public const int ForceDirFieldNumber = 3;

	private bool forceDir_;

	public const int LandEffectFieldNumber = 4;

	private bool landEffect_;

	public const int PathFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_path_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> path_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MoveC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[246];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Direction
	{
		get
		{
			return direction_;
		}
		set
		{
			direction_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool ForceDir
	{
		get
		{
			return forceDir_;
		}
		set
		{
			forceDir_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool LandEffect
	{
		get
		{
			return landEffect_;
		}
		set
		{
			landEffect_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Path => path_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveC2S(MoveC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		direction_ = other.direction_;
		forceDir_ = other.forceDir_;
		landEffect_ = other.landEffect_;
		path_ = other.path_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveC2S Clone()
	{
		return new MoveC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MoveC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MoveC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (Direction != other.Direction)
		{
			return false;
		}
		if (ForceDir != other.ForceDir)
		{
			return false;
		}
		if (LandEffect != other.LandEffect)
		{
			return false;
		}
		if (!path_.Equals(other.path_))
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (Direction != 0)
		{
			num ^= Direction.GetHashCode();
		}
		if (ForceDir)
		{
			num ^= ForceDir.GetHashCode();
		}
		if (LandEffect)
		{
			num ^= LandEffect.GetHashCode();
		}
		num ^= path_.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (Direction != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Direction);
		}
		if (ForceDir)
		{
			output.WriteRawTag(24);
			output.WriteBool(ForceDir);
		}
		if (LandEffect)
		{
			output.WriteRawTag(32);
			output.WriteBool(LandEffect);
		}
		path_.WriteTo(ref output, _repeated_path_codec);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (Direction != 0)
		{
			num += 5;
		}
		if (ForceDir)
		{
			num += 2;
		}
		if (LandEffect)
		{
			num += 2;
		}
		num += path_.CalculateSize(_repeated_path_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MoveC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.Direction != 0)
		{
			Direction = other.Direction;
		}
		if (other.ForceDir)
		{
			ForceDir = other.ForceDir;
		}
		if (other.LandEffect)
		{
			LandEffect = other.LandEffect;
		}
		path_.Add(other.path_);
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
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 21u:
				Direction = input.ReadSFixed32();
				break;
			case 24u:
				ForceDir = input.ReadBool();
				break;
			case 32u:
				LandEffect = input.ReadBool();
				break;
			case 42u:
			case 45u:
				path_.AddEntriesFrom(ref input, _repeated_path_codec);
				break;
			}
		}
	}
}
