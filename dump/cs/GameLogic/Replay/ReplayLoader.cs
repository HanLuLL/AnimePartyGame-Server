using System;
using System.Collections.Generic;
using System.IO;
using Core.Net;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic.Replay;

public static class ReplayLoader
{
	private const string LOG_PREFIX = "[ReplayLoader]";

	private const int SERVER_PACK_HEADER_LENGTH = 6;

	private static byte[] ReadPayloadBytes(ByteBuf stream, int length)
	{
		byte[] array = new byte[length];
		Buffer.BlockCopy(stream.GetRaw(), stream.ReaderIndex(), array, 0, length);
		stream.SkipBytes(length);
		return array;
	}

	public static byte[] LoadFromLocalFile(string filePath)
	{
		return File.ReadAllBytes(filePath);
	}

	public static ReplayLoadResult ValidatePackage(ReplayPackage package)
	{
		ReplayLoadResult replayLoadResult = new ReplayLoadResult
		{
			Package = package
		};
		if (package == null)
		{
			return Fail(replayLoadResult, "PackageNull", "回放包为 null。");
		}
		if (package.Frames == null || package.Frames.Count == 0)
		{
			return Fail(replayLoadResult, "FramesEmpty", "回放包没有任何帧。");
		}
		if (!TryFindRunningGameRoom(replayLoadResult, package, out var failResult))
		{
			return failResult;
		}
		FillPackageHeaderFromRoom(package, replayLoadResult.RunningGame.Room);
		ExtractTurnNodes(replayLoadResult, package);
		if (!TryFindGameFinish(replayLoadResult, package, out var failResult2))
		{
			return failResult2;
		}
		replayLoadResult.Success = true;
		LogSuccess(replayLoadResult);
		return replayLoadResult;
	}

	public static ReplayLoadResult LoadServerPackStream(byte[] bytes)
	{
		if (bytes == null || bytes.Length == 0)
		{
			return Fail("BytesEmpty", "回放抓包流为空。");
		}
		ReplayPackage replayPackage = new ReplayPackage();
		ByteBuf byteBuf = new ByteBuf(bytes).SetIndex(0, bytes.Length);
		int num = 0;
		while (byteBuf.ReadableBytes() > 0)
		{
			int num2 = byteBuf.ReaderIndex();
			if (!byteBuf.HasBytes(6))
			{
				return Fail("TruncatedFrameHeader", $"回放帧头被截断，offset={num2}, remaining={byteBuf.ReadableBytes()}。");
			}
			short num3 = byteBuf.ReadShort();
			int num4 = byteBuf.ReadInt();
			if (num4 < 0)
			{
				return Fail("FramePayloadTooLarge", $"回放帧负载过大，index={num}, offset={num2}, cmdId={num3}, payloadLength={num4}。");
			}
			if (num4 > byteBuf.ReadableBytes())
			{
				return Fail("TruncatedFramePayload", $"回放帧负载被截断，index={num}, offset={num2}, cmdId={num3}, payloadLength={num4}, remaining={byteBuf.ReadableBytes()}。");
			}
			byte[] payload = ((num4 > 0) ? ReadPayloadBytes(byteBuf, num4) : Array.Empty<byte>());
			replayPackage.Frames.Add(new ReplayFrame(num, num3, payload));
			num++;
		}
		return ValidatePackage(replayPackage);
	}

	public static (GameFinishS2C gameFinish, ReplaySnapshotS2C lastSnapshot) TryParseReplaySettlementOnly(byte[] bytes)
	{
		if (bytes == null || bytes.Length < 6)
		{
			return (gameFinish: null, lastSnapshot: null);
		}
		ByteBuf byteBuf = new ByteBuf(bytes).SetIndex(0, bytes.Length);
		GameFinishS2C item = null;
		ReplaySnapshotS2C item2 = null;
		while (byteBuf.ReadableBytes() >= 6)
		{
			byteBuf.ReaderIndex();
			short num = byteBuf.ReadShort();
			int num2 = byteBuf.ReadInt();
			if (num2 < 0)
			{
				return (gameFinish: item, lastSnapshot: item2);
			}
			if (num2 > byteBuf.ReadableBytes())
			{
				return (gameFinish: item, lastSnapshot: item2);
			}
			if (num == 1016 && num2 > 0)
			{
				byte[] array = ReadPayloadBytes(byteBuf, num2);
				try
				{
					item = ByteBuf.ReadObject<GameFinishS2C>(array);
				}
				catch (Exception)
				{
				}
			}
			else if (num == 1113 && num2 > 0)
			{
				byte[] array2 = ReadPayloadBytes(byteBuf, num2);
				try
				{
					item2 = ByteBuf.ReadObject<ReplaySnapshotS2C>(array2);
				}
				catch (Exception)
				{
				}
			}
			else if (num2 > 0)
			{
				byteBuf.SkipBytes(num2);
			}
		}
		return (gameFinish: item, lastSnapshot: item2);
	}

	private static void FillPackageHeaderFromRoom(ReplayPackage package, Room room)
	{
		if (package.RoomId == 0L)
		{
			package.RoomId = room.Id;
		}
		if (package.RoomServerId == 0)
		{
			package.RoomServerId = room.RoomServerId;
		}
		if (package.MapId == 0)
		{
			package.MapId = room.MapId;
		}
		if (package.MapType == 0)
		{
			package.MapType = room.MapType;
		}
	}

	private static bool TryFindRunningGameRoom(ReplayLoadResult result, ReplayPackage package, out ReplayLoadResult failResult)
	{
		failResult = null;
		for (int i = 0; i < package.Frames.Count; i++)
		{
			ReplayFrame replayFrame = package.Frames[i];
			if (replayFrame.CmdId != 1003)
			{
				continue;
			}
			if (!replayFrame.HasPayload)
			{
				result.AddWarning($"RunningGameS2C payload is empty; skipped. index={replayFrame.Index}");
				continue;
			}
			RunningGameS2C runningGameS2C;
			try
			{
				runningGameS2C = ByteBuf.ReadObject<RunningGameS2C>(replayFrame.Payload);
			}
			catch (Exception ex)
			{
				failResult = Fail(result, "RunningGameParseFailed", $"解析 RunningGameS2C 失败。index={replayFrame.Index}, message={ex.Message}");
				return false;
			}
			if (runningGameS2C.Room == null)
			{
				result.AddWarning($"RunningGameS2C.Room is null; skipped. index={replayFrame.Index}");
			}
			else if (runningGameS2C.Room.State == Room.Types.State.Running)
			{
				result.RunningGame = runningGameS2C;
				result.RebuildFrameIndex = replayFrame.Index;
				result.PlaybackStartFrameIndex = Math.Min(replayFrame.Index + 1, package.Frames.Count);
				return true;
			}
		}
		failResult = Fail(result, "RunningGameRunningRoomNotFound", $"回放包中没有 Room.State=Running 的 RunningGameS2C。frameCount={package.FrameCount}");
		return false;
	}

	private static bool TryFindGameFinish(ReplayLoadResult result, ReplayPackage package, out ReplayLoadResult failResult)
	{
		failResult = null;
		int num = Math.Max(0, result.PlaybackStartFrameIndex);
		for (int num2 = package.Frames.Count - 1; num2 >= num; num2--)
		{
			ReplayFrame replayFrame = package.Frames[num2];
			if (replayFrame.CmdId == 1016)
			{
				if (replayFrame.HasPayload)
				{
					GameFinishS2C gameFinish;
					try
					{
						gameFinish = ByteBuf.ReadObject<GameFinishS2C>(replayFrame.Payload);
					}
					catch (Exception ex)
					{
						failResult = Fail(result, "GameFinishParseFailed", $"解析 GameFinishS2C 失败。index={replayFrame.Index}, message={ex.Message}");
						return false;
					}
					result.GameFinish = gameFinish;
					result.GameFinishFrameIndex = replayFrame.Index;
					return true;
				}
				result.AddWarning($"GameFinishS2C payload is empty; skipped. index={replayFrame.Index}");
			}
		}
		failResult = Fail(result, "GameFinishNotFound", $"回放包中没有 GameFinishS2C。frameCount={package.FrameCount}, playbackStartFrameIndex={result.PlaybackStartFrameIndex}");
		return false;
	}

	private static ReplayLoadResult Fail(string code, string message)
	{
		return Fail(new ReplayLoadResult(), code, message);
	}

	private static ReplayLoadResult Fail(ReplayLoadResult result, string code, string message)
	{
		result.Success = false;
		result.ErrorCode = code;
		result.ErrorMessage = message;
		Debug.LogError("[ReplayLoader] 加载失败。code=" + code + ", message=" + message);
		return result;
	}

	private static void ExtractTurnNodes(ReplayLoadResult result, ReplayPackage package)
	{
		package.TurnNodes.Clear();
		package.Rounds.Clear();
		ReplayRoundNode replayRoundNode = null;
		ReplayTurnNode replayTurnNode = null;
		for (int i = 0; i < package.Frames.Count; i++)
		{
			ReplayFrame frame = package.Frames[i];
			ReplaySnapshotS2C snapshot;
			if (frame.CmdId != 1113)
			{
				if (frame.HasPayload && replayTurnNode != null)
				{
					TryApplyBehaviorMarker(frame, i, replayTurnNode, result);
				}
			}
			else if (TryParseSnapshot(frame, i, result, out snapshot))
			{
				if (replayRoundNode == null || replayRoundNode.Round != snapshot.Room.Round)
				{
					replayRoundNode = new ReplayRoundNode
					{
						Round = snapshot.Room.Round,
						FrameIndex = i,
						PlaybackStartIndex = i + 1
					};
					package.Rounds.Add(replayRoundNode);
				}
				ReplayTurnNode replayTurnNode2 = new ReplayTurnNode
				{
					FrameIndex = i,
					PlaybackStartIndex = i + 1,
					TurnIndex = replayRoundNode.Turns.Count,
					Round = snapshot.Room.Round,
					PlayerId = snapshot.PlayerId,
					Snapshot = snapshot
				};
				replayRoundNode.Turns.Add(replayTurnNode2);
				package.TurnNodes.Add(replayTurnNode2);
				replayTurnNode = replayTurnNode2;
			}
		}
		if (package.TurnNodes.Count > 0 && package.Rounds.Count > 0)
		{
			package.TurnNodes.RemoveAt(package.TurnNodes.Count - 1);
			List<ReplayRoundNode> rounds = package.Rounds;
			ReplayRoundNode replayRoundNode2 = rounds[rounds.Count - 1];
			if (replayRoundNode2.Turns.Count > 0)
			{
				replayRoundNode2.Turns.RemoveAt(replayRoundNode2.Turns.Count - 1);
			}
			if (replayRoundNode2.Turns.Count == 0)
			{
				package.Rounds.RemoveAt(package.Rounds.Count - 1);
			}
		}
	}

	private static bool TryParseSnapshot(ReplayFrame frame, int frameIndex, ReplayLoadResult result, out ReplaySnapshotS2C snapshot)
	{
		snapshot = null;
		if (!frame.HasPayload)
		{
			return false;
		}
		try
		{
			snapshot = ByteBuf.ReadObject<ReplaySnapshotS2C>(frame.Payload);
			if (snapshot?.Room == null)
			{
				result.AddWarning($"ReplaySnapshotS2C.Room is null; skipped. frameIndex={frameIndex}");
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			result.AddWarning("Failed to parse ReplaySnapshotS2C. " + $"frameIndex={frameIndex}, message={ex.Message}");
			return false;
		}
	}

	private static void TryApplyBehaviorMarker(ReplayFrame frame, int frameIndex, ReplayTurnNode turn, ReplayLoadResult result)
	{
		try
		{
			switch (frame.CmdId)
			{
			case 1040:
				ApplyEffects(ByteBuf.ReadObject<UpdateHeroAttrS2C>(frame.Payload)?.EffectDatas, turn);
				break;
			case 1096:
			{
				HeroSkillMoveEffectS2C heroSkillMoveEffectS2C = ByteBuf.ReadObject<HeroSkillMoveEffectS2C>(frame.Payload);
				if (heroSkillMoveEffectS2C?.EffectData == null)
				{
					break;
				}
				{
					foreach (HeroSkillMoveEffect value2 in heroSkillMoveEffectS2C.EffectData.Values)
					{
						if (value2?.Effects == null)
						{
							continue;
						}
						foreach (UpdateHeroAttrS2C effect in value2.Effects)
						{
							ApplyEffects(effect?.EffectDatas, turn);
						}
					}
					break;
				}
			}
			case 1098:
			{
				SayPhraseNotifyS2C sayPhraseNotifyS2C = ByteBuf.ReadObject<SayPhraseNotifyS2C>(frame.Payload);
				if (sayPhraseNotifyS2C?.Phrase != null && sayPhraseNotifyS2C.Phrase.TriggerType == TriggerPhrase.Types.Type.TransferGold && sayPhraseNotifyS2C.Phrase.ActivePlayerId == turn.PlayerId)
				{
					turn.Behaviors |= TurnBehavior.TRANSFER_GOLD;
				}
				break;
			}
			case 1115:
			{
				ReplayDieS2C replayDieS2C = ByteBuf.ReadObject<ReplayDieS2C>(frame.Payload);
				if (replayDieS2C != null && replayDieS2C.KillerId == turn.PlayerId && StaticConfigure.Monster.InfoDict.TryGetValue(replayDieS2C.HeroId, out var value) && value.MonsterType == MonsterType.Elite)
				{
					turn.Behaviors |= TurnBehavior.KILL_MONSTER;
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
			result.AddWarning($"ApplyBehaviorMarker: parse failed. frameIndex={frameIndex}, cmdId={frame.CmdId}, message={ex.Message}");
		}
	}

	private static void ApplyEffects(IEnumerable<HeroAttrEffect> effects, ReplayTurnNode turn)
	{
		foreach (HeroAttrEffect effect in effects)
		{
			ApplyEffect(effect, turn);
		}
	}

	private static void ApplyEffect(HeroAttrEffect eff, ReplayTurnNode turn)
	{
		if (eff != null && eff.Lv != null && eff.Lv.PlayerId == turn.PlayerId)
		{
			turn.Behaviors |= TurnBehavior.LEVEL_UP;
		}
	}

	private static void LogSuccess(ReplayLoadResult result)
	{
		ReplayPackage package = result.Package;
		_ = package.Frames[0];
		List<ReplayFrame> frames = package.Frames;
		_ = frames[frames.Count - 1];
		_ = result.RunningGame.Room;
	}
}
