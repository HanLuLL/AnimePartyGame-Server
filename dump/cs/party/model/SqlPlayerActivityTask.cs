using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SqlPlayerActivityTask : IMessage<SqlPlayerActivityTask>, IMessage, IEquatable<SqlPlayerActivityTask>, IDeepCloneable<SqlPlayerActivityTask>, IBufferMessage
{
	private static readonly MessageParser<SqlPlayerActivityTask> _parser = new MessageParser<SqlPlayerActivityTask>(() => new SqlPlayerActivityTask());

	private UnknownFieldSet _unknownFields;

	public const int TaskFieldNumber = 1;

	private static readonly FieldCodec<ActivityInfo> _repeated_task_codec = FieldCodec.ForMessage(10u, ActivityInfo.Parser);

	private readonly RepeatedField<ActivityInfo> task_ = new RepeatedField<ActivityInfo>();

	public const int Day7FieldNumber = 2;

	private static readonly MapField<int, Day7Reward>.Codec _map_day7_codec = new MapField<int, Day7Reward>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, Day7Reward.Parser), 18u);

	private readonly MapField<int, Day7Reward> day7_ = new MapField<int, Day7Reward>();

	public const int SignInRewardFieldNumber = 3;

	private static readonly MapField<int, SignInReward>.Codec _map_signInReward_codec = new MapField<int, SignInReward>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.SignInReward.Parser), 26u);

	private readonly MapField<int, SignInReward> signInReward_ = new MapField<int, SignInReward>();

	public const int ReturnInfoFieldNumber = 4;

	private ReturnInfo returnInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SqlPlayerActivityTask> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[37];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ActivityInfo> Task => task_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, Day7Reward> Day7 => day7_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, SignInReward> SignInReward => signInReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnInfo ReturnInfo
	{
		get
		{
			return returnInfo_;
		}
		set
		{
			returnInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerActivityTask()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerActivityTask(SqlPlayerActivityTask other)
		: this()
	{
		task_ = other.task_.Clone();
		day7_ = other.day7_.Clone();
		signInReward_ = other.signInReward_.Clone();
		returnInfo_ = ((other.returnInfo_ != null) ? other.returnInfo_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerActivityTask Clone()
	{
		return new SqlPlayerActivityTask(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SqlPlayerActivityTask);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SqlPlayerActivityTask other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!task_.Equals(other.task_))
		{
			return false;
		}
		if (!Day7.Equals(other.Day7))
		{
			return false;
		}
		if (!SignInReward.Equals(other.SignInReward))
		{
			return false;
		}
		if (!object.Equals(ReturnInfo, other.ReturnInfo))
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
		num ^= task_.GetHashCode();
		num ^= Day7.GetHashCode();
		num ^= SignInReward.GetHashCode();
		if (returnInfo_ != null)
		{
			num ^= ReturnInfo.GetHashCode();
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
		task_.WriteTo(ref output, _repeated_task_codec);
		day7_.WriteTo(ref output, _map_day7_codec);
		signInReward_.WriteTo(ref output, _map_signInReward_codec);
		if (returnInfo_ != null)
		{
			output.WriteRawTag(34);
			output.WriteMessage(ReturnInfo);
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
		num += task_.CalculateSize(_repeated_task_codec);
		num += day7_.CalculateSize(_map_day7_codec);
		num += signInReward_.CalculateSize(_map_signInReward_codec);
		if (returnInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ReturnInfo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SqlPlayerActivityTask other)
	{
		if (other == null)
		{
			return;
		}
		task_.Add(other.task_);
		day7_.MergeFrom(other.day7_);
		signInReward_.MergeFrom(other.signInReward_);
		if (other.returnInfo_ != null)
		{
			if (returnInfo_ == null)
			{
				ReturnInfo = new ReturnInfo();
			}
			ReturnInfo.MergeFrom(other.ReturnInfo);
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
				task_.AddEntriesFrom(ref input, _repeated_task_codec);
				break;
			case 18u:
				day7_.AddEntriesFrom(ref input, _map_day7_codec);
				break;
			case 26u:
				signInReward_.AddEntriesFrom(ref input, _map_signInReward_codec);
				break;
			case 34u:
				if (returnInfo_ == null)
				{
					ReturnInfo = new ReturnInfo();
				}
				input.ReadMessage(ReturnInfo);
				break;
			}
		}
	}
}
