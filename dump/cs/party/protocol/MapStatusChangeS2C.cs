using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class MapStatusChangeS2C : IMessage<MapStatusChangeS2C>, IMessage, IEquatable<MapStatusChangeS2C>, IDeepCloneable<MapStatusChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<MapStatusChangeS2C> _parser = new MessageParser<MapStatusChangeS2C>(() => new MapStatusChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int MapIdFieldNumber = 1;

	private int mapId_;

	public const int GroupIdFieldNumber = 2;

	private int groupId_;

	public const int OldStatusFieldNumber = 3;

	private int oldStatus_;

	public const int NewStatusFieldNumber = 4;

	private int newStatus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapStatusChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[463];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapId
	{
		get
		{
			return mapId_;
		}
		set
		{
			mapId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GroupId
	{
		get
		{
			return groupId_;
		}
		set
		{
			groupId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OldStatus
	{
		get
		{
			return oldStatus_;
		}
		set
		{
			oldStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NewStatus
	{
		get
		{
			return newStatus_;
		}
		set
		{
			newStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapStatusChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapStatusChangeS2C(MapStatusChangeS2C other)
		: this()
	{
		mapId_ = other.mapId_;
		groupId_ = other.groupId_;
		oldStatus_ = other.oldStatus_;
		newStatus_ = other.newStatus_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapStatusChangeS2C Clone()
	{
		return new MapStatusChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapStatusChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapStatusChangeS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MapId != other.MapId)
		{
			return false;
		}
		if (GroupId != other.GroupId)
		{
			return false;
		}
		if (OldStatus != other.OldStatus)
		{
			return false;
		}
		if (NewStatus != other.NewStatus)
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
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (GroupId != 0)
		{
			num ^= GroupId.GetHashCode();
		}
		if (OldStatus != 0)
		{
			num ^= OldStatus.GetHashCode();
		}
		if (NewStatus != 0)
		{
			num ^= NewStatus.GetHashCode();
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
		if (MapId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(MapId);
		}
		if (GroupId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(GroupId);
		}
		if (OldStatus != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OldStatus);
		}
		if (NewStatus != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(NewStatus);
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
		if (MapId != 0)
		{
			num += 5;
		}
		if (GroupId != 0)
		{
			num += 5;
		}
		if (OldStatus != 0)
		{
			num += 5;
		}
		if (NewStatus != 0)
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
	public void MergeFrom(MapStatusChangeS2C other)
	{
		if (other != null)
		{
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.GroupId != 0)
			{
				GroupId = other.GroupId;
			}
			if (other.OldStatus != 0)
			{
				OldStatus = other.OldStatus;
			}
			if (other.NewStatus != 0)
			{
				NewStatus = other.NewStatus;
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
			case 13u:
				MapId = input.ReadSFixed32();
				break;
			case 21u:
				GroupId = input.ReadSFixed32();
				break;
			case 29u:
				OldStatus = input.ReadSFixed32();
				break;
			case 37u:
				NewStatus = input.ReadSFixed32();
				break;
			}
		}
	}
}
