using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattlePassRewardAdsConfigure : IMessage<BattlePassRewardAdsConfigure>, IMessage, IEquatable<BattlePassRewardAdsConfigure>, IDeepCloneable<BattlePassRewardAdsConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattlePassRewardAdsConfigure> _parser = new MessageParser<BattlePassRewardAdsConfigure>(() => new BattlePassRewardAdsConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BattlePassRewardAdsConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<BattlePassRewardAdsConfigureItem> _repeated_battlePassRewardAdsConfigureItems_codec = FieldCodec.ForMessage(18u, BattlePassRewardAdsConfigureItem.Parser);

	private readonly RepeatedField<BattlePassRewardAdsConfigureItem> battlePassRewardAdsConfigureItems_ = new RepeatedField<BattlePassRewardAdsConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassRewardAdsConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[6];

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
	public RepeatedField<BattlePassRewardAdsConfigureItem> BattlePassRewardAdsConfigureItems => battlePassRewardAdsConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardAdsConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardAdsConfigure(BattlePassRewardAdsConfigure other)
		: this()
	{
		id_ = other.id_;
		battlePassRewardAdsConfigureItems_ = other.battlePassRewardAdsConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassRewardAdsConfigure Clone()
	{
		return new BattlePassRewardAdsConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassRewardAdsConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassRewardAdsConfigure other)
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
		if (!battlePassRewardAdsConfigureItems_.Equals(other.battlePassRewardAdsConfigureItems_))
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
		num ^= battlePassRewardAdsConfigureItems_.GetHashCode();
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
		battlePassRewardAdsConfigureItems_.WriteTo(ref output, _repeated_battlePassRewardAdsConfigureItems_codec);
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
		num += battlePassRewardAdsConfigureItems_.CalculateSize(_repeated_battlePassRewardAdsConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassRewardAdsConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			battlePassRewardAdsConfigureItems_.Add(other.battlePassRewardAdsConfigureItems_);
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
				battlePassRewardAdsConfigureItems_.AddEntriesFrom(ref input, _repeated_battlePassRewardAdsConfigureItems_codec);
				break;
			}
		}
	}
}
