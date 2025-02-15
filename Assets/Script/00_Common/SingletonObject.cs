using UnityEngine;

/*
    TODO: 생성과정을 보면, 없으면 무조건적으로 강제로 생성하는데, 이것이 문제가 될 수 있다.
    Solution: 무조건적으로 생성하는 Object와 그렇지 않은 Object를 구분한 Class로 나누어야 한다.
    Ex: PlayerHPUIController는 무조건적으로 생성되어야 하지 않기 때문에, SingletonObject를 상속받지 않는데, 기능적인건 대부분 필요한 상황임.
*/
public abstract class SingletonObject<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            TryCreateSingletonObject();
            return _instance;
        }
    }

    public static T InstanceWithoutCreate
    {
        get
        {
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = gameObject.GetComponent<T>();
            if (Util.IsRootGameObject(gameObject)) DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) // 중복 방지: 기존 인스턴스와 다르면 제거
        {
            DebugConsole.Warning($"[SingletonObject] 중복된 {typeof(T).Name} 인스턴스가 발견되어 제거됩니다.");
            Destroy(gameObject);
            return;
        }
    }

    public static void TryCreateSingletonObject()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                Debug.LogWarning($"[SingletonObject] {typeof(T).Name}이(가) 존재하지 않아서 새로운 인스턴스를 생성합니다.");
                GameObject go = new GameObject(typeof(T).ToString());
                _instance = go.AddComponent<T>();

                if (Util.IsRootGameObject(go)) DontDestroyOnLoad(go);
            }
        }
    }
}