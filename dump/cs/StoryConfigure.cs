using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class StoryConfigure : IMessage<StoryConfigure>, IMessage, IEquatable<StoryConfigure>, IDeepCloneable<StoryConfigure>, IBufferMessage
{
	private static readonly MessageParser<StoryConfigure> _parser = new MessageParser<StoryConfigure>(() => new StoryConfigure());

	private UnknownFieldSet _unknownFields;

	public const int StorysFieldNumber = 1;

	private static readonly FieldCodec<StoryStoryConfigure> _repeated_storys_codec = FieldCodec.ForMessage(10u, StoryStoryConfigure.Parser);

	private readonly RepeatedField<StoryStoryConfigure> storys_ = new RepeatedField<StoryStoryConfigure>();

	public const int StoryDictFieldNumber = 2;

	private static readonly MapField<int, StoryStoryConfigure>.Codec _map_storyDict_codec = new MapField<int, StoryStoryConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, StoryStoryConfigure.Parser), 18u);

	private readonly MapField<int, StoryStoryConfigure> storyDict_ = new MapField<int, StoryStoryConfigure>();

	public const int SpeakersFieldNumber = 3;

	private static readonly FieldCodec<StorySpeakerConfigure> _repeated_speakers_codec = FieldCodec.ForMessage(26u, StorySpeakerConfigure.Parser);

	private readonly RepeatedField<StorySpeakerConfigure> speakers_ = new RepeatedField<StorySpeakerConfigure>();

	public const int SpeakerDictFieldNumber = 4;

	private static readonly MapField<int, StorySpeakerConfigure>.Codec _map_speakerDict_codec = new MapField<int, StorySpeakerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, StorySpeakerConfigure.Parser), 34u);

	private readonly MapField<int, StorySpeakerConfigure> speakerDict_ = new MapField<int, StorySpeakerConfigure>();

	public const int SkinsFieldNumber = 5;

	private static readonly FieldCodec<StorySkinConfigure> _repeated_skins_codec = FieldCodec.ForMessage(42u, StorySkinConfigure.Parser);

	private readonly RepeatedField<StorySkinConfigure> skins_ = new RepeatedField<StorySkinConfigure>();

	public const int SkinDictFieldNumber = 6;

	private static readonly MapField<int, StorySkinConfigure>.Codec _map_skinDict_codec = new MapField<int, StorySkinConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, StorySkinConfigure.Parser), 50u);

	private readonly MapField<int, StorySkinConfigure> skinDict_ = new MapField<int, StorySkinConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<StoryConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => StoryReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<StoryStoryConfigure> Storys => storys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, StoryStoryConfigure> StoryDict => storyDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<StorySpeakerConfigure> Speakers => speakers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, StorySpeakerConfigure> SpeakerDict => speakerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<StorySkinConfigure> Skins => skins_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, StorySkinConfigure> SkinDict => skinDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StoryConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StoryConfigure(StoryConfigure other)
		: this()
	{
		storys_ = other.storys_.Clone();
		storyDict_ = other.storyDict_.Clone();
		speakers_ = other.speakers_.Clone();
		speakerDict_ = other.speakerDict_.Clone();
		skins_ = other.skins_.Clone();
		skinDict_ = other.skinDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StoryConfigure Clone()
	{
		return new StoryConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as StoryConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(StoryConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!storys_.Equals(other.storys_))
		{
			return false;
		}
		if (!StoryDict.Equals(other.StoryDict))
		{
			return false;
		}
		if (!speakers_.Equals(other.speakers_))
		{
			return false;
		}
		if (!SpeakerDict.Equals(other.SpeakerDict))
		{
			return false;
		}
		if (!skins_.Equals(other.skins_))
		{
			return false;
		}
		if (!SkinDict.Equals(other.SkinDict))
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
		num ^= storys_.GetHashCode();
		num ^= StoryDict.GetHashCode();
		num ^= speakers_.GetHashCode();
		num ^= SpeakerDict.GetHashCode();
		num ^= skins_.GetHashCode();
		num ^= SkinDict.GetHashCode();
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
		storys_.WriteTo(ref output, _repeated_storys_codec);
		storyDict_.WriteTo(ref output, _map_storyDict_codec);
		speakers_.WriteTo(ref output, _repeated_speakers_codec);
		speakerDict_.WriteTo(ref output, _map_speakerDict_codec);
		skins_.WriteTo(ref output, _repeated_skins_codec);
		skinDict_.WriteTo(ref output, _map_skinDict_codec);
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
		num += storys_.CalculateSize(_repeated_storys_codec);
		num += storyDict_.CalculateSize(_map_storyDict_codec);
		num += speakers_.CalculateSize(_repeated_speakers_codec);
		num += speakerDict_.CalculateSize(_map_speakerDict_codec);
		num += skins_.CalculateSize(_repeated_skins_codec);
		num += skinDict_.CalculateSize(_map_skinDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(StoryConfigure other)
	{
		if (other != null)
		{
			storys_.Add(other.storys_);
			storyDict_.MergeFrom(other.storyDict_);
			speakers_.Add(other.speakers_);
			speakerDict_.MergeFrom(other.speakerDict_);
			skins_.Add(other.skins_);
			skinDict_.MergeFrom(other.skinDict_);
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
				storys_.AddEntriesFrom(ref input, _repeated_storys_codec);
				break;
			case 18u:
				storyDict_.AddEntriesFrom(ref input, _map_storyDict_codec);
				break;
			case 26u:
				speakers_.AddEntriesFrom(ref input, _repeated_speakers_codec);
				break;
			case 34u:
				speakerDict_.AddEntriesFrom(ref input, _map_speakerDict_codec);
				break;
			case 42u:
				skins_.AddEntriesFrom(ref input, _repeated_skins_codec);
				break;
			case 50u:
				skinDict_.AddEntriesFrom(ref input, _map_skinDict_codec);
				break;
			}
		}
	}
}
