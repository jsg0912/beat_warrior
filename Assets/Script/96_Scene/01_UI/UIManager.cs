using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Language language = Language.kr;
    public static UIManager Instance;
    public GameObject AltarPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


    }

    public static void CreateUI()
    {
        GameObject UI = Instantiate(Resources.Load<GameObject>(PrefabRouter.UIPrefab));
        DontDestroyOnLoad(UI);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SceneReset();
        }
    }

    private void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

