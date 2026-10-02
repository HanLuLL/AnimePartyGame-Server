using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SignInConfigure : IMessage<SignInConfigure>, IMessage, IEquatable<SignInConfigure>, IDeepCloneable<SignInConfigure>, IBufferMessage
{
	private static readonly MessageParser<SignInConfigure> _parser = new MessageParser<SignInConfigure>(() => new SignInConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<SignInInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, SignInInfoConfigure.Parser);

	private readonly RepeatedField<SignInInfoConfigure> infos_ = new RepeatedField<SignInInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, SignInInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, SignInInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SignInInfoConfigure.Parser), 18u);

	private readonly MapField<int, SignInInfoConfigure> infoDict_ = new MapField<int, SignInInfoConfigure>();

	public const int DatasFieldNumber = 3;

	private static readonly FieldCodec<SignInDataConfigure> _repeated_datas_codec = FieldCodec.ForMessage(26u, SignInDataConfigure.Parser);

	private readonly RepeatedField<SignInDataConfigure> datas_ = new RepeatedField<SignInDataConfigure>();

	public const int DataDictFieldNumber = 4;

	private static readonly MapField<int, SignInDataConfigure>.Codec _map_dataDict_codec = new MapField<int, SignInDataConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, SignInDataConfigure.Parser), 34u);

	private readonly MapField<int, SignInDataConfigure> dataDict_ = new MapField<int, SignInDataConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SignInConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SignInReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SignInInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SignInInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SignInDataConfigure> Datas => datas_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SignInDataConfigure> DataDict => dataDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInConfigure(SignInConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		datas_ = other.datas_.Clone();
		dataDict_ = other.dataDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInConfigure Clone()
	{
		return new SignInConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SignInConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SignInConfigure other)
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
		if (!datas_.Equals(other.datas_))
		{
			return false;
		}
		if (!DataDict.Equals(other.DataDict))
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
		num ^= datas_.GetHashCode();
		num ^= DataDict.GetHashCode();
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
		datas_.WriteTo(ref output, _repeated_datas_codec);
		dataDict_.WriteTo(ref output, _map_dataDict_codec);
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
		num += datas_.CalculateSize(_repeated_datas_codec);
		num += dataDict_.CalculateSize(_map_dataDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SignInConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			datas_.Add(other.datas_);
			dataDict_.MergeFrom(other.dataDict_);
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
				datas_.AddEntriesFrom(ref input, _repeated_datas_codec);
				break;
			case 34u:
				dataDict_.AddEntriesFrom(ref input, _map_dataDict_codec);
				break;
			}
		}
	}

	public void Fix(FixSignInConfigure FixSignIn)
	{
		if (FixSignIn == null)
		{
			return;
		}
		MapField<int, FixSignInInfoConfigure> infoDict = FixSignIn.InfoDict;
		if (infoDict == null || infoDict.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < infos_.Count; i++)
		{
			if (infoDict.TryGetValue(infos_[i].Id, out var value))
			{
				infos_[i].FixTime(value);
				infoDict_[infos_[i].Id].FixTime(value);
			}
		}
	}
}
