using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class CategoryMissions : IMessage<CategoryMissions>, IMessage, IEquatable<CategoryMissions>, IDeepCloneable<CategoryMissions>, IBufferMessage
{
	private static readonly MessageParser<CategoryMissions> _parser = new MessageParser<CategoryMissions>(() => new CategoryMissions());

	private UnknownFieldSet _unknownFields;

	public const int CategoryFieldNumber = 1;

	private int category_;

	public const int MissionsFieldNumber = 2;

	private static readonly FieldCodec<Mission> _repeated_missions_codec = FieldCodec.ForMessage(18u, Mission.Parser);

	private readonly RepeatedField<Mission> missions_ = new RepeatedField<Mission>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CategoryMissions> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[536];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Category
	{
		get
		{
			return category_;
		}
		set
		{
			category_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<Mission> Missions => missions_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CategoryMissions()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CategoryMissions(CategoryMissions other)
		: this()
	{
		category_ = other.category_;
		missions_ = other.missions_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CategoryMissions Clone()
	{
		return new CategoryMissions(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CategoryMissions);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CategoryMissions other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Category != other.Category)
		{
			return false;
		}
		if (!missions_.Equals(other.missions_))
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
		if (Category != 0)
		{
			num ^= Category.GetHashCode();
		}
		num ^= missions_.GetHashCode();
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
		if (Category != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Category);
		}
		missions_.WriteTo(ref output, _repeated_missions_codec);
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
		if (Category != 0)
		{
			num += 5;
		}
		num += missions_.CalculateSize(_repeated_missions_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CategoryMissions other)
	{
		if (other != null)
		{
			if (other.Category != 0)
			{
				Category = other.Category;
			}
			missions_.Add(other.missions_);
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
				Category = input.ReadSFixed32();
				break;
			case 18u:
				missions_.AddEntriesFrom(ref input, _repeated_missions_codec);
				break;
			}
		}
	}
}
