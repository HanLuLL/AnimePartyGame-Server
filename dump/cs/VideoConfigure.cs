using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class VideoConfigure : IMessage<VideoConfigure>, IMessage, IEquatable<VideoConfigure>, IDeepCloneable<VideoConfigure>, IBufferMessage
{
	private static readonly MessageParser<VideoConfigure> _parser = new MessageParser<VideoConfigure>(() => new VideoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GlobalsFieldNumber = 1;

	private static readonly FieldCodec<VideoGlobalConfigure> _repeated_globals_codec = FieldCodec.ForMessage(10u, VideoGlobalConfigure.Parser);

	private readonly RepeatedField<VideoGlobalConfigure> globals_ = new RepeatedField<VideoGlobalConfigure>();

	public const int GlobalDictFieldNumber = 2;

	private static readonly MapField<int, VideoGlobalConfigure>.Codec _map_globalDict_codec = new MapField<int, VideoGlobalConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, VideoGlobalConfigure.Parser), 18u);

	private readonly MapField<int, VideoGlobalConfigure> globalDict_ = new MapField<int, VideoGlobalConfigure>();

	public const int VideoQueuesFieldNumber = 3;

	private static readonly FieldCodec<VideoVideoQueueConfigure> _repeated_videoQueues_codec = FieldCodec.ForMessage(26u, VideoVideoQueueConfigure.Parser);

	private readonly RepeatedField<VideoVideoQueueConfigure> videoQueues_ = new RepeatedField<VideoVideoQueueConfigure>();

	public const int VideoQueueDictFieldNumber = 4;

	private static readonly MapField<int, VideoVideoQueueConfigure>.Codec _map_videoQueueDict_codec = new MapField<int, VideoVideoQueueConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, VideoVideoQueueConfigure.Parser), 34u);

	private readonly MapField<int, VideoVideoQueueConfigure> videoQueueDict_ = new MapField<int, VideoVideoQueueConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<VideoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => VideoReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<VideoGlobalConfigure> Globals => globals_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, VideoGlobalConfigure> GlobalDict => globalDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<VideoVideoQueueConfigure> VideoQueues => videoQueues_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, VideoVideoQueueConfigure> VideoQueueDict => videoQueueDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VideoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VideoConfigure(VideoConfigure other)
		: this()
	{
		globals_ = other.globals_.Clone();
		globalDict_ = other.globalDict_.Clone();
		videoQueues_ = other.videoQueues_.Clone();
		videoQueueDict_ = other.videoQueueDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VideoConfigure Clone()
	{
		return new VideoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as VideoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(VideoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!globals_.Equals(other.globals_))
		{
			return false;
		}
		if (!GlobalDict.Equals(other.GlobalDict))
		{
			return false;
		}
		if (!videoQueues_.Equals(other.videoQueues_))
		{
			return false;
		}
		if (!VideoQueueDict.Equals(other.VideoQueueDict))
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
		num ^= globals_.GetHashCode();
		num ^= GlobalDict.GetHashCode();
		num ^= videoQueues_.GetHashCode();
		num ^= VideoQueueDict.GetHashCode();
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
		globals_.WriteTo(ref output, _repeated_globals_codec);
		globalDict_.WriteTo(ref output, _map_globalDict_codec);
		videoQueues_.WriteTo(ref output, _repeated_videoQueues_codec);
		videoQueueDict_.WriteTo(ref output, _map_videoQueueDict_codec);
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
		num += globals_.CalculateSize(_repeated_globals_codec);
		num += globalDict_.CalculateSize(_map_globalDict_codec);
		num += videoQueues_.CalculateSize(_repeated_videoQueues_codec);
		num += videoQueueDict_.CalculateSize(_map_videoQueueDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(VideoConfigure other)
	{
		if (other != null)
		{
			globals_.Add(other.globals_);
			globalDict_.MergeFrom(other.globalDict_);
			videoQueues_.Add(other.videoQueues_);
			videoQueueDict_.MergeFrom(other.videoQueueDict_);
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
			case 10u:
				globals_.AddEntriesFrom(ref input, _repeated_globals_codec);
				break;
			case 18u:
				globalDict_.AddEntriesFrom(ref input, _map_globalDict_codec);
				break;
			case 26u:
				videoQueues_.AddEntriesFrom(ref input, _repeated_videoQueues_codec);
				break;
			case 34u:
				videoQueueDict_.AddEntriesFrom(ref input, _map_videoQueueDict_codec);
				break;
			}
		}
	}
}
