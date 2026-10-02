using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class GuideInfoConfigureItem : IMessage<GuideInfoConfigureItem>, IMessage, IEquatable<GuideInfoConfigureItem>, IDeepCloneable<GuideInfoConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<GuideInfoConfigureItem> _parser = new MessageParser<GuideInfoConfigureItem>(() => new GuideInfoConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int StepIdFieldNumber = 1;

	private int stepId_;

	public const int DialogIdFieldNumber = 2;

	private int dialogId_;

	public const int TutorialIdFieldNumber = 3;

	private int tutorialId_;

	public const int IsEndFieldNumber = 4;

	private bool isEnd_;

	private GuideDialogConfigure _DialogConfigure;

	private GuideTutorialConfigure _TutorialConfigure;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuideInfoConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuideReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StepId
	{
		get
		{
			return stepId_;
		}
		private set
		{
			stepId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DialogId
	{
		get
		{
			return dialogId_;
		}
		private set
		{
			dialogId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TutorialId
	{
		get
		{
			return tutorialId_;
		}
		private set
		{
			tutorialId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsEnd
	{
		get
		{
			return isEnd_;
		}
		private set
		{
			isEnd_ = value;
		}
	}

	public GuideDialogConfigure DialogConfigure
	{
		get
		{
			if (DialogId == 0)
			{
				return null;
			}
			if (_DialogConfigure == null && !StaticConfigure.Guide.DialogDict.TryGetValue(DialogId, out _DialogConfigure))
			{
				Debug.LogError("在Guide.DialogDict表里并没有找到对话id：" + DialogId);
				return null;
			}
			return _DialogConfigure;
		}
	}

	public GuideTutorialConfigure TutorialConfigure
	{
		get
		{
			if (TutorialId == 0)
			{
				return null;
			}
			if (_TutorialConfigure == null && !StaticConfigure.Guide.TutorialDict.TryGetValue(TutorialId, out _TutorialConfigure))
			{
				Debug.LogError("在Guide.TutorialDict表里并没有找到教程id：" + DialogId);
				return null;
			}
			return _TutorialConfigure;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideInfoConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideInfoConfigureItem(GuideInfoConfigureItem other)
		: this()
	{
		stepId_ = other.stepId_;
		dialogId_ = other.dialogId_;
		tutorialId_ = other.tutorialId_;
		isEnd_ = other.isEnd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideInfoConfigureItem Clone()
	{
		return new GuideInfoConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuideInfoConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuideInfoConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (StepId != other.StepId)
		{
			return false;
		}
		if (DialogId != other.DialogId)
		{
			return false;
		}
		if (TutorialId != other.TutorialId)
		{
			return false;
		}
		if (IsEnd != other.IsEnd)
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
		if (StepId != 0)
		{
			num ^= StepId.GetHashCode();
		}
		if (DialogId != 0)
		{
			num ^= DialogId.GetHashCode();
		}
		if (TutorialId != 0)
		{
			num ^= TutorialId.GetHashCode();
		}
		if (IsEnd)
		{
			num ^= IsEnd.GetHashCode();
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
		if (StepId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(StepId);
		}
		if (DialogId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(DialogId);
		}
		if (TutorialId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(TutorialId);
		}
		if (IsEnd)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsEnd);
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
		if (StepId != 0)
		{
			num += 5;
		}
		if (DialogId != 0)
		{
			num += 5;
		}
		if (TutorialId != 0)
		{
			num += 5;
		}
		if (IsEnd)
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
	public void MergeFrom(GuideInfoConfigureItem other)
	{
		if (other != null)
		{
			if (other.StepId != 0)
			{
				StepId = other.StepId;
			}
			if (other.DialogId != 0)
			{
				DialogId = other.DialogId;
			}
			if (other.TutorialId != 0)
			{
				TutorialId = other.TutorialId;
			}
			if (other.IsEnd)
			{
				IsEnd = other.IsEnd;
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
				StepId = input.ReadSFixed32();
				break;
			case 21u:
				DialogId = input.ReadSFixed32();
				break;
			case 29u:
				TutorialId = input.ReadSFixed32();
				break;
			case 32u:
				IsEnd = input.ReadBool();
				break;
			}
		}
	}
}
