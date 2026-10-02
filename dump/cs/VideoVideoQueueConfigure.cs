using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class VideoVideoQueueConfigure : IMessage<VideoVideoQueueConfigure>, IMessage, IEquatable<VideoVideoQueueConfigure>, IDeepCloneable<VideoVideoQueueConfigure>, IBufferMessage
{
	private static readonly MessageParser<VideoVideoQueueConfigure> _parser = new MessageParser<VideoVideoQueueConfigure>(() => new VideoVideoQueueConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int LoadedKeyStartFieldNumber = 2;

	private string loadedKeyStart_ = "";

	public const int LoadedKeyLoopFieldNumber = 3;

	private string loadedKeyLoop_ = "";

	public const int LoadedKeyEndFieldNumber = 4;

	private string loadedKeyEnd_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<VideoVideoQueueConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => VideoReflection.Descriptor.MessageTypes[1];

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
	public string LoadedKeyStart
	{
		get
		{
			return loadedKeyStart_;
		}
		private set
		{
			loadedKeyStart_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeyLoop
	{
		get
		{
			return loadedKeyLoop_;
		}
		private set
		{
			loadedKeyLoop_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LoadedKeyEnd
	{
		get
		{
			return loadedKeyEnd_;
		}
		private set
		{
			loadedKeyEnd_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VideoVideoQueueConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VideoVideoQueueConfigure(VideoVideoQueueConfigure other)
		: this()
	{
		id_ = other.id_;
		loadedKeyStart_ = other.loadedKeyStart_;
		loadedKeyLoop_ = other.loadedKeyLoop_;
		loadedKeyEnd_ = other.loadedKeyEnd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VideoVideoQueueConfigure Clone()
	{
		return new VideoVideoQueueConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as VideoVideoQueueConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(VideoVideoQueueConfigure other)
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
		if (LoadedKeyStart != other.LoadedKeyStart)
		{
			return false;
		}
		if (LoadedKeyLoop != other.LoadedKeyLoop)
		{
			return false;
		}
		if (LoadedKeyEnd != other.LoadedKeyEnd)
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
		if (LoadedKeyStart.Length != 0)
		{
			num ^= LoadedKeyStart.GetHashCode();
		}
		if (LoadedKeyLoop.Length != 0)
		{
			num ^= LoadedKeyLoop.GetHashCode();
		}
		if (LoadedKeyEnd.Length != 0)
		{
			num ^= LoadedKeyEnd.GetHashCode();
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
		if (LoadedKeyStart.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(LoadedKeyStart);
		}
		if (LoadedKeyLoop.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(LoadedKeyLoop);
		}
		if (LoadedKeyEnd.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(LoadedKeyEnd);
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
		if (LoadedKeyStart.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyStart);
		}
		if (LoadedKeyLoop.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyLoop);
		}
		if (LoadedKeyEnd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LoadedKeyEnd);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(VideoVideoQueueConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.LoadedKeyStart.Length != 0)
			{
				LoadedKeyStart = other.LoadedKeyStart;
			}
			if (other.LoadedKeyLoop.Length != 0)
			{
				LoadedKeyLoop = other.LoadedKeyLoop;
			}
			if (other.LoadedKeyEnd.Length != 0)
			{
				LoadedKeyEnd = other.LoadedKeyEnd;
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
				LoadedKeyStart = input.ReadString();
				break;
			case 26u:
				LoadedKeyLoop = input.ReadString();
				break;
			case 34u:
				LoadedKeyEnd = input.ReadString();
				break;
			}
		}
	}
}
