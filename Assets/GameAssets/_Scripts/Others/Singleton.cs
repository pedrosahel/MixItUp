using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance;

	protected void Awake()
	{
		if (Instance != null) Destroy(gameObject);
		Instance = this as T;
	}

	protected virtual void OnApplicationQuit()
	{
		Instance = null;
		Destroy(gameObject);
	}
}