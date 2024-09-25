using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Language language = Language.kr;


    private void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(this.gameObject);

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

