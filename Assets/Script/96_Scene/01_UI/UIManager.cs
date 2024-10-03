using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Language language = Language.kr;
    public static UIManager Instance;
    public GameObject UIPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (UIPrefab != null)
        {
            GameObject UI = Instantiate(UIPrefab);
            DontDestroyOnLoad (UI);
        }
    }

    

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.M))
        {
            SceneReset();
        }
    }

    private void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}

