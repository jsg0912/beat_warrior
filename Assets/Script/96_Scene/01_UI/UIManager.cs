using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Language language = Language.kr;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

}