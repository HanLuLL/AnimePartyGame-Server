using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class TutorialConfigure : IMessage<TutorialConfigure>, IMessage, IEquatable<TutorialConfigure>, IDeepCloneable<TutorialConfigure>, IBufferMessage
{
	private static readonly MessageParser<TutorialConfigure> _parser = new MessageParser<TutorialConfigure>(() => new TutorialConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<TutorialinfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, TutorialinfoConfigure.Parser);

	private readonly RepeatedField<TutorialinfoConfigure> infos_ = new RepeatedField<TutorialinfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, TutorialinfoConfigure>.Codec _map_infoDict_codec = new MapField<int, TutorialinfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TutorialinfoConfigure.Parser), 18u);

	private readonly MapField<int, TutorialinfoConfigure> infoDict_ = new MapField<int, TutorialinfoConfigure>();

	public const int DialogsFieldNumber = 3;

	private static readonly FieldCodec<TutorialdialogConfigure> _repeated_dialogs_codec = FieldCodec.ForMessage(26u, TutorialdialogConfigure.Parser);

	private readonly RepeatedField<TutorialdialogConfigure> dialogs_ = new RepeatedField<TutorialdialogConfigure>();

	public const int DialogDictFieldNumber = 4;

	private static readonly MapField<int, TutorialdialogConfigure>.Codec _map_dialogDict_codec = new MapField<int, TutorialdialogConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TutorialdialogConfigure.Parser), 34u);

	private readonly MapField<int, TutorialdialogConfigure> dialogDict_ = new MapField<int, TutorialdialogConfigure>();

	public const int PopupsFieldNumber = 5;

	private static readonly FieldCodec<TutorialpopupConfigure> _repeated_popups_codec = FieldCodec.ForMessage(42u, TutorialpopupConfigure.Parser);

	private readonly RepeatedField<TutorialpopupConfigure> popups_ = new RepeatedField<TutorialpopupConfigure>();

	public const int PopupDictFieldNumber = 6;

	private static readonly MapField<int, TutorialpopupConfigure>.Codec _map_popupDict_codec = new MapField<int, TutorialpopupConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TutorialpopupConfigure.Parser), 50u);

	private readonly MapField<int, TutorialpopupConfigure> popupDict_ = new MapField<int, TutorialpopupConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TutorialConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TutorialReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TutorialinfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TutorialinfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TutorialdialogConfigure> Dialogs => dialogs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TutorialdialogConfigure> DialogDict => dialogDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TutorialpopupConfigure> Popups => popups_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TutorialpopupConfigure> PopupDict => popupDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialConfigure(TutorialConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		dialogs_ = other.dialogs_.Clone();
		dialogDict_ = other.dialogDict_.Clone();
		popups_ = other.popups_.Clone();
		popupDict_ = other.popupDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialConfigure Clone()
	{
		return new TutorialConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TutorialConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TutorialConfigure other)
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
		if (!dialogs_.Equals(other.dialogs_))
		{
			return false;
		}
		if (!DialogDict.Equals(other.DialogDict))
		{
			return false;
		}
		if (!popups_.Equals(other.popups_))
		{
			return false;
		}
		if (!PopupDict.Equals(other.PopupDict))
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
		num ^= dialogs_.GetHashCode();
		num ^= DialogDict.GetHashCode();
		num ^= popups_.GetHashCode();
		num ^= PopupDict.GetHashCode();
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
		dialogs_.WriteTo(ref output, _repeated_dialogs_codec);
		dialogDict_.WriteTo(ref output, _map_dialogDict_codec);
		popups_.WriteTo(ref output, _repeated_popups_codec);
		popupDict_.WriteTo(ref output, _map_popupDict_codec);
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
		num += dialogs_.CalculateSize(_repeated_dialogs_codec);
		num += dialogDict_.CalculateSize(_map_dialogDict_codec);
		num += popups_.CalculateSize(_repeated_popups_codec);
		num += popupDict_.CalculateSize(_map_popupDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TutorialConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			dialogs_.Add(other.dialogs_);
			dialogDict_.MergeFrom(other.dialogDict_);
			popups_.Add(other.popups_);
			popupDict_.MergeFrom(other.popupDict_);
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
				dialogs_.AddEntriesFrom(ref input, _repeated_dialogs_codec);
				break;
			case 34u:
				dialogDict_.AddEntriesFrom(ref input, _map_dialogDict_codec);
				break;
			case 42u:
				popups_.AddEntriesFrom(ref input, _repeated_popups_codec);
				break;
			case 50u:
				popupDict_.AddEntriesFrom(ref input, _map_popupDict_codec);
				break;
			}
		}
	}
}
