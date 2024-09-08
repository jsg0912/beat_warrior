using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Language language = Language.kr;

    [SerializeField] private GameObject HP;
    private GameObject HPPrefab;
    private List<Image> HPList;
    [SerializeField] Text AttackCountView;

    [SerializeField] private GameObject Setting;
    [SerializeField] private GameObject Altar;
    [SerializeField] private TextMeshProUGUI SoulText;

    public GameObject Menu;

    private bool isMenuActive = false;
    private bool isSettingActive = false;
    private bool isAltarActive = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        HPPrefab = Resources.Load("Prefab/PlayerHP") as GameObject;
        HPList = new();
    }

    private void Update()
    {
        UpdateCoolTime();
        if (Input.GetKeyDown(KeyCode.Escape)) SetMenuActive();
    }

    public void SetAndUpdateHPUI(int hp)
    {
        SetHPUI(hp);
        UpdateHPUI();
    }

    public void SetHPUI(int hp)
    {
        HPList.Clear();
        foreach (Transform child in HP.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hp; i++)
        {
            GameObject hpui = Instantiate(HPPrefab);
            hpui.transform.SetParent(HP.transform, false);
            HPList.Add(hpui.transform.GetChild(0).GetComponent<Image>());
        }
    }

    public void UpdateHPUI()
    {
        int hp = Player.Instance.GetCurrentStat(StatKind.HP);

        if (hp == 0)
        {
            foreach (Image image in HPList) image.gameObject.SetActive(true);
            return;
        }

        for (int i = 0; i < Player.Instance.GetFinalStat(StatKind.HP); i++)
        {
            if (i < hp) HPList[i].gameObject.SetActive(false);
            else HPList[i].gameObject.SetActive(true);
        }
    }

    private void UpdateCoolTime()
    {
        AttackCountView.text = Player.Instance.GetCurrentStat(StatKind.AttackCount).ToString();
    }

    private void SetMenuActive()
    {
        isMenuActive = !isMenuActive;
        Menu.SetActive(isMenuActive);

        if (isMenuActive == false && isSettingActive == true) SetSettingActive();
    }

    public void SetSettingActive()
    {
        isSettingActive = !isSettingActive;
        Setting.SetActive(isSettingActive);
    }

    public void SetAltarActive()
    {
        isAltarActive = !isAltarActive;
        Altar.SetActive(isAltarActive);

        SoulText.text = Inventory.Instance.GetSoulNumber().ToString();
    }
}