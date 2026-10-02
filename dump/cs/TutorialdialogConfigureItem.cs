using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class TutorialdialogConfigureItem : IMessage<TutorialdialogConfigureItem>, IMessage, IEquatable<TutorialdialogConfigureItem>, IDeepCloneable<TutorialdialogConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<TutorialdialogConfigureItem> _parser = new MessageParser<TutorialdialogConfigureItem>(() => new TutorialdialogConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int ExpressionFieldNumber = 2;

	private string expression_ = "";

	public const int ContentIdFieldNumber = 3;

	private int contentId_;

	public const int AudioIdFieldNumber = 4;

	private int audioId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TutorialdialogConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TutorialReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Expression
	{
		get
		{
			return expression_;
		}
		private set
		{
			expression_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ContentId
	{
		get
		{
			return contentId_;
		}
		private set
		{
			contentId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AudioId
	{
		get
		{
			return audioId_;
		}
		private set
		{
			audioId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialdialogConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialdialogConfigureItem(TutorialdialogConfigureItem other)
		: this()
	{
		index_ = other.index_;
		expression_ = other.expression_;
		contentId_ = other.contentId_;
		audioId_ = other.audioId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TutorialdialogConfigureItem Clone()
	{
		return new TutorialdialogConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TutorialdialogConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TutorialdialogConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (Expression != other.Expression)
		{
			return false;
		}
		if (ContentId != other.ContentId)
		{
			return false;
		}
		if (AudioId != other.AudioId)
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (Expression.Length != 0)
		{
			num ^= Expression.GetHashCode();
		}
		if (ContentId != 0)
		{
			num ^= ContentId.GetHashCode();
		}
		if (AudioId != 0)
		{
			num ^= AudioId.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (Expression.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Expression);
		}
		if (ContentId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ContentId);
		}
		if (AudioId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(AudioId);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (Expression.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Expression);
		}
		if (ContentId != 0)
		{
			num += 5;
		}
		if (AudioId != 0)
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
	public void MergeFrom(TutorialdialogConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.Expression.Length != 0)
			{
				Expression = other.Expression;
			}
			if (other.ContentId != 0)
			{
				ContentId = other.ContentId;
			}
			if (other.AudioId != 0)
			{
				AudioId = other.AudioId;
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
				Index = input.ReadSFixed32();
				break;
			case 18u:
				Expression = input.ReadString();
				break;
			case 29u:
				ContentId = input.ReadSFixed32();
				break;
			case 37u:
				AudioId = input.ReadSFixed32();
				break;
			}
		}
	}
}
