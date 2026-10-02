using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class UIConfigure : IMessage<UIConfigure>, IMessage, IEquatable<UIConfigure>, IDeepCloneable<UIConfigure>, IBufferMessage
{
	private static readonly MessageParser<UIConfigure> _parser = new MessageParser<UIConfigure>(() => new UIConfigure());

	private UnknownFieldSet _unknownFields;

	public const int PanelsFieldNumber = 1;

	private static readonly FieldCodec<UIPanelConfigure> _repeated_panels_codec = FieldCodec.ForMessage(10u, UIPanelConfigure.Parser);

	private readonly RepeatedField<UIPanelConfigure> panels_ = new RepeatedField<UIPanelConfigure>();

	public const int PanelDictFieldNumber = 2;

	private static readonly MapField<int, UIPanelConfigure>.Codec _map_panelDict_codec = new MapField<int, UIPanelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, UIPanelConfigure.Parser), 18u);

	private readonly MapField<int, UIPanelConfigure> panelDict_ = new MapField<int, UIPanelConfigure>();

	public const int WindowsFieldNumber = 3;

	private static readonly FieldCodec<UIWindowConfigure> _repeated_windows_codec = FieldCodec.ForMessage(26u, UIWindowConfigure.Parser);

	private readonly RepeatedField<UIWindowConfigure> windows_ = new RepeatedField<UIWindowConfigure>();

	public const int WindowDictFieldNumber = 4;

	private static readonly MapField<int, UIWindowConfigure>.Codec _map_windowDict_codec = new MapField<int, UIWindowConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, UIWindowConfigure.Parser), 34u);

	private readonly MapField<int, UIWindowConfigure> windowDict_ = new MapField<int, UIWindowConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UIConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UIReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<UIPanelConfigure> Panels => panels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, UIPanelConfigure> PanelDict => panelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<UIWindowConfigure> Windows => windows_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, UIWindowConfigure> WindowDict => windowDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIConfigure(UIConfigure other)
		: this()
	{
		panels_ = other.panels_.Clone();
		panelDict_ = other.panelDict_.Clone();
		windows_ = other.windows_.Clone();
		windowDict_ = other.windowDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIConfigure Clone()
	{
		return new UIConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UIConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UIConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!panels_.Equals(other.panels_))
		{
			return false;
		}
		if (!PanelDict.Equals(other.PanelDict))
		{
			return false;
		}
		if (!windows_.Equals(other.windows_))
		{
			return false;
		}
		if (!WindowDict.Equals(other.WindowDict))
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
		num ^= panels_.GetHashCode();
		num ^= PanelDict.GetHashCode();
		num ^= windows_.GetHashCode();
		num ^= WindowDict.GetHashCode();
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
		panels_.WriteTo(ref output, _repeated_panels_codec);
		panelDict_.WriteTo(ref output, _map_panelDict_codec);
		windows_.WriteTo(ref output, _repeated_windows_codec);
		windowDict_.WriteTo(ref output, _map_windowDict_codec);
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
		num += panels_.CalculateSize(_repeated_panels_codec);
		num += panelDict_.CalculateSize(_map_panelDict_codec);
		num += windows_.CalculateSize(_repeated_windows_codec);
		num += windowDict_.CalculateSize(_map_windowDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(UIConfigure other)
	{
		if (other != null)
		{
			panels_.Add(other.panels_);
			panelDict_.MergeFrom(other.panelDict_);
			windows_.Add(other.windows_);
			windowDict_.MergeFrom(other.windowDict_);
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
				panels_.AddEntriesFrom(ref input, _repeated_panels_codec);
				break;
			case 18u:
				panelDict_.AddEntriesFrom(ref input, _map_panelDict_codec);
				break;
			case 26u:
				windows_.AddEntriesFrom(ref input, _repeated_windows_codec);
				break;
			case 34u:
				windowDict_.AddEntriesFrom(ref input, _map_windowDict_codec);
				break;
			}
		}
	}
}
