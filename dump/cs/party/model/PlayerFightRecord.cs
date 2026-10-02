using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class PlayerFightRecord : IMessage<PlayerFightRecord>, IMessage, IEquatable<PlayerFightRecord>, IDeepCloneable<PlayerFightRecord>, IBufferMessage
{
	private static readonly MessageParser<PlayerFightRecord> _parser = new MessageParser<PlayerFightRecord>(() => new PlayerFightRecord());

	private UnknownFieldSet _unknownFields;

	public const int TimeFieldNumber = 1;

	private long time_;

	public const int DataFieldNumber = 2;

	private static readonly FieldCodec<PlayerFightData> _repeated_data_codec = FieldCodec.ForMessage(18u, PlayerFightData.Parser);

	private readonly RepeatedField<PlayerFightData> data_ = new RepeatedField<PlayerFightData>();

	public const int ReplayIdFieldNumber = 3;

	private string replayId_ = "";

	public const int VersionFieldNumber = 4;

	private string version_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PlayerFightRecord> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[26];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Time
	{
		get
		{
			return time_;
		}
		set
		{
			time_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<PlayerFightData> Data => data_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ReplayId
	{
		get
		{
			return replayId_;
		}
		set
		{
			replayId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Version
	{
		get
		{
			return version_;
		}
		set
		{
			version_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFightRecord()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFightRecord(PlayerFightRecord other)
		: this()
	{
		time_ = other.time_;
		data_ = other.data_.Clone();
		replayId_ = other.replayId_;
		version_ = other.version_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerFightRecord Clone()
	{
		return new PlayerFightRecord(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PlayerFightRecord);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PlayerFightRecord other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Time != other.Time)
		{
			return false;
		}
		if (!data_.Equals(other.data_))
		{
			return false;
		}
		if (ReplayId != other.ReplayId)
		{
			return false;
		}
		if (Version != other.Version)
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
		if (Time != 0L)
		{
			num ^= Time.GetHashCode();
		}
		num ^= data_.GetHashCode();
		if (ReplayId.Length != 0)
		{
			num ^= ReplayId.GetHashCode();
		}
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
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
		if (Time != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(Time);
		}
		data_.WriteTo(ref output, _repeated_data_codec);
		if (ReplayId.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(ReplayId);
		}
		if (Version.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Version);
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
		if (Time != 0L)
		{
			num += 9;
		}
		num += data_.CalculateSize(_repeated_data_codec);
		if (ReplayId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ReplayId);
		}
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PlayerFightRecord other)
	{
		if (other != null)
		{
			if (other.Time != 0L)
			{
				Time = other.Time;
			}
			data_.Add(other.data_);
			if (other.ReplayId.Length != 0)
			{
				ReplayId = other.ReplayId;
			}
			if (other.Version.Length != 0)
			{
				Version = other.Version;
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
			case 9u:
				Time = input.ReadSFixed64();
				break;
			case 18u:
				data_.AddEntriesFrom(ref input, _repeated_data_codec);
				break;
			case 26u:
				ReplayId = input.ReadString();
				break;
			case 34u:
				Version = input.ReadString();
				break;
			}
		}
	}
}
