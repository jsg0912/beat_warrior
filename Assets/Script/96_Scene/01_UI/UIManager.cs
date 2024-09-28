using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Language language = Language.kr;


    private void Start()
    {
        //CheckInstance();
        Instance = this;
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

    private void CheckInstance()
    {
        if (Instance != null && Instance != this) Destroy(Instance.gameObject);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

