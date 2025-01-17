using TMPro;
using UnityEngine;

public class MenuUI : PopupSystem
{
    public static MenuUI Instance;
    public GameObject menu;
    [SerializeField] private GameObject setting;
    public TextMeshProUGUI[] txt;
    public AltarPopup altarPopup;

    public override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public override void TurnOnPopup()
    {
        base.TurnOnPopup();
        Util.SetActive(menu, true);
    }

    public void OnClickClose()
    {
        TurnOffPopup();
    }

    public void OnClickSetting()
    {
        Util.SetActive(setting, true);
    }

    public void OnClickReStart()
    {
        GameManager.Instance.RestartGame();
    }
}