using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class ClientDataUploadC2S : IMessage<ClientDataUploadC2S>, IMessage, IEquatable<ClientDataUploadC2S>, IDeepCloneable<ClientDataUploadC2S>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum OpsData
		{
			[OriginalName("None")]
			None,
			[OriginalName("GuideData")]
			GuideData,
			[OriginalName("DateChangeData")]
			DateChangeData,
			[OriginalName("SongData")]
			SongData,
			[OriginalName("SettingData")]
			SettingData,
			[OriginalName("NewItemData")]
			NewItemData,
			[OriginalName("CampaignTutorialData")]
			CampaignTutorialData,
			[OriginalName("StarExpression")]
			StarExpression,
			[OriginalName("TopExpression")]
			TopExpression
		}
	}

	private static readonly MessageParser<ClientDataUploadC2S> _parser = new MessageParser<ClientDataUploadC2S>(() => new ClientDataUploadC2S());

	private UnknownFieldSet _unknownFields;

	public const int DataFieldNumber = 1;

	private ClientData data_;

	public const int OpsDataFieldNumber = 2;

	private Types.OpsData opsData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ClientDataUploadC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[61];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientData Data
	{
		get
		{
			return data_;
		}
		set
		{
			data_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.OpsData OpsData
	{
		get
		{
			return opsData_;
		}
		set
		{
			opsData_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientDataUploadC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientDataUploadC2S(ClientDataUploadC2S other)
		: this()
	{
		data_ = ((other.data_ != null) ? other.data_.Clone() : null);
		opsData_ = other.opsData_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientDataUploadC2S Clone()
	{
		return new ClientDataUploadC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ClientDataUploadC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ClientDataUploadC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Data, other.Data))
		{
			return false;
		}
		if (OpsData != other.OpsData)
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
		if (data_ != null)
		{
			num ^= Data.GetHashCode();
		}
		if (OpsData != Types.OpsData.None)
		{
			num ^= OpsData.GetHashCode();
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
		if (data_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Data);
		}
		if (OpsData != Types.OpsData.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)OpsData);
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
		if (data_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Data);
		}
		if (OpsData != Types.OpsData.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)OpsData);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ClientDataUploadC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.data_ != null)
		{
			if (data_ == null)
			{
				Data = new ClientData();
			}
			Data.MergeFrom(other.Data);
		}
		if (other.OpsData != Types.OpsData.None)
		{
			OpsData = other.OpsData;
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
				if (data_ == null)
				{
					Data = new ClientData();
				}
				input.ReadMessage(Data);
				break;
			case 16u:
				OpsData = (Types.OpsData)input.ReadEnum();
				break;
			}
		}
	}
}
