using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class ChatConfigure : IMessage<ChatConfigure>, IMessage, IEquatable<ChatConfigure>, IDeepCloneable<ChatConfigure>, IBufferMessage
{
	private static readonly MessageParser<ChatConfigure> _parser = new MessageParser<ChatConfigure>(() => new ChatConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<ChatInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, ChatInfoConfigure.Parser);

	private readonly RepeatedField<ChatInfoConfigure> infos_ = new RepeatedField<ChatInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, ChatInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, ChatInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChatInfoConfigure.Parser), 18u);

	private readonly MapField<int, ChatInfoConfigure> infoDict_ = new MapField<int, ChatInfoConfigure>();

	public const int MarksFieldNumber = 3;

	private static readonly FieldCodec<ChatMarkConfigure> _repeated_marks_codec = FieldCodec.ForMessage(26u, ChatMarkConfigure.Parser);

	private readonly RepeatedField<ChatMarkConfigure> marks_ = new RepeatedField<ChatMarkConfigure>();

	public const int MarkDictFieldNumber = 4;

	private static readonly MapField<int, ChatMarkConfigure>.Codec _map_markDict_codec = new MapField<int, ChatMarkConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, ChatMarkConfigure.Parser), 34u);

	private readonly MapField<int, ChatMarkConfigure> markDict_ = new MapField<int, ChatMarkConfigure>();

	private List<ChatInfoConfigure> _roomWaitChats;

	private List<ChatInfoConfigure> _battleResultChats;

	private Dictionary<int, CharacterExpressionPackConfigureItem> expressionDic;

	private Dictionary<int, int> expressionPackIdDic;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChatConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChatReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChatInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChatInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ChatMarkConfigure> Marks => marks_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChatMarkConfigure> MarkDict => markDict_;

	public List<ChatInfoConfigure> RoomWaitChats
	{
		get
		{
			if (_roomWaitChats == null)
			{
				_roomWaitChats = new List<ChatInfoConfigure>();
				foreach (ChatInfoConfigure item in infos_)
				{
					if (item.ChatType.Contains(ChatType.Room))
					{
						_roomWaitChats.Add(item);
					}
				}
			}
			return _roomWaitChats;
		}
	}

	public List<ChatInfoConfigure> BattleResultChats
	{
		get
		{
			if (_battleResultChats == null)
			{
				_battleResultChats = new List<ChatInfoConfigure>();
				foreach (ChatInfoConfigure item in infos_)
				{
					if (item.ChatType.Contains(ChatType.Result))
					{
						_battleResultChats.Add(item);
					}
				}
			}
			return _battleResultChats;
		}
	}

	public Dictionary<int, CharacterExpressionPackConfigureItem> ExpressionDic
	{
		get
		{
			if (expressionDic == null)
			{
				expressionDic = new Dictionary<int, CharacterExpressionPackConfigureItem>();
				foreach (KeyValuePair<int, CharacterExpressionPackConfigure> item in StaticConfigure.Character.ExpressionPackDict)
				{
					foreach (CharacterExpressionPackConfigureItem characterExpressionPackConfigureItem in item.Value.CharacterExpressionPackConfigureItems)
					{
						expressionDic[characterExpressionPackConfigureItem.ItemID] = characterExpressionPackConfigureItem;
					}
				}
			}
			return expressionDic;
		}
	}

	public Dictionary<int, int> ExpressionPackIdDic
	{
		get
		{
			if (expressionPackIdDic == null)
			{
				expressionPackIdDic = new Dictionary<int, int>();
				foreach (KeyValuePair<int, CharacterExpressionPackConfigure> item in StaticConfigure.Character.ExpressionPackDict)
				{
					foreach (CharacterExpressionPackConfigureItem characterExpressionPackConfigureItem in item.Value.CharacterExpressionPackConfigureItems)
					{
						expressionPackIdDic[characterExpressionPackConfigureItem.ItemID] = item.Value.Id;
					}
				}
			}
			return expressionPackIdDic;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatConfigure(ChatConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		marks_ = other.marks_.Clone();
		markDict_ = other.markDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatConfigure Clone()
	{
		return new ChatConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChatConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChatConfigure other)
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
		if (!marks_.Equals(other.marks_))
		{
			return false;
		}
		if (!MarkDict.Equals(other.MarkDict))
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
		num ^= marks_.GetHashCode();
		num ^= MarkDict.GetHashCode();
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
		marks_.WriteTo(ref output, _repeated_marks_codec);
		markDict_.WriteTo(ref output, _map_markDict_codec);
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
		num += marks_.CalculateSize(_repeated_marks_codec);
		num += markDict_.CalculateSize(_map_markDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChatConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			marks_.Add(other.marks_);
			markDict_.MergeFrom(other.markDict_);
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
				marks_.AddEntriesFrom(ref input, _repeated_marks_codec);
				break;
			case 34u:
				markDict_.AddEntriesFrom(ref input, _map_markDict_codec);
				break;
			}
		}
	}

	public List<ChatInfoConfigure> GetBattleChats(MapModeType mode, int mapId)
	{
		List<ChatInfoConfigure> list = new List<ChatInfoConfigure>();
		foreach (ChatInfoConfigure item in infos_)
		{
			if (item.ChatType.Contains(ChatType.InGame) && item.MapModeType.Contains(mode) && (item.MapLimit.Count == 0 || item.MapLimit.Contains(mapId)))
			{
				list.Add(item);
			}
		}
		return list;
	}
}
