using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SelectRelicC2S : IMessage<SelectRelicC2S>, IMessage, IEquatable<SelectRelicC2S>, IDeepCloneable<SelectRelicC2S>, IBufferMessage
{
	private static readonly MessageParser<SelectRelicC2S> _parser = new MessageParser<SelectRelicC2S>(() => new SelectRelicC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int RelicsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_relics_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> relics_ = new RepeatedField<int>();

	public const int IdxFieldNumber = 3;

	private int idx_;

	public const int IsRerollFieldNumber = 4;

	private bool isReroll_;

	public const int LvFieldNumber = 5;

	private int lv_;

	public const int SupLvFieldNumber = 6;

	private int supLv_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SelectRelicC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[257];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Relics => relics_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Idx
	{
		get
		{
			return idx_;
		}
		set
		{
			idx_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsReroll
	{
		get
		{
			return isReroll_;
		}
		set
		{
			isReroll_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SupLv
	{
		get
		{
			return supLv_;
		}
		set
		{
			supLv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRelicC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRelicC2S(SelectRelicC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		relics_ = other.relics_.Clone();
		idx_ = other.idx_;
		isReroll_ = other.isReroll_;
		lv_ = other.lv_;
		supLv_ = other.supLv_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRelicC2S Clone()
	{
		return new SelectRelicC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SelectRelicC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SelectRelicC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (!relics_.Equals(other.relics_))
		{
			return false;
		}
		if (Idx != other.Idx)
		{
			return false;
		}
		if (IsReroll != other.IsReroll)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (SupLv != other.SupLv)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		num ^= relics_.GetHashCode();
		if (Idx != 0)
		{
			num ^= Idx.GetHashCode();
		}
		if (IsReroll)
		{
			num ^= IsReroll.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (SupLv != 0)
		{
			num ^= SupLv.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		relics_.WriteTo(ref output, _repeated_relics_codec);
		if (Idx != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Idx);
		}
		if (IsReroll)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsReroll);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Lv);
		}
		if (SupLv != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(SupLv);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		num += relics_.CalculateSize(_repeated_relics_codec);
		if (Idx != 0)
		{
			num += 5;
		}
		if (IsReroll)
		{
			num += 2;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (SupLv != 0)
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
	public void MergeFrom(SelectRelicC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		relics_.Add(other.relics_);
		if (other.Idx != 0)
		{
			Idx = other.Idx;
		}
		if (other.IsReroll)
		{
			IsReroll = other.IsReroll;
		}
		if (other.Lv != 0)
		{
			Lv = other.Lv;
		}
		if (other.SupLv != 0)
		{
			SupLv = other.SupLv;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 18u:
			case 21u:
				relics_.AddEntriesFrom(ref input, _repeated_relics_codec);
				break;
			case 29u:
				Idx = input.ReadSFixed32();
				break;
			case 32u:
				IsReroll = input.ReadBool();
				break;
			case 45u:
				Lv = input.ReadSFixed32();
				break;
			case 53u:
				SupLv = input.ReadSFixed32();
				break;
			}
		}
	}
}
