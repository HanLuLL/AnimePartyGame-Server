using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixINTSteamConfigure : IMessage<FixINTSteamConfigure>, IMessage, IEquatable<FixINTSteamConfigure>, IDeepCloneable<FixINTSteamConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixINTSteamConfigure> _parser = new MessageParser<FixINTSteamConfigure>(() => new FixINTSteamConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ItemInfosFieldNumber = 1;

	private static readonly FieldCodec<FixINTSteamItemInfoConfigure> _repeated_itemInfos_codec = FieldCodec.ForMessage(10u, FixINTSteamItemInfoConfigure.Parser);

	private readonly RepeatedField<FixINTSteamItemInfoConfigure> itemInfos_ = new RepeatedField<FixINTSteamItemInfoConfigure>();

	public const int ItemInfoDictFieldNumber = 2;

	private static readonly MapField<int, FixINTSteamItemInfoConfigure>.Codec _map_itemInfoDict_codec = new MapField<int, FixINTSteamItemInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixINTSteamItemInfoConfigure.Parser), 18u);

	private readonly MapField<int, FixINTSteamItemInfoConfigure> itemInfoDict_ = new MapField<int, FixINTSteamItemInfoConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixINTSteamConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixINTSteamReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixINTSteamItemInfoConfigure> ItemInfos => itemInfos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixINTSteamItemInfoConfigure> ItemInfoDict => itemInfoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTSteamConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTSteamConfigure(FixINTSteamConfigure other)
		: this()
	{
		itemInfos_ = other.itemInfos_.Clone();
		itemInfoDict_ = other.itemInfoDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixINTSteamConfigure Clone()
	{
		return new FixINTSteamConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixINTSteamConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixINTSteamConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!itemInfos_.Equals(other.itemInfos_))
		{
			return false;
		}
		if (!ItemInfoDict.Equals(other.ItemInfoDict))
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
		num ^= itemInfos_.GetHashCode();
		num ^= ItemInfoDict.GetHashCode();
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
		itemInfos_.WriteTo(ref output, _repeated_itemInfos_codec);
		itemInfoDict_.WriteTo(ref output, _map_itemInfoDict_codec);
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
		num += itemInfos_.CalculateSize(_repeated_itemInfos_codec);
		num += itemInfoDict_.CalculateSize(_map_itemInfoDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixINTSteamConfigure other)
	{
		if (other != null)
		{
			itemInfos_.Add(other.itemInfos_);
			itemInfoDict_.MergeFrom(other.itemInfoDict_);
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
				itemInfos_.AddEntriesFrom(ref input, _repeated_itemInfos_codec);
				break;
			case 18u:
				itemInfoDict_.AddEntriesFrom(ref input, _map_itemInfoDict_codec);
				break;
			}
		}
	}
}
