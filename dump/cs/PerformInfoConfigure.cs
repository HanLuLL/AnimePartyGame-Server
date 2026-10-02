using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PerformInfoConfigure : IMessage<PerformInfoConfigure>, IMessage, IEquatable<PerformInfoConfigure>, IDeepCloneable<PerformInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<PerformInfoConfigure> _parser = new MessageParser<PerformInfoConfigure>(() => new PerformInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int TotalTimeFieldNumber = 2;

	private int totalTime_;

	public const int IsFollowFieldNumber = 3;

	private bool isFollow_;

	public const int IsAlternatelyFieldNumber = 4;

	private bool isAlternately_;

	public const int SwitchTimeFieldNumber = 5;

	private int switchTime_;

	public const int CustomShowsFieldNumber = 6;

	private static readonly MapField<string, int>.Codec _map_customShows_codec = new MapField<string, int>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<string, int> customShows_ = new MapField<string, int>();

	public const int AnimationsFieldNumber = 7;

	private static readonly MapField<string, int>.Codec _map_animations_codec = new MapField<string, int>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForSFixed32(21u, 0), 58u);

	private readonly MapField<string, int> animations_ = new MapField<string, int>();

	public const int UpdateAttributeFieldNumber = 8;

	private int updateAttribute_;

	public const int EffectsFieldNumber = 9;

	private static readonly MapField<int, int>.Codec _map_effects_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 74u);

	private readonly MapField<int, int> effects_ = new MapField<int, int>();

	public const int ScreenPumpFieldNumber = 10;

	private static readonly MapField<string, int>.Codec _map_screenPump_codec = new MapField<string, int>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<string, int> screenPump_ = new MapField<string, int>();

	public const int AudioFieldNumber = 11;

	private static readonly MapField<int, int>.Codec _map_audio_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 90u);

	private readonly MapField<int, int> audio_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PerformInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PerformReflection.Descriptor.MessageTypes[0];

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
	public int TotalTime
	{
		get
		{
			return totalTime_;
		}
		private set
		{
			totalTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsFollow
	{
		get
		{
			return isFollow_;
		}
		private set
		{
			isFollow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAlternately
	{
		get
		{
			return isAlternately_;
		}
		private set
		{
			isAlternately_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SwitchTime
	{
		get
		{
			return switchTime_;
		}
		private set
		{
			switchTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, int> CustomShows => customShows_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, int> Animations => animations_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpdateAttribute
	{
		get
		{
			return updateAttribute_;
		}
		private set
		{
			updateAttribute_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Effects => effects_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, int> ScreenPump => screenPump_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Audio => audio_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformInfoConfigure(PerformInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		totalTime_ = other.totalTime_;
		isFollow_ = other.isFollow_;
		isAlternately_ = other.isAlternately_;
		switchTime_ = other.switchTime_;
		customShows_ = other.customShows_.Clone();
		animations_ = other.animations_.Clone();
		updateAttribute_ = other.updateAttribute_;
		effects_ = other.effects_.Clone();
		screenPump_ = other.screenPump_.Clone();
		audio_ = other.audio_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PerformInfoConfigure Clone()
	{
		return new PerformInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PerformInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PerformInfoConfigure other)
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
		if (TotalTime != other.TotalTime)
		{
			return false;
		}
		if (IsFollow != other.IsFollow)
		{
			return false;
		}
		if (IsAlternately != other.IsAlternately)
		{
			return false;
		}
		if (SwitchTime != other.SwitchTime)
		{
			return false;
		}
		if (!CustomShows.Equals(other.CustomShows))
		{
			return false;
		}
		if (!Animations.Equals(other.Animations))
		{
			return false;
		}
		if (UpdateAttribute != other.UpdateAttribute)
		{
			return false;
		}
		if (!Effects.Equals(other.Effects))
		{
			return false;
		}
		if (!ScreenPump.Equals(other.ScreenPump))
		{
			return false;
		}
		if (!Audio.Equals(other.Audio))
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
		if (TotalTime != 0)
		{
			num ^= TotalTime.GetHashCode();
		}
		if (IsFollow)
		{
			num ^= IsFollow.GetHashCode();
		}
		if (IsAlternately)
		{
			num ^= IsAlternately.GetHashCode();
		}
		if (SwitchTime != 0)
		{
			num ^= SwitchTime.GetHashCode();
		}
		num ^= CustomShows.GetHashCode();
		num ^= Animations.GetHashCode();
		if (UpdateAttribute != 0)
		{
			num ^= UpdateAttribute.GetHashCode();
		}
		num ^= Effects.GetHashCode();
		num ^= ScreenPump.GetHashCode();
		num ^= Audio.GetHashCode();
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
		if (TotalTime != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(TotalTime);
		}
		if (IsFollow)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsFollow);
		}
		if (IsAlternately)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsAlternately);
		}
		if (SwitchTime != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(SwitchTime);
		}
		customShows_.WriteTo(ref output, _map_customShows_codec);
		animations_.WriteTo(ref output, _map_animations_codec);
		if (UpdateAttribute != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(UpdateAttribute);
		}
		effects_.WriteTo(ref output, _map_effects_codec);
		screenPump_.WriteTo(ref output, _map_screenPump_codec);
		audio_.WriteTo(ref output, _map_audio_codec);
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
		if (TotalTime != 0)
		{
			num += 5;
		}
		if (IsFollow)
		{
			num += 2;
		}
		if (IsAlternately)
		{
			num += 2;
		}
		if (SwitchTime != 0)
		{
			num += 5;
		}
		num += customShows_.CalculateSize(_map_customShows_codec);
		num += animations_.CalculateSize(_map_animations_codec);
		if (UpdateAttribute != 0)
		{
			num += 5;
		}
		num += effects_.CalculateSize(_map_effects_codec);
		num += screenPump_.CalculateSize(_map_screenPump_codec);
		num += audio_.CalculateSize(_map_audio_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PerformInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.TotalTime != 0)
			{
				TotalTime = other.TotalTime;
			}
			if (other.IsFollow)
			{
				IsFollow = other.IsFollow;
			}
			if (other.IsAlternately)
			{
				IsAlternately = other.IsAlternately;
			}
			if (other.SwitchTime != 0)
			{
				SwitchTime = other.SwitchTime;
			}
			customShows_.MergeFrom(other.customShows_);
			animations_.MergeFrom(other.animations_);
			if (other.UpdateAttribute != 0)
			{
				UpdateAttribute = other.UpdateAttribute;
			}
			effects_.MergeFrom(other.effects_);
			screenPump_.MergeFrom(other.screenPump_);
			audio_.MergeFrom(other.audio_);
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
				TotalTime = input.ReadSFixed32();
				break;
			case 24u:
				IsFollow = input.ReadBool();
				break;
			case 32u:
				IsAlternately = input.ReadBool();
				break;
			case 45u:
				SwitchTime = input.ReadSFixed32();
				break;
			case 50u:
				customShows_.AddEntriesFrom(ref input, _map_customShows_codec);
				break;
			case 58u:
				animations_.AddEntriesFrom(ref input, _map_animations_codec);
				break;
			case 69u:
				UpdateAttribute = input.ReadSFixed32();
				break;
			case 74u:
				effects_.AddEntriesFrom(ref input, _map_effects_codec);
				break;
			case 82u:
				screenPump_.AddEntriesFrom(ref input, _map_screenPump_codec);
				break;
			case 90u:
				audio_.AddEntriesFrom(ref input, _map_audio_codec);
				break;
			}
		}
	}
}
