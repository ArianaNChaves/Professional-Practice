using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    [SerializeField] private bool dontDestroyOnLoad = true;

    private static T _instance;
    private static bool _wasDestroyed = false;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();

                if (!_instance && !_wasDestroyed)
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return _instance;
        }
    }
    
    private void Awake ()
    {
        if (Instance == this)
        {
            _wasDestroyed = false;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            OnAwaken();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy ()
    {
        if (_instance == this)
        {
            _wasDestroyed = true;
            _instance = null;

            OnDestroyed();
        }
    }

    protected virtual void OnAwaken () { }

    protected virtual void OnDestroyed () { }
}