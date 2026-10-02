using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using UnityEngine;

public sealed class GuideTutorialConfigure : IMessage<GuideTutorialConfigure>, IMessage, IEquatable<GuideTutorialConfigure>, IDeepCloneable<GuideTutorialConfigure>, IBufferMessage
{
	private static readonly MessageParser<GuideTutorialConfigure> _parser = new MessageParser<GuideTutorialConfigure>(() => new GuideTutorialConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PCTutorialIdFieldNumber = 2;

	private int pCTutorialId_;

	public const int MobileTutorialIdFieldNumber = 3;

	private int mobileTutorialId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GuideTutorialConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GuideReflection.Descriptor.MessageTypes[3];

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
	public int PCTutorialId
	{
		get
		{
			return pCTutorialId_;
		}
		private set
		{
			pCTutorialId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MobileTutorialId
	{
		get
		{
			return mobileTutorialId_;
		}
		private set
		{
			mobileTutorialId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideTutorialConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideTutorialConfigure(GuideTutorialConfigure other)
		: this()
	{
		id_ = other.id_;
		pCTutorialId_ = other.pCTutorialId_;
		mobileTutorialId_ = other.mobileTutorialId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuideTutorialConfigure Clone()
	{
		return new GuideTutorialConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GuideTutorialConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GuideTutorialConfigure other)
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
		if (PCTutorialId != other.PCTutorialId)
		{
			return false;
		}
		if (MobileTutorialId != other.MobileTutorialId)
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
		if (PCTutorialId != 0)
		{
			num ^= PCTutorialId.GetHashCode();
		}
		if (MobileTutorialId != 0)
		{
			num ^= MobileTutorialId.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (PCTutorialId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(PCTutorialId);
		}
		if (MobileTutorialId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MobileTutorialId);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (PCTutorialId != 0)
		{
			num += 5;
		}
		if (MobileTutorialId != 0)
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
	public void MergeFrom(GuideTutorialConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PCTutorialId != 0)
			{
				PCTutorialId = other.PCTutorialId;
			}
			if (other.MobileTutorialId != 0)
			{
				MobileTutorialId = other.MobileTutorialId;
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				PCTutorialId = input.ReadSFixed32();
				break;
			case 29u:
				MobileTutorialId = input.ReadSFixed32();
				break;
			}
		}
	}

	public RepeatedField<TutorialinfoConfigureItem> GetPCTutorial()
	{
		if (!StaticConfigure.Tutorial.InfoDict.TryGetValue(PCTutorialId, out var value))
		{
			Debug.LogError("在Tutorial.InfoDict表里并没有找到PC教程id：" + PCTutorialId);
			return null;
		}
		return value.TutorialinfoConfigureItems;
	}

	public RepeatedField<TutorialinfoConfigureItem> GetMobileTutorial()
	{
		if (!StaticConfigure.Tutorial.InfoDict.TryGetValue(MobileTutorialId, out var value))
		{
			Debug.LogError("在Tutorial.InfoDict表里并没有找到Mobile教程id：" + MobileTutorialId);
			return null;
		}
		return value.TutorialinfoConfigureItems;
	}
}
