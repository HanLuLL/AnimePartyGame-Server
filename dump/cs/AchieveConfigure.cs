using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class AchieveConfigure : IMessage<AchieveConfigure>, IMessage, IEquatable<AchieveConfigure>, IDeepCloneable<AchieveConfigure>, IBufferMessage
{
	private static readonly MessageParser<AchieveConfigure> _parser = new MessageParser<AchieveConfigure>(() => new AchieveConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<AchieveInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, AchieveInfoConfigure.Parser);

	private readonly RepeatedField<AchieveInfoConfigure> infos_ = new RepeatedField<AchieveInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, AchieveInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, AchieveInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AchieveInfoConfigure.Parser), 18u);

	private readonly MapField<int, AchieveInfoConfigure> infoDict_ = new MapField<int, AchieveInfoConfigure>();

	public const int GlobalsFieldNumber = 3;

	private static readonly FieldCodec<AchieveGlobalConfigure> _repeated_globals_codec = FieldCodec.ForMessage(26u, AchieveGlobalConfigure.Parser);

	private readonly RepeatedField<AchieveGlobalConfigure> globals_ = new RepeatedField<AchieveGlobalConfigure>();

	public const int GlobalDictFieldNumber = 4;

	private static readonly MapField<int, AchieveGlobalConfigure>.Codec _map_globalDict_codec = new MapField<int, AchieveGlobalConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AchieveGlobalConfigure.Parser), 34u);

	private readonly MapField<int, AchieveGlobalConfigure> globalDict_ = new MapField<int, AchieveGlobalConfigure>();

	public const int ShieldsFieldNumber = 5;

	private static readonly FieldCodec<AchieveShieldConfigure> _repeated_shields_codec = FieldCodec.ForMessage(42u, AchieveShieldConfigure.Parser);

	private readonly RepeatedField<AchieveShieldConfigure> shields_ = new RepeatedField<AchieveShieldConfigure>();

	public const int ShieldDictFieldNumber = 6;

	private static readonly MapField<int, AchieveShieldConfigure>.Codec _map_shieldDict_codec = new MapField<int, AchieveShieldConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, AchieveShieldConfigure.Parser), 50u);

	private readonly MapField<int, AchieveShieldConfigure> shieldDict_ = new MapField<int, AchieveShieldConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AchieveConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AchieveReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AchieveInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AchieveInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AchieveGlobalConfigure> Globals => globals_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AchieveGlobalConfigure> GlobalDict => globalDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<AchieveShieldConfigure> Shields => shields_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, AchieveShieldConfigure> ShieldDict => shieldDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AchieveConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AchieveConfigure(AchieveConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		globals_ = other.globals_.Clone();
		globalDict_ = other.globalDict_.Clone();
		shields_ = other.shields_.Clone();
		shieldDict_ = other.shieldDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AchieveConfigure Clone()
	{
		return new AchieveConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AchieveConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AchieveConfigure other)
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
		if (!globals_.Equals(other.globals_))
		{
			return false;
		}
		if (!GlobalDict.Equals(other.GlobalDict))
		{
			return false;
		}
		if (!shields_.Equals(other.shields_))
		{
			return false;
		}
		if (!ShieldDict.Equals(other.ShieldDict))
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
		num ^= globals_.GetHashCode();
		num ^= GlobalDict.GetHashCode();
		num ^= shields_.GetHashCode();
		num ^= ShieldDict.GetHashCode();
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
		globals_.WriteTo(ref output, _repeated_globals_codec);
		globalDict_.WriteTo(ref output, _map_globalDict_codec);
		shields_.WriteTo(ref output, _repeated_shields_codec);
		shieldDict_.WriteTo(ref output, _map_shieldDict_codec);
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
		num += globals_.CalculateSize(_repeated_globals_codec);
		num += globalDict_.CalculateSize(_map_globalDict_codec);
		num += shields_.CalculateSize(_repeated_shields_codec);
		num += shieldDict_.CalculateSize(_map_shieldDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(AchieveConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			globals_.Add(other.globals_);
			globalDict_.MergeFrom(other.globalDict_);
			shields_.Add(other.shields_);
			shieldDict_.MergeFrom(other.shieldDict_);
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
				globals_.AddEntriesFrom(ref input, _repeated_globals_codec);
				break;
			case 34u:
				globalDict_.AddEntriesFrom(ref input, _map_globalDict_codec);
				break;
			case 42u:
				shields_.AddEntriesFrom(ref input, _repeated_shields_codec);
				break;
			case 50u:
				shieldDict_.AddEntriesFrom(ref input, _map_shieldDict_codec);
				break;
			}
		}
	}

	public void Fix(FixAchieveConfigure FixAchieveConfig)
	{
		if (FixAchieveConfig?.Globals == null || FixAchieveConfig.Globals.Count == 0)
		{
			return;
		}
		MapField<int, FixAchieveGlobalConfigure> globalDict = FixAchieveConfig.GlobalDict;
		for (int num = globals_.Count - 1; num >= 0; num--)
		{
			if (globalDict.ContainsKey(globals_[num].Id))
			{
				globalDict_.Remove(globals_[num].Id);
				globals_.RemoveAt(num);
			}
		}
	}
}
