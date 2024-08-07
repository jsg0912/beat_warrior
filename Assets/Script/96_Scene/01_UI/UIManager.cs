using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
            txt[i].text = KeySetting.keys[(ACTION)i].ToString();
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
            txt[i].text = KeySetting.keys[(ACTION)i].ToString();
        }
    }

    private void InitializeHP()
    {
        HPPrefab = Resources.Load("Prefab/HP") as GameObject;
        HPList = new List<Image>();

        for (int i = 0; i < PlayerConstant.hpMax; i++)
        {
            GameObject hp = Instantiate(HPPrefab);
            hp.transform.SetParent(HP.transform, false);
            HPList.Add(hp.transform.GetChild(0).GetComponent<Image>());
        }
    }

    private void UpdateHP()
    {
        int hp = Player.Instance.GetHP();

        if (hp == PlayerConstant.hpMax) return;

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
                    = Player.Instance.GetSkillCoolTime(PLAYERSKILLNAME.RECOVERYHP) / PlayerSkillConstant.recoveryHPTimeMax;
        }
    }

    private void UpdateCoolTime()
    {
        MarkImg.GetComponent<Image>().fillAmount 
            = 1 - Player.Instance.GetSkillCoolTime(PLAYERSKILLNAME.MARK) / PlayerSkillConstant.markCoolTimeMax;
        DashImg.GetComponent<Image>().fillAmount 
            = 1 - Player.Instance.GetSkillCoolTime(PLAYERSKILLNAME.DASH) / PlayerSkillConstant.dashCoolTimeMax;
        Skill1Img.GetComponent<Image>().fillAmount 
            = 1 - Player.Instance.GetSkillCoolTime(PLAYERSKILLNAME.SKILL1) / PlayerSkillConstant.skill1CoolTimeMax;
        Skill2Img.GetComponent<Image>().fillAmount 
            = 1 - Player.Instance.GetSkillCoolTime(PLAYERSKILLNAME.SKILL2) / PlayerSkillConstant.skill2CoolTimeMax;
    }

    private void AppearGameSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuSet.SetActive(true);
        }
    }
}
