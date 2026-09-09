using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    [SerializeField] private bool _dontDestroyOnLoad = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    private void OnDestroy()
    {
        if (Instance != this) return;

        OnSingletonDestroyed();
        Instance = null;
    }

    protected virtual void OnSingletonDestroyed()
    {
    }
}
