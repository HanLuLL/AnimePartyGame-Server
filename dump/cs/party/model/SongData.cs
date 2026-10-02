using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SongData : IMessage<SongData>, IMessage, IEquatable<SongData>, IDeepCloneable<SongData>, IBufferMessage
{
	private static readonly MessageParser<SongData> _parser = new MessageParser<SongData>(() => new SongData());

	private UnknownFieldSet _unknownFields;

	public const int EasyTopScoreFieldNumber = 1;

	private int easyTopScore_;

	public const int EasyCompleteStateFieldNumber = 2;

	private SongCompleteState easyCompleteState_;

	public const int HardTopScoreFieldNumber = 3;

	private int hardTopScore_;

	public const int HardCompleteStateFieldNumber = 4;

	private SongCompleteState hardCompleteState_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SongData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[81];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EasyTopScore
	{
		get
		{
			return easyTopScore_;
		}
		set
		{
			easyTopScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SongCompleteState EasyCompleteState
	{
		get
		{
			return easyCompleteState_;
		}
		set
		{
			easyCompleteState_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HardTopScore
	{
		get
		{
			return hardTopScore_;
		}
		set
		{
			hardTopScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SongCompleteState HardCompleteState
	{
		get
		{
			return hardCompleteState_;
		}
		set
		{
			hardCompleteState_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SongData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SongData(SongData other)
		: this()
	{
		easyTopScore_ = other.easyTopScore_;
		easyCompleteState_ = other.easyCompleteState_;
		hardTopScore_ = other.hardTopScore_;
		hardCompleteState_ = other.hardCompleteState_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SongData Clone()
	{
		return new SongData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SongData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SongData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (EasyTopScore != other.EasyTopScore)
		{
			return false;
		}
		if (EasyCompleteState != other.EasyCompleteState)
		{
			return false;
		}
		if (HardTopScore != other.HardTopScore)
		{
			return false;
		}
		if (HardCompleteState != other.HardCompleteState)
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
		if (EasyTopScore != 0)
		{
			num ^= EasyTopScore.GetHashCode();
		}
		if (EasyCompleteState != SongCompleteState.NoneComplete)
		{
			num ^= EasyCompleteState.GetHashCode();
		}
		if (HardTopScore != 0)
		{
			num ^= HardTopScore.GetHashCode();
		}
		if (HardCompleteState != SongCompleteState.NoneComplete)
		{
			num ^= HardCompleteState.GetHashCode();
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
		if (EasyTopScore != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(EasyTopScore);
		}
		if (EasyCompleteState != SongCompleteState.NoneComplete)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)EasyCompleteState);
		}
		if (HardTopScore != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(HardTopScore);
		}
		if (HardCompleteState != SongCompleteState.NoneComplete)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)HardCompleteState);
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
		if (EasyTopScore != 0)
		{
			num += 5;
		}
		if (EasyCompleteState != SongCompleteState.NoneComplete)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)EasyCompleteState);
		}
		if (HardTopScore != 0)
		{
			num += 5;
		}
		if (HardCompleteState != SongCompleteState.NoneComplete)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)HardCompleteState);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SongData other)
	{
		if (other != null)
		{
			if (other.EasyTopScore != 0)
			{
				EasyTopScore = other.EasyTopScore;
			}
			if (other.EasyCompleteState != SongCompleteState.NoneComplete)
			{
				EasyCompleteState = other.EasyCompleteState;
			}
			if (other.HardTopScore != 0)
			{
				HardTopScore = other.HardTopScore;
			}
			if (other.HardCompleteState != SongCompleteState.NoneComplete)
			{
				HardCompleteState = other.HardCompleteState;
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
				EasyTopScore = input.ReadSFixed32();
				break;
			case 16u:
				EasyCompleteState = (SongCompleteState)input.ReadEnum();
				break;
			case 29u:
				HardTopScore = input.ReadSFixed32();
				break;
			case 32u:
				HardCompleteState = (SongCompleteState)input.ReadEnum();
				break;
			}
		}
	}
}
