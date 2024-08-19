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
    [SerializeField] private Image AttackImg;

    public TextMeshProUGUI[] txt;
    public GameObject menuSet;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        HPPrefab = Resources.Load("Prefab/HP") as GameObject;
        HPList = new();
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
        AppearGameSet();

        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
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
        int hp = Player.Instance.GetCurrentHP();

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
        MarkImg.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Mark) / PlayerSkillConstant.markCoolTimeMax;
        DashImg.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Dash) / PlayerSkillConstant.dashCoolTimeMax;
        Skill1Img.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Skill1) / PlayerSkillConstant.skill1CoolTimeMax;
        Skill2Img.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Skill2) / PlayerSkillConstant.skill2CoolTimeMax;
        AttackImg.GetComponent<Image>().fillAmount
            = 1 - Player.Instance.GetSkillCoolTime(SkillName.Attack) / PlayerSkillConstant.attackChargeTimeMax;
    }

    private void AppearGameSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuSet.SetActive(true);
        }
    }
}
