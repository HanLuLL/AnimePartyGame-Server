using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class TriggerDivinationC2S : IMessage<TriggerDivinationC2S>, IMessage, IEquatable<TriggerDivinationC2S>, IDeepCloneable<TriggerDivinationC2S>, IBufferMessage
{
	private static readonly MessageParser<TriggerDivinationC2S> _parser = new MessageParser<TriggerDivinationC2S>(() => new TriggerDivinationC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int CanChoiceIdsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_canChoiceIds_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> canChoiceIds_ = new RepeatedField<int>();

	public const int IdFieldNumber = 3;

	private int id_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TriggerDivinationC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[333];

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
	public RepeatedField<int> CanChoiceIds => canChoiceIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationC2S(TriggerDivinationC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		canChoiceIds_ = other.canChoiceIds_.Clone();
		id_ = other.id_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationC2S Clone()
	{
		return new TriggerDivinationC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TriggerDivinationC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TriggerDivinationC2S other)
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
		if (!canChoiceIds_.Equals(other.canChoiceIds_))
		{
			return false;
		}
		if (Id != other.Id)
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
		num ^= canChoiceIds_.GetHashCode();
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
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
		canChoiceIds_.WriteTo(ref output, _repeated_canChoiceIds_codec);
		if (Id != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Id);
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
		num += canChoiceIds_.CalculateSize(_repeated_canChoiceIds_codec);
		if (Id != 0)
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
	public void MergeFrom(TriggerDivinationC2S other)
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
		canChoiceIds_.Add(other.canChoiceIds_);
		if (other.Id != 0)
		{
			Id = other.Id;
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
				canChoiceIds_.AddEntriesFrom(ref input, _repeated_canChoiceIds_codec);
				break;
			case 29u:
				Id = input.ReadSFixed32();
				break;
			}
		}
	}
}
