using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattlePassConfigure : IMessage<BattlePassConfigure>, IMessage, IEquatable<BattlePassConfigure>, IDeepCloneable<BattlePassConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattlePassConfigure> _parser = new MessageParser<BattlePassConfigure>(() => new BattlePassConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<BattlePassInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, BattlePassInfoConfigure.Parser);

	private readonly RepeatedField<BattlePassInfoConfigure> infos_ = new RepeatedField<BattlePassInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, BattlePassInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, BattlePassInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassInfoConfigure.Parser), 18u);

	private readonly MapField<int, BattlePassInfoConfigure> infoDict_ = new MapField<int, BattlePassInfoConfigure>();

	public const int GoodssFieldNumber = 3;

	private static readonly FieldCodec<BattlePassGoodsConfigure> _repeated_goodss_codec = FieldCodec.ForMessage(26u, BattlePassGoodsConfigure.Parser);

	private readonly RepeatedField<BattlePassGoodsConfigure> goodss_ = new RepeatedField<BattlePassGoodsConfigure>();

	public const int GoodsDictFieldNumber = 4;

	private static readonly MapField<int, BattlePassGoodsConfigure>.Codec _map_goodsDict_codec = new MapField<int, BattlePassGoodsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassGoodsConfigure.Parser), 34u);

	private readonly MapField<int, BattlePassGoodsConfigure> goodsDict_ = new MapField<int, BattlePassGoodsConfigure>();

	public const int TasksFieldNumber = 5;

	private static readonly FieldCodec<BattlePassTaskConfigure> _repeated_tasks_codec = FieldCodec.ForMessage(42u, BattlePassTaskConfigure.Parser);

	private readonly RepeatedField<BattlePassTaskConfigure> tasks_ = new RepeatedField<BattlePassTaskConfigure>();

	public const int TaskDictFieldNumber = 6;

	private static readonly MapField<int, BattlePassTaskConfigure>.Codec _map_taskDict_codec = new MapField<int, BattlePassTaskConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassTaskConfigure.Parser), 50u);

	private readonly MapField<int, BattlePassTaskConfigure> taskDict_ = new MapField<int, BattlePassTaskConfigure>();

	public const int RewardsFieldNumber = 7;

	private static readonly FieldCodec<BattlePassRewardConfigure> _repeated_rewards_codec = FieldCodec.ForMessage(58u, BattlePassRewardConfigure.Parser);

	private readonly RepeatedField<BattlePassRewardConfigure> rewards_ = new RepeatedField<BattlePassRewardConfigure>();

	public const int RewardDictFieldNumber = 8;

	private static readonly MapField<int, BattlePassRewardConfigure>.Codec _map_rewardDict_codec = new MapField<int, BattlePassRewardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassRewardConfigure.Parser), 66u);

	private readonly MapField<int, BattlePassRewardConfigure> rewardDict_ = new MapField<int, BattlePassRewardConfigure>();

	public const int RewardAdssFieldNumber = 9;

	private static readonly FieldCodec<BattlePassRewardAdsConfigure> _repeated_rewardAdss_codec = FieldCodec.ForMessage(74u, BattlePassRewardAdsConfigure.Parser);

	private readonly RepeatedField<BattlePassRewardAdsConfigure> rewardAdss_ = new RepeatedField<BattlePassRewardAdsConfigure>();

	public const int RewardAdsDictFieldNumber = 10;

	private static readonly MapField<int, BattlePassRewardAdsConfigure>.Codec _map_rewardAdsDict_codec = new MapField<int, BattlePassRewardAdsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassRewardAdsConfigure.Parser), 82u);

	private readonly MapField<int, BattlePassRewardAdsConfigure> rewardAdsDict_ = new MapField<int, BattlePassRewardAdsConfigure>();

	public const int RewardDetailsFieldNumber = 11;

	private static readonly FieldCodec<BattlePassRewardDetailConfigure> _repeated_rewardDetails_codec = FieldCodec.ForMessage(90u, BattlePassRewardDetailConfigure.Parser);

	private readonly RepeatedField<BattlePassRewardDetailConfigure> rewardDetails_ = new RepeatedField<BattlePassRewardDetailConfigure>();

	public const int RewardDetailDictFieldNumber = 12;

	private static readonly MapField<int, BattlePassRewardDetailConfigure>.Codec _map_rewardDetailDict_codec = new MapField<int, BattlePassRewardDetailConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, BattlePassRewardDetailConfigure.Parser), 98u);

	private readonly MapField<int, BattlePassRewardDetailConfigure> rewardDetailDict_ = new MapField<int, BattlePassRewardDetailConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[10];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassGoodsConfigure> Goodss => goodss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassGoodsConfigure> GoodsDict => goodsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassTaskConfigure> Tasks => tasks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassTaskConfigure> TaskDict => taskDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassRewardConfigure> Rewards => rewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassRewardConfigure> RewardDict => rewardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassRewardAdsConfigure> RewardAdss => rewardAdss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassRewardAdsConfigure> RewardAdsDict => rewardAdsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BattlePassRewardDetailConfigure> RewardDetails => rewardDetails_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, BattlePassRewardDetailConfigure> RewardDetailDict => rewardDetailDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassConfigure(BattlePassConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		goodss_ = other.goodss_.Clone();
		goodsDict_ = other.goodsDict_.Clone();
		tasks_ = other.tasks_.Clone();
		taskDict_ = other.taskDict_.Clone();
		rewards_ = other.rewards_.Clone();
		rewardDict_ = other.rewardDict_.Clone();
		rewardAdss_ = other.rewardAdss_.Clone();
		rewardAdsDict_ = other.rewardAdsDict_.Clone();
		rewardDetails_ = other.rewardDetails_.Clone();
		rewardDetailDict_ = other.rewardDetailDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassConfigure Clone()
	{
		return new BattlePassConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!goodss_.Equals(other.goodss_))
		{
			return false;
		}
		if (!GoodsDict.Equals(other.GoodsDict))
		{
			return false;
		}
		if (!tasks_.Equals(other.tasks_))
		{
			return false;
		}
		if (!TaskDict.Equals(other.TaskDict))
		{
			return false;
		}
		if (!rewards_.Equals(other.rewards_))
		{
			return false;
		}
		if (!RewardDict.Equals(other.RewardDict))
		{
			return false;
		}
		if (!rewardAdss_.Equals(other.rewardAdss_))
		{
			return false;
		}
		if (!RewardAdsDict.Equals(other.RewardAdsDict))
		{
			return false;
		}
		if (!rewardDetails_.Equals(other.rewardDetails_))
		{
			return false;
		}
		if (!RewardDetailDict.Equals(other.RewardDetailDict))
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
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= goodss_.GetHashCode();
		num ^= GoodsDict.GetHashCode();
		num ^= tasks_.GetHashCode();
		num ^= TaskDict.GetHashCode();
		num ^= rewards_.GetHashCode();
		num ^= RewardDict.GetHashCode();
		num ^= rewardAdss_.GetHashCode();
		num ^= RewardAdsDict.GetHashCode();
		num ^= rewardDetails_.GetHashCode();
		num ^= RewardDetailDict.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		goodss_.WriteTo(ref output, _repeated_goodss_codec);
		goodsDict_.WriteTo(ref output, _map_goodsDict_codec);
		tasks_.WriteTo(ref output, _repeated_tasks_codec);
		taskDict_.WriteTo(ref output, _map_taskDict_codec);
		rewards_.WriteTo(ref output, _repeated_rewards_codec);
		rewardDict_.WriteTo(ref output, _map_rewardDict_codec);
		rewardAdss_.WriteTo(ref output, _repeated_rewardAdss_codec);
		rewardAdsDict_.WriteTo(ref output, _map_rewardAdsDict_codec);
		rewardDetails_.WriteTo(ref output, _repeated_rewardDetails_codec);
		rewardDetailDict_.WriteTo(ref output, _map_rewardDetailDict_codec);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += goodss_.CalculateSize(_repeated_goodss_codec);
		num += goodsDict_.CalculateSize(_map_goodsDict_codec);
		num += tasks_.CalculateSize(_repeated_tasks_codec);
		num += taskDict_.CalculateSize(_map_taskDict_codec);
		num += rewards_.CalculateSize(_repeated_rewards_codec);
		num += rewardDict_.CalculateSize(_map_rewardDict_codec);
		num += rewardAdss_.CalculateSize(_repeated_rewardAdss_codec);
		num += rewardAdsDict_.CalculateSize(_map_rewardAdsDict_codec);
		num += rewardDetails_.CalculateSize(_repeated_rewardDetails_codec);
		num += rewardDetailDict_.CalculateSize(_map_rewardDetailDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			goodss_.Add(other.goodss_);
			goodsDict_.MergeFrom(other.goodsDict_);
			tasks_.Add(other.tasks_);
			taskDict_.MergeFrom(other.taskDict_);
			rewards_.Add(other.rewards_);
			rewardDict_.MergeFrom(other.rewardDict_);
			rewardAdss_.Add(other.rewardAdss_);
			rewardAdsDict_.MergeFrom(other.rewardAdsDict_);
			rewardDetails_.Add(other.rewardDetails_);
			rewardDetailDict_.MergeFrom(other.rewardDetailDict_);
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
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				goodss_.AddEntriesFrom(ref input, _repeated_goodss_codec);
				break;
			case 34u:
				goodsDict_.AddEntriesFrom(ref input, _map_goodsDict_codec);
				break;
			case 42u:
				tasks_.AddEntriesFrom(ref input, _repeated_tasks_codec);
				break;
			case 50u:
				taskDict_.AddEntriesFrom(ref input, _map_taskDict_codec);
				break;
			case 58u:
				rewards_.AddEntriesFrom(ref input, _repeated_rewards_codec);
				break;
			case 66u:
				rewardDict_.AddEntriesFrom(ref input, _map_rewardDict_codec);
				break;
			case 74u:
				rewardAdss_.AddEntriesFrom(ref input, _repeated_rewardAdss_codec);
				break;
			case 82u:
				rewardAdsDict_.AddEntriesFrom(ref input, _map_rewardAdsDict_codec);
				break;
			case 90u:
				rewardDetails_.AddEntriesFrom(ref input, _repeated_rewardDetails_codec);
				break;
			case 98u:
				rewardDetailDict_.AddEntriesFrom(ref input, _map_rewardDetailDict_codec);
				break;
			}
		}
	}

	public void Fix(FixBattlePassConfigure FixBattlePass)
	{
		if (FixBattlePass == null)
		{
			return;
		}
		MapField<int, FixBattlePassInfoConfigure> infoDict = FixBattlePass.InfoDict;
		if (infoDict != null && infoDict.Count > 0)
		{
			for (int i = 0; i < infos_.Count; i++)
			{
				if (infoDict.TryGetValue(infos_[i].Id, out var value))
				{
					infos_[i].FixTime(value);
					infoDict_[infos_[i].Id].FixTime(value);
				}
			}
		}
		MapField<int, FixBattlePassTaskConfigure> taskDict = FixBattlePass.TaskDict;
		if (taskDict == null || taskDict.Count <= 0)
		{
			return;
		}
		for (int j = 0; j < tasks_.Count; j++)
		{
			if (taskDict.TryGetValue(tasks_[j].Id, out var value2))
			{
				tasks_[j].FixTime(value2);
				taskDict_[tasks_[j].Id].FixTime(value2);
			}
		}
	}
}
