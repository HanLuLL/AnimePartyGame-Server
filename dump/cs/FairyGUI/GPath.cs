using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class GPath
{
	protected struct Segment
	{
		public GPathPoint.CurveType type;

		public float length;

		public int ptStart;

		public int ptCount;
	}

	protected List<Segment> _segments;

	protected List<Vector3> _points;

	protected float _fullLength;

	private static List<GPathPoint> helperList = new List<GPathPoint>();

	private static List<Vector3> splinePoints = new List<Vector3>();

	public float length => _fullLength;

	public int segmentCount => _segments.Count;

	public GPath()
	{
		_segments = new List<Segment>();
		_points = new List<Vector3>();
	}

	public void Create(GPathPoint pt1, GPathPoint pt2)
	{
		helperList.Clear();
		helperList.Add(pt1);
		helperList.Add(pt2);
		Create(helperList);
	}

	public void Create(GPathPoint pt1, GPathPoint pt2, GPathPoint pt3)
	{
		helperList.Clear();
		helperList.Add(pt1);
		helperList.Add(pt2);
		helperList.Add(pt3);
		Create(helperList);
	}

	public void Create(GPathPoint pt1, GPathPoint pt2, GPathPoint pt3, GPathPoint pt4)
	{
		helperList.Clear();
		helperList.Add(pt1);
		helperList.Add(pt2);
		helperList.Add(pt3);
		helperList.Add(pt4);
		Create(helperList);
	}

	public void Create(IEnumerable<GPathPoint> points)
	{
		_segments.Clear();
		_points.Clear();
		splinePoints.Clear();
		_fullLength = 0f;
		IEnumerator<GPathPoint> enumerator = points.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		GPathPoint gPathPoint = enumerator.Current;
		if (gPathPoint.curveType == GPathPoint.CurveType.CRSpline)
		{
			splinePoints.Add(gPathPoint.pos);
		}
		while (enumerator.MoveNext())
		{
			GPathPoint current = enumerator.Current;
			if (gPathPoint.curveType != GPathPoint.CurveType.CRSpline)
			{
				Segment item = new Segment
				{
					type = gPathPoint.curveType,
					ptStart = _points.Count
				};
				if (gPathPoint.curveType == GPathPoint.CurveType.Straight)
				{
					item.ptCount = 2;
					_points.Add(gPathPoint.pos);
					_points.Add(current.pos);
				}
				else if (gPathPoint.curveType == GPathPoint.CurveType.Bezier)
				{
					item.ptCount = 3;
					_points.Add(gPathPoint.pos);
					_points.Add(current.pos);
					_points.Add(gPathPoint.control1);
				}
				else if (gPathPoint.curveType == GPathPoint.CurveType.CubicBezier)
				{
					item.ptCount = 4;
					_points.Add(gPathPoint.pos);
					_points.Add(current.pos);
					_points.Add(gPathPoint.control1);
					_points.Add(gPathPoint.control2);
				}
				item.length = Vector3.Distance(gPathPoint.pos, current.pos);
				_fullLength += item.length;
				_segments.Add(item);
			}
			if (current.curveType != GPathPoint.CurveType.CRSpline)
			{
				if (splinePoints.Count > 0)
				{
					splinePoints.Add(current.pos);
					CreateSplineSegment();
				}
			}
			else
			{
				splinePoints.Add(current.pos);
			}
			gPathPoint = current;
		}
		if (splinePoints.Count > 1)
		{
			CreateSplineSegment();
		}
	}

	private void CreateSplineSegment()
	{
		int count = splinePoints.Count;
		splinePoints.Insert(0, splinePoints[0]);
		splinePoints.Add(splinePoints[count]);
		splinePoints.Add(splinePoints[count]);
		count += 3;
		Segment item = new Segment
		{
			type = GPathPoint.CurveType.CRSpline,
			ptStart = _points.Count,
			ptCount = count
		};
		_points.AddRange(splinePoints);
		item.length = 0f;
		for (int i = 1; i < count; i++)
		{
			item.length += Vector3.Distance(splinePoints[i - 1], splinePoints[i]);
		}
		_fullLength += item.length;
		_segments.Add(item);
		splinePoints.Clear();
	}

	public void Clear()
	{
		_segments.Clear();
		_points.Clear();
	}

	public Vector3 GetPointAt(float t)
	{
		t = Mathf.Clamp01(t);
		int count = _segments.Count;
		if (count == 0)
		{
			return Vector3.zero;
		}
		if (t == 1f)
		{
			Segment segment = _segments[count - 1];
			if (segment.type == GPathPoint.CurveType.Straight)
			{
				return Vector3.Lerp(_points[segment.ptStart], _points[segment.ptStart + 1], t);
			}
			if (segment.type == GPathPoint.CurveType.Bezier || segment.type == GPathPoint.CurveType.CubicBezier)
			{
				return onBezierCurve(segment.ptStart, segment.ptCount, t);
			}
			return onCRSplineCurve(segment.ptStart, segment.ptCount, t);
		}
		float num = t * _fullLength;
		Vector3 result = default(Vector3);
		for (int i = 0; i < count; i++)
		{
			Segment segment = _segments[i];
			num -= segment.length;
			if (num < 0f)
			{
				t = 1f + num / segment.length;
				if (segment.type == GPathPoint.CurveType.Straight)
				{
					return Vector3.Lerp(_points[segment.ptStart], _points[segment.ptStart + 1], t);
				}
				if (segment.type == GPathPoint.CurveType.Bezier || segment.type == GPathPoint.CurveType.CubicBezier)
				{
					return onBezierCurve(segment.ptStart, segment.ptCount, t);
				}
				return onCRSplineCurve(segment.ptStart, segment.ptCount, t);
			}
		}
		return result;
	}

	public float GetSegmentLength(int segmentIndex)
	{
		return _segments[segmentIndex].length;
	}

	public void GetPointsInSegment(int segmentIndex, float t0, float t1, List<Vector3> points, List<float> ts = null, float pointDensity = 0.1f)
	{
		if (points == null)
		{
			points = new List<Vector3>();
		}
		ts?.Add(t0);
		Segment segment = _segments[segmentIndex];
		if (segment.type == GPathPoint.CurveType.Straight)
		{
			points.Add(Vector3.Lerp(_points[segment.ptStart], _points[segment.ptStart + 1], t0));
			points.Add(Vector3.Lerp(_points[segment.ptStart], _points[segment.ptStart + 1], t1));
		}
		else if (segment.type == GPathPoint.CurveType.Bezier || segment.type == GPathPoint.CurveType.CubicBezier)
		{
			points.Add(onBezierCurve(segment.ptStart, segment.ptCount, t0));
			int num = (int)Mathf.Min(segment.length * pointDensity, 50f);
			for (int i = 0; i <= num; i++)
			{
				float num2 = (float)i / (float)num;
				if (num2 > t0 && num2 < t1)
				{
					points.Add(onBezierCurve(segment.ptStart, segment.ptCount, num2));
					ts?.Add(num2);
				}
			}
			points.Add(onBezierCurve(segment.ptStart, segment.ptCount, t1));
		}
		else
		{
			points.Add(onCRSplineCurve(segment.ptStart, segment.ptCount, t0));
			int num3 = (int)Mathf.Min(segment.length * pointDensity, 50f);
			for (int j = 0; j <= num3; j++)
			{
				float num4 = (float)j / (float)num3;
				if (num4 > t0 && num4 < t1)
				{
					points.Add(onCRSplineCurve(segment.ptStart, segment.ptCount, num4));
					ts?.Add(num4);
				}
			}
			points.Add(onCRSplineCurve(segment.ptStart, segment.ptCount, t1));
		}
		ts?.Add(t1);
	}

	public void GetAllPoints(List<Vector3> points, float pointDensity = 0.1f)
	{
		int count = _segments.Count;
		for (int i = 0; i < count; i++)
		{
			GetPointsInSegment(i, 0f, 1f, points, null, pointDensity);
		}
	}

	private Vector3 onCRSplineCurve(int ptStart, int ptCount, float t)
	{
		int num = Mathf.FloorToInt(t * (float)(ptCount - 4)) + ptStart;
		Vector3 result = default(Vector3);
		Vector3 vector = _points[num];
		Vector3 vector2 = _points[num + 1];
		Vector3 vector3 = _points[num + 2];
		Vector3 vector4 = _points[num + 3];
		float num2 = ((t == 1f) ? 1f : Mathf.Repeat(t * (float)(ptCount - 4), 1f));
		float num3 = ((0f - num2 + 2f) * num2 - 1f) * num2 * 0.5f;
		float num4 = ((3f * num2 - 5f) * num2 * num2 + 2f) * 0.5f;
		float num5 = ((-3f * num2 + 4f) * num2 + 1f) * num2 * 0.5f;
		float num6 = (num2 - 1f) * num2 * num2 * 0.5f;
		result.x = vector.x * num3 + vector2.x * num4 + vector3.x * num5 + vector4.x * num6;
		result.y = vector.y * num3 + vector2.y * num4 + vector3.y * num5 + vector4.y * num6;
		result.z = vector.z * num3 + vector2.z * num4 + vector3.z * num5 + vector4.z * num6;
		return result;
	}

	private Vector3 onBezierCurve(int ptStart, int ptCount, float t)
	{
		float num = 1f - t;
		Vector3 vector = _points[ptStart];
		Vector3 vector2 = _points[ptStart + 1];
		Vector3 vector3 = _points[ptStart + 2];
		if (ptCount == 4)
		{
			Vector3 vector4 = _points[ptStart + 3];
			return num * num * num * vector + 3f * num * num * t * vector3 + 3f * num * t * t * vector4 + t * t * t * vector2;
		}
		return num * num * vector + 2f * num * t * vector3 + t * t * vector2;
	}
}
