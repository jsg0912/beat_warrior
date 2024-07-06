using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "PlayBtn":
                SceneManager.LoadScene("Player");
                break;
            case "SettingBtn":
                SceneManager.LoadScene("Setting");
                break;
            case "ExitBtn":
                Application.Quit();
                break;
        }
    }

   
}
