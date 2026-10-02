using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class WayInfoConfigure : IMessage<WayInfoConfigure>, IMessage, IEquatable<WayInfoConfigure>, IDeepCloneable<WayInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<WayInfoConfigure> _parser = new MessageParser<WayInfoConfigure>(() => new WayInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int WayTypeFieldNumber = 1;

	private WayType wayType_;

	public const int WayIDFieldNumber = 2;

	private int wayID_;

	public const int UiTypeFieldNumber = 3;

	private UIType uiType_;

	public const int PanelTypeFieldNumber = 4;

	private UIPanelType panelType_;

	public const int WindowTypeFieldNumber = 5;

	private UIWindowType windowType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<WayInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WayReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayType WayType
	{
		get
		{
			return wayType_;
		}
		private set
		{
			wayType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WayID
	{
		get
		{
			return wayID_;
		}
		private set
		{
			wayID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIType UiType
	{
		get
		{
			return uiType_;
		}
		private set
		{
			uiType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIPanelType PanelType
	{
		get
		{
			return panelType_;
		}
		private set
		{
			panelType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIWindowType WindowType
	{
		get
		{
			return windowType_;
		}
		private set
		{
			windowType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayInfoConfigure(WayInfoConfigure other)
		: this()
	{
		wayType_ = other.wayType_;
		wayID_ = other.wayID_;
		uiType_ = other.uiType_;
		panelType_ = other.panelType_;
		windowType_ = other.windowType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WayInfoConfigure Clone()
	{
		return new WayInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WayInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(WayInfoConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (WayType != other.WayType)
		{
			return false;
		}
		if (WayID != other.WayID)
		{
			return false;
		}
		if (UiType != other.UiType)
		{
			return false;
		}
		if (PanelType != other.PanelType)
		{
			return false;
		}
		if (WindowType != other.WindowType)
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
		if (WayType != WayType.None)
		{
			num ^= WayType.GetHashCode();
		}
		if (WayID != 0)
		{
			num ^= WayID.GetHashCode();
		}
		if (UiType != UIType.None)
		{
			num ^= UiType.GetHashCode();
		}
		if (PanelType != UIPanelType.None)
		{
			num ^= PanelType.GetHashCode();
		}
		if (WindowType != UIWindowType.None)
		{
			num ^= WindowType.GetHashCode();
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
		if (WayType != WayType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)WayType);
		}
		if (WayID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(WayID);
		}
		if (UiType != UIType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)UiType);
		}
		if (PanelType != UIPanelType.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)PanelType);
		}
		if (WindowType != UIWindowType.None)
		{
			output.WriteRawTag(40);
			output.WriteEnum((int)WindowType);
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
		if (WayType != WayType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)WayType);
		}
		if (WayID != 0)
		{
			num += 5;
		}
		if (UiType != UIType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)UiType);
		}
		if (PanelType != UIPanelType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PanelType);
		}
		if (WindowType != UIWindowType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)WindowType);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(WayInfoConfigure other)
	{
		if (other != null)
		{
			if (other.WayType != WayType.None)
			{
				WayType = other.WayType;
			}
			if (other.WayID != 0)
			{
				WayID = other.WayID;
			}
			if (other.UiType != UIType.None)
			{
				UiType = other.UiType;
			}
			if (other.PanelType != UIPanelType.None)
			{
				PanelType = other.PanelType;
			}
			if (other.WindowType != UIWindowType.None)
			{
				WindowType = other.WindowType;
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
				WayType = (WayType)input.ReadEnum();
				break;
			case 21u:
				WayID = input.ReadSFixed32();
				break;
			case 24u:
				UiType = (UIType)input.ReadEnum();
				break;
			case 32u:
				PanelType = (UIPanelType)input.ReadEnum();
				break;
			case 40u:
				WindowType = (UIWindowType)input.ReadEnum();
				break;
			}
		}
	}
}
