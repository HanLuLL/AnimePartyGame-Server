using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RemoveResourceConfigure : IMessage<RemoveResourceConfigure>, IMessage, IEquatable<RemoveResourceConfigure>, IDeepCloneable<RemoveResourceConfigure>, IBufferMessage
{
	private static readonly MessageParser<RemoveResourceConfigure> _parser = new MessageParser<RemoveResourceConfigure>(() => new RemoveResourceConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ImagesFieldNumber = 1;

	private static readonly FieldCodec<RemoveResourceImageConfigure> _repeated_images_codec = FieldCodec.ForMessage(10u, RemoveResourceImageConfigure.Parser);

	private readonly RepeatedField<RemoveResourceImageConfigure> images_ = new RepeatedField<RemoveResourceImageConfigure>();

	public const int ImageDictFieldNumber = 2;

	private static readonly MapField<int, RemoveResourceImageConfigure>.Codec _map_imageDict_codec = new MapField<int, RemoveResourceImageConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RemoveResourceImageConfigure.Parser), 18u);

	private readonly MapField<int, RemoveResourceImageConfigure> imageDict_ = new MapField<int, RemoveResourceImageConfigure>();

	public const int VideosFieldNumber = 3;

	private static readonly FieldCodec<RemoveResourceVideoConfigure> _repeated_videos_codec = FieldCodec.ForMessage(26u, RemoveResourceVideoConfigure.Parser);

	private readonly RepeatedField<RemoveResourceVideoConfigure> videos_ = new RepeatedField<RemoveResourceVideoConfigure>();

	public const int VideoDictFieldNumber = 4;

	private static readonly MapField<int, RemoveResourceVideoConfigure>.Codec _map_videoDict_codec = new MapField<int, RemoveResourceVideoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RemoveResourceVideoConfigure.Parser), 34u);

	private readonly MapField<int, RemoveResourceVideoConfigure> videoDict_ = new MapField<int, RemoveResourceVideoConfigure>();

	public const int AudiosFieldNumber = 5;

	private static readonly FieldCodec<RemoveResourceAudioConfigure> _repeated_audios_codec = FieldCodec.ForMessage(42u, RemoveResourceAudioConfigure.Parser);

	private readonly RepeatedField<RemoveResourceAudioConfigure> audios_ = new RepeatedField<RemoveResourceAudioConfigure>();

	public const int AudioDictFieldNumber = 6;

	private static readonly MapField<int, RemoveResourceAudioConfigure>.Codec _map_audioDict_codec = new MapField<int, RemoveResourceAudioConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RemoveResourceAudioConfigure.Parser), 50u);

	private readonly MapField<int, RemoveResourceAudioConfigure> audioDict_ = new MapField<int, RemoveResourceAudioConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RemoveResourceConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RemoveResourceReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RemoveResourceImageConfigure> Images => images_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RemoveResourceImageConfigure> ImageDict => imageDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RemoveResourceVideoConfigure> Videos => videos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RemoveResourceVideoConfigure> VideoDict => videoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RemoveResourceAudioConfigure> Audios => audios_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RemoveResourceAudioConfigure> AudioDict => audioDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RemoveResourceConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RemoveResourceConfigure(RemoveResourceConfigure other)
		: this()
	{
		images_ = other.images_.Clone();
		imageDict_ = other.imageDict_.Clone();
		videos_ = other.videos_.Clone();
		videoDict_ = other.videoDict_.Clone();
		audios_ = other.audios_.Clone();
		audioDict_ = other.audioDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RemoveResourceConfigure Clone()
	{
		return new RemoveResourceConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RemoveResourceConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RemoveResourceConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!images_.Equals(other.images_))
		{
			return false;
		}
		if (!ImageDict.Equals(other.ImageDict))
		{
			return false;
		}
		if (!videos_.Equals(other.videos_))
		{
			return false;
		}
		if (!VideoDict.Equals(other.VideoDict))
		{
			return false;
		}
		if (!audios_.Equals(other.audios_))
		{
			return false;
		}
		if (!AudioDict.Equals(other.AudioDict))
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
		num ^= images_.GetHashCode();
		num ^= ImageDict.GetHashCode();
		num ^= videos_.GetHashCode();
		num ^= VideoDict.GetHashCode();
		num ^= audios_.GetHashCode();
		num ^= AudioDict.GetHashCode();
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
		images_.WriteTo(ref output, _repeated_images_codec);
		imageDict_.WriteTo(ref output, _map_imageDict_codec);
		videos_.WriteTo(ref output, _repeated_videos_codec);
		videoDict_.WriteTo(ref output, _map_videoDict_codec);
		audios_.WriteTo(ref output, _repeated_audios_codec);
		audioDict_.WriteTo(ref output, _map_audioDict_codec);
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
		num += images_.CalculateSize(_repeated_images_codec);
		num += imageDict_.CalculateSize(_map_imageDict_codec);
		num += videos_.CalculateSize(_repeated_videos_codec);
		num += videoDict_.CalculateSize(_map_videoDict_codec);
		num += audios_.CalculateSize(_repeated_audios_codec);
		num += audioDict_.CalculateSize(_map_audioDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RemoveResourceConfigure other)
	{
		if (other != null)
		{
			images_.Add(other.images_);
			imageDict_.MergeFrom(other.imageDict_);
			videos_.Add(other.videos_);
			videoDict_.MergeFrom(other.videoDict_);
			audios_.Add(other.audios_);
			audioDict_.MergeFrom(other.audioDict_);
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
				images_.AddEntriesFrom(ref input, _repeated_images_codec);
				break;
			case 18u:
				imageDict_.AddEntriesFrom(ref input, _map_imageDict_codec);
				break;
			case 26u:
				videos_.AddEntriesFrom(ref input, _repeated_videos_codec);
				break;
			case 34u:
				videoDict_.AddEntriesFrom(ref input, _map_videoDict_codec);
				break;
			case 42u:
				audios_.AddEntriesFrom(ref input, _repeated_audios_codec);
				break;
			case 50u:
				audioDict_.AddEntriesFrom(ref input, _map_audioDict_codec);
				break;
			}
		}
	}
}
