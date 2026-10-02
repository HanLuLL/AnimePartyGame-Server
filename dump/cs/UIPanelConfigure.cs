using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class UIPanelConfigure : IMessage<UIPanelConfigure>, IMessage, IEquatable<UIPanelConfigure>, IDeepCloneable<UIPanelConfigure>, IBufferMessage
{
	private static readonly MessageParser<UIPanelConfigure> _parser = new MessageParser<UIPanelConfigure>(() => new UIPanelConfigure());

	private UnknownFieldSet _unknownFields;

	public const int PanelTypeFieldNumber = 1;

	private UIPanelType panelType_;

	public const int IdFieldNumber = 2;

	private int id_;

	public const int PackageNameFieldNumber = 3;

	private string packageName_ = "";

	public const int IsRootInSceneFieldNumber = 4;

	private bool isRootInScene_;

	public const int NeedBackgroundUIFieldNumber = 5;

	private bool needBackgroundUI_;

	public const int NeedBottomMenuFieldNumber = 6;

	private bool needBottomMenu_;

	public const int CloseActivityHubFieldNumber = 7;

	private bool closeActivityHub_;

	public const int NeedBottomPlayerLabelFieldNumber = 8;

	private bool needBottomPlayerLabel_;

	public const int ShowFunctionFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_showFunction_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> showFunction_ = new RepeatedField<int>();

	public const int LayerFieldNumber = 10;

	private int layer_;

	public const int NeedClosePreviousFieldNumber = 11;

	private bool needClosePrevious_;

	public const int BGMConfigIDFieldNumber = 12;

	private int bGMConfigID_;

	public const int ShowCurrenciesFieldNumber = 13;

	private static readonly MapField<int, UIPanelConfigureShowCurrenciess>.Codec _map_showCurrencies_codec = new MapField<int, UIPanelConfigureShowCurrenciess>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, UIPanelConfigureShowCurrenciess.Parser), 106u);

	private readonly MapField<int, UIPanelConfigureShowCurrenciess> showCurrencies_ = new MapField<int, UIPanelConfigureShowCurrenciess>();

	public const int MoneyLineUpFieldNumber = 14;

	private int moneyLineUp_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UIPanelConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UIReflection.Descriptor.MessageTypes[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string PackageName
	{
		get
		{
			return packageName_;
		}
		private set
		{
			packageName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRootInScene
	{
		get
		{
			return isRootInScene_;
		}
		private set
		{
			isRootInScene_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NeedBackgroundUI
	{
		get
		{
			return needBackgroundUI_;
		}
		private set
		{
			needBackgroundUI_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NeedBottomMenu
	{
		get
		{
			return needBottomMenu_;
		}
		private set
		{
			needBottomMenu_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CloseActivityHub
	{
		get
		{
			return closeActivityHub_;
		}
		private set
		{
			closeActivityHub_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NeedBottomPlayerLabel
	{
		get
		{
			return needBottomPlayerLabel_;
		}
		private set
		{
			needBottomPlayerLabel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ShowFunction => showFunction_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Layer
	{
		get
		{
			return layer_;
		}
		private set
		{
			layer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NeedClosePrevious
	{
		get
		{
			return needClosePrevious_;
		}
		private set
		{
			needClosePrevious_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BGMConfigID
	{
		get
		{
			return bGMConfigID_;
		}
		private set
		{
			bGMConfigID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, UIPanelConfigureShowCurrenciess> ShowCurrencies => showCurrencies_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MoneyLineUp
	{
		get
		{
			return moneyLineUp_;
		}
		private set
		{
			moneyLineUp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIPanelConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIPanelConfigure(UIPanelConfigure other)
		: this()
	{
		panelType_ = other.panelType_;
		id_ = other.id_;
		packageName_ = other.packageName_;
		isRootInScene_ = other.isRootInScene_;
		needBackgroundUI_ = other.needBackgroundUI_;
		needBottomMenu_ = other.needBottomMenu_;
		closeActivityHub_ = other.closeActivityHub_;
		needBottomPlayerLabel_ = other.needBottomPlayerLabel_;
		showFunction_ = other.showFunction_.Clone();
		layer_ = other.layer_;
		needClosePrevious_ = other.needClosePrevious_;
		bGMConfigID_ = other.bGMConfigID_;
		showCurrencies_ = other.showCurrencies_.Clone();
		moneyLineUp_ = other.moneyLineUp_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIPanelConfigure Clone()
	{
		return new UIPanelConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UIPanelConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UIPanelConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PanelType != other.PanelType)
		{
			return false;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (PackageName != other.PackageName)
		{
			return false;
		}
		if (IsRootInScene != other.IsRootInScene)
		{
			return false;
		}
		if (NeedBackgroundUI != other.NeedBackgroundUI)
		{
			return false;
		}
		if (NeedBottomMenu != other.NeedBottomMenu)
		{
			return false;
		}
		if (CloseActivityHub != other.CloseActivityHub)
		{
			return false;
		}
		if (NeedBottomPlayerLabel != other.NeedBottomPlayerLabel)
		{
			return false;
		}
		if (!showFunction_.Equals(other.showFunction_))
		{
			return false;
		}
		if (Layer != other.Layer)
		{
			return false;
		}
		if (NeedClosePrevious != other.NeedClosePrevious)
		{
			return false;
		}
		if (BGMConfigID != other.BGMConfigID)
		{
			return false;
		}
		if (!ShowCurrencies.Equals(other.ShowCurrencies))
		{
			return false;
		}
		if (MoneyLineUp != other.MoneyLineUp)
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
		if (PanelType != UIPanelType.None)
		{
			num ^= PanelType.GetHashCode();
		}
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (PackageName.Length != 0)
		{
			num ^= PackageName.GetHashCode();
		}
		if (IsRootInScene)
		{
			num ^= IsRootInScene.GetHashCode();
		}
		if (NeedBackgroundUI)
		{
			num ^= NeedBackgroundUI.GetHashCode();
		}
		if (NeedBottomMenu)
		{
			num ^= NeedBottomMenu.GetHashCode();
		}
		if (CloseActivityHub)
		{
			num ^= CloseActivityHub.GetHashCode();
		}
		if (NeedBottomPlayerLabel)
		{
			num ^= NeedBottomPlayerLabel.GetHashCode();
		}
		num ^= showFunction_.GetHashCode();
		if (Layer != 0)
		{
			num ^= Layer.GetHashCode();
		}
		if (NeedClosePrevious)
		{
			num ^= NeedClosePrevious.GetHashCode();
		}
		if (BGMConfigID != 0)
		{
			num ^= BGMConfigID.GetHashCode();
		}
		num ^= ShowCurrencies.GetHashCode();
		if (MoneyLineUp != 0)
		{
			num ^= MoneyLineUp.GetHashCode();
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
		if (PanelType != UIPanelType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)PanelType);
		}
		if (Id != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Id);
		}
		if (PackageName.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(PackageName);
		}
		if (IsRootInScene)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsRootInScene);
		}
		if (NeedBackgroundUI)
		{
			output.WriteRawTag(40);
			output.WriteBool(NeedBackgroundUI);
		}
		if (NeedBottomMenu)
		{
			output.WriteRawTag(48);
			output.WriteBool(NeedBottomMenu);
		}
		if (CloseActivityHub)
		{
			output.WriteRawTag(56);
			output.WriteBool(CloseActivityHub);
		}
		if (NeedBottomPlayerLabel)
		{
			output.WriteRawTag(64);
			output.WriteBool(NeedBottomPlayerLabel);
		}
		showFunction_.WriteTo(ref output, _repeated_showFunction_codec);
		if (Layer != 0)
		{
			output.WriteRawTag(80);
			output.WriteSInt32(Layer);
		}
		if (NeedClosePrevious)
		{
			output.WriteRawTag(88);
			output.WriteBool(NeedClosePrevious);
		}
		if (BGMConfigID != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(BGMConfigID);
		}
		showCurrencies_.WriteTo(ref output, _map_showCurrencies_codec);
		if (MoneyLineUp != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(MoneyLineUp);
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
		if (PanelType != UIPanelType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PanelType);
		}
		if (Id != 0)
		{
			num += 5;
		}
		if (PackageName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PackageName);
		}
		if (IsRootInScene)
		{
			num += 2;
		}
		if (NeedBackgroundUI)
		{
			num += 2;
		}
		if (NeedBottomMenu)
		{
			num += 2;
		}
		if (CloseActivityHub)
		{
			num += 2;
		}
		if (NeedBottomPlayerLabel)
		{
			num += 2;
		}
		num += showFunction_.CalculateSize(_repeated_showFunction_codec);
		if (Layer != 0)
		{
			num += 1 + CodedOutputStream.ComputeSInt32Size(Layer);
		}
		if (NeedClosePrevious)
		{
			num += 2;
		}
		if (BGMConfigID != 0)
		{
			num += 5;
		}
		num += showCurrencies_.CalculateSize(_map_showCurrencies_codec);
		if (MoneyLineUp != 0)
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
	public void MergeFrom(UIPanelConfigure other)
	{
		if (other != null)
		{
			if (other.PanelType != UIPanelType.None)
			{
				PanelType = other.PanelType;
			}
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PackageName.Length != 0)
			{
				PackageName = other.PackageName;
			}
			if (other.IsRootInScene)
			{
				IsRootInScene = other.IsRootInScene;
			}
			if (other.NeedBackgroundUI)
			{
				NeedBackgroundUI = other.NeedBackgroundUI;
			}
			if (other.NeedBottomMenu)
			{
				NeedBottomMenu = other.NeedBottomMenu;
			}
			if (other.CloseActivityHub)
			{
				CloseActivityHub = other.CloseActivityHub;
			}
			if (other.NeedBottomPlayerLabel)
			{
				NeedBottomPlayerLabel = other.NeedBottomPlayerLabel;
			}
			showFunction_.Add(other.showFunction_);
			if (other.Layer != 0)
			{
				Layer = other.Layer;
			}
			if (other.NeedClosePrevious)
			{
				NeedClosePrevious = other.NeedClosePrevious;
			}
			if (other.BGMConfigID != 0)
			{
				BGMConfigID = other.BGMConfigID;
			}
			showCurrencies_.MergeFrom(other.showCurrencies_);
			if (other.MoneyLineUp != 0)
			{
				MoneyLineUp = other.MoneyLineUp;
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
				PanelType = (UIPanelType)input.ReadEnum();
				break;
			case 21u:
				Id = input.ReadSFixed32();
				break;
			case 26u:
				PackageName = input.ReadString();
				break;
			case 32u:
				IsRootInScene = input.ReadBool();
				break;
			case 40u:
				NeedBackgroundUI = input.ReadBool();
				break;
			case 48u:
				NeedBottomMenu = input.ReadBool();
				break;
			case 56u:
				CloseActivityHub = input.ReadBool();
				break;
			case 64u:
				NeedBottomPlayerLabel = input.ReadBool();
				break;
			case 74u:
			case 77u:
				showFunction_.AddEntriesFrom(ref input, _repeated_showFunction_codec);
				break;
			case 80u:
				Layer = input.ReadSInt32();
				break;
			case 88u:
				NeedClosePrevious = input.ReadBool();
				break;
			case 101u:
				BGMConfigID = input.ReadSFixed32();
				break;
			case 106u:
				showCurrencies_.AddEntriesFrom(ref input, _map_showCurrencies_codec);
				break;
			case 117u:
				MoneyLineUp = input.ReadSFixed32();
				break;
			}
		}
	}
}
