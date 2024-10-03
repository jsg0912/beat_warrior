using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ButtonPanelInfo
{
    public int buttonID;
    public Button button;
    public GameObject panel;
}

public class SettingOption : MonoBehaviour
{

    public ButtonPanelInfo[] buttonPanelInfos;

    public int CurrentID;

    private GameObject activePanel;

    void Start()
    {

        foreach (var info in buttonPanelInfos)
        {
            Button btn = info.button;
            int id = info.buttonID;
            btn.onClick.AddListener(() => OnButtonClick(id));
        }
    }

    public void OnButtonClick(int buttonID)
    {

        foreach (var info in buttonPanelInfos)
        {
            if (info.buttonID == buttonID)
            {

                if (activePanel != null)
                {
                    activePanel.SetActive(false);
                }


                info.panel.SetActive(true);
                activePanel = info.panel;
                CurrentID = info.buttonID;
                break;
            }
        }
    }
}