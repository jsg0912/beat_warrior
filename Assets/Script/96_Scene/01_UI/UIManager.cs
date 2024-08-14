using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject HP;
    private GameObject HPPrefab;
    private List<Image> HPList;

    [SerializeField] private Image MarkImg;
    [SerializeField] private Image DashImg;
    [SerializeField] private Image Skill1Img;
    [SerializeField] private Image Skill2Img;

    public TextMeshProUGUI[] txt;
    public GameObject menuSet;

    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }

        InitializeHP();
    }

    private void Update()
    {
        UpdateHP();
        UpdateCoolTime();
        AppearGameSet();


        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void InitializeHP()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        HPPrefab = Resources.Load("Prefab/HP") as GameObject;
        HPList = new List<Image>();

        for (int i = 0; i < PlayerConstant.hpMax; i++)
        {
            AddHPUI();
        }
    }

    public void AddHPUI()
    {
        GameObject hp = Instantiate(HPPrefab);
        hp.transform.SetParent(HP.transform, false);
        HPList.Add(hp.transform.GetChild(0).GetComponent<Image>());
    }

    public void RemoveHPUI()
    {
        Destroy(HP.transform.GetChild(0).gameObject);
        HPList.RemoveAt(0);
    }

    private void UpdateHP()
    {
        int hp = Player.Instance.GetCurrentHP();

        if (hp == Player.Instance.GetFinalStat(StatKind.HP)) return;

        if (hp == 0)
        {
            foreach (Image image in HPList) image.fillAmount = 1;
            return;
        }

        for (int i = 0; i < PlayerConstant.hpMax; i++)
        {
            if (i > hp) HPList[i].fillAmount = 1;
            else if (i < hp) HPList[i].fillAmount = 0;
            else HPList[i].fillAmount
                    = Player.Instance.GetSkillCoolTime(SkillName.KillRecoveryHP) / PlayerSkillConstant.recoveryHPTimeMax;
        }
    }

    private void UpdateCoolTime()
    {
        MarkImg.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Mark) / PlayerSkillConstant.markCoolTimeMax;
        DashImg.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Dash) / PlayerSkillConstant.dashCoolTimeMax;
        Skill1Img.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Skill1) / PlayerSkillConstant.skill1CoolTimeMax;
        Skill2Img.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Skill2) / PlayerSkillConstant.skill2CoolTimeMax;
    }

    private void AppearGameSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuSet.SetActive(true);
        }
    }
}
