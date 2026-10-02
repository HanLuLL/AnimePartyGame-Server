using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixCharacterConfigure : IMessage<FixCharacterConfigure>, IMessage, IEquatable<FixCharacterConfigure>, IDeepCloneable<FixCharacterConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixCharacterConfigure> _parser = new MessageParser<FixCharacterConfigure>(() => new FixCharacterConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<FixCharacterInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, FixCharacterInfoConfigure.Parser);

	private readonly RepeatedField<FixCharacterInfoConfigure> infos_ = new RepeatedField<FixCharacterInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, FixCharacterInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, FixCharacterInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixCharacterInfoConfigure.Parser), 18u);

	private readonly MapField<int, FixCharacterInfoConfigure> infoDict_ = new MapField<int, FixCharacterInfoConfigure>();

	public const int ShieldSkillsFieldNumber = 3;

	private static readonly FieldCodec<FixCharacterShieldSkillConfigure> _repeated_shieldSkills_codec = FieldCodec.ForMessage(26u, FixCharacterShieldSkillConfigure.Parser);

	private readonly RepeatedField<FixCharacterShieldSkillConfigure> shieldSkills_ = new RepeatedField<FixCharacterShieldSkillConfigure>();

	public const int ShieldSkillDictFieldNumber = 4;

	private static readonly MapField<int, FixCharacterShieldSkillConfigure>.Codec _map_shieldSkillDict_codec = new MapField<int, FixCharacterShieldSkillConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixCharacterShieldSkillConfigure.Parser), 34u);

	private readonly MapField<int, FixCharacterShieldSkillConfigure> shieldSkillDict_ = new MapField<int, FixCharacterShieldSkillConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixCharacterConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixCharacterReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixCharacterInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixCharacterInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixCharacterShieldSkillConfigure> ShieldSkills => shieldSkills_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixCharacterShieldSkillConfigure> ShieldSkillDict => shieldSkillDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixCharacterConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixCharacterConfigure(FixCharacterConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		shieldSkills_ = other.shieldSkills_.Clone();
		shieldSkillDict_ = other.shieldSkillDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixCharacterConfigure Clone()
	{
		return new FixCharacterConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixCharacterConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixCharacterConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!shieldSkills_.Equals(other.shieldSkills_))
		{
			return false;
		}
		if (!ShieldSkillDict.Equals(other.ShieldSkillDict))
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
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= shieldSkills_.GetHashCode();
		num ^= ShieldSkillDict.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		shieldSkills_.WriteTo(ref output, _repeated_shieldSkills_codec);
		shieldSkillDict_.WriteTo(ref output, _map_shieldSkillDict_codec);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += shieldSkills_.CalculateSize(_repeated_shieldSkills_codec);
		num += shieldSkillDict_.CalculateSize(_map_shieldSkillDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixCharacterConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			shieldSkills_.Add(other.shieldSkills_);
			shieldSkillDict_.MergeFrom(other.shieldSkillDict_);
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
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				shieldSkills_.AddEntriesFrom(ref input, _repeated_shieldSkills_codec);
				break;
			case 34u:
				shieldSkillDict_.AddEntriesFrom(ref input, _map_shieldSkillDict_codec);
				break;
			}
		}
	}
}
