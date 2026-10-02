using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleBuildingData : IMessage<SingleBuildingData>, IMessage, IEquatable<SingleBuildingData>, IDeepCloneable<SingleBuildingData>, IBufferMessage
{
	private static readonly MessageParser<SingleBuildingData> _parser = new MessageParser<SingleBuildingData>(() => new SingleBuildingData());

	private UnknownFieldSet _unknownFields;

	public const int BuildingsFieldNumber = 1;

	private static readonly FieldCodec<SingleBuilding> _repeated_buildings_codec = FieldCodec.ForMessage(10u, SingleBuilding.Parser);

	private readonly RepeatedField<SingleBuilding> buildings_ = new RepeatedField<SingleBuilding>();

	public const int BuildingUpgradeCountFieldNumber = 2;

	private int buildingUpgradeCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleBuildingData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[108];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<SingleBuilding> Buildings => buildings_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuildingUpgradeCount
	{
		get
		{
			return buildingUpgradeCount_;
		}
		set
		{
			buildingUpgradeCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuildingData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuildingData(SingleBuildingData other)
		: this()
	{
		buildings_ = other.buildings_.Clone();
		buildingUpgradeCount_ = other.buildingUpgradeCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuildingData Clone()
	{
		return new SingleBuildingData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleBuildingData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleBuildingData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!buildings_.Equals(other.buildings_))
		{
			return false;
		}
		if (BuildingUpgradeCount != other.BuildingUpgradeCount)
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
		num ^= buildings_.GetHashCode();
		if (BuildingUpgradeCount != 0)
		{
			num ^= BuildingUpgradeCount.GetHashCode();
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
		buildings_.WriteTo(ref output, _repeated_buildings_codec);
		if (BuildingUpgradeCount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(BuildingUpgradeCount);
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
		num += buildings_.CalculateSize(_repeated_buildings_codec);
		if (BuildingUpgradeCount != 0)
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
	public void MergeFrom(SingleBuildingData other)
	{
		if (other != null)
		{
			buildings_.Add(other.buildings_);
			if (other.BuildingUpgradeCount != 0)
			{
				BuildingUpgradeCount = other.BuildingUpgradeCount;
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
			case 10u:
				buildings_.AddEntriesFrom(ref input, _repeated_buildings_codec);
				break;
			case 21u:
				BuildingUpgradeCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
