using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class CampaignNotifyS2C : IMessage<CampaignNotifyS2C>, IMessage, IEquatable<CampaignNotifyS2C>, IDeepCloneable<CampaignNotifyS2C>, IBufferMessage
{
	private static readonly MessageParser<CampaignNotifyS2C> _parser = new MessageParser<CampaignNotifyS2C>(() => new CampaignNotifyS2C());

	private UnknownFieldSet _unknownFields;

	public const int VictoryConditionFieldNumber = 1;

	private VictoryCondition victoryCondition_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CampaignNotifyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[397];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VictoryCondition VictoryCondition
	{
		get
		{
			return victoryCondition_;
		}
		set
		{
			victoryCondition_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignNotifyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignNotifyS2C(CampaignNotifyS2C other)
		: this()
	{
		victoryCondition_ = ((other.victoryCondition_ != null) ? other.victoryCondition_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignNotifyS2C Clone()
	{
		return new CampaignNotifyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CampaignNotifyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CampaignNotifyS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(VictoryCondition, other.VictoryCondition))
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
		if (victoryCondition_ != null)
		{
			num ^= VictoryCondition.GetHashCode();
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
		if (victoryCondition_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(VictoryCondition);
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
		if (victoryCondition_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(VictoryCondition);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CampaignNotifyS2C other)
	{
		if (other == null)
		{
			return;
		}
		if (other.victoryCondition_ != null)
		{
			if (victoryCondition_ == null)
			{
				VictoryCondition = new VictoryCondition();
			}
			VictoryCondition.MergeFrom(other.VictoryCondition);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				continue;
			}
			if (victoryCondition_ == null)
			{
				VictoryCondition = new VictoryCondition();
			}
			input.ReadMessage(VictoryCondition);
		}
	}
}
