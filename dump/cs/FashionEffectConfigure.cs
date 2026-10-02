using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class FashionEffectConfigure : IMessage<FashionEffectConfigure>, IMessage, IEquatable<FashionEffectConfigure>, IDeepCloneable<FashionEffectConfigure>, IBufferMessage
{
	private static readonly MessageParser<FashionEffectConfigure> _parser = new MessageParser<FashionEffectConfigure>(() => new FashionEffectConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int KillVfxFieldNumber = 2;

	private int killVfx_;

	public const int IsRootFieldNumber = 3;

	private bool isRoot_;

	public const int FlyVfxFieldNumber = 4;

	private int flyVfx_;

	public const int KillCameraShakeFieldNumber = 5;

	private string killCameraShake_ = "";

	public const int PreviewVideoFieldNumber = 6;

	private string previewVideo_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FashionEffectConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FashionReflection.Descriptor.MessageTypes[2];

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
	public int KillVfx
	{
		get
		{
			return killVfx_;
		}
		private set
		{
			killVfx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRoot
	{
		get
		{
			return isRoot_;
		}
		private set
		{
			isRoot_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FlyVfx
	{
		get
		{
			return flyVfx_;
		}
		private set
		{
			flyVfx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string KillCameraShake
	{
		get
		{
			return killCameraShake_;
		}
		private set
		{
			killCameraShake_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PreviewVideo
	{
		get
		{
			return previewVideo_;
		}
		private set
		{
			previewVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionEffectConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionEffectConfigure(FashionEffectConfigure other)
		: this()
	{
		id_ = other.id_;
		killVfx_ = other.killVfx_;
		isRoot_ = other.isRoot_;
		flyVfx_ = other.flyVfx_;
		killCameraShake_ = other.killCameraShake_;
		previewVideo_ = other.previewVideo_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FashionEffectConfigure Clone()
	{
		return new FashionEffectConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FashionEffectConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FashionEffectConfigure other)
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
		if (KillVfx != other.KillVfx)
		{
			return false;
		}
		if (IsRoot != other.IsRoot)
		{
			return false;
		}
		if (FlyVfx != other.FlyVfx)
		{
			return false;
		}
		if (KillCameraShake != other.KillCameraShake)
		{
			return false;
		}
		if (PreviewVideo != other.PreviewVideo)
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
		if (KillVfx != 0)
		{
			num ^= KillVfx.GetHashCode();
		}
		if (IsRoot)
		{
			num ^= IsRoot.GetHashCode();
		}
		if (FlyVfx != 0)
		{
			num ^= FlyVfx.GetHashCode();
		}
		if (KillCameraShake.Length != 0)
		{
			num ^= KillCameraShake.GetHashCode();
		}
		if (PreviewVideo.Length != 0)
		{
			num ^= PreviewVideo.GetHashCode();
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
		if (KillVfx != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(KillVfx);
		}
		if (IsRoot)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsRoot);
		}
		if (FlyVfx != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(FlyVfx);
		}
		if (KillCameraShake.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(KillCameraShake);
		}
		if (PreviewVideo.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(PreviewVideo);
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
		if (KillVfx != 0)
		{
			num += 5;
		}
		if (IsRoot)
		{
			num += 2;
		}
		if (FlyVfx != 0)
		{
			num += 5;
		}
		if (KillCameraShake.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(KillCameraShake);
		}
		if (PreviewVideo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PreviewVideo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FashionEffectConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.KillVfx != 0)
			{
				KillVfx = other.KillVfx;
			}
			if (other.IsRoot)
			{
				IsRoot = other.IsRoot;
			}
			if (other.FlyVfx != 0)
			{
				FlyVfx = other.FlyVfx;
			}
			if (other.KillCameraShake.Length != 0)
			{
				KillCameraShake = other.KillCameraShake;
			}
			if (other.PreviewVideo.Length != 0)
			{
				PreviewVideo = other.PreviewVideo;
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
				KillVfx = input.ReadSFixed32();
				break;
			case 24u:
				IsRoot = input.ReadBool();
				break;
			case 37u:
				FlyVfx = input.ReadSFixed32();
				break;
			case 42u:
				KillCameraShake = input.ReadString();
				break;
			case 50u:
				PreviewVideo = input.ReadString();
				break;
			}
		}
	}
}
