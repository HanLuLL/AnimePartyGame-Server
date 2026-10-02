using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ActionGroup : IMessage<ActionGroup>, IMessage, IEquatable<ActionGroup>, IDeepCloneable<ActionGroup>, IBufferMessage
{
	private static readonly MessageParser<ActionGroup> _parser = new MessageParser<ActionGroup>(() => new ActionGroup());

	private UnknownFieldSet _unknownFields;

	public const int ActionIdFieldNumber = 1;

	private int actionId_;

	public const int AffirmNumFieldNumber = 2;

	private int affirmNum_;

	public const int AffirmIdsFieldNumber = 3;

	private static readonly MapField<long, bool>.Codec _map_affirmIds_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 26u);

	private readonly MapField<long, bool> affirmIds_ = new MapField<long, bool>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActionGroup> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[84];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActionId
	{
		get
		{
			return actionId_;
		}
		set
		{
			actionId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AffirmNum
	{
		get
		{
			return affirmNum_;
		}
		set
		{
			affirmNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> AffirmIds => affirmIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionGroup()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionGroup(ActionGroup other)
		: this()
	{
		actionId_ = other.actionId_;
		affirmNum_ = other.affirmNum_;
		affirmIds_ = other.affirmIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionGroup Clone()
	{
		return new ActionGroup(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActionGroup);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActionGroup other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ActionId != other.ActionId)
		{
			return false;
		}
		if (AffirmNum != other.AffirmNum)
		{
			return false;
		}
		if (!AffirmIds.Equals(other.AffirmIds))
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
		if (ActionId != 0)
		{
			num ^= ActionId.GetHashCode();
		}
		if (AffirmNum != 0)
		{
			num ^= AffirmNum.GetHashCode();
		}
		num ^= AffirmIds.GetHashCode();
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
		if (ActionId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ActionId);
		}
		if (AffirmNum != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(AffirmNum);
		}
		affirmIds_.WriteTo(ref output, _map_affirmIds_codec);
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
		if (ActionId != 0)
		{
			num += 5;
		}
		if (AffirmNum != 0)
		{
			num += 5;
		}
		num += affirmIds_.CalculateSize(_map_affirmIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ActionGroup other)
	{
		if (other != null)
		{
			if (other.ActionId != 0)
			{
				ActionId = other.ActionId;
			}
			if (other.AffirmNum != 0)
			{
				AffirmNum = other.AffirmNum;
			}
			affirmIds_.MergeFrom(other.affirmIds_);
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
				ActionId = input.ReadSFixed32();
				break;
			case 21u:
				AffirmNum = input.ReadSFixed32();
				break;
			case 26u:
				affirmIds_.AddEntriesFrom(ref input, _map_affirmIds_codec);
				break;
			}
		}
	}
}
