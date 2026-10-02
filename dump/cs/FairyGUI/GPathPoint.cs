using System;
using UnityEngine;

namespace FairyGUI;

[Serializable]
public struct GPathPoint
{
	public enum CurveType
	{
		CRSpline,
		Bezier,
		CubicBezier,
		Straight
	}

	public Vector3 pos;

	public Vector3 control1;

	public Vector3 control2;

	public CurveType curveType;

	public bool smooth;

	public GPathPoint(Vector3 pos)
	{
		this.pos = pos;
		control1 = Vector3.zero;
		control2 = Vector3.zero;
		curveType = CurveType.CRSpline;
		smooth = true;
	}

	public GPathPoint(Vector3 pos, Vector3 control)
	{
		this.pos = pos;
		control1 = control;
		control2 = Vector3.zero;
		curveType = CurveType.Bezier;
		smooth = true;
	}

	public GPathPoint(Vector3 pos, Vector3 control1, Vector3 control2)
	{
		this.pos = pos;
		this.control1 = control1;
		this.control2 = control2;
		curveType = CurveType.CubicBezier;
		smooth = true;
	}

	public GPathPoint(Vector3 pos, CurveType curveType)
	{
		this.pos = pos;
		control1 = Vector3.zero;
		control2 = Vector3.zero;
		this.curveType = curveType;
		smooth = true;
	}
}
