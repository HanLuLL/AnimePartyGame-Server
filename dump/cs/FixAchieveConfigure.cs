using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixAchieveConfigure : IMessage<FixAchieveConfigure>, IMessage, IEquatable<FixAchieveConfigure>, IDeepCloneable<FixAchieveConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixAchieveConfigure> _parser = new MessageParser<FixAchieveConfigure>(() => new FixAchieveConfigure());

	private UnknownFieldSet _unknownFields;

	public const int GlobalsFieldNumber = 1;

	private static readonly FieldCodec<FixAchieveGlobalConfigure> _repeated_globals_codec = FieldCodec.ForMessage(10u, FixAchieveGlobalConfigure.Parser);

	private readonly RepeatedField<FixAchieveGlobalConfigure> globals_ = new RepeatedField<FixAchieveGlobalConfigure>();

	public const int GlobalDictFieldNumber = 2;

	private static readonly MapField<int, FixAchieveGlobalConfigure>.Codec _map_globalDict_codec = new MapField<int, FixAchieveGlobalConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixAchieveGlobalConfigure.Parser), 18u);

	private readonly MapField<int, FixAchieveGlobalConfigure> globalDict_ = new MapField<int, FixAchieveGlobalConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixAchieveConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixAchieveReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixAchieveGlobalConfigure> Globals => globals_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixAchieveGlobalConfigure> GlobalDict => globalDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixAchieveConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixAchieveConfigure(FixAchieveConfigure other)
		: this()
	{
		globals_ = other.globals_.Clone();
		globalDict_ = other.globalDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixAchieveConfigure Clone()
	{
		return new FixAchieveConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixAchieveConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixAchieveConfigure other)
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= globals_.GetHashCode();
		num ^= GlobalDict.GetHashCode();
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixAchieveConfigure other)
	{
		if (other != null)
		{
			globals_.Add(other.globals_);
			globalDict_.MergeFrom(other.globalDict_);
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
			}
		}
	}
}
