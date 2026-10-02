using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MapSceneConfigure : IMessage<MapSceneConfigure>, IMessage, IEquatable<MapSceneConfigure>, IDeepCloneable<MapSceneConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapSceneConfigure> _parser = new MessageParser<MapSceneConfigure>(() => new MapSceneConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int AssetNameFieldNumber = 2;

	private string assetName_ = "";

	public const int MiniMapNameFieldNumber = 3;

	private string miniMapName_ = "";

	public const int CharacterHeightFieldNumber = 4;

	private float characterHeight_;

	public const int SummonHeightFieldNumber = 5;

	private float summonHeight_;

	public const int BgmFieldNumber = 6;

	private int bgm_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapSceneConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[1];

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
	public string AssetName
	{
		get
		{
			return assetName_;
		}
		private set
		{
			assetName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string MiniMapName
	{
		get
		{
			return miniMapName_;
		}
		private set
		{
			miniMapName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float CharacterHeight
	{
		get
		{
			return characterHeight_;
		}
		private set
		{
			characterHeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float SummonHeight
	{
		get
		{
			return summonHeight_;
		}
		private set
		{
			summonHeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Bgm
	{
		get
		{
			return bgm_;
		}
		private set
		{
			bgm_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapSceneConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapSceneConfigure(MapSceneConfigure other)
		: this()
	{
		id_ = other.id_;
		assetName_ = other.assetName_;
		miniMapName_ = other.miniMapName_;
		characterHeight_ = other.characterHeight_;
		summonHeight_ = other.summonHeight_;
		bgm_ = other.bgm_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapSceneConfigure Clone()
	{
		return new MapSceneConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapSceneConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapSceneConfigure other)
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
		if (AssetName != other.AssetName)
		{
			return false;
		}
		if (MiniMapName != other.MiniMapName)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(CharacterHeight, other.CharacterHeight))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(SummonHeight, other.SummonHeight))
		{
			return false;
		}
		if (Bgm != other.Bgm)
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
		if (AssetName.Length != 0)
		{
			num ^= AssetName.GetHashCode();
		}
		if (MiniMapName.Length != 0)
		{
			num ^= MiniMapName.GetHashCode();
		}
		if (CharacterHeight != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(CharacterHeight);
		}
		if (SummonHeight != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(SummonHeight);
		}
		if (Bgm != 0)
		{
			num ^= Bgm.GetHashCode();
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
		if (AssetName.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(AssetName);
		}
		if (MiniMapName.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(MiniMapName);
		}
		if (CharacterHeight != 0f)
		{
			output.WriteRawTag(37);
			output.WriteFloat(CharacterHeight);
		}
		if (SummonHeight != 0f)
		{
			output.WriteRawTag(45);
			output.WriteFloat(SummonHeight);
		}
		if (Bgm != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Bgm);
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
		if (AssetName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AssetName);
		}
		if (MiniMapName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MiniMapName);
		}
		if (CharacterHeight != 0f)
		{
			num += 5;
		}
		if (SummonHeight != 0f)
		{
			num += 5;
		}
		if (Bgm != 0)
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
	public void MergeFrom(MapSceneConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.AssetName.Length != 0)
			{
				AssetName = other.AssetName;
			}
			if (other.MiniMapName.Length != 0)
			{
				MiniMapName = other.MiniMapName;
			}
			if (other.CharacterHeight != 0f)
			{
				CharacterHeight = other.CharacterHeight;
			}
			if (other.SummonHeight != 0f)
			{
				SummonHeight = other.SummonHeight;
			}
			if (other.Bgm != 0)
			{
				Bgm = other.Bgm;
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
				AssetName = input.ReadString();
				break;
			case 26u:
				MiniMapName = input.ReadString();
				break;
			case 37u:
				CharacterHeight = input.ReadFloat();
				break;
			case 45u:
				SummonHeight = input.ReadFloat();
				break;
			case 53u:
				Bgm = input.ReadSFixed32();
				break;
			}
		}
	}
}
