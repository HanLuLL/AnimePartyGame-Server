using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GameModeGalleryMapConfigure : IMessage<GameModeGalleryMapConfigure>, IMessage, IEquatable<GameModeGalleryMapConfigure>, IDeepCloneable<GameModeGalleryMapConfigure>, IBufferMessage
{
	private static readonly MessageParser<GameModeGalleryMapConfigure> _parser = new MessageParser<GameModeGalleryMapConfigure>(() => new GameModeGalleryMapConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GalleryMapTypeFieldNumber = 1;

	private GalleryMapType galleryMapType_;

	public const int MapIDFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_mapID_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> mapID_ = new RepeatedField<int>();

	public const int GallerynameIDFieldNumber = 3;

	private int gallerynameID_;

	public const int GalleryDesIDFieldNumber = 4;

	private int galleryDesID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeGalleryMapConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[6];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GalleryMapType GalleryMapType
	{
		get
		{
			return galleryMapType_;
		}
		private set
		{
			galleryMapType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapID => mapID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GallerynameID
	{
		get
		{
			return gallerynameID_;
		}
		private set
		{
			gallerynameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GalleryDesID
	{
		get
		{
			return galleryDesID_;
		}
		private set
		{
			galleryDesID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeGalleryMapConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeGalleryMapConfigure(GameModeGalleryMapConfigure other)
		: this()
	{
		galleryMapType_ = other.galleryMapType_;
		mapID_ = other.mapID_.Clone();
		gallerynameID_ = other.gallerynameID_;
		galleryDesID_ = other.galleryDesID_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeGalleryMapConfigure Clone()
	{
		return new GameModeGalleryMapConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeGalleryMapConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeGalleryMapConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GalleryMapType != other.GalleryMapType)
		{
			return false;
		}
		if (!mapID_.Equals(other.mapID_))
		{
			return false;
		}
		if (GallerynameID != other.GallerynameID)
		{
			return false;
		}
		if (GalleryDesID != other.GalleryDesID)
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
		if (GalleryMapType != GalleryMapType.None)
		{
			num ^= GalleryMapType.GetHashCode();
		}
		num ^= mapID_.GetHashCode();
		if (GallerynameID != 0)
		{
			num ^= GallerynameID.GetHashCode();
		}
		if (GalleryDesID != 0)
		{
			num ^= GalleryDesID.GetHashCode();
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
		if (GalleryMapType != GalleryMapType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)GalleryMapType);
		}
		mapID_.WriteTo(ref output, _repeated_mapID_codec);
		if (GallerynameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(GallerynameID);
		}
		if (GalleryDesID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(GalleryDesID);
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
		if (GalleryMapType != GalleryMapType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)GalleryMapType);
		}
		num += mapID_.CalculateSize(_repeated_mapID_codec);
		if (GallerynameID != 0)
		{
			num += 5;
		}
		if (GalleryDesID != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameModeGalleryMapConfigure other)
	{
		if (other != null)
		{
			if (other.GalleryMapType != GalleryMapType.None)
			{
				GalleryMapType = other.GalleryMapType;
			}
			mapID_.Add(other.mapID_);
			if (other.GallerynameID != 0)
			{
				GallerynameID = other.GallerynameID;
			}
			if (other.GalleryDesID != 0)
			{
				GalleryDesID = other.GalleryDesID;
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
			case 8u:
				GalleryMapType = (GalleryMapType)input.ReadEnum();
				break;
			case 18u:
			case 21u:
				mapID_.AddEntriesFrom(ref input, _repeated_mapID_codec);
				break;
			case 29u:
				GallerynameID = input.ReadSFixed32();
				break;
			case 37u:
				GalleryDesID = input.ReadSFixed32();
				break;
			}
		}
	}
}
