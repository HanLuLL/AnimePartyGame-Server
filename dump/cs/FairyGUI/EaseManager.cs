using System;

namespace FairyGUI;

public static class EaseManager
{
	private static class Bounce
	{
		public static float EaseIn(float time, float duration)
		{
			return 1f - EaseOut(duration - time, duration);
		}

		public static float EaseOut(float time, float duration)
		{
			if ((time /= duration) < 0.36363637f)
			{
				return 7.5625f * time * time;
			}
			if (time < 0.72727275f)
			{
				return 7.5625f * (time -= 0.54545456f) * time + 0.75f;
			}
			if (time < 0.90909094f)
			{
				return 7.5625f * (time -= 0.8181818f) * time + 0.9375f;
			}
			return 7.5625f * (time -= 21f / 22f) * time + 63f / 64f;
		}

		public static float EaseInOut(float time, float duration)
		{
			if (time < duration * 0.5f)
			{
				return EaseIn(time * 2f, duration) * 0.5f;
			}
			return EaseOut(time * 2f - duration, duration) * 0.5f + 0.5f;
		}
	}

	private const float _PiOver2 = (float)Math.PI / 2f;

	private const float _TwoPi = (float)Math.PI * 2f;

	public static float Evaluate(EaseType easeType, float time, float duration, float overshootOrAmplitude = 1.70158f, float period = 0f, CustomEase customEase = null)
	{
		if (duration <= 0f)
		{
			return 1f;
		}
		switch (easeType)
		{
		case EaseType.Linear:
			return time / duration;
		case EaseType.SineIn:
			return 0f - (float)Math.Cos(time / duration * ((float)Math.PI / 2f)) + 1f;
		case EaseType.SineOut:
			return (float)Math.Sin(time / duration * ((float)Math.PI / 2f));
		case EaseType.SineInOut:
			return -0.5f * ((float)Math.Cos((float)Math.PI * time / duration) - 1f);
		case EaseType.QuadIn:
			return (time /= duration) * time;
		case EaseType.QuadOut:
			return (0f - (time /= duration)) * (time - 2f);
		case EaseType.QuadInOut:
			if ((time /= duration * 0.5f) < 1f)
			{
				return 0.5f * time * time;
			}
			return -0.5f * ((time -= 1f) * (time - 2f) - 1f);
		case EaseType.CubicIn:
			return (time /= duration) * time * time;
		case EaseType.CubicOut:
			return (time = time / duration - 1f) * time * time + 1f;
		case EaseType.CubicInOut:
			if ((time /= duration * 0.5f) < 1f)
			{
				return 0.5f * time * time * time;
			}
			return 0.5f * ((time -= 2f) * time * time + 2f);
		case EaseType.QuartIn:
			return (time /= duration) * time * time * time;
		case EaseType.QuartOut:
			return 0f - ((time = time / duration - 1f) * time * time * time - 1f);
		case EaseType.QuartInOut:
			if ((time /= duration * 0.5f) < 1f)
			{
				return 0.5f * time * time * time * time;
			}
			return -0.5f * ((time -= 2f) * time * time * time - 2f);
		case EaseType.QuintIn:
			return (time /= duration) * time * time * time * time;
		case EaseType.QuintOut:
			return (time = time / duration - 1f) * time * time * time * time + 1f;
		case EaseType.QuintInOut:
			if ((time /= duration * 0.5f) < 1f)
			{
				return 0.5f * time * time * time * time * time;
			}
			return 0.5f * ((time -= 2f) * time * time * time * time + 2f);
		case EaseType.ExpoIn:
			if (time != 0f)
			{
				return (float)Math.Pow(2.0, 10f * (time / duration - 1f));
			}
			return 0f;
		case EaseType.ExpoOut:
			if (time == duration)
			{
				return 1f;
			}
			return 0f - (float)Math.Pow(2.0, -10f * time / duration) + 1f;
		case EaseType.ExpoInOut:
			if (time == 0f)
			{
				return 0f;
			}
			if (time == duration)
			{
				return 1f;
			}
			if ((time /= duration * 0.5f) < 1f)
			{
				return 0.5f * (float)Math.Pow(2.0, 10f * (time - 1f));
			}
			return 0.5f * (0f - (float)Math.Pow(2.0, -10f * (time -= 1f)) + 2f);
		case EaseType.CircIn:
			return 0f - ((float)Math.Sqrt(1f - (time /= duration) * time) - 1f);
		case EaseType.CircOut:
			return (float)Math.Sqrt(1f - (time = time / duration - 1f) * time);
		case EaseType.CircInOut:
			if ((time /= duration * 0.5f) < 1f)
			{
				return -0.5f * ((float)Math.Sqrt(1f - time * time) - 1f);
			}
			return 0.5f * ((float)Math.Sqrt(1f - (time -= 2f) * time) + 1f);
		case EaseType.ElasticIn:
		{
			if (time == 0f)
			{
				return 0f;
			}
			if ((time /= duration) == 1f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.3f;
			}
			float num;
			if (overshootOrAmplitude < 1f)
			{
				overshootOrAmplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = period / ((float)Math.PI * 2f) * (float)Math.Asin(1f / overshootOrAmplitude);
			}
			return 0f - overshootOrAmplitude * (float)Math.Pow(2.0, 10f * (time -= 1f)) * (float)Math.Sin((time * duration - num) * ((float)Math.PI * 2f) / period);
		}
		case EaseType.ElasticOut:
		{
			if (time == 0f)
			{
				return 0f;
			}
			if ((time /= duration) == 1f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.3f;
			}
			float num3;
			if (overshootOrAmplitude < 1f)
			{
				overshootOrAmplitude = 1f;
				num3 = period / 4f;
			}
			else
			{
				num3 = period / ((float)Math.PI * 2f) * (float)Math.Asin(1f / overshootOrAmplitude);
			}
			return overshootOrAmplitude * (float)Math.Pow(2.0, -10f * time) * (float)Math.Sin((time * duration - num3) * ((float)Math.PI * 2f) / period) + 1f;
		}
		case EaseType.ElasticInOut:
		{
			if (time == 0f)
			{
				return 0f;
			}
			if ((time /= duration * 0.5f) == 2f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.45000002f;
			}
			float num2;
			if (overshootOrAmplitude < 1f)
			{
				overshootOrAmplitude = 1f;
				num2 = period / 4f;
			}
			else
			{
				num2 = period / ((float)Math.PI * 2f) * (float)Math.Asin(1f / overshootOrAmplitude);
			}
			if (time < 1f)
			{
				return -0.5f * (overshootOrAmplitude * (float)Math.Pow(2.0, 10f * (time -= 1f)) * (float)Math.Sin((time * duration - num2) * ((float)Math.PI * 2f) / period));
			}
			return overshootOrAmplitude * (float)Math.Pow(2.0, -10f * (time -= 1f)) * (float)Math.Sin((time * duration - num2) * ((float)Math.PI * 2f) / period) * 0.5f + 1f;
		}
		case EaseType.BackIn:
			return (time /= duration) * time * ((overshootOrAmplitude + 1f) * time - overshootOrAmplitude);
		case EaseType.BackOut:
			return (time = time / duration - 1f) * time * ((overshootOrAmplitude + 1f) * time + overshootOrAmplitude) + 1f;
		case EaseType.BackInOut:
			if ((time /= duration * 0.5f) < 1f)
			{
				return 0.5f * (time * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time - overshootOrAmplitude));
			}
			return 0.5f * ((time -= 2f) * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time + overshootOrAmplitude) + 2f);
		case EaseType.BounceIn:
			return Bounce.EaseIn(time, duration);
		case EaseType.BounceOut:
			return Bounce.EaseOut(time, duration);
		case EaseType.BounceInOut:
			return Bounce.EaseInOut(time, duration);
		case EaseType.Custom:
			return customEase?.Evaluate(time / duration) ?? (time / duration);
		default:
			return (0f - (time /= duration)) * (time - 2f);
		}
	}
}
