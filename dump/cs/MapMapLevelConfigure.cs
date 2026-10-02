using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MapMapLevelConfigure : IMessage<MapMapLevelConfigure>, IMessage, IEquatable<MapMapLevelConfigure>, IDeepCloneable<MapMapLevelConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapMapLevelConfigure> _parser = new MessageParser<MapMapLevelConfigure>(() => new MapMapLevelConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int MapMapLevelConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<MapMapLevelConfigureItem> _repeated_mapMapLevelConfigureItems_codec = FieldCodec.ForMessage(18u, MapMapLevelConfigureItem.Parser);

	private readonly RepeatedField<MapMapLevelConfigureItem> mapMapLevelConfigureItems_ = new RepeatedField<MapMapLevelConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapMapLevelConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[5];

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
	public RepeatedField<MapMapLevelConfigureItem> MapMapLevelConfigureItems => mapMapLevelConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapLevelConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapLevelConfigure(MapMapLevelConfigure other)
		: this()
	{
		id_ = other.id_;
		mapMapLevelConfigureItems_ = other.mapMapLevelConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMapLevelConfigure Clone()
	{
		return new MapMapLevelConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapMapLevelConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapMapLevelConfigure other)
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
		if (!mapMapLevelConfigureItems_.Equals(other.mapMapLevelConfigureItems_))
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
		num ^= mapMapLevelConfigureItems_.GetHashCode();
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
		mapMapLevelConfigureItems_.WriteTo(ref output, _repeated_mapMapLevelConfigureItems_codec);
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
		num += mapMapLevelConfigureItems_.CalculateSize(_repeated_mapMapLevelConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapMapLevelConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			mapMapLevelConfigureItems_.Add(other.mapMapLevelConfigureItems_);
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
				mapMapLevelConfigureItems_.AddEntriesFrom(ref input, _repeated_mapMapLevelConfigureItems_codec);
				break;
			}
		}
	}

	public void FixData(FixMapMapLevelConfigure FixData)
	{
		RepeatedField<FixMapMapLevelConfigureItem> fixMapMapLevelConfigureItems = FixData.FixMapMapLevelConfigureItems;
		for (int i = 0; i < fixMapMapLevelConfigureItems.Count; i++)
		{
			mapMapLevelConfigureItems_[i].FixData(fixMapMapLevelConfigureItems[i]);
		}
	}
}
