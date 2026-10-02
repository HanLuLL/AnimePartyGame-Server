using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Core.Tutorial;

public static class TutorialGame
{
	private static readonly Dictionary<Type, IController> _controllers = new Dictionary<Type, IController>();

	private static readonly Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

	private static readonly Dictionary<Type, IModel> _models = new Dictionary<Type, IModel>();

	private static readonly Queue<ITick> _tickQueue = new Queue<ITick>();

	private static readonly Stack<IDispose> _disposeStack = new Stack<IDispose>();

	public static void Tick()
	{
		foreach (ITick item in _tickQueue)
		{
			item.Tick();
		}
	}

	public static void Dispose()
	{
		while (_disposeStack.Count > 0)
		{
			_disposeStack.Pop().Dispose();
		}
		_controllers.Clear();
		_systems.Clear();
		_models.Clear();
		_tickQueue.Clear();
		_disposeStack.Clear();
	}

	public static async UniTask RegisterController<T>() where T : class, IController, new()
	{
		await RegisterController(new T());
	}

	public static async UniTask RegisterController<T>(T instance) where T : class, IController
	{
		Type typeFromHandle = typeof(T);
		if (!_controllers.TryAdd(typeFromHandle, instance))
		{
			throw new Exception("Controller already registered: " + typeFromHandle.Name);
		}
		if (instance is IInitialize initialize)
		{
			await initialize.Initialize();
		}
		if (instance is ITick item)
		{
			_tickQueue.Enqueue(item);
		}
		if (instance is IDispose item2)
		{
			_disposeStack.Push(item2);
		}
	}

	public static async UniTask RegisterController<T, A>(A a) where T : class, IController, new()
	{
		await RegisterController(new T(), a);
	}

	public static async UniTask RegisterController<T, A>(T instance, A a) where T : class, IController
	{
		Type typeFromHandle = typeof(T);
		if (!_controllers.TryAdd(typeFromHandle, instance))
		{
			throw new Exception("Controller already registered: " + typeFromHandle.Name);
		}
		if (instance is IInitialize<A> initialize)
		{
			await initialize.Initialize(a);
		}
		if (instance is ITick item)
		{
			_tickQueue.Enqueue(item);
		}
		if (instance is IDispose item2)
		{
			_disposeStack.Push(item2);
		}
	}

	public static T GetController<T>() where T : class, IController
	{
		Type typeFromHandle = typeof(T);
		if (_controllers.TryGetValue(typeFromHandle, out var value))
		{
			return value as T;
		}
		return null;
	}

	public static async UniTask RegisterSystem<T>() where T : class, ISystem, new()
	{
		await RegisterSystem(new T());
	}

	public static async UniTask RegisterSystem<T>(T instance) where T : class, ISystem
	{
		Type typeFromHandle = typeof(T);
		if (!_systems.TryAdd(typeFromHandle, instance))
		{
			throw new Exception("System already registered: " + typeFromHandle.Name);
		}
		if (instance is IInitialize initialize)
		{
			await initialize.Initialize();
		}
		if (instance is ITick item)
		{
			_tickQueue.Enqueue(item);
		}
		if (instance is IDispose item2)
		{
			_disposeStack.Push(item2);
		}
	}

	public static async UniTask RegisterSystem<T, A>(A a) where T : class, ISystem, new()
	{
		await RegisterSystem(new T(), a);
	}

	public static async UniTask RegisterSystem<T, A>(T instance, A a) where T : class, ISystem
	{
		Type typeFromHandle = typeof(T);
		if (!_systems.TryAdd(typeFromHandle, instance))
		{
			throw new Exception("System already registered: " + typeFromHandle.Name);
		}
		if (instance is IInitialize<A> initialize)
		{
			await initialize.Initialize(a);
		}
		if (instance is ITick item)
		{
			_tickQueue.Enqueue(item);
		}
		if (instance is IDispose item2)
		{
			_disposeStack.Push(item2);
		}
	}

	public static T GetSystem<T>() where T : class, ISystem
	{
		Type typeFromHandle = typeof(T);
		if (_systems.TryGetValue(typeFromHandle, out var value))
		{
			return value as T;
		}
		return null;
	}

	public static async UniTask RegisterModel<T>() where T : class, IModel, new()
	{
		await RegisterModel(new T());
	}

	public static async UniTask RegisterModel<T>(T instance) where T : class, IModel
	{
		Type typeFromHandle = typeof(T);
		if (!_models.TryAdd(typeFromHandle, instance))
		{
			throw new Exception("Model already registered: " + typeFromHandle.Name);
		}
		if (instance is IInitialize initialize)
		{
			await initialize.Initialize();
		}
		if (instance is ITick item)
		{
			_tickQueue.Enqueue(item);
		}
		if (instance is IDispose item2)
		{
			_disposeStack.Push(item2);
		}
	}

	public static async UniTask RegisterModel<T, A>(A a) where T : class, IModel, new()
	{
		await RegisterModel(new T(), a);
	}

	public static async UniTask RegisterModel<T, A>(T instance, A a) where T : class, IModel
	{
		Type typeFromHandle = typeof(T);
		if (!_models.TryAdd(typeFromHandle, instance))
		{
			throw new Exception("Model already registered: " + typeFromHandle.Name);
		}
		if (instance is IInitialize<A> initialize)
		{
			await initialize.Initialize(a);
		}
		if (instance is ITick item)
		{
			_tickQueue.Enqueue(item);
		}
		if (instance is IDispose item2)
		{
			_disposeStack.Push(item2);
		}
	}

	public static T GetModel<T>() where T : class, IModel
	{
		Type typeFromHandle = typeof(T);
		if (_models.TryGetValue(typeFromHandle, out var value))
		{
			return value as T;
		}
		return null;
	}
}
