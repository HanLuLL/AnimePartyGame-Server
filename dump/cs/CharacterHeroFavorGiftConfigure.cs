using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class CharacterHeroFavorGiftConfigure : IMessage<CharacterHeroFavorGiftConfigure>, IMessage, IEquatable<CharacterHeroFavorGiftConfigure>, IDeepCloneable<CharacterHeroFavorGiftConfigure>, IBufferMessage
{
	private static readonly MessageParser<CharacterHeroFavorGiftConfigure> _parser = new MessageParser<CharacterHeroFavorGiftConfigure>(() => new CharacterHeroFavorGiftConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int CharacterHeroFavorGiftConfigureItemsFieldNumber = 2;

	private static readonly FieldCodec<CharacterHeroFavorGiftConfigureItem> _repeated_characterHeroFavorGiftConfigureItems_codec = FieldCodec.ForMessage(18u, CharacterHeroFavorGiftConfigureItem.Parser);

	private readonly RepeatedField<CharacterHeroFavorGiftConfigureItem> characterHeroFavorGiftConfigureItems_ = new RepeatedField<CharacterHeroFavorGiftConfigureItem>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CharacterHeroFavorGiftConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CharacterReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CharacterHeroFavorGiftConfigureItem> CharacterHeroFavorGiftConfigureItems => characterHeroFavorGiftConfigureItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterHeroFavorGiftConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterHeroFavorGiftConfigure(CharacterHeroFavorGiftConfigure other)
		: this()
	{
		id_ = other.id_;
		characterHeroFavorGiftConfigureItems_ = other.characterHeroFavorGiftConfigureItems_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CharacterHeroFavorGiftConfigure Clone()
	{
		return new CharacterHeroFavorGiftConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CharacterHeroFavorGiftConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CharacterHeroFavorGiftConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (!characterHeroFavorGiftConfigureItems_.Equals(other.characterHeroFavorGiftConfigureItems_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		num ^= characterHeroFavorGiftConfigureItems_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		characterHeroFavorGiftConfigureItems_.WriteTo(ref output, _repeated_characterHeroFavorGiftConfigureItems_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		num += characterHeroFavorGiftConfigureItems_.CalculateSize(_repeated_characterHeroFavorGiftConfigureItems_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CharacterHeroFavorGiftConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			characterHeroFavorGiftConfigureItems_.Add(other.characterHeroFavorGiftConfigureItems_);
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
				Id = input.ReadSFixed32();
				break;
			case 18u:
				characterHeroFavorGiftConfigureItems_.AddEntriesFrom(ref input, _repeated_characterHeroFavorGiftConfigureItems_codec);
				break;
			}
		}
	}
}
