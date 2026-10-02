using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ShowPlayerStatistics : IMessage<ShowPlayerStatistics>, IMessage, IEquatable<ShowPlayerStatistics>, IDeepCloneable<ShowPlayerStatistics>, IBufferMessage
{
	private static readonly MessageParser<ShowPlayerStatistics> _parser = new MessageParser<ShowPlayerStatistics>(() => new ShowPlayerStatistics());

	private UnknownFieldSet _unknownFields;

	public const int FightCountFieldNumber = 1;

	private int fightCount_;

	public const int WinFightCountFieldNumber = 2;

	private int winFightCount_;

	public const int RoleCardCountFieldNumber = 3;

	private int roleCardCount_;

	public const int UseHeroFieldNumber = 4;

	private int useHero_;

	public const int AdornCountFieldNumber = 5;

	private int adornCount_;

	public const int SkinCountFieldNumber = 6;

	private int skinCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ShowPlayerStatistics> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[107];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FightCount
	{
		get
		{
			return fightCount_;
		}
		set
		{
			fightCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WinFightCount
	{
		get
		{
			return winFightCount_;
		}
		set
		{
			winFightCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoleCardCount
	{
		get
		{
			return roleCardCount_;
		}
		set
		{
			roleCardCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseHero
	{
		get
		{
			return useHero_;
		}
		set
		{
			useHero_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AdornCount
	{
		get
		{
			return adornCount_;
		}
		set
		{
			adornCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinCount
	{
		get
		{
			return skinCount_;
		}
		set
		{
			skinCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerStatistics()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerStatistics(ShowPlayerStatistics other)
		: this()
	{
		fightCount_ = other.fightCount_;
		winFightCount_ = other.winFightCount_;
		roleCardCount_ = other.roleCardCount_;
		useHero_ = other.useHero_;
		adornCount_ = other.adornCount_;
		skinCount_ = other.skinCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShowPlayerStatistics Clone()
	{
		return new ShowPlayerStatistics(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ShowPlayerStatistics);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ShowPlayerStatistics other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (FightCount != other.FightCount)
		{
			return false;
		}
		if (WinFightCount != other.WinFightCount)
		{
			return false;
		}
		if (RoleCardCount != other.RoleCardCount)
		{
			return false;
		}
		if (UseHero != other.UseHero)
		{
			return false;
		}
		if (AdornCount != other.AdornCount)
		{
			return false;
		}
		if (SkinCount != other.SkinCount)
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
		if (FightCount != 0)
		{
			num ^= FightCount.GetHashCode();
		}
		if (WinFightCount != 0)
		{
			num ^= WinFightCount.GetHashCode();
		}
		if (RoleCardCount != 0)
		{
			num ^= RoleCardCount.GetHashCode();
		}
		if (UseHero != 0)
		{
			num ^= UseHero.GetHashCode();
		}
		if (AdornCount != 0)
		{
			num ^= AdornCount.GetHashCode();
		}
		if (SkinCount != 0)
		{
			num ^= SkinCount.GetHashCode();
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
		if (FightCount != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(FightCount);
		}
		if (WinFightCount != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(WinFightCount);
		}
		if (RoleCardCount != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(RoleCardCount);
		}
		if (UseHero != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(UseHero);
		}
		if (AdornCount != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(AdornCount);
		}
		if (SkinCount != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(SkinCount);
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
		if (FightCount != 0)
		{
			num += 5;
		}
		if (WinFightCount != 0)
		{
			num += 5;
		}
		if (RoleCardCount != 0)
		{
			num += 5;
		}
		if (UseHero != 0)
		{
			num += 5;
		}
		if (AdornCount != 0)
		{
			num += 5;
		}
		if (SkinCount != 0)
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
	public void MergeFrom(ShowPlayerStatistics other)
	{
		if (other != null)
		{
			if (other.FightCount != 0)
			{
				FightCount = other.FightCount;
			}
			if (other.WinFightCount != 0)
			{
				WinFightCount = other.WinFightCount;
			}
			if (other.RoleCardCount != 0)
			{
				RoleCardCount = other.RoleCardCount;
			}
			if (other.UseHero != 0)
			{
				UseHero = other.UseHero;
			}
			if (other.AdornCount != 0)
			{
				AdornCount = other.AdornCount;
			}
			if (other.SkinCount != 0)
			{
				SkinCount = other.SkinCount;
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
				FightCount = input.ReadSFixed32();
				break;
			case 21u:
				WinFightCount = input.ReadSFixed32();
				break;
			case 29u:
				RoleCardCount = input.ReadSFixed32();
				break;
			case 37u:
				UseHero = input.ReadSFixed32();
				break;
			case 45u:
				AdornCount = input.ReadSFixed32();
				break;
			case 53u:
				SkinCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
