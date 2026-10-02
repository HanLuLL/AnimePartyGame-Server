using System;

namespace Tools;

public class Int32Encryptor : IEncryptor<int>
{
	private const int defaultKey = 20260316;

	private const int shiftBits = 3;

	private readonly int key;

	private int encryptedNumber;

	public Int32Encryptor()
	{
		key = DateTime.Now.Millisecond * DateTime.Now.Minute + 20260316;
		EncryptSet(0);
	}

	public Int32Encryptor(int value)
	{
		key = DateTime.Now.Millisecond * DateTime.Now.Minute + 20260316;
		EncryptSet(value);
	}

	public int DecryptGet()
	{
		return ((encryptedNumber >>> 3) | (encryptedNumber << 29)) ^ key;
	}

	public void EncryptSet(int originalNumber)
	{
		int num = originalNumber ^ key;
		int num2 = (num << 3) | (num >>> 29);
		encryptedNumber = num2;
	}
}
