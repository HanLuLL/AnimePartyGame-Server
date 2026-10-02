namespace Tools;

public interface IEncryptor<T> where T : struct
{
	void EncryptSet(T value);

	T DecryptGet();
}
