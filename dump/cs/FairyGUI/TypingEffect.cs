using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public class TypingEffect
{
	protected TextField _textField;

	protected Vector3[] _backupVerts;

	protected Vector3[] _vertices;

	protected bool _stroke;

	protected bool _shadow;

	protected int _printIndex;

	protected int _mainLayerStart;

	protected int _strokeLayerStart;

	protected int _strokeDrawDirs;

	protected int _vertIndex;

	protected int _mainLayerVertCount;

	protected bool _started;

	public TypingEffect(TextField textField)
	{
		_textField = textField;
		_textField.EnableCharPositionSupport();
	}

	public TypingEffect(GTextField textField)
	{
		if (textField is GRichTextField)
		{
			_textField = ((RichTextField)textField.displayObject).textField;
		}
		else
		{
			_textField = (TextField)textField.displayObject;
		}
		_textField.EnableCharPositionSupport();
	}

	public void Start()
	{
		_textField.graphics.meshModifier -= OnMeshModified;
		_textField.Redraw();
		_textField.graphics.meshModifier += OnMeshModified;
		_stroke = false;
		_shadow = false;
		_strokeDrawDirs = 4;
		_mainLayerStart = 0;
		_mainLayerVertCount = 0;
		_printIndex = 0;
		_vertIndex = 0;
		_started = true;
		int vertexCount = _textField.graphics.mesh.vertexCount;
		_backupVerts = _textField.graphics.mesh.vertices;
		if (_vertices == null || _vertices.Length != vertexCount)
		{
			_vertices = new Vector3[vertexCount];
		}
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < vertexCount; i++)
		{
			_vertices[i] = zero;
		}
		_textField.graphics.mesh.vertices = _vertices;
		if (_textField.richTextField != null)
		{
			int htmlElementCount = _textField.richTextField.htmlElementCount;
			for (int j = 0; j < htmlElementCount; j++)
			{
				_textField.richTextField.ShowHtmlObject(j, show: false);
			}
		}
		int count = _textField.charPositions.Count;
		for (int k = 0; k < count; k++)
		{
			_mainLayerVertCount += _textField.charPositions[k].vertCount;
		}
		if (_mainLayerVertCount < vertexCount)
		{
			int num = vertexCount / _mainLayerVertCount;
			_stroke = num > 2;
			_shadow = num % 2 == 0;
			_mainLayerStart = vertexCount - vertexCount / num;
			_strokeLayerStart = (_shadow ? (vertexCount / num) : 0);
			_strokeDrawDirs = ((num > 8) ? 8 : 4);
		}
	}

	public bool Print()
	{
		if (!_started)
		{
			return false;
		}
		List<TextField.CharPosition> charPositions = _textField.charPositions;
		int count = charPositions.Count;
		while (_printIndex < count - 1)
		{
			TextField.CharPosition charPosition = charPositions[_printIndex++];
			if (charPosition.vertCount > 0)
			{
				output(charPosition.vertCount);
			}
			if (charPosition.imgIndex > 0)
			{
				_textField.richTextField.ShowHtmlObject(charPosition.imgIndex - 1, show: true);
				return true;
			}
			if (!char.IsWhiteSpace(_textField.parsedText[_printIndex - 1]))
			{
				return true;
			}
		}
		Cancel();
		return false;
	}

	private void output(int vertCount)
	{
		int num = _mainLayerStart + _vertIndex;
		int num2 = num + vertCount;
		for (int i = num; i < num2; i++)
		{
			_vertices[i] = _backupVerts[i];
		}
		if (_stroke)
		{
			int num3 = _strokeLayerStart + _vertIndex;
			num2 = num3 + vertCount;
			for (int j = num3; j < num2; j++)
			{
				for (int k = 0; k < _strokeDrawDirs; k++)
				{
					int num4 = j + _mainLayerVertCount * k;
					_vertices[num4] = _backupVerts[num4];
				}
			}
		}
		if (_shadow)
		{
			int vertIndex = _vertIndex;
			num2 = vertIndex + vertCount;
			for (int l = vertIndex; l < num2; l++)
			{
				_vertices[l] = _backupVerts[l];
			}
		}
		_textField.graphics.mesh.vertices = _vertices;
		_vertIndex += vertCount;
	}

	public IEnumerator Print(float interval)
	{
		while (Print())
		{
			yield return new WaitForSeconds(interval);
		}
	}

	public void PrintAll(float interval)
	{
		Timers.inst.StartCoroutine(Print(interval));
	}

	public void Cancel()
	{
		if (_started)
		{
			_started = false;
			_textField.graphics.meshModifier -= OnMeshModified;
			_textField.graphics.SetMeshDirty();
		}
	}

	private void OnMeshModified()
	{
		if (_textField.graphics.mesh.vertexCount != _backupVerts.Length)
		{
			Cancel();
			return;
		}
		_backupVerts = _textField.graphics.mesh.vertices;
		int num = _vertices.Length;
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < num; i++)
		{
			if (_vertices[i] != zero)
			{
				_vertices[i] = _backupVerts[i];
			}
		}
		_textField.graphics.mesh.vertices = _vertices;
	}

	public int ChildCount()
	{
		if (_textField?.charPositions == null)
		{
			return 0;
		}
		return _textField.charPositions.Count;
	}
}
