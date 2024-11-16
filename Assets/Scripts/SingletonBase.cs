using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}