using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SportsMeetInfo : IMessage<SportsMeetInfo>, IMessage, IEquatable<SportsMeetInfo>, IDeepCloneable<SportsMeetInfo>, IBufferMessage
{
	private static readonly MessageParser<SportsMeetInfo> _parser = new MessageParser<SportsMeetInfo>(() => new SportsMeetInfo());

	private UnknownFieldSet _unknownFields;

	public const int ChallengeDataFieldNumber = 1;

	private static readonly MapField<int, ChallengeData>.Codec _map_challengeData_codec = new MapField<int, ChallengeData>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.ChallengeData.Parser), 10u);

	private readonly MapField<int, ChallengeData> challengeData_ = new MapField<int, ChallengeData>();

	public const int PassMapIdsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_passMapIds_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> passMapIds_ = new RepeatedField<int>();

	public const int IsKnockoutMatchFieldNumber = 3;

	private bool isKnockoutMatch_;

	public const int IsFinalMatchFieldNumber = 4;

	private bool isFinalMatch_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SportsMeetInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, ChallengeData> ChallengeData => challengeData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PassMapIds => passMapIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsKnockoutMatch
	{
		get
		{
			return isKnockoutMatch_;
		}
		set
		{
			isKnockoutMatch_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsFinalMatch
	{
		get
		{
			return isFinalMatch_;
		}
		set
		{
			isFinalMatch_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SportsMeetInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SportsMeetInfo(SportsMeetInfo other)
		: this()
	{
		challengeData_ = other.challengeData_.Clone();
		passMapIds_ = other.passMapIds_.Clone();
		isKnockoutMatch_ = other.isKnockoutMatch_;
		isFinalMatch_ = other.isFinalMatch_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SportsMeetInfo Clone()
	{
		return new SportsMeetInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SportsMeetInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SportsMeetInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!ChallengeData.Equals(other.ChallengeData))
		{
			return false;
		}
		if (!passMapIds_.Equals(other.passMapIds_))
		{
			return false;
		}
		if (IsKnockoutMatch != other.IsKnockoutMatch)
		{
			return false;
		}
		if (IsFinalMatch != other.IsFinalMatch)
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
		num ^= ChallengeData.GetHashCode();
		num ^= passMapIds_.GetHashCode();
		if (IsKnockoutMatch)
		{
			num ^= IsKnockoutMatch.GetHashCode();
		}
		if (IsFinalMatch)
		{
			num ^= IsFinalMatch.GetHashCode();
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
		challengeData_.WriteTo(ref output, _map_challengeData_codec);
		passMapIds_.WriteTo(ref output, _repeated_passMapIds_codec);
		if (IsKnockoutMatch)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsKnockoutMatch);
		}
		if (IsFinalMatch)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsFinalMatch);
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
		num += challengeData_.CalculateSize(_map_challengeData_codec);
		num += passMapIds_.CalculateSize(_repeated_passMapIds_codec);
		if (IsKnockoutMatch)
		{
			num += 2;
		}
		if (IsFinalMatch)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SportsMeetInfo other)
	{
		if (other != null)
		{
			challengeData_.MergeFrom(other.challengeData_);
			passMapIds_.Add(other.passMapIds_);
			if (other.IsKnockoutMatch)
			{
				IsKnockoutMatch = other.IsKnockoutMatch;
			}
			if (other.IsFinalMatch)
			{
				IsFinalMatch = other.IsFinalMatch;
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
				challengeData_.AddEntriesFrom(ref input, _map_challengeData_codec);
				break;
			case 18u:
			case 21u:
				passMapIds_.AddEntriesFrom(ref input, _repeated_passMapIds_codec);
				break;
			case 24u:
				IsKnockoutMatch = input.ReadBool();
				break;
			case 32u:
				IsFinalMatch = input.ReadBool();
				break;
			}
		}
	}
}
