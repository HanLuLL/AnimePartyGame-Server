using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class SkinSkinPendantConfigure : IMessage<SkinSkinPendantConfigure>, IMessage, IEquatable<SkinSkinPendantConfigure>, IDeepCloneable<SkinSkinPendantConfigure>, IBufferMessage
{
	private static readonly MessageParser<SkinSkinPendantConfigure> _parser = new MessageParser<SkinSkinPendantConfigure>(() => new SkinSkinPendantConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PendantIdFieldNumber = 2;

	private int pendantId_;

	public const int ReplacePerformFieldNumber = 3;

	private int replacePerform_;

	public const int ShowVideoFieldNumber = 4;

	private string showVideo_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkinSkinPendantConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SkinReflection.Descriptor.MessageTypes[3];

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
	public int PendantId
	{
		get
		{
			return pendantId_;
		}
		private set
		{
			pendantId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ReplacePerform
	{
		get
		{
			return replacePerform_;
		}
		private set
		{
			replacePerform_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ShowVideo
	{
		get
		{
			return showVideo_;
		}
		private set
		{
			showVideo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSkinPendantConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSkinPendantConfigure(SkinSkinPendantConfigure other)
		: this()
	{
		id_ = other.id_;
		pendantId_ = other.pendantId_;
		replacePerform_ = other.replacePerform_;
		showVideo_ = other.showVideo_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkinSkinPendantConfigure Clone()
	{
		return new SkinSkinPendantConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkinSkinPendantConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkinSkinPendantConfigure other)
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
		if (PendantId != other.PendantId)
		{
			return false;
		}
		if (ReplacePerform != other.ReplacePerform)
		{
			return false;
		}
		if (ShowVideo != other.ShowVideo)
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
		if (PendantId != 0)
		{
			num ^= PendantId.GetHashCode();
		}
		if (ReplacePerform != 0)
		{
			num ^= ReplacePerform.GetHashCode();
		}
		if (ShowVideo.Length != 0)
		{
			num ^= ShowVideo.GetHashCode();
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
		if (PendantId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(PendantId);
		}
		if (ReplacePerform != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ReplacePerform);
		}
		if (ShowVideo.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(ShowVideo);
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
		if (PendantId != 0)
		{
			num += 5;
		}
		if (ReplacePerform != 0)
		{
			num += 5;
		}
		if (ShowVideo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ShowVideo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SkinSkinPendantConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PendantId != 0)
			{
				PendantId = other.PendantId;
			}
			if (other.ReplacePerform != 0)
			{
				ReplacePerform = other.ReplacePerform;
			}
			if (other.ShowVideo.Length != 0)
			{
				ShowVideo = other.ShowVideo;
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
				PendantId = input.ReadSFixed32();
				break;
			case 29u:
				ReplacePerform = input.ReadSFixed32();
				break;
			case 34u:
				ShowVideo = input.ReadString();
				break;
			}
		}
	}
}
