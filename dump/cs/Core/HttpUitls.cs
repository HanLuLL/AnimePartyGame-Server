using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace Core;

internal class HttpUitls
{
	public static string Get(string Url)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
		httpWebRequest.Proxy = null;
		httpWebRequest.KeepAlive = false;
		httpWebRequest.Method = "GET";
		httpWebRequest.ContentType = "application/json; charset=UTF-8";
		httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		Stream responseStream = httpWebResponse.GetResponseStream();
		StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		responseStream.Close();
		httpWebResponse?.Close();
		httpWebRequest?.Abort();
		return result;
	}

	public static string DoPost(string url, Hashtable paramsOfUrl)
	{
		if (url == null)
		{
			throw new Exception("WebService地址为空");
		}
		_ = (HttpWebRequest)WebRequest.Create(url);
		byte[] jointBOfParams = GetJointBOfParams(paramsOfUrl);
		HttpWebRequest obj = (HttpWebRequest)WebRequest.Create(url);
		obj.Method = "POST";
		obj.ContentType = "application/x-www-form-urlencoded";
		obj.ContentLength = jointBOfParams.Length;
		Stream requestStream = obj.GetRequestStream();
		requestStream.Write(jointBOfParams, 0, jointBOfParams.Length);
		requestStream.Close();
		StreamReader streamReader = new StreamReader(((HttpWebResponse)obj.GetResponse()).GetResponseStream(), Encoding.UTF8);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	private static string GetJointSOfParams(Hashtable paramsOfUrl)
	{
		if (paramsOfUrl == null || paramsOfUrl.Count == 0)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		foreach (DictionaryEntry item in paramsOfUrl)
		{
			string text = ToHttpChar(item.Value.ToString());
			if (num == 0)
			{
				stringBuilder.Append(item.Key?.ToString() + "=" + text);
			}
			else
			{
				stringBuilder.Append("&" + item.Key?.ToString() + "=" + text);
			}
			num++;
		}
		return stringBuilder.ToString();
	}

	private static byte[] GetJointBOfParams(Hashtable paramsOfUrl)
	{
		string jointSOfParams = GetJointSOfParams(paramsOfUrl);
		return new ASCIIEncoding().GetBytes(jointSOfParams);
	}

	private static string ToHttpChar(string value)
	{
		value = value.ToString().Replace("+", "%2B");
		return value;
	}

	public static string HttpApi(string url, string jsonstr, string type)
	{
		Encoding uTF = Encoding.UTF8;
		HttpWebRequest obj = (HttpWebRequest)WebRequest.Create(url);
		obj.Accept = "text/html,application/xhtml+xml,*/*";
		obj.ContentType = "application/json";
		obj.Method = type.ToUpper().ToString();
		byte[] bytes = uTF.GetBytes(jsonstr);
		obj.ContentLength = bytes.Length;
		obj.GetRequestStream().Write(bytes, 0, bytes.Length);
		using StreamReader streamReader = new StreamReader(((HttpWebResponse)obj.GetResponse()).GetResponseStream(), Encoding.UTF8);
		return streamReader.ReadToEnd();
	}
}
