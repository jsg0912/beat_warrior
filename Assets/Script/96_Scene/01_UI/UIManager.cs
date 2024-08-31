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

    [SerializeField] private List<Image> SkillCoolTimeImgList;
    private Dictionary<SkillName, Image> SkillCoolTimeImg = new();

    [SerializeField] private GameObject Setting;
    [SerializeField] private GameObject Altar;
    [SerializeField] private TextMeshProUGUI SpiritText;

    public TextMeshProUGUI[] txt;
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

        SkillCoolTimeImg.Add(SkillName.Mark, SkillCoolTimeImgList[0]);
        SkillCoolTimeImg.Add(SkillName.Dash, SkillCoolTimeImgList[1]);
        SkillCoolTimeImg.Add(SkillName.Skill1, SkillCoolTimeImgList[2]);
        SkillCoolTimeImg.Add(SkillName.Skill2, SkillCoolTimeImgList[3]);
        SkillCoolTimeImg.Add(SkillName.Attack, SkillCoolTimeImgList[4]);
    }

    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void Update()
    {
        UpdateCoolTime();
        if (Input.GetKeyDown(KeyCode.Escape)) SetMenuActive();

        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
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
        foreach (var skill in SkillCoolTimeImg)
            skill.Value.fillAmount
                = 1 - Player.Instance.GetSkillCoolTime(skill.Key) / PlayerSkillConstant.SkillCoolTime[skill.Key];


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

        SpiritText.text = Inventory.Instance.GetSpiritNumber().ToString();
    }
}