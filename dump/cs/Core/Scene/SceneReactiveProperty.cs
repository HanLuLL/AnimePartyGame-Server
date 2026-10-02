using System.Collections.Generic;
using Tools;

namespace Core.Scene;

public class SceneReactiveProperty : ReactiveProperty<Scene>
{
	private class SceneEqualityComparer : IEqualityComparer<Scene>
	{
		public bool Equals(Scene x, Scene y)
		{
			if (x != null && y != null && x.sceneType == y.sceneType && x.stateType == y.stateType)
			{
				return x.sceneName == y.sceneName;
			}
			return false;
		}

		public int GetHashCode(Scene obj)
		{
			return obj.sceneType.GetHashCode() ^ (obj.stateType.GetHashCode() << 2) ^ (obj.sceneName.GetHashCode() >> 2);
		}
	}

	private static readonly SceneEqualityComparer _comparer = new SceneEqualityComparer();

	private readonly Scene _scene;

	protected override IEqualityComparer<Scene> EqualityComparer => _comparer;

	public SceneReactiveProperty()
	{
		_scene = new Scene();
	}

	public SceneReactiveProperty(Scene scene)
		: base(scene)
	{
		_scene = scene;
	}

	public void UpdateScene(SceneType newSceneType, SceneStateType newStateType, string newName)
	{
		bool num = _scene.sceneType != newSceneType || _scene.stateType != newStateType || _scene.sceneName != newName;
		_scene.UpdateSceneType(newSceneType);
		_scene.UpdateStateType(newStateType);
		_scene.UpdateSceneName(newName);
		if (num)
		{
			SetValueAndForceDispath(_scene);
		}
	}

	public void UpdateStateType(SceneStateType newStateType)
	{
		bool num = _scene.stateType != newStateType;
		_scene.UpdateStateType(newStateType);
		if (num)
		{
			SetValueAndForceDispath(_scene);
		}
	}
}
