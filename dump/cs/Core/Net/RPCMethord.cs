using System;

namespace Core.Net;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
public class RPCMethord : Attribute
{
	public RPCType rpcType;

	public ProtolcalType protoType = ProtolcalType.Background;

	public string FuncName = "null";

	public bool GenCallBack;

	public Type[] callBackParam;
}
