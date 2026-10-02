namespace Core.Net;

public interface Protocal
{
	Frame TranslateFrame(ByteBuf head, ByteBuf payload);

	Frame TranslateFrame(ByteBuf src);

	int HeaderLen();
}
