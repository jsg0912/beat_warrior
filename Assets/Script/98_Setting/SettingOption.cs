using System.Linq;
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
        foreach (var buttonPanelInfo in buttonPanelInfos)
        {
            Button btn = buttonPanelInfo.button;
            btn.onClick.AddListener(() => OnButtonClick(buttonPanelInfo.buttonID));
        }
    }

    public void OnButtonClick(int buttonID)
    {
        ButtonPanelInfo buttonPanelInfo = buttonPanelInfos.FirstOrDefault(x => x.buttonID == buttonID);

        if (buttonPanelInfo != null)
        {
            Util.SetActive(activePanel, false);
            Util.SetActive(buttonPanelInfo.panel, true);
            activePanel = buttonPanelInfo.panel;
            CurrentID = buttonPanelInfo.buttonID;
            return;
        }
    }
}