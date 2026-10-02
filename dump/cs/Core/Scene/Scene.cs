namespace Core.Scene;

public class Scene
{
	public SceneType sceneType { get; private set; }

	public SceneStateType stateType { get; private set; }

	public string sceneName { get; private set; }

	public Scene()
	{
		sceneType = SceneType.None;
		stateType = SceneStateType.None;
		sceneName = null;
	}

	public void UpdateSceneType(SceneType newSceneType)
	{
		sceneType = newSceneType;
	}

	public void UpdateStateType(SceneStateType newStateType)
	{
		stateType = newStateType;
	}

	public void UpdateSceneName(string newName)
	{
		sceneName = newName;
	}
}
