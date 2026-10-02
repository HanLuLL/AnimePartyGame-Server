using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class GmUnlockRoleInfoS2C : IMessage<GmUnlockRoleInfoS2C>, IMessage, IEquatable<GmUnlockRoleInfoS2C>, IDeepCloneable<GmUnlockRoleInfoS2C>, IBufferMessage
{
	private static readonly MessageParser<GmUnlockRoleInfoS2C> _parser = new MessageParser<GmUnlockRoleInfoS2C>(() => new GmUnlockRoleInfoS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoleInfoFieldNumber = 1;

	private static readonly MapField<int, RoleCard>.Codec _map_roleInfo_codec = new MapField<int, RoleCard>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RoleCard.Parser), 10u);

	private readonly MapField<int, RoleCard> roleInfo_ = new MapField<int, RoleCard>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GmUnlockRoleInfoS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[188];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RoleCard> RoleInfo => roleInfo_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmUnlockRoleInfoS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmUnlockRoleInfoS2C(GmUnlockRoleInfoS2C other)
		: this()
	{
		roleInfo_ = other.roleInfo_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmUnlockRoleInfoS2C Clone()
	{
		return new GmUnlockRoleInfoS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GmUnlockRoleInfoS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GmUnlockRoleInfoS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!RoleInfo.Equals(other.RoleInfo))
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
		num ^= RoleInfo.GetHashCode();
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
		roleInfo_.WriteTo(ref output, _map_roleInfo_codec);
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
		num += roleInfo_.CalculateSize(_map_roleInfo_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GmUnlockRoleInfoS2C other)
	{
		if (other != null)
		{
			roleInfo_.MergeFrom(other.roleInfo_);
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
			if (num != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
			}
			else
			{
				roleInfo_.AddEntriesFrom(ref input, _map_roleInfo_codec);
			}
		}
	}
}
